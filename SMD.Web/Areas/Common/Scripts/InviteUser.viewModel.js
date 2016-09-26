/*
    Module with the view model for the User
*/
define("common/InviteUser.viewModel",
    ["jquery", "amplify", "ko", "common/InviteUser.dataService", "common/InviteUser.model", "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model,confirmation, stripeChargeCustomer) {
        var ist = window.ist || {};
        ist.inviteUser = {
            viewModel: (function() {
                var view,
                     // Current User
                     InviteEmail = ko.observable(),
                       // Get User Profile For Editing 
                   hideInviteUserPopup = function () {
                       view.hideInviteUser();
                   },
                
                    showInviteUserPopup = function () {
                        
                        view.showInviteUser();
                    },

                    Invite = function () {

                       
                        dataservice.InviteUserSvc({
                            email: InviteEmail(), companyId: gCompanyID, mode: 'advertiser'
                     },{

                               success: function () {
                                  
                                   toastr.success("Invitation Sent!");
                                   view.hideInviteUser();

                               },
                               error: function () {
                                   toastr.error("Error Invitation Sent!");
                               }
                           });

                     
                 },
                

                    // Initialize the view model
                    initialize = function (specifiedView) {
                       
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        ko.applyBindings(view.viewModel, view.bindingPartial);

                    };
                return {
                    initialize: initialize,
                   
                    showInviteUserPopup: showInviteUserPopup,
                    Invite: Invite,
                    InviteEmail: InviteEmail
                   
                };
            })()
        };
        return ist.inviteUser.viewModel;
    });
