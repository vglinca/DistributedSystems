using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AviaSalesApi.Data.DbMapping;
using Cassandra.Mapping;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
// ReSharper disable All

namespace AviaSalesApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            try
            {
                MappingConfiguration.Global.Define<CassandraMapping>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}