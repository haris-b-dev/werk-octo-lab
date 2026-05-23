using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octo.ServicePortal.Domain.ServiceRequests
{
	public enum ServiceRequestType
	{
		Failure = 0,
		Maintenance = 1,
		Installation = 2,
		Purchase = 3,
		Sales = 4,
		Other = 5
	}
}
