/*
    Module with the view model for the User
*/
define("common/dashboardGraphAnalytics.viewModel",
    ["jquery", "amplify", "ko", "common/dashboardGraphAnalytics.dataService", "common/dashboardGraphAnalytics.model", "common/confirmation.viewModel"],
    function ($, amplify, ko, dataService, model, confirmation) {
        var ist = window.ist || {};
        ist.inviteUser = {
            viewModel: (function() {
                var view,
                     // Current User
                     Deals = ko.observable(),
                     ProfileQuestion = ko.observable(),
                     AdCampaign = ko.observable(0),
                     getBaseData = function ()
                     {
                         dataService.getBaseData(null, {

                             success: function (data) {
                                 Deals(model.EntityServertoClientMapper(updateStatusValue(data.Deals)));
                                 ProfileQuestion(model.EntityServertoClientMapper(updateStatusValue(data.ProfileQuestion)));
                                 AdCampaign(model.EntityServertoClientMapper(updateStatusValue(data.VideoCampaign)));
                             },
                             error: function () {
                             }
                         });

                     },
                      updateStatusValue = function (item) {

                          if (item.Status == 1) {
                              item.StatusValue = "Draft";
                              if (item.Type > 0 && item.Type == 1) {
                                  item.campaigntype = "Video Ad"
                              }
                              else if (item.Type > 0 && item.Type == 4) {
                                  item.campaigntype = "Display Ad"
                              }
                          } else if (item.Status == 2) {
                              if (item.Type > 0 && item.Type == 1) {
                                  item.campaigntype = "Video Ad"
                              }
                              else if (item.Type > 0 && item.Type == 4) {
                                  item.campaigntype = "Display Ad"
                              }
                              item.StatusValue = "Pending Approval"
                          } else if (item.Status == 3) {
                              if (item.Type > 0 && item.Type == 1) {
                                  item.campaigntype = "Video Ad"
                              }
                              else if (item.Type > 0 && item.Type == 4) {
                                  item.campaigntype = "Display Ad"
                              }
                              item.StatusValue = "Live";
                          } else if (item.Status == 4) {
                              if (item.Type > 0 && item.Type == 1) {
                                  item.campaigntype = "Video Ad"
                              }
                              else if (item.Type > 0 && item.Type == 4) {
                                  item.campaigntype = "Display Ad"
                              }
                              item.StatusValue = "Paused"
                          } else if (item.Status == 5) {
                              if (item.Type > 0 && item.Type == 1) {
                                  item.campaigntype = "Video Ad"
                              }
                              else if (item.Type > 0 && item.Type == 4) {
                                  item.campaigntype = "Display Ad"
                              }
                              item.StatusValue = "Completed"
                          } else if (item.Status == 6) {
                              if (item.Type > 0 && item.Type == 1) {
                                  item.campaigntype = "Video Ad"
                              }
                              else if (item.Type > 0 && item.Type == 4) {
                                  item.campaigntype = "Display Ad"
                              }
                              item.StatusValue = "Approval Rejected"
                          } else if (item.Status == 7) {
                              if (item.Type > 0 && item.Type == 1) {
                                  item.campaigntype = "Video Ad"
                              }
                              else if (item.Type > 0 && item.Type == 4) {
                                  item.campaigntype = "Display Ad"
                              }
                              item.StatusValue = ("Remove");
                          } else if (item.Status == 9) {
                              if (item.Type > 0 && item.Type == 1) {
                                  item.campaigntype = "Video Ad"
                              }
                              else if (item.Type > 0 && item.Type == 4) {
                                  item.campaigntype = "Display Ad"
                              }
                              item.StatusValue = ("Completed");
                          } else if (item.Status == 8) {
                              if (item.Type > 0 && item.Type == 1) {
                                  item.campaigntype = "Video Ad"
                              }
                              else if (item.Type > 0 && item.Type == 4) {
                                  item.campaigntype = "Display Ad"
                              }
                              item.StatusValue = ("Archived");
                          }
                          return item;
                      },

                    initialize = function (specifiedView) {
                       
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                       
                        getBaseData();
                    };
                return {
                    initialize: initialize,
                    Deals: Deals,
                    ProfileQuestion: ProfileQuestion,
                    AdCampaign: AdCampaign
                };
            })()
        };
        return ist.inviteUser.viewModel;
    });
