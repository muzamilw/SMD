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
    Branch = function (specifiedbranchId, specifiedbranchTitle, specifiedAddressline1, specifiedAddressline2, specifiedCity, specifiedState, specifiedZipcode,spccountryId, specifiedPhone, specifiedLocationLat, specifiedLocationLong, specifiedBranchCategoryId) {
        var
            self,
            branchId = ko.observable(specifiedbranchId),
            branchTitle = ko.observable(specifiedbranchTitle).extend({ required: true}),
            branchAddressline1 = ko.observable(specifiedAddressline1).extend({ required: true}),
            branchAddressline2 = ko.observable(specifiedAddressline2),
            branchCity = ko.observable(specifiedCity).extend({ required: true }),
            branchState = ko.observable(specifiedState).extend({ required: true }),
            branchZipCode = ko.observable(specifiedZipcode),
            branchPhone = ko.observable(specifiedPhone).extend({ required: true}),
            countryId = ko.observable(spccountryId),
            branchLocationLat = ko.observable(specifiedLocationLat).extend({ required: true}),
            branchLocationLon = ko.observable(specifiedLocationLong).extend({ required: true}),
            branchCategoryId = ko.observable(specifiedBranchCategoryId).extend({ required: true }),
            isDeleted = ko.observable(false),
            isBranchChecked = ko.observable(false),

             // Errors
            errors = ko.validation.group({
                branchTitle: branchTitle,
                branchAddressline1: branchAddressline1,
                branchPhone: branchPhone,
                branchCity: branchCity,
                countryId:countryId,
                branchState: branchState,
                branchLocationLat: branchLocationLat,
                branchLocationLon: branchLocationLon,
                branchCategoryId: branchCategoryId,
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),
               showAllErrors = function () {
                   // Show Item Errors
                   errors.showAllMessages();
                   // Show Item Stock Option Errors
                   
               },

            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                branchId: branchId,
                branchTitle: branchTitle,
                branchAddressline1: branchAddressline1,
                branchAddressline2: branchAddressline2,
                branchCity: branchCity,
                branchState: branchState,
                branchZipCode: branchZipCode,
                countryId:countryId,
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
                    countryId:countryId(),
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
            countryId:countryId,
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
            showAllErrors:showAllErrors,
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
        return new Branch(source.BranchId, source.BranchTitle, source.BranchAddressLine1, source.BranchAddressLine2, source.BranchCity, source.BranchState, source.BranchZipCode,source.CountryId, source.BranchPhone, source.BranchLocationLat, source.BranchLocationLong, source.BranchCategoryId);
    }
    Branch.CreateBillingAddress = function (source) {
        return new Branch(null, null, source.BillingAddressLine1, source.BillingAddressLine2,'Lahore', source.BillingState, source.BillingZipCode, source.BillingCountryId, source.BillingPhone);
    }


    return {
        BranchCategory: BranchCategory,
        BranchField: BranchField,
        Branch: Branch,
        BranchModel: BranchModel
    };
});