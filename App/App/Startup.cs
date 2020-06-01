using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Permissions;
using App.Extensions;
using App.Models;
using App.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;

namespace App
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
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<ApplicationDbContext>(opts =>
                opts.UseSqlServer(@"Server=.\SQLEXPRESS;Database=LibraryCMS;User Id=Library;Password=Library;MultipleActiveResultSets=true;")
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library CMS API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
            services.AddAutoMapper(typeof(Startup));
            services.RegisterDataServices();

            var key = Encoding.ASCII.GetBytes("fwiwdaaunykelhmeeegaoribyynrihotlwppcnimxvmevxbzmmesprmqogdtnwpbdzpajwgshkzmdlnarihjwhwqqjjhojoouaedbnwympcqpvfaumhgymexrcqkcfmgijrcyhjvbvumugdofutxvoggrejkoaiyzyspfhsfoysmcsyeyyhvlnrxswpbrqzozhmeatcavdhqhdfzgfbewnntgigxdkuahtpipswhiodarupccumlltsfibfbcher");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                    //options.Authority = "https://dev-542381.okta.com/oauth2/default";
                    //options.Audience = "api://default";

                    //options.Events = new JwtBearerEvents
                    //{
                    //    OnTokenValidated = async context =>
                    //    {
                    //        var provider = context.HttpContext.RequestServices;
                    //        var user = provider.GetService<UsersService>().GetUser(context.Principal.Identity.Name);

                    //        if(user.IsAdmin)
                    //        {
                    //            context.Principal.AddIdentity(;
                    //        }
                    //    }
                    //};
                });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin", builder => 
                    builder.WithOrigins("http://localhost:8080")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                    );
            });
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowMyOrigin"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseCors("AllowMyOrigin");

            //app.UseHttpsRedirection();
            app.UseAuthentication(); //must to be above UseMvc()
            //app.UseAuthorization();
            app.UseMvc();

            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library CMS API");
            });

        }
    }
}
