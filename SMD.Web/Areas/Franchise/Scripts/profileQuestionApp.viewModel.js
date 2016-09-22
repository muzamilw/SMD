/*
    Module with the view model for the AdCampaign
*/
define("FranchiseDashboard/profileQuestionApp.viewModel",
    ["jquery", "amplify", "ko", "FranchiseDashboard/profileQuetionApp.dataservice", "FranchiseDashboard/profileQuestionApp.model", "common/pagination",
     "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation) {
        var ist = window.ist || {};
        ist.ProfileQuestion = {
            viewModel: (function () {
                var view,
                    profileQuestion = ko.observableArray([]),
                    //pager
                    pager = ko.observable(),
                    //sorting
                    sortOn = ko.observable(5),
                    //Assending  / Desending
                    sortIsAsc = ko.observable(true),
                    isEditorVisible = ko.observable(false),
                   // isShowCopounMode = ko.observable(false),
                    selectedProfileQuestion = ko.observable(),
                    onEditPQ = function (item) {
                        dataservice.getPqAnswer(
                        {
                            ProfileQuestionId: item.id
                        },
                            {
                                success: function (answers) {
                                    selectedProfileQuestion().pqAnswers.removeAll();
                                    _.each(answers, function (item) {
                                        selectedProfileQuestion().pqAnswers.push(item);
                                    });
                                    getProfileGroup(item);
                                   // var pqAnswers = answers;
                                },
                                error: function () {
                                    toastr.error("Failed to load profile question!");
                                }
                            });
                        selectedProfileQuestion(item);
                        isEditorVisible(true);
                    },
                    closeEditDialog = function () {
                        selectedProfileQuestion(undefined);
                        isEditorVisible(false);
                    },
                    getProfileQuestions = function () {
                        dataservice.getPQForApproval(
                            {
                                PageSize: pager().pageSize(),
                                PageNo: pager().currentPage(),
                                SortBy: sortOn(),
                                IsAsc: sortIsAsc(),

                            },
                            {
                                success: function (data) {
                                    profileQuestion.removeAll();
                                    console.log("approval Profile Question");
                                    console.log(data.ProfileQuestion);
                                    _.each(data.ProfileQuestion, function (item) {
                                        profileQuestion.push(model.ProfileQuestionServertoClientMapper(item));
                                    });
                                    ////pager().totalCount(0);
                                    pager().totalCount(data.TotalCount);
                                },
                                error: function () {
                                    toastr.error("Failed to load Ad Campaigns!");
                                }
                            });
                    },
                    onApprovePq = function () {
                       confirmation.messageText("Do you want to approve this Survay question ? System will attempt to collect payment and generate invoice");
                       confirmation.show();
                       confirmation.afterCancel(function () {
                           confirmation.hide();
                       });
                       confirmation.afterProceed(function () {
                       selectedProfileQuestion().isApproved(true);
                       onSavePQ();
                           toastr.success("Approved Successfully.");
                       });
                   },
                    onSavePQ = function () {

                        var pQId = selectedProfileQuestion().id();
                        dataservice.savePq(selectedProfileQuestion().convertToServerData(), {
                              success: function (response) {

                                  if (response.indexOf("Failed") == -1) {
                                      dataservice.sendApprovalRejectionEmail(selectedProfileQuestion().convertToServerData(), {
                                          success: function (obj) {
                                              getProfileQuestions();
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
                    getProfileGroup = function (item)
                    {
                        dataservice.getProfileGroupbyID(
                       { id: item.profileGroupId },
                       {
                           success: function (data) {
                               
                               selectedProfileQuestion().profileGroupId(data.ProfileGroupName)
                           },
                           error: function () {
                               selectedProfileQuestion().profileGroupId(item.profileGroupId());
                               //isEditorVisible(true);
                               //toastr.error("Failed to load PG");
                           }
                       });
                    },
                    hasChangesOnPQ = ko.computed(function () {
                        if (selectedProfileQuestion() == undefined) {
                               return false;
                           }
                        return (selectedProfileQuestion().hasChanges());
                       }),
                    onRejectPQ = function () {
                        confirmation.messageText("Do you want to Reject this Survay question ?");
                        confirmation.show();
                        confirmation.afterCancel(function () {
                            confirmation.hide();
                        });
                        confirmation.afterProceed(function () {
                            if (selectedProfileQuestion().rejectedReason() == undefined || selectedProfileQuestion().rejectedReason() == "" || selectedProfileQuestion().rejectedReason() == " ") {
                                toastr.info("Please add rejection reason!");
                                return false;
                            }
                            selectedProfileQuestion().isApproved(false);
                            onSavePQ();
                            toastr.success("Rejected Successfully.");
                        });
                      },

                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        pager(pagination.Pagination({ PageSize: 10 }, profileQuestion, getProfileQuestions));
                        var mode = getParameter(window.location.href, "mode");
                        //if (mode == 2) {
                        //    lblPageTitle("Coupons For Approval");
                        //    isShowCopounMode(true);
                        //}
                        //// First request for LV
                        getProfileQuestions();
                    };
                return {

                    initialize: initialize,
                    getProfileQuestions: getProfileQuestions,
                    profileQuestion: profileQuestion,
                    pager: pager,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    isEditorVisible: isEditorVisible,
                    onEditPQ: onEditPQ,
                    selectedProfileQuestion: selectedProfileQuestion,
                    closeEditDialog: closeEditDialog,
                    onApprovePq: onApprovePq,
                    onSavePQ: onSavePQ,
                    onRejectPQ: onRejectPQ,
                    hasChangesOnPQ: hasChangesOnPQ,

                };
            })()
        };
        return ist.ProfileQuestion.viewModel;
    });
