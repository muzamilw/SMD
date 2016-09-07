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
return {
    GetActiveUser: GetActiveUser
}
});