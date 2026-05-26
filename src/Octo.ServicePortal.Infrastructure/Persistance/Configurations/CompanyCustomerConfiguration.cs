using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Octo.ServicePortal.Domain.Customers;

namespace Octo.ServicePortal.Infrastructure.Persistance.Configurations
{
	public class CompanyCustomerConfiguration : IEntityTypeConfiguration<CompanyCustomer>
	{
		public void Configure(EntityTypeBuilder<CompanyCustomer> builder)
		{
			builder.HasKey(companyCustomer => companyCustomer.Id);

			builder.Property(companyCustomer => companyCustomer.CompanyName).IsRequired().HasMaxLength(300);
			builder.Property(companyCustomer => companyCustomer.CompanyAddress).IsRequired().HasMaxLength(600);
			builder.Property(companyCustomer => companyCustomer.ContactPerson).IsRequired().HasMaxLength(300);
			builder.Property(companyCustomer => companyCustomer.Email).IsRequired().HasMaxLength(200);
			builder.Property(companyCustomer => companyCustomer.CreatedAt).IsRequired();
		}
	}
}
