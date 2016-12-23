/*
    Data service module with ajax calls to the server
*/
define("invoice/Email.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    //Search Invoices
                    amplify.request.define('getEmails', 'ajax', {
                        url: '/Api/SystemEmail',
                        dataType: 'json',
                        type: 'GET'
                    });
                    amplify.request.define('saveEmails', 'ajax', {
                        url: '/Api/SystemEmail',
                        dataType: 'json',
                        type: 'POST'
                    });

                    isInitialized = true;
                }
            },
            // Search Invoices
            getEmails = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getEmails',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
             saveEmails = function (params, callbacks) {
                 initialize();
                 return amplify.request({
                     resourceId: 'saveEmails',
                     success: callbacks.success,
                     error: callbacks.error,
                     data: params
                 });
             };
            

            // Get Invoice Details
         

        return {
            getEmails: getEmails,
            saveEmails: saveEmails
        };
    })();
    return dataService;
});