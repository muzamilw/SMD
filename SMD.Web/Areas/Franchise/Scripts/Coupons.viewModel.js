/*
    Module with the view model for the AdCampaign
*/
define("FranchiseDashboard/Coupons.viewModel",
    ["jquery", "amplify", "ko", "FranchiseDashboard/Coupons.dataservice", "FranchiseDashboard/Coupons.model", "common/pagination",
     "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation) {
        var ist = window.ist || {};
        ist.Coupons = {
            viewModel: (function () {
                var view,
                    campaigns = ko.observableArray([]),
                    //pager
                    pager = ko.observable(),
                    //sorting
                    sortOn = ko.observable(5),
                    //Assending  / Desending
                    sortIsAsc = ko.observable(true),
                      isShowCopounMode = ko.observable(false),
                    getCampaigns = function () {
                        dataservice.getCouponsForApproval(
                            {
                                PageSize: pager().pageSize(),
                                PageNo: pager().currentPage(),
                                SortBy: sortOn(),
                                IsAsc: sortIsAsc(),
                                
                            },
                            {
                                success: function (data) {
                                    campaigns.removeAll();
                                    console.log("approval campaign");
                                    //console.log(data.AdCampaigns);
                                    //_.each(data.AdCampaigns, function (item) {
                                    //    campaigns.push(model.AdCampaignServertoClientMapper(item));
                                    //});
                                    //pager().totalCount(0);
                                    //pager().totalCount(data.TotalCount);
                                },
                                error: function () {
                                    toastr.error("Failed to load Ad Campaigns!");
                                }
                            });
                    },
                  
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        pager(pagination.Pagination({ PageSize: 10 }, campaigns, getCampaigns));
                        var mode = getParameter(window.location.href, "mode");
                        if (mode == 2) {
                            lblPageTitle("Coupons For Approval");
                            isShowCopounMode(true);
                        }
                        //// First request for LV
                        getCampaigns();
                    };
                return {

                    initialize: initialize,
                    getCampaigns: getCampaigns,
                 
                };
            })()
        };
        return ist.Coupons.viewModel;
    });
