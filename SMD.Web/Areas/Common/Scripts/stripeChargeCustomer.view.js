/*
    View for the Stripe Charge Customer. Used to keep the viewmodel clear of UI related logic
*/
define("common/stripeChargeCustomer.view",
    ["jquery", "common/stripeChargeCustomer.viewModel"], function ($) {

        var ist = window.ist || {};
        // View 
        ist.stripeChargeCustomer.view = (function (specifiedViewModel) {
            var // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#stripeChargeCustomerDialog")[0],
                // Stripe Checkout handler
                handler,
                // Show Dialog
                showDialog = function() {
                    handler = StripeCheckout.configure({
                        key: 'pk_test_H5ynmxoBz7YATHN18SgZ5hjc',
                        image: '/img/documentation/checkout/marketplace.png',
                        locale: 'auto',
                        token: function (token) {
                            // Use the token to create the charge with a server-side script.
                            // You can access the token ID with `token.id`
                            viewModel.createCustomer(token);
                        },
                        closed: function() {
                            handler.close();
                        }
                    });
                    
                    // Close Checkout on page navigation
                    $(window).on('popstate', function () {
                        handler.close();
                    });
                    
                    // Trigger button
                    $("#stripeChargeCustomerButton").click();
                };
            
            // Wire up On Click Event for Stripe Charge Button
            $("#stripeChargeCustomerButton").on('click', function (event) {
                // Open Checkout with further options
                handler.open({
                    name: 'Sell My Data',
                    description: viewModel.chargeDescription(),
                    amount: viewModel.chargeAmount()
                });
                event.preventDefault(); 
            });

            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,
                showDialog: showDialog
            };
            
        })(ist.stripeChargeCustomer.viewModel);

        // Initialize the view model
        if (ist.stripeChargeCustomer.view.bindingRoot) {
            ist.stripeChargeCustomer.viewModel.initialize(ist.stripeChargeCustomer.view);
        }
    });