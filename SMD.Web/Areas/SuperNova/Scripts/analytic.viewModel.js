﻿/*
    Module with the view model for the AdCampaign
*/
define("analytic/analytic.viewModel",
    ["jquery", "amplify", "ko", "analytic/analytic.dataservice", "analytic/analytic.model"],
    function ($, amplify, ko, dataservice, model) {
        var ist = window.ist || {};
        ist.analytic = {
            viewModel: (function () {
                var view,
                    activeUser = ko.observable(),
					DashboardInsightsData = ko.observableArray(),
					granularityDropDown = ko.observableArray([{ id: 1, name: "daily" }, { id: 2, name: "weekly" }, { id: 3, name: "monthly" }, { id: 4, name: "yearly" }]),
                    analyticFromdate = ko.observable(new Date()),
					analyticTodate = ko.observable(new Date()),
					selectedGranualforRevenue = ko.observable(1),
					RevenueOverTimeData = ko.observable([]),
					CampaignsByStatusData = ko.observable([]),
					UserCountsData = ko.observable([]),
					intializeDashboardInsightsData = function(){
						DashboardInsightsData.push(new model.DashboardInsightsModel("Users who logged in"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("New Users who registered"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Advertisers who created a NEW campaign"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("New Video Ad campaigns"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("New Display Ad campaigns"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("New Survey campaigns"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("New Poll campaigns"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("",true));
						DashboardInsightsData.push(new model.DashboardInsightsModel("# Answered", true));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Video Ads (Ad click charged)"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Display Ads (Ad click charged)"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Surveys"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Polls"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("",true));
						DashboardInsightsData.push(new model.DashboardInsightsModel("# Skipped",true));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Video Ads"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Display Ads"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Surveys"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Polls"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("",true));
						DashboardInsightsData.push(new model.DashboardInsightsModel("#Conversion to landing pages", true));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Videos Ads"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Display Ads"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Deals"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("",true));
						DashboardInsightsData.push(new model.DashboardInsightsModel("# Openeded", true));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Video Ads"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Display Ads"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Surveys"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Polls"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Deals viewed (Opened)"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("",true));
						DashboardInsightsData.push(new model.DashboardInsightsModel("$ Revenue by Campaign types", true));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Video Ads"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Display Ads"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Surveys"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Polls"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Deals"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("",true));
						DashboardInsightsData.push(new model.DashboardInsightsModel("$ Total Revenue (Stripe)"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("",true));
						DashboardInsightsData.push(new model.DashboardInsightsModel("$ Total Payouts (PayPal)"));
						getDashboardInsights();
					},
					getDashboardInsights = function () {
                        dataservice.getDashboardInsights(
                            {
                                                           },
                            {
                                success: function (data) {
								
									for(var i=0;i<data.length;i++){
										if(data[i].pMonth == "current" ){
											DashboardInsightsData()[data[i].ordr].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[data[i].ordr].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[data[i].ordr].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[data[i].ordr].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[data[i].ordr].aeC(data[i].ae != null ? data[i].ae:0);
											DashboardInsightsData()[data[i].ordr].ordr(data[i].ordr);
										}else if(data[i].pMonth == "prev" ){
											DashboardInsightsData()[data[i].ordr].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[data[i].ordr].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[data[i].ordr].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[data[i].ordr].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[data[i].ordr].aeL(data[i].ae != null ? data[i].ae:0);
											DashboardInsightsData()[data[i].ordr].ordr(data[i].ordr);
										}
										
									}
									
									for(var i=0;i<DashboardInsightsData().length;i++){
										if(DashboardInsightsData()[i].usL() > 0){
											DashboardInsightsData()[i].usT(Math.round(((DashboardInsightsData()[i].usC() - DashboardInsightsData()[i].usL())/DashboardInsightsData()[i].usL())*100));
										}else{
											DashboardInsightsData()[i].usT(DashboardInsightsData()[i].usC()*100);
										}
										if(DashboardInsightsData()[i].ukL() > 0){
											DashboardInsightsData()[i].ukT(Math.round(((DashboardInsightsData()[i].ukC() - DashboardInsightsData()[i].ukL())/DashboardInsightsData()[i].ukL())*100));
										}else{
											DashboardInsightsData()[i].ukT(DashboardInsightsData()[i].ukC()*100);
										}
										if(DashboardInsightsData()[i].caL() > 0){
											DashboardInsightsData()[i].caT(Math.round(((DashboardInsightsData()[i].caC() - DashboardInsightsData()[i].caL())/DashboardInsightsData()[i].caL())*100));
										}else{
											DashboardInsightsData()[i].caT(DashboardInsightsData()[i].caC()*100);
										}
										if(DashboardInsightsData()[i].auL() > 0){
											DashboardInsightsData()[i].auT(Math.round(((DashboardInsightsData()[i].auC() - DashboardInsightsData()[i].auL())/DashboardInsightsData()[i].auL())*100));
										}else{
											DashboardInsightsData()[i].auT(DashboardInsightsData()[i].auC()*100);
										}
										if(DashboardInsightsData()[i].aeL() > 0){
											DashboardInsightsData()[i].aeT(Math.round(((DashboardInsightsData()[i].aeC() - DashboardInsightsData()[i].aeL())/DashboardInsightsData()[i].aeL())*100));
										}else{
											DashboardInsightsData()[i].aeT(DashboardInsightsData()[i].aeC()*100);
										}
										
									}
	                            },
                                error: function (response) {
                                    toastr.error("Failed to load Ad Campaigns!");
                                }
                            });
                    },
               
                    getActiveUsers = function () {
                        dataservice.getactiveuser(
                            {
                             },
                            {
                                success: function (data) {
									activeUser([]);
									ko.utils.arrayPushAll(activeUser(), data);
									activeUser.valueHasMutated();
								},
                                error: function (response) {
                                    toastr.error("Failed to load Ad Campaigns!");
                                }
                            });
                    },
               
			   getRevenueOverTime = function () {
                        dataservice.getRevenueOverTime(
                            {
								granuality: selectedGranualforRevenue(),
								DateFrom: analyticFromdate().toISOString().substring(0, 10),
								DateTo :analyticTodate().toISOString().substring(0, 10)
                                                           },
                            {
                                success: function (data) {
									//data[0].amountcollected 
									//data[0].granular
								RevenueOverTimeData([]);
                                ko.utils.arrayPushAll(RevenueOverTimeData(), data);
                                RevenueOverTimeData.valueHasMutated();
	                            },
                                error: function (response) {
                                    toastr.error("Failed to load Ad Campaigns!");
                                }
                            });
                    },
					 getCampaignsByStatus = function () {
                        dataservice.getCampaignsByStatus(
                            { },
                            {
                                success: function (data) {
								
								CampaignsByStatusData([]);
                                ko.utils.arrayPushAll(CampaignsByStatusData(), data);
                                CampaignsByStatusData.valueHasMutated();
	                            },
                                error: function (response) {
                                    toastr.error("Failed to load Ad Campaigns!");
                                }
                            });
                    },
					getUserCounts = function () {
                        dataservice.getUserCounts(
                            { },
                            {
                                success: function (data) {
								
								UserCountsData([]);
                                ko.utils.arrayPushAll(UserCountsData(), data);
                                UserCountsData.valueHasMutated();
	                            },
                                error: function (response) {
                                    toastr.error("Failed to load Ad Campaigns!");
                                }
                            });
                    },
					ReloadAnalytic = function () {
						getRevenueOverTime();
						
					},
                    // Initialize the view model
                    initialize = function (specifiedView) {
						
                        view = specifiedView;
						analyticFromdate().setMonth(analyticFromdate().getMonth()-1)
                        ko.applyBindings(view.viewModel, view.bindingRoot);
						intializeDashboardInsightsData();
						getCampaignsByStatus();
						getUserCounts();
					//	getActiveUsers();
						getRevenueOverTime();
                    };
                return {

                    initialize: initialize,
                    getActiveUsers: getActiveUsers,
					activeUser:activeUser,
					granularityDropDown:granularityDropDown,
					analyticFromdate: analyticFromdate,
					analyticTodate:analyticTodate,
					DashboardInsightsData:DashboardInsightsData,
					selectedGranualforRevenue:selectedGranualforRevenue,
					RevenueOverTimeData:RevenueOverTimeData,
					getRevenueOverTime:getRevenueOverTime,
					ReloadAnalytic:ReloadAnalytic,
					getCampaignsByStatus : getCampaignsByStatus,
					CampaignsByStatusData:CampaignsByStatusData,
					getUserCounts:getUserCounts,
					UserCountsData:UserCountsData

                };
            })()
        };
        return ist.analytic.viewModel;
    });
