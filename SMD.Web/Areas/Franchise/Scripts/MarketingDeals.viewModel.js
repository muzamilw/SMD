/*
    Module with the view model for the AdCampaign
*/
define("FranchiseDashboard/MarketingDeals.viewModel",
    ["jquery", "amplify", "ko", "FranchiseDashboard/MarketingDeals.dataservice", "FranchiseDashboard/MarketingDeals.model", "common/pagination",
     "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation) {
        var ist = window.ist || {};
        ist.Deal = {
            viewModel: (function () {
                var view,
                    marketingDeals = ko.observableArray([]),
                    couponsPriceOption = ko.observableArray([]),
                    //pager
                    pager = ko.observable(),
                    //sorting
                    sortOn = ko.observable(5),
                    currencyCode = ko.observable(),
                    //Assending  / Desending
                    sortIsAsc = ko.observable(true),
                    isEditorVisible = ko.observable(false),
                    isShowCopounMode = ko.observable(false),
                    selectedCoupon = ko.observable(),
                    selectedCompany = ko.observable(),
                    getMarketingDeals = function () {
                        dataservice.getMarketingDeals(
                                {
                                    PageSize: pager().pageSize(),
                                    PageNo: pager().currentPage(),
                                    SortBy: sortOn(),
                                    IsAsc: sortIsAsc(),

                                },
                                {
                                    success: function (data) {
                                        marketingDeals.removeAll();
                                        console.log("Marketing Deals");
                                        console.log(data.Coupons);
                                        _.each(data.Coupons, function (item) {
                                            marketingDeals.push(model.MarketingDealServertoClientMapper(item));
                                        });
                                        //pager().totalCount(0);
                                        pager().totalCount(data.TotalCount);

                                    },
                                    error: function () {
                                        toastr.error("Failed to load Ad Campaigns!");
                                    }
                                });
                    },
                    onEditMarketingDeal = function (item) {
                        $("#topArea").css("display", "none");
                        $("#divApprove").css("display", "none");
                        selectedCoupon(item);
                        getCompanyData(item)


                    },
                    closeEditDialog = function () {
                        selectedCoupon(undefined);
                        isEditorVisible(false);
                        $("#topArea").css("display", "block");
                        $("#divApprove").css("display", "block");
                        $("#main_btnPannels").css("display", "none");
                        $("#MarketobjDiv").css("display", "none");
                        $(".hideInCoupons").css("display", "none");
                        $("#topArea").css("display", "none");
                        $("#divApprove").css("display", "none");
                    },
                    getCouponPriceOption = function (item,coData) {
                        dataservice.getCouponPriceOption(
                                             { CouponId: item.couponId },
                                             {
                                                 success: function (data) {
                                                     couponsPriceOption.removeAll();
                                                     _.each(data, function (item) {
                                                         couponsPriceOption.push(item);
                                                     });
                                                     getCurrencyData(coData);

                                                 },
                                                 error: function () {
                                                     toastr.error("Failed to load Coupon Price Options ");
                                                 }
                                             });

                    },
                    getCurrencyData = function (item) {
                        dataservice.getCurrenybyID(
                       { id: item.CurrencyID },
                       {
                           success: function (data) {
                               currencyCode(null);
                               var cCode = '(' + data.CurrencySymbol + ')';
                               currencyCode(cCode);
                               isEditorVisible(true);



                           },
                           error: function () {

                           }
                       });
                    },
                    getCompanyData = function (selectedItem) {
                        dataservice.getCompanyData(
                     {
                         companyId: selectedItem.companyId,
                         userId: selectedItem.userId,
                     },
                     {
                         success: function (comData) {
                             getCouponPriceOption(selectedItem, comData);
                         },
                         error: function () {
                             toastr.error("Failed to load Company");
                         }
                     });

                    },
                    SaveCoupon = function () {
                         var couponId = selectedCoupon().couponId();
                         dataservice.saveCoupon(selectedCoupon().convertToServerData(), {
                             success: function (response) {
                                 getMarketingDeals();
                                 isEditorVisible(false);
                                 toastr.success("Saved Successfully.");
                             },
                             error: function () {
                                 toastr.error("Failed to save!");
                             }
                         });
                        
                    },
                    onSaveCoupon = function () {
                         var conformTet = "Save changes?";
                         confirmation.messageText(conformTet);
                         confirmation.show();
                         confirmation.afterCancel(function () {
                             confirmation.hide();
                         });
                         confirmation.afterProceed(function () {
                             SaveCoupon();
                             //$("#topArea").css("display", "block");
                             //$("#divApprove").css("display", "block");
                             //toastr.success("Approved Successfully.");
                         });
                     },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        pager(pagination.Pagination({ PageSize: 10 }, marketingDeals, getMarketingDeals));
                        var mode = getParameter(window.location.href, "mode");
                        if (mode == 2) {
                            lblPageTitle("Coupons For Approval");
                            isShowCopounMode(true);
                        }
                        //// First request for LV
                        getMarketingDeals();
                    };
                return {

                    initialize: initialize,
                    getMarketingDeals: getMarketingDeals,
                    marketingDeals: marketingDeals,
                    pager: pager,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    isEditorVisible: isEditorVisible,
                    selectedCoupon: selectedCoupon,
                    currencyCode: currencyCode,
                    onEditMarketingDeal: onEditMarketingDeal,
                    closeEditDialog: closeEditDialog,
                    couponsPriceOption: couponsPriceOption,
                    onSaveCoupon:onSaveCoupon
                };
            })()
        };
        return ist.Deal.viewModel;
    });
