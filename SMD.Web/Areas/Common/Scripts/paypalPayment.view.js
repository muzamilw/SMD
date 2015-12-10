/*
    View for the Paypal Payment. Used to keep the viewmodel clear of UI related logic
*/
define("common/paypalPayment.view",
    ["jquery", "common/paypalPayment.viewModel"], function ($) {

        var ist = window.ist || {};
        // View 
        ist.paypalPayment.view = (function (specifiedViewModel) {
            var // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#paypalPaymentBinding")[0];
            
            
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel
            };
            
        })(ist.paypalPayment.viewModel);

        // Initialize the view model
        if (ist.paypalPayment.view.bindingRoot) {
            ist.paypalPayment.viewModel.initialize(ist.paypalPayment.view);
        }
    });