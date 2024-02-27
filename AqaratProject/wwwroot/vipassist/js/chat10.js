class Message {
    constructor(username, text, when, groupValue) {
        this.userName = username;
        this.text = text;
        this.when = when;
        this.groupValue = groupValue;
        
    }
}

// userName is declared in razor page.
const username = userName;
const textInput = document.getElementById('messageText');
const whenInput = document.getElementById('when');
const chat = document.getElementById('chat');
const messagesQueue = [];
const groupElement = document.getElementById("group")

document.getElementById('submitButton').addEventListener('click', () => {
   
    var currentdate = new Date();
    when.innerHTML =
        (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/"
        + currentdate.getFullYear() + " "
        + currentdate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true })
});

function clearInputField() {
    messagesQueue.push(textInput.value);
    textInput.value = "";
}

function sendMessage() {
   
    let text = messagesQueue.shift() || "";
    if (text.trim() === "") return;
    
    let when = new Date();
    let groupValue = groupElement.options[groupElement.selectedIndex].value;
    let message = new Message(username, text, when, groupValue); //groupValue
   
    sendMessageToHub(message);
}

function getNotification(message) {
    
    var lastPageText = $("#notifications").text(message.text);
    $("#notifications").text(lastPageText.text() + message.text);
    let text = document.createElement('p');
    text.innerHTML = message.text;
    document.getElementById('#notifications').appendChild(text);
    console.log(message.text);
    console.log(message);
    //$.ajax({
    //    url: "/Home/GetNotification",
    //    method: "GET",
    //    success: function (result) {

    //        if (result.count != 0) {
    //            $("#noti_Counter").html(result.count);
    //            $("#noti_Counter").show('slow');
    //        } else {
    //            $("#noti_Counter").html();
    //            $("#noti_Counter").hide('slow');
    //            $("#noti_Counter").popover('hide');
    //        }

            //var notifications = result.usernotification;
            //notifications.foreach(element => {
            //    res = res.append("<div class='list-group-item notification-text' data-id='" + element.notification.id + "'>" + element.notification.text + "</div>");
            //});



            //$("#notifications").html(res);

        //    console.log(result);
        //},
        //error: function (error) {
        //    console.log(error);
    //    //}
    //});
}
function addMessageToChat(message) {
    
    let isCurrentUserMessage = message.userName === username;

    let container = document.createElement('div');
    container.className = isCurrentUserMessage ? "container darker" : "container";

    let sender = document.createElement('p');
    sender.className = "sender";
    sender.innerHTML = message.userName;
    let text = document.createElement('p');
    text.innerHTML = message.text;

    let when = document.createElement('span');
    when.className = isCurrentUserMessage ? "time-left" : "time-right";
    var currentdate = new Date();
    when.innerHTML = 
        (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/"
        + currentdate.getFullYear() + " "
        + currentdate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true })

    container.appendChild(sender);
    container.appendChild(text);
    container.appendChild(when);
    chat.appendChild(container);
}


$(function () {
    console.log("1");
    $('span.noti').click(function (e) {
        console.log("2");
        console.log(e);
        e.stopPropagation();
        $('.noti-content').show();
        console.log($('.noti-content').text());
        var count = 0;
        count = parseInt($('span.count').html()) || 0;
        console.log(count);
        if (count > 0) {
            updateNotification();
        }
        $('span.count', this).html("&nbsp;");
    })
    $('html').click(function () {
        console.log("3");
        $('.noti-content').hide();
    })
    function updateNotification() {
        console.log("4");
        $('#noticontent').empty();
        $('#noticontent').append($("<li>Loading...</li>"));
        $.ajax({
            type: "GET",
            url: "/home/GetNotificationContacts",
            success: function (response) {
                console.log(response);
                $('#noticontent').empty();
                if (response.length == 0) {
                    $('#noticontent').append($("<li>No Data Available...</li>"));
                }
                $.each(response, function (index, value) {
                    $('#noticontent').append($('<li>New Contact : ' + value.CntactName + ' (' + value.ContacNo + ') added</li>'));
                })
            },
            error: function (error) {
                console.log("5");
                console.log(error);
            }
        })
    }

    function updateNotificationCount() {
        console.log("6");
        var count = 0;
        count = parseInt($('span.count').html()) || 0;
        count++;
        $('span.count').html(count);
    }

    //var notificationHub = $.connection.notificationHub;
    //$.connection.hub.start().done(function () {
    //    console.log("7");
    //    console.log("notificatuion hub start");
    //});


    //notificationHub.client.notify = function (message) {
    //    console.log(message);
    //    console.log("8");
    //    if (message && message.toLowerCase() == "added") {
    //        updateNotificationCount();

    //    }
    //}
})