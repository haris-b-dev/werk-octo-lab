using Microsoft.Extensions.DependencyInjection;
using Octo.ServicePortal.Application.ServiceRequests;

namespace Octo.ServicePortal.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddScoped<CreateServiceRequestService>();
			services.AddScoped<ServiceRequestTypeProvider>();

			return services;
		}
	}
}
