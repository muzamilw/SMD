/*
    View for the ads. Used to keep the viewmodel clear of UI related logic
*/
/*
    View for the Order. Used to keep the viewmodel clear of UI related logic
*/
define("ads/ads.view",
    ["jquery", "ads/ads.viewModel"], function ($, contentViewModel) {

        var ist = window.ist || {};
      //  ist.Ads = window.ist.Ads || {};

        // View 
        ist.Ads.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#adsBinding")[0],
                
                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }
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

                    $('#searchLocations').typeahead({
                        highlight: true
                    },
                        {
                            displayKey: 'LocationName',
                            source: array.ttAdapter()
                    }).bind('typeahead:selected', function (obj, selected) {
                            if (selected) {
                                
                                var idOfEleCnt = $("#in_ex_count_opt").val() + "|" + selected.LocationId;
                                var htmlToAppend = '<div class="count_city_newcnt"  id="' + idOfEleCnt + '" ><input type="text"  class="form-control lang_nr_box" readonly="readonly" value=' + selected.LocationName + '><a id="deleteToken' + selected.LocationId + '" onclick=RemoveCountryToken("' + selected.LocationId + '"); title="Delete" class="lang_del"> <i class="fa fa-times "></i></a></div>';

                                $("#coun_token_cnt").css("display", "block");
                                $("#coun_token_cnt").append(htmlToAppend);
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
                                var langhtmlToAppend = '<div class="lang_newcnt" id="' + selected.LanguageId + '"><input type="text"   class="form-control lang_nr_box" readonly="readonly" value=' + selected.LanguageName + '><a id="deleteToken' + selected.LanguageId + '" onclick=RemoveLanguageToken("' + selected.LanguageId + '"); title="Delete" class="lang_del"> <i class="fa fa-times "></i></a></div>';

                                $("#lang_token_cnt").css("display", "block");
                                $("#lang_token_cnt").append(langhtmlToAppend);
                            }
                        });
                };
            initialize();

            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,

            };
        })(contentViewModel);

        // Initialize the view model
        if (ist.Ads.view.bindingRoot) {
            ist.Ads.viewModel.initialize(ist.Ads.view);
        }
        return ist.Ads.view;
    });
