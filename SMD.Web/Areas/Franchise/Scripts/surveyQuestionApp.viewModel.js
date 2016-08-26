﻿/*
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
                                toastr.success("Saved Successfully!");
                                isEditorVisible(false);
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
                    onRejectQuestion= function() {
                        if (selectedQuestion().rejectionReason() == undefined || selectedQuestion().rejectionReason() == "" || selectedQuestion().rejectionReason() == " ") {
                            toastr.info("Please add rejection reason!");
                            return false;
                        }
                        onSaveQuestion();
                    },
                     onApproveQuestion = function () {
                         selectedQuestion().isApproved(true);
                         onSaveQuestion();
                     },
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
                    onApproveQuestion: onApproveQuestion
                };
            })()
        };
        return ist.SurveyQuestion.viewModel;
    });