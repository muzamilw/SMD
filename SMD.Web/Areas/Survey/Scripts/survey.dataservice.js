﻿/*
    Data service module with ajax calls to the server
*/
define("survey/survey.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {


                    isInitialized = true;
                }
            };

        return {
           
        };
    })();
    return dataService;
});