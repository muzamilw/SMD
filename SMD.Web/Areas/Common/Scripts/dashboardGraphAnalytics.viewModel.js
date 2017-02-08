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
                     Draftlist = ko.observableArray([]),
                     currentDraft = ko.observable(),
                     LiveList = ko.observableArray([]),
                     PauseList = ko.observableArray([]),
                     ApprovalList = ko.observableArray([]),
                     AnalyticsList = ko.observableArray([]),

                     LiveVidCampaign = ko.observable(0),
                     LiveDisplayCampaign = ko.observable(0),
                     LiveCoupons = ko.observable(0),
                     LivePolls = ko.observable(0),
                     FreeAdsCounter = ko.observable(0),

                     getBaseData = function ()
                     {
                         //dataService.getBaseData(null, {

                         //    success: function (data) {
                                
                         //        _.each(data.AnalyticsList, function (item, index) {
                                     
                        
                                    
                         //            AnalyticsList.push(CreateGraph(model.EntityServertoClientMapper(updateStatusValue(item, index)), index));
                         //        });
                                 
                         //    },
                         //    error: function () {
                         //    }
                         //});


                         dataService.GetCounters(null, {
                             
                             success: function (data) {
                                 debugger;
                                 LiveVidCampaign(data.LiveVideoCampaign);
                                 LiveDisplayCampaign(data.LiveDisplayCampaign);
                                 LiveCoupons(data.LiveDeals);
                                 LivePolls(data.LivePolls);
                                 FreeAdsCounter(data.FreeAdsCounter);

                             },
                             error: function () {
                             }
                         });
                     },
                     CreateGraph = function (data,index)
                     {
                        
                         var gdata = [];

                         if (data.PQID  > 0)
                         {
                             var serial1 = new Array(data.Option1, data.Option1Percentage);
                             gdata.push(serial1);
                             var serial2 = new Array(data.Option2, data.Option2Percentage);
                             gdata.push(serial2);
                             var serial3 = new Array(data.Option3, data.Option3Percentage);
                             gdata.push(serial3);
                             var serial4 = new Array(data.Option4, data.Option4Percentage);
                             gdata.push(serial4);
                             var serial5 = new Array(data.Option5, data.Option5Percentage);
                             gdata.push(serial5);
                             var serial6 = new Array(data.Option6, data.Option6Percentage);
                             gdata.push(serial6);
                             data.DivId='Pq'+index;

                             //var chart = new Highcharts.Chart({

                             //    chart: {
                             //        renderTo:'abc',   //Target Div for chart
                             //        defaultSeriesType: 'pie'
                             //    },

                             //    series: [{
                             //        data: gdata   //Binding json data to chart
                             //    }]
                             //});
                         }
                         else if (data.CampaignID > 0)
                         {
                             var serial1 = new Array(data.Answer1, data.Ans1Percentage);
                             gdata.push(serial1);
                             var serial2 = new Array(data.Answer2, data.Ans2Percentage);
                             gdata.push(serial2);
                             var serial3 = new Array(data.Answer3, data.Ans3Percentage);
                             gdata.push(serial3);
                             data.DivId='camp'+index;

                             //var chart = new Highcharts.Chart({

                             //    chart: {
                             //        renderTo: data.DivId,   //Target Div for chart
                             //        defaultSeriesType: 'pie'
                             //    },

                             //    series: [{
                             //        data: gdata   //Binding json data to chart
                             //    }]
                             //});
                         }

                         return data;
                     },

                      updateStatusValue = function (item,index) {

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
                          return CreateGraph(item, index)
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
                    AdCampaign: AdCampaign,
                    Draftlist: Draftlist,
                    ApprovalList:ApprovalList,
                    PauseList:PauseList,
                    LiveList: LiveList,
                    AnalyticsList: AnalyticsList,
                    LiveVidCampaign:LiveVidCampaign,
                    LiveDisplayCampaign:LiveDisplayCampaign,
                    LiveCoupons:LiveCoupons,
                    LivePolls: LivePolls,
                    FreeAdsCounter: FreeAdsCounter
                };
            })()
        };
        return ist.inviteUser.viewModel;
    });
