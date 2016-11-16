/*
    View for the User. Used to keep the viewmodel clear of UI related logic
*/
define("common/dashboardGraphAnalytics.view",
    ["jquery", "common/dashboardGraphAnalytics.viewModel"], function ($, inviteUserViewModel) {
        var ist = window.ist || {};
        // View 
        debugger;
        ist.inviteUser.view = (function (specifiedViewModel) {
            var
                // View model 
                
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#bindingRootgraphs")[0],
               //AdCampaign

               InilializeAdCampaignCharts = function (AdCampagin)
               {
                   
                   
                   var ctx = $("#mycanvas").get(0).getContext("2d");

                   //pie chart data
                   //sum of values = 360
                   var data = [
                       {
                           value: AdCampagin.ans1Percentage,
                           color: "cornflowerblue",
                           highlight: "lightskyblue",
                           label: AdCampagin.answer1
                       },
                       {
                           value: AdCampagin.ans2Percentage,
                           color: "lightgreen",
                           highlight: "yellowgreen",
                           label: AdCampagin.answer2
                       },
                       {
                           value: AdCampagin.ans2Percentage,
                           color: "orange",
                           highlight: "darkorange",
                           label: AdCampagin.answer3
                       }
                       
                    
                   ];

                   //draw
                   var piechart = new Chart(ctx).Pie(data);

               },

                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }
                };
            initialize();
           // InilializeCharts();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel
         
            };
        })(inviteUserViewModel);
        // Initialize the view model
        if (ist.inviteUser.view.bindingRoot) {
            
            inviteUserViewModel.initialize(ist.inviteUser.view);
            setTimeout(function () {
                
                var AdCampagin = inviteUserViewModel.AdCampaign();

                var ProfileQuestion = inviteUserViewModel.ProfileQuestion();

                var ctx = $("#mycanvas").get(0).getContext("2d");
                var ctx1 = $("#mycanvas1").get(0).getContext("2d");
                var ctx2 = $("#mycanvasProfileQuestion").get(0).getContext("2d");
              
                var AdCampaignData = [
                    {
                        value: AdCampagin.ans1Percentage(),
                        color: "cornflowerblue",
                        highlight: "lightskyblue",
                        label: AdCampagin.answer1()
                    },
                    {
                        value: AdCampagin.ans2Percentage(),
                        color: "lightgreen",
                        highlight: "yellowgreen",
                        label: AdCampagin.answer2()
                    },
                    {
                        value: AdCampagin.ans3Percentage(),
                        color: "orange",
                        highlight: "darkorange",
                        label: AdCampagin.answer3()
                    }


                ];

                var ProfileQuestionData = [
                   {
                       value: ProfileQuestion.option1Percentage(),
                       color: "cornflowerblue",
                       highlight: "lightskyblue",
                       label: ProfileQuestion.option1()
                   },
                   {
                       value: ProfileQuestion.option2Percentage(),
                       color: "lightgreen",
                       highlight: "yellowgreen",
                       label: ProfileQuestion.option2()
                   },
                   {
                       value: ProfileQuestion.option3Percentage(),
                       color: "orange",
                       highlight: "darkorange",
                       label: ProfileQuestion.option3()
                   },
                    {
                        value: ProfileQuestion.option4Percentage(),
                        color: "pink",
                        highlight: "darkorange",
                        label: ProfileQuestion.option4()
                    },
                     {
                         value: ProfileQuestion.option5Percentage(),
                         color: "red",
                         highlight: "darkorange",
                         label: ProfileQuestion.option5()
                     },
                      {
                          value: ProfileQuestion.option6Percentage(),
                          color: "blue",
                          highlight: "darkorange",
                          label: ProfileQuestion.option5()
                      }

                ];

                //draw
                var piechart = new Chart(ctx).Pie(AdCampaignData);
                var piechart1 = new Chart(ctx1).Pie(AdCampaignData);
                var piechart3 = new Chart(ctx2).Pie(ProfileQuestionData);
            }, 2000);
           
        }
        //if (ist.inviteUser.view.bindingRootUser) {
        //    userViewModel.initialize(ist.inviteUser.view);
        //}
        return ist.inviteUser.view;
    });
