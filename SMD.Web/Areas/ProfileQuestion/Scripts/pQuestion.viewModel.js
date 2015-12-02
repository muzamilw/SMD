/*
    Module with the view model for the Profile Questions
*/
define("pQuestion/pQuestion.viewModel",
    ["jquery", "amplify", "ko", "pQuestion/pQuestion.dataservice", "pQuestion/pQuestion.model"],
    function ($, amplify, ko, dataservice, model) {
        var ist = window.ist || {};
        ist.Contact = {
            viewModel: (function() {
                var view,
                    //  Array
                    questions = ko.observableArray([]),
                    //pager
                    pager = ko.observable(),
                    //sorting
                    sortOn = ko.observable(1),
                    // Search Filter value 
                    filterValue = ko.observable(),
                    //Assending  / Desending
                    sortIsAsc = ko.observable(true),
                    // Controlls editor visibility 
                    isEditorVisible = ko.observable(false),
                    // Editor View Model
                    editorViewModel = new ist.ViewModel(model.hireGroupImage),
                    //selected Question
                    selectedQuestion = editorViewModel.itemForEditing,

                    //Get Questions
                    getQuestions = function () {
                        dataservice.searchProfileQuestions(
                            {
                                ProfileQuestionFilterText: filterValue(),
                                PageSize: 10,
                                PageNo: 1,//pager().currentPage(),
                                SortBy: sortOn(),
                                IsAsc: sortIsAsc()
                            },
                            {
                                success: function (data) {
                                    questions.removeAll();
                                   //  pager().totalCount(data.TotalCount);
                                    _.each(data.ProfileQuestions, function (item) {
                                        questions.push(model.questionServertoClientMapper(item));
                                    });

                                },
                                error: function () {
                                    toastr.error("Failed to load profile questions!");
                                }
                            });
                    },
                    
                    // Search Filter 
                    filterProfileQuestion= function() {
                        getQuestions();
                    },
                    // Add new Profile Question
                    addNewProfileQuestion = function() {
                       isEditorVisible(true);
                    },
                    // Close Editor 
                    closeEditDialog= function() {
                        isEditorVisible(false);
                    },
                    // On editing of existing PQ
                    onEditProfileQuestion= function(item) {
                        selectedQuestion(item);
                        isEditorVisible(true);
                    },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                       // pager(pagination.Pagination({ PageSize: 10 }, questions, getQuestions));
                        getQuestions();
                    };
                return {
                    initialize: initialize,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    pager: pager,
                    selectedQuestion: selectedQuestion,
                    questions: questions,
                    getQuestions: getQuestions,
                    isEditorVisible: isEditorVisible,
                    filterProfileQuestion: filterProfileQuestion,
                    filterValue: filterValue,
                    addNewProfileQuestion: addNewProfileQuestion,
                    closeEditDialog: closeEditDialog,
                    onEditProfileQuestion: onEditProfileQuestion
                    
                };
            })()
        };
        return ist.Contact.viewModel;
    });
