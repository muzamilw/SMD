define(["ko", "underscore", "underscore-ko"], function(ko) {

    var // ReSharper disable InconsistentNaming
      AdCampaign = function (sQId, spcName, spcDes, spcIsApproved, spcRejectionReason,
          subDate, spcCreatedBy, spcType, spcPath, spcLink, spcrate,spcBudget) {
          var
              id = ko.observable(sQId),
              campaignName = ko.observable(spcName),
              description = ko.observable(spcDes),

              isApproved = ko.observable(spcIsApproved),
              rejectionReason = ko.observable(spcRejectionReason),
              submissionDate = ko.observable(subDate),
              createdBy = ko.observable(spcCreatedBy),
              type = ko.observable(spcType),
              imagePath = ko.observable(spcPath),
              landingLink = ko.observable(spcLink),
              clickRate = ko.observable(spcrate),
              maxBudget = ko.observable(spcBudget),
             
             
              errors = ko.validation.group({

              }),
              // Is Valid
              isValid = ko.computed(function () {
                  return errors().length === 0;
              }),
              dirtyFlag = new ko.dirtyFlag({
                  rejectionReason: rejectionReason
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
                      CampaignId: id(),
                      Approved: isApproved(),
                      RejectedReason: rejectionReason(),
                  };
              };
          return {
              id: id,
              campaignName: campaignName,
              description: description,
              isApproved: isApproved,
              rejectionReason: rejectionReason,
              submissionDate: submissionDate,
              createdBy: createdBy,
              type: type,
              imagePath: imagePath,
              landingLink: landingLink,
              clickRate:clickRate,
              maxBudget:maxBudget,
              
              hasChanges: hasChanges,
              convertToServerData:convertToServerData,
              reset: reset,
              isValid: isValid,
              errors: errors
          };
      };


    ///////////////////////////////////////////////////////// Ad-Campaign
    //server to client mapper For AdCampaign
    var AdCampaignServertoClientMapper = function (itemFromServer) {
        return new AdCampaign(itemFromServer.CampaignId, itemFromServer.CampaignName, itemFromServer.Description,
            itemFromServer.Approved, itemFromServer.RejectedReason,
            itemFromServer.CreatedDateTime, itemFromServer.CreatedBy, itemFromServer.Type, itemFromServer.ImagePath, itemFromServer.LandingPageVideoLink,
        itemFromServer.ClickRate, itemFromServer.MaxBudget);
    };
    
    // Function to attain cancel button functionality AdCampaign
    AdCampaign.CreateFromClientModel = function (item) {
       // To be Implemented
    };
   
    return {
        AdCampaign: AdCampaign,
        AdCampaignServertoClientMapper: AdCampaignServertoClientMapper
    };
});