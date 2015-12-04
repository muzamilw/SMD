define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
        ads = function () {
            var
               
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

                AdvertGridModel = function () {
                var
                self,
                CampaignID = ko.observable(),
                DisplayTitle = ko.observable(),
                Status = ko.observable(),
                StartDateTime = ko.observable(),
                EndDateTime = ko.observable(),
                MaxBudget = ko.observable(),
                ResultClicks = ko.observable(),
                AmountSpent = ko.observable()
                self = {
                    CampaignID: CampaignID,
                    DisplayTitle: DisplayTitle,
                    Status: Status,
                    StartDateTime: StartDateTime,
                    EndDateTime: EndDateTime,
                    MaxBudget: MaxBudget,
                    ResultClicks: ResultClicks,
                    AmountSpent: AmountSpent
                };
                return self;
            };
            return {
                AdvertGridModel: AdvertGridModel,
                hasChanges: hasChanges,
                reset: reset,
                isValid: isValid,
                errors: errors
            };
        };

   
    return {
        ads: ads
       
    };
});