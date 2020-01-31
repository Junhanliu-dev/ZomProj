using System;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ZomAPIs.Helpler;
using ZomAPIs.Model;
using ZomAPIs.Model.Data;
using ZomAPIs.Services;

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

            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSetting>(appSettingSection);
            
            //Configure jwt auth
            var appSettings = appSettingSection.Get<AppSetting>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "http://localhost:5001",
                    ValidAudience = "http://localhost:5001",
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
            services.AddControllers();

            var mvcCoreBuilder = services.AddMvcCore(option => option.EnableEndpointRouting = false);
            
            mvcCoreBuilder.AddFormatterMappings().AddNewtonsoftJson().AddCors();


            //AutoMapper configuration
            services.AddAutoMapper(typeof(Startup));

            //Cors Domain config
            services.AddCors(o =>
                o.AddPolicy("EnableCORS", builder => { builder.AllowAnyOrigin()
                                                                                .AllowAnyMethod()
                                                                                .AllowAnyHeader(); }));

            //Mysql and MongoDB Configuration
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
            services.AddScoped<IAuthService, AuthService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserDbContext contexts)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            contexts.Database.EnsureCreated();
            app.UseAuthentication();
            app.UseMvc();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors("EnableCORS");
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}