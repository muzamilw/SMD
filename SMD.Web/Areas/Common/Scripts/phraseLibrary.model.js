define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

    //Section view Entity
    Section = function (specifiedSectionId, specifiedName, specifiedSectionOrder) {
        var
            self,
            //Unique ID
            sectionId = ko.observable(specifiedSectionId),
            //Name
            name = ko.observable(specifiedName),
            //parent Id
            //Is Expanded
            isExpanded = ko.observable(false),
           //Section Order
            sectionOrder = ko.observable(specifiedSectionOrder),
            //Convert To Server
            Phrases = ko.observableArray([]),

            convertToServerData = function (source) {
                var result = {};
               //result.SectionId = source.sectionId();
                result.SectionId = source.SectionId();
                result.SectionName = source.SectionName();
                result.SecOrder = source.SecOrder();
                return result;
            };
        self = {
            sectionId: sectionId,
            isExpanded: isExpanded,
            name: name,
            sectionOrder: sectionOrder,
            convertToServerData: convertToServerData,
            Phrases: Phrases,
        };
        return self;
    };

    Section.Create = function (source) {

        return new Section(source.SectionId, source.SectionName);
    }



    //Phrase Entity
    Phrase = function (specifiedPhraseId, specifiedPhrase1, specifiedSectionId, IsDeleted) {
        var
            self,
            //Unique ID
            phraseId = ko.observable(specifiedPhraseId),
            //Field Text
            phraseText = ko.observable(specifiedPhrase1).extend({ required: true }),
            //Field Id
            SectionId = ko.observable(specifiedSectionId),
            //Flag For deleted phrase
            IsDeleted = ko.observable(false),
           //Is phrase checkbox is checked


             // Errors
            errors = ko.validation.group({
                phraseText: phraseText
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),

            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                phraseId: phraseId,
                phraseText: phraseText,
                
            }),
             // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),

            //Convert To Server
            convertToServerData = function (source) {
                
                var result = {};
                result.PhraseId = source.phraseId();
                result.PhraseName = source.phraseText();
                //  result.FieldId = source.fieldId();
                result.SectionId = source.SectionId();
                result.IsDeleted = source.IsDeleted;
               // result.IsDeleted = source.isDeleted();
                return result;
            };
        self = {
            phraseId: phraseId,
            phraseText: phraseText,
            SectionId:SectionId,
            convertToServerData: convertToServerData,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            IsDeleted: IsDeleted
        };
        return self;
    };
    Phrase.Create = function (source) {
        return new Phrase(source.PhraseId, source.PhraseName, source.SectionId, source.IsDeleted);
    }

  

    PhraseLibrarySaveModel = function () {
        var
            self,
               //Convert To Server
           PhrasesList = [];
        self = {
            PhrasesList: PhrasesList
        };
        return self;
    };





    return {
        Section: Section,

        Phrase: Phrase,
        PhraseLibrarySaveModel: PhraseLibrarySaveModel
    };
});