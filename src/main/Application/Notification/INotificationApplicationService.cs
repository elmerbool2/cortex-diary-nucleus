using System.Threading;
using System.Threading.Tasks;
using works.ei8.Cortex.Diary.Common;

namespace works.ei8.Cortex.Diary.Nucleus.Application.Notification
{
    public interface INotificationApplicationService
    {
        Task<NotificationLog> GetNotificationLog(string notificationLogId, CancellationToken token = default(CancellationToken));
    }
}
