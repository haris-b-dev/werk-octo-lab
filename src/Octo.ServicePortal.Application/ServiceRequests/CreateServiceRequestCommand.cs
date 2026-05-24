using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octo.ServicePortal.Application.ServiceRequests
{
	public class CreateServiceRequestCommand
	{
		public string Title { get; init; } = string.Empty;
		public string Description { get; init; } = string.Empty;
		public int CompanyCustomerId { get; init; }
		public int MachineId { get; init; }
		public int Type { get; init; }

	}
}
