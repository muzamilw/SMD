define(["ko", "underscore", "underscore-ko"], function(ko) {

    var // ReSharper disable InconsistentNaming
      User = function (specifiedId, specifiedFullName, specifiedAddress1, specifiedCmpname, specifiedEmail,
          specifiedJTitle, specifiedTimeZone, specifiedGender, specifiedAddress2, specifiedAge, specifiedCityId,
          specifiedContNotes, specifiedCountryId,indsId, specifiedPhn1, specifiedPhn2, specifiedState, specifiedZip, specifiedImg,
          advertisingContact, advertisingEmail, advertisingPhone, spcEduId, spcStripe, spcPayPal, spcGoogle, ProfileImageBytes, CompanyId) {
          var
              id = ko.observable(specifiedId),
              fullName = ko.observable(specifiedFullName),
              address1 = ko.observable(specifiedAddress1),
              companyName = ko.observable(specifiedCmpname),
              email = ko.observable(specifiedEmail),

              jobTitle = ko.observable(specifiedJTitle),
              userTimeZone = ko.observable(specifiedTimeZone),
              gender = ko.observable(specifiedGender),
              address2 = ko.observable(specifiedAddress2),

              dob = ko.observable(specifiedAge ? moment(specifiedAge).toDate() : undefined),
              cityId = ko.observable(specifiedCityId),
              contactNotes = ko.observable(specifiedContNotes),
              countryId = ko.observable(specifiedCountryId),

              industeryId = ko.observable(indsId),

              phone1 = ko.observable(specifiedPhn1),
              phone2 = ko.observable(specifiedPhn2),
              state = ko.observable(specifiedState),
              zipCode = ko.observable(specifiedZip),

              imageUrl = ko.observable(specifiedImg),
              ProfileImageBytes = ko.observable(ProfileImageBytes),
              advert = ko.observable(advertisingContact),
              advertEmail = ko.observable(advertisingEmail),
              advertPhone = ko.observable(advertisingPhone),
              educationId = ko.observable(spcEduId),
              CompanyId = ko.observable(CompanyId),
              stripeId = ko.observable(spcStripe || 'undefined'),
              payPalId = ko.observable(spcPayPal || 'undefined'),
              googleValletId = ko.observable(spcGoogle || 'undefined'),

              errors = ko.validation.group({
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
                  contactNotes: contactNotes,
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
                  educationId:educationId

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
                      ContactNotes: contactNotes(),
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
                      CompanyId: CompanyId
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
              contactNotes: contactNotes,
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

              hasChanges: hasChanges,
              convertToServerData:convertToServerData,
              reset: reset,
              isValid: isValid,
              errors: errors,
              ProfileImageBytes: ProfileImageBytes
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
            itemFromServer.EducationId, itemFromServer.StripeId, itemFromServer.PayPal, itemFromServer.GoogleVallet, null,itemFromServer.CompanyId);
     
    };
    
    // Function to attain cancel button functionality User
    User.CreateFromClientModel = function () {
        //todo
    };
   
    var ManageUsers = function (source) {
        var self
        if (source != undefined) {
            Name = ko.observable(source.Name),
            Role = ko.observable(source.Role)
           

        } else {

            Name = ko.observable(),
           Role = ko.observable()
        
        }


        errors = ko.validation.group({
        }),
        // Is Valid
       isValid = ko.computed(function () {
           return errors().length === 0;
       }),
       dirtyFlag = new ko.dirtyFlag({
       }),
        // Has Changes
       hasChanges = ko.computed(function () {
           return dirtyFlag.isDirty();
       }),
        // Reset
       reset = function () {
           dirtyFlag.reset();
       };

        self = {
            Name: Name(),
            Role: Role(),
          
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,

        };
        return self;

    }
    return {
        User: User,
        UserServertoClientMapper: UserServertoClientMapper,
        ManageUsers: ManageUsers
    };
});