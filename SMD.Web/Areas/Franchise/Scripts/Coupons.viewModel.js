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
                    coupons = ko.observableArray([]),
                    //pager
                    pager = ko.observable(),
                    //sorting
                    sortOn = ko.observable(5),
                    //Assending  / Desending
                    sortIsAsc = ko.observable(true),
                    isEditorVisible = ko.observable(false),
                    isShowCopounMode = ko.observable(false),
                    selectedCoupon = ko.observable(),
                    onEditCoupon = function (item) {
                        selectedCoupon(item);
                        isEditorVisible(true);
                    },
                    closeEditDialog = function () {
                        selectedCoupon(undefined);
                        isEditorVisible(false);
                    },
                    getCoupons = function () {
                        dataservice.getCouponsForApproval(
                            {
                                PageSize: pager().pageSize(),
                                PageNo: pager().currentPage(),
                                SortBy: sortOn(),
                                IsAsc: sortIsAsc(),

                            },
                            {
                                success: function (data) {
                                    coupons.removeAll();
                                    console.log("approval Coupons");
                                    console.log(data.Coupons);
                                    _.each(data.Coupons, function (item) {
                                        coupons.push(model.CouponsServertoClientMapper(item));
                                    });
                                    //pager().totalCount(0);
                                    pager().totalCount(data.TotalCount);
                                },
                                error: function () {
                                    toastr.error("Failed to load Ad Campaigns!");
                                }
                            });
                    },
                      onSaveCoupon = function () {

                          var couponId = selectedCoupon().couponId();
                          dataservice.saveAdCampaign(selectedCoupon().convertToServerData(), {
                              success: function (response) {

                                  //if (response.indexOf("Failed") == -1) {
                                  //    dataservice.sendApprovalRejectionEmail(selectedCoupon().convertToServerData(), {
                                  //        success: function (obj) {
                                  //            getCampaigns();
                                  //            //var existingCampaigntodelete = $.grep(campaigns(), function (n, i) {
                                  //            //    return (n.id() == campId);
                                  //            //});

                                  //            //campaigns.remove(existingCampaigntodelete);

                                  //            isEditorVisible(false);
                                  //        },
                                  //        error: function () {
                                  //            toastr.error("Failed to save!");
                                  //        }
                                  //    });
                                  //} else {
                                  //    toastr.error(response);
                                  //}
                              },
                              error: function () {
                                  toastr.error("Failed to save!");
                              }
                          });
                      },

                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        pager(pagination.Pagination({ PageSize: 10 }, coupons, getCoupons));
                        var mode = getParameter(window.location.href, "mode");
                        if (mode == 2) {
                            lblPageTitle("Coupons For Approval");
                            isShowCopounMode(true);
                        }
                        //// First request for LV
                        getCoupons();
                    };
                return {

                    initialize: initialize,
                    getCoupons: getCoupons,
                    coupons: coupons,
                    pager: pager,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    isEditorVisible: isEditorVisible,
                    onEditCoupon: onEditCoupon,
                    selectedCoupon: selectedCoupon,
                    closeEditDialog: closeEditDialog,
                    onSaveCoupon: onSaveCoupon,

                };
            })()
        };
        return ist.Coupons.viewModel;
    });
