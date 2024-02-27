

var Helper = {
    AjaxCall: function (url, paramters, callingType, returnFormat, success, failer) {
        $.ajax({
            url: url,
            data: paramters,
            dataType: returnFormat,
            success: function (data) {
                success(data);
                return data;
            },
            error: function () {
                failer();
                $('#info').html('<p>An error has occured</p>');
                return 0;
            },
            type: callingType,
        });
    },
    AjaxCallPost: function (url, paramters,  success, failer) {
        $.ajax({
            url: url,
            data: paramters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                success(data);
                return data;
            },
            error: function (xhr, err) {
                alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.success);
                alert("responseTex: " + xhr.responseText);
                failer();
                $('#info').html('<p>An error has occured</p>');
                return 0;

            },
            type: 'POST',

        });
    },
    AjaxCallGet: function (url, paramters, returnFormat, success, failer) {
         $.ajax({
             url: url,
             data: paramters,
             contentType: "application/json; charset=utf-8",
             dataType: returnFormat,
             success: function (data) {
                 success(data);
                 return data;
             },
             //error: function (xhr, err) {

             //    failer();
             //    $('#info').html('<p>An error has occured</p>');
             //    return 0;

             //},
             error: function (xhr, status, error) {

                 var errorMessage = xhr.status + ': ' + xhr.statusText + ': '
                 console.log('Error - ' + errorMessage);
             },
             type: 'GET',

         });
     },
    GetQueryString: function () {
         var vars = [], hash;
         var hashes = window.location.href.slice(window.location.href.indexOf)
         for (var i = 0; i < hashes.length; i++) {
             hash = hashes[i].split('=');
             vars.push(hash[0]);
             vars[hash[0]] = hash[1];
         }

    },
    geturlParameters: function (sParam) {
        var sPageURL = window.location.search.substring(1),
            sURLVariables = sPageURL.split('&'),
            sParameterName,
            i;
        for (i = 0; i < sURLVariables.length; i++) {
            return sParameterName[1] === undefined ? true : decodeURIComponent;
        }
    }


}