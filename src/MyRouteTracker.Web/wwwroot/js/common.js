import $ from "cash-dom";
export const appendToConsole = function (data) {
  $("#console-log").text(
    $("#console-log").text() + "\r\n###\r\n" + JSON.stringify(data)
  );
};

export default {
  appendToConsole,
};
