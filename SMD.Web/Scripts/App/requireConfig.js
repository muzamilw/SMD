﻿// Setup requirejs
(function () {

    var root = this;
    var ist = window.ist;

    if (!ist.siteUrl) {
        ist.siteUrl = $("#siteUrl").val();
    }

    requirejs.config({
        baseUrl: ist.siteUrl + "/Scripts/App",
        waitSeconds: 20,
        paths: {
            "sammy": "/Scripts/sammy-0.7.5.min",
            "common":  "/Areas/Common/Scripts",
            // SMD 
            "pQuestion": "/Areas/ProfileQuestion/Scripts",
            "ads": "/Areas/Ads/Scripts",
            "addApproval": "/Areas/Ads/Scripts",
            "survey": "/Areas/Survey/Scripts",
            "surveyQuestionApp": "/Areas/Survey/Scripts",
            "invoice":"/Areas/Billing/Scripts"
        }
    });

    function defineThirdPartyModules() {
        // These are already loaded via bundles. 
        // We define them and put them in the root object.
        define("jquery", [], function () { return root.jQuery; });
        define("ko", [], function () { return root.ko; });
        define("underscore-knockout", [], function () { });
        define("underscore-ko", [], function () { });
        define("knockout", [], function () { return root.ko; });
        define("knockout-validation", [], function () { });
        define("moment", [], function () { return root.moment; });
        define("amplify", [], function () { return root.amplify; });
        define("underscore", [], function () { return root._; });
    }

    defineThirdPartyModules();


})();
