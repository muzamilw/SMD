define("PhraseLibrary/phraseLibrary.view",
    ["jquery", "PhraseLibrary/phraseLibrary.viewModel"], function ($, branchViewModel) {
        var ist = window.ist || {};
        // View 

        ist.Layout.view = (function (specifiedViewModel) {

            var // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#phraseLibraryBinding")[0],
                bindingPartial = $("#bindingPhrasePartialViews")[0],
                     // Show BranchCategory dialog
                showBranchCategoryDialog = function () {
                    $("#branchCategoryDialog").modal("show");
                },
                // Hide BranchCategory dialog
                hideBranchCategoryDialog = function () {
                    $("#branchCategoryDialog").modal("hide");
                };

            // Show BranchCategory dialog
            showphraseLibraryDialog = function () {
                $("#phraseLibraryDialog").modal("show");
            },
            // Hide BranchCategory dialog
            HidephraseLibraryDialog = function () {
                $("#phraseLibraryDialog").modal("hide");
            };

            return {

                bindingRoot: bindingRoot,
                bindingPartial: bindingPartial,
                viewModel: viewModel,
                showBranchCategoryDialog: showBranchCategoryDialog,

                hideBranchCategoryDialog: hideBranchCategoryDialog,
                showphraseLibraryDialog: showphraseLibraryDialog,
                HidephraseLibraryDialog: HidephraseLibraryDialog,

                hideBranchCategoryDialog: hideBranchCategoryDialog

            };

        })(branchViewModel);

        // Initialize the view model
        if (ist.Layout.view.bindingRoot) {
            branchViewModel.initialize(ist.Layout.view);
        }
        return ist.Layout.view;
    });
