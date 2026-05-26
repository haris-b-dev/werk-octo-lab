using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Octo.ServicePortal.Domain.ServiceRequests;

namespace Octo.ServicePortal.Infrastructure.Persistance.Configurations
{
	public class ServiceRequestConfiguration : IEntityTypeConfiguration<ServiceRequest>
	{
		public void Configure(EntityTypeBuilder<ServiceRequest> builder)
		{
			builder.HasKey(serviceRequest => serviceRequest.Id);
			builder.Property(serviceRequest => serviceRequest.Title).IsRequired().HasMaxLength(400);
			builder.Property(serviceRequest => serviceRequest.ServiceRequestStatus).IsRequired();
			builder.Property(serviceRequest => serviceRequest.ServiceRequestType).IsRequired();
			builder.Property(serviceRequest => serviceRequest.CreatedAt).IsRequired();
			builder.Property(serviceRequest => serviceRequest.UpdatedAt);

			builder.HasOne(serviceRequest => serviceRequest.CompanyCustomer)
				   .WithMany(customer => customer.ServiceRequests)
				   .HasForeignKey(serviceRequest => serviceRequest.CompanyCustomerId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(serviceRequest => serviceRequest.Machine)
				   .WithMany(machine => machine.ServiceRequests)
				   .HasForeignKey(serviceRequest => serviceRequest.MachineId)
				   .OnDelete(DeleteBehavior.Restrict);
		}
	}
}
