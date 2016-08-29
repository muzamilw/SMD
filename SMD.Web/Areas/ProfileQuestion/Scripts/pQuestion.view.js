/*
    View for the Questions. Used to keep the viewmodel clear of UI related logic
*/
define("pQuestion/pQuestion.view",
    ["jquery", "pQuestion/pQuestion.viewModel"], function ($, parentHireGroupViewModel) {
        var ist = window.ist || {};
        // View 
        ist.ProfileQuestion.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#ProfileQuestionBindingSpot")[0],
                // Hide Answer Dialog
                hideAnswerDialog = function() {
                    $("#pqAnswerDialog").modal('hide');
                },
                   initializeTypeahead = function () {
                       var array = new Bloodhound({
                           datumTokenizer: function (d) {
                               return Bloodhound.tokenizers.whitespace(d.LocationName);
                           },
                           queryTokenizer: Bloodhound.tokenizers.whitespace,
                           remote: {
                               rateLimitWait: 1000,
                               url: '/Api/AdCampaignBase?searchText=%QUERY',
                               ajax: {
                                   type: 'POST'
                               },
                               replace: function (url, query) {
                                   query = query + "|1";
                                   return url.replace('%QUERY', query);
                               },
                               filter: function (data) {

                                   return data.countriesAndCities;
                               }
                           }
                       });

                       array.initialize();
                       $('#searchCampaignLocations').typeahead({
                           highlight: true
                       }, {
                           displayKey: 'bindedValue',
                           source: array.ttAdapter()
                       }).bind('typeahead:selected', function (obj, selected) {
                           
                           if (selected) {
                               var CityID = null, CountryID = null, Radius = 0, Country = '', City = '', latitude = '', longitude = '';
                               if (selected.IsCountry) {
                                   Country = selected.LocationName;
                                   CountryID = selected.CountryId;
                               }
                               if (selected.IsCity) {
                                   City = selected.LocationName;
                                   CountryID = selected.CountryId;
                                   CityID = selected.CityId;
                                   latitude = selected.GeoLat;
                                   longitude = selected.GeoLong;
                                   Country = selected.parentCountryName;
                               }
                               var obj = {
                                   CountryID: CountryID,
                                   CityID: CityID,
                                   Radius: Radius,
                                   Country: Country,
                                   City: City,
                                   Latitude: latitude,
                                   Longitude: longitude
                               }

                               
                               viewModel.selectedLocation(obj);
                               viewModel.onAddLocation();
                               $('#searchCampaignLocations').val("");
                               $('.twitter-typeahead input').val("");
                               $('#searchCampaignLocations').focus(function () {
                                   $('.twitter-typeahead input').val("");
                               });
                           }
                       });

                       var lan_array = new Bloodhound({
                           datumTokenizer: function (d) {
                               return Bloodhound.tokenizers.whitespace(d.LanguageName);
                           },
                           queryTokenizer: Bloodhound.tokenizers.whitespace,
                           remote: {
                               rateLimitWait: 1000,
                               url: '/Api/AdCampaignBase?searchText=%QUERY',
                               ajax: {
                                   type: 'POST'
                               },
                               replace: function (url, query) {
                                   query = query + "|2";
                                   return url.replace('%QUERY', query);
                               },
                               filter: function (data) {

                                   return data.Languages;
                               }
                           }
                       });

                       lan_array.initialize();

                       $('#searchLanguages').typeahead({
                           highlight: true
                       },
                           {
                               displayKey: 'LanguageName',
                               source: lan_array.ttAdapter()
                           }).bind('typeahead:selected', function (obj, selected) {
                               if (selected) {
                                   console.log(selected);
                                   viewModel.addLanguage(selected);
                               }
                           });
                       // industry
                       var ind_array = new Bloodhound({
                           datumTokenizer: function (d) {
                               return Bloodhound.tokenizers.whitespace(d.IndustryName);
                           },
                           queryTokenizer: Bloodhound.tokenizers.whitespace,
                           remote: {
                               rateLimitWait: 1000,
                               url: '/Api/AdCampaignBase?searchText=%QUERY',
                               ajax: {
                                   type: 'POST'
                               },
                               replace: function (url, query) {
                                   query = query + "|3";
                                   return url.replace('%QUERY', query);
                               },
                               filter: function (data) {

                                   return data.listIndustry;
                               }
                           }
                       });

                       ind_array.initialize();

                       $('#searchIndustries').typeahead({
                           highlight: true
                       },
                               {
                                   displayKey: 'IndustryName',
                                   source: ind_array.ttAdapter()
                               }).bind('typeahead:selected', function (obj, selected) {
                                   if (selected) {
                                       viewModel.addIndustry(selected);
                                   }
                               });
                       // education
                       var edu_array = new Bloodhound({
                           datumTokenizer: function (d) {
                               return Bloodhound.tokenizers.whitespace(d.Title);
                           },
                           queryTokenizer: Bloodhound.tokenizers.whitespace,
                           remote: {
                               rateLimitWait: 1000,
                               url: '/Api/AdCampaignBase?searchText=%QUERY',
                               ajax: {
                                   type: 'POST'
                               },
                               replace: function (url, query) {
                                   query = query + "|4";
                                   return url.replace('%QUERY', query);
                               },
                               filter: function (data) {

                                   return data.listEducation;
                               }
                           }
                       });

                       edu_array.initialize();

                       $('#searchEducations').typeahead({
                           highlight: true
                       },
                               {
                                   displayKey: 'Title',
                                   source: edu_array.ttAdapter()
                               }).bind('typeahead:selected', function (obj, selected) {
                                   debugger;
                                   if (selected) {
                                       viewModel.addEducation(selected);
                                   }
                               });

                   },
                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }
                    // Handle Sorting
                    handleSorting("profileQuestionLVTable", viewModel.sortOn, viewModel.sortIsAsc, viewModel.getQuestions);
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,
                hideAnswerDialog: hideAnswerDialog,
                initializeTypeahead: initializeTypeahead
            };
        })(parentHireGroupViewModel);
        // Initialize the view model
        if (ist.ProfileQuestion.view.bindingRoot) {
            parentHireGroupViewModel.initialize(ist.ProfileQuestion.view);
        }
        return ist.ProfileQuestion.view;
    });

// Reads File - Print Out Section
function readPhotoURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var img = new Image;
            img.onload = function () {
                if (img.height > 250 || img.width > 250) {
                 //   toastr.error("Image Max. width 1280 and height 1024px; please resize the image and try again");
                } else {
                    $('#vehicleImage')
                    .attr('src', e.target.result)
                    .width(120)
                    .height(120);

                }
            };
            img.src = reader.result;
            ist.ProfileQuestion.viewModel.selectedAnswer().imagePath(img.src);
        };
        reader.readAsDataURL(input.files[0]);
    }
}