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
	public class CompanyCustomerRepository : ICompanyCustomerRepository
	{
		private readonly AppDbContext _db;

		public CompanyCustomerRepository(AppDbContext db)
		{
			_db = db;
		}

		public async Task<bool> ExistsAsync(int companyCustomerId, CancellationToken cancellationToken = default)
		{
			return await _db.CompanyCustomers.AnyAsync(companyCustomer => companyCustomer.Id == companyCustomerId, cancellationToken);
		}
	}
}
