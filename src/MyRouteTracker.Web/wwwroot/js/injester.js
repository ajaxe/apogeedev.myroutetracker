import { appendToConsole, GeoSettings, GeoPermission } from "./common";

/**
 * Time interval to collect gps data before pushing to the server.
 */
const PusherInterval = 20 * 1000;
/**
 * Time interval to collect gps coordinate.
 */
const LimiterInterval = 1200;

/**
 * @type {?GeoLocationRecorder}
 */
let singleton = null;

export class GeoLocationRecorder {
  /**
   *
   * @param {string} routeId
   */
  constructor(routeId) {
    this.routeId = routeId;
    this.enabled = false;
    this.recorderTimeout = -1;
    this.dataTransmitTimeout = -1;
    /**
     * @type {Array<GeolocationPosition>}
     */
    this.buffer = [];
  }

  static Create = function (routeId) {
    if (singleton) {
      singleton.stop();
      appendToConsole({ m: "Instance stopped" });
    }

    singleton = new GeoLocationRecorder(routeId);

    return singleton;
  };

  static Current = () => singleton;

  start() {
    console.log(`injester: permission: ${GeoPermission().enabled}`);
    const timeout = LimiterInterval + Math.random() * 100;

    this.recorderTimeout = setInterval(recorder, timeout, this);
    this.dataTransmitTimeout = setInterval(
      pusher,
      PusherInterval + Math.random() * 100,
      this,
      true
    );
    this.enabled = true;
  }

  stop() {
    if (this.recorderTimeout !== -1) {
      clearInterval(this.recorderTimeout);
    }
    if (this.dataTransmitTimeout !== -1) {
      clearInterval(this.dataTransmitTimeout);
    }

    let self = this;
    pusher(this, true).then(function () {
      self.buffer = [];
    });

    this.enabled = false;
  }
}

/**
 *
 * @param {GeoLocationRecorder} instance
 */
const recorder = function (instance) {
  navigator.geolocation.getCurrentPosition(
    function (position) {
      instance.buffer.push(position);
    },
    function (error) {
      appendToConsole({
        m: "GPS error, stopping",
        code: error.code,
        message: error.message,
      });
      instance.stop();
    },
    GeoSettings
  );
};

/**
 *
 * @param {GeoLocationRecorder} instance
 * @param {boolean} useTimeout
 */
const pusher = async function (instance, useTimeout) {
  useTimeout = useTimeout || true;
  let timeout = 0;

  if (useTimeout) {
    timeout = PusherInterval + Math.random() * 100;
  }

  var d = [].concat(instance.buffer);

  if (d.length > 0) {
    instance.buffer = [];
    let url = `${ingesterUrl}/${instance.routeId}/datapoint?clientId=${clientId}`;
    appendToConsole({
      count: d.length,
      url,
    });
    let body = JSON.stringify(d);
    appendToConsole({ body });
    /**
     * {Response}
     */
    let response = await fetch(
      new Request(url, {
        method: "POST",
        body: body,
        headers: new Headers({ "content-type": "application/json" }),
      })
    );
    let responseTxt = "";
    if (response.status !== 204) responseTxt = await response.text();
    appendToConsole({
      status: response.status,
      response: responseTxt,
    });
  } else {
    appendToConsole({ m: "No data to push" });
  }
};
