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
                                    // Load Cities by Country
                                    updateCities(userProfile.CityId);
                                    selectedUser().countryId.subscribe(function() {
                                        updateCities();
                                    });
                                },
                                error: function () {
                                    toastr.error("Failed to load User's Profile!");
                                }
                            });
                   },
                   // Update City DD on country change 
                   updateCities = function (cityId) {
                       if (!selectedUser().countryId()) {
                           return;
                       }
                       dataservice.getCitiesByCountry({ countryId: selectedUser().countryId()},
                           {
                               success: function (serverData) {
                                   cities.removeAll();
                                   ko.utils.arrayPushAll(cities(), serverData);
                                   cities.valueHasMutated();
                                   if (!cityId) {
                                       return;
                                   }
                                   selectedUser().cityId(cityId);
                               },
                               error: function () {
                                   toastr.error("Failed to get cities!");
                               }
                           });
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
                                // Get Profile When Base data is loaded 
                                getUserProfile();
                            },
                            error: function () {
                                toastr.error("Failed to load base data!");
                            }
                        });
                    },
                         // store right side ans image
                    storeImageCallback = function (file, data) {
                        selectedUser().ProfileImageBytes(data);
                    },
                    //Update Profile
                     //Get Base Data for Questions
                    updateProfile = function () {
                      
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
                    chargeCustomer: chargeCustomer,
                    storeImageCallback: storeImageCallback
                };
            })()
        };
        return ist.userProfile.viewModel;
    });
