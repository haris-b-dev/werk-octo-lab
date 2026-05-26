using Microsoft.EntityFrameworkCore;
using Octo.ServicePortal.Domain.Customers;
using Octo.ServicePortal.Domain.Machines;

namespace Octo.ServicePortal.Infrastructure.Persistance
{
	public class DevelopmentDataSeed
	{
		public static async Task SeedAsync(AppDbContext db)
		{
			if (await db.CompanyCustomers.AnyAsync())
				return;

			var companyCustomer1 = new CompanyCustomer(
				"X Example GmbH",
				"Example Street 123," +
				" 12345 Example City",
				"John Doe",
				"example@xexample.com"
				);

			await db.CompanyCustomers.AddAsync(companyCustomer1);
			await db.SaveChangesAsync();

			var machine1 = new Machine(
				"CNC Example Model A",
				"123AAXX456",
				companyCustomer1.Id
				);

			await db.Machines.AddAsync(machine1);
			await db.SaveChangesAsync();
		}
	}
}
