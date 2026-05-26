using Microsoft.EntityFrameworkCore;
using Octo.ServicePortal.Application.Common.Interfaces;
using Octo.ServicePortal.Application.ServiceRequests.ListServiceRequests;
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

		public async Task<IReadOnlyList<ServiceRequestListItemDto>> GetListAsync(CancellationToken cancellationToken = default)
		{
			return await _db.ServiceRequests
				.AsNoTracking().OrderByDescending(sr => sr.CreatedAt).Select(sr => new ServiceRequestListItemDto()
			{
				Id = sr.Id,
				Titel = sr.Title,
				Status = sr.ServiceRequestStatus.ToString(),
				Type = sr.ServiceRequestType.ToString(),
				CompanyCustomerId = sr.CompanyCustomerId,
				MachineId = sr.MachineId,
				Description = sr.ServiceRequestDescription,
				CreatedAt = sr.CreatedAt
			}).ToListAsync(cancellationToken);
		}
	}
}
