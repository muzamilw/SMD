/*
    Data service module with ajax calls to the server
*/
define("invoice/invoice.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function() {
                if (!isInitialized) {

                    //Search Invoices
                    amplify.request.define('searchInvoices', 'ajax', {
                        url: '/Api/Invoice',
                        dataType: 'json',
                        type: 'GET'
                    });
                    

                    // Edit Invoices
                    amplify.request.define('saveSurveyQuestion', 'ajax', {
                        url: '/Api/SurveyQuestionApproval',
                        dataType: 'json',
                        type: 'POST'
                    });
                    isInitialized = true;
                }
            },       
            // Search Invoices
            searchInvoices = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'searchInvoices',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            

            
            // Save Invoices edit
            saveSurveyQuestion = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveSurveyQuestion',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            };

        return {
            saveSurveyQuestion: saveSurveyQuestion,
            searchInvoices: searchInvoices
        };
    })();
    return dataService;
});