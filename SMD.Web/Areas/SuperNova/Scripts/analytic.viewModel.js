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
										if((data[i].ordr ==1) && data[i].pMonth == "current" ){
											DashboardInsightsData()[0].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[0].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[0].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[0].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[0].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==1) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[0].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[0].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[0].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[0].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[0].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==2) && data[i].pMonth == "current" ){
											DashboardInsightsData()[1].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[1].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[1].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[1].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[1].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==2) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[1].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[1].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[1].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[1].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[1].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==3) && data[i].pMonth == "current" ){
											DashboardInsightsData()[5].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[5].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[5].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[5].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[5].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==3) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[5].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[5].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[5].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[5].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[5].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==4) && data[i].pMonth == "current" ){
											DashboardInsightsData()[6].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[6].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[6].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[6].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[6].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==4) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[6].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[6].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[6].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[6].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[6].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==5) && data[i].pMonth == "current" ){
											DashboardInsightsData()[4].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[4].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[4].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[4].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[4].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==5) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[4].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[4].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[4].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[4].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[4].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==6) && data[i].pMonth == "current" ){
											DashboardInsightsData()[7].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[7].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[7].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[7].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[7].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==6) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[7].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[7].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[7].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[7].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[7].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==7) && data[i].pMonth == "current" ){
											DashboardInsightsData()[8].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[8].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[8].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[8].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[8].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==7) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[8].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[8].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[8].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[8].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[8].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==8) && data[i].pMonth == "current" ){
											DashboardInsightsData()[12].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[12].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[12].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[12].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[12].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==8) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[12].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[12].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[12].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[12].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[12].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==9) && data[i].pMonth == "current" ){
											DashboardInsightsData()[13].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[13].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[13].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[13].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[13].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==9) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[13].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[13].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[13].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[13].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[13].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==10) && data[i].pMonth == "current" ){
											DashboardInsightsData()[11].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[11].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[11].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[11].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[11].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==10) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[11].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[11].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[11].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[11].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[11].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==11) && data[i].pMonth == "current" ){
											DashboardInsightsData()[14].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[14].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[14].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[14].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[14].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==11) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[14].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[14].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[14].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[14].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[14].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==12) && data[i].pMonth == "current" ){
											DashboardInsightsData()[15].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[15].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[15].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[15].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[15].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==12) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[15].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[15].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[15].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[15].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[15].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==13) && data[i].pMonth == "current" ){
											DashboardInsightsData()[18].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[18].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[18].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[18].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[18].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==13) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[18].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[18].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[18].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[18].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[18].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==14) && data[i].pMonth == "current" ){
											DashboardInsightsData()[19].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[19].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[19].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[19].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[19].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==14) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[19].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[19].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[19].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[19].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[19].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==15) && data[i].pMonth == "current" ){
											DashboardInsightsData()[20].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[20].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[20].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[20].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[20].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==15) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[20].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[20].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[20].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[20].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[20].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==16) && data[i].pMonth == "current" ){
											DashboardInsightsData()[21].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[21].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[21].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[21].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[21].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==16) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[21].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[21].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[21].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[21].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[21].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==17) && data[i].pMonth == "current" ){
											DashboardInsightsData()[22].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[22].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[22].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[22].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[22].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==17) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[22].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[22].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[22].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[22].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[22].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==18) && data[i].pMonth == "current" ){
											DashboardInsightsData()[23].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[23].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[23].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[23].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[23].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==18) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[23].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[23].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[23].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[23].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[23].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==19) && data[i].pMonth == "current" ){
											DashboardInsightsData()[24].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[24].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[24].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[24].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[24].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==19) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[24].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[24].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[24].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[24].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[24].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==20) && data[i].pMonth == "current" ){
											DashboardInsightsData()[25].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[25].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[25].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[25].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[25].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==20) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[25].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[25].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[25].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[25].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[25].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==21) && data[i].pMonth == "current" ){
											DashboardInsightsData()[26].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[26].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[26].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[26].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[26].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==21) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[26].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[26].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[26].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[26].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[26].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==22) && data[i].pMonth == "current" ){
											DashboardInsightsData()[27].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[27].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[27].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[27].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[27].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==22) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[27].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[27].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[27].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[27].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[27].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==23) && data[i].pMonth == "current" ){
											DashboardInsightsData()[28].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[28].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[28].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[28].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[28].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==23) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[28].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[28].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[28].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[28].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[28].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==24) && data[i].pMonth == "current" ){
											DashboardInsightsData()[30].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[30].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[30].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[30].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[30].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==24) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[30].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[30].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[30].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[30].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[30].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==25) && data[i].pMonth == "current" ){
											DashboardInsightsData()[29].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[29].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[29].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[29].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[29].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==25) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[29].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[29].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[29].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[29].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[29].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==26) && data[i].pMonth == "current" ){
											DashboardInsightsData()[33].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[33].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[33].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[33].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[33].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==26) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[33].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[33].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[33].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[33].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[33].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==27) && data[i].pMonth == "current" ){
											DashboardInsightsData()[34].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[34].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[34].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[34].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[34].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==27) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[34].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[34].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[34].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[34].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[34].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==29) && data[i].pMonth == "current" ){
											DashboardInsightsData()[35].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[35].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[35].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[35].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[35].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==29) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[35].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[35].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[35].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[35].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[35].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==30) && data[i].pMonth == "current" ){
											DashboardInsightsData()[36].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[36].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[36].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[36].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[36].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==30) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[36].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[36].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[36].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[36].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[36].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==31) && data[i].pMonth == "current" ){
											DashboardInsightsData()[37].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[37].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[37].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[37].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[37].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==31) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[37].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[37].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[37].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[37].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[37].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==32) && data[i].pMonth == "current" ){
											DashboardInsightsData()[38].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[38].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[38].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[38].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[38].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].ordr ==32) && data[i].pMonth == "prev" ){
											DashboardInsightsData()[38].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[38].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[38].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[38].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[38].aeL(data[i].ae != null ? data[i].ae:0);
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
               
                    getAnalytic = function () {
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
