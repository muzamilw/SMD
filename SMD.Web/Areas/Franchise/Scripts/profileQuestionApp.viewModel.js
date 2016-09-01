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
                   // onApproveCoupon = function () {
                   //    confirmation.messageText("Do you want to approve this Coupon ? System will attempt to collect payment and generate invoice");
                   //    confirmation.show();
                   //    confirmation.afterCancel(function () {
                   //        confirmation.hide();
                   //    });
                   //    confirmation.afterProceed(function () {
                   //        selectedCoupon().isApproved(true);
                   //        onSaveCoupon();
                   //        toastr.success("Approved Successfully.");
                   //    });
                   //},
                   // onSaveCoupon = function () {

                   //       var couponId = selectedCoupon().couponId();
                   //       dataservice.saveCoupon(selectedCoupon().convertToServerData(), {
                   //           success: function (response) {

                   //               if (response.indexOf("Failed") == -1) {
                   //                   dataservice.sendApprovalRejectionEmail(selectedCoupon().convertToServerData(), {
                   //                       success: function (obj) {
                   //                           getCoupons();
                   //                           //var existingCampaigntodelete = $.grep(campaigns(), function (n, i) {
                   //                           //    return (n.id() == campId);
                   //                           //});

                   //                           //campaigns.remove(existingCampaigntodelete);

                   //                           isEditorVisible(false);
                   //                       },
                   //                       error: function () {
                   //                           toastr.error("Failed to save!");
                   //                       }
                   //                   });
                   //               }
                   //               else {

                   //                   toastr.error(response);
                   //               }
                   //           },
                   //           error: function () {
                   //               toastr.error("Failed to save!");
                   //           }
                   //       });
                   //   },
                   // hasChangesOnCoupon = ko.computed(function () {
                   //        if (selectedCoupon() == undefined) {
                   //            return false;
                   //        }
                   //        return (selectedCoupon().hasChanges());
                   //    }),
                   // onRejectCoupon = function () {
                   //       if (selectedCoupon().rejectedReason() == undefined || selectedCoupon().rejectedReason() == "" || selectedCoupon().rejectedReason() == " ") {
                   //           toastr.info("Please add rejection reason!");
                   //           return false;
                   //       }
                   //       selectedCoupon().isApproved(false);
                   //       onSaveCoupon();
                   //       toastr.success("Rejected Successfully.");
                   //   },

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
                    //onApproveCoupon: onApproveCoupon,
                    //onSaveCoupon: onSaveCoupon,
                    //onRejectCoupon: onRejectCoupon,
                    //hasChangesOnCoupon: hasChangesOnCoupon,

                };
            })()
        };
        return ist.ProfileQuestion.viewModel;
    });
