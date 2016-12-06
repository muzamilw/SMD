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

                 selectedMedia = ko.observable(),

                // list of countries
                countries = ko.observableArray([]),
                // list of cities 
                cities = ko.observableArray([]),
                currentTab = ko.observable(1),
                isMapVisible = ko.observable(false),
                errorList = ko.observableArray([]),
                OldPassword = ko.observable(),
                NewPassword = ko.observable(),
                ConfirmPassword = ko.observable(),
                    logoImage = '',
                logoChangeDetector = ko.observable(),
                 showCompanyProfileDialog = function () {

                     // view.showCompanyProfileDialog();
                     getCompanyProfile(view.showCompanyProfileDialog);
                 },
                 geocoderComp = '',
                //closing
                 onCloseCompanyProfileDialog = function () {

                     if (selectedCompany().hasChanges()) {    //&& (campaignModel().Status() == null || campaignModel().Status() == 1)
                         confirmation.messageText("Do you want to save changes?");
                         confirmation.afterProceed(function () {

                             var data = selectedCompany().convertToServerData();
                             dataservice.savecompanyProfile(
                                 data, {
                                     success: function () {
                                         toastr.success("Company profile updated successfully!");
                                     },
                                     error: function () {
                                         toastr.error("Failed to update profile!");
                                     }
                                 });
                         });
                         confirmation.afterCancel(function () {
                             selectedCompany().reset();
                             view.CloseCompanyProfileDialog();
                         });
                         confirmation.show();
                         logoImage = '';
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
                genderList = ko.observableArray([{
                    id: 1,
                    name: 'Mr.'
                }, {
                    id: 2,
                    name: 'Ms.'
                }
                , {
                    id: 3,
                    name: 'Mrs.'
                }]),
                professionsList = ko.observableArray([]),
                companyTypes = ko.observableArray([
                    { Id: 1, Name: 'Amusement, Gambling, and Recreation Industries' },
                    { Id: 2, Name: 'Arts, Entertainment, and Recreation' },
                    { Id: 3, Name: 'Broadcasting (except Internet)' },
                    { Id: 4, Name: 'Building Material and Garden Equipment and Supplies Dealers' },
                    { Id: 5, Name: 'Clothing and Clothing Accessories Stores' },
                    { Id: 6, Name: 'Computer and Electronics' },
                    { Id: 7, Name: 'Construction' },
                    { Id: 8, Name: 'Couriers and Messengers' },
                    { Id: 9, Name: 'Data Processing, Hosting, and Related Services' },
                    { Id: 10, Name: 'Health Services' },
                    { Id: 11, Name: 'Educational Services' },
                    { Id: 12, Name: 'Electronics and Appliance Stores' },
                    { Id: 13, Name: 'Finance and Insurance' },
                    { Id: 14, Name: 'Food Services' },
                    { Id: 15, Name: 'Food and Beverage Stores' },
                    { Id: 16, Name: 'Furniture and Home Furnishings Stores' },
                    { Id: 17, Name: 'General Merchandise Stores' },
                    { Id: 18, Name: 'Health Care' },
                    { Id: 19, Name: 'Internet Publishing and Broadcasting' },
                    { Id: 20, Name: 'Leisure and Hospitality' },
                    { Id: 21, Name: 'Manufacturing' },
                    { Id: 22, Name: 'Merchant Wholesalers ' },
                    { Id: 23, Name: 'Motor Vehicle and Parts Dealers ' },
                    { Id: 24, Name: 'Museums, Historical Sites, and Similar Institutions ' },
                    { Id: 25, Name: 'Performing Arts, Spectator Sports' },
                    { Id: 26, Name: 'Printing Services' },
                    { Id: 27, Name: 'Professional and Business Services' },
                    { Id: 28, Name: 'Real Estate' },
                    { Id: 29, Name: 'Repair and Maintenance' },
                    { Id: 30, Name: 'Scenic and Sightseeing Transportation' },
                    { Id: 31, Name: 'Service-Providing Industries' },
                    { Id: 32, Name: 'Social Assistance' },
                    { Id: 33, Name: 'Sporting Goods, Hobby, Book, and Music Stores' },
                    { Id: 34, Name: 'Telecommunications' },
                    { Id: 35, Name: 'Transportation' },
                    { Id: 36, Name: 'Utilities' }
                ]),
                isAddressFilled = ko.computed(function () {
                    if (selectedCompany() != undefined && selectedCompany() != null) {
                        if ((selectedCompany().BillingAddressLine1() == undefined || selectedCompany().BillingAddressLine1() == "") || (selectedCompany().billingCity() == undefined || selectedCompany().billingCity() == "") || (selectedCompany().BillingState() == undefined || selectedCompany().BillingState() == "")) {
                            return false;
                        }
                        else {
                            isMapVisible(true);
                            return true;
                        }

                    }
                    else {

                        return false;
                    }


                }),
                //Update Profile
                //Get Base Data for Questions
                updateProfile = function () {
                    if (selectedCompany().isSubmit()) {
                        if (doBeforeSave()) {
                            saveCompanyProfile();
                        }
                    } else {
                        saveCompanyProfile();
                    }

                },
                saveCompanyProfile = function () {
                    if (logoImage != '')
                        selectedCompany().Logo(logoImage);
                    var data = selectedCompany().convertToServerData();
                    dataservice.saveCompanyProfile(
                        data, {
                            success: function () {
                                selectedCompany().reset();
                                toastr.success("Profile updated successfully!");
                                view.CloseCompanyProfileDialog();
                            },
                            error: function () {
                                toastr.error("Failed to update profile!");
                            }
                        });
                },
                doBeforeSave = function () {
                    var flag = true;
                    errorList.removeAll();
                    if (!selectedCompany().isValid()) {
                        selectedCompany().errors.showAllMessages();
                        setValidationSummary(selectedCompany());
                        flag = false;
                    }
                    return flag;
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
               getCompanyProfile = function (callback) {
                   dataservice.getCompanyProfile(null,
                       {
                           success: function (companyProfile) {
                               //console.log(userProfile);
                               selectedCompany(model.CompanyServertoClientMapper(companyProfile));
                               // Load Cities by Country
                               //updateCities(userProfile.CityId);

                               selectedCompany().billingCity.subscribe(function () {
                                   googleAddressMap();

                               });

                               selectedCompany().BillingAddressLine1.subscribe(function () {
                                   googleAddressMap();

                               });

                               selectedCompany().BillingState.subscribe(function () {
                                   googleAddressMap();

                               });

                               selectedCompany().BillingZipCode.subscribe(function () {
                                   googleAddressMap();

                               });



                               //if (selectedCompany().FacebookHandle() != null) 
                               //    selectedCompany().selectedMedia('Facebook');
                               //    if(selectedCompany().TwitterHandle() != null)
                               //        selectedCompany().selectedMedia('Twitter');
                               //    if(selectedCompany().InstagramHandle() != null)
                               //        selectedCompany().selectedMedia('Instagram');
                               //    if(selectedCompany().PinterestHandle() != null)
                               //        selectedCompany().selectedMedia('Pinterest');


                               currentTab(1);
                               selectedCompany().reset();
                               if (callback && typeof callback === "function") {
                                   callback();
                               }



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
                            debugger;
                            if (baseDataFromServer.GetCouponReviewCount > 0) {
                                $("#imgRedbell").css("display", "block");
                                $("#whiteicon").css("display", "none");
                            }
                            else {

                                $("#whiteicon").css("display", "block");
                                $("#imgRedbell").css("display", "none");
                            }


                            $('#couponCount').text(baseDataFromServer.GetApprovalCount.CouponCount);
                            $('#vidioAdCount').text(baseDataFromServer.GetApprovalCount.AdCmpaignCount);
                            $('#displayAdCount').text(baseDataFromServer.GetApprovalCount.DisplayAdCount);
                            $('#surveyCount').text(baseDataFromServer.GetApprovalCount.SurveyQuestionCount);
                            $('#profileCount').text(baseDataFromServer.GetApprovalCount.ProfileQuestionCount);

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
                                professionsList.removeAll();
                                ko.utils.arrayPushAll(professionsList(), baseDataFromServer.IndusteryDropdowns);
                                professionsList.valueHasMutated();
                            }


                            randonNumber("?r=" + Math.floor(Math.random() * (20 - 1 + 1)) + 1);

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
                    if (selectedCompany().hasChanges() || logoImage != '')
                        return true;
                    else
                        false;
                    // return (selectedCompany().hasChanges());
                }),
                onTabChange = function (tabNo) {
                    currentTab(tabNo);

                    if (tabNo == 2) {
                        setTimeout(googleAddressMap, 500);
                    }
                },

                //-------Google Map Code--------------
                googleAddressMap = function () {

                    initializeGeoLocation();
                    setCompanyAddress();
                    google.maps.event.addDomListener(window, 'load', initializeGeoLocation);

                },
                initializeGeoLocation = function () {
                    geocoderComp = new google.maps.Geocoder();
                    var latlngComp = new google.maps.LatLng(-34.397, 150.644);
                    var mapOptions = {
                        zoom: 15,
                        center: latlngComp,
                        mapTypeId: google.maps.MapTypeId.ROADMAP
                    };
                    debugger;
                   compMap = new google.maps.Map(document.getElementById('map-canvasCompany'), mapOptions);

                    //map = new google.maps.Map($('#map-canvasCompany'), mapOptions);

                },
                setCompanyAddress = function () {
                    if (selectedCompany().BillingAddressLine1() != null && selectedCompany().BillingAddressLine1() != '') {
                        var address = selectedCompany().BillingAddressLine1().toLowerCase() + ' ' + selectedCompany().billingCity() + ' ' + selectedCompany().BillingZipCode() + ' ' + selectedCompany().BillingState().toLowerCase();
                    }
                    geocoderComp.geocode({
                        'address': address
                    }, function (results, status) {
                        if (status == google.maps.GeocoderStatus.OK) {
                            isMapVisible(true);
                            if (isCodeAddressEdit() == false) {
                                selectedCompany().branchLocationLat(results[0].geometry.location.lat());
                                selectedCompany().branchLocationLong(results[0].geometry.location.lng());
                            }

                            compMap.setCenter(results[0].geometry.location);

                            var marker = new google.maps.Marker({
                                map: compMap,
                                position: results[0].geometry.location
                            });
                            google.maps.event.addListener(compMap, 'click', function (event) {
                                selectedCompany().branchLocationLat(event.latLng.lat());
                                selectedCompany().branchLocationLong(event.latLng.lng());
                                var geocoder = new google.maps.Geocoder();
                                geocoder.geocode({
                                    "latLng": event.latLng
                                }, function (results, status) {
                                    console.log(results, status);
                                    if (status == google.maps.GeocoderStatus.OK) {
                                        console.log(results);
                                        var lat = results[0].geometry.location.lat(),
                                            lng = results[0].geometry.location.lng(),
                                            placeName = results[0].address_components[0].long_name,
                                            latlng = new google.maps.LatLng(lat, lng);

                                        moveMarker(placeName, latlng);
                                    }
                                });
                            });
                            function moveMarker(placeName, latlng) {
                                marker.setIcon(image);
                                marker.setPosition(latlng);
                                infowindow.setContent(placeName);
                                infowindow.open(map, marker);
                            }
                            isCodeAddressEdit(false);


                        } else {
                            toastr.error("Failed to Search Address,please add valid address and search it . Error: " + status);
                            isMapVisible(false);

                        }
                    });

                },
                //-------Google Map Code Ends


                // Update Button handler
                onUpdateProfile = function () {
                    //selectedCompany().TwitterHandle(null);
                    //selectedCompany().InstagramHandle(null);
                    //selectedCompany().FacebookHandle(null);
                    //selectedCompany().PinterestHandle(null)
                    //if (selectedCompany().selectedMedia() == 'Twitter')
                    //    selectedCompany().TwitterHandle(selectedCompany().mediaHandleui());
                    //else if (selectedCompany().selectedMedia() == 'Facebook')
                    //    selectedCompany().FacebookHandle(selectedCompany().mediaHandleui());
                    //else if (selectedCompany().selectedMedia() == 'Instagram')
                    //    selectedCompany().InstagramHandle(selectedCompany().mediaHandleui());
                    //else if (selectedCompany().selectedMedia() == 'Pinterest')
                    //    selectedCompany().PinterestHandle(selectedCompany().mediaHandleui());

                    updateProfile();
                },
                // Charge Customer
                chargeCustomer = function () {
                    stripeChargeCustomer.show(undefined, 2000, '2 Widgets');
                },
                 setValidationSummary = function (selectedItem) {

                     if (selectedItem.CompanyName.error) {
                         errorList.push({ name: selectedItem.CompanyName.domElement.name, element: selectedItem.CompanyName.domElement });
                     }
                     if (selectedItem.salutation.error) {
                         errorList.push({ name: selectedItem.salutation.domElement.name, element: selectedItem.salutation.domElement });
                     }
                     if (selectedItem.fullName.error) {
                         errorList.push({ name: selectedItem.fullName.domElement.name, element: selectedItem.fullName.domElement });
                     }
                     if (selectedItem.mobileNumber.error) {
                         errorList.push({ name: selectedItem.mobileNumber.domElement.name, element: selectedItem.mobileNumber.domElement });
                     }
                     if (selectedItem.dateOfBirth.error) {
                         errorList.push({ name: selectedItem.dateOfBirth.domElement.name, element: selectedItem.dateOfBirth.domElement });
                     }
                     if (selectedItem.billingCity.error) {
                         errorList.push({ name: selectedItem.billingCity.domElement.name, element: selectedItem.billingCity.domElement });
                     }
                     if (selectedItem.BillingAddressLine1.error) {
                         errorList.push({ name: selectedItem.BillingAddressLine1.domElement.name, element: selectedItem.BillingAddressLine1.domElement });
                     }

                 },
                gotoElement = function (validation) {
                    view.gotoElement(validation.element);
                },
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
                    else if (NewPassword() == "" || NewPassword() == undefined) {
                        toastr.error("New Password is Required!");
                        return;
                    } else if (ConfirmPassword() == "" || ConfirmPassword() == undefined) {
                        toastr.error("Confirm Password is Required!");
                        return;
                    }
                    else if (oldPasswordLength.length < 6) {
                        toastr.error("Old Password is not correct it should have minimum 6 characters!");
                    }
                    else if (newPasswordLength.length < 6) {
                        toastr.error("New Password is required with minimum 6 characters!");
                    }
                    else if (NewPassword() != ConfirmPassword()) {
                        toastr.error("Password not matched!");
                    }
                    else {
                        $.getJSON("/Account/ChangePassword?Password=" + NewPassword() + "&OldPassword=" + OldPassword() + "&UserId=" + selectedCompany().userId(),
                               function (xdata) {
                                   OldPassword(undefined);
                                   ConfirmPassword(undefined);
                                   NewPassword(undefined);
                                   toastr.success("Password Changed Successfully!");
                                   view.hideChangePassword();
                               });
                        toastr.success("Password Changed Successfully!");
                        view.hideChangePassword();
                    }

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
                    chargeCustomer: chargeCustomer,
                    hasChangesOnProfile: hasChangesOnProfile,
                    getCitiesByCountryId: getCitiesByCountryId,
                    showCompanyProfileDialog: showCompanyProfileDialog,
                    onCloseCompanyProfileDialog: onCloseCompanyProfileDialog,
                    socialMedia: socialMedia,
                    selectedMedia: selectedMedia,
                    currentTab: currentTab,
                    onTabChange: onTabChange,
                    genderList: genderList,
                    professionsList: professionsList,
                    companyTypes: companyTypes,
                    isAddressFilled: isAddressFilled,
                    errorList: errorList,
                    gotoElement: gotoElement,
                    ChangePassword: ChangePassword,
                    ChangePasswordOk: ChangePasswordOk,
                    OldPassword: OldPassword,
                    NewPassword: NewPassword,
                    ConfirmPassword: ConfirmPassword,
                    logoChangeDetector: logoChangeDetector
                };
            })()
        };
        return ist.companyProfile.viewModel;
    });
