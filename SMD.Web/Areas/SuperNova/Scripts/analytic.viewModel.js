/*
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
					granularityDropDown = ko.observableArray([{ id: 1, name: "Day" }, { id: 2, name: "Week" }, { id: 3, name: "Month" }, { id: 4, name: "Year" }]),
                    analyticFromdate = ko.observable(new Date()),
					analyticTodate = ko.observable(new Date()),
					selectedGranualforRevenue = ko.observable(1),
					RevenueOverTimeData = ko.observable([]),
					intializeDashboardInsightsData = function(){
						DashboardInsightsData.push(new model.DashboardInsightsModel("Active App Users"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("New App Users"));
						DashboardInsightsData.push(new model.DashboardInsightsModel(""));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Approved Campaigns", true));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Deals Campaigns"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Video Campaigns"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Game Campaigns"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Survey card Campaigns"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Survey question Campaigns"));
						DashboardInsightsData.push(new model.DashboardInsightsModel(""));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Active Campaigns", true));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Deals Campaigns"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Video Campaigns"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Game Campaigns"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Survey card Campaigns"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Survey question Campaigns"));
						DashboardInsightsData.push(new model.DashboardInsightsModel(""));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Impressions", true));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Deals viewed"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Deals referred to landing pages"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Deals Saved"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Deals Redeemed"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Videos referred to landing pages"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Video Quiz Ad clicks"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Game quiz Ad clicks"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Survey questions answered"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Survey cards answered"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Video ads skipped"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Games skipped"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Survey cards skipped"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Survey question skipped"));
						DashboardInsightsData.push(new model.DashboardInsightsModel(""));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Revenue", true));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Video Ad revenue"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Game Ad revenue"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Survey card revenue"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Survey Question revenue"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("All Revenue (income from stripe)"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Payout via PayPal"));
						
						getDashboardInsights();
					},
					getDashboardInsights = function () {
                        dataservice.getDashboardInsights(
                            {
                                                           },
                            {
                                success: function (data) {
								
									for(var i=0;i<data.length;i++){
										
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
               
                    getAnalytic = function () {
                        dataservice.getactiveuser(
                            {
                                                           },
                            {
                                success: function (data) {
									activeUser([
											{ y: '1 day' , a: data.Last1DayActiveUser },
											{ y: '7 days' , a: data.Last7DayActiveUser },
											{ y: '14 days' , a: data.Last14DayActiveUser },
											{ y: '30 days' , a: data.Last30DayActiveUser },
											{ y: '3 months' , a: data.Last3MonthsActiveUser }
										]);
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
					ReloadAnalytic = function () {
						getRevenueOverTime();
						
					},
                    // Initialize the view model
                    initialize = function (specifiedView) {
						
                        view = specifiedView;
						analyticFromdate().setMonth(analyticFromdate().getMonth()-1)
                        ko.applyBindings(view.viewModel, view.bindingRoot);
						intializeDashboardInsightsData();
						//getAnalytic();
						//getRevenueOverTime();
                    };
                return {

                    initialize: initialize,
                    getAnalytic: getAnalytic,
					activeUser:activeUser,
					granularityDropDown:granularityDropDown,
					analyticFromdate: analyticFromdate,
					analyticTodate:analyticTodate,
					DashboardInsightsData:DashboardInsightsData,
					selectedGranualforRevenue:selectedGranualforRevenue,
					RevenueOverTimeData:RevenueOverTimeData,
					getRevenueOverTime:getRevenueOverTime,
					ReloadAnalytic:ReloadAnalytic

                };
            })()
        };
        return ist.analytic.viewModel;
    });
