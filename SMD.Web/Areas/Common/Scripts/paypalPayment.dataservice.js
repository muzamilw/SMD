/*
    Data service module with ajax calls to the server
*/
define("common/paypalPayment.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function() {
                if (!isInitialized) {

                    // Charge Customer
                    amplify.request.define('paypalPayment', 'ajax', {
                        url: '/Api/AdaptivePaypalPayment',
                        dataType: 'json',
                        type: 'POST'
                    });

                    isInitialized = true;
                }
            },       
            // paypal Payment
            paypalPayment = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'paypalPayment',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            };

        return {
            paypalPayment: paypalPayment
        };
    })();
    return dataService;
});