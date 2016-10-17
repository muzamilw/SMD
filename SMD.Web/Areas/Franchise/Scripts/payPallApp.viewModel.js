/*
    Module with the view model for the PayPall
*/
define("FranchiseDashboard/payPallApp.viewModel",
    ["jquery", "amplify", "ko", "FranchiseDashboard/payPallApp.dataservice", "FranchiseDashboard/payPallApp.model", "common/pagination",
     "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation) {
        var ist = window.ist || {};
        ist.PayPall = {
            viewModel: (function () {
                var view,
                    payOutHistory = ko.observableArray([]),
                    //pager
                    pager = ko.observable(),
                    //sorting
                    sortOn = ko.observable(5),
                    //Assending  / Desending
                    sortIsAsc = ko.observable(true),
                       getPayOutHistory = function () {
                           dataservice.getpayOutHistoryForApproval(
                                {
                                    PageSize: pager().pageSize(),
                                    PageNo: pager().currentPage(),
                                    SortBy: sortOn(),
                                    IsAsc: sortIsAsc(),
                                    isFlage:true,

                                },
                                {
                                    success: function (data) {
                                        payOutHistory.removeAll();
                                        console.log("approval Profile Question");
                                        console.log(data.PayOutHistory);
                                        _.each(data.PayOutHistory, function (item) {
                                            payOutHistory.push(model.PayPallAppServertoClientMapper(item));
                                        });
                                        ////pager().totalCount(0);
                                        pager().totalCount(data.TotalCount);
                                    },
                                    error: function () {
                                        toastr.error("Failed to load data");
                                    }
                                });
                        },
                 
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        pager(pagination.Pagination({ PageSize: 10 }, payOutHistory, getPayOutHistory));
                        var mode = getParameter(window.location.href, "mode");
                        //if (mode == 2) {
                        //    lblPageTitle("Coupons For Approval");
                        //    isShowCopounMode(true);
                        //}
                        //// First request for LV
                        getPayOutHistory();
                    };
                return {

                    initialize: initialize,
                    payOutHistory: payOutHistory,
                    pager: pager,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    getPayOutHistory: getPayOutHistory
                   

                };
            })()
        };
        return ist.PayPall.viewModel;
    });