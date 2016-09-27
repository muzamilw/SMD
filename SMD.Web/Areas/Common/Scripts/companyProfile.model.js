define(["ko", "underscore", "underscore-ko"], function(ko) {

    var // ReSharper disable InconsistentNaming
      User = function (CompanyId, CompanyName, Tel1, Tel2, Logo,
          StripeCustomerId, SalesEmail, WebsiteLink, VoucherSecretKey, BillingAddressLine1, BillingAddressLine2,
          BillingState, BillingCountryId, BillingCity, BillingZipCode, BillingPhone, BillingEmail, TwitterHandle, FacebookHandle,
          InstagramHandle, PinterestHandle, Logo, LogoImageBase64, specifiedSolutation, specifiedProfession, specifiedfullName, specifiedEmail, specifiedMobile,
            specifiedDob, specifiedPassport, specifiedIsDeals, specifiedIsWeeklyUpdate, specifiedIsLatestService, specifiedBranchName, specifiedBranchCount,
            specifiedSalesPhone, specifiedCompanyType, specifiedAboutUs, specifiedPaypalId, specifiedBusinessName, specifiedRegNumber, specifiedStartDate,
            specifiedVatNumber) {
          debugger;
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
                billingCity = ko.observable(BillingCity).extend({ required: true }),
                BillingZipCode = ko.observable(BillingZipCode).extend({ required: true }),
                BillingPhone = ko.observable(BillingPhone).extend({ required: true }),
                BillingEmail = ko.observable(BillingEmail).extend({ required: true }),
                mediaHandleui = ko.observable(TwitterHandle != null ? TwitterHandle : FacebookHandle != null ? FacebookHandle : InstagramHandle != null ? InstagramHandle : PinterestHandle != null ? PinterestHandle : ""),
                TwitterHandle = ko.observable(TwitterHandle),
                FacebookHandle = ko.observable(FacebookHandle),
                InstagramHandle = ko.observable(InstagramHandle),
                PinterestHandle = ko.observable(PinterestHandle),
                selectedMedia = ko.observable().extend({ required: true }),
                Logo = ko.observable(Logo),
                LogoImageBase64 = ko.observable(LogoImageBase64),
              
                
              
              //Fields for User profil-------------
              solutation = ko.observable(specifiedSolutation).extend({ required: true, message:"Solutation is required" }),
              professsion = ko.observable(specifiedProfession).extend({ required: true, message: "Profession is required" }),
              fullName = ko.observable(specifiedfullName).extend({ required: true, message: "Name is required" }),
              email = ko.observable(specifiedEmail).extend({ required: true, message: "Email is required" }),
              mobileNumber = ko.observable(specifiedMobile).extend({ required: true, message:"Mobile number is required" }),
              dateOfBirth = ko.observable(specifiedDob).extend({ required: true, message: "Date of birth is required" }),
              passportNumber = ko.observable(specifiedPassport).extend({ required: true, message: "Valid Passport number is required" }),
              isReceiveDeals = ko.observable(specifiedIsDeals),
              isReceiveWeeklyUpdates = ko.observable(specifiedIsWeeklyUpdate),
              isReceiveLatestServices = ko.observable(specifiedIsLatestService),
              //----------------
              //Fields for User Branch-------------
              branchName = ko.observable(specifiedBranchName),
              numberOfBranches = ko.observable(specifiedBranchCount),
              salesPhone = ko.observable(specifiedSalesPhone),
              companyType = ko.observable(specifiedCompanyType),
              aboutUs = ko.observable(specifiedAboutUs),
              
              //---------------------------
              //Money Tab Fields
              paypalMailId = ko.observable(specifiedPaypalId),
              billingBusinessName = ko.observable(specifiedBusinessName),
              companyRegNo = ko.observable(specifiedRegNumber),
              businessStartDate = ko.observable(specifiedStartDate),
              creditCard = ko.observable(StripeCustomerId),
              vatNumber = ko.observable(specifiedVatNumber),
              creditCardUi = ko.computed(function () {
                  return creditCard() ? "Linked | " + creditCard() : "Not Linked | Link with Stripe Id";
                  }),
              
              //-------------------
                
                //FacebookHandleui = ko.computed({
                //    read: function () {                        
                //        return FacebookHandle());
                //    },
                //    write: function (value) {
                //        FacebookHandle(value);
                //        TwitterHandle(undefined);
                //        InstagramHandle(undefined);
                //        PinterestHandle(undefined);
                //    }
                //}),
                //ActiveHandle = ko.computed(function () {
                //    return dirtyFlag.isDirty();
                //}),
              
              errors = ko.validation.group({
                  CompanyName: CompanyName,
                  Tel1: Tel1,
                  SalesEmail: SalesEmail,
                  VoucherSecretKey: VoucherSecretKey,
                  BillingAddressLine1: BillingAddressLine1,
                  
                  BillingState: BillingState,
                  BillingCountryId: BillingCountryId,
                  billingCity: billingCity,
                  BillingZipCode: BillingZipCode,
                  BillingPhone: BillingPhone,
                  BillingEmail: BillingEmail,
                  selectedMedia: selectedMedia,
                  
                  solutation: solutation,
                  fullName: fullName,
                  email: email,
                  mobileNumber: mobileNumber,
                  dateOfBirth: dateOfBirth,
                  passportNumber: passportNumber,
                  numberOfBranches: numberOfBranches,
                  salesPhone: salesPhone,
                  companyType: companyType,
                  aboutUs: aboutUs,
                  paypalMailId: paypalMailId,
                  billingBusinessName: billingBusinessName,
                  companyRegNo: companyRegNo,
                  businessStartDate: businessStartDate,
                  vatNumber: vatNumber,
                  professsion: professsion,
                  creditCard: creditCard

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
                  billingCity: billingCity,
                  BillingZipCode: BillingZipCode,
                  BillingPhone: BillingPhone,
                  BillingEmail: BillingEmail,
                  TwitterHandle: TwitterHandle,
                  FacebookHandle: FacebookHandle,
                  InstagramHandle: InstagramHandle,
                  PinterestHandle: PinterestHandle,
                  LogoImageBase64: LogoImageBase64,
                  mediaHandleui: mediaHandleui,
                  selectedMedia: selectedMedia,
                  solutation: solutation,
                  fullName: fullName,
                  email: email,
                  mobileNumber: mobileNumber,
                  dateOfBirth: dateOfBirth,
                  passportNumber: passportNumber,
                  isReceiveDeals: isReceiveDeals,
                  isReceiveWeeklyUpdates: isReceiveWeeklyUpdates,
                  isReceiveLatestServices: isReceiveLatestServices,
                  branchName: branchName,
                  numberOfBranches: numberOfBranches,
                  salesPhone: salesPhone,
                  companyType: companyType,
                  aboutUs: aboutUs,
                  paypalMailId: paypalMailId,
                  billingBusinessName: billingBusinessName,
                  companyRegNo: companyRegNo,
                  businessStartDate: businessStartDate,
                  vatNumber: vatNumber,
                  professsion: professsion,
                  creditCard: creditCard

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
                  
                  Logo: logoImage == "" ? Logo() : logoImage,

                  StripeCustomerId: StripeCustomerId(),
                  SalesEmail: SalesEmail(),
                  WebsiteLink: WebsiteLink(),
                  VoucherSecretKey: VoucherSecretKey(),
                  BillingAddressLine1: BillingAddressLine1(),
                  BillingAddressLine2: BillingAddressLine2(),
                  BillingState: BillingState(),
                  BillingCountryId: BillingCountryId(),
                  BillingCity: billingCity(),
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
              billingCity: billingCity,
              BillingZipCode: BillingZipCode,
              BillingPhone: BillingPhone,
              BillingEmail: BillingEmail,
              TwitterHandle: TwitterHandle,
              FacebookHandle: FacebookHandle,
              InstagramHandle: InstagramHandle,
              PinterestHandle: PinterestHandle,
              LogoImageBase64: LogoImageBase64,
              mediaHandleui: mediaHandleui,
              selectedMedia:selectedMedia,
              //FacebookHandleui:FacebookHandleui,
              convertToServerData:convertToServerData,
              hasChanges: hasChanges,
             
              reset: reset,
              isValid: isValid,
              
              errors: errors,
              solutation: solutation,
              fullName: fullName,
              email: email,
              mobileNumber: mobileNumber,
              dateOfBirth: dateOfBirth,
              passportNumber: passportNumber,
              isReceiveDeals: isReceiveDeals,
              isReceiveWeeklyUpdates: isReceiveWeeklyUpdates,
              isReceiveLatestServices: isReceiveLatestServices,
              branchName: branchName,
              numberOfBranches: numberOfBranches,
              salesPhone: salesPhone,
              companyType: companyType,
              aboutUs: aboutUs,
              paypalMailId: paypalMailId,
              billingBusinessName: billingBusinessName,
              companyRegNo: companyRegNo,
              businessStartDate: businessStartDate,
              vatNumber: vatNumber,
              creditCardUi: creditCardUi,
              professsion: professsion,
              creditCard: creditCard
            
          };
      };

    ////=================================== User
    //Server to Client mapper For User
    var CompanyServertoClientMapper = function (objSrv) {
        debugger;
        return new User(objSrv.CompanyId, objSrv.CompanyName, objSrv.Tel1, objSrv.Tel2, objSrv.Logo,
          objSrv.StripeCustomerId, objSrv.SalesEmail, objSrv.WebsiteLink, objSrv.VoucherSecretKey, objSrv.BillingAddressLine1, objSrv.BillingAddressLine2,
          objSrv.BillingState, objSrv.BillingCountryId, objSrv.BillingCity, objSrv.BillingZipCode, objSrv.BillingPhone, objSrv.BillingEmail, objSrv.TwitterHandle, objSrv.FacebookHandle,
          objSrv.InstagramHandle, objSrv.PinterestHandle, objSrv.Logo, objSrv.LogoImageBase64, objSrv.Solutation, objSrv.Profession, objSrv.FirstName, objSrv.Email, objSrv.Mobile, objSrv.DateOfBirth
       , objSrv.PassportNumber, objSrv.IsReceiveDeals, objSrv.IsReceiveWeeklyUpdates, objSrv.IsReceiveLatestServices, objSrv.BranchName, objSrv.BranchesCount, objSrv.SalesPhone, objSrv.CompanyType
        , objSrv.AboutUs, objSrv.PayPalId, objSrv.BillingBusinessName, objSrv.CompanyRegistrationNo, objSrv.BusinessStartDate, objSrv.VatNumber);
     
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