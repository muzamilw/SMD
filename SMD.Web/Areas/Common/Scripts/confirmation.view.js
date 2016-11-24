﻿/*
    View for the Confirmation. Used to keep the viewmodel clear of UI related logic
*/
define("common/confirmation.view",
    ["jquery", "common/confirmation.viewModel"], function ($) {

        var ist = window.ist || {};
        // View 
        ist.confirmation.view = (function (specifiedViewModel) {
            var // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#dialog-confirm")[0],
                // Binding root used with knockout
                bindingRootq = $("#dialog-ok")[0],
                 // Binding root used with knockout for upgrade dialog
                bindingRootupgrade = $("#dialog-okLicensing")[0],

                 // Binding root used with knockout for user actions log
                bindingRootaction = $("#dialog-okUserActionLog")[0],

                // Show the dialog
                show = function() {
                    $("#dialog-confirm").modal("show");
                },
                // Hide the dialog
                hide = function() {
                    $("#dialog-confirm").modal("hide");
                },
                showWarningPopup = function() {
                    $("#dialog-ok").modal("show");
                },
                 showOKpopup = function () {
                     $("#dialog-okWarning").modal("show");
                 },
                  hideshowOKpopup = function () {
                      $("#dialog-okWarning").modal("hide");
                  },
                   showOKpopupforinfo = function () {
                        $("#dialog-okWarningforinfo").modal("show");
                   },
                     showOKpopupforFreeCoupon = function () {
                         $("#dialog-okWarningforFreeCoupon").modal("show");
                     },
                      showOKpopupfordealheadline = function () {
                          $("#dialog-okWarningfordealheadline").modal("show");
                       },
                   showOKpopupforchart = function () {
                       $("#dialog-chartmessage").modal("show");
                   },
                    hidesOKpopupforinfo = function () {
                        $("#dialog-okWarningforinfo").modal("hide");
                    },
                // Hide the dialog
                hideWarningPopup = function() {
                    $("#dialog-ok").modal("hide");
                },
                showAccountSetingPopup = function () {
                    $("#AccountSetingConformation").modal("show");
                   },
                // Show Upgrade Plan dialog
                showUpgradePopup = function() {
                    $("#dialog-okLicensing").modal("show");
                };
                // Show Upgrade Plan dialog
                showActionPopup = function () {
                    $("#dialog-okUserActionLog").modal("show");
                };

           
                hideActionPopup = function () {
                    $("#dialog-okUserActionLog").modal("hide");
                };


            return {
                bindingRoot: bindingRoot,
                bindingRootq: bindingRootq,
                bindingRootupgrade: bindingRootupgrade,
                bindingRootaction: bindingRootaction,
                viewModel: viewModel,
                show: show,
                hide: hide,
                showWarningPopup: showWarningPopup,
                hideWarningPopup: hideWarningPopup,
                showUpgradePopup: showUpgradePopup,
                showActionPopup: showActionPopup,
                hideActionPopup: hideActionPopup,
                showOKpopup: showOKpopup,
                hideshowOKpopup: hideshowOKpopup,
                showOKpopupforinfo: showOKpopupforinfo,
                hidesOKpopupforinfo: hidesOKpopupforinfo,
                showOKpopupforchart: showOKpopupforchart,
                showOKpopupforFreeCoupon: showOKpopupforFreeCoupon,
                showOKpopupfordealheadline: showOKpopupfordealheadline,
                showAccountSetingPopup: showAccountSetingPopup
            };
        })(ist.confirmation.viewModel);

        // Initialize the view model
        if (ist.confirmation.view.bindingRoot) {
            ist.confirmation.viewModel.initialize(ist.confirmation.view);
        }
    });