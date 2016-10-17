/*
    Data service module with ajax calls to the server
*/
define("FranchiseDashboard/payPallApp.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    //Search AdCampaigns
                    amplify.request.define('getpayOutHistoryForApproval', 'ajax', {
                        url: '/Api/ApprovePayOutHistory',
                        dataType: 'json',
                        type: 'GET'
                    });


                    // Edit AdCampaign
                    amplify.request.define('savePq', 'ajax', {
                        url: '/Api/ApproveProfileQuestion',
                        dataType: 'json',
                        type: 'POST'
                    });


                    // Send Mail
                    amplify.request.define('sendApprovalRejectionEmail', 'ajax', {
                        url: '/Api/BuyIt',
                        dataType: 'json',
                        type: 'POST'
                    });
                    amplify.request.define('getCompanyData', 'ajax', {
                        url: '/Api/CompanyDetail',
                        dataType: 'json',
                        type: 'GET'
                    });
                    isInitialized = true;
                }
            },
            // Search Ad Campaigns
            getpayOutHistoryForApproval = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getpayOutHistoryForApproval',
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
            savePq = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'savePq',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
         getCompanyData = function (params, callbacks) {
             initialize();
             return amplify.request({
                 resourceId: 'getCompanyData',
                 success: callbacks.success,
                 error: callbacks.error,
                 data: params
             });
         };
   

        return {
            savePq: savePq,
            getpayOutHistoryForApproval: getpayOutHistoryForApproval,
            sendApprovalRejectionEmail: sendApprovalRejectionEmail,
            getCompanyData: getCompanyData
    
        };
    })();
    return dataService;
});