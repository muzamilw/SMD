/*
    Module with the view model for the AdCampaign
*/
define("Emails/EmailPromotion.viewModel",
    ["jquery", "amplify", "ko", "Emails/EmailPromotion.dataservice", "Emails/EmailPromotion.model", "common/pagination",
     "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation) {
        var ist = window.ist || {};
        ist.EmailPromotion = {
            viewModel: (function () {
                var view,
                    //  AdCampaign list on LV
                    Emails = ko.observableArray([]),
                    //pager
                    pager = ko.observable(),

                    //sorting
                    sortOn = ko.observable(5),
                    isEditorVisible = ko.observable(false),
                    //Assending  / Desending
                    sortIsAsc = ko.observable(true),
                    // Controlls editor visibility 
                    addNewEmailPromotion = function()
                    {
                    
                    isEditorVisible(true);
                    
                    },

                    closeEditCampaign = function ()
                    {
                        isEditorVisible(false);

                    },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        //pager(pagination.Pagination({ PageSize: 10 }, campaigns, getCampaigns));


                    };
                return {
                    initialize: initialize,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    pager: pager,
                    Emails: Emails,
                    addNewEmailPromotion: addNewEmailPromotion,
                    isEditorVisible: isEditorVisible,
                    closeEditCampaign: closeEditCampaign,

                };
            })()
        };
        return ist.EmailPromotion.viewModel;
    });
