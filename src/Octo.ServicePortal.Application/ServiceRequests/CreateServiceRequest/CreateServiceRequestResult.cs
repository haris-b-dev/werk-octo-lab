using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octo.ServicePortal.Application.ServiceRequests.CreateServiceRequest
{
	public class CreateServiceRequestResult
	{
		public int ServiceRequestId { get; init; }
		public string Status { get; init; } = string.Empty;
	}
}
