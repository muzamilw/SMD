define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
        PayPallApp = function (poStageOneUserId, poCentzAmount, poTargetPayoutAccount, poCompanyId, poPayOutId) {
            var
                stageOneUserId = ko.observable(poStageOneUserId),
                centzAmount = ko.observable(poCentzAmount),
                targetPayoutAccount = ko.observable(poTargetPayoutAccount),
                companyId = ko.observable(poCompanyId),
                payOutId = ko.observable(poPayOutId),

                errors = ko.validation.group({

                }),
                // Is Valid
                isValid = ko.computed(function () {
                    return errors().length === 0;
                }),
                dirtyFlag = new ko.dirtyFlag({
                    //rejectedReason: rejectedReason
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
                        //PqId: id(),
                        //Approved: isApproved(),
                        //RejectionReason: rejectedReason(),
                    };
                };
            return {

                stageOneUserId: stageOneUserId,
                centzAmount: centzAmount,
                targetPayoutAccount: targetPayoutAccount,
                companyId: companyId,
                payOutId: payOutId,
                //isApproved: isApproved,
                //rejectedReason: rejectedReason,
                hasChanges: hasChanges,
                convertToServerData: convertToServerData,
                reset: reset,
                isValid: isValid,
                errors: errors

            };
        };


    ///////////////////////////////////////////////////////// Ad-Campaign
    //server to client mapper For AdCampaign
    var PayPallAppServertoClientMapper = function (itemFromServer) {


        return new PayPallApp(itemFromServer.StageOneUserId, itemFromServer.CentzAmount, itemFromServer.TargetPayoutAccount, itemFromServer.CompanyId, itemFromServer.PayOutId);
    };

    // Function to attain cancel button functionality ProfileQuestion
    PayPallApp.CreateFromClientModel = function (item) {
        // To be Implemented
    };

    return {
        PayPallApp: PayPallApp,
        PayPallAppServertoClientMapper: PayPallAppServertoClientMapper

    };
});