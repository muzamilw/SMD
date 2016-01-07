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
                          displayKey: 'LocationName',
                          source: array.ttAdapter()
                      }).bind('typeahead:selected', function (obj, selected) {
                          debugger;
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
                };
            initialize();

            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,
                initializeTypeahead: initializeTypeahead
            };
        })(contentViewModel);

        // Initialize the view model
        if (ist.Ads.view.bindingRoot) {
            ist.Ads.viewModel.initialize(ist.Ads.view);
        }
        return ist.Ads.view;
    });
