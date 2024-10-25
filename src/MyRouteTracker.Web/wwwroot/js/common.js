import $ from "cash-dom";
export const appendToConsole = function (data) {
  let $console = $("#console-log");

  if ($console.length > 0) {
    $console.text($console.text() + "\r\n###\r\n" + JSON.stringify(data));
  } else {
    console.log(JSON.stringify(data));
  }
};

export const GeoSettings = {
  maximumAge: 0,
  timeout: 500,
  enableHighAccuracy: true,
};
const geoLocationStatus = {
  enabled: false,
  state: "",
  init: false,
};

function setGeoLocationState(state) {
  console.log("Permission: " + state);
  geoLocationStatus.enabled = state === "granted";
  geoLocationStatus.state = state;
  geoLocationStatus.init = true;

  return geoLocationStatus;
}
export const GeoPermission = () => geoLocationStatus;

function checkStatus() {
  if (geoLocationStatus.init) {
    return geoLocationStatus;
  }
  navigator.permissions.query({ name: "geolocation" }).then(function (result) {
    result.onchange = function () {
      setGeoLocationState(result.state);
    };

    if (result.state == "granted") {
      return setGeoLocationState(result.state);
    } else if (result.state == "prompt") {
      navigator.geolocation.getCurrentPosition(
        function (pos) {
          appendToConsole({ m: "Check", pos });
        },
        function (err) {
          appendToConsole({
            m: "Check error",
            code: err.code,
            message: err.message,
          });
        },
        GeoSettings
      );
      return setGeoLocationState(result.state);
    } else {
      return setGeoLocationState(result.state);
    }
  });
}

checkStatus();
