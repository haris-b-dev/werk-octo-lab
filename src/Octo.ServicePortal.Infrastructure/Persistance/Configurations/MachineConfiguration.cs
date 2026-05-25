using Microsoft.EntityFrameworkCore;
using Octo.ServicePortal.Domain.Machines;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Octo.ServicePortal.Infrastructure.Persistance.Configurations
{
	public class MachineConfiguration : IEntityTypeConfiguration<Machine>
	{
		public void Configure(EntityTypeBuilder<Machine> builder)
		{
			builder.HasKey(machine => machine.Id);
			builder.Property(machine => machine.ModelName).IsRequired().HasMaxLength(300);
			builder.Property(machine => machine.SerialNumber).IsRequired().HasMaxLength(100);
			builder.Property(machine => machine.CreatedAt).IsRequired();

			builder.HasOne(machine => machine.CompanyCustomer)
				   .WithMany(companyCustomer => companyCustomer.Machines)
				   .HasForeignKey(machine => machine.CompanyCustomerId)
				   .OnDelete(DeleteBehavior.Restrict);
		}
	}
}
