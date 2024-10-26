// Import all of Bootstrap's JS
import htmx from "htmx.org";
import * as bootstrap from "bootstrap";
import * as utils from "./js/common";
import * as site from "./js/site";
import $ from "cash-dom";

window.htmx = htmx;
window.tzOffset = new Date().getTimezoneOffset();
window.$ = $;
