﻿using GrpcBroker.Services;
using GrpcBroker.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GrpcBroker
{
    public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddGrpc();
			services.AddSingleton<IMessageStorageService, MessageStorageService>();
			services.AddSingleton<IConnectionManagementService, ConnectionManagementService>();
			services.AddSingleton<IMessageSenderService, MessageSenderService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGrpcService<PublisherService>();
				endpoints.MapGrpcService<SubscriberService>();
				endpoints.MapGrpcService<SensorsListGetterService>();

				endpoints.MapGet("/", async context =>
				{
					await context.Response.WriteAsync("Sal m8. Who are U?");
				});
			});
		}
	}
}
