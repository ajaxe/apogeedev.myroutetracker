// Import all of Bootstrap's JS
import htmx from "htmx.org";
import * as bootstrap from "bootstrap";
import * as utils from "./js/common";
import * as site from "./js/site";
import GeoLocationSensor from "./js/injester";

window.htmx = htmx;
window.GeoLocationSensor = GeoLocationSensor;
window.utils = utils;
