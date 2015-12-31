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
                    

                    // Edit Invoices / Details
                    amplify.request.define('getInvoiceDetails', 'ajax', {
                        url: '/Api/InvoiceDetail',
                        dataType: 'json',
                        type: 'GET'
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
            
            // Get Invoice Details
            getInvoiceDetails = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getInvoiceDetails',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            };

        return {
            getInvoiceDetails: getInvoiceDetails,
            searchInvoices: searchInvoices
        };
    })();
    return dataService;
});