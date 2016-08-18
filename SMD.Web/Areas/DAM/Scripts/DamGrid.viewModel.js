/*
    Module with the view model for the Profile Questions
*/
define("DamGrid/DamGrid.viewModel",
    ["jquery", "amplify", "ko", "DamGrid/DamGrid.dataservice", "common/pagination", "DamGrid/DamGrid.model", "common/confirmation.viewModel", "common/stripeChargeCustomer.viewModel"],
    function ($, amplify, ko, dataservice, pagination, model, confirmation, stripeChargeCustomer) {
        var ist = window.ist || {};
        ist.DamGrid = {
            viewModel: (function () {
                var view,

                    //  Array
                    questions = ko.observableArray([]),
                    selectedLocationLat = ko.observable(0),
                    qStatuses = ko.observableArray([{ id: 0, value: 'All' }, { id: 1, value: 'Draft' }, { id: 2, value: 'Submitted for Approval' }, { id: 3, value: 'Live' }, { id: 4, value: 'Paused' }, { id: 5, value: 'Completed' }, { id: 6, value: 'Rejected' }]);
                    statusFilterValue = ko.observable();
                    //Get Questions
                    getImages = function () {   
                        dataservice.searchDamGridQuestions(
                            {
                                SearchText: filterValue(),
                                LanguageFilter: langfilterValue(),
                                CountryFilter: countryfilterValue(),
                                PageSize: pager().pageSize(),
                                PageNo: pager().currentPage(),
                                Status: statusFilterValue(),
                                FirstLoad:false
                            },
                            {
                                success: function (data) {
                                    populateDamGridQuestions(data);
                                },
                                error: function () {
                                    toastr.error("Failed to load  questions!");
                                }
                            });
                    },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        getQuestions();
                    };
                return {
                    initialize: initialize,
                    pager: pager,
                    selectedQuestion: selectedQuestion,
                    questions: questions,
                    getQuestions: getQuestions,
                    getBasedata: getBasedata,
                    isEditorVisible: isEditorVisible,
                    filterDamGridQuestion: filterDamGridQuestion,
                    filterValue: filterValue,
                    addNewDamGrid: addNewDamGrid,
                    closeEditDialog: closeEditDialog,
                    onEditDamGrid: onEditDamGrid,
                    langs: langs,
                    countries: countries,
                    langfilterValue: langfilterValue,
                    clearFilters: clearFilters,
                    countryfilterValue: countryfilterValue,
                    storeLeftImageCallback: storeLeftImageCallback,
                    storeRightImageCallback: storeRightImageCallback,
                    ageRange: ageRange,
                    selectedLocation: selectedLocation,
                    selectedLocationRadius: selectedLocationRadius,
                    onAddLocation: onAddLocation,
                    onRemoveLocation: onRemoveLocation,
                    deleteLocation: deleteLocation,
                    selectedLocationIncludeExclude: selectedLocationIncludeExclude,
                    selectedLangIncludeExclude: selectedLangIncludeExclude,
                    selectedIndustryIncludeExclude: selectedIndustryIncludeExclude,
                    selectedEducationIncludeExclude:selectedEducationIncludeExclude,
                    selectedLocationLat: selectedLocationLat,
                    selectedLocationLong: selectedLocationLong,
                    addLanguage: addLanguage,
                    addIndustry:addIndustry,
                    onRemoveLanguage: onRemoveLanguage,
                    selectedCriteria: selectedCriteria,
                    profileQuestionList: profileQuestionList,
                    profileAnswerList: profileAnswerList,
                    DamGridQuestionList: DamGridQuestionList,
                    addNewProfileCriteria: addNewProfileCriteria,
              //      addNewDamGridCriteria: addNewDamGridCriteria,
                    saveCriteria: saveCriteria,
                    onEditCriteria: onEditCriteria,
                    onDeleteCriteria: onDeleteCriteria,
                    onChangeProfileQuestion: onChangeProfileQuestion,
                  //  onChangeDamGridQuestion: onChangeDamGridQuestion,
                    isCriteriaEditable: isCriteriaEditable,
                    hasChangesOnQuestion: hasChangesOnQuestion,
                    editCriteriaHeading: editCriteriaHeading,
                    isNewCriteria: isNewCriteria,
                    onSaveDamGridQuestion: onSaveDamGridQuestion,
                    onSubmitDamGridQuestion: onSubmitDamGridQuestion,
                    canSubmitForApproval:canSubmitForApproval,
                    titleText: titleText,
                    onRemoveIndustry: onRemoveIndustry,
                    getAudienceCount: getAudienceCount,
                    visibleTargetAudience: visibleTargetAudience,
                    totalAudience: totalAudience,
                    reachedAudience: reachedAudience,
                    audienceReachMode: audienceReachMode,
                    bindAudienceReachCount: bindAudienceReachCount,
                    userBaseData: userBaseData,
                    setupPrice: setupPrice,
                    addEducation: addEducation,
                    selectedQuestionCountryList: selectedQuestionCountryList,
                    addCountryToCountryList: addCountryToCountryList,
                    findLocationsInCountry: findLocationsInCountry,
                    errorList: errorList,
                    changeStatus: changeStatus,
                    educations: educations,
                    professions: professions,
                    addNewEducationCriteria: addNewEducationCriteria,
                    addNewProfessionCriteria: addNewProfessionCriteria,
                    statusFilterValue: statusFilterValue,
                    qStatuses: qStatuses,
                    saveProfileQuestion: saveProfileQuestion,
                    updateProfileQuestion: updateProfileQuestion,
                    updateDamGridCriteria: updateDamGridCriteria
                };
            })()
        };
        return ist.DamGrid.viewModel;
    });
