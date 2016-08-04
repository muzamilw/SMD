/*
    Data service module with ajax calls to the server
*/
define("common/stripeChargeCustomer.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function() {
                if (!isInitialized) {

                    // Charge Customer
                    amplify.request.define('chargeCustomer', 'ajax', {
                        url: '/Api/ChargeCustomer',
                        dataType: 'json',
                        type: 'POST'
                    });

                    // Charge Customer
                    amplify.request.define('createStripeCustomer', 'ajax', {
                        url: '/Api/CreateStripeCustomer',
                        dataType: 'json',
                        type: 'POST'
                    });

                    isInitialized = true;
                }
            },       
            // Charge Customer
            chargeCustomer = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'chargeCustomer',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // Create Stripe Customer
            createStripeCustomer = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'createStripeCustomer',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            };

        return {
            chargeCustomer: chargeCustomer,
            createStripeCustomer: createStripeCustomer
        };
    })();
    return dataService;
});