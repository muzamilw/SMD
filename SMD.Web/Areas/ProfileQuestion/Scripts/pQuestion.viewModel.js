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
                                PageSize: 10,
                                PageNo: 1,//pager().currentPage(),
                                SortBy: sortOn(),
                                IsAsc: sortIsAsc()
                            },
                            {
                                success: function (data) {
                                    questions.removeAll();
                                    toastr.success("I am back !");
                                    // pager().totalCount(data.TotalCount);
                                    //_.each(data.ParentHireGroupDesc, function (item) {
                                    //    parentHireGroups.push(model.parentHireGroupServertoClientMapper(item));
                                    //});

                                },
                                error: function () {
                                    toastr.error(ist.resourceText.DepartmentLoadFailError);
                                }
                            });
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
                    isEditorVisible: isEditorVisible
                    
                };
            })()
        };
        return ist.Contact.viewModel;
    });
