
using ArchiProject.BL.Services;
using ArchiProject.DAL.EF;
using ArchiProject.DAL.Interfaces;
using ArchiProject.DAL.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ArchiProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {

            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<UserContext>(options => options.UseSqlServer(connection));
            services.AddControllersWithViews();


            services.AddSingleton<Profile, ArchiProject.DAL.Mapping.UserProfile>();
               
            services.AddTransient<BL.Interfaces.IAuthorizationService, AuthorizationService >();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddSingleton(x =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfiles(x.GetServices<Profile>());
                });
                config.AssertConfigurationIsValid();
                return config.CreateMapper(x.GetService);
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });
            services.AddControllersWithViews();

        }


        public void Configure(IApplicationBuilder app, UserContext context)
        {
            context.Database.EnsureCreated();

            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();    // аутентификация
            app.UseAuthorization();     // авторизация


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
