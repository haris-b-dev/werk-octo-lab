using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octo.ServicePortal.Application.Common.Interfaces
{
	public interface ICompanyCustomerRepository
	{
		Task<bool> ExistsAsync(int companyCustomerId, CancellationToken cancellationToken = default);
	}
}
