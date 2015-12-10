/*
    Module with the view model for paypal Payment
*/
define("common/paypalPayment.viewModel",
    ["jquery", "amplify", "ko", "common/paypalPayment.dataservice"], function ($, amplify, ko, dataservice) {
        var ist = window.ist || {};
        ist.paypalPayment = {
            viewModel: (function () {
                var
                    // The view 
                    view,
                    senderEmail = ko.observable("khurram-facilitator@innostark.com"),
                    recieverEmail = ko.observable("khurram-buyer@innostark.com"),
                    amount = ko.observable(100),
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
                    },
                    // Show Paypal Payment Section
                    setupPayment = function (proceedCallback, paymentAmount, payer, payee) {
                        afterProceed(proceedCallback);
                        amount(paymentAmount || 0);
                        senderEmail(payer || 'khurram-facilitator@innostark.com');
                        recieverEmail(payee || 'khurram-buyer@innostark.com');
                    },
                    // Make Payment
                    makePayment = function() {
                        dataservice.paypalPayment({ SenderEmail: senderEmail(), RecieverEmail: recieverEmail(), Amount: amount() }, {
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
                    setupPayment: setupPayment,
                    proceed: proceed,
                    resetDialog: resetDialog,
                    amount: amount,
                    senderEmail: senderEmail,
                    recieverEmail: recieverEmail,
                    makePayment: makePayment
                };
            })()
        };

        return ist.paypalPayment.viewModel;
    });

