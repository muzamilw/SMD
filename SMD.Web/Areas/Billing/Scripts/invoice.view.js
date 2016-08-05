/*
    View for the Invoice. Used to keep the viewmodel clear of UI related logic
*/
define("invoice/invoice.view",
    ["jquery", "invoice/invoice.viewModel"], function ($, invoiceViewModel) {
        var ist = window.ist || {};
        // View 
        ist.Invoice.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#InvoiceLVBindingSpot")[0],
                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }
                    // Handle Sorting
                    handleSorting("invoiceLVTable", viewModel.sortOn, viewModel.sortIsAsc, viewModel.getInvoices);
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel
            };
        })(invoiceViewModel);
        // Initialize the view model
        if (ist.Invoice.view.bindingRoot) {
            invoiceViewModel.initialize(ist.Invoice.view);
        }
        return ist.Invoice.view;
    });
