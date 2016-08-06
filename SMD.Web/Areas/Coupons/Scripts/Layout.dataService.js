/*
    Data service module with ajax calls to the server
*/
define("Layout/Layout.dataService", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
               
                if (!isInitialized) {

                    isInitialized = true;
                    amplify.request.define('getSections', 'ajax', {
                        url: '/Api/PhaseLibrary',
                        dataType: 'json',
                        type: 'GET'
                    });

                    amplify.request.define('getPhraseBySectionID', 'ajax', {
                        url: '/Api/PhaseLibrary',
                        dataType: 'json',
                        type: 'GET'
                    });

                   
                }
            };

        getSections = function (params, callbacks) {
            initialize();
            
            return amplify.request({
                resourceId: 'getSections',
                data: params,
                success: callbacks.success,
                error: callbacks.error,
            });
        },
        getPhraseBySectionID = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getPhraseBySectionID',
                data: params,
                success: callbacks.success,
                error: callbacks.error,
            });
        }
      
        return {
            getSections: getSections,
            getPhraseBySectionID: getPhraseBySectionID
        };
    })();
    return dataService;
});