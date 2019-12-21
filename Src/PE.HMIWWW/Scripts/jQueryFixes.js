
//ACCEPT COMMA AS SEPARATOR
$.validator.methods.number = function (value, element) {
  return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:,\d{3})+)?(?:[.|\,]\d+)?$/.test(value);
};

//ADD SMF RULES VALIDATION TO CLIENT-VALIDATION

$.validator.unobtrusive.adapters.add("smfrequierd", ["smfrequierd"], function (options) {
  options.rules["smfrequierd"] = "#" + options.params.requiredif;
  options.messages["smfrequierd"] = options.message;
});

$.validator.unobtrusive.adapters.add("smfrange", ["smfrange"], function (options) {
  options.rules["smfrange"] = "#" + options.params.requiredif;
  options.messages["smfrange"] = options.message;
});