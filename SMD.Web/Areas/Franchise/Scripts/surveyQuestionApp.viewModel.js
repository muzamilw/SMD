/*
    Module with the view model for the Survey Questions
*/
define("FranchiseDashboard/surveyQuestionApp.viewModel",
    ["jquery", "amplify", "ko", "FranchiseDashboard/surveyQuestionApp.dataservice", "FranchiseDashboard/surveyQuestionApp.model", "common/pagination",
     "common/confirmation.viewModel", "common/stripeChargeCustomer.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation, stripeChargeCustomer) {
        var ist = window.ist || {};
        ist.SurveyQuestion = {
            viewModel: (function() {
                var view,
                    //  Questions list on LV
                    questions = ko.observableArray([]),
                    //pager
                    pager = ko.observable(),
                    //sorting
                    sortOn = ko.observable(1),
                    selectedCompany = ko.observable(),
                    company = ko.observable(),
                    //Assending  / Desending
                    sortIsAsc = ko.observable(true),
                    // Controlls editor visibility 
                    isEditorVisible = ko.observable(false),
                     //selected Answer
                    selectedQuestion = ko.observable(),
                    //Get Questions
                    getQuestions = function () {
                        dataservice.searchSurveyQuestions(
                            {
                                PageSize: pager().pageSize(),
                                PageNo: pager().currentPage(),
                                SortBy: sortOn(),
                                IsAsc: sortIsAsc()
                            },
                            {
                                success: function (data) {
                                    questions.removeAll();
                                    _.each(data.SurveyQuestions, function (item) {
                                        questions.push(model.SurveyquestionServertoClientMapper(item));
                                    });
                                    pager().totalCount(0);
                                    pager().totalCount(data.TotalCount);

                                },
                                error: function () {
                                    toastr.error("Failed to load servey questions!");
                                }
                            });
                    },
                    // Close Editor 
                    closeEditDialog = function () {
                        // Ask for confirmation
                        //confirmation.afterProceed(function () {
                           
                        //});
                        //confirmation.show();
                        selectedQuestion(undefined);
                        isEditorVisible(false);
                        $("#topArea").css("display", "block");
                        $("#divApprove").css("display", "block");
                    },
                    // On editing of existing PQ
                    onEditQuestion = function (item) {
                        $("#topArea").css("display", "none");
                        $("#divApprove").css("display", "none");
                        selectedQuestion(item);
                        if (item.companyId() != null)
                            getCompanyData(item);
                        else {
                            isEditorVisible(true);
                            selectedCompany(null);
                        }
                    },
                  
                    // Save Question / Add 
                    onSaveQuestion = function () {
                        dataservice.saveSurveyQuestion(selectedQuestion().convertToServerData(), {
                            success: function (obj) {
                                var newObjtodelete = questions.find(function (temp) {
                                    return obj.SqId == temp.id();
                                });
                                questions.remove(newObjtodelete);
                                toastr.success("Saved Successfully!");
                                isEditorVisible(false);
                                getApprovalCount();
                            },
                            error: function () {
                                toastr.error("Failed to update!");
                            }
                        });
                    },
                   
                    // Has Changes
                    hasChangesOnQuestion = ko.computed(function () {
                        if (selectedQuestion() == undefined) {
                            return false;
                        }
                        return (selectedQuestion().hasChanges());
                    }),
                    onRejectQuestion = function () {

                        confirmation.messageText("Do you want to Reject this Survay Cards ?");
                        confirmation.show();
                        confirmation.afterCancel(function () {
                            confirmation.hide();
                        });
                        confirmation.afterProceed(function () {
                            if (selectedQuestion().rejectionReason() == undefined || selectedQuestion().rejectionReason() == "" || selectedQuestion().rejectionReason() == " ") {
                                toastr.info("Please add rejection reason!");
                                return false;
                            }
                            onSaveQuestion();
                            $("#topArea").css("display", "block");
                            $("#divApprove").css("display", "block");
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
                                company(cType.Name);
                                isEditorVisible(true);

                            },
                            error: function () {
                                toastr.error("Failed to load Company");
                            }
                        });

                       },
                    onApproveQuestion = function () {

                         confirmation.messageText("Do you want to approve this Survay Cards ? System will attempt to collect payment and generate invoice");
                         confirmation.show();
                         confirmation.afterCancel(function () {
                             confirmation.hide();
                         });
                         confirmation.afterProceed(function () {
                             selectedQuestion().isApproved(true);
                             onSaveQuestion();
                             $("#topArea").css("display", "block");
                             $("#divApprove").css("display", "block");
                             toastr.success("Approved Successfully.");
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
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        pager(pagination.Pagination({ PageSize: 10 }, questions, getQuestions));
                       
                        // First request for LV
                        getQuestions();
                    };
                return {
                    initialize: initialize,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    pager: pager,
                    questions: questions,
                    getQuestions: getQuestions,
                    isEditorVisible: isEditorVisible,
                    closeEditDialog: closeEditDialog,
                    onEditQuestion: onEditQuestion,
                    selectedQuestion: selectedQuestion,
                    onSaveQuestion: onSaveQuestion,
                    hasChangesOnQuestion: hasChangesOnQuestion,
                    onRejectQuestion: onRejectQuestion,
                    onApproveQuestion: onApproveQuestion,
                    selectedCompany: selectedCompany,
                    getApprovalCount: getApprovalCount,
                    company: company,
                    companyTypes: companyTypes,
                };
            })()
        };
        return ist.SurveyQuestion.viewModel;
    });
