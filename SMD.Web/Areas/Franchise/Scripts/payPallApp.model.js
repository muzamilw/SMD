define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
        PayPallApp = function (poStageOneUserId, poCentzAmount, poTargetPayoutAccount, poCompanyId, poPayOutId, poCompanyName, poDollarAmount, poStageOneRejectionReason, poStageTwoRejectionReason, poEmail, poUserId, poStageOneStatus,poStageTwoStatus) {
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
                rejectionReasonStage2 = ko.observable(poStageTwoRejectionReason),
                email = ko.observable(poEmail),
                userId = ko.observable(poUserId),
                stageOneStatus = ko.observable(poStageOneStatus),
                stageTwoStatus = ko.observable(poStageTwoStatus),

                errors = ko.validation.group({

                }),
                // Is Valid
                isValid = ko.computed(function () {
                    return errors().length === 0;
                }),
                dirtyFlag = new ko.dirtyFlag({
                    rejectionReasonStage1: rejectionReasonStage1,
                    rejectionReasonStage2: rejectionReasonStage2
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
                        PayOutId: payOutId(),
                        //Approved: isApproved(),
                        StageOneRejectionReason: rejectionReasonStage1(),
                        StageTwoRejectionReason:rejectionReasonStage2(),
                        StageOneStatus: stageOneStatus(),
                        StageTwoStatus: stageTwoStatus(),
                    };
                };
            return {

                stageOneUserId: stageOneUserId,
                centzAmount: centzAmount,
                targetPayoutAccount: targetPayoutAccount,
                companyId: companyId,
                payOutId: payOutId,
                companyName: companyName,
                dollarAmount:dollarAmount,
                //isApproved: isApproved,
                rejectionReasonStage1: rejectionReasonStage1,
                rejectionReasonStage2: rejectionReasonStage2,
                stageOneStatus: stageOneStatus,
                stageTwoStatus: stageTwoStatus,
                email: email,
                userId:userId,
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


        return new PayPallApp(itemFromServer.StageOneUserId, itemFromServer.CentzAmount, itemFromServer.TargetPayoutAccount, itemFromServer.CompanyId, itemFromServer.PayOutId, itemFromServer.CompanyName, itemFromServer.DollarAmount, itemFromServer.StageOneRejectionReason, itemFromServer.StageTwoRejectionReason, itemFromServer.Email, itemFromServer.UserId, itemFromServer.StageOneStatus,itemFromServer.StageTwoStatus);
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