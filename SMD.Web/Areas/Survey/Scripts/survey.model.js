define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
        Survey = function (SQID, LanguageID, CountryID, UserID, Status, Question, Gender, Language, Country) {
            var
                SQID = ko.observable(SQID),
                LanguageID = ko.observable(LanguageID),
                CountryID = ko.observable(CountryID),
                UserID = ko.observable(UserID),
                Status = ko.observable(Status),
                Question = ko.observable(Question),
                Gender = ko.observable(Gender),
                Language = ko.observable(Language),
                Country = ko.observable(Country),
                errors = ko.validation.group({

                    }),
                // Is Valid
                isValid = ko.computed(function () {
                    return errors().length === 0;
                }),
                dirtyFlag = new ko.dirtyFlag({

                }),
                // Has Changes
                hasChanges = ko.computed(function () {
                    return dirtyFlag.isDirty();
                }),
                // Reset
                reset = function () {
                    dirtyFlag.reset();
                },
                // Convert to server data
                convertToServerData = function () {
                    return {
                        SQID: SQID(),
                        LanguageID: LanguageID(),
                        CountryID: CountryID(),
                        UserID: UserID(),
                        Status: Status(),
                        Question: Question(),
                        Gender: Gender(),
                        Language: Language(),
                        Country: Country()
                    };
                };
            return {
                SQID :(SQID),
                LanguageID :(LanguageID),
                CountryID :(CountryID),
                UserID :(UserID),
                Status :(Status),
                Question :(Question),
                Gender :(Gender),
                Language :(Language),
                Country :(Country),
                hasChanges: hasChanges,
                reset: reset,
                isValid: isValid,
                errors: errors
            };
        };

   
    // Factory Method
    Survey.Create = function (source) {
        return new Survey(source.SQID, source.LanguageID, source.CountryID, source.UserID, source.Status, source.Question, source.Gender, source.Language, source.Country);
    };

    return {
        Survey: Survey
       
    };
});