using Microsoft.AspNetCore.Builder;
using Nancy.Owin;

namespace ei8.Cortex.Diary.Nucleus.Port.Adapter.Out.Api
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(buildFunc => buildFunc.UseNancy());
        }
    }
}
