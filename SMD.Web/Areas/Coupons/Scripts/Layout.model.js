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
            convertToServerData = function (source) {
                var result = {};
                // result.SectionId = source.sectionId();
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
        };
        return self;
    };

    Section.Create = function (source) {
        
        return new Section(source.SectionId, source.SectionName);
    }



    //Phrase Entity
    Phrase = function (specifiedPhraseId, specifiedPhrase1, specifiedSectionId) {
        var
            self,
            //Unique ID
            phraseId = ko.observable(specifiedPhraseId),
            //Field Text
            phraseText = ko.observable(specifiedPhrase1),
            //Field Id
            SectionId = ko.observable(specifiedSectionId),
            //Flag For deleted phrase

           //Is phrase checkbox is checked


             // Errors
            errors = ko.validation.group({
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),

            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                phraseId: phraseId,
                phraseText: phraseText,
                fieldId: fieldId,
                isDeleted: isDeleted,
            }),
             // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),

            //Convert To Server
            convertToServerData = function (source) {
                var result = {};
                result.PhraseId = source.phraseId();
                result.Phrase1 = source.phraseText();
                result.FieldId = source.fieldId();
                result.IsDeleted = source.isDeleted();
                return result;
            };
        self = {
            phraseId: phraseId,
            phraseText: phraseText,
            fieldId: fieldId,
            isDeleted: isDeleted,
            convertToServerData: convertToServerData,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            isPhraseChecked: isPhraseChecked,
        };
        return self;
    };
    Phrase.Create = function (source) {
        return new Phrase(source.PhraseId, source.Phrase1, source.FieldId);
    }

    //Phrase Library Save Model Entity
    PhraseLibrarySaveModel = function () {
        var
            self,
               //Convert To Server
            convertToServerData = function (source) {
                var result = {};
                result.Sections = [];
                return result;
            };
        self = {
            convertToServerData: convertToServerData,
        };
        return self;
    };

    return {
        Section: Section,
        
        Phrase: Phrase,
        PhraseLibrarySaveModel: PhraseLibrarySaveModel,
    };
});