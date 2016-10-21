/*
    View for the ProfileQuestion. Used to keep the viewmodel clear of UI related logic
*/
define("FranchiseDashboard/registeredUsers.view",
    ["jquery", "FranchiseDashboard/registeredUsers.viewModel"], function ($, registeredUsersViewModel) {
        var ist = window.ist || {};
        // View 
        ist.RegisteredUsers.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#RegisterUsersBindingSpot")[0],
                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }
                    // Handle Sorting
                    handleSorting("registerUsersLVTable", viewModel.sortOn, viewModel.sortIsAsc, viewModel.getRegisteredUsers);
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel
            };
        })(registeredUsersViewModel);
        // Initialize the view model
        if (ist.RegisteredUsers.view.bindingRoot) {
            registeredUsersViewModel.initialize(ist.RegisteredUsers.view);
        }
        return ist.RegisteredUsers.view;
    });
