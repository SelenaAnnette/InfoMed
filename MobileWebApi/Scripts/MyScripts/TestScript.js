$(document).ready(function () {
    var params = {
        personId: '11111111-1111-1111-1111-111111111111',
        sendingDateMilliseconds: new Date().getTime(),
        timezoneOffset: new Date().getTimezoneOffset(),
        measurings: [{
            MeasuringTypeId: '11111111-1111-1111-1111-111111111111',
            Value: 20.5
        },
       {
           MeasuringTypeId: '11111111-1111-1111-1111-111111111111',
           Value: 20.5
       }]
    };

    $.ajax({
        url: "http://localhost:15695/Measuring/AddPersonMeasurings",
        type: 'GET',
        data: params,
        dataType: 'application/json; charset=utf-8',
        success: function (response) {
            console.log(response);
        }
    });
});