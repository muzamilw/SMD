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
                    payOutPreviousHistory = ko.observableArray([]),
                    //pager
                    pager = ko.observable(),
                    //sorting
                    sortOn = ko.observable(5),
                    //Assending  / Desending
                    sortIsAsc = ko.observable(true),
                    isEditorVisible = ko.observable(false),
                    selectedCompany = ko.observable(),
                    selectedPayPall = ko.observable(),
                    approval1 = ko.observable(false),
                    approval2 = ko.observable(false),
                    getPayOutHistory = function () {
                        if (UserRole == 'Franchise Admin')
                            approval1(true);
                        else if (UserRole == 'Franchise Account Manager')
                            approval2(true);
                        else
                        {
                            approval1(false);
                            approval2(false);
                        }

                           dataservice.getpayOutHistoryForApproval(
                                {
                                    PageSize: pager().pageSize(),
                                    PageNo: pager().currentPage(),
                                    SortBy: sortOn(),
                                    IsAsc: sortIsAsc(),
                                    UserRole: UserRole

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
                                    error: function (msg) {
                                        toastr.error("Failed to load data" + msg);
                                    }
                                });
                    },
                    onEditPayPallHistory = function (item) {

                            $("#topArea").css("display", "none");
                            $("#divApprove").css("display", "none");
                            selectedPayPall(item);
                            getCompanyData(item);
                            // isEditorVisible(true);
                    },
                    closeEditDialog = function () {
                          
                        selectedPayPall(undefined);
                           isEditorVisible(false);
                           $("#topArea").css("display", "block");
                           $("#divApprove").css("display", "block");
                    },
                    getCompanyData = function (selectedItem) {
                        
                             dataservice.getCompanyData(
                          {
                              companyId: selectedItem.companyId,
                              userId: selectedItem.userId,
                          },
                          {
                              success: function (comData) {
                                  selectedCompany(comData);
                                  getPayOutHistoryByCId(selectedItem);
                                  var cType = companyTypes().find(function (item) {
                                      return comData.CompanyType === item.Id;
                                  });
                                  if (cType != undefined)
                                      company(cType.Name);
                                  else
                                      company(null);
                                  isEditorVisible(true);

                              },
                              error: function () {
                                  toastr.error("Failed to load Company");
                              }
                          });

                    },
                    onRejectPayOut = function () {
                          confirmation.messageText("Do you want to reject this payout?  ");
                          confirmation.show();
                          confirmation.afterCancel(function () {
                              confirmation.hide();
                          });
                          confirmation.afterProceed(function () {
                              if (selectedPayPall().rejectionReasonStage1() == undefined || selectedPayPall().rejectionReasonStage1() == "" || selectedPayPall().rejectionReasonStage1() == " ") {
                                  toastr.info("Please add rejection reason!");
                                  return false;
                              }
                              //selectedPayPall().isApproved(false);
                              if (UserRole == 'Franchise Admin')
                                  selectedPayPall().stageOneStatus(2)
                              else
                                  selectedPayPall().stageTwoStatus(2)
                              onSavePayOut();
                              $("#topArea").css("display", "block");
                              $("#divApprove").css("display", "block");
                          });

                    },
                    onApprovePayOut = function () {
                          confirmation.messageText("Do you want to approve this payout?");
                          confirmation.show();
                          confirmation.afterCancel(function () {
                              confirmation.hide();
                          });
                          confirmation.afterProceed(function () {
                              if (selectedPayPall().rejectionReasonStage1() == undefined || selectedPayPall().rejectionReasonStage1() == "" || selectedPayPall().rejectionReasonStage1() == " ") {
                                  toastr.info("Please add approval reason!");
                                  return false;
                              }
                              //selectedPayPall().isApproved(false);
                              if (UserRole == 'Franchise Admin')
                                  selectedPayPall().stageOneStatus(1)
                              else
                                  selectedPayPall().stageTwoStatus(4)
                              onSavePayOut();
                              $("#topArea").css("display", "block");
                              $("#divApprove").css("display", "block");
                          });

                      },
                    onInvestigationPayOut = function () {
                             confirmation.messageText("Do you want to investigate this payout?  ");
                             confirmation.show();
                             confirmation.afterCancel(function () {
                                 confirmation.hide();
                             });
                             confirmation.afterProceed(function () {
                                 if (selectedPayPall().rejectionReasonStage2() == undefined || selectedPayPall().rejectionReasonStage2() == "" || selectedPayPall().rejectionReasonStage2() == " ") {
                                     toastr.info("Please add investigation reason!");
                                     return false;
                                 }
                                 //selectedPayPall().isApproved(false);
                                 if (UserRole == 'Franchise Account Manager') {
                                     selectedPayPall().stageTwoStatus(3)
                                     onSavePayOut();
                                 }
                                 $("#topArea").css("display", "block");
                                 $("#divApprove").css("display", "block");
                             });

                         },
                    onSavePayOut = function () {

                        var payOutId = selectedPayPall().payOutId();
                        dataservice.savePayOut(selectedPayPall().convertToServerData(), {
                                    success: function (response) {

                                        if (response.indexOf("Failed") == -1) {
                                            dataservice.sendApprovalRejectionEmail(selectedPayPall().convertToServerData(), {
                                                success: function (obj) {
                                                    getPayOutHistory();
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
                    company = ko.observable(),
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
                    getPayOutHistoryByCId = function (selectedItem) {

                        dataservice.getPayOutHistory(
                       {
                           companyID: selectedItem.companyId
                           
                       },
                       {
                           success: function (data) {
                               payOutPreviousHistory.removeAll();
                               _.each(data, function (item) {
                                   payOutPreviousHistory.push(item);
                               });
                               isEditorVisible(true);
                           },
                           error: function (error) {
                               toastr.error("Failed to load PayOutHistory");
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
                    getPayOutHistory: getPayOutHistory,
                    isEditorVisible: isEditorVisible,
                    selectedCompany: selectedCompany,
                    selectedPayPall: selectedPayPall,
                    onEditPayPallHistory: onEditPayPallHistory,
                    closeEditDialog: closeEditDialog,
                    onRejectPayOut: onRejectPayOut,
                    onSavePayOut: onSavePayOut,
                    onApprovePayOut: onApprovePayOut,
                    approval1: approval1,
                    approval2: approval2,
                    onInvestigationPayOut: onInvestigationPayOut,
                    payOutPreviousHistory: payOutPreviousHistory,
                    companyTypes: companyTypes,
                    company: company


                   

                };
            })()
        };
        return ist.PayPall.viewModel;
    });