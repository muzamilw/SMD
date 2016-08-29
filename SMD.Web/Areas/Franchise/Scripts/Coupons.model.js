define(["ko", "underscore", "underscore-ko"], function (ko) {

	var // ReSharper disable InconsistentNaming
      Coupons = function (coSubBy, coTitle, coCategory, coSwabCost,coSubDate) {
      	var
            submittedBy = ko.observable(coSubBy),
            title = ko.observable(coTitle),
            category = ko.observable(coCategory),
            swabCost = ko.observable(coSwabCost),
			submissionDate = ko.observable(coSubDate),
			

			errors = ko.validation.group({

			}),
			// Is Valid
			isValid = ko.computed(function () {
				return errors().length === 0;
			}),
			dirtyFlag = new ko.dirtyFlag({
				rejectionReason: rejectionReason
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
					CampaignId: id(),
					Approved: isApproved(),
					RejectedReason: rejectionReason(),
				};
			};
      	return {
 
      	    submittedBy: submittedBy,
      	    title: title,
      	    category: category,
      	    swabCost:swabCost,
      	    submissionDate: submissionDate,
      		hasChanges: hasChanges,
      		convertToServerData: convertToServerData,
      		reset: reset,
      		isValid: isValid,
      		errors: errors,
      		

      	};
      };


	///////////////////////////////////////////////////////// Ad-Campaign
	//server to client mapper For AdCampaign
	var CouponsServertoClientMapper = function (itemFromServer) {
	

	    return new Coupons(itemFromServer.CreatedBy, itemFromServer.CouponTitle, itemFromServer.CreatedBy,
            itemFromServer.SwapCost, itemFromServer.SubmissionDateTime);
	};

	// Function to attain cancel button functionality Coupons
	Coupons.CreateFromClientModel = function (item) {
		// To be Implemented
	};

	return {
		Coupons: Coupons,
		CouponsServertoClientMapper: CouponsServertoClientMapper
	};
});