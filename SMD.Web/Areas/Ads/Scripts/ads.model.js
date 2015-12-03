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
                // Convert to server data
                convertToServerData = function () {
                    return {
                       
                    };
                };
            return {
               
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