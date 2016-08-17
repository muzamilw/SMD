define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
        DamGrid = function (SQID) {
           
            var
                //type and userID will be set on server sside
                SQID = ko.observable(SQID);
            return {
                SQID :(SQID)
            };
        };

   
    // Factory Method
    DamGrid.Create = function (source) {
        var DamGrid = new DamGrid(source.SqId);
        return DamGrid;
    };
   
    return {
        DamGrid: DamGrid
    };
});