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
                    Falgval = ko.observable(false),
                    isEditorVisible = ko.observable(false),
                    selectedCompany = ko.observable(),
                    company = ko.observable(),
                   // isShowCopounMode = ko.observable(false),
                    selectedProfileQuestion = ko.observable(),
                    onEditPQ = function (item) {
                        $("#topArea").css("display", "none");
                        $("#divApprove").css("display", "none");
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
                        $("#topArea").css("display", "block");
                        $("#divApprove").css("display", "block");
                    },
                    getProfileQuestions = function () {
                        Falgval(true);
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
                                    getApprovalCount();
                                },
                                error: function () {
                                    toastr.error("Failed to load Ad Campaigns!");
                                }
                            });
                    },
                    onApprovePq = function () {
                        confirmation.messageText("Do you want to approve this survay question?  "+"<br\>" + "System will attempt to collect payment and generate invoice.");
                        confirmation.show();
                        confirmation.afterCancel(function () {
                            confirmation.hide();
                        });
                        confirmation.afterProceed(function () {
                            selectedProfileQuestion().isApproved(true);
                            onSavePQ();
                            $("#topArea").css("display", "block");
                            $("#divApprove").css("display", "block");
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
                    getProfileGroup = function (item) {
                        dataservice.getProfileGroupbyID(
                       { id: item.profileGroupId },
                       {
                           success: function (data) {

                               selectedProfileQuestion().profileGroupId(data.ProfileGroupName)
                               if (item.companyId() != null)
                                   getCompanyData(item);
                               else
                               { selectedCompany(null) }
                           },
                           error: function () {
                               selectedProfileQuestion().profileGroupId(item.profileGroupId());
                               getCompanyData(item);
                               //isEditorVisible(true);
                               //toastr.error("Failed to load PG");
                           }
                       });
                    },
                    getApprovalCount = function () {
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
                    getCompanyData = function (selectedItem) {
                        dataservice.getCompanyData(
                     {
                         companyId: selectedItem.companyId,
                         userId: selectedItem.userID,
                     },
                     {
                         success: function (comData) {
                             selectedCompany(comData);
                             var cType = companyTypes().find(function (item) {
                                 return comData.CompanyType === item.Id;
                             });
                             if (cType != undefined)
                                 company(cType.Name);
                             else
                                 company(null);

                         },
                         error: function () {
                             toastr.error("Failed to load Company");
                         }
                     });

                    },
                    hasChangesOnPQ = ko.computed(function () {
                        if (selectedProfileQuestion() == undefined) {
                            return false;
                        }
                        return (selectedProfileQuestion().hasChanges());
                    }),
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
                    onRejectPQ = function () {
                        confirmation.messageText("Do you want to reject this survay question?  ");
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
                            $("#topArea").css("display", "block");
                            $("#divApprove").css("display", "block");
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
                    selectedCompany: selectedCompany,
                    getApprovalCount: getApprovalCount,
                    companyTypes: companyTypes,
                    company: company,
                    Falgval: Falgval
                };
            })()
        };
        return ist.ProfileQuestion.viewModel;
    });
