export const RouteNames = {
  RouteList: "route_list",
  About: "about",
  Login: "login",
};
const apiBase = process.env.API.trimEnd("/");
export const ApiRoutes = {
  get session() {
    return `${apiBase}/api/session`;
  },
  /**
   *
   * @param {number} tzOffset
   * @returns
   */
  routeList: (tzOffset) => `${apiBase}/api/routes?tzOffset=` + tzOffset,

  routePost: () => `${apiBase}/api/routes`,

  /**
   *
   * @param {string} trackerId Tracker Id to delete.
   * @returns
   */
  routeDelete: (trackerId) => `${apiBase}/api/routes/${trackerId}`,
};

export const Helpers = {
  mapModeToIcon(mode) {
    mode = mode || "";
    switch (mode.toLowerCase()) {
      case "walk":
        return "directions_walk";
      default:
        return "pending";
    }
  },
};
