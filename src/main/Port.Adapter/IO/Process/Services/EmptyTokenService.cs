using org.neurul.Common.Http;

namespace works.ei8.Cortex.Diary.Nucleus.Port.Adapter.IO.Process.Services
{
    public class EmptyTokenService : ITokenService
    {
        public string GetAccessToken()
        {
            return string.Empty;
        }
    }
}
