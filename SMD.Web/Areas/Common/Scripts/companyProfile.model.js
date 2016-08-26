﻿define(["ko", "underscore", "underscore-ko"], function(ko) {

    var // ReSharper disable InconsistentNaming
      User = function (CompanyId, CompanyName, Tel1, Tel2, Logo,
          StripeCustomerId, SalesEmail, WebsiteLink, VoucherSecretKey, BillingAddressLine1, BillingAddressLine2,
          BillingState, BillingCountryId, BillingCityId, BillingZipCode, BillingPhone, BillingEmail, TwitterHandle, FacebookHandle,
          InstagramHandle, PinterestHandle, Logo, LogoImageBase64) {
          var

                CompanyId = ko.observable(CompanyId),
                CompanyName = ko.observable(CompanyName).extend({ required: true }),
                Tel1 = ko.observable(Tel1).extend({ required: true }),
                Tel2 = ko.observable(Tel2),
                Logo = ko.observable(Logo),
                StripeCustomerId = ko.observable(StripeCustomerId || 'undefined'),
                SalesEmail = ko.observable(SalesEmail).extend({ required: true }),
                WebsiteLink = ko.observable(WebsiteLink),
                VoucherSecretKey = ko.observable(VoucherSecretKey).extend({ required: true }),
                BillingAddressLine1 = ko.observable(BillingAddressLine1).extend({ required: true }),
                BillingAddressLine2 = ko.observable(BillingAddressLine2),
                BillingState = ko.observable(BillingState).extend({ required: true }),
                BillingCountryId = ko.observable(BillingCountryId).extend({ required: true }),
                BillingCityId = ko.observable(BillingCityId),//.extend({ required: true }),
                BillingZipCode = ko.observable(BillingZipCode).extend({ required: true }),
                BillingPhone = ko.observable(BillingPhone).extend({ required: true }),
                BillingEmail = ko.observable(BillingEmail).extend({ required: true }),
                TwitterHandle = ko.observable(TwitterHandle),
                FacebookHandle = ko.observable(FacebookHandle),
                InstagramHandle = ko.observable(InstagramHandle),
                PinterestHandle = ko.observable(PinterestHandle),
                Logo = ko.observable(Logo),
                LogoImageBase64 = ko.observable(LogoImageBase64),



              errors = ko.validation.group({
                  CompanyName: CompanyName,
                  Tel1: Tel1,
                  SalesEmail: SalesEmail,
                  VoucherSecretKey: VoucherSecretKey,
                  BillingAddressLine1: BillingAddressLine1,
                  
                  BillingState: BillingState,
                  BillingCountryId: BillingCountryId,
                  //BillingCityId: BillingCityId,
                  BillingZipCode: BillingZipCode,
                  BillingPhone: BillingPhone,
                  BillingEmail: BillingEmail

              }),
              // Is Valid
              isValid = ko.computed(function () {
                  return errors().length === 0;
              }),
              dirtyFlag = new ko.dirtyFlag({

                  CompanyName: CompanyName,
                  Tel1: Tel1,
                  Tel2: Tel2,
                  Logo: Logo,
                  StripeCustomerId: StripeCustomerId,
                  SalesEmail: SalesEmail,
                  WebsiteLink: WebsiteLink,
                  VoucherSecretKey: VoucherSecretKey,
                  BillingAddressLine1: BillingAddressLine1,
                  BillingAddressLine2: BillingAddressLine2,
                  BillingState: BillingState,
                  BillingCountryId: BillingCountryId,
                  BillingCityId: BillingCityId,
                  BillingZipCode: BillingZipCode,
                  BillingPhone: BillingPhone,
                  BillingEmail: BillingEmail,
                  TwitterHandle: TwitterHandle,
                  FacebookHandle: FacebookHandle,
                  InstagramHandle: InstagramHandle,
                  PinterestHandle: PinterestHandle,
                  LogoImageBase64: LogoImageBase64

              }),
              // Has Changes
              hasChanges = ko.computed(function () {
                  return dirtyFlag.isDirty();
              }),
              // Reset
              reset = function () {
                  dirtyFlag.reset();
              },
          convertToServerData = function () {
              
              return {
                  CompanyId:CompanyId(),
                  CompanyName: CompanyName(),
                  Tel1: Tel1(),
                  Tel2: Tel2(),
                  Logo: Logo(),
                  StripeCustomerId: StripeCustomerId(),
                  SalesEmail: SalesEmail(),
                  WebsiteLink: WebsiteLink(),
                  VoucherSecretKey: VoucherSecretKey(),
                  BillingAddressLine1: BillingAddressLine1(),
                  BillingAddressLine2: BillingAddressLine2(),
                  BillingState: BillingState(),
                  BillingCountryId: BillingCountryId(),
                  BillingCityId: BillingCityId(),
                  BillingZipCode: BillingZipCode(),
                  BillingPhone: BillingPhone(),
                  BillingEmail: BillingEmail(),
                  TwitterHandle: TwitterHandle(),
                  FacebookHandle: FacebookHandle(),
                  InstagramHandle: InstagramHandle(),
                  PinterestHandle: PinterestHandle(),
                  LogoImageBase64: LogoImageBase64()
              };
          };


          return {
              CompanyId:CompanyId,
              CompanyName: CompanyName,
              Tel1: Tel1,
              Tel2: Tel2,
              Logo: Logo,
              StripeCustomerId: StripeCustomerId,
              SalesEmail: SalesEmail,
              WebsiteLink: WebsiteLink,
              VoucherSecretKey: VoucherSecretKey,
              BillingAddressLine1: BillingAddressLine1,
              BillingAddressLine2: BillingAddressLine2,
              BillingState: BillingState,
              BillingCountryId: BillingCountryId,
              BillingCityId: BillingCityId,
              BillingZipCode: BillingZipCode,
              BillingPhone: BillingPhone,
              BillingEmail: BillingEmail,
              TwitterHandle: TwitterHandle,
              FacebookHandle: FacebookHandle,
              InstagramHandle: InstagramHandle,
              PinterestHandle: PinterestHandle,
              LogoImageBase64: LogoImageBase64,

              convertToServerData:convertToServerData,
              hasChanges: hasChanges,
             
              reset: reset,
              isValid: isValid,
              errors: errors
            
          };
      };

    ////=================================== User
    //Server to Client mapper For User
    var CompanyServertoClientMapper = function (objSrv) {
        return new User(objSrv.CompanyId, objSrv.CompanyName, objSrv.Tel1, objSrv.Tel2, objSrv.Logo,
          objSrv.StripeCustomerId, objSrv.SalesEmail, objSrv.WebsiteLink, objSrv.VoucherSecretKey, objSrv.BillingAddressLine1, objSrv.BillingAddressLine2,
          objSrv.BillingState, objSrv.BillingCountryId, objSrv.BillingCityId, objSrv.BillingZipCode, objSrv.BillingPhone, objSrv.BillingEmail, objSrv.TwitterHandle, objSrv.FacebookHandle,
          objSrv.InstagramHandle, objSrv.PinterestHandle, objSrv.Logo, objSrv.LogoImageBase64);
     
    };
    
    // Function to attain cancel button functionality User
    User.CreateFromClientModel = function () {
        //todo
    };
   
    return {
        User: User,
        CompanyServertoClientMapper: CompanyServertoClientMapper,
      
    };
});