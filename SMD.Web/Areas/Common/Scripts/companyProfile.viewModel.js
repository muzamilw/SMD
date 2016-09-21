/*
    Module with the view model for the User
*/
define("common/companyProfile.viewModel",
    ["jquery", "amplify", "ko", "common/companyProfile.dataservice", "common/companyProfile.model", "common/confirmation.viewModel", "common/stripeChargeCustomer.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation, stripeChargeCustomer) {
        var ist = window.ist || {};
        ist.companyProfile = {
            viewModel: (function () {
                var view,
                     // Current User
                    selectedCompany = ko.observable(),

                 selectedMedia = ko.observable();
                
                // list of countries
                countries = ko.observableArray([]),
                // list of cities 
                cities = ko.observableArray([]),


                 showCompanyProfileDialog = function () {

                     view.showCompanyProfileDialog();
                     getCompanyProfile();
                 },
                //closing
                 onCloseCompanyProfileDialog = function () {

                     if (selectedCompany().hasChanges()) {    //&& (campaignModel().Status() == null || campaignModel().Status() == 1)
                         confirmation.messageText("Do you want to save changes?");
                         confirmation.afterProceed(function () {

                             var data = selectedCompany().convertToServerData();
                             dataservice.savecompanyProfile(
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
                             view.CloseCompanyProfileDialog();
                         });
                         confirmation.show();
                     }
                     else {

                         view.CloseCompanyProfileDialog();
                     }

                 },
            socialMedia = ko.observableArray([
            { Id: 1, Text: 'Facebook' },
            { Id: 2, Text: 'Twitter' },
            { Id: 3, Text: 'Instagram' },
            { Id: 4, Text: 'Pinterest' }
                ]),
                  socialMediaHandler = function () {
                      if (selectedMedia() == 'Facebook')
                      {
                          selectedCompany().TwitterHandle(null)
                          selectedCompany().InstagramHandle(null) 
                          selectedCompany().PinterestHandle(null)
                      }
                      else if (selectedMedia() == 'Twitter')
                      {
                          selectedCompany().FacebookHandle(null)
                          selectedCompany().InstagramHandle(null)
                          selectedCompany().PinterestHandle(null)
                      
                      }
                      else if (selectedMedia() == 'Instagram')
                      {
                          selectedCompany().FacebookHandle(null)
                          selectedCompany().TwitterHandle(null)
                          selectedCompany().PinterestHandle(null)
                      }
                      else if (selectedMedia() == 'Pinterest') {

                          selectedCompany().FacebookHandle(null)
                          selectedCompany().TwitterHandle(null)
                          selectedCompany().InstagramHandle(null)
                      }
                      else {
                          selectedCompany().FacebookHandle(null)
                          selectedCompany().TwitterHandle(null)
                          selectedCompany().InstagramHandle(null)
                          selectedCompany().PinterestHandle(null)
                      }

                           
                     
                  },
                //Update Profile
                //Get Base Data for Questions
                updateProfile = function () {

                    var data = selectedCompany().convertToServerData();
                    dataservice.saveCompanyProfile(
                        data, {
                            success: function () {
                                selectedCompany().reset();
                                toastr.success("Profile updated!");
                                view.CloseCompanyProfileDialog();
                            },
                            error: function () {
                                toastr.error("Failed to update profile!");
                            }
                        });

                },

                getCitiesByCountryId = function (data) {

                    dataservice.getCitiesByCountry(
                   { countryId: data.BillingCountryId },
                   {
                       success: function (data) {
                           if (data.length > 0) {
                               cities.removeAll();
                               ko.utils.arrayPushAll(cities(), data);
                               cities.valueHasMutated();
                           }
                       },
                       error: function () {
                           toastr.error("Failed to load Cities.");
                       }
                   });

                },
                // Get User Profile For Editing 
               getCompanyProfile = function () {
                   dataservice.getCompanyProfile(null,
                       {
                           success: function (companyProfile) {
                               //console.log(userProfile);
                               selectedCompany(model.CompanyServertoClientMapper(companyProfile));
                               // Load Cities by Country
                               //updateCities(userProfile.CityId);
                               //selectedCompany().countryId.subscribe(function() {
                               //    updateCities();

                               //});
                             
                               selectedCompany().reset();
                                setFieldSubscription();
                               if (selectedCompany().FacebookHandle() != null) 
                                   selectedMedia('Facebook');
                                   if(selectedCompany().TwitterHandle() != null)
                                       selectedMedia('Twitter');
                                   if(selectedCompany().InstagramHandle() != null)
                                       selectedMedia('Instagram');
                                   if(selectedCompany().PinterestHandle() != null)
                                       selectedMedia('Pinterest');
                               
                           },
                           error: function () {
                               toastr.error("Failed to load User's Profile!");
                           }
                       });
               },

                 LogoUrlImageCallback = function (file, data) {
                     selectedCompany().LogoImageBase64(data);
                     selectedCompany().Logo('');
                 },
                  randonNumber = ko.observable("?r=0"),

                //Get Base Data for Questions
                getBasedata = function () {
                    dataservice.getBaseDataForCompanyProfile(null, {
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



                        },
                        error: function () {
                            toastr.error("Failed to load base data!");
                        }
                    });
                },

                hasChangesOnProfile = ko.computed(function () {
                    if (selectedCompany() == undefined) {
                        return false;
                    }
                    return (selectedCompany().hasChanges());
                }),

                setFieldSubscription = function () {
                    if (selectedCompany() == undefined)
                        return;
                        selectedCompany().FacebookHandle.subscribe(function (value) {
                            if (selectedMedia == 'Facebook') {
                            if (value !== selectedCompany().FacebookHandle()) {
                                selectedCompany().FacebookHandle(value);
                            }
                            selectedCompany().TwitterHandle(undefined);
                            selectedCompany().InstagramHandle(undefined);
                            selectedCompany().PinterestHandle(undefined);
                            }
                        });
                    
                    
                        selectedCompany().TwitterHandle.subscribe(function (value) {
                            if (selectedMedia == 'Twitter') {
                                if (value !== selectedCompany().TwitterHandle()) {
                                    selectedCompany().TwitterHandle(value);
                                }
                                selectedCompany().FacebookHandle(undefined);
                                selectedCompany().InstagramHandle(undefined);
                                selectedCompany().PinterestHandle(undefined);
                            }
                    });
                        selectedCompany().InstagramHandle.subscribe(function (value) {
                            if (selectedMedia == 'Instagram') {
                                if (value !== selectedCompany().InstagramHandle()) {
                                    selectedCompany().InstagramHandle(value);
                                }
                                selectedCompany().TwitterHandle(undefined);
                                selectedCompany().FacebookHandle(undefined);
                                selectedCompany().PinterestHandle(undefined);
                            }
                    });
                        selectedCompany().PinterestHandle.subscribe(function (value) {
                            if (selectedMedia == 'Pinterest') {
                                if (value !== selectedCompany().PinterestHandle()) {
                                    selectedCompany().PinterestHandle(value);
                                }
                                selectedCompany().TwitterHandle(undefined);
                                selectedCompany().InstagramHandle(undefined);
                                selectedCompany().FacebookHandle(undefined);
                            }
                    });
                },







                // Update Button handler
                onUpdateProfile = function () {
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
                   


                };
        return {
            initialize: initialize,
            getCompanyProfile: getCompanyProfile,
            selectedCompany: selectedCompany,
            countries: countries,
            cities: cities,
            LogoUrlImageCallback: LogoUrlImageCallback,
            randonNumber: randonNumber,
            onUpdateProfile: onUpdateProfile,
            socialMediaHandler: socialMediaHandler,
            chargeCustomer: chargeCustomer,
            hasChangesOnProfile: hasChangesOnProfile,
            getCitiesByCountryId: getCitiesByCountryId,
            showCompanyProfileDialog: showCompanyProfileDialog,
            onCloseCompanyProfileDialog: onCloseCompanyProfileDialog,
            socialMedia: socialMedia,
            selectedMedia: selectedMedia
        };
    })()
};
return ist.companyProfile.viewModel;
});
