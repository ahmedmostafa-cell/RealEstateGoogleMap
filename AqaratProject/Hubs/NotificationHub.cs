using BL;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AqaratProject.Hubs
{
    public class NotificationHub : Hub
    {
       
        Al3QaratContext ctx;
        public NotificationHub(Al3QaratContext context)
        {
           
            ctx = context;
           
        }

        public class MessageObject
        {
            public string mesage { get; set; } 
            public string id { get; set; }

            public string id2 { get; set; }

            public string type { get; set; }
        }
        public static int notificationCounter = 0;
        public static List<MessageObject> messages = new List<MessageObject>();

        //public async Task SendMessage(string message)
        //{
        //    if (!string.IsNullOrEmpty(message))
        //    {
        //        notificationCounter++;
        //        messages.Add(message);
        //        await LoadMessages();
        //    }
        //}

        public async Task LoadMessages()
        {
            var user = Context.User;
            var userid = user.FindFirstValue(ClaimTypes.NameIdentifier);
            messages.Clear();
            foreach (var i in ctx.TbRealTimeNotifcations.ToList().Where(a=> a.Notes == userid)) 
            {
                messages.Add(new MessageObject { id = i.RealTimeNotifcationId.ToString(), mesage = i.NotificationType , id2 = i.CreatedBy , type=i.UpdatedBy });
                   

            }
            notificationCounter = ctx.TbRealTimeNotifcations.ToList().Where(a=> a.Notes == userid).Count();
            await Clients.All.SendAsync("LoadNotification", messages, notificationCounter);
        }
    }
}
