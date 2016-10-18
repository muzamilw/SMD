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
                    isEditorVisible = ko.observable(false),
                    selectedCompany = ko.observable(),
                    selectedPayPall = ko.observable(),
                    approval1 = ko.observable(false),
                    approval2 = ko.observable(false),
                    getPayOutHistory = function () {
                        if (UserRole == 'Franchise_Approver1')
                            approval1(true);
                        else
                            approval2(true);

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

                            //$("#topArea").css("display", "none");
                            //$("#divApprove").css("display", "none");
                            selectedPayPall(item);
                            getCompanyData(item);
                            // isEditorVisible(true);
                    },
                    closeEditDialog = function () {
                          
                        selectedPayPall(undefined);
                           isEditorVisible(false);
                           //$("#topArea").css("display", "block");
                           //$("#divApprove").css("display", "block");
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
                                  //var cType = companyTypes().find(function (item) {
                                  //    return comData.CompanyType === item.Id;
                                  //});
                                  //if (cType != undefined)
                                  //    company(cType.Name);
                                  //else
                                  //    company(null);
                                  isEditorVisible(true);

                              },
                              error: function () {
                                  toastr.error("Failed to load Company");
                              }
                          });

                    },
                    onRejectPayOut = function () {
                          confirmation.messageText("Do you want to Reject this PayOut ?");
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
                              if (UserRole == 'Franchise_Approver1')
                                  selectedPayPall().stageOneStatus(2)
                              else
                                  selectedPayPall().stageTwoStatus(2)
                              onSavePayOut();
                              //$("#topArea").css("display", "block");
                              //$("#divApprove").css("display", "block");
                          });

                    },
                    onApprovePayOut = function () {
                          confirmation.messageText("Do you want to Approve this PayOut ?");
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
                              if (UserRole == 'Franchise_Approver1')
                                  selectedPayPall().stageOneStatus(1)
                              else
                                  selectedPayPall().stageTwoStatus(4)
                              onSavePayOut();
                              //$("#topArea").css("display", "block");
                              //$("#divApprove").css("display", "block");
                          });

                      },
                    onInvestigationPayOut = function () {
                             confirmation.messageText("Do you want to Investigate this PayOut ?");
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
                                 if (UserRole == 'Franchise_Approver2') {
                                     selectedPayPall().stageTwoStatus(3)
                                     onSavePayOut();
                                 }
                                 //$("#topArea").css("display", "block");
                                 //$("#divApprove").css("display", "block");
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
                    getPayOutHistoryByCId = function (selectedItem) {

                        dataservice.getPayOutHistory(
                       {
                           companyID: selectedItem.companyId
                           
                       },
                       {
                           success: function (data) {
                               var a = data;
                          

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


                   

                };
            })()
        };
        return ist.PayPall.viewModel;
    });