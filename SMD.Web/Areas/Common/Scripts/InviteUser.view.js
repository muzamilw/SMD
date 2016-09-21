/*
    View for the User. Used to keep the viewmodel clear of UI related logic
*/
define("common/InviteUser.view",
    ["jquery", "common/InviteUser.viewModel"], function ($, userViewModel) {
        var ist = window.ist || {};
        // View 
        ist.userProfile.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#manageUserBinding")[0],
                // Binding root used with knockout
                //bindingRootUser = $("#manageUserBinding")[0],

             
                

                // show invite user dialoge
                showInviteUser = function () {
                    $("#InviteUser").modal("show");
                    initializeLabelPopovers();
                },
               
                 // Hideinvite user
                hideEditManagedUserPopup = function () {
                    $("#EditManagedUser").modal("hide");
                },
                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                //bindingRootUser: bindingRootUser,
                viewModel: viewModel,
         
                showInviteUser: showInviteUser,
               
                hideEditManagedUserPopup: hideEditManagedUserPopup

            };
        })(userViewModel);
        // Initialize the view model
        if (ist.userProfile.view.bindingRoot) {
            userViewModel.initialize(ist.userProfile.view);
        }
        //if (ist.userProfile.view.bindingRootUser) {
        //    userViewModel.initialize(ist.userProfile.view);
        //}
        return ist.userProfile.view;
    });
