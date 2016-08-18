define(["ko", "underscore", "underscore-ko"], function(ko) {

    var // ReSharper disable InconsistentNaming
      User = function (CompanyId, CompanyName, Tel1, Tel2, Logo,
          StripeCustomerId, SalesEmail, WebsiteLink, VoucherSecretKey, BillingAddressLine1, BillingAddressLine2,
          BillingState, BillingCountryId, BillingCityId, BillingZipCode, BillingPhone, BillingEmail, TwitterHandle, FacebookHandle,
          InstagramHandle, PinterestHandle, Logo, LogoImageBytes) {
          var

                CompanyId = ko.observable(CompanyId),
                CompanyName = ko.observable(CompanyName),
                Tel1 = ko.observable(Tel1),
                Tel2 = ko.observable(Tel2),
                Logo = ko.observable(Logo),
                StripeCustomerId = ko.observable(StripeCustomerId || 'undefined'),
                SalesEmail = ko.observable(SalesEmail),
                WebsiteLink = ko.observable(WebsiteLink),
                VoucherSecretKey = ko.observable(VoucherSecretKey),
                BillingAddressLine1 = ko.observable(BillingAddressLine1),
                BillingAddressLine2 = ko.observable(BillingAddressLine2),
                BillingState = ko.observable(BillingState),
                BillingCountryId = ko.observable(BillingCountryId),
                BillingCityId = ko.observable(BillingCityId),
                BillingZipCode = ko.observable(BillingZipCode),
                BillingPhone = ko.observable(BillingPhone),
                BillingEmail = ko.observable(BillingEmail),
                TwitterHandle = ko.observable(TwitterHandle),
                FacebookHandle = ko.observable(FacebookHandle),
                InstagramHandle = ko.observable(InstagramHandle),
                PinterestHandle = ko.observable(PinterestHandle),
                Logo = ko.observable(Logo),
                LogoImageBytes = ko.observable(LogoImageBytes),



              errors = ko.validation.group({
                  CompanyName: CompanyName
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
                  LogoImageBytes: LogoImageBytes

              }),
              // Has Changes
              hasChanges = ko.computed(function () {
                  return dirtyFlag.isDirty();
              }),
              // Reset
              reset = function () {
                  dirtyFlag.reset();
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
              LogoImageBytes: LogoImageBytes,


              hasChanges: hasChanges,
              convertToServerData:convertToServerData,
              reset: reset,
              isValid: isValid,
              errors: errors
            
          };
      };

    ////=================================== User
    //Server to Client mapper For User
    var UserServertoClientMapper = function (objSrv) {
        return new User(objSrv.CompanyId, objSrv.CompanyName, objSrv.Tel1, objSrv.Tel2, objSrv.Logo,
          objSrv.StripeCustomerId, objSrv.SalesEmail, objSrv.WebsiteLink, objSrv.VoucherSecretKey, objSrv.BillingAddressLine1, objSrv.BillingAddressLine2,
          objSrv.BillingState, objSrv.BillingCountryId, objSrv.BillingCityId, objSrv.BillingZipCode, objSrv.BillingPhone, objSrv.BillingEmail, objSrv.TwitterHandle, objSrv.FacebookHandle,
          objSrv.InstagramHandle, objSrv.PinterestHandle, objSrv.Logo, objSrv.LogoImageBytes);
     
    };
    
    // Function to attain cancel button functionality User
    User.CreateFromClientModel = function () {
        //todo
    };
   
    return {
        User: User,
        UserServertoClientMapper: UserServertoClientMapper,
      
    };
});