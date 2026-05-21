using Octo.ServicePortal.Domain.Customers;
using Octo.ServicePortal.Domain.Machines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octo.ServicePortal.Domain.ServiceRequests
{
	public class ServiceRequest
	{
		public int Id { get; private set; }
		public string Title { get; private set; }
		public ServiceRequestStatus ServiceRequestStatus { get; private set; }
		public ServiceRequestType ServiceRequestType { get; private set; }
		public CompanyCustomer CompanyCustomer { get; private set; }
		public int CompanyCustomerId { get; private set; }
		public Machine Machine { get; private set; }
		public int MachineId { get; private set; }
		public string ServiceRequestDescription { get; private set; }
		public DateTime CreatedAt { get; private set; }
		public DateTime? UpdatedAt { get; private set; }

		private ServiceRequest()
		{
			Title = string.Empty;
			ServiceRequestDescription = string.Empty;
			CompanyCustomer = null!;
			Machine = null!;
		}

		public ServiceRequest(
			string title,
			string serviceRequestDescription,
			ServiceRequestType serviceRequestType,
			int companyCustomerId,
			int machineId
			)
		{
			ValidateRequiredField(title, nameof(title));
			ValidateRequiredField(serviceRequestDescription, nameof(serviceRequestDescription));
			ValidateRequiredField(serviceRequestType.ToString(), nameof(serviceRequestType));

			if (companyCustomerId <= 0)
			{
				throw new ArgumentException($"Company ID must be valid", nameof(companyCustomerId));
			}

			if (machineId <= 0)
			{
				throw new ArgumentException($"Machine ID must be valid", nameof(machineId));
			}

			CreatedAt = DateTime.UtcNow;
			ServiceRequestStatus = ServiceRequestStatus.New;
			Title = title;
			ServiceRequestDescription = serviceRequestDescription;
			ServiceRequestType = serviceRequestType;
			CompanyCustomerId = companyCustomerId;
			MachineId = machineId;
			CompanyCustomer = null!;
			Machine = null!;
		}

		private static void ValidateRequiredField(string value, string fieldName)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException($"{fieldName} is required and cannot be empty.");
			}
		}

		public void StartProcessingServiceRequest()
		{
			if (ServiceRequestStatus == ServiceRequestStatus.Closed)
			{
				throw new InvalidOperationException("Cloesd service request cannot be processed.");
			}

			ServiceRequestStatus = ServiceRequestStatus.InProgress;
			UpdatedAt = DateTime.UtcNow;
		}
	}
}
