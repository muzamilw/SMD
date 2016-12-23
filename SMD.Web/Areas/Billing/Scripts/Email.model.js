define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
      Email = function (mailId, title, subject, fromName, fromEmail, emailTarget, body) {
          var
              MailId = ko.observable(mailId),
              Title = ko.observable(title),
              Subject = ko.observable(subject),
              FromName = ko.observable(fromName),
              FromEmail = ko.observable(fromEmail),
              EmailTarget = ko.observable(emailTarget),
              Body = ko.observable(body),

              errors = ko.validation.group({

              }),
              // Is Valid
              isValid = ko.computed(function () {
                  return errors().length === 0;
              }),
              dirtyFlag = new ko.dirtyFlag({
                  //rejectedReason: rejectedReason
              }),
              // Has Changes
              hasChanges = ko.computed(function () {
                  return dirtyFlag.isDirty();
              }),
              // Reset
              reset = function () {
                  dirtyFlag.reset();
              };

              // Convert to server data
          convertToServerData = function () {
                  return {
                      MailID: MailId(),
                      Title: Title(),
                      FromName: FromName(),
                      FromEmail: FromEmail(),
                      Subject: Subject(),
                      body: Body(),
                      EmailTarget: EmailTarget(),

                  };
              };
          return {

              MailId: MailId,
              Title: Title,
              Subject: Subject,
              FromName: FromName,
              FromEmail: FromEmail,
              EmailTarget: EmailTarget,
              Body:Body,
              hasChanges: hasChanges,
              convertToServerData: convertToServerData,
              reset: reset,
              isValid: isValid,
              errors: errors

          };
      };

    var EmailServertoClientMapper = function (itemFromServer) {


        return new Email(itemFromServer.MailId, itemFromServer.Title, itemFromServer.Subject, itemFromServer.FromName, itemFromServer.FromEmail, itemFromServer.EmailTarget, itemFromServer.Body);
    };

    // Function to attain cancel button functionality Email
    Email.CreateFromClientModel = function (item) {
        // To be Implemented
    };

    return {
        Email: Email,
        EmailServertoClientMapper: EmailServertoClientMapper

    };
});