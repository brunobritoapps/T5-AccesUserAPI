
$(document).ready(function () {
    $.getJSON('https://ipapi.co/json/', function (d) {

        const Data = {
            Ip: d.ip,
            Page: window.location.href.split('?')[0],
            Browser: navigator.appVersion,
            Parameters: window.location.href.split('?')[1],
            Date: getDateNow(),
        };

        const Url = "https://localhost:44360/api/accesses/";
        const Param = {
            method: "POST",
            headers: {'Content-Type':'application/json'},
            body: JSON.stringify(Data),
        };

        fetch(Url, Param)
            .then(data => { return data.json() })
            .then(function (data) {
                console.log( data);
            });

    });

});

$(function () {
     var getAccess = function () {
     var url = "https://localhost:44360/api/accesses";

     $.get(url).always(showResponse);
         return false;
    };
     var showResponse = function (object) {
         $("#preOutput").text(JSON.stringify(object, null, 4));
     };
     $("#btnGetAccess").click(getAccess);
 });


function getDateNow() {
    var dNow = new Date();
    var localdate = dNow.getDate() + '-' + (dNow.getMonth() + 1) + '-' + dNow.getFullYear() + ' ' + dNow.getHours() + ':' + dNow.getMinutes() + ':' + dNow.getSeconds();
    return localdate;
}

