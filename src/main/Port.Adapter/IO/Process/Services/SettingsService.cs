using System;
using works.ei8.Cortex.Diary.Nucleus.Application;
using works.ei8.Cortex.Diary.Nucleus.Port.Adapter.Common;

namespace works.ei8.Cortex.Diary.Nucleus.Port.Adapter.IO.Process.Services
{
    public class SettingsService : ISettingsService
    {
        public string CortexInBaseUrl => Environment.GetEnvironmentVariable(EnvironmentVariableKeys.CortexInBaseUrl);

        public string CortexOutBaseUrl => Environment.GetEnvironmentVariable(EnvironmentVariableKeys.CortexOutBaseUrl);

        public string CortexGraphOutBaseUrl => Environment.GetEnvironmentVariable(EnvironmentVariableKeys.CortexGraphOutBaseUrl);

        public string EventSourcingOutBaseUrl => Environment.GetEnvironmentVariable(EnvironmentVariableKeys.EventSourcingOutBaseUrl);
    }
}
