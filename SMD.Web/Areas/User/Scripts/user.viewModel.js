/*
    Module with the view model for the User
*/
define("user/user.viewModel",
    ["jquery", "amplify", "ko", "user/user.dataservice", "user/user.model", "common/stripeChargeCustomer.viewModel"],
    function ($, amplify, ko, dataservice, model, stripeChargeCustomer) {
        var ist = window.ist || {};
        ist.userProfile = {
            viewModel: (function() {
                var view,
                     // Current User
                    selectedUser = ko.observable(),
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
                    // Gender list
                    genderList = ko.observableArray([{
                         id: 1,
                         name: 'Male'
                     }, {
                         id: 2,
                         name: 'Female'
                     }]),
                    // Get User Profile For Editing 
                   getUserProfile = function () {
                        dataservice.getUserProfile(null,
                            {
                                success: function (userProfile) {
                                    selectedUser(model.UserServertoClientMapper(userProfile));
                                    selectedUser().countryId.subscribe(function(oldValue) {
                                        updateCities();
                                    });
                                },
                                error: function () {
                                    toastr.error("Failed to load User's Profile!");
                                }
                            });
                   },
                   // Update City DD on country change 
                   updateCities = function() {
                       dataservice.getCitiesByCountry({ countryId: selectedUser().countryId()},
                           {
                               success: function (serverData) {
                                   cities.removeAll();
                                   ko.utils.arrayPushAll(cities(), serverData);
                                   cities.valueHasMutated();
                               },
                               error: function () {
                                   toastr.error("Failed to get cities!");
                               }
                           });
                   },
                    // Has Changes Makes 
                    hasChangesOnQuestion = ko.computed(function () {
                        //if (selectedInvoice() == undefined) {
                        //    return false;
                        //}
                        //return (selectedInvoice().hasChanges());
                    }),
                    //Get Base Data for Questions
                    getBasedata = function () {
                        dataservice.getBaseDataForUserProfile(null, {
                            success: function (baseDataFromServer) {
                                if (baseDataFromServer != null && baseDataFromServer.CountryDropdowns != null) {
                                    countries.removeAll();
                                    ko.utils.arrayPushAll(countries(), baseDataFromServer.CountryDropdowns);
                                    countries.valueHasMutated();
                                }

                                if (baseDataFromServer != null && baseDataFromServer.CityDropDowns != null) {
                                    cities.removeAll();
                                    ko.utils.arrayPushAll(cities(), baseDataFromServer.CityDropDowns);
                                    cities.valueHasMutated();
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
                                // Get Profile When Base data is loaded 
                                getUserProfile();
                            },
                            error: function () {
                                toastr.error("Failed to load base data!");
                            }
                        });
                    },
                    //Update Profile
                     //Get Base Data for Questions
                    updateProfile = function () {
                        dataservice.saveUserProfile(selectedUser().convertToServerData(), {
                            success: function (baseDataFromServer) {
                                toastr.success("Profile updated!");
                            },
                            error: function () {
                                toastr.error("Failed to update profile!");
                            }
                        });
                    },
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
                        // Base data call
                        getBasedata();
                    };
                return {
                    initialize: initialize,
                    getInvoices: getUserProfile,
                    selectedUser: selectedUser,
                    countries: countries,
                    cities: cities,
                    industries: industries,
                    genderList: genderList,
                    onUpdateProfile: onUpdateProfile,
                    educations: educations,
                    timeZones: timeZones,
                    chargeCustomer: chargeCustomer
                };
            })()
        };
        return ist.userProfile.viewModel;
    });
