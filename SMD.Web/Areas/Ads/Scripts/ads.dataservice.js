/*
    Data service module with ajax calls to the server
*/
define("ads/ads.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    isInitialized = true;
                    amplify.request.define('GetAdverts', 'ajax', {
                        url: '/Api/AdCampaignBase',
                        dataType: 'json',
                        type: 'GET'
                    });
                }
            };

        GetAdverts = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'GetAdverts',
                data: params,
                success: callbacks.success,
                error: callbacks.error,
            });
        }
        return {
            GetAdverts: GetAdverts,
        };
    })();
    return dataService;
});