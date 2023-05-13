using Arkeos.Support;
using BackSistemaUbala.Models;
using General_back.Helpers;
using General_back.oData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static BackSistemaUbala.Models.ApplicationRoleExtension;

namespace BackSistemaUbala
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

            services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddODataScoped();
            services.AddIdentity<ApplicationUser, ApplicationRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            })
                .AddRoleValidator<MyRoleValidator>()
                .AddEntityFrameworkStores<AplicationDbContext>().AddDefaultTokenProviders()
                .AddErrorDescriber<SpanishIdentityErrorDescriber>();
            var oDataBuilder = new GeneraloDataConventionModelBuilder(services);
            var edmModel = oDataBuilder.GetEdmModel();

            services.AddControllers().AddOData(opt =>
            {
                opt.Count().Filter().Expand().Select().OrderBy().SetMaxTop(100).AddRouteComponents("odata", edmModel, new ODataBatchTransactionHandler());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BackSistemaUbala", Version = "v1" });
            });
            //Inyectar el contexto en el contenedor de servicios para luego ser usado por inyecciones de indepnedencia
            services.AddDbContext<AplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));
            //Autenticación
            services
                .AddAuthentication(options =>
                {
                    //Esquema de autenticación por medio de "Bearer"
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                    //options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                    //options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                    //options.DefaultSignInScheme = IdentityConstants.ExternalScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false, //Emisor-Por que estan en false
                        ValidateAudience = false,//Destinarios del token-Por que estan en false
                       // ValidateLifetime = false,//Tiempo de vida-Por que estan en false
                        ValidIssuer = Configuration["URLubala"],
                        ValidAudience = Configuration["URLubala"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("7AUW3u4mF3QOmdEBr4mQIlxcjT4h6E")),//Configuration["JwtKey"]
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });

                services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                                       .AllowAnyMethod()
                                                                         .AllowAnyHeader()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackSistemaUbala v1"));
            }



            app.UseRouting();

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
