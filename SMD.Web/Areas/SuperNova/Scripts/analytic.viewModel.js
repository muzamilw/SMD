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
                    analyticFromdate = ko.observable();
					analyticTodate = ko.observable();
					getDashboardInsights = function () {
                        dataservice.getDashboardInsights(
                            {
                                                           },
                            {
                                success: function (data) {
									
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
               
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
						getDashboardInsights();
                       // getAnalytic();
                    };
                return {

                    initialize: initialize,
                    getAnalytic: getAnalytic,
					activeUser:activeUser,
					granularityDropDown:granularityDropDown,
					analyticFromdate: analyticFromdate,
					analyticTodate:analyticTodate,
					DashboardInsightsData:DashboardInsightsData

                };
            })()
        };
        return ist.analytic.viewModel;
    });
