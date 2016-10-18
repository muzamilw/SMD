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
                    couponsPriceOption = ko.observableArray([]),
                    //pager
                    pager = ko.observable(),
                    //sorting
                    sortOn = ko.observable(5),
                    company = ko.observable(),
                    currencyCode = ko.observable(),
                    //Assending  / Desending
                    sortIsAsc = ko.observable(true),
                    isEditorVisible = ko.observable(false),
                    isShowCopounMode = ko.observable(false),
                    selectedCoupon = ko.observable(),
                    selectedCompany = ko.observable(),
                    onEditCoupon = function (item) {
                        $("#topArea").css("display", "none");
                        $("#divApprove").css("display", "none");
                        selectedCoupon(item);
                        getCompanyData(item);
                       

                        //selectedCoupon(item);
                        //isEditorVisible(true);
                    },
                    getCouponPriceOption = function (couponid)
                    {
                        dataservice.getCouponPriceOption(
                                             { CouponId: couponid },
                                             {
                                                 success: function (data) {
                                                     couponsPriceOption.removeAll();
                                                     _.each(data, function (item) {
                                                         couponsPriceOption.push(item);
                                                     });
                                                    
 
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
                                    getApprovalCount();
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
                    getApprovalCount = function ()
                    {
                        dataservice.getapprovalCount({
                            success: function (data) {
                                $('#couponCount').text(data.CouponCount);
                                $('#vidioAdCount').text(data.AdCmpaignCount);
                                $('#displayAdCount').text(data.DisplayAdCount);
                                $('#surveyCount').text(data.SurveyQuestionCount);
                                $('#profileCount').text(data.ProfileQuestionCount);

                            },
                            error: function () {
                                toastr.error("Failed to load Approval Count.");
                            }
                        });
                    },
                    getCurrencyData = function (item)
                    {
                        dataservice.getCurrenybyID(
                       { id: item.CurrencyID },
                       {
                           success: function (data) {
                               currencyCode(null);
                               var cCode = '(' + data.CurrencySymbol + ')';
                               currencyCode(cCode);



                           },
                           error: function () {
                            
                           }
                       });
                    },
                    companyTypes = ko.observableArray([
                    { Id: 1, Name: 'Amusement, Gambling, and Recreation Industries' },
                    { Id: 2, Name: 'Arts, Entertainment, and Recreation' },
                    { Id: 3, Name: 'Broadcasting (except Internet)' },
                    { Id: 4, Name: 'Building Material and Garden Equipment and Supplies Dealers' },
                    { Id: 5, Name: 'Clothing and Clothing Accessories Stores' },
                    { Id: 6, Name: 'Computer and Electronics' },
                    { Id: 7, Name: 'Construction' },
                    { Id: 8, Name: 'Couriers and Messengers' },
                    { Id: 9, Name: 'Data Processing, Hosting, and Related Services' },
                    { Id: 10, Name: 'Health Services' },
                    { Id: 11, Name: 'Educational Services' },
                    { Id: 12, Name: 'Electronics and Appliance Stores' },
                    { Id: 13, Name: 'Finance and Insurance' },
                    { Id: 14, Name: 'Food Services' },
                    { Id: 15, Name: 'Food and Beverage Stores' },
                    { Id: 16, Name: 'Furniture and Home Furnishings Stores' },
                    { Id: 17, Name: 'General Merchandise Stores' },
                    { Id: 18, Name: 'Health Care' },
                    { Id: 19, Name: 'Internet Publishing and Broadcasting' },
                    { Id: 20, Name: 'Leisure and Hospitality' },
                    { Id: 21, Name: 'Manufacturing' },
                    { Id: 22, Name: 'Merchant Wholesalers ' },
                    { Id: 23, Name: 'Motor Vehicle and Parts Dealers ' },
                    { Id: 24, Name: 'Museums, Historical Sites, and Similar Institutions ' },
                    { Id: 25, Name: 'Performing Arts, Spectator Sports' },
                    { Id: 26, Name: 'Printing Services' },
                    { Id: 27, Name: 'Professional and Business Services' },
                    { Id: 28, Name: 'Real Estate' },
                    { Id: 29, Name: 'Repair and Maintenance' },
                    { Id: 30, Name: 'Scenic and Sightseeing Transportation' },
                    { Id: 31, Name: 'Service-Providing Industries' },
                    { Id: 32, Name: 'Social Assistance' },
                    { Id: 33, Name: 'Sporting Goods, Hobby, Book, and Music Stores' },
                    { Id: 34, Name: 'Telecommunications' },
                    { Id: 35, Name: 'Transportation' },
                    { Id: 36, Name: 'Utilities' }
                           ]),
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
                             var cType = companyTypes().find(function (item) {
                                 return comData.CompanyType === item.Id;
                             });
                             if (cType != undefined)
                                 company(cType.Name);
                             else
                                 company(null);
                             selectedCompany(comData);
                             getCouponPriceOption(selectedItem.couponId);
                             getCurrencyData(comData);
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
                    couponsPriceOption: couponsPriceOption,
                    getApprovalCount: getApprovalCount,
                    companyTypes: companyTypes,
                    company: company,
                    currencyCode: currencyCode,

                };
            })()
        };
        return ist.Coupons.viewModel;
    });
