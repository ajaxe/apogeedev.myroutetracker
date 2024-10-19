using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using MyRouteTracker.Web.Abstractions;
using MyRouteTracker.Web.Abstractions.Services;
using MyRouteTracker.Web.Helpers;
using MyRouteTracker.Web.Helpers.Configuration;
using MyRouteTracker.Web.Services;
using Serilog;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.Claims;
using Microsoft.IdentityModel.Logging;
using Microsoft.AspNetCore.HttpOverrides;

namespace MyRouteTracker.Web;

public class Startup
{
    public const string EnvVarPrefix = "APP_";
    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Env { get; }
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        Env = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSerilog((s, lc) => lc.ReadFrom.Configuration(Configuration));
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        services.AddHealthChecks();

        services.Configure<AppOptions>(
            Configuration.GetSection(AppOptions.SectionName)
        );
        services.Configure<OAuthOptions>(
            Configuration.GetSection(OAuthOptions.SectionName)
        );

        // Add services to the container.
        services.AddControllersWithViews();


        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders =
                ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.RequireHeaderSymmetry = false;
            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
        });

        var authBuilder = services.AddAuthentication(o =>
        {
            o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        })
        .AddCookie(o =>
        {
            o.LoginPath = "/login";
            o.LogoutPath = "/logout";
            o.Cookie.Name = "oidc";
            o.Cookie.SameSite = SameSiteMode.None;
            o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            o.Cookie.IsEssential = true;
        });

        ConfigureOpenIdConnect(services, authBuilder);

        services.AddOptions();

        services.AddScoped<IUserContextProvider, UserContextProvider>();
        services.AddTransient<IRouteDataService, RouteDataService>();
        services.AddTransient<IDataIngestionService, DataIngestionService>();
        services.AddTransient<OpenIdConnectEventsHandler>();

        services.AddDbContext<AppDbContext>((sp, o) =>
            {
                var opts = sp.GetRequiredService<IOptionsSnapshot<AppOptions>>().Value;
                o.UseMongoDB(opts.MongoDbConnection, opts.DatabaseName)
                    .UseCamelCaseNamingConvention();
            });
    }

    private void ConfigureOpenIdConnect(IServiceCollection services,
        AuthenticationBuilder authBuilder)
    {
        IdentityModelEventSource.ShowPII = true;

        var authOptions = new OAuthOptions();
        Configuration.GetSection(OAuthOptions.SectionName)
            .Bind(authOptions);

        authBuilder.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
        {
            // configuring openIDconnect options
            options.NonceCookie.SecurePolicy = CookieSecurePolicy.Always;
            options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;
            // How middleware persists the user identity? (Cookie)
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.GetClaimsFromUserInfoEndpoint = true;
            // How Browser redirects user to authentication provider?
            // (direct get)
            options.AuthenticationMethod = OpenIdConnectRedirectBehavior.RedirectGet;

            // How response should be sent back from authentication provider?
            //(form_post)
            options.ResponseMode = OpenIdConnectResponseMode.FormPost;

            // Who is the authentication provider? (IDP)
            options.Authority = authOptions.Authority;

            // Who are we? (client id)
            options.ClientId = authOptions.ClientId;

            // How does authentication provider know, we are ligit? (secret key)
            options.ClientSecret = authOptions.ClientSecret;

            // What do we intend to receive back?
            // (code to make for consequent requests)
            options.ResponseType = OpenIdConnectResponseType.Code;

            // Should there be extra layer of security?
            // (false: as we are using hybrid)
            options.UsePkce = true;

            // Where we would like to get the response after authentication?
            options.CallbackPath = authOptions.CallbackPath;
            options.SignedOutCallbackPath = authOptions.SignedOutCallbackPath;

            // Should we persist tokens?
            options.SaveTokens = true;
            options.Prompt = OpenIdConnectPrompt.SelectAccount;

            options.ClaimActions.MapUniqueJsonKey(ClaimTypes.Name, "name");
            options.ClaimActions.MapUniqueJsonKey(ClaimTypes.NameIdentifier, "sub");
            options.ClaimActions.MapUniqueJsonKey(ClaimTypes.Email, "email");
            options.ClaimActions.MapUniqueJsonKey("picture", "picture");

            // Should we request user profile details for user end point?
            options.GetClaimsFromUserInfoEndpoint = true;

            // What scopes do we need?
            options.Scope.Add("openid");
            options.Scope.Add("email");
            options.Scope.Add("phone");
            options.Scope.Add("profile");

            options.EventsType = typeof(OpenIdConnectEventsHandler);
            options.BackchannelTimeout = TimeSpan.FromSeconds(300);
        });
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseForwardedHeaders();
        app.UseSerilogRequestLogging();

        string appPrefix = Environment.GetEnvironmentVariable($"{EnvVarPrefix}AppPathPrefix") ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(appPrefix))
        {
            app.Use((context, next) =>
            {
                context.Request.PathBase = appPrefix;
                return next();
            });
        }
        app.UseExceptionHandler();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        //app.MapHtmxAntiforgeryScript();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            endpoints.MapHealthChecks("/healthcheck");
        });
    }
}