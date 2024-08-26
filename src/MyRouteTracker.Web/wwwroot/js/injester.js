const GeoLocationSensor = (function () {
  const PusherInterval = 10 * 1000;
  const LimiterInterval = 900;

  const pusher = async function (useTimeout) {
    useTimeout = useTimeout || true;
    let timeout = 0;

    if (useTimeout) {
      timeout = PusherInterval + Math.random() * 100;
    }

    var d = [].concat(instance.buffer);

    if (d.length > 0) {
      instance.buffer = [];
      let url = `${ingesterUrl}/${instance.userId}/${instance.routeId}/datapoint?clientId=${clientId}`;
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
      console.log("No data to push");
    }
    if (useTimeout) {
      instance.pusherId = setTimeout(pusher, timeout);
    }
  };

  /**
   * Data point limiter select limited number of data points
   * from reported gps data.
   */
  const limiter = function () {
    const timeout = LimiterInterval + Math.random() * 100;
    if (
      instance.current &&
      (instance.receivedFirst === false ||
        instance.buffer.length === 0 ||
        instance.current.timestamp !==
          instance.buffer[instance.buffer.length - 1].timestamp)
    ) {
      instance.buffer.push(instance.current);
      instance.receivedFirst = true;
    }

    instance.limiterId = setTimeout(limiter, timeout);
  };

  /**
   *
   * @param {GeolocationPositionError} positionError
   */
  const onError = function (positionError) {
    instance.error = JSON.stringify(positionError);
    instance.enable = false;

    console.log(instance.error);
    console.log("<--- stopping sensor due to error");

    instance.stop();

    appendToConsole({ error: instance.error });
  };
  /**
   *
   * @param {GeolocationPosition} position
   */
  const onSuccess = function (position) {
    instance.current = position;
    console.log(position);
  };

  let instance = {
    userId: "",
    routeId: "",
    enabled: false,
    handler: -1,
    buffer: [],
    limiterId: -1,
    pusherId: -1,
    receivedFirst: false,
    stop: function () {
      if (navigator.geolocation) {
        navigator.geolocation.clearWatch(this.handler);
      }
      clearTimeout(this.limiterId);
      clearTimeout(this.pusherId);

      this.limiterId = -1;
      this.pusherId = -1;
      this.enabled = false;
      this.buffer = [];

      pusher(false);
    },
  };
  let template = {
    enabled: false,
    current: () => {},
    stop: () => {},
  };

  /**
   *
   * @param {string} userId
   * @param {string} routeId
   */
  const start = function (userId, routeId) {
    let handle = Object.assign(template, {
      enabled: false,
    });

    if (navigator.geolocation) {
      if (instance.enabled) {
        instance.stop();
      }

      instance.userId = userId;
      instance.routeId = routeId;
      instance.enabled = true;

      instance.handler = createPositionWatcher(onSuccess, onError);

      pusher();
      limiter();

      handle = {
        enable: instance.enabled,
        current: () => instance.current,
        stop: () => instance.stop(),
      };
    }
    return handle;
  };

  const getInstance = function () {
    return {
      enable: instance.enabled,
      current: () => instance.current,
      stop: () => instance.stop(),
    };
  };

  const createPositionWatcher = (onSuccess, onError) => {
    return navigator.geolocation.watchPosition(onSuccess, onError, {
      maximumAge: 0,
      timeout: 500,
      enableHighAccuracy: true,
    });
  };

  return { start, getInstance };
})();
