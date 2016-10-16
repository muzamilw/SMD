/*
    View for the ads. Used to keep the viewmodel clear of UI related logic
*/
define("survey/survey.view",
    ["jquery", "survey/survey.viewModel"], function ($, parentHireGroupViewModel) {
        var ist = window.ist || {};
        // View 
        ist.survey.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#surveyBinding")[0],
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

                    $('#searchSurveyLocations').typeahead({
                        highlight: true
                    },{
                        displayKey: 'bindedValue',
                            source: array.ttAdapter()
                    }).bind('typeahead:selected', function (obj, selected) {
                            if (selected) {
                                var CityID = null, CountryID = null, Radius = 0, Country = '', City = '',latitude = '',longitude = '';
                                if (selected.IsCountry)
                                {
                                    Country = selected.LocationName;
                                    CountryID = selected.CountryId;
                                }
                                if (selected.IsCity) {
                                    City = selected.LocationName;
                                    CountryID = selected.CountryId;
                                    CityID = selected.CityId;
                                    Country = selected.parentCountryName;
                                    latitude = selected.GeoLat;
                                    longitude = selected.GeoLong;
                                }
                                var obj = {
                                    CountryID :CountryID,
                                    CityID :CityID,
                                    Radius :Radius,
                                    Country :Country,
                                    City: City,
                                    Latitude: latitude,
                                    Longitude : longitude
                                }
                              
                                viewModel.selectedLocation(obj);
                                viewModel.onAddLocation();
                                $('.twitter-typeahead input').val("");
                                $('#searchSurveyLocations').focus(function () {
                                    $('.twitter-typeahead input').val("");
                                });
                                $('#searchSurveyLocations').typeahead('close');
                                $('#searchSurveyLocations').typeahead('val', '');
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
                                if (selected) {
                                    viewModel.addEducation(selected);
                                }
                            });
                    var myEvent = window.attachEvent || window.addEventListener;
                    var chkevent = window.attachEvent ? 'onbeforeunload' : 'beforeunload'; /// make IE7, IE8 compatable

                    myEvent(chkevent, function (e) { // For >=IE7, Chrome, Firefox
                        if (viewModel.selectedQuestion().hasChanges()) {
                            var confirmationMessage = ' ';  // a space
                            (e || window.event).returnValue = confirmationMessage;
                            return confirmationMessage;
                        }
                    });
                },
                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }
                    // Handle Sorting
                    handleSorting("surveyQuestionLVTable", viewModel.sortOn, viewModel.sortIsAsc, viewModel.getQuestions);

                    
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,
                initializeTypeahead: initializeTypeahead
            };
        })(parentHireGroupViewModel);
        // Initialize the view model
        if (ist.survey.view.bindingRoot) {
            parentHireGroupViewModel.initialize(ist.survey.view);
        }
        return ist.survey.view;
    });
