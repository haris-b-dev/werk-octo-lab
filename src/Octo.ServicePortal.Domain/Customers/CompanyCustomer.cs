using Octo.ServicePortal.Domain.Machines;
using Octo.ServicePortal.Domain.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octo.ServicePortal.Domain.Customers
{
	public class CompanyCustomer
	{
		public int Id { get; private set; }
		public string CompanyName { get; private set; }
		public string CompanyAddress { get; private set; }
		public string ContactPerson { get; private set; }
		public string Email { get; private set; }
		public DateTime CreatedAt { get; private set; }

		private readonly List<Machine> _machines = new();
		private readonly List<ServiceRequest> _serviceRequests = new();
		public IReadOnlyCollection<Machine> Machines => _machines.AsReadOnly();
		public IReadOnlyCollection<ServiceRequest> ServiceRequests => _serviceRequests.AsReadOnly();

		private CompanyCustomer()
		{
			CompanyName = string.Empty;
			CompanyAddress = string.Empty;
			ContactPerson = string.Empty;
			Email = string.Empty;
		}

		public CompanyCustomer(string companyName, string companyAddress, string contactPerson, string email)
		{
			CompanyName = companyName;
			CompanyAddress = companyAddress;
			ContactPerson = contactPerson;
			Email = email;
			CreatedAt = DateTime.UtcNow;

			ValidateRequiredField(CompanyName, nameof(companyName));
			ValidateRequiredField(CompanyAddress, nameof(companyAddress));
			ValidateRequiredField(ContactPerson, nameof(contactPerson));
			ValidateRequiredField(Email, nameof(email));
		}

		private static void ValidateRequiredField(string value, string fieldName)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException($"{fieldName}, is required and cannot be empty.", fieldName);
			}
		}

		public void UpdateContact(string contactPerson, string email)
		{
			ValidateRequiredField(contactPerson, nameof(contactPerson));
			ValidateRequiredField(email, nameof(email));

			ContactPerson = contactPerson;
			Email = email;
		}

		public void UpdateAddress(string address)
		{
			ValidateRequiredField(address, nameof(address));
			CompanyAddress = address;
		}
	}
}
