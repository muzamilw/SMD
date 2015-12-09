/*
    Module with the view model for the Survey Questions
*/
define("surveyQuestionApp/surveyQuestionApp.viewModel",
    ["jquery", "amplify", "ko", "surveyQuestionApp/surveyQuestionApp.dataservice", "surveyQuestionApp/surveyQuestionApp.model", "common/pagination",
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
                                    pager().totalCount(0);
                                    pager().totalCount(data.TotalCount);
                                    _.each(data.SurveyQuestions, function (item) {
                                        questions.push(model.SurveyquestionServertoClientMapper(item));
                                    });

                                },
                                error: function () {
                                    toastr.error("Failed to load servey questions!");
                                }
                            });
                    },
                    // Close Editor 
                    closeEditDialog = function () {
                        // Ask for confirmation
                        confirmation.afterProceed(function () {
                            selectedQuestion(undefined);
                            isEditorVisible(false);
                        });
                        confirmation.show();
                    },
                    // On editing of existing PQ
                    onEditQuestion = function (item) {
                        selectedQuestion(item);
                        isEditorVisible(true);
                    },
                  
                    // Save Question / Add 
                    onSaveQuestion = function () {
                        dataservice.saveSurveyQuestion(selectedQuestion().convertToServerData(), {
                            success: function (obj) {
                                var newObjtodelete = questions.find(function (temp) {
                                    return obj.SqId == temp.id();
                                });
                                questions.remove(newObjtodelete);
                                toastr.success("You are Good!");
                                isEditorVisible(false);
                            },
                            error: function () {
                                toastr.error("Failed to delete!");
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
                    hasChangesOnQuestion: hasChangesOnQuestion
                };
            })()
        };
        return ist.SurveyQuestion.viewModel;
    });
