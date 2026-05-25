using Microsoft.EntityFrameworkCore;
using Octo.ServicePortal.Domain.Customers;
using Octo.ServicePortal.Domain.ServiceRequests;
using Octo.ServicePortal.Domain.Machines;

namespace Octo.ServicePortal.Infrastructure.Persistance
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{			
		}

		public DbSet<CompanyCustomer> CompanyCustomers => Set<CompanyCustomer>();
		public DbSet<Machine> Machines => Set<Machine>();
		public DbSet<ServiceRequest> ServiceRequests => Set<ServiceRequest>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
		}
	}
}
