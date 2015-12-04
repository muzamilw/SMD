define(["ko", "underscore", "underscore-ko"], function (ko) {
    var AdvertGridModel = function () {
        var
        self,
       CampaignId = ko.observable(),
                DisplayTitle = ko.observable(),
                Status = ko.observable(),
                StartDateTime = ko.observable(),
                EndDateTime = ko.observable(),
                MaxBudget = ko.observable(),
                ResultClicks = ko.observable(),
                AmountSpent = ko.observable(),
        self = {
            CampaignId: CampaignId,
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

    var adsMapper = function (oads) {
        return {
            CampaignId: oads.CampaignId,
            DisplayTitle: oads.DisplayTitle,
            Status: oads.Status,
            StartDateTime: oads.StartDateTime,
            EndDateTime: oads.EndDateTime,
            MaxBudget: oads.MaxBudget,
            ResultClicks: oads.ResultClicks,
            AmountSpent: oads.AmountSpent
        };
    };
   
    return {
        AdvertGridModel: AdvertGridModel,
        adsMapper: adsMapper
       
    };
});