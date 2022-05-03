using Alachisoft.NCache.Caching.Distributed;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SM.Repositories.IRepository;
using SM.Repositories.Repository;
using SM.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SM.Web
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
            //Registration of Base Repository.
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddSession();
            //For cacheing purpose
            services.AddResponseCaching();
            services.AddMvc();
            services.AddNCacheDistributedCache(options =>
            {
                //options.CacheName = "PrivateCache";
                //options.EnableLogs = true;
                //options.ExceptionsEnabled = true;
                options.CacheName = "PrivateCache";
                _ = (new CacheProfile()
                {
                    NoStore = true,
                    Duration = 0,
                    //Location = ResponseCacheLocation.None
                });
            });
  
            services.AddControllersWithViews();
            services.AddDistributedMemoryCache();
 
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(options =>
            //    {
            //        //options.LoginPath = "/Auth/Login";
            //        options.Events = new CookieAuthenticationEvents()
            //        {
            //            OnSigningIn = async context =>
            //            {
            //                var principal = context.Principal;
            //                if (principal.HasClaim(x => x.Type == ClaimTypes.NameIdentifier))
            //                {
            //                    if (principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value == "kdshah929@gmail.com")
            //                    {
            //                        var claimsIdentity = principal.Identity as ClaimsIdentity;
            //                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            //                    }
            //                }
            //                await Task.CompletedTask;
            //            },
            //            OnSignedIn = async context =>
            //            {
            //                await Task.CompletedTask;
            //            },
            //            OnSigningOut = async context =>
            //            {
            //                await Task.CompletedTask;
            //            },
            //        };
            //    });
       

            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.AddDbContext<SchoolManagementContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SchoolManagementContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SchoolManagementContext schoolManagementContext)
        {
            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
            };

            schoolManagementContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseResponseCaching();

            app.UseCookiePolicy(cookiePolicyOptions);

            app.UseAuthentication();

            app.UseAuthorization();

           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Auth}/{action=Login}/{id?}");
            });
            AppDomain.CurrentDomain.SetData("ContentRootPath", env.ContentRootPath);
            AppDomain.CurrentDomain.SetData("WebRootPath", env.WebRootPath);
        }
    }
}
