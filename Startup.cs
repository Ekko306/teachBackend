using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using teachBackend.Models;
using teachBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace teachBackend
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
            services.Configure<TeachDbSettings>(
        Configuration.GetSection(nameof(TeachDbSettings)));

            services.AddSingleton<ITeachDbSettings>(sp =>
                sp.GetRequiredService<IOptions<TeachDbSettings>>().Value);

            services.AddSingleton<TeacherService>();
            services.AddSingleton<StudentService>();
            services.AddSingleton<RecordService>();

            services.AddDbContext<OnlineContext>(opt =>
                opt.UseInMemoryDatabase("Online"));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
