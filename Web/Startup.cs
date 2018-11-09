using AutoMapper;
using Core.Business.Models.Interfaces;
using Core.Business.Models.Repository;
using Core.Domain.Business.Models;
using Core.Models.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using Web.AutoMapper;

namespace Web
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

			services.AddMvc();
			services.AddDbContext<BaseModel>(options =>
			options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<User, IdentityRole>()
				.AddEntityFrameworkStores<BaseModel>()
				.AddDefaultTokenProviders();

			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<ICarRepository, CarRepository>();
			services.AddScoped<IShowroomRepository, ShowroomRepository>();

			services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

			services.AddLocalization(o => o.ResourcesPath = "Resources");
			services.Configure<RequestLocalizationOptions>(options =>
			{
				var supportedCultures = new[]
				{
					new CultureInfo("en-US"),
					new CultureInfo("en-GB"),
					new CultureInfo("de-DE")
				};
				options.DefaultRequestCulture = new RequestCulture("en-US", "en-US");

				// You must explicitly state which cultures your application supports.
				// These are the cultures the app supports for formatting 
				// numbers, dates, etc.

				options.SupportedCultures = supportedCultures;

				// These are the cultures the app supports for UI strings, 
				// i.e. we have localized resources for.

				options.SupportedUICultures = supportedCultures;
			});

            services.AddAutoMapper();
            //services.AddAutoMapper(x => x.AddProfile(new MappingEntity()));

            services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Login";
				options.LogoutPath = "/Logout";
			});

			services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequireDigit = false;
				options.Password.RequiredLength = 8;
				options.Password.RequireUppercase = false;
				options.Password.RequireNonAlphanumeric = false;
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseAuthentication();

			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
