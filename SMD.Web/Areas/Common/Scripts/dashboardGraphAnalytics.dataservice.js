/*
    Data service module with ajax calls to the server
*/
define("common/dashboardGraphAnalytics.dataService", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
               
                if (!isInitialized) {

                    isInitialized = true;
                    amplify.request.define('getBaseData', 'ajax', {
                        url: '/Api/DashboardAnalytics',
                        dataType: 'json',
                        type: 'GET'
                    });
                 

                    amplify.request.define('GetCounters', 'ajax', {
                        url: '/Api/DashboardStatusCounter',
                        dataType: 'json',
                        type: 'GET'
                    });
                }
            };

        
                



        GetCounters = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'GetCounters',
                data: params,
                success: callbacks.success,
                error: callbacks.error,
            });
        };
        getBaseData = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getBaseData',
                data: params,
                success: callbacks.success,
                error: callbacks.error,
            });
        };
        return {
            getBaseData: getBaseData,
            GetCounters: GetCounters
        };
    })();
    return dataService;
});