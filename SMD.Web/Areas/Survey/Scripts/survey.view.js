/*
    View for the ads. Used to keep the viewmodel clear of UI related logic
*/
define("survey/survey.view",
    ["jquery", "survey/survey.viewModel"], function ($, parentHireGroupViewModel) {
        var ist = window.ist || {};
        // View 
        ist.survey.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#surveyBinding")[0],
                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }
                    // Handle Sorting
                    handleSorting("surveyQuestionLVTable", viewModel.sortOn, viewModel.sortIsAsc, viewModel.getQuestions);
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel
            };
        })(parentHireGroupViewModel);
        // Initialize the view model
        if (ist.survey.view.bindingRoot) {
            parentHireGroupViewModel.initialize(ist.survey.view);
        }
        return ist.survey.view;
    });
