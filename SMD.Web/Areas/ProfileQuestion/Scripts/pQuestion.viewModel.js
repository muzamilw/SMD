/*
    Module with the view model for the Profile Questions
*/
define("pQuestion/pQuestion.viewModel",
    ["jquery", "amplify", "ko", "pQuestion/pQuestion.dataservice", "pQuestion/pQuestion.model", "common/pagination",
     "common/confirmation.viewModel", "common/stripeChargeCustomer.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation, stripeChargeCustomer) {
        var ist = window.ist || {};
        ist.ProfileQuestion = {
            viewModel: (function() {
                var view,
                    //  Array
                    questions = ko.observableArray([]),
                    // Base Data
                    langs = ko.observableArray([]),
                    countries = ko.observableArray([]),
                    qGroup = ko.observableArray([]),
                    priorityList = ko.observableArray([0,1,2,3,4,5,6,7,8,9]),
                    questiontype = ko.observableArray([{
                        typeId: 1,
                        typeName:'Single Choice'
                    }, {
                        typeId: 2,
                        typeName: 'Multiple Choice'
                    }]),
                    //pager
                    pager = ko.observable(),
                    //sorting
                    sortOn = ko.observable(1),
                    // Search Filter value 
                    filterValue = ko.observable(),
                    langfilterValue = ko.observable(41),
                    countryfilterValue = ko.observable(214),
                    qGroupfilterValue = ko.observable(undefined),
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
                                LanguageFilter: langfilterValue() || 41,
                                QuestionGroupFilter: qGroupfilterValue() || 0,
                                CountryFilter : countryfilterValue() || 214,
                                PageSize: pager().pageSize(),
                                PageNo: pager().currentPage(),
                                SortBy: sortOn(),
                                IsAsc: sortIsAsc()
                            },
                            {
                                success: function (data) {
                                    questions.removeAll();
                                     pager().totalCount(data.TotalCount);
                                    _.each(data.ProfileQuestions, function (item) {
                                        questions.push(model.questionServertoClientMapper(item));
                                    });

                                },
                                error: function () {
                                    toastr.error("Failed to load profile questions!");
                                }
                            });
                    },
                    
                     //Get Base Data for Questions
                    getBasedata = function () {
                        dataservice.getBaseData(null, {
                            success: function (baseDataFromServer) {
                                langs.removeAll();
                                countries.removeAll();
                                qGroup.removeAll();
                                
                                ko.utils.arrayPushAll(langs(), baseDataFromServer.LanguageDropdowns);
                                ko.utils.arrayPushAll(countries(), baseDataFromServer.CountryDropdowns);
                                ko.utils.arrayPushAll(qGroup(), baseDataFromServer.ProfileQuestionGroupDropdowns);
                                
                                langs.valueHasMutated();
                                countries.valueHasMutated();
                                qGroup.valueHasMutated();
                                
                                langfilterValue(41);
                                countryfilterValue(214);
                            },
                            error: function () {
                                    toastr.error("Failed to load base data!");
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
                    onDeleteProfileQuestion = function(item) {
                        // Ask for confirmation
                        confirmation.afterProceed(function () {
                            deleteProfileQuestion(item);
                        });
                        confirmation.show();
                    },
                    // Delete PQ
                    deleteProfileQuestion = function (item) {
                        dataservice.deleteProfileQuestion(item.convertToServerData(), {
                            success: function () {
                                var newObjtodelete = questions.find(function (temp) {
                                    return temp.qId() == temp.qId();
                                });
                                questions.remove(newObjtodelete);
                                toastr.success("You are Good!");
                            },
                            error: function () {
                                    toastr.error("Failed to delete!");
                            }
                        });
                    },
                    // Make Filters Claer
                    clearFilters= function() {
                        langfilterValue(undefined);
                        countryfilterValue(undefined);
                        qGroupfilterValue(undefined);
                        filterValue(undefined);
                        getQuestions();
                    },
                    // Charge Customer
                    chargeCustomer = function() {
                        stripeChargeCustomer.show(undefined, 2000, '2 Widgets');
                    },
                    // Charge Existing Customer
                    chargeExistingCustomer = function () {
                        stripeChargeCustomer.show(undefined, 2000, '2 Widgets', true);
                    },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        pager(pagination.Pagination({ PageSize: 10 }, questions, getQuestions));
                        // Base Data Call
                        getBasedata();
                        // First request for LV
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
                    getBasedata:getBasedata,
                    isEditorVisible: isEditorVisible,
                    filterProfileQuestion: filterProfileQuestion,
                    filterValue: filterValue,
                    addNewProfileQuestion: addNewProfileQuestion,
                    closeEditDialog: closeEditDialog,
                    onEditProfileQuestion: onEditProfileQuestion,
                    onDeleteProfileQuestion: onDeleteProfileQuestion,
                    langs: langs,
                    countries: countries,
                    qGroup: qGroup,
                    langfilterValue :langfilterValue,
                    countryfilterValue:countryfilterValue,
                    qGroupfilterValue: qGroupfilterValue,
                    clearFilters: clearFilters,
                    priorityList: priorityList,
                    questiontype: questiontype,
                    chargeCustomer: chargeCustomer,
                    chargeExistingCustomer: chargeExistingCustomer
                };
            })()
        };
        return ist.ProfileQuestion.viewModel;
    });
