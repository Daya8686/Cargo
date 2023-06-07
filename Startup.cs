//using CargoManagerSystem.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.OpenApi.Models;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using CargoManagerSystem.Models.AuthFiles;
//using System.Text;

//namespace CargoManagerSystem
//{
//    public class Startup
//    {

//            public Startup(IConfiguration configuration)
//            {
//                Configuration = configuration;
//            }

//            public IConfiguration Configuration { get; }
//            public object NegotiateDefaults { get; private set; }

//            // This method gets called by the runtime. Use this method to add services to the container.
//            public void ConfigureServices(IServiceCollection services)
//            {
//            services.AddDbContext<MyIdentityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConStr")));
//            // services.AddDbContext<MyIdentityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
//            services.AddDbContext<MyCargoDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConStr")));

//            services.AddControllers();

//                services.AddCors();
//                services.AddCors(options =>
//                {
//                    options.AddPolicy("CorsPolicy",
//                    builder => builder.AllowAnyOrigin()
//                    .AllowAnyMethod()
//                    .AllowAnyHeader()
//                    );
//                });

//            //Adding Swagger Middleware
//            services.AddSwaggerGen(c =>
//            {
//                c.SwaggerDoc("Ver1", new OpenApiInfo() { Title = "My Demo Web API", Version = "Ver1" });
//                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
//                {
//                    Name = "Authorization",
//                    Type = SecuritySchemeType.ApiKey,
//                    Scheme = "Bearer",
//                    BearerFormat = "JWT",
//                    In = ParameterLocation.Header,
//                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
//                });

//                // For Identity
//                services.AddIdentity<MyIdentityUser, MyIdentityRole>()
//                    .AddEntityFrameworkStores<MyIdentityDbContext>()
//                    .AddDefaultTokenProviders();

//                //Adding Authentication
//                services.AddAuthentication(options =>
//                    {
//            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//        })
//                    // Adding Jwt Bearer
//                    .AddJwtBearer(options =>
//                    {
//            options.SaveToken = true;
//            options.RequireHttpsMetadata = false;
//            options.TokenValidationParameters = new TokenValidationParameters()
//            {
//                ValidateIssuer = true,
//                ValidateAudience = true,
//                ValidAudience = Configuration["JWT:ValidAudience"],
//                ValidIssuer = Configuration["JWT:ValidIssuer"],
//                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
//            };
//        });
//            //    services.AddControllers(options => options.Filters.Add(new HttpResponseExceptionFilter()));
//            //}

//        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//          public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
//            {
//                if (env.IsProduction())
//                {
//                    app.UseDeveloperExceptionPage();

//                }



//                app.UseHttpsRedirection();
//                app.UseStaticFiles();

//                app.UseRouting();
//                app.UseSwagger();

//                //Adding CORS middleware
//                app.UseCorsMiddleware();
//                app.UseCors("CorsPolicy");


//                app.UseSwaggerUI(c =>
//                {
//                    c.SwaggerEndpoint("/swagger/Ver1/swagger.json", "My Demo Web API");
//                });
//                app.UseAuthorization();
//                app.UseAuthentication();



//                app.UseEndpoints(endpoints =>
//                {
//                    // endpoints.MapControllers();
//                    endpoints.MapControllerRoute("default", "{Controller=Products}/{action=Get}/{id?}");
//                });
//            }
//        }

//}
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using CargoManagerSystem.Models.AuthFiles;
using CargoManagerSystem;
using CargoManagerSystem.Models;

namespace CargoManagerSystem
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
            services.AddDbContext<MyCargoDBContext>();//(options => options.UseSqlServer(Configuration.GetConnectionString("MyConStr")));
            services.AddDbContext<MyIdentityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConStr")));


            // For Identity
            services.AddIdentity<MyIdentityUser, MyIdentityRole>()
            .AddEntityFrameworkStores<MyIdentityDbContext>()
             .AddDefaultTokenProviders();


            services.AddControllers();

            //Enabling CORS middleware 
            services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                );
            });


            // Adding Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });

            ////Adding Swagger Middleware
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Ver1", new OpenApiInfo() { Title = "My Sample API", Version = "Ver1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
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

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseSwagger();

            //Injecting CORS Middleware
            app.UseCorsMiddleware();
            app.UseCors("CorsPolicy");

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/Ver1/swagger.json", "My Sample Web API");
            });
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}