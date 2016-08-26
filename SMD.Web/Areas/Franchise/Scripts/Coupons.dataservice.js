/*
    Data service module with ajax calls to the server
*/
define("FranchiseDashboard/Coupons.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    //Search AdCampaigns
                    amplify.request.define('getCouponsForApproval', 'ajax', {
                        url: '/Api/CouponApprovalController',
                        dataType: 'json',
                        type: 'GET'
                    });


                    // Edit AdCampaign
                    amplify.request.define('saveAdCampaign', 'ajax', {
                        url: '/Api/AdCampaignApproval',
                        dataType: 'json',
                        type: 'POST'
                    });

                    // Send Mail
                    amplify.request.define('sendApprovalRejectionEmail', 'ajax', {
                        url: '/Api/BuyIt',
                        dataType: 'json',
                        type: 'POST'
                    });
                    isInitialized = true;
                }
            },
            // Search Ad Campaigns
            getCouponsForApproval = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCouponsForApproval',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },


             // Search Ad Campaigns
            sendApprovalRejectionEmail = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'sendApprovalRejectionEmail',
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
            getCouponsForApproval: getCouponsForApproval,
            sendApprovalRejectionEmail: sendApprovalRejectionEmail
        };
    })();
    return dataService;
});