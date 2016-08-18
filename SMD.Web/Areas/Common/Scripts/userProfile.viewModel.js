/*
    Module with the view model for the User
*/
define("common/userProfile.viewModel",
    ["jquery", "amplify", "ko", "common/userProfile.dataservice", "common/userProfile.model","common/confirmation.viewModel", "common/stripeChargeCustomer.viewModel"],
    function ($, amplify, ko, dataservice, model,confirmation, stripeChargeCustomer) {
        var ist = window.ist || {};
        ist.userProfile = {
            viewModel: (function() {
                var view,
                     // Current User
                    selectedUser = ko.observable(),
                     // is user ID
                    isUserEdit = ko.observable(false),
                    // selected userid
                    selectedUserId = ko.observable(false),
                    // selected roleid
                    selectedRoleId = ko.observable(),

                    isChangePasswordVisible = ko.observable(false),

                    OldPassword = ko.observable(),

                    NewPassword = ko.observable(),

                    ConfirmPassword = ko.observable(),

                     InviteEmail = ko.observable(),
                    
                    // list of countries
                    countries = ko.observableArray([]),
                    // list of cities 
                    cities = ko.observableArray([]),
                    // list of industries 
                    industries = ko.observableArray([]),
                    // list of education 
                    educations = ko.observableArray([]),
                    // list of Time Zones 
                    timeZones = ko.observableArray([]),
                       // roles
                    roles = ko.observableArray([]),
                    // manage users
                    userList = ko.observableArray([]),
                    // Gender list
                    genderList = ko.observableArray([{
                         id: 1,
                         name: 'Male'
                     }, {
                         id: 2,
                         name: 'Female'
                     }]),

                     showUserProfileDialog = function () {
                         view.showUserProfileDialog();
                         getUserProfile();
                     },
                     //closing
                     onCloseUserProfileDialog = function () {
                       
                         if (selectedUser().hasChanges()) {    //&& (campaignModel().Status() == null || campaignModel().Status() == 1)
                             confirmation.messageText("Do you want to save changes?");
                             confirmation.afterProceed(function () {

                                 var data = selectedUser().convertToServerData();
                                 dataservice.saveUserProfile(
                                     data, {
                                         success: function () {
                                             toastr.success("Profile updated!");
                                         },
                                         error: function () {
                                             toastr.error("Failed to update profile!");
                                         }
                                     });
                             });
                             confirmation.afterCancel(function () {
                                 view.CloseUserProfileDialog();
                             });
                             confirmation.show();
                         }
                         else {

                             view.CloseUserProfileDialog();
                         }
                        
                     },

                    //Update Profile
                     //Get Base Data for Questions
                    updateProfile = function () {

                        var data = selectedUser().convertToServerData();
                        dataservice.saveUserProfile(
                            data, {
                                success: function () {
                                    selectedUser().reset();
                                    toastr.success("Profile updated!");
                                    view.CloseUserProfileDialog();
                                },
                                error: function () {
                                    toastr.error("Failed to update profile!");
                                }
                            });

                    },

                    
                    // Get User Profile For Editing 
                   getUserProfile = function () {
                        dataservice.getUserProfile(null,
                            {
                                success: function (userProfile) {
                                    //console.log(userProfile);
                                    selectedUser(model.UserServertoClientMapper(userProfile));
                                    // Load Cities by Country
                                    //updateCities(userProfile.CityId);
                                    //selectedUser().countryId.subscribe(function() {
                                    //    updateCities();
                                       
                                    //});
                                    selectedUser().reset();
                                },
                                error: function () {
                                    toastr.error("Failed to load User's Profile!");
                                }
                            });
                   },


                       // Get User Profile For Editing 
                   ChangePassword = function () {
                       view.showChangePassword();
                   },

                   ChangePasswordOk = function () {

                       var oldPasswordLength = OldPassword();
                       var newPasswordLength = NewPassword();
                       if (OldPassword() == "" || OldPassword() == undefined) {
                           toastr.error("Old Password is Required!");
                           return;
                       }
                       else if (NewPassword() == "" || NewPassword() == undefined)
                       {
                           toastr.error("New Password is Required!");
                           return;
                       }else if (ConfirmPassword() == "" || ConfirmPassword() == undefined) {
                           toastr.error("Confirm Password is Required!");
                           return;
                       }
                       else if(oldPasswordLength.length < 6)
                       {
                           toastr.error("Old Password is not correct it should have minimum 6 characters!");
                       }
                       else if (newPasswordLength.length < 6) {
                           toastr.error("New Password is required with minimum 6 characters!");
                       }
                       else if (NewPassword() != ConfirmPassword())
                       {
                           toastr.error("Password not matched!");
                       }
                       else {
                           $.getJSON("/Account/ChangePassword?Password=" + NewPassword() + "&OldPassword=" + OldPassword() + "&UserId=" + selectedUser().id(),
                                  function (xdata) {
                                      toastr.success("Password Changed Successfully!");
                                      view.hideChangePassword();
                                  });
                           toastr.success("Password Changed Successfully!");
                           view.hideChangePassword();
                       }
                    
                   },


                  

                  
                    //Get Base Data for Questions
                    getBasedata = function () {
                        dataservice.getBaseDataForUserProfile(null, {
                            success: function (baseDataFromServer) {
                              
                                if (baseDataFromServer != null && baseDataFromServer.CountryDropdowns != null) {
                                    countries.removeAll();
                                    ko.utils.arrayPushAll(countries(), baseDataFromServer.CountryDropdowns);
                                    countries.valueHasMutated();
                                }
                                if (baseDataFromServer != null && baseDataFromServer.IndusteryDropdowns != null) {
                                    industries.removeAll();
                                    ko.utils.arrayPushAll(industries(), baseDataFromServer.IndusteryDropdowns);
                                    industries.valueHasMutated();
                                }
                                if (baseDataFromServer != null && baseDataFromServer.EducationDropdowns != null) {
                                    educations.removeAll();
                                    ko.utils.arrayPushAll(educations(), baseDataFromServer.EducationDropdowns);
                                    educations.valueHasMutated();
                                }
                                if (baseDataFromServer != null && baseDataFromServer.TimeZoneDropDowns != null) {
                                    timeZones.removeAll();
                                    ko.utils.arrayPushAll(timeZones(), baseDataFromServer.TimeZoneDropDowns);
                                    timeZones.valueHasMutated();
                                }
                                if (baseDataFromServer != null && baseDataFromServer.UserRoles != null) {
                                    roles.removeAll();
                                    ko.utils.arrayPushAll(roles(), baseDataFromServer.UserRoles);
                                    roles.valueHasMutated();
                                }

                            },
                            error: function () {
                                toastr.error("Failed to load base data!");
                            }
                        });
                    },

                    hasChangesOnProfile = ko.computed(function () {
                          if (selectedUser() == undefined) {
                              return false;
                          }
                          return (selectedUser().hasChanges());
                      }),


                 

                

                

                    // Update Button handler
                    onUpdateProfile= function() {
                        updateProfile();
                    },
                     // Charge Customer
                    chargeCustomer = function () {
                        stripeChargeCustomer.show(undefined, 2000, '2 Widgets');
                    },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        ko.applyBindings(view.viewModel, view.bindingPartial);
                       
                        getBasedata();
                        isUserEdit(true);


                    };
                return {
                    initialize: initialize,
                    getUserProfile: getUserProfile,
                    selectedUser: selectedUser,
                    countries: countries,
                    cities: cities,
                    industries: industries,
                    genderList: genderList,
                    onUpdateProfile: onUpdateProfile,
                    educations: educations,
                    timeZones: timeZones,
                    chargeCustomer: chargeCustomer,
                    hasChangesOnProfile: hasChangesOnProfile,
                    userList: userList,
                  
                    isUserEdit: isUserEdit,
                    roles: roles,
                    selectedRoleId: selectedRoleId,
                    OldPassword: OldPassword,
                  
                    ChangePassword: ChangePassword,
                    isChangePasswordVisible: isChangePasswordVisible,
                    NewPassword: NewPassword,
                    ConfirmPassword: ConfirmPassword,
                    ChangePasswordOk: ChangePasswordOk,
                  
                    
                    showUserProfileDialog: showUserProfileDialog,
                    onCloseUserProfileDialog: onCloseUserProfileDialog
                };
            })()
        };
        return ist.userProfile.viewModel;
    });
