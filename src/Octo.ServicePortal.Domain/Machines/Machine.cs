using Octo.ServicePortal.Domain.Customers;
using Octo.ServicePortal.Domain.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octo.ServicePortal.Domain.Machines
{
	public class Machine
	{
		public int Id { get; private set; }
		public string ModelName { get; private set; }
		public string SerialNumber { get; private set; }
		public CompanyCustomer CompanyCustomer { get; private set; }
		public int CompanyCustomerId { get; private set; }
		public DateTime CreatedAt { get; private set; }
		public DateTime UpdatedAt { get; private set; }

		private readonly List<ServiceRequest> _serviceRequests = new();
		public IReadOnlyCollection<ServiceRequest> ServiceRequests => _serviceRequests.AsReadOnly();

		private Machine()
		{
			ModelName = string.Empty;
			SerialNumber = string.Empty;
			CompanyCustomer = null!;
		}

		public Machine(
			int companyCustomerId,
			string modelName,
			string serialNumber
			)
		{
			if (companyCustomerId <= 0)
			{
				throw new ArgumentException($"Company ID must be valid.", nameof(companyCustomerId));
			}

			ValidateRequiredFields(modelName, nameof(modelName));
			ValidateRequiredFields(serialNumber, nameof(serialNumber));

			CompanyCustomerId = companyCustomerId;
			ModelName = modelName;
			SerialNumber = serialNumber;
			CreatedAt = DateTime.UtcNow;

			CompanyCustomer = null!;
		}

		public void UpdateModelName(string modelName)
		{
			ValidateRequiredFields(modelName, nameof(modelName));
			ModelName = modelName;
			UpdatedAt = DateTime.UtcNow;
		}


		private static void ValidateRequiredFields(string value, string fieldName)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException($"{fieldName} is required and cannot be empty.");
			}
		}

	}
}
