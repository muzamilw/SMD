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
                    //  Questions list on LV
                    questions = ko.observableArray([]),
                    //  Question list on Editor for linked questions
                    linkedQuestions = ko.observableArray([]),
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
                    // Controls visibility of Image/text on answer editor 
                    isAnswerIsImage = ko.observable(false),
                    // Editor View Model
                    editorViewModel = new ist.ViewModel(model.hireGroupImage),
                    //selected Question
                    selectedQuestion = editorViewModel.itemForEditing,
                     //selected Answer
                    selectedAnswer = ko.observable(),
                    // Random number
                    randomIdForNewObjects= -1,
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
                                linkedQuestions.removeAll();
                                
                                ko.utils.arrayPushAll(langs(), baseDataFromServer.LanguageDropdowns);
                                ko.utils.arrayPushAll(countries(), baseDataFromServer.CountryDropdowns);
                                ko.utils.arrayPushAll(qGroup(), baseDataFromServer.ProfileQuestionGroupDropdowns);
                                ko.utils.arrayPushAll(linkedQuestions(), baseDataFromServer.ProfileQuestionDropdowns);
                                
                                langs.valueHasMutated();
                                countries.valueHasMutated();
                                qGroup.valueHasMutated();
                                linkedQuestions.valueHasMutated();
                                
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
                    addNewProfileQuestion = function () {
                        selectedQuestion(new model.question());
                        isEditorVisible(true);
                    },
                    // Close Editor 
                    closeEditDialog = function () {
                        // Ask for confirmation
                        confirmation.afterProceed(function () {
                            selectedQuestion().answers.removeAll();
                            selectedQuestion(undefined);
                            isEditorVisible(false);
                        });
                        confirmation.show();
                    },
                    // On editing of existing PQ
                    onEditProfileQuestion = function (item) {
                        getQuestionAnswer(item.qId());
                        selectedQuestion(item);
                        isEditorVisible(true);
                    },
                    // On Edit PQ, Get PQ Answer & linked Question 
                    getQuestionAnswer= function(profileQuestionId) {
                        dataservice.getPqAnswer(
                           {
                               ProfileQuestionId: profileQuestionId
                           },
                           {
                               success: function (answers) {
                                   _.each(answers, function (item) {
                                       selectedQuestion().answers.push(model.questionAnswerServertoClientMapper(item));
                                   });
                                   selectedQuestion().reset();
                               },
                               error: function () {
                                   toastr.error("Failed to load profile questions!");
                               }
                           });
                    },
                   
                    // Delete Handler PQ
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
                                    return item.qId() == temp.qId();
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
                    // Add new Answer
                    addNewAnswer = function () {
                        var obj = new model.questionAnswer();
                        obj.type("1");
                        obj.pqAnswerId(randomIdForNewObjects);
                        randomIdForNewObjects--;
                        selectedAnswer(obj);
                    },
                    answerTypeChangeHandler= function(item) {
                        //var tfff = this;
                        //return true;
                    },
                    onEditQuestionAnswer= function(item) {
                        selectedAnswer(item);
                    },
                    //
                    onSaveNewAnswer = function () {
                        var newOnj = selectedAnswer();
                        var objId = newOnj.pqAnswerId();
                        if (objId < 0) {
                            var newlyAddedobj = selectedQuestion().answers.find(function (ans) {
                                return ans.pqAnswerId() == objId;
                            });
                            selectedQuestion().answers.remove(newlyAddedobj);
                            selectedQuestion().answers.push(newOnj);
                        } else {
                            var existingAns = selectedQuestion().answers.find(function (ans) {
                                return ans.pqAnswerId() == objId;
                            });
                            existingAns.answerString(newOnj.answerString());
                            existingAns.imagePath(newOnj.imagePath());
                            existingAns.linkedQuestion1Id(newOnj.linkedQuestion1Id());
                            existingAns.question1String(model.setAnswerString(existingAns.linkedQuestion1Id(), existingAns));
                            existingAns.linkedQuestion2Id(newOnj.linkedQuestion2Id());
                            existingAns.question2String(model.setAnswerString(existingAns.linkedQuestion2Id(), existingAns));
                            existingAns.linkedQuestion3Id(newOnj.linkedQuestion3Id());
                            existingAns.question3String(model.setAnswerString(existingAns.linkedQuestion3Id(), existingAns));
                            existingAns.linkedQuestion4Id(newOnj.linkedQuestion4Id());
                            existingAns.question4String(model.setAnswerString(existingAns.linkedQuestion4Id(), existingAns));
                            existingAns.linkedQuestion5Id(newOnj.linkedQuestion5Id());
                            existingAns.question5String(model.setAnswerString(existingAns.linkedQuestion5Id(), existingAns));
                            existingAns.linkedQuestion6Id(newOnj.linkedQuestion6Id());
                            existingAns.question6String(model.setAnswerString(existingAns.linkedQuestion6Id(), existingAns));
                            existingAns.type(newOnj.type());
                            
                        }
                    },
                    // Save Question / Add 
                    onSaveProfileQuestion= function() {
                        var serverAnswers=[];
                        _.each(selectedQuestion().answers(), function (item) {
                            if (item !== null && typeof item === 'object') {
                                serverAnswers.push(item.convertToServerData());
                            } else {
                                serverAnswers.push(item().convertToServerData());
                            }
                        });
                        var serverQuestion = selectedQuestion().convertToServerData();
                        serverQuestion.ProfileQuestionAnswers = serverAnswers;
                        
                        dataservice.saveProfileQuestion(serverQuestion, {
                            success: function (obj) {
                                var newObjtodelete = questions.find(function (temp) {
                                    return serverQuestion.PqId == temp.qId();
                                });
                                questions.remove(newObjtodelete);
                                questions.push(model.questionServertoClientMapper(obj));
                                isEditorVisible(false);
                                toastr.success("You are Good!");
                            },
                            error: function () {
                                toastr.error("Failed to delete!");
                            }
                        });
                    },
                    onDeleteQuestionAnswer= function(itemTobeDeleted) {
                        // Ask for confirmation
                        confirmation.afterProceed(function () {
                            deleteAnswer(itemTobeDeleted);
                        });
                        confirmation.show();
                    },
                    // Delete answer 
                    deleteAnswer = function (itemTobeDeleted) {
                        var newObjtodelete = selectedQuestion().answers.find(function (temp) {
                            return itemTobeDeleted.pqAnswerId() == temp.pqAnswerId();
                        });
                        selectedQuestion().answers.remove(newObjtodelete);
                        toastr.success("You are Good!");
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
                    isAnswerIsImage: isAnswerIsImage,
                    addNewAnswer: addNewAnswer,
                    answerTypeChangeHandler: answerTypeChangeHandler,
                    selectedAnswer: selectedAnswer,
                    linkedQuestions: linkedQuestions,
                    onEditQuestionAnswer: onEditQuestionAnswer,
                    onSaveNewAnswer: onSaveNewAnswer,
                    onSaveProfileQuestion: onSaveProfileQuestion,
                    onDeleteQuestionAnswer: onDeleteQuestionAnswer,
                    hasChangesOnQuestion: hasChangesOnQuestion
                };
            })()
        };
        return ist.ProfileQuestion.viewModel;
    });
