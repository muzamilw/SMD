define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
        RegisteredUser = function (ruLastLoginTime, ruEmail, ruAccountBalance, ruStatus, ruCompanyId, ruCompanyName, ruId, rufullname) {
            var
                lastLoginTime = ko.observable(ruLastLoginTime),
                email = ko.observable(ruEmail),
                accountBalance = ko.observable(ruAccountBalance),
                status = ko.observable(ruStatus),
                companyId = ko.observable(ruCompanyId),
                companyName = ko.observable(ruCompanyName),
                userId = ko.observable(ruId),
                fullname = ko.observable(rufullname),
                

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

                lastLoginTime: lastLoginTime,
                email: email,
                accountBalance: accountBalance,
                status: status,
                companyId: companyId,
                companyName: companyName,
                userId: userId,
                //isApproved: isApproved,
                fullname: fullname,
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


        return new RegisteredUser(itemFromServer.LastLoginTime, itemFromServer.Email, itemFromServer.AccountBalance, itemFromServer.Status, itemFromServer.CompanyId, itemFromServer.CompanyName, itemFromServer.Id, itemFromServer.fullname);
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