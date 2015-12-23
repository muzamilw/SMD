/*
    View for the Survey Questions. Used to keep the viewmodel clear of UI related logic
*/
define("invoice/invoice.view",
    ["jquery", "invoice/invoice.viewModel"], function ($, surveyQuestionViewModel) {
        var ist = window.ist || {};
        // View 
        ist.SurveyQuestion.view = (function (specifiedViewModel) {
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
        })(surveyQuestionViewModel);
        // Initialize the view model
        if (ist.SurveyQuestion.view.bindingRoot) {
            surveyQuestionViewModel.initialize(ist.SurveyQuestion.view);
        }
        return ist.SurveyQuestion.view;
    });
