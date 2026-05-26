using Octo.ServicePortal.Domain.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octo.ServicePortal.Application.ServiceRequests
{
	public sealed class ServiceRequestTypeProvider
	{
		public IReadOnlyCollection<ServiceRequestTypeOption> GetOptions()
		{
			return Enum.GetValues<ServiceRequestType>().Select(type => new ServiceRequestTypeOption
			{
				Value = (int)type,
				Text = type.ToString()
			}).ToList();
		}
	}
}
