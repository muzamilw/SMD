/*
    Data service module with ajax calls to the server
*/
define("addApproval/addApproval.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function() {
                if (!isInitialized) {

                    //Search AdCampaigns
                    amplify.request.define('searchAdCampaigns', 'ajax', {
                        url: '/Api/AdCampaign',
                        dataType: 'json',
                        type: 'GET'
                    });
                    

                    // Edit AdCampaign
                    amplify.request.define('saveAdCampaign', 'ajax', {
                        url: '/Api/AdCampaign',
                        dataType: 'json',
                        type: 'POST'
                    });
                    isInitialized = true;
                }
            },       
            // Search Ad Campaigns
            searchAdCampaigns = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'searchAdCampaigns',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            

            
            // Save Ad Campaign edit
            saveAdCampaign = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveAdCampaign',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            };

        return {
            saveAdCampaign: saveAdCampaign,
            searchAdCampaigns: searchAdCampaigns
        };
    })();
    return dataService;
});