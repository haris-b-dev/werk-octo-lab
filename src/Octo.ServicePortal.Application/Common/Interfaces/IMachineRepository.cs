using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octo.ServicePortal.Application.Common.Interfaces
{
	public interface IMachineRepository
	{
		Task<bool> ExistsAsync(int machineId, CancellationToken cancellationToken = default);
		Task<bool> BelongsToCustomerAsync(int machineId, int companyCustomerId, CancellationToken cancellationToken = default);
	}
}
