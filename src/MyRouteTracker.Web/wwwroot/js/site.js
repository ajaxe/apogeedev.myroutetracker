import $ from "cash-dom";
import { GeoLocationRecorder } from "./injester";
import { appendToConsole } from "./common";

window.onerror = function (errMsg, url, line, column, error) {
  let newline = "\r\n";
  let m = `${errMsg} ${newline}[${url}][${line}, ${column}] ${newline}${error}`;
  fetch(
    new Request(`${errorsUrl}?clientId=${clientId}`, {
      method: "POST",
      body: JSON.stringify({
        errMsg,
        url,
        line,
        column,
        error,
      }),
      headers: new Headers({ "content-type": "application/json" }),
    })
  );
  alert(m);
  let consoleLogger = $("#console-log");
  consoleLogger.text(consoleLogger.text() + "\r\n###\r\n" + m);
};

/**
 *
 * @param {Event} event
 * @param {string} selector
 */
const getTarget = function (event, selector) {
  let $target = $(event.target);
  if ($target.is(selector)) {
    return $target;
  } else {
    return $(event.target).parents(selector);
  }
};

$(function () {
  htmx.on(
    "init.collector",
    /**
     *
     * @param {Event | { detail }} e
     */
    function (e) {
      let { routeId } = e.detail;
      let instance = GeoLocationRecorder.Create(routeId);
      appendToConsole({
        m: "Starting instance",
        routeId: routeId,
      });
      instance.start();
      $("#client-id").text(clientId);
    }
  );

  htmx.on(
    "#collector-wrapper",
    "click",
    /**
     * Handle pause
     * @param {Event} e
     */
    function (e) {
      let $target = getTarget(e, "#collect-pause");

      if ($target.length == 0) {
        return;
      }

      GeoLocationRecorder.Current()?.stop();
      $target.addClass("d-none");
      $("#collect-resume").removeClass("d-none");
      e.preventDefault();
    }
  );
  htmx.on(
    "#collector-wrapper",
    "click",
    /**
     * Handle resume operation
     * @param {Event} e
     */
    function (e) {
      let $target = getTarget(e, "#collect-resume");

      if ($target.length == 0) {
        return;
      }

      const routeId = $("#collector-container").data("routeid");
      appendToConsole({
        m: "Starting instance",
        routeId: routeId,
      });
      const instance = GeoLocationRecorder.Create(routeId);

      instance.start();
      $("#client-id").text(clientId);

      $target.addClass("d-none");
      $("#collect-pause").removeClass("d-none");
      e.preventDefault();
    }
  );
  htmx.on(
    "#collector-wrapper",
    "click",
    /**
     * Handle stop
     * @param {Event} e
     */
    function (e) {
      let $target = getTarget(e, "#collect-finish");

      if ($target.length == 0) {
        return;
      }

      GeoLocationRecorder.Current()?.stop();

      $target.parents("#collector-wrapper").empty();

      e.preventDefault();
    }
  );
});
