using Octo.ServicePortal.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octo.ServicePortal.Application.ServiceRequests.ListServiceRequests
{
	public sealed class GetServiceRequestsService
	{
		private readonly IServiceRequestRepository _serviceRequestRepository;
		public GetServiceRequestsService(IServiceRequestRepository serviceRequestRepository)
		{
			_serviceRequestRepository = serviceRequestRepository;
		}

		public async Task<IReadOnlyList<ServiceRequestListItemDto>> HandleAsync(CancellationToken cancellationtoken = default)
		{
			return await _serviceRequestRepository.GetListAsync(cancellationtoken);
		}
	}
}
