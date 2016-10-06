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
                    selectedCompany = ko.observable(),
                    onEditCoupon = function (item) {
                        $("#topArea").css("display", "none");
                        $("#divApprove").css("display", "none");
                        dataservice.getCurrenybyID(
                        { id: item.currencyId },
                        {
                            success: function (data) {
                                selectedCoupon(item);
                                selectedCoupon().currencyId(data.CurrencyCode)
                                getCompanyData(item);
                               // isEditorVisible(true);
                                //getCouponCategories(item.couponId);

                               

                            },
                            error: function () {
                                selectedCoupon(item);
                                getCompanyData(item);
                                //toastr.error("Failed to load Currency");
                            }
                        });

                        //selectedCoupon(item);
                        //isEditorVisible(true);
                    },
                    getCouponCategories = function (couponid)
                    {
                        dataservice.getCouponCategories(
                                             { CampaignId: couponid },
                                             {
                                                 success: function (data) {
                                                     var a = data;


                                                 },
                                                 error: function () {
                                                     toastr.error("Failed to load CouponCategory");
                                                 }
                                             });

                    },
                    closeEditDialog = function () {
                        selectedCoupon(undefined);
                        isEditorVisible(false);
                        $("#topArea").css("display", "block");
                        $("#divApprove").css("display", "block");
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
                    onApproveCoupon = function () {
                             confirmation.messageText("Do you want to approve this Coupon ? System will attempt to collect payment and generate invoice");
                             confirmation.show();
                             confirmation.afterCancel(function () {
                                 confirmation.hide();
                             });
                             confirmation.afterProceed(function () {
                                 selectedCoupon().isApproved(true);
                                 onSaveCoupon();
                                 $("#topArea").css("display", "block");
                                 $("#divApprove").css("display", "block");
                                 toastr.success("Approved Successfully.");
                             });
                         },
                    onSaveCoupon = function () {

                          var couponId = selectedCoupon().couponId();
                          dataservice.saveCoupon(selectedCoupon().convertToServerData(), {
                              success: function (response) {

                                  if (response.indexOf("Failed") == -1) {
                                      dataservice.sendApprovalRejectionEmail(selectedCoupon().convertToServerData(), {
                                          success: function (obj) {
                                              getCoupons();
                                              //var existingCampaigntodelete = $.grep(campaigns(), function (n, i) {
                                              //    return (n.id() == campId);
                                              //});

                                              //campaigns.remove(existingCampaigntodelete);

                                              isEditorVisible(false);
                                          },
                                          error: function () {
                                              toastr.error("Failed to save!");
                                          }
                                      });
                                  }
                                  else {

                                      toastr.error(response);
                                  }
                              },
                              error: function () {
                                  toastr.error("Failed to save!");
                              }
                          });
                      },
                    hasChangesOnCoupon = ko.computed(function () {
                           if (selectedCoupon() == undefined) {
                                return false;
                            }
                           return (selectedCoupon().hasChanges());
                        }),
                    onRejectCoupon = function () {
                          if (selectedCoupon().rejectedReason() == undefined || selectedCoupon().rejectedReason() == "" || selectedCoupon().rejectedReason() == " ") {
                                 toastr.info("Please add rejection reason!");
                                 return false;
                             }
                             selectedCoupon().isApproved(false);
                             onSaveCoupon();
                             $("#topArea").css("display", "block");
                             $("#divApprove").css("display", "block");
                             toastr.success("Rejected Successfully.");
                    },
                    getCompanyData = function (selectedItem)
                    {
                        dataservice.getCompanyData(
                     {
                         companyId: selectedItem.companyId,
                         userId: selectedItem.userID,
                     },
                     {
                         success: function (comData) {
                             selectedCompany(comData);
                             isEditorVisible(true);
                         
                         },
                         error: function () {
                             toastr.error("Failed to load Company");
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
                    onApproveCoupon:onApproveCoupon,
                    onSaveCoupon: onSaveCoupon,
                    selectedCompany:selectedCompany,
                    onRejectCoupon: onRejectCoupon,
                    hasChangesOnCoupon: hasChangesOnCoupon,

                };
            })()
        };
        return ist.Coupons.viewModel;
    });
