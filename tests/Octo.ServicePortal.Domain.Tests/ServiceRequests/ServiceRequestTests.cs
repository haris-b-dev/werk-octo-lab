using Octo.ServicePortal.Domain.ServiceRequests;

namespace Octo.ServicePortal.Domain.Tests.ServiceRequests
{
	public class ServiceRequestTests
	{
		[Test]
		public void Constructor_ShouldCreateServiceRequest_WhenDataIsValid()
		{
			var title = "Machine spindle issue";
			var serviceRequestType = ServiceRequestType.Maintenance;
			var companyCustomerId = 1;
			var machineId = 15;
			var serviceRequestDescription = "The spindle vibrates excessively during operation.";

			var serviceRequest = new ServiceRequest(
				title,
				serviceRequestDescription,
				serviceRequestType,
				companyCustomerId,
				machineId
				);

			// Assert
			Assert.That(serviceRequest.Id, Is.EqualTo(0));
			Assert.That(serviceRequest.Title, Is.EqualTo(title));
			Assert.That(serviceRequest.ServiceRequestDescription, Is.EqualTo(serviceRequestDescription));
			Assert.That(serviceRequest.ServiceRequestType, Is.EqualTo(serviceRequestType));
			Assert.That(serviceRequest.ServiceRequestStatus, Is.EqualTo(ServiceRequestStatus.New));
			Assert.That(serviceRequest.CompanyCustomerId, Is.EqualTo(companyCustomerId));
			Assert.That(serviceRequest.MachineId, Is.EqualTo(machineId));

			Assert.That(serviceRequest.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(10).Seconds);
			Assert.That(serviceRequest.UpdatedAt, Is.Null);

			Assert.That(serviceRequest.CompanyCustomer, Is.Null);
			Assert.That(serviceRequest.Machine, Is.Null);
		}

		[Test]
		public void Constructor_ShouldThrowArgumentException_WhenCompanyCustomerIdIsInvalid()
		{
			var title = "Machine x spindle issue";
			var serviceRequestType = ServiceRequestType.Maintenance;
			var companyCustomerId = 0;
			var machineId = 20;
			var serviceRequestDescription = "The spindle throws error during operation.";

			var exception = Assert.Throws<ArgumentException>(() =>
				new ServiceRequest(
				title,
				serviceRequestDescription,
				serviceRequestType,
				companyCustomerId,
				machineId
				));

			Assert.That(exception!.ParamName, Is.EqualTo("companyCustomerId"));		
		}

		[Test]
		public void Constructor_ShouldThrowArgumentException_WhenMachineIdIsInvalid()
		{
			var title = "Machine y spindle issue";
			var serviceRequestType = ServiceRequestType.Maintenance;
			var companyCustomerId = 35;
			var machineId = 0;
			var serviceRequestDescription = "The spindle throws error X during operation.";

			var exception = Assert.Throws<ArgumentException>(() =>
			new ServiceRequest(
				title,
				serviceRequestDescription,
				serviceRequestType,
				companyCustomerId,
				machineId
				));

			Assert.That(exception!.ParamName, Is.EqualTo("machineId"));
		}

		[Test]
		public void Constructor_ShouldThrowArgumentException_WhenTitleIsEmpty()
		{
			var title = "";
			var serviceRequestType = ServiceRequestType.Maintenance;
			var companyCustomerId = 432;
			var machineId = 123;
			var serviceRequestDescription = "The PLC throws error H during operation.";

			var exception = Assert.Throws<ArgumentException>(() =>
			new ServiceRequest(
				title,
				serviceRequestDescription,
				serviceRequestType,
				companyCustomerId,
				machineId
				));

			Assert.That(exception!.ParamName, Is.EqualTo("title"));
		}

		[Test]
		public void Constructor_ShouldThrowArgumentException_WhenDescriptionIsEmpty()
		{
			var title = "Machine maintenance";
			var serviceRequestType = ServiceRequestType.Maintenance;
			var companyCustomerId = 232;
			var machineId = 63;
			var serviceRequestDescription = "";

			var exception = Assert.Throws<ArgumentException>(() =>
			new ServiceRequest(
				title,
				serviceRequestDescription,
				serviceRequestType,
				companyCustomerId,
				machineId
				));

			Assert.That(exception!.ParamName, Is.EqualTo("serviceRequestDescription"));
		}

		[Test]
		public void Constructor_ShouldSetStatusToNew_WhenServiceRequestIsCreated()
		{
			var serviceRequest = new ServiceRequest(
				"Issue x",
				"Description X.",
				ServiceRequestType.Failure,
				10,
				5
				);

			Assert.That(serviceRequest.ServiceRequestStatus, Is.EqualTo(ServiceRequestStatus.New));
		}

		[Test]
		public void StartProcessingServiceRequest_ShouldSetServiceRequestStatusInProgress()
		{
			var serviceRequest = new ServiceRequest(
				"Issue y",
				"Description T.",
				ServiceRequestType.Other,
				10,
				5
				);

			serviceRequest.StartProcessingServiceRequest();

			Assert.That(serviceRequest.ServiceRequestStatus, Is.EqualTo(ServiceRequestStatus.InProgress));
			Assert.That(serviceRequest.UpdatedAt, Is.Not.Null);
		}
	}
}