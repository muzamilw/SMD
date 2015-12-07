/*
    Module with the view model for the Profile Questions
*/
define("survey/survey.viewModel",
    ["jquery", "amplify", "ko", "survey/survey.dataservice", "common/pagination", "survey/survey.model", "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, pagination, model, confirmation) {
        var ist = window.ist || {};
        ist.survey = {
            viewModel: (function () {
                var view,

                    //  Array
                    questions = ko.observableArray([]),
                    // Base Data
                    langs = ko.observableArray([]),
                    countries = ko.observableArray([]),
                    //pager
                    pager = ko.observable(),
                    // Search Filter value 
                    filterValue = ko.observable(),
                    langfilterValue = ko.observable(41),
                    countryfilterValue = ko.observable(214),
                    // Controlls editor visibility 
                    isEditorVisible = ko.observable(false),
                    //// Editor View Model   // view model for editing survey
                    //editorViewModel = new ist.ViewModel(model.hireGroupImage),
                    ////selected Question
                    //selectedQuestion = editorViewModel.itemForEditing,

                    //Get Questions
                    getQuestions = function () {   
                        dataservice.searchSurveyQuestions(
                            {
                                SearchText: filterValue(),
                                LanguageFilter: langfilterValue(),
                                CountryFilter: countryfilterValue(),
                                PageSize: pager().pageSize(),
                                PageNo: pager().currentPage(),
                                FirstLoad:false
                            },
                            {
                                success: function (data) {
                                    populateSurveyQuestions(data);
                                },
                                error: function () {
                                    toastr.error("Failed to load  questions!");
                                }
                            });
                    },

                    // //Get Base Data for Questions
                    getBasedata = function () {
                        dataservice.searchSurveyQuestions({
                            FirstLoad: true,
                            PageSize: pager().pageSize(),
                            PageNo: pager().currentPage()
                        },
                            {
                                success: function (data) {
                                    // popullate base data 
                                    langs.removeAll();
                                    countries.removeAll();
                                    ko.utils.arrayPushAll(langs(), data.LanguageDropdowns);
                                    ko.utils.arrayPushAll(countries(), data.CountryDropdowns);
                                    langs.valueHasMutated();
                                    countries.valueHasMutated();
                                    // populate survey questions 
                                    populateSurveyQuestions(data);
                                    console.log(questions());
                                },
                                error: function () {
                                    toastr.error("Failed to load base data!");
                                }
                            });
           
                    },
                    // update survey questions 
                    populateSurveyQuestions = function (data) {
                        questions.removeAll();
                        pager().totalCount(data.TotalCount);
                        _.each(data.SurveyQuestions, function (item) {
                            questions.push(model.Survey.Create(item));
                        });

                    }
                    // Search Filter 
                    filterSurveyQuestion = function () {
                        getQuestions();
                    },
                    // Add new Profile Question
                    addNewSurvey = function () {
                        isEditorVisible(true);
                    },
                    // Close Editor 
                    closeEditDialog = function () {
                        isEditorVisible(false);
                    },
                    // On editing of existing PQ
                    onEditSurvey = function (item) {
                       // selectedQuestion(item);
                        isEditorVisible(true);
                    },
                    onDeleteSurvey = function (item) {
                        // Ask for confirmation
                        confirmation.afterProceed(function () {
                            deleteProfileQuestion(item);
                        });
                        confirmation.show();
                    },
                    // Delete PQ
                    deleteSurvey = function (item) {
                        //dataservice.deleteProfileQuestion(item.convertToServerData(), {
                        //    success: function () {
                        //        var newObjtodelete = questions.find(function (temp) {
                        //            return temp.qId() == temp.qId();
                        //        });
                        //        questions.remove(newObjtodelete);
                        //        toastr.success("You are Good!");
                        //    },
                        //    error: function () {
                        //        toastr.error("Failed to delete!");
                        //    }
                        //});
                    },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        pager(pagination.Pagination({ PageSize: 10 }, questions, getQuestions));
                        // Base Data Call
                        getBasedata();
                        // First request for LV
                      //  getQuestions();
                    };
                return {
                    initialize: initialize,
                    pager: pager,
                  //  selectedQuestion: selectedQuestion,
                    questions: questions,
                    getQuestions: getQuestions,
                    getBasedata: getBasedata,
                    isEditorVisible: isEditorVisible,
                    filterSurveyQuestion: filterSurveyQuestion,
                    filterValue: filterValue,
                    addNewSurvey: addNewSurvey,
                    closeEditDialog: closeEditDialog,
                    onEditSurvey: onEditSurvey,
                    deleteSurvey: deleteSurvey,
                    langs: langs,
                    countries: countries,
                    langfilterValue: langfilterValue,
                    countryfilterValue: countryfilterValue
                };
            })()
        };
        return ist.survey.viewModel;
    });
