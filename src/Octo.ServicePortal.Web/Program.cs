using Octo.ServicePortal.Application;
using Octo.ServicePortal.Infrastructure;
using Octo.ServicePortal.Infrastructure.Persistance;

namespace Octo.ServicePortal.Web
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddApplication();
			builder.Services.AddInfrastructure(builder.Configuration);

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			if (app.Environment.IsDevelopment())
			{
				using var scope = app.Services.CreateScope();
				var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
				await DevelopmentDataSeed.SeedAsync(db);
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
