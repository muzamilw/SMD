/*
    View for the User. Used to keep the viewmodel clear of UI related logic
*/
define("common/InviteUser.view",
    ["jquery", "common/InviteUser.viewModel"], function ($, inviteUserViewModel) {
        var ist = window.ist || {};
        // View 
        ist.inviteUser.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#InviteUserBinding")[0],
                bindingPartial = $("#bindingInviteUserPartialViews")[0],

             
                

                // show invite user dialoge
                showInviteUser = function () {
                    $("#InviteSharingUser").modal("show");
                    //initializeLabelPopovers();
                },
               
                 // Hideinvite user
                hideInviteUser = function () {
                    $("#InviteSharingUser").modal("hide");
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
                bindingPartial: bindingPartial,
                viewModel: viewModel,
         
                showInviteUser: showInviteUser,
               
                hideInviteUser: hideInviteUser

            };
        })(inviteUserViewModel);
        // Initialize the view model
        if (ist.inviteUser.view.bindingRoot) {
            inviteUserViewModel.initialize(ist.inviteUser.view);
        }
        //if (ist.inviteUser.view.bindingRootUser) {
        //    userViewModel.initialize(ist.inviteUser.view);
        //}
        return ist.inviteUser.view;
    });
