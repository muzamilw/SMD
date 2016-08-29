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
                    selectedQuestionCountryList = ko.observableArray([]),
                    professions = ko.observableArray([]),
                    ageRange = ko.observableArray([]),
                    AgeRangeEnd=ko.observable(80),
                    AgeRangeStart = ko.observable(13),
                    SelectedPvcVal = ko.observable(0),
                    Gender = ko.observable('1'),
                    selectedLocationLong = ko.observable(0),
                    selectedLocationLat = ko.observable(0),
                    genderppc = ko.observable(),
                    selectedLocation = ko.observable(),
                    selectedLocationRadius = ko.observable(),
                    HeaderText = ko.observable(),
                    StatusText = ko.observable(),
                    priorityList = ko.observableArray([0, 1, 2, 3, 4, 5, 6, 7, 8, 9]),
                    selectedLocationIncludeExclude = ko.observable(true),
                    isShowArchiveBtn = ko.observable(true),
                    canSubmitForApproval = ko.observable(true),
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
                    ageppc= ko.observable(),
                    GetObj = ko.observable(),
                    isTerminateBtnVisible = ko.observable(true),
                    previewScreenNumber = ko.observable(0),
                    // Random number
                    randomIdForNewObjects= -1,
                    //Get Questions
                    getQuestions = function (defaultCountryId, defaultLanguageId) {
                        dataservice.searchProfileQuestions(
                            {
                                ProfileQuestionFilterText: filterValue(),
                                LanguageFilter: langfilterValue() || defaultLanguageId,
                                QuestionGroupFilter: qGroupfilterValue(),
                                CountryFilter: countryfilterValue() || defaultCountryId,
                                PageSize: pager().pageSize(),
                                PageNo: pager().currentPage(),
                                SortBy: sortOn(),
                                IsAsc: sortIsAsc(),
                                fmode : fmodevar
                            },
                            {
                                success: function (data) {
                                    questions.removeAll();
                                    _.each(data.ProfileQuestions, function (item) {
                                        

                                        questions.push(model.questionServertoClientMapper(SetStatusForQuestion(item)));
                                        professions.removeAll();
                                        ko.utils.arrayPushAll(professions(), data.Professions);
                                        professions.valueHasMutated();
                                    });
                                   
                                    pager().totalCount(data.TotalCount);
                                },
                                error: function () {
                                    toastr.error("Failed to load profile questions!");
                                }
                            });
                    },
                       DilveredPercentage = function (item)
                       {
                           var percent = 0.0;
                           if (item.AsnswerCount != null && item.AsnswerCount > 0 && item.AnswerNeeded != null && item.AnswerNeeded > 0) {
                               percent = (item.AsnswerCount / item.AnswerNeeded) * 100;
                           }
                           return Math.round(percent);
                         },
                    SetStatusForQuestion = function (item)
                    {
                       
                        if (item.Status == 1) {
                            item.StatusValue = "Draft";
                        } else if (item.status == 2) {
                            item.StatusValue = "Submitted for Approval";// canSubmitForApproval(false);
                        } else if (item.Status == 3) {
                            item.StatusValue = "Live";// canSubmitForApproval(false);
                        } else if (item.Status == 4) {
                            item.StatusValue = "Paused"; //canSubmitForApproval(false);
                        } else if (item.Status == 5) {
                            item.StatusValue = "Completed"; //canSubmitForApproval(false);
                        } else if (item.Status == 6) {
                            item.StatusValue = "Approval Rejected"; //canSubmitForApproval(true);
                        }
                        item.CreatedBy=DilveredPercentage(item);
                        return item;
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
                        pager().reset();
                        getQuestions();
                    },
                    getQuestionsByFilter = function ()
                    {
                        pager().reset();
                        getQuestions();
                    },
                    onRemoveLocation = function (item) {
                        // Ask for confirmation
                        confirmation.afterProceed(function () {
                            deleteLocation(item);
                            // build location dropdown
                            selectedQuestionCountryList.removeAll();
                            _.each(selectedQuestion().ProfileQuestionTargetLocation(), function (item) {
                                addCountryToCountryList(item.CountryID(), item.Country());
                            });
                        });
                        confirmation.show();

                    },
                    deleteLocation = function (item) {
                        if (item.CountryID() == userBaseData().CountryId && item.CityID() == userBaseData().CityId) {
                            toastr.error("You cannot remove your home town or country!");
                        } else {
                            selectedQuestion().ProfileQuestionTargetLocation.remove(item);
                            toastr.success("Removed Successfully!");
                        }

                    },
                       addCountryToCountryList = function (country, name) {
                           debugger;
                           if (country != undefined) {

                               var matcharry = ko.utils.arrayFirst(selectedQuestionCountryList(), function (item) {

                                   return item.id == country;
                               });

                               if (matcharry == null) {
                                   selectedQuestionCountryList.push({ id: country, name: name });
                               }
                           }
                       },
                findLocationsInCountry = function (id) {
                    var list = ko.utils.arrayFilter(selectedQuestion().ProfileQuestionTargetLocation(), function (prod) {
                        return prod.CountryID() == id;
                    });
                    return list;
                },
                    // Add new Profile Question
                    addNewProfileQuestion = function () {
                        selectedQuestion(new model.question());
                        // Set Country Id and Language Id for now as UK and English
                        selectedQuestion().countryId(214);
                        selectedQuestion().languageId(41);
                        selectedQuestion().penalityForNotAnswering(0);
                        previewScreenNumber(1);
                        view.initializeTypeahead();
                        isEditorVisible(true);
                    },
                     resetLocations = function () {
                         $("#searchCampaignLocations").val("");
                         selectedLocationRadius("");
                     },
                        addCountryToCountryList = function (country, name) {
                            if (country != undefined) {

                                var matcharry = ko.utils.arrayFirst(selectedQuestionCountryList(), function (item) {

                                    return item.id == country;
                                });

                                if (matcharry == null) {
                                    selectedQuestionCountryList.push({ id: country, name: name });
                                }
                            }
                        },
                
                    // Close Editor 
                    closeEditDialog = function () {
                        if (!hasChangesOnQuestion()) {
                            isEditorVisible(false);
                            return;
                        }
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
                        gotoScreen(1);
                        HeaderText(item.questionString());
                        StatusText(item.statusValue());
                        isTerminateBtnVisible(false);
                        view.initializeTypeahead();
                        isShowArchiveBtn(false);
                        if (item.status() == 1 || item.status() == 2 || item.status() == 3 || item.status() == 4 || item.status() == null || item.status() == 7 || item.status() == 9) {
                            canSubmitForApproval(true);
                        }
                      
                       
                        SelectedPvcVal(item.answerNeeded());
                        
                    },
                    // On Edit PQ, Get PQ Answer & linked Question 
                    getQuestionAnswer= function(profileQuestionId) {
                        dataservice.getPqAnswer(
                           {
                               ProfileQuestionId: profileQuestionId
                           },
                           {
                               success: function (answers) {
                                   selectedQuestion().answers.removeAll();
                                   _.each(answers, function (item) {
                                       selectedQuestion().answers.push(model.questionAnswerServertoClientMapper(item));
                                   });
                                   selectedQuestion().reset();
                               },
                               error: function () {
                                   toastr.error("Failed to load profile question!");
                               }
                           });
                    },
                            addNewProfessionCriteria = function () {
                                //if ($("#ddpIndustory").val() != "") {

                                //    var matchedprofessionRec = ko.utils.arrayFirst(professions(), function (arrayitem) {

                                //        return arrayitem.IndustryId == $("#ddpIndustory").val();
                                //    });
                                //    if (matchedprofessionRec != null) {
                                //        addIndustry(matchedprofessionRec);
                                //    }
                                //}
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
                                toastr.success("Deleted Successfully!");
                            },
                            error: function () {
                                    toastr.error("Failed to delete!");
                            }
                        });
                    },
                    // Make Filters Claer
                    clearFilters= function() {
                        // Language and country are hidden for now
                        //langfilterValue(undefined);
                        //countryfilterValue(undefined);
                        qGroupfilterValue(undefined);
                        filterValue(undefined);
                        filterProfileQuestion();
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
                      gotoScreen = function (number) {
                          //  toastr.error("Validation.");
                          debugger;
                          previewScreenNumber(number);

                      },
                    doBeforeSaveAnswer = function () {
                        var isValid = true;
                        if (!selectedAnswer().isValid()) {
                            selectedAnswer().errors.showAllMessages();
                            isValid = false;
                        }
                        return isValid;
                    },
                    //
                    onSaveNewAnswer = function () {
                        if (!doBeforeSaveAnswer()) {
                            return;
                        }
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

                        view.hideAnswerDialog();
                    },
                    // To do before save
                    doBeforeSave = function() {
                        var isValid = true;
                        if (!selectedQuestion().isValid()) {
                            selectedQuestion().errors.showAllMessages();
                            isValid = false;
                        }
                        if (selectedQuestion().answers().length === 0) {
                            isValid = false;
                            toastr.info("Question must have atleast 2 answers");
                        }
                        if ((selectedQuestion().answers().length) % 2 !== 0) {
                            isValid = false;
                            toastr.info("Answers must be even in number");
                        }
                        if (selectedQuestion().hasLinkedQuestions()) {
                            var answerWithLinkedQtns = selectedQuestion().answers.find(function(answer) {
                                return answer.linkedQustionsCount() > 0;
                            });
                            if (!answerWithLinkedQtns) {
                                isValid = false;
                                toastr.info("Linked Questions should have atleast 1 question linked");
                            }   
                        }
                        return isValid;
                    },
                    // Save Question / Add 
                    onSaveProfileQuestion = function () {
                        if (!doBeforeSave()) {
                            return;
                        }
                        var serverAnswers=[];
                        _.each(selectedQuestion().answers(), function (item) {
                            if (item !== null && typeof item === 'object') {
                                serverAnswers.push(item.convertToServerData());
                            } else {
                                serverAnswers.push(item().convertToServerData());
                            }
                        });
                        debugger;
                        var serverQuestion = selectedQuestion().convertToServerData();
                        serverQuestion.ProfileQuestionAnswers = serverAnswers;
                        
                        dataservice.saveProfileQuestion(serverQuestion, {
                            success: function (obj) {
                                var newAssigendGroup = qGroup.find(function (temp) {
                                    return obj.ProfileGroupId == temp.ProfileGroupId;
                                });
                                selectedQuestion().profileGroupName(newAssigendGroup.ProfileGroupName);
                                selectedQuestion().questionString(obj.Question);
                                selectedQuestion().priority(obj.Priority);
                                selectedQuestion().hasLinkedQuestions(obj.HasLinkedQuestions);
                                // Update Linked Questions
                                linkedQuestions.push({ PqId: obj.PqId, Question: obj.Question });
                                isEditorVisible(false);
                                toastr.success("Saved Successfully.");
                            },
                            error: function () {
                                toastr.error("Failed to save!");
                            }
                        });
                    },
                    // Delete Answer
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
                    },
                    // Has Changes
                    hasChangesOnQuestion = ko.computed(function () {
                        if (selectedQuestion() == undefined) {
                            return false;
                        }
                        return (selectedQuestion().hasChanges());
                    }),

                    // show / hide add answers button
                    CanAddAnswers = function () {
                        if (selectedQuestion().answers().length == 6) {
                            return false;
                        } else {
                            return true;
                        }
                    },
                    // Filter Linked Questions on edit of Profile Question 
                    filteredLinkedQuestions = ko.computed(function () {
                        if (!selectedQuestion()) {
                            return linkedQuestions();
                        }
                        // Return all but that is being edited
                        return linkedQuestions.filter(function (temp) {
                            return selectedQuestion().qId() !== temp.PqId;
                        });
                    }),
                    // get sorted array of answers 
                    getSortedAnswers = ko.computed(function () {
                        if (selectedQuestion() == null)
                            return null;
                        return selectedQuestion().answers().sort(function (left, right) {
                            var leftOrder = 100,rightOrder = 100;
                            if (right.sortOrder() != null)
                                rightOrder = right.sortOrder();
                            if (left.sortOrder() != null)
                                leftOrder = left.sortOrder();
                            return leftOrder == rightOrder ?
                                 null :
                                 (leftOrder < rightOrder ? -1 : 1);
                        });
                    }),
                    onAddLocation = function (item) {

                        selectedLocation().Radius = (selectedLocationRadius);
                        selectedLocation().IncludeorExclude = (selectedLocationIncludeExclude);
                        selectedQuestion().ProfileQuestionTargetLocation.push(new model.ProfileQuestionTargetLocation.Create({
                            CountryId: selectedLocation().CountryID,
                            CityId: selectedLocation().CityID,
                            Radius: selectedLocation().Radius(),
                            Country: selectedLocation().Country,
                            City: selectedLocation().City,
                            IncludeorExclude: selectedLocation().IncludeorExclude(),
                            PQID: selectedQuestion().qId(),
                            Latitude: selectedLocation().Latitude,
                            Longitude: selectedLocation().Longitude,
                        }));
                        addCountryToCountryList(selectedLocation().CountryID, selectedLocation().Country);
                        resetLocations();
                    },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        pager(pagination.Pagination({ PageSize: 10 }, questions, getQuestions));
                        // Base Data Call
                        getBasedata();
                        // First request for LV
                        getQuestions(214, 41);
                        for (var i = 10; i < 81; i++) {
                            var text = i.toString();
                            if (i == 110)
                                text += "+";
                            ageRange.push({ value: i.toString(), text: text });
                        }
                       
                        ageRange.push({ value: 120, text: "80+" });
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
                    hasChangesOnQuestion: hasChangesOnQuestion,
                    CanAddAnswers: CanAddAnswers,
                    filteredLinkedQuestions: filteredLinkedQuestions,
                    getSortedAnswers: getSortedAnswers,
                    getQuestionsByFilter: getQuestionsByFilter,
                    SetStatusForQuestion: SetStatusForQuestion,
                    previewScreenNumber: previewScreenNumber,
                    gotoScreen: gotoScreen,
                    selectedQuestionCountryList: selectedQuestionCountryList,
                    ageRange: ageRange,
                    AgeRangeStart: AgeRangeStart,
                    AgeRangeEnd:AgeRangeEnd,
                    Gender: Gender,
                    selectedLocationIncludeExclude: selectedLocationIncludeExclude,
                    selectedLocationRadius: selectedLocationRadius,
                    selectedLocationLat: selectedLocationLat,
                    selectedLocationLong: selectedLocationLong,
                    genderppc: genderppc,
                    ageppc: ageppc,
                    addNewProfessionCriteria: addNewProfessionCriteria,
                    professions: professions,
                    HeaderText: HeaderText,
                    StatusText: StatusText,
                    SelectedPvcVal: SelectedPvcVal,
                    isTerminateBtnVisible: isTerminateBtnVisible,
                    isShowArchiveBtn: isShowArchiveBtn,
                    canSubmitForApproval: canSubmitForApproval,
                    resetLocations:resetLocations,
                    onAddLocation: onAddLocation,
                    addCountryToCountryList: addCountryToCountryList,
                    findLocationsInCountry: findLocationsInCountry,
                    selectedLocation: selectedLocation,
                    onRemoveLocation: onRemoveLocation,
                    deleteLocation: deleteLocation
                };
            })()
        };
        return ist.ProfileQuestion.viewModel;
    });
