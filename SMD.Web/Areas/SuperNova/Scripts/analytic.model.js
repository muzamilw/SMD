define(["ko", "underscore", "underscore-ko"], function (ko) {

var GetActiveUser = function (data) {
    var tempData = new Array();
    tempData[1] = { y: 'Last 1 day', a: data.Last1DayActiveUser };
    tempData[2] = { y: 'Last 7 days', a: data.Last7DayActiveUser };
    tempData[3] = { y: 'Last 14 days', a: data.Last14DayActiveUser };
    tempData[4] = { y: 'Last 30 days', a: data.Last30DayActiveUser };
    tempData[5] = { y: 'Last 3 months', a: data.Last3MonthsActiveUser };
   
    
    return tempData;
};

var DashboardInsightsModel = function (ActivityName) {
	if(ActivityName.length>0){
		activity = ko.observable(ActivityName), 
	 usL = ko.observable(0), 
	 usC = ko.observable(0),
	 ukL = ko.observable(0),
	 ukC = ko.observable(0),
	 caL = ko.observable(0),
	 caC = ko.observable(0), 
	 auL = ko.observable(0), 
	 auC = ko.observable(0),
	 aeL = ko.observable(0), 
	 aeC = ko.observable(0),
	 ukT= ko.observable(0),
	 usT= ko.observable(0),
	 caT= ko.observable(0),
	 auT= ko.observable(0),
	 aeT= ko.observable(0)
	} else{
	 activity = ko.observable(ActivityName), 
	 usL = ko.observable(""), 
	 usC = ko.observable(""),
	 ukL = ko.observable(""),
	 ukC = ko.observable(""),
	 caL = ko.observable(""),
	 caC = ko.observable(""), 
	 auL = ko.observable(""), 
	 auC = ko.observable(""),
	 aeL = ko.observable(""), 
	 aeC = ko.observable(""),
	 ukT= ko.observable(""),
	 usT= ko.observable(""),
	 caT= ko.observable(""),
	 auT= ko.observable(""),
	 aeT= ko.observable("")
	}
	return{
		activity:activity,
		usL:usL,
		usC:usC,
		ukL:ukL,
		ukC:ukC,
		caL:caL,
		caC:caC,
		auL:auL,
		auC:auC,
		aeL:aeL,
		aeC:aeC,
		usT:usT,
		ukT:ukT,
		auT:auT,
		caT:caT,
		aeT:aeT
	}
    
   
    
    
};
return {
    GetActiveUser: GetActiveUser,
	DashboardInsightsModel:DashboardInsightsModel
};
});