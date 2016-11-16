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
                 
                }
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
            
            getBaseData:getBaseData
        };
    })();
    return dataService;
});