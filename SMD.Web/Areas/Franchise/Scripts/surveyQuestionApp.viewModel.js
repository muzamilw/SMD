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

                        confirmation.messageText("Do you want to reject this survay card?");
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
                    onApproveQuestion = function () {

                        confirmation.messageText("Do you want to approve this poll?"+ "<br\>" +"System will attempt to collect payment and generate invoice.");
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
                     rejectionReasondd = ko.observableArray([
                         {
                             "RejectionId": "1",
                             "Reason": " Content is of unlawful, harmful, tortious, threatening, abusive, harassing, hateful, racist or homophobic."

                         },
                           {
                               "RejectionId": "2",
                               "Reason": "Content is of infringing, pornographic, violent, misleading, grossly offensive, of an indecent, obscene or menacing character, blasphemous or defamatory."

                           },
                           {
                               "RejectionId": "3",
                               "Reason": "Content is of of a libellous nature of any person or entity, in contempt of court or in breach of confidence, or which infringes the rights of another person or entity, including copyrights, trademarks, trade secrets, patents, rights of personality, publicity or privacy or any other third party rights."

                           },
                            {
                                "RejectionId": "4",
                                "Reason": "We suspect that material, including User Content, you may have not obtained all necessary licenses and/or approvals (from us or third parties); or which constitutes or encourages conduct that would be considered a criminal offence, give rise to civil liability, or otherwise be contrary to the law of or infringe the rights of any third party in any country in the world."

                            },
                             {
                                 "RejectionId": "5",
                                 "Reason": "We suspect material which may technically harmful (including computer viruses, logic bombs, Trojan horses, worms, harmful components, corrupted data, malicious software, harmful data, or anything else which may interrupt, interfere with, corrupt or otherwise cause loss, damage, destruction or limitation to the functionality of any software or computer equipment)."

                             },
                             {
                                 "RejectionId": "6",
                                 "Reason": "Content may  to cause annoyance, inconvenience or needless anxiety to others."
                             },
                             {
                                 "RejectionId": "7",
                                 "Reason": "We suspect that material, including User Content, may intercept or attempt to intercept any communications transmitted by way of a telecommunications system."
                             },
                             {
                                 "RejectionId": "8",
                                 "Reason": "We suspect that material, including User Content,  may not be for a purpose other than which we have designed them or intended them to be used."
                             },
                              {
                                  "RejectionId": "9",
                                  "Reason": "We suspect that material, including User Content, may be for fraudulent purposes."
                              },
                              {
                                  "RejectionId": "10",
                                  "Reason": "We suspect that material, including User Content, may be used in any way which could be calculated to incite hatred against any ethnic, religious or any other minority or is otherwise could be calculated to adversely affect any individual, group or entity."
                              },
                              {
                                  "RejectionId": "11",
                                  "Reason": "Poor Imag quality, use higher quality images i.e. 300 dpi and of size 300 x 300 as a minimum."
                              },
                              {
                                  "RejectionId": "12",
                                  "Reason": "Too much text on image(s), use clear and inspiring images	Not enough text in Descriptions or Titles."
                              },
                              {
                                  "RejectionId": "13",
                                  "Reason": "URL LINK does not click thru to a vaild website page."
                              },
                               {
                                   "RejectionId": "14",
                                   "Reason": "Video Ad does not player on our app players, change format to MP4 and less than 30 seconds."
                               },
                               {
                                   "RejectionId": "15",
                                   "Reason": "Video Ad length is to long, reduce to less than 30 seconds."
                               },
                               {
                                   "RejectionId": "16",
                                   "Reason": "Incomplete information about campaign promotion."
                               },
                                {
                                    "RejectionId": "17",
                                    "Reason": "Your Company Logo or Details are incomplete."
                                },
                                 {
                                     "RejectionId": "18",
                                     "Reason": "Your Discount Pricing seems unrealitic for your promotion."
                                 },
                                 {
                                     "RejectionId": "19",
                                     "Reason": "Your Branch Address Details must be in the UK and have correct Geo X,Y co ordinates."
                                 },
                                 {
                                     "RejectionId": "20",
                                     "Reason": "Your telephone number does not work."
                                 },
                                 {
                                     "RejectionId": "21",
                                     "Reason": "Your web site is not valid or live."
                                 },
                                  {
                                      "RejectionId": "22",
                                      "Reason": "Your Deal Group Heading is not clear and concise."
                                  },
                                  {
                                      "RejectionId": "23",
                                      "Reason": "Your Deal Lines are not Clear or are ambiguous."
                                  },
                                  {
                                      "RejectionId": "24",
                                      "Reason": "Your Video Ad title is not Clear or is ambiguous."
                                  },
                                  {
                                      "RejectionId": "25",
                                      "Reason": "Your Content is not in line with our ethical or moral publishing policies."
                                  },
                                  {
                                      "RejectionId": "26",
                                      "Reason": "Your Content is not Clear or is ambiguous."
                                  },
                     ]),
                     onChangeRejectionReason = function ()
                     {

                         var disper = $("#rejectReasondd").val();
                         selectedQuestion().rejectionReason(disper);
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
                    rejectionReasondd: rejectionReasondd,
                    onChangeRejectionReason: onChangeRejectionReason
                };
            })()
        };
        return ist.SurveyQuestion.viewModel;
    });
