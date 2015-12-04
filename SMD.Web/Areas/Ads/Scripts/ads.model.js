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
                StatusName = ko.observable(),
                StatusColor = ko.observable(),
        self = {
            CampaignId: CampaignId,
            DisplayTitle: DisplayTitle,
            Status: Status,
            StartDateTime: StartDateTime,
            EndDateTime: EndDateTime,
            MaxBudget: MaxBudget,
            ResultClicks: ResultClicks,
            AmountSpent: AmountSpent,
            StatusName: StatusName,
            StatusColor: StatusColor
        };
        return self;
    };

  
    return {
        AdvertGridModel: AdvertGridModel
       
    };
});