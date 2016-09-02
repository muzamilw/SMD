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
                    amplify.request.define('getPQForApproval', 'ajax', {
                        url: '/Api/ApproveProfileQuestion',
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
                    //Get Profile Question Answer 
                    amplify.request.define('getPqAnswer', 'ajax', {
                        url: '/Api/ApproveProfileQuestionAns',
                        dataType: 'json',
                        type: 'GET'
                    });
                    isInitialized = true;
                }
            },
            // Search Ad Campaigns
            getPQForApproval = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getPQForApproval',
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
            },
        // Get Profile Questions Answer On edit 
        getPqAnswer = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getPqAnswer',
                success: callbacks.success,
                error: callbacks.error,
                data: params
            });
        };

        return {
            saveCoupon: saveCoupon,
            getPQForApproval: getPQForApproval,
            sendApprovalRejectionEmail: sendApprovalRejectionEmail,
            getPqAnswer: getPqAnswer
        };
    })();
    return dataService;
});