var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();
connection.on('receiveMessage', function () {
    document.getElementById("myForm").style.display = "block";
    updateNotification();

})
connection.on('receiveMessage', addMessageToChat);
connection.on('receiveMessage', getNotification);




updateNotification();

$('span.noti').click(function (e) {
    console.log("2");
    console.log(e);
    e.stopPropagation();
    $('.noti-content').toggle();
    //$('.noti-content').show();
    console.log($('.noti-content').text());
    var count = 0;
    count = parseInt($('.count').html()) || 0;
    console.log(count);
    if (count > 0) {
        updateNotification();
    }
    $('.count', this).text("0");
})
function controller(tag) {
    
    console.log(tag);
    console.log(typeof (tag));
    $('.noti-content').hide();
    var count = 0;
    count = parseInt($('.count').html()) || 0;
    count--;
    $('.count').html(count);
    $.ajax({
        url: '/home/Index30',
        method: 'POST',
        data: {
            id: tag

        }

    });

}
//$('html').click(function () {
//    console.log($('html'));
//        $('.noti-content').hide();
//        var count = 0;
//        count = parseInt($('.count').html()) || 0;
//        count--;
//        $('.count').html(count);
//        $.ajax({
//            url: '/home/Indexxx',
//            method: 'POST',
//            data: {
//                id: $('#ahmed').text()

//            }

//        });
//        console.log(id);
//    })
function updateNotification() {
    $('#noticontent').empty();
    $('#noticontent').append($("<li>Loading...</li>"));
    var tr = "";
    $.ajax({
        url: '/home/Index20',
        method: 'GET',
        success: (result) => {
            console.log(result);
            console.log(result.length);
            $('.count').text(result.length);
            console.log(('.count').text);
            $('#noticontent').empty();
            if (result.length == 0) {
                $('#noticontent').append($("<li>No Data Available...</li>"));
            }
            $.each(result, function (index, value) {
                
                $('#noticontent').append($('<li onclick="controller(id);"  id=' + value.messagesId + ' >' + value.updatedBy +    ' Says'    +' (' + value.messageText + ') </li><a href="/Home/NewChat7?id='+value.updatedBy+'">Go To Life Chat</a>'));
                
            })

        },
        error: (error) => {
            console.log(error)
        }
    });
}

function updateNotificationCount() {
    console.log("6");
    var count = 0;
    count = parseInt($('.count').html()) || 0;
    count++;
    $('.count').html(count);
}

connection.start()
    .catch(error => {
        console.error(error.message);
    });


//function updateNotification() {
//    $('#noticontent').empty();
//    $('#noticontent').append($("<li>Loading...</li>"));
//    var tr = "";
//    $.ajax({
//        url: '/home/Indexx',
//        method: 'GET',
//        success: (result) => {
//            console.log(result);
//            console.log(result.length);


//            $('#noticontent').empty();
//            if (result.length == 0) {
//                $('#noticontent').append($("<li>No Data Available...</li>"));
//            }
//            $.each(result, function (index, value) {
//                $('#noticontent').append($('<li>New Contact : ' + value.id + ' (' + value.name + ') added</li>'));
//            })

//        },
//        error: (error) => {
//            console.log(error)
//        }
//    });
//}
//function updateNotificationCount()
//{
//    console.log("6");
//    var count = 0;
//    count = parseInt($('.count').html()) || 0;
//    count++;
//    $('.count').html(count);
//}
//connection.on('receiveMessage', getNotification);
//connection.on("UserConnected", function (connectionId) {
//    var groupElement = document.getElementById("group");
//    var option = document.createElement("option");
//    option.text = connectionId;
//    option.value = connectionId;
//    groupElement.add(option);
//});

//connection.on("UserDisconnected", function (connectionId) {
//    var groupElement = document.getElementById("group");
//    for (var i = 0; i < groupElement.length; i++) {
//        if (groupElement.options[i].value == connectionId) {
//            groupElement.remove(i);
//        }
//    }
//});



//function sendMessageToHub(message) {
//    console.log(message.groupValue);
//    alert("kjkj");
//    if (message.groupValue === "Myself") {

//        method = "SendMessageToCaller";
//        connection.invoke(method, message);
//    }
//    else if (message.groupValue === "All") {
//        method = "SendMessage"
//        connection.invoke(method, message);
//    }
//    else if (message.groupValue === "PrivateGroup") {
//        method = "SendMessageToGroup"
//        connection.invoke(method, message.groupValue, message);
//    }
//    else {
//        method = "SendMessageToUser";
//        connection.invoke(method, message.groupValue, message);
//    }

//}





//document.getElementById("joinGroup").addEventListener("click", function (event) {
//    connection.invoke("JoinGroup", "PrivateGroup").catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});


//$(document).ready(function () {



//    //function getNotification() {
//    //    var res = $("#notifications");
//    //    $.ajax({
//    //        url: "/Home/GetNotification",
//    //        method: "GET",
//    //        success: function (result) {

//    //            if (result.count != 0) {
//    //                $("#noti_Counter").html(result.count);
//    //                $("#noti_Counter").show('slow');
//    //            } else {
//    //                $("#noti_Counter").html();
//    //                $("#noti_Counter").hide('slow');
//    //                $("#noti_Counter").popover('hide');
//    //            }

//    //            //var notifications = result.usernotification;
//    //            //notifications.foreach(element => {
//    //            //    res = res.append("<div class='list-group-item notification-text' data-id='" + element.notification.id + "'>" + element.notification.text + "</div>");
//    //            //});



//    //            //$("#notifications").html(res);

//    //            console.log(result);
//    //        },
//    //        error: function (error) {
//    //            console.log(error);
//    //        }
//    //    });
//    //}




//    //getNotification();
//    // ANIMATEDLY DISPLAY THE NOTIFICATION COUNTER.
//    $('#noti_Counter')
//        .css({ opacity: 0 })
//        .text('17')  // ADD DYNAMIC VALUE (YOU CAN EXTRACT DATA FROM DATABASE OR XML).
//        .css({ top: '-10px' })
//        .animate({ top: '-2px', opacity: 1 }, 500);

//    $('#noti_Button').click(function () {

//        // TOGGLE (SHOW OR HIDE) NOTIFICATION WINDOW.
//        $('#notifications').fadeToggle('fast', 'linear', function () {
//            if ($('#notifications').is(':hidden')) {
//                $('#noti_Button').css('background-color', '#2E467C');
//            }
//            // CHANGE BACKGROUND COLOR OF THE BUTTON.
//            else $('#noti_Button').css('background-color', '#FFF');
//        });

//        $('#noti_Counter').fadeOut('slow');     // HIDE THE COUNTER.

//        return false;
//    });

//    // HIDE NOTIFICATIONS WHEN CLICKED ANYWHERE ON THE PAGE.
//    $(document).click(function () {
//        $('#notifications').hide();

//        // CHECK IF NOTIFICATION COUNTER IS HIDDEN.
//        if ($('#noti_Counter').is(':hidden')) {
//            // CHANGE BACKGROUND COLOR OF THE BUTTON.
//            $('#noti_Button').css('background-color', '#2E467C');
//        }
//    });

//    $('#notifications').click(function () {
//        return false;       // DO NOTHING WHEN CONTAINER IS CLICKED.
//    });
//});


























connection.on("UserConnected", function (connectionId) {
    var groupElement = document.getElementById("group");
    var option = document.createElement("option");
    option.text = connectionId;
    option.value = connectionId;
    groupElement.add(option);
});

connection.on("UserDisconnected", function (connectionId) {
    var groupElement = document.getElementById("group");
    for (var i = 0; i < groupElement.length; i++) {
        if (groupElement.options[i].value == connectionId) {
            groupElement.remove(i);
        }
    }
});

connection.start()
    .catch(error => {
        console.error(error.message);
    });

function sendMessageToHub(message) {
   
    console.log(message);
    
    if (message.groupValue === "Myself") {

        method = "SendMessageToCaller";
        connection.invoke(method, message);
    }
    else if (message.groupValue === "All") {
        
        method = "SendMessage"
        connection.invoke(method, message);
    }
    else if (message.groupValue === "PrivateGroup") {
        method = "SendMessageToGroup"
        connection.invoke(method, message.groupValue, message);
    }
    else {
        method = "SendMessageToUser";
        connection.invoke(method, message.groupValue, message);
    }

}





document.getElementById("joinGroup").addEventListener("click", function (event) {
    connection.invoke("JoinGroup", "PrivateGroup").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});


$(document).ready(function () {



    //function getNotification() {
    //    var res = $("#notifications");
    //    $.ajax({
    //        url: "/Home/GetNotification",
    //        method: "GET",
    //        success: function (result) {

    //            if (result.count != 0) {
    //                $("#noti_Counter").html(result.count);
    //                $("#noti_Counter").show('slow');
    //            } else {
    //                $("#noti_Counter").html();
    //                $("#noti_Counter").hide('slow');
    //                $("#noti_Counter").popover('hide');
    //            }

    //            //var notifications = result.usernotification;
    //            //notifications.foreach(element => {
    //            //    res = res.append("<div class='list-group-item notification-text' data-id='" + element.notification.id + "'>" + element.notification.text + "</div>");
    //            //});

            

    //            //$("#notifications").html(res);

    //            console.log(result);
    //        },
    //        error: function (error) {
    //            console.log(error);
    //        }
    //    });
    //}

    


    //getNotification();
    // ANIMATEDLY DISPLAY THE NOTIFICATION COUNTER.
    $('#noti_Counter')
        .css({ opacity: 0 })
        .text('17')  // ADD DYNAMIC VALUE (YOU CAN EXTRACT DATA FROM DATABASE OR XML).
        .css({ top: '-10px' })
        .animate({ top: '-2px', opacity: 1 }, 500);

    $('#noti_Button').click(function () {

        // TOGGLE (SHOW OR HIDE) NOTIFICATION WINDOW.
        $('#notifications').fadeToggle('fast', 'linear', function () {
            if ($('#notifications').is(':hidden')) {
                $('#noti_Button').css('background-color', '#2E467C');
            }
            // CHANGE BACKGROUND COLOR OF THE BUTTON.
            else $('#noti_Button').css('background-color', '#FFF');
        });

        $('#noti_Counter').fadeOut('slow');     // HIDE THE COUNTER.

        return false;
    });

    // HIDE NOTIFICATIONS WHEN CLICKED ANYWHERE ON THE PAGE.
    $(document).click(function () {
        $('#notifications').hide();

        // CHECK IF NOTIFICATION COUNTER IS HIDDEN.
        if ($('#noti_Counter').is(':hidden')) {
            // CHANGE BACKGROUND COLOR OF THE BUTTON.
            $('#noti_Button').css('background-color', '#2E467C');
        }
    });

    $('#notifications').click(function () {
        return false;       // DO NOTHING WHEN CONTAINER IS CLICKED.
    });
});