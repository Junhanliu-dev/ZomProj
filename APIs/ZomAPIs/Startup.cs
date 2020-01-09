using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson.Serialization;
using ZomAPIs.Model;
using ZomAPIs.Model.Data;

namespace ZomAPIs
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
            
            //AutoMapper configuration
            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<UserDbContext>(options => 
                options.UseMySql(Configuration.GetSection("MysqlConnection:ConnectionString").Value));
            
            services.Configure<Settings>(options =>
                {
                    options.ConnectionString
                        = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                    options.Database
                        = Configuration.GetSection("MongoConnection:Database").Value;
                }
            );
            services.AddTransient<IRestaurantRepository, RestaurantRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserDbContext contexts)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            contexts.Database.EnsureCreated();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}