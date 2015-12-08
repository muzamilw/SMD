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
                    amplify.request.define('getBaseData', 'ajax', {
                        url: '/Api/AdCampaignBase',
                        dataType: 'json',
                        type: 'GET'
                    });

                    amplify.request.define('getCampaignData', 'ajax', {
                        url: '/Api/Campaign',
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
        },
        getCampaignData = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getCampaignData',
                data: params,
                success: callbacks.success,
                error: callbacks.error,
            });
        }
        return {
            getBaseData: getBaseData,
            getCampaignData: getCampaignData,
        };
    })();
    return dataService;
});