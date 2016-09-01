/*
    Data service module with ajax calls to the server
*/
define("FranchiseDashboard/profileQuetionApp.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    //Search AdCampaigns
                    amplify.request.define('getCouponsForApproval', 'ajax', {
                        url: '/Api/CouponApproval',
                        dataType: 'json',
                        type: 'GET'
                    });


                    // Edit AdCampaign
                    amplify.request.define('saveCoupon', 'ajax', {
                        url: '/Api/CouponApproval',
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
            saveCoupon = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveCoupon',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            };

        return {
            saveCoupon: saveCoupon,
            getCouponsForApproval: getCouponsForApproval,
            sendApprovalRejectionEmail: sendApprovalRejectionEmail
        };
    })();
    return dataService;
});