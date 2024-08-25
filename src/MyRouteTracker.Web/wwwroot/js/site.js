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
        container.data("userId"),
        container.data("routeId")
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
