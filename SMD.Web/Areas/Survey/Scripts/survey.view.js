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
                            displayKey: 'LocationName',
                            source: array.ttAdapter()
                        }).bind('typeahead:selected', function (obj, selected) {
                            if (selected) {
                                var CityID = 0, CountryID = 0, Radius = 0, Country = '', City = '';
                                if (selected.IsCountry)
                                {
                                    Country = selected.LocationName;
                                    CountryID = selected.CountryId;
                                }
                                if (selected.IsCity) {
                                    City = selected.LocationName;
                                    CountryID = selected.CountryId;
                                    CityID = selected.CityId;
                                    $(".locMap").css("display", "inline-block");
                                    initializeMap(selected.GeoLong, selected.GeoLat,selected.LocationName);
                                    $("#us3-radius").change(function () {
                                        addRadius($("#us3-radius").val());
                                    });
                                }
                                var obj = {
                                    CountryID :CountryID,
                                    CityID :CityID,
                                    Radius :Radius,
                                    Country :Country,
                                    City :City,
                                }
                              
                                viewModel.selectedLocation(obj);
                                $(".locVisibility").css("display", "inline-block");
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
