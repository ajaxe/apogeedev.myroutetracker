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
    instance.buffer = [];
    try {
      if (d.length > 0) {
        await fetch(
          new Request(
            `${ingesterUrl}/${instance.userId}/${instance.routeId}/datapoint`,
            {
              method: "POST",
              body: JSON.stringify(d),
              headers: new Headers({ "content-type": "application/json" }),
            }
          )
        );
      } else {
        console.log("No data to push");
      }
    } catch (err) {
      console.error(`Failed to push data. ${err}`);
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
    if (instance.current) {
      instance.buffer.push(instance.current);
    }

    instance.limiterId = setTimeout(limiter, timeout);
  };

  /**
   *
   * @param {GeolocationPositionError} positionError
   */
  const onError = function (positionError) {
    this.error = JSON.stringify(positionError);
    this.enable = false;

    console.log(this.error);
    console.log("<--- stopping sensor due to error");

    this.stop();
  };
  /**
   *
   * @param {GeolocationPosition} position
   */
  const onSuccess = function (position) {
    this.current = position;
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

      instance.handler = createPositionWatcher(
        onSuccess.bind(instance),
        onError.bind(instance)
      );

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
