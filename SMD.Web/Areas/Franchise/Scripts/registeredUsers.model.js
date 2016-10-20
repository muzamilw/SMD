define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
        RegisteredUser = function (poStageOneUserId, poCentzAmount, poTargetPayoutAccount, poCompanyId, poPayOutId, poCompanyName, poDollarAmount, poStageOneRejectionReason) {
            var
                stageOneUserId = ko.observable(poStageOneUserId),
                centzAmount = ko.observable(poCentzAmount),
                targetPayoutAccount = ko.observable(poTargetPayoutAccount),
                companyId = ko.observable(poCompanyId),
                payOutId = ko.observable(poPayOutId),
                companyName = ko.observable(poCompanyName),
                dollarAmount = ko.observable(poDollarAmount),
                //isApproved = ko.observable(spcIsApproved),
                rejectionReasonStage1 = ko.observable(poStageOneRejectionReason),
                

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
                        
                    };
                };
            return {

                stageOneUserId: stageOneUserId,
                centzAmount: centzAmount,
                targetPayoutAccount: targetPayoutAccount,
                companyId: companyId,
                payOutId: payOutId,
                companyName: companyName,
                dollarAmount: dollarAmount,
                //isApproved: isApproved,
                rejectionReasonStage1: rejectionReasonStage1,
                hasChanges: hasChanges,
                convertToServerData: convertToServerData,
                reset: reset,
                isValid: isValid,
                errors: errors

            };
        };


    ///////////////////////////////////////////////////////// Ad-Campaign
    //server to client mapper For AdCampaign
    var RegisteredUserServertoClientMapper = function (itemFromServer) {


        return new RegisteredUser(itemFromServer.StageOneUserId, itemFromServer.CentzAmount, itemFromServer.TargetPayoutAccount, itemFromServer.CompanyId, itemFromServer.PayOutId, itemFromServer.CompanyName, itemFromServer.DollarAmount, itemFromServer.StageOneRejectionReason);
    };

    // Function to attain cancel button functionality ProfileQuestion
    RegisteredUser.CreateFromClientModel = function (item) {
        // To be Implemented
    };

    return {
        RegisteredUser: RegisteredUser,
        RegisteredUserServertoClientMapper: RegisteredUserServertoClientMapper

    };
});