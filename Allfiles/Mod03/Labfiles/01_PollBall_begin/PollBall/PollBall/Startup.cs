﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace PollBall
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.Use(async (context, next) =>
            {
                if (context.Request.QueryString.HasValue)
                {
                    string parameterText = context.Request.Query["Favorite"];
                    int equationLocation = context.Request.QueryString.Value.LastIndexOf('=');
                    string selectedValue = parameterText.Substring(equationLocation + 1, parameterText.Length - (equationLocation + 1));
                    await context.Response.WriteAsync("Selected Value is = " + selectedValue);
                }
                else await next();

            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
