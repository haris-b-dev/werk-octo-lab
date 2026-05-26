using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Octo.ServicePortal.Application.Common.Interfaces;
using Octo.ServicePortal.Infrastructure.Persistance;
using Octo.ServicePortal.Infrastructure.Repositories;

namespace Octo.ServicePortal.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

			services.AddScoped<ICompanyCustomerRepository, CompanyCustomerRepository>();
			services.AddScoped<IMachineRepository, MachineRepository>();
			services.AddScoped<IServiceRequestRepository, ServiceRequestRepository>();

			return services;
		}
	}
}
