using System;
using System.Collections.Generic;
using System.Text;

namespace works.ei8.Cortex.Diary.Nucleus.Application
{
    public interface ISettingsService
    {
        string CortexInBaseUrl { get; }
        string CortexOutBaseUrl { get; }
    }
}
