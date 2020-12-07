using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;

namespace header_parser
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.Run(async context =>
			{
				if (!context.Request.Headers.TryGetValue("User-Agent", out var software))
				{
					context.Response.StatusCode = 400;
					await context.Response.WriteAsync("<h1>Bad request</h1>");
					return;
				}

				context.Response.Headers.Add("X-Application-Purpose", "FreeCodeCamp Request Header Parser Microservice");
				context.Response.ContentType = "text/html";
				context.Response.StatusCode = 200;
				await context.Response.WriteAsync("<h1>Hello, ASP.NET Core!</h1>");
			});
		}
	}
}