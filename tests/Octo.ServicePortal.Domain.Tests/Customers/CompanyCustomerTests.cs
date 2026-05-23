using Octo.ServicePortal.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octo.ServicePortal.Domain.Tests.Customers
{
	public class CompanyCustomerTests
	{
		[Test]
		public void Constructor_ShouldCreateCompanyCustomer_WhenDataIsValid()
		{
			var companyName = "Test x company";
			var companyAddress = "Teststreet 123, Test City";
			var contactPerson = "Test Person";
			var email = "test@company.com";

			var customer = new CompanyCustomer(
				companyName,
				companyAddress,
				contactPerson,
				email
				);

			Assert.That(customer.Id, Is.EqualTo(0));
			Assert.That(customer.CompanyName, Is.EqualTo(companyName));
			Assert.That(customer.CompanyAddress, Is.EqualTo(companyAddress));
			Assert.That(customer.ContactPerson, Is.EqualTo(contactPerson));
			Assert.That(customer.Email, Is.EqualTo(email));
			Assert.That(customer.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(10).Seconds);

			Assert.That(customer.ServiceRequests, Is.Empty);
			Assert.That(customer.Machines, Is.Empty);
		}

		[Test]
		public void Constructor_ShouldThrowArgumentException_WhenCompanyNameIsEmpty()
		{
			var companyName = "";
			var companyAddress = "Teststreet 123, Test City";
			var contactPerson = "Test Person";
			var email = "test@company.com";

			var exception = Assert.Throws<ArgumentException>(() =>
				new CompanyCustomer(
					companyName,
					companyAddress,
					contactPerson,
					email
					));

			Assert.That(exception!.ParamName, Is.EqualTo("companyName"));
		}

		[Test]
		public void Constructor_ShouldThrowArgumentException_WhenCompanyAdressIsEmpty()
		{
			var companyName = "Test Company x";
			var companyAddress = "";
			var contactPerson = "Test Person";
			var email = "test@company.com";

			var exception = Assert.Throws<ArgumentException>(() =>
				new CompanyCustomer(
					companyName,
					companyAddress,
					contactPerson,
					email
					));

			Assert.That(exception!.ParamName, Is.EqualTo("companyAddress"));
		}

		[Test]
		public void Constructor_ShouldThrowArgumentException_WhenContactPersonIsEmpty()
		{
			var companyName = "Test Company x";
			var companyAddress = "Teststreet 123, Test City";
			var contactPerson = "";
			var email = "test@company.com";

			var exception = Assert.Throws<ArgumentException>(() =>
				new CompanyCustomer(
					companyName,
					companyAddress,
					contactPerson,
					email
					));

			Assert.That(exception!.ParamName, Is.EqualTo("contactPerson"));
		}

		[Test]
		public void Constructor_ShouldThrowArgumentException_WhenEmailIsEmpty()
		{
			var companyName = "Test Company x";
			var companyAddress = "Teststreet 123, Test City";
			var contactPerson = "Test Person";
			var email = "";

			var exception = Assert.Throws<ArgumentException>(() =>
				new CompanyCustomer(
					companyName,
					companyAddress,
					contactPerson,
					email
					));

			Assert.That(exception!.ParamName, Is.EqualTo("email"));
		}

		[Test]
		public void UpdateContact_ShouldUpdateContactPersonAndEmail_WhenDataIsValid()
		{
			var companyName = "Test Company x";
			var companyAddress = "Teststreet 123, Test City";
			var contactPerson = "Test Person";
			var email = "test@testmail.com";

			var customer = new CompanyCustomer(
				companyName,
				companyAddress,
				contactPerson,
				email
				);

			var newContactPerson = "Updated Person";
			var newEmail = "new@testemail.com";

			customer.UpdateContact(newContactPerson, newEmail);

			Assert.That(customer.ContactPerson, Is.EqualTo(newContactPerson));
			Assert.That(customer.Email, Is.EqualTo(newEmail));
		}

		[Test]
		public void UpdateAddress_ShouldUpdateCompanyAddress_WhenDataIsValid()
		{
			var companyName = "Test Company x";
			var companyAddress = "Teststreet 123, Test City";
			var contactPerson = "Test Person";
			var email = "test@testmail.com";

			var customer = new CompanyCustomer(
				companyName,
				companyAddress,
				contactPerson,
				email
				);

			var newCompanyAddress = "Updated Address";

			customer.UpdateAddress(newCompanyAddress);

			Assert.That(customer.CompanyAddress, Is.EqualTo(newCompanyAddress));
		}
	}
}
