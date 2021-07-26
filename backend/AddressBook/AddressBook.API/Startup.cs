using AddressBook.EntityFramework.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AddressBook.Core.Addresses.Managers;
using AddressBook.Core.Auxiliaries.Repositories;
using AddressBook.EntityFramework.Auxiliaries;
using AddressBook.Core.Auxiliaries.Entities;
using AddressBook.Core.Addresses.Entities;

namespace AddressBook.API
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AddressBook.API", Version = "v1" });
            });
            services.AddDbContext<AddressBookContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AddressBookContext")));

            //DI
            services.AddSingleton<IRepository<Address>, Repository<Address>>();
            services.AddTransient<IAddressManager, AddressManager>();

            //CORS
            //var allowCors = Configuration.GetSection("AppOptions")["Cors"].Split(",").ToArray();
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowSpecificOrigin",

            //        builder => builder.WithOrigins(allowCors).AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AddressBook.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
