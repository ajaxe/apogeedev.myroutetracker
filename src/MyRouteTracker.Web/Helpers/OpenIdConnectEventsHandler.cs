using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace MyRouteTracker.Web.Helpers;

public class OpenIdConnectEventsHandler : OpenIdConnectEvents
{
    private readonly ILogger<OpenIdConnectEventsHandler> logger;

    public OpenIdConnectEventsHandler(ILogger<OpenIdConnectEventsHandler> logger)
    {
        this.logger = logger;
    }

    public override Task AuthorizationCodeReceived(AuthorizationCodeReceivedContext context)
    {
        logger.LogInformation("AuthorizationCodeReceived {@Context}",
            new { context.Scheme, context.Result });
        return Task.CompletedTask;
    }
    public override Task UserInformationReceived(UserInformationReceivedContext context)
    {
        logger.LogInformation("UserInformationReceived {@Context}",
            new { context.Result });
        return Task.CompletedTask;
    }
    public override Task TokenResponseReceived(TokenResponseReceivedContext context)
    {
        logger.LogInformation("TokenResponseReceived {@Context}",
            new { context.TokenEndpointResponse });
        return Task.CompletedTask;
    }
    public override Task RemoteFailure(RemoteFailureContext context)
    {
        logger.LogInformation("RemoteFailure {@Context}",
            new { context.Failure, context.Result });
        return Task.CompletedTask;
    }
    public override Task AuthenticationFailed(AuthenticationFailedContext context)
    {
        logger.LogInformation("AuthenticationFailed {@Context}",
            new { context.Exception, context.Result, context.ProtocolMessage });
        return Task.CompletedTask;
    }
}