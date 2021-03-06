﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cornhacks2019.Accessors;
using Cornhacks2019.Engines;
using Cornhacks2019.Interfaces.AccessorInterfaces;
using Cornhacks2019.Interfaces.EngineInterfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cornhacks2019
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IEmailAccessor, EmailAccessor>();
            services.AddScoped<IGithubAccessor, GithubAccessor>();
            services.AddScoped<ISponsorAccessor, SponsorAccessor>();
            services.AddScoped<IPreferenceAccessor, PreferenceAccessor>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IEmailEngine, EmailEngine>();
            services.AddScoped<IGithubEngine, GithubEngine>();
            services.AddScoped<IPreferenceEngine, PreferenceEngine>();
            services.AddScoped<ISubscriptionEngine, SubscriptionEngine>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(policy => policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseMvc();
        }
    }
}
