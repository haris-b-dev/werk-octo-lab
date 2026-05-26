using Microsoft.EntityFrameworkCore;
using Octo.ServicePortal.Application.Common.Interfaces;
using Octo.ServicePortal.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octo.ServicePortal.Infrastructure.Repositories
{
	public class MachineRepository : IMachineRepository
	{
		private readonly AppDbContext _db;

		public MachineRepository(AppDbContext db)
		{
			_db = db;
		}

		public async Task<bool> ExistsAsync(int machineId, CancellationToken cancellationToken = default)
		{
			return await _db.Machines.AnyAsync(machine => machine.Id == machineId, cancellationToken);
		}

		public async Task<bool> BelongsToCustomerAsync(int machineId, int companyCustomerId, CancellationToken cancellationToken = default)
		{
			return await _db.Machines.AnyAsync(machine =>
				machine.Id == machineId &&
				machine.CompanyCustomerId == companyCustomerId,
				cancellationToken
			);
		}
	}
}
