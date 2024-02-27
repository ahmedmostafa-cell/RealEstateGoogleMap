



var co = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

co.on("RecieveMessage", function (fromUser, message) {
    var msg = fromUser + ": " + message;
    var li = document.createElement("li");
    li.textContent = msg;
    $("#list").prepend(li);
})

co.start();
$("#btnSend").on("click", function () {
    var fromUser = $("#txtUser").val();
    var message = $("#txtMessage").val();

    co.invoke("SendMessage", fromUser, message);
});