using CustomHandlers.CustomHandler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SM.Entity.EFContext;
using SM.Repository.Core;
using SM.Services;
using System;
using System.Security.Claims;

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
            services.AddDbContext<Entity.EFContext.SMContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SchoolManagementContext")));
            services.AddMvc(options =>
            {
                // This pushes users to login if not authenticated
                //options.Filters.Add(new AuthorizeFilter());

                options.CacheProfiles.Add("Default0",
                    new CacheProfile()
                    {
                        Duration = 0,
                        NoStore = true
                    });
            });

            services.AddScoped<IDatabaseContext, SMContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddServices();
            services.AddTransient<IContextFactory, ContextFactory>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IUserServices, UserServices>();
            //services.AddScoped<IDatabaseContext, SMContext>();
            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddTransient(typeof(IUserServices), typeof(UserServices));
            ////services.AddTransient<IContextFactory,  >();
            //services.AddTransient<IUnitOfWork, UnitOfWork>();


            services.AddAuthentication("Cookies")
                 .AddCookie("Cookies", config =>
                 {
                     config.Cookie.Name = "UserLoginCookie"; // Name of cookie     
                     config.LoginPath = "/Auth/Login"; // Path for the redirect to user login page    
                     config.AccessDeniedPath = "/Auth/Error";
                 });

            services.AddAuthorization(config =>
            {
                config.AddPolicy("UserPolicy", policyBuilder =>
                {
                    policyBuilder.UserRequireCustomClaim(ClaimTypes.Email);
                });
            });

            //services.AddScoped<IAuthorizationHandler, PoliciesAuthorizationHandler>();
            //services.AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();

            services.AddSession();

            //For cacheing purpose
            //services.AddResponseCaching();
            

            

            //services.AddNCacheDistributedCache(options =>
            //{
            //    options.CacheName = "PrivateCache";
            //    options.EnableLogs = true;
            //    options.ExceptionsEnabled = true;
            //    //options.CacheName = "PrivateCache";
            //    //_ = (new CacheProfile()
            //    //{
            //    //    NoStore = true,
            //    //    Duration = 0,
            //    //    //Location = ResponseCacheLocation.None
            //    //});
            //});
            services.AddControllers();
            services.AddControllersWithViews();

            services.AddDistributedMemoryCache();

            services.AddRazorPages().AddRazorRuntimeCompilation();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Entity.EFContext.SMContext schoolManagementContext)
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
                app.UseHsts();
            }
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            //app.UseResponseCaching();

            app.UseCookiePolicy(cookiePolicyOptions);

            // who are you?  
            app.UseAuthentication();

            // are you allowed?  
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
