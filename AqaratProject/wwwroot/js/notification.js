
var connectionNotification = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/notification").build();

document.getElementById("sendButton").disabled = true;

connectionNotification.on("LoadNotification", function (message, counter) {
    document.getElementById("messageList").innerHTML = "";
    var notificationCounter = document.getElementById("notificationCounter");
    notificationCounter.innerHTML = "<span>(" + counter + ")</span>";
    console.log(message);
    for(let i = 0; i <message.length; i++) {
        console.log(message[i]);
        var li = document.createElement("li");
        var li2 = document.createElement("li");
       
        li.textContent = "Notification - " + message[i].mesage;
        li2.textContent = "الاطلاع عل التفاصيل";
       
       
        const lineBreak = document.createElement('br');
        document.getElementById("messageList").appendChild(li);
        document.getElementById("messageList").appendChild(li2);
        document.getElementById("messageList").appendChild(lineBreak);
        document.getElementById("messageList").appendChild(lineBreak);
       
       
        li2.addEventListener('click', () => {
            var arr = [];
          
            var arr = [li2.textContent];
            
          
            $.ajax({
                type: "POST",
                url: "/Admin/Home/RealTimeNotification",
                data: ({ Ids: arr, id: message[i].id , OnlyActive: true }),
                contextType: "application/json; charset=utf-8",
                datatype: "json",
                traditional: true,
                success: OnSuccessResult,
                error: OnError
            });
         
            function OnSuccessResult(data) {
              
                if (data.type === 'اضافة طلب') {
                   
                    window.location.href = "/Admin/Request/Form?id=" + data.id;

                }
                else if (data.type === 'تعديل طلب') {

                    window.location.href = "/Admin/Request/Form?id=" + data.id;

                }
                else if (data.type === 'تعديل استفسار')
                {
                   
                    window.location.href = "/Admin/Inquiry/Form2?id=" + data.id;

                }
                else if (data.type === 'اضافة استفسار') {


                    window.location.href = "/Admin/Inquiry/Form2?id=" + data.id;

                }
                else if (data.type === 'اضافة عرض') {

                    window.location.href = "/Admin/Offer/Form?id=" + data.id;

                }
                else if (data.type === 'تعديل عرض') {

                    window.location.href = "/Admin/Offer/Form?id=" + data.id;

                }
                else if (data.type === 'تعديل صورة') {

                    window.location.href = "/Admin/Offer/Details?id=" + data.id;

                }
                else if (data.type === 'اضافة صورة') {

                    window.location.href = "/Admin/Offer/Details?id=" + data.id;

                }

                else if (data.type === 'اضافة ملاحظة') {

                    window.location.href = "/Admin/Offer/Details?id=" + data.id;

                }
                else if (data.type === 'تعديل ملاحظة') {

                    window.location.href = "/Admin/Offer/Details?id=" + data.id;

                }
                else if (data.type === 'عرض غير متحرك') {

                    window.location.href = "/Admin/Offer/Details?id=" + data.id;

                }
                else if (data.type === 'حجز عرض') {

                    window.location.href = "/Admin/OfferBooking/Form?id=" + data.id;

                }
                else if (data.type === 'تعديل حجز عرض') {

                    window.location.href = "/Admin/OfferBooking/Form?id=" + data.id;

                }
                else if (data.type === 'دخول') {

                    window.location.href = "/Admin/LogHistory/Index";

                }
              
                
            }
          
            function OnError(data) {

            }



        }
            




        );
      
    }
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("notificationInput").value;
    connectionNotification.send("SendMessage", message).then(function () {
        document.getElementById("notificationInput").value = "";
    });
    event.preventDefault();
});


connectionNotification.start().then(function () {
    connectionNotification.send("LoadMessages")
    document.getElementById("sendButton").disabled = false;
});
