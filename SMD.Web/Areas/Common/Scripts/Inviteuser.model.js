define(["ko", "underscore", "underscore-ko"], function(ko) {

    var // ReSharper disable InconsistentNaming
      InviteUser = function (Email) {
          var
             
              Email = ko.observable(Email).extend({ required: { params: true, message: 'This field is required with minimum 6 characters!' }, minLength: 6 }),
           
              errors = ko.validation.group({
                  Email: Email,
                
              }),
              // Is Valid
              isValid = ko.computed(function () {
                  return errors().length === 0;
              }),
              dirtyFlag = new ko.dirtyFlag({
                 
                  Email: Email
             

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
                     
                      Email: Email()
                  
                  };
              };
          return {
              
              Email: Email
            
          };
      };

   
    
    // Function to attain cancel button functionality User
    User.CreateFromClientModel = function () {
        //todo
    };
   

    return {
        User: User
       
       
    };
});