using Nancy;
using Nancy.Responses;
using Newtonsoft.Json;
using works.ei8.Cortex.Diary.Nucleus.Application.Notification;
using org.neurul.Common;
using System.Linq;
using System.Text;
using works.ei8.Cortex.Diary.Common;

namespace works.ei8.Cortex.Diary.Nucleus.Port.Adapter.Out.Api
{
    public class NotificationModule : NancyModule
    {
        public NotificationModule(INotificationApplicationService notificationService) : base("/nuclei/d23/notifications")
        {
            this.Get("/", async (parameters) => new TextResponse(JsonConvert.SerializeObject(
                await notificationService.GetNotificationLog(string.Empty)
                ))
            );

            this.Get("/{logid}", async (parameters) => new TextResponse(JsonConvert.SerializeObject(
                await notificationService.GetNotificationLog(parameters.logid)
                ))
            );
        }
    }
}