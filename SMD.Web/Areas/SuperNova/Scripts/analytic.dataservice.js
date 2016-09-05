/*
    Data service module with ajax calls to the server
*/
define("analytic/analytic.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                     
                    amplify.request.define('getactiveuser', 'ajax', {
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
        };

        return {
            savePq: savePq,
            getPQForApproval: getPQForApproval,
            sendApprovalRejectionEmail: sendApprovalRejectionEmail,
            getPqAnswer: getPqAnswer
        };
    })();
    return dataService;
});