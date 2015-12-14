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
                                    viewModel.selectedLocationLong(selected.GeoLong);
                                    viewModel.selectedLocationLat(selected.GeoLat);
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

                                //var idOfEleCnt = $("#in_ex_count_opt").val() + "|" + selected.LocationId;
                                //var htmlToAppend = '<div class="count_city_newcnt"  id="' + idOfEleCnt + '" ><input type="text"  class="form-control lang_nr_box" readonly="readonly" value=' + selected.LocationName + '><a id="deleteToken' + selected.LocationId + '" onclick=RemoveCountryToken("' + selected.LocationId + '"); title="Delete" class="lang_del"> <i class="fa fa-times "></i></a></div>';

                                //$("#coun_token_cnt").css("display", "block");
                                //$("#coun_token_cnt").append(htmlToAppend);
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
