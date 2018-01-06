﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Example.ScenarioTesting.Tests.Factories
{
    public static class ServerFactory
    {
        public static TestServer CreateServer()
        {
            var webHostBuilder = new WebHostBuilder().UseStartup<TestStartup>();

            return new TestServer(webHostBuilder);
        }
    }
}
