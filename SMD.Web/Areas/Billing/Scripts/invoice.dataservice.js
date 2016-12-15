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
                    
                    amplify.request.define('getCompanySub', 'ajax', {
                        url: '/Api/CompanySubscription',
                        dataType: 'json',
                        type: 'GET'
                    });
                    amplify.request.define('saveSubscription', 'ajax', {
                        url: '/Api/CompanySubscription',
                        dataType: 'json',
                        type: 'POST'
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
             saveSubscription = function (params, callbacks) {
                 initialize();
                 return amplify.request({
                     resourceId: 'saveSubscription',
                     success: callbacks.success,
                     error: callbacks.error,
                     data: params
                 });
             },
              getCompanySub = function (callbacks) {
                  initialize();

                  return amplify.request({
                      resourceId: 'getCompanySub',
                      success: callbacks.success,
                      error: callbacks.error,
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
            searchInvoices: searchInvoices,
            getCompanySub: getCompanySub,
            saveSubscription: saveSubscription
        };
    })();
    return dataService;
});