using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Students.Client.HttpHandlers;
using IdentityModel.Client;
using Microsoft.IdentityModel.Tokens;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Students.Client.ApiServices;
using Microsoft.EntityFrameworkCore;
using Students.Client.Data;

namespace Students.Client
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
            services.AddControllersWithViews();
            services.AddScoped<IStudentApiService, StudentApiService>();
           

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
             .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
             {
                
                 options.Authority = "https://localhost:5005";
                 options.ClientId = "students_mvc_client";
                 options.ClientSecret = "secret";
                 options.ResponseType = "code id_token";

                 options.Scope.Add("openid");
                 options.Scope.Add("profile"); 
                 options.Scope.Add("studentAPI");
                 options.Scope.Add("roles");
                 options.Scope.Add("address");
                 options.Scope.Add("email");
                 options.ClaimActions.MapUniqueJsonKey("role", "role");
               
                 options.SaveTokens = true;
                  
                 options.GetClaimsFromUserInfoEndpoint = true;
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     NameClaimType = JwtClaimTypes.GivenName,
                     RoleClaimType = JwtClaimTypes.Role,
                    
                    

                 };

             });
            // 1 create an HttpClient used for accessing the students.API
            services.AddTransient<AuthenticationDelegatingHandler>();

            services.AddHttpClient("StudentAPIClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:5010/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            }).AddHttpMessageHandler<AuthenticationDelegatingHandler>();


            // 2 create an HttpClient used for accessing the IDP
            services.AddHttpClient("IDPClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:5005/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            });
            services.AddHttpContextAccessor();


        /*  services.AddSingleton(new ClientCredentialsTokenRequest
            {                                                
              Address = "https://localhost:5005/connect/token",
              ClientId = "studentClient",
              ClientSecret = "secret",
              Scope = "studentAPI"
            });*/

            services.AddDbContext<StudentsClientContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("StudentsClientContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
             {
                 app.UseExceptionHandler("/Home/Error");
                 // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                 app.UseHsts();
             }
            //  app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Strict });
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
          
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
