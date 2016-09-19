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
                    analyticFromdate = ko.observable(),
					analyticTodate = ko.observable(),
					selectedGranualforRevenue = ko.observable(1),
					RevenueOverTimeData = ko.observable([]),
					intializeDashboardInsightsData = function(){
						DashboardInsightsData.push(new model.DashboardInsightsModel("Active App User"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("New App User"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Active Campaigns"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("New Campaigns"));
						DashboardInsightsData.push(new model.DashboardInsightsModel(""));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Coupons Approved"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Coupons Purchased"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Coupons Redeemed"));
						DashboardInsightsData.push(new model.DashboardInsightsModel(""));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Video Ads Delivered"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Game Ads Delivered"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Survey Cards Campaigns"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Profile Questions Campaingns"));
						DashboardInsightsData.push(new model.DashboardInsightsModel(""));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Video Ads Revenue"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Game Ads Revenue"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Survey Cards Revenue"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Profile Questions Revenue"));
						DashboardInsightsData.push(new model.DashboardInsightsModel(""));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Payout via Paypal"));
						DashboardInsightsData.push(new model.DashboardInsightsModel("Income from Stripe"));
						getDashboardInsights();
					},
					getDashboardInsights = function () {
                        dataservice.getDashboardInsights(
                            {
                                                           },
                            {
                                success: function (data) {
								
									for(var i=0;i<data.length;i++){
										if((data[i].rectype =="Active App Users") && data[i].pMonth == "current" ){
											DashboardInsightsData()[0].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[0].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[0].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[0].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[0].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].rectype =="Active App Users") && data[i].pMonth == "prev" ){
											DashboardInsightsData()[0].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[0].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[0].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[0].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[0].aeL(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].rectype =="New App Users") && data[i].pMonth == "current" ){
											DashboardInsightsData()[1].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[1].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[1].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[1].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[1].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].rectype =="New App Users") && data[i].pMonth == "prev" ){
											DashboardInsightsData()[1].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[1].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[1].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[1].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[1].aeL(data[i].ae != null ? data[i].ae:0);
										 }else if((data[i].rectype =="Active Campaigns") && data[i].pMonth == "current" ){
											DashboardInsightsData()[2].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[2].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[2].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[2].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[2].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].rectype =="Active Campaigns") && data[i].pMonth == "prev" ){
											DashboardInsightsData()[2].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[2].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[2].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[2].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[2].aeL(data[i].ae != null ? data[i].ae:0); 
										}else if((data[i].rectype =="New Campaigns") && data[i].pMonth == "current" ){
											DashboardInsightsData()[3].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[3].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[3].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[3].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[3].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].rectype =="New Campaigns") && data[i].pMonth == "prev" ){
											DashboardInsightsData()[3].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[3].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[3].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[3].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[3].aeL(data[i].ae != null ? data[i].ae:0); 
										}else if((data[i].rectype =="Coupons approved") && data[i].pMonth == "current" ){
											DashboardInsightsData()[5].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[5].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[5].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[5].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[5].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].rectype =="Coupons approved") && data[i].pMonth == "prev" ){
											DashboardInsightsData()[5].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[5].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[5].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[5].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[5].aeL(data[i].ae != null ? data[i].ae:0); 
										}else if((data[i].rectype =="Coupons Purchased") && data[i].pMonth == "current" ){
											DashboardInsightsData()[6].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[6].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[6].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[6].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[6].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].rectype =="Coupons Purchased") && data[i].pMonth == "prev" ){
											DashboardInsightsData()[6].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[6].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[6].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[6].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[6].aeL(data[i].ae != null ? data[i].ae:0); 
										}else if((data[i].rectype =="Coupons Redeemed") && data[i].pMonth == "current" ){
											DashboardInsightsData()[7].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[7].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[7].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[7].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[7].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].rectype =="Coupons Redeemed") && data[i].pMonth == "prev" ){
											DashboardInsightsData()[7].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[7].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[7].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[7].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[7].aeL(data[i].ae != null ? data[i].ae:0); 
										}else if((data[i].rectype =="Video Ads delivered") && data[i].pMonth == "current" ){
											DashboardInsightsData()[9].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[9].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[9].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[9].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[9].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].rectype =="Video Ads delivered") && data[i].pMonth == "prev" ){
											DashboardInsightsData()[9].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[9].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[9].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[9].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[9].aeL(data[i].ae != null ? data[i].ae:0); 
										}else if((data[i].rectype =="Games Ads delivered") && data[i].pMonth == "current" ){
											DashboardInsightsData()[10].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[10].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[10].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[10].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[10].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].rectype =="Games Ads delivered") && data[i].pMonth == "prev" ){
											DashboardInsightsData()[10].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[10].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[10].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[10].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[10].aeL(data[i].ae != null ? data[i].ae:0); 
										}else if((data[i].rectype =="Survey Question Answered") && data[i].pMonth == "current" ){
											DashboardInsightsData()[11].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[11].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[11].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[11].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[11].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].rectype =="Survey Question Answered") && data[i].pMonth == "prev" ){
											DashboardInsightsData()[11].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[11].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[11].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[11].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[11].aeL(data[i].ae != null ? data[i].ae:0); 
										}else if((data[i].rectype =="Profile Question Answered") && data[i].pMonth == "current" ){
											DashboardInsightsData()[12].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[12].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[12].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[12].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[12].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].rectype =="Profile Question Answered") && data[i].pMonth == "prev" ){
											DashboardInsightsData()[12].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[12].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[12].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[12].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[12].aeL(data[i].ae != null ? data[i].ae:0); 
										}else if((data[i].rectype =="Video Ads Revenue") && data[i].pMonth == "current" ){
											DashboardInsightsData()[14].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[14].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[14].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[14].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[14].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].rectype =="Video Ads Revenue") && data[i].pMonth == "prev" ){
											DashboardInsightsData()[14].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[14].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[14].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[14].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[14].aeL(data[i].ae != null ? data[i].ae:0); 
										}else if((data[i].rectype =="Game Ads Revenue") && data[i].pMonth == "current" ){
											DashboardInsightsData()[15].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[15].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[15].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[15].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[15].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].rectype =="Game Ads Revenue") && data[i].pMonth == "prev" ){
											DashboardInsightsData()[15].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[15].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[15].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[15].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[15].aeL(data[i].ae != null ? data[i].ae:0); 
										}else if((data[i].rectype =="Survay Cards Revenue") && data[i].pMonth == "current" ){
											DashboardInsightsData()[16].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[16].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[16].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[16].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[16].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].rectype =="Survay Cards Revenue") && data[i].pMonth == "prev" ){
											DashboardInsightsData()[16].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[16].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[16].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[16].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[16].aeL(data[i].ae != null ? data[i].ae:0); 
										}else if((data[i].rectype =="Profile Questions Revenue") && data[i].pMonth == "current" ){
											DashboardInsightsData()[17].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[17].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[17].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[17].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[17].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].rectype =="Profile Questions Revenue") && data[i].pMonth == "prev" ){
											DashboardInsightsData()[17].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[17].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[17].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[17].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[17].aeL(data[i].ae != null ? data[i].ae:0); 
										}else if((data[i].rectype =="Payout via PayPal") && data[i].pMonth == "current" ){
											DashboardInsightsData()[19].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[19].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[19].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[19].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[19].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].rectype =="Payout via PayPal") && data[i].pMonth == "prev" ){
											DashboardInsightsData()[19].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[19].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[19].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[19].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[19].aeL(data[i].ae != null ? data[i].ae:0); 
										}else if((data[i].rectype =="Income from Stripe") && data[i].pMonth == "current" ){
											DashboardInsightsData()[20].usC(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[20].ukC(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[20].caC(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[20].auC(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[20].aeC(data[i].ae != null ? data[i].ae:0);
										}else if((data[i].rectype =="Income from Stripe") && data[i].pMonth == "prev" ){
											DashboardInsightsData()[20].usL(data[i].us != null ? data[i].us:0);
											DashboardInsightsData()[20].ukL(data[i].uk != null ? data[i].uk:0);
											DashboardInsightsData()[20].caL(data[i].ca!= null ? data[i].ca:0);
											DashboardInsightsData()[20].auL(data[i].au != null ? data[i].au:0);
											DashboardInsightsData()[20].aeL(data[i].ae != null ? data[i].ae:0); 
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
								DateFrom: '2016-08-02',
								DateTo :'2016-09-17'
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
               
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
						intializeDashboardInsightsData();
						getAnalytic();
						getRevenueOverTime();
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
					getRevenueOverTime:getRevenueOverTime

                };
            })()
        };
        return ist.analytic.viewModel;
    });
