using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SpaApiMiddleware;
using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace api
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
             var conStr = Configuration.GetConnectionString("DefaultConnection");
            var pgConn = Environment.GetEnvironmentVariable("DATABASE_URL");

            if (!string.IsNullOrWhiteSpace(pgConn))
                conStr = HerokuPGParser.ConnectionHelper.BuildExpectedConnectionString(pgConn);
           
            services.AddDbContext<AppDb>(options => options.UseNpgsql(conStr));
            
            services.AddAutoMapper();

            services.AddTransient<UpdaterJob>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMvc();
        }
    }
}
