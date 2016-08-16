define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

    //Section view Entity
    BranchCategory = function (specifiedSectionId, specifiedName, specifiedSectionOrder) {
        var
            self,
            categoryId = ko.observable(specifiedSectionId),
            name = ko.observable(specifiedName),
            brachFeilds = ko.observableArray([]),
            isExpanded = ko.observable(false),
            isEditMode = ko.observable(false),
            isVisibleDelete = ko.observable(true),
             convertToServerData = function () {
                 return {
                     BranchCategoryId: categoryId(),
                     BranchCategoryName: name(),
                 }


             };
        self = {
            categoryId: categoryId,
            name: name,
            convertToServerData:convertToServerData,
            brachFeilds: brachFeilds,
            isExpanded: isExpanded,
            isEditMode: isEditMode,
            isVisibleDelete: isVisibleDelete,
        };
        return self;
    };
    BranchField = function (specifiedSectionId, specifiedName, branchCategoryId) {
        var
            self,
            branchId = ko.observable(specifiedSectionId),
            branchTitle = ko.observable(specifiedName),
            categoryId = ko.observable(branchCategoryId),
        self = {
            branchId: branchId,
            branchTitle: branchTitle,
            categoryId: categoryId,

        };
        return self;
    };
    Branch = function (specifiedbranchId, specifiedbranchTitle, specifiedAddressline1, specifiedAddressline2, specifiedCity, specifiedState, specifiedZipcode, specifiedPhone, specifiedLocationLat, specifiedLocationLong, specifiedBranchCategoryId) {
        var
            self,
            branchId = ko.observable(specifiedbranchId),
            branchTitle = ko.observable(specifiedbranchTitle),
            branchAddressline1 = ko.observable(specifiedAddressline1),
            branchAddressline2 = ko.observable(specifiedAddressline2),
            branchCity = ko.observable(specifiedCity),
            branchState = ko.observable(specifiedState),
            branchZipCode = ko.observable(specifiedZipcode),
            branchPhone = ko.observable(specifiedPhone),
            branchLocationLat = ko.observable(specifiedLocationLat),
            branchLocationLon = ko.observable(specifiedLocationLong),
            branchCategoryId = ko.observable(specifiedBranchCategoryId),
            isDeleted = ko.observable(false),
            isBranchChecked = ko.observable(false),

             // Errors
            errors = ko.validation.group({
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),

            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                branchId: branchId,
                branchTitle: branchTitle,
                branchAddressline1: branchAddressline1,
                branchAddressline2: branchAddressline2,
                branchCity: branchCity,
                branchState: branchState,
                branchZipCode: branchZipCode,
                branchPhone: branchPhone,
                branchLocationLat: branchLocationLat,
                branchLocationLon: branchLocationLon,
                branchCategoryId: branchCategoryId,
                isDeleted: isDeleted,
            }),
             //Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),

            //Convert To Server
            convertToServerData = function () {
                return {
                    BranchId: branchId(),
                    BranchTitle: branchTitle(),
                    BranchAddressLine1: branchAddressline1(),
                    BranchAddressLine2: branchAddressline2(),
                    BranchCity: branchCity(),
                    BranchState: branchState(),
                    BranchZipCode: branchZipCode(),
                    BranchPhone: branchPhone(),
                    BranchLocationLat: branchLocationLat(),
                    BranchLocationLong: branchLocationLon(),
                    BranchCategoryId: branchCategoryId(),
                    IsDeletedBranch: isDeleted(),

                }


            };
        self = {
            branchId: branchId,
            branchTitle: branchTitle,
            branchAddressline1: branchAddressline1,
            branchAddressline2: branchAddressline2,
            branchCity: branchCity,
            branchState: branchState,
            branchZipCode: branchZipCode,
            branchPhone: branchPhone,
            branchLocationLat: branchLocationLat,
            branchLocationLon: branchLocationLon,
            branchCategoryId: branchCategoryId,


            //  fieldId: fieldId,
            isDeleted: isDeleted,
            convertToServerData: convertToServerData,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            isBranchChecked: isBranchChecked,
        };
        return self;
    };


    BranchModel = function () {
        var
            self,
               //Convert To Server

        Branch = ko.observable(),
        BranchCategoryI = ko.observable()

        self = {
            Branch: Branch,
            BranchCategoryId: BranchCategoryId
        };
        return self;
    };
    BranchCategory.Create = function (source) {

        return new BranchCategory(source.BranchCategoryId, source.BranchCategoryName);
    }
    BranchField.Create = function (source) {

        return new BranchField(source.BranchId, source.BranchTitle, source.BranchCategoryId);
    }
    Branch.Create = function (source) {
        return new Branch(source.BranchId, source.BranchTitle, source.BranchAddressLine1, source.BranchAddressLine2, source.BranchCity, source.BranchState, source.BranchZipCode, source.BranchPhone, source.BranchLocationLat, source.BranchLocationLong, source.BranchCategoryId);
    }


    return {
        BranchCategory: BranchCategory,
        BranchField: BranchField,
        Branch: Branch,
        BranchModel: BranchModel
    };
});