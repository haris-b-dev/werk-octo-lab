using Microsoft.Extensions.DependencyInjection;
using Octo.ServicePortal.Application.ServiceRequests;
using Octo.ServicePortal.Application.ServiceRequests.ListServiceRequests;

namespace Octo.ServicePortal.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddScoped<CreateServiceRequestService>();
			services.AddScoped<GetServiceRequestsService>();
			services.AddScoped<ServiceRequestTypeProvider>();

			return services;
		}
	}
}
