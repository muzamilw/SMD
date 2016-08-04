define("Layout/Layout.viewModel",
    ["jquery", "amplify", "ko", "Layout/Layout.dataService", "Layout/Layout.model", "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation) {
        var ist = window.ist || {};
        ist.Layout = {
            viewModel: (function () {
                var // The view 
                   view,
                      showBranchDialoge = function () {
                          view.showBranchCategoryDialog();
                      },
                // Initialize the view model
                 initialize = function (specifiedView) {
                     view = specifiedView;
                     ko.applyBindings(view.viewModel, view.bindingRoot);
                     ko.applyBindings(view.viewModel, view.bindingPartial);
                     
                 };
                return {

                    initialize: initialize,
                    showBranchDialoge: showBranchDialoge

                };

            })()
        };

        return ist.Layout.viewModel;

    });
define("Layout/Layout.viewModel",
    ["jquery", "amplify", "ko", "Layout/Layout.dataService", "Layout/Layout.model", "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation) {
        var ist = window.ist || {};
        ist.Layout = {
            viewModel: (function () {
                var // The view 
                   view,
                      showBranchDialoge = function () {
                          view.showBranchCategoryDialog();
                      },
                // Initialize the view model
                 initialize = function (specifiedView) {
                     view = specifiedView;
                     ko.applyBindings(view.viewModel, view.bindingRoot);
                     ko.applyBindings(view.viewModel, view.bindingPartial);
                     
                 };
                return {

                    initialize: initialize,
                    showBranchDialoge: showBranchDialoge

                };

            })()
        };

        return ist.Layout.viewModel;

    });