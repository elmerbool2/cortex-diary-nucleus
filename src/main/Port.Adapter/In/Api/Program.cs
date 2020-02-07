﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace works.ei8.Cortex.Diary.Nucleus.Port.Adapter.In.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}