define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
      EmailPromotion = function (sQId, spcName) {
          var
              id = ko.observable(sQId),
              campaignName = ko.observable(spcName),
           


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
                      //CampaignId: id(),
                      //Approved: isApproved(),
                      //RejectedReason: rejectionReason(),
                  };
              };
          return {
              id: id,
              campaignName: campaignName,
             
              hasChanges: hasChanges,
              convertToServerData: convertToServerData,
              reset: reset,
              isValid: isValid,
              errors: errors
             

          };
      };


    ///////////////////////////////////////////////////////// Ad-Campaign
    //server to client mapper For AdCampaign
    var EmailServertoClientMapper = function (itemFromServer) {
       

        return new EmailPromotion(itemFromServer.CampaignId, itemFromServer.CampaignName);
    };

    // Function to attain cancel button functionality AdCampaign
    EmailPromotion.CreateFromClientModel = function (item) {
        // To be Implemented
    };

    return {
        EmailPromotion: EmailPromotion,
        EmailServertoClientMapper: EmailServertoClientMapper
    };
});