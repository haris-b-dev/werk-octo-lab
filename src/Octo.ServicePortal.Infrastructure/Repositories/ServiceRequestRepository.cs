using Octo.ServicePortal.Application.Common.Interfaces;
using Octo.ServicePortal.Domain.ServiceRequests;
using Octo.ServicePortal.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octo.ServicePortal.Infrastructure.Repositories
{
	public class ServiceRequestRepository : IServiceRequestRepository
	{
		private readonly AppDbContext _db;

		public ServiceRequestRepository(AppDbContext db)
		{
			_db = db;
		}
		public async Task AddAsync(ServiceRequest serviceRequest, CancellationToken cancellationToken = default)
		{
			await _db.ServiceRequests.AddAsync(serviceRequest, cancellationToken);
			await _db.SaveChangesAsync(cancellationToken);
		}
	}
}
