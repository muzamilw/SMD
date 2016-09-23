define("Report/walletReport.viewModel",
    ["jquery", "amplify", "ko", "Report/walletReport.dataservice", "Report/walletReport.model", "common/pagination",
     "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation) {
        var ist = window.ist || {};
        ist.Report = {
            viewModel: (function () {
                var view,
                    profileQuestion = ko.observableArray([]),
                    //pager
                   
                
                    getProfileQuestions = function () {

                        alert(success);
                        //dataservice.getPQForApproval(
                        //    {
                        //        PageSize: pager().pageSize(),
                        //        PageNo: pager().currentPage(),
                        //        SortBy: sortOn(),
                        //        IsAsc: sortIsAsc(),

                        //    },
                        //    {
                        //        success: function (data) {
                        //            profileQuestion.removeAll();
                        //            console.log("approval Profile Question");
                        //            console.log(data.ProfileQuestion);
                        //            _.each(data.ProfileQuestion, function (item) {
                        //                profileQuestion.push(model.ProfileQuestionServertoClientMapper(item));
                        //            });
                        //            ////pager().totalCount(0);
                        //            pager().totalCount(data.TotalCount);
                        //        },
                        //        error: function () {
                        //            toastr.error("Failed to load Ad Campaigns!");
                        //        }
                        //    });
                    },
                   
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        getProfileQuestions();
                    };
                return {

                    initialize: initialize,
                    getProfileQuestions: getProfileQuestions,
                };
            })()
        };
        return ist.Report.viewModel;
    });