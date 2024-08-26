import $ from "cash-dom";

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
      let { userId, routeId } = e.detail;
      let instance = GeoLocationSensor.getInstance();
      if (instance.enabled) {
        instance.stop();
        utils.appendToConsole({ m: "Instance stopped" });
      } else {
        utils.appendToConsole({
          m: "Starting instance",
          userId: userId,
          routeId: routeId,
        });
        instance = GeoLocationSensor.start(userId, routeId);
      }
      $("#client-id").text(clientId);
    }
  );

  htmx.on(
    "#collector-wrapper",
    "click",
    /**
     *
     * @param {Event} e
     */
    function (e) {
      let $target = getTarget(e, "#collect-pause");

      if ($target.length == 0) {
        return;
      }

      GeoLocationSensor.getInstance().stop();
      $target.addClass("d-none");
      $("#collect-resume").removeClass("d-none");
      e.preventDefault();
    }
  );
  htmx.on(
    "#collector-wrapper",
    "click",
    /**
     *
     * @param {Event} e
     */
    function (e) {
      let $target = getTarget(e, "#collect-resume");

      if ($target.length == 0) {
        return;
      }

      let container = $("#collector-container");

      GeoLocationSensor.start(
        container.data("userid"),
        container.data("routeid")
      );

      $target.addClass("d-none");
      $("#collect-pause").removeClass("d-none");
      e.preventDefault();
    }
  );
  htmx.on(
    "#collector-wrapper",
    "click",
    /**
     *
     * @param {Event} e
     */
    function (e) {
      let $target = getTarget(e, "#collect-finish");

      if ($target.length == 0) {
        return;
      }

      GeoLocationSensor.getInstance().stop();

      $target.parents("#collector-wrapper").empty();

      e.preventDefault();
    }
  );
});
