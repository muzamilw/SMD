/*
    Module with the view model for Confirmation
*/
define("common/confirmation.viewModel",
    ["jquery", "amplify", "ko"], function ($, amplify, ko) {
        var ist = window.ist || {};
        ist.confirmation = {
            viewModel: (function () {
                var// The view 
                    view,
                    // True if we are loading data
                    isLoading = ko.observable(false),
                    // default Header text
                    defaultHeaderText = ist.resourceText.defaultHeaderText,
                    // Heading Text
                    headingText = ko.observable(defaultHeaderText),
                    // default confirmation text
                    defaultConfirmationText = "Do you want to proceed with the request?",
                    // Message Text

                    messageText = ko.observable(defaultConfirmationText),


                    defaultButtonTextYes = "Yes",
                    yesBtnText = ko.observable(defaultButtonTextYes),

                    defaultButtonTextNo = "No",

                    noBtnText = ko.observable(defaultButtonTextNo),

                     defaultDealButtonTextYes = "Yes",
                    yesdealBtnText = ko.observable(defaultDealButtonTextYes),

                    defaultDealButtonTextNo = "No",

                    nodealBtnText = ko.observable(defaultDealButtonTextNo),

                    defaultIsCancelVisible = true;

                IsCancelVisible = ko.observable(defaultIsCancelVisible),
                // On Proceed
                 afterProceed = ko.observable(),
                 afterProceedDeal = ko.observable(),
                // On Proceed
                 afterActionProceed = ko.observable(),

                // On Cancel
                 afterCancel = ko.observable(),
                afterCancelDeal  = ko.observable(),
                // On No
                 afterNo = ko.observable(),
                // Is Proceed Visible
                 isProceedVisible = ko.observable(true),


                // Comments for logs
                 comment = ko.observable(),

                 UserRandomNum = ko.observable(),

                 RandomNumber = ko.observable(),
                //errors = ko.validation.group({
                //    comment: comment,
                //    UserRandomNum: UserRandomNum
                //}),
                // Proceed with the request
                 proceed = function () {
                     if (typeof afterProceed() === "function") {
                         afterProceed()();
                     }
                     hide();

                 },
                proceedDeal = function () {
                    if (typeof afterProceedDeal() === "function") {
                        afterProceedDeal()();
                    }
                    hideDeal();

                },
                // Proceed with the request
                 proceedAction = function () {

                     if (comment() == "" || comment() == undefined) {
                         toastr.error("Please enter comment to submit!");
                         return false;
                     }
                     if (UserRandomNum() == "" || UserRandomNum() == undefined) {
                         toastr.error("Please enter number to submit!");
                         return false;
                     }

                     if (UserRandomNum() == RandomNumber()) {

                         if (typeof afterActionProceed() === "function") {
                             afterActionProceed()();
                         }
                         hideActionPopup();
                     }
                     else {
                         toastr.error("Number not match!");
                         return false;
                     }

                 },
                // Reset Dialog
                 resetDialog = function () {
                     afterCancel(undefined);
                     afterProceed(undefined);
                     afterCancelDeal(undefined);
                     afterProceedDeal(undefined);

                     afterNo(undefined);
                     isProceedVisible(true);
                     headingText(defaultHeaderText);
                     messageText(defaultConfirmationText);
                     yesBtnText(defaultButtonTextYes);
                     nodealBtnText(defaultDealButtonTextNo);
                     yesdealBtnText(defaultDealButtonTextYes);
                     noBtnText(defaultButtonTextNo);
                     IsCancelVisible(defaultIsCancelVisible);
                 },
                // Show the dialog
                 show = function () {
                     isLoading(true);
                     view.show();
                 },
                // Hide the dialog
                 hide = function () {
                     // Reset Call Backs
                     resetDialog();
                     view.hide();
                 },
                   showDeal = function () {
                       isLoading(true);
                       view.showDeal();
                   },
                // Hide the dialog
                 hideDeal = function () {
                     // Reset Call Backs
                     resetDialog();
                     view.hideDeal();
                 },


                 showWarningPopup = function () {
                     isLoading(true);
                     view.showWarningPopup();
                 },
               showOKpopup = function () {
                   isLoading(true);
                   view.showOKpopup();
               },
                showOKpopupforinfo = function () {
                    isLoading(true);
                    view.showOKpopupforinfo();
                },
              showAccountSetingPopup = function () {
                  isLoading(true);
                  view.showAccountSetingPopup();
              },
               showOKpopupforFreeCoupon = function () {
                   isLoading(true);
                   view.showOKpopupforFreeCoupon();
               },
                showOKpopupforMax3Deal = function () {
                    isLoading(true);
                    view.showOKpopupforMax3Deal();
                },
              showOKpopupfordealheadline = function () {
                  isLoading(true);
                  view.showOKpopupfordealheadline();
              },
              showOKpopupforChart = function () {
                  isLoading(true);
                  view.showOKpopupforchart();
              },
                 showUpgradePopup = function () {
                     isLoading(true);
                     view.showUpgradePopup();
                 },
                 showActionPopup = function () {
                     isLoading(true);
                     var num = Math.floor(Math.random() * 90000) + 10000;
                     RandomNumber(num);

                     view.showActionPopup();
                 },

                // Hide the dialog
                 hideWarningPopup = function () {
                     // Reset Call Backs
                     resetDialog();
                     view.hideWarningPopup();
                     view.hide();
                 },
                hideshowOKpopup = function () {
                    // Reset Call Backs
                    resetDialog();
                    view.hideshowOKpopup();
                    view.hide();
                },
                 hidesOKpopupforInfo = function () {
                     // Reset Call Backs
                     resetDialog();
                     view.hidesOKpopupforinfo();
                     view.hide();
                 },

                // Hide the dialog
                 hideActionPopup = function () {
                     // Reset Call Backs
                     resetDialog();
                     view.hideActionPopup();
                     view.hide();
                 },
                // Cancel 
                 cancel = function () {
                     if (typeof afterCancel() === "function") {
                         afterCancel()();
                     }
                     hide();
                 },
                 cancelDeal = function () {
                     if (typeof afterCancelDeal() === "function") {
                         afterCancelDeal()();
                     }
                     hideDeal();
                 },
                // Cancel 
                 Warningcancel = function () {
                     if (typeof afterCancel() === "function") {
                         afterCancel()();
                     }
                     hideWarningPopup();
                 },

                 ActionPopupCancel = function () {
                     if (typeof afterCancel() === "function") {
                         afterCancel()();
                     }
                     hideActionPopup();
                 },
                // No
                 no = function () {
                     if (typeof afterNo() === "function") {
                         afterNo()();
                     }
                     hide();
                 },
                // Initialize the view model
                 initialize = function (specifiedView) {
                     view = specifiedView;
                     ko.applyBindings(view.viewModel, view.bindingRoot);
                     ko.applyBindings(view.viewModel, view.bindingRootq);
                     ko.applyBindings(view.viewModel, view.bindingRootupgrade);
                     ko.applyBindings(view.viewModel, view.bindingRootaction);
                     var logo = $('#companyLogo').prop('src');
                     if ((logo == null || logo == "" || logo == undefined) && (UserRoleId == "EndUser_Admin"))
                         showAccountSetingPopup();

                 };

                return {
                    isLoading: isLoading,
                    headingText: headingText,
                    initialize: initialize,
                    show: show,
                    showDeal:showDeal,
                    cancel: cancel,
                    cancelDeal:cancelDeal,
                    Warningcancel: Warningcancel,
                    proceed: proceed,
                    proceedDeal:proceedDeal,
                    proceedAction: proceedAction,
                    no: no,
                afterProceed: afterProceed,
                afterProceedDeal:afterProceedDeal,
                afterActionProceed: afterActionProceed,
                afterCancel: afterCancel,
                afterCancelDeal:afterCancelDeal,
                afterNo: afterNo,
                isProceedVisible: isProceedVisible,
                resetDialog: resetDialog,
                messageText: messageText,
                yesBtnText: yesBtnText,
                yesdealBtnText: yesdealBtnText,
                nodealBtnText:nodealBtnText,
                noBtnText: noBtnText,
                IsCancelVisible: IsCancelVisible,
                hide: hide,
                hideDeal:hideDeal,
                showWarningPopup: showWarningPopup,
                showOKpopup: showOKpopup,
                hideshowOKpopup: hideshowOKpopup,
                hideWarningPopup: hideWarningPopup,
                showUpgradePopup: showUpgradePopup,
                showActionPopup: showActionPopup,
                hideActionPopup: hideActionPopup,
                comment: comment,
                ActionPopupCancel: ActionPopupCancel,
                //errors: errors,
                UserRandomNum: UserRandomNum,
                showOKpopupforinfo: showOKpopupforinfo,
                hidesOKpopupforInfo: hidesOKpopupforInfo,
                showOKpopupforChart: showOKpopupforChart,
                showOKpopupforFreeCoupon: showOKpopupforFreeCoupon,
                showOKpopupfordealheadline: showOKpopupfordealheadline,
                showAccountSetingPopup: showAccountSetingPopup,
                showOKpopupforMax3Deal: showOKpopupforMax3Deal
            };
    })()
};

return ist.confirmation.viewModel;
});

