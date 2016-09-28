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
                    amplify.request.define('savePq', 'ajax', {
                        url: '/Api/ApproveProfileQuestion',
                        dataType: 'json',
                        type: 'POST'
                    });
                    amplify.request.define('getProfileGroupbyID', 'ajax', {
                        url: '/Api/ProfileQuestionGroupApp',
                        dataType: 'json',
                        type: 'GET'
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
                    amplify.request.define('getCompanyData', 'ajax', {
                        url: '/Api/Company',
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
               getProfileGroupbyID = function (params, callbacks) {
                   initialize();
                   return amplify.request({
                       resourceId: 'getProfileGroupbyID',
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
        // Get Profile Questions Answer On edit 
        getPqAnswer = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getPqAnswer',
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
            getPQForApproval: getPQForApproval,
            sendApprovalRejectionEmail: sendApprovalRejectionEmail,
            getPqAnswer: getPqAnswer,
            getProfileGroupbyID: getProfileGroupbyID,
            getCompanyData: getCompanyData
        };
    })();
    return dataService;
});