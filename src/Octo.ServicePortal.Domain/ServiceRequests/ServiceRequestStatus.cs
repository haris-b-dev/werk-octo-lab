using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octo.ServicePortal.Domain.ServiceRequests
{
	public enum ServiceRequestStatus
	{
		New = 0,
		InProgress = 1,
		Resolved = 2,
		Closed = 3
	}
}
