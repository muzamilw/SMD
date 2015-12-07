/*
    Module with the view model for Stripe Charge 
*/
define("common/stripeChargeCustomer.viewModel",
    ["jquery", "amplify", "ko", "common/stripeChargeCustomer.dataservice"], function ($, amplify, ko, dataservice) {
        var ist = window.ist || {};
        ist.stripeChargeCustomer = {
            viewModel: (function () {
                var
                    // The view 
                    view,
                    // Amount to be charged
                    chargeAmount = ko.observable(),
                    // Description
                    chargeDescription = ko.observable(),
                    // On Proceed
                    afterProceed = ko.observable(),
                    // Proceed with the request
                    proceed = function () {
                        if (typeof afterProceed() === "function") {
                            afterProceed()();
                        }
                    },
                    // Reset Dialog
                    resetDialog = function () {
                        afterProceed(undefined);
                        chargeAmount(0);
                        chargeDescription('');
                    },
                    // Show Charge Dialog
                    show = function (proceedCallback, amount, description, isExistingCustomer) {
                        afterProceed(proceedCallback);
                        chargeAmount(amount || 0);
                        chargeDescription(description || '');
                        if (isExistingCustomer) {
                            charge({});
                            return;
                        }
                        view.showDialog();
                    },
                    // Create Customer
                    createCustomer = function (token) {
                        dataservice.chargeCustomer({ Token: token.id, Email: token.email, Amount: chargeAmount() }, {
                            success: function () {
                                toastr.success("Customer has been added!");
                                proceed(); // Callback if any
                            },
                            error: function (response) {
                                toastr.error("Failed to do payment. Error: " + response);
                            }
                        });
                    },
                    // Charge Customer
                    charge = function(token) {
                        dataservice.chargeCustomer({ Token: token.id, Email: token.email, Amount: chargeAmount() }, {                            
                           success: function() {
                               toastr.success("Payment has been made successfully!");
                               proceed(); // Callback if any
                           },
                           error: function(response) {
                               toastr.error("Failed to do payment. Error: " + response);
                           }
                        });
                    },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                    };

                return {
                    afterProceed: afterProceed,
                    initialize: initialize,
                    show: show,
                    proceed: proceed,
                    resetDialog: resetDialog,
                    chargeAmount: chargeAmount,
                    chargeDescription: chargeDescription,
                    charge: charge,
                    createCustomer: createCustomer
                };
            })()
        };

        return ist.stripeChargeCustomer.viewModel;
    });

