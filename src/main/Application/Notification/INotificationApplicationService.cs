using System.Threading;
using System.Threading.Tasks;
using ei8.Cortex.Diary.Common;

namespace ei8.Cortex.Diary.Nucleus.Application.Notification
{
    public interface INotificationApplicationService
    {
        Task<NotificationLog> GetNotificationLog(string notificationLogId, CancellationToken token = default(CancellationToken));
    }
}
