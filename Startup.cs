using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace header_parser
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("AllowAll", policy =>
				{
					policy.AllowAnyOrigin();
					policy.AllowAnyMethod();
					policy.AllowAnyHeader();
				});
			});
		}
		
		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors("AllowAll");

			app.Run(async context =>
			{
				if (!context.Request.Headers.TryGetValue("User-Agent", out var software) ||
					!context.Request.Headers.TryGetValue("X-Forwarded-For", out var ipAddress) ||
					!context.Request.Headers.TryGetValue("Accept-Language", out var language))
				{
					context.Response.StatusCode = 400;
					await context.Response.WriteAsync("<h1>Bad request</h1>");
					return;
				}
				
				var headers = new RequestHeaders
				{
					IpAddress = ipAddress,
					Language = language,
					Software = software
				};

				context.Response.ContentType = "application/json";
				context.Response.StatusCode = 200;
				await context.Response.WriteAsync(JsonSerializer.Serialize(headers));
			});
		}
	}
}