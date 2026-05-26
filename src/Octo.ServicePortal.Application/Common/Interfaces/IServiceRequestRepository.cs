using Octo.ServicePortal.Application.ServiceRequests.ListServiceRequests;
using Octo.ServicePortal.Domain.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octo.ServicePortal.Application.Common.Interfaces
{
	public interface IServiceRequestRepository
	{
		Task AddAsync(ServiceRequest serviceRequest, CancellationToken cancellationToken = default);
		Task<IReadOnlyList<ServiceRequestListItemDto>> GetListAsync(CancellationToken cancellationToken = default);
	}
}
