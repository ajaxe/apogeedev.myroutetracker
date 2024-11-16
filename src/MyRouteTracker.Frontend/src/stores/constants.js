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
};
