using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AviaSalesApi.Data;
using AviaSalesApi.Data.DbMapping;
using AviaSalesApi.Data.Entities;
using AviaSalesApi.Data.Repository.Interfaces;
using AviaSalesApi.Helpers;
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

                #region seed
                
                var processor = scope.ServiceProvider.GetService<IJsonFileProcessor>();
                var ticketByLocationAndDateRepository = scope.ServiceProvider.GetService<ICassandraRepository<Ticket>>();
                var ticketByIdRepository = scope.ServiceProvider.GetService<ICassandraRepository<TicketById>>();
                //await Seed.SeedDataAsync(ticketByLocationAndDateRepository, ticketByIdRepository, processor);
                
                #endregion
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