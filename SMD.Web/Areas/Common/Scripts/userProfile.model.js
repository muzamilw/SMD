define(["ko", "underscore", "underscore-ko"], function(ko) {

    var // ReSharper disable InconsistentNaming
      User = function (specifiedId, specifiedFullName, specifiedAddress1, specifiedCmpname, specifiedEmail,
          specifiedJTitle, specifiedTimeZone, specifiedGender, specifiedAddress2, specifiedAge, specifiedCityId,
          ContactNotes, specifiedCountryId, indsId, specifiedPhn1, specifiedPhn2, specifiedState, specifiedZip, specifiedImg,
          advertisingContact, advertisingEmail, advertisingPhone, spcEduId, spcStripe, spcPayPal, spcGoogle
          , ProfileImageBytes, CompanyId, RoleId, Password, VoucherSecretKey) {
          var
              id = ko.observable(specifiedId),
              fullName = ko.observable(specifiedFullName).extend({ required: true }),
              address1 = ko.observable(specifiedAddress1),
              companyName = ko.observable(specifiedCmpname),
              email = ko.observable(specifiedEmail),

              jobTitle = ko.observable(specifiedJTitle).extend({ required: true }),
              userTimeZone = ko.observable(specifiedTimeZone),
              gender = ko.observable(specifiedGender),
              address2 = ko.observable(specifiedAddress2),

              dob = ko.observable(specifiedAge ? moment(specifiedAge).toDate() : undefined).extend({ required: true }),
              cityId = ko.observable(specifiedCityId),
              ContactNotes = ko.observable(ContactNotes),
              countryId = ko.observable(specifiedCountryId),

              industeryId = ko.observable(indsId).extend({ required: true }),

              phone1 = ko.observable(specifiedPhn1).extend({ required: true }),
              phone2 = ko.observable(specifiedPhn2),
              state = ko.observable(specifiedState),
              zipCode = ko.observable(specifiedZip),

              imageUrl = ko.observable(specifiedImg),
              ProfileImageBytes = ko.observable(ProfileImageBytes),
              advert = ko.observable(advertisingContact),
              advertEmail = ko.observable(advertisingEmail),
              advertPhone = ko.observable(advertisingPhone),
              educationId = ko.observable(spcEduId).extend({ required: true }),
              CompanyId = ko.observable(CompanyId),
              stripeId = ko.observable(spcStripe || 'undefined'),
              payPalId = ko.observable(spcPayPal || 'undefined'),
              googleValletId = ko.observable(spcGoogle || 'undefined'),
              RoleId = ko.observable(RoleId),
              VoucherSecretKey = ko.observable(VoucherSecretKey),
              Password = ko.observable(Password).extend({ required: { params: true, message: 'This field is required with minimum 6 characters!' }, minLength: 6 }),
              ConfirmPassword = ko.observable(Password).extend({ compareWith: Password }),
              errors = ko.validation.group({
                  Password: Password,
                  ConfirmPassword: ConfirmPassword,
                  fullName: fullName,
                  dob: dob,
                  industeryId: industeryId,
                  educationId: educationId
              }),
              // Is Valid
              isValid = ko.computed(function () {
                  return errors().length === 0;
              }),
              dirtyFlag = new ko.dirtyFlag({
                  fullName: fullName,
                  address1: address1,
                  companyName: companyName,
                  email: email,
                  jobTitle: jobTitle,
                  userTimeZone: userTimeZone,
                  gender: gender,
                  address2: address2,
                  dob: dob,
                  cityId: cityId,
                  ContactNotes: ContactNotes,
                  countryId: countryId,
                  industeryId:industeryId,
                  phone1: phone1,
                  phone2: phone2,
                  state: state,
                  zipCode: zipCode,
                  imageUrl: imageUrl,
                  advert: advert,
                  advertEmail: advertEmail,
                  advertPhone: advertPhone,
                  educationId: educationId,
                  RoleId: RoleId,
                  Password: Password,
                  ConfirmPassword: ConfirmPassword,
                  VoucherSecretKey: VoucherSecretKey

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
                  debugger;
                  return {
                      UserId: id(),
                      FullName: fullName(),
                      Address1: address1(),
                      CompanyName: companyName(),
                      Email: email(),
                      JobTitle: jobTitle(),
                      TimeZone: userTimeZone(),
                      Gender: gender(),
                      Address2: address2(),
                      DOB: dob() ? moment(dob()).format(ist.utcFormat) + 'Z' :  undefined,
                      CityId: cityId(),
                      ContactNotes: ContactNotes(),
                      CountryId: countryId(),
                      IndustryId: industeryId(),
                      Phone1: phone1(),
                      Phone2: phone2(),
                      State: state(),
                      ZipCode: zipCode(),
                      ProfileImage: imageUrl(),
                      AdvertContact: advert(),
                      AdvertContactEmail: advertEmail(),
                      AdvertContactPhone: advertPhone(),
                      EducationId: educationId(),
                      PayPal: payPalId(),
                      ProfileImageBytesString: ProfileImageBytes(),
                      CompanyId: CompanyId,
                      RoleId: RoleId(),
                      Password: Password(),
                      VoucherSecretKey: VoucherSecretKey()
                  };
              };
          return {
              id:id,
              fullName: fullName,
              address1: address1,
              companyName: companyName,
              email: email,
              jobTitle: jobTitle,
              userTimeZone: userTimeZone,
              gender: gender,
              address2: address2,
              dob: dob,
              cityId: cityId,
              ContactNotes: ContactNotes,
              countryId: countryId,
              industeryId: industeryId,
              phone1:phone1,
              phone2: phone2,
              state: state,
              zipCode: zipCode,
              imageUrl: imageUrl,

              advert: advert,
              advertEmail: advertEmail,
              advertPhone: advertPhone,
              educationId:educationId,
            
              stripeId :stripeId,
              payPalId :payPalId,
              googleValletId: googleValletId,
              RoleId: RoleId,
              Password: Password,
              ConfirmPassword: ConfirmPassword,
              hasChanges: hasChanges,
              convertToServerData:convertToServerData,
              reset: reset,
              isValid: isValid,
              errors: errors,
              ProfileImageBytes: ProfileImageBytes,
              VoucherSecretKey: VoucherSecretKey
          };
      };

    ////=================================== User
    //Server to Client mapper For User
    var UserServertoClientMapper = function (itemFromServer) {
        return new User(itemFromServer.UserId,itemFromServer.FullName,itemFromServer.Address1,itemFromServer.CompanyName,
            itemFromServer.Email,itemFromServer.JobTitle,itemFromServer.UserTimeZone,itemFromServer.Gender,
            itemFromServer.Address2, itemFromServer.DOB, itemFromServer.CityId, itemFromServer.ContactNotes, itemFromServer.CountryId,
            itemFromServer.IndustryId,itemFromServer.Phone1,itemFromServer.Phone2,itemFromServer.State,itemFromServer.ZipCode,
            itemFromServer.ImageUrl ,itemFromServer.AdvertContact,itemFromServer.AdvertContactEmail,itemFromServer.AdvertContactPhone,
            itemFromServer.EducationId, itemFromServer.StripeId, itemFromServer.PayPal, itemFromServer.GoogleVallet, null, itemFromServer.CompanyId, itemFromServer.RoleId, itemFromServer.Password, itemFromServer.VoucherSecretKey);
     
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