using Moq;
using Octo.ServicePortal.Application.Common.Interfaces;
using Octo.ServicePortal.Application.ServiceRequests;
using Octo.ServicePortal.Domain.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octo.ServicePortal.Application.Tests.ServiceRequests.CreateServiceRequest
{
	public class CreateServiceRequestServiceTests
	{
		[Test]
		public async Task HandleAsync_ShouldCreateServiceRequest_WhenCommandIsValid()
		{
			var companyCustomerRepositoryMock = new Mock<ICompanyCustomerRepository>();
			var machineRepositoryMock = new Mock<IMachineRepository>();
			var serviceRequestRepositoryMock = new Mock<IServiceRequestRepository>();

			var command = new CreateServiceRequestCommand()
			{
				Title = "Machine spindle issue",
				Description = "The spindle of machine X is making unusual noise.",
				CompanyCustomerId = 1,
				MachineId = 1,
				Type = (int)ServiceRequestType.Maintenance
			};

			companyCustomerRepositoryMock
				.Setup(repo => repo.ExistsAsync(command.CompanyCustomerId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(true);

			machineRepositoryMock
				.Setup(repo => repo.ExistsAsync(command.MachineId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(true);

			machineRepositoryMock
				.Setup(repo => repo.BelongsToCustomerAsync(command.MachineId, command.CompanyCustomerId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(true);

			var createServiceRequestService = new CreateServiceRequestService(
				companyCustomerRepositoryMock.Object,
				machineRepositoryMock.Object,
				serviceRequestRepositoryMock.Object);

			var createServiceRequestResult = await createServiceRequestService.HandleAsync(command);

			Assert.That(createServiceRequestResult, Is.Not.Null);
			Assert.That(createServiceRequestResult.Status, Is.EqualTo(ServiceRequestStatus.New.ToString()));

			serviceRequestRepositoryMock.Verify(repo => repo.AddAsync(
				It.Is<ServiceRequest>(sr =>
				sr.Title == command.Title &&
				sr.ServiceRequestDescription == command.Description &&
				sr.CompanyCustomerId == command.CompanyCustomerId &&
				sr.MachineId == command.MachineId &&
				sr.ServiceRequestType == ServiceRequestType.Maintenance &&
				sr.ServiceRequestStatus == ServiceRequestStatus.New), It.IsAny<CancellationToken>()), Times.Once);

		}

		[Test]
		public async Task HandleAsync_ShouldThrowInvalidOperationException_WhenCustomerDoesNotExist()
		{
			var companyCustomerRepositoryMock = new Mock<ICompanyCustomerRepository>();
			var machineRepositoryMock = new Mock<IMachineRepository>();
			var serviceRequestRepositoryMock = new Mock<IServiceRequestRepository>();

			var command = new CreateServiceRequestCommand()
			{
				Title = "Machine spindle issue",
				Description = "The spindle of machine X is making unusual noise.",
				CompanyCustomerId = 1,
				MachineId = 1,
				Type = (int)ServiceRequestType.Maintenance
			};

			companyCustomerRepositoryMock
				.Setup(repo => repo.ExistsAsync(command.CompanyCustomerId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(false);

			var createServiceRequestService = new CreateServiceRequestService(
				companyCustomerRepositoryMock.Object,
				machineRepositoryMock.Object,
				serviceRequestRepositoryMock.Object);

			var exception = Assert.ThrowsAsync<InvalidOperationException>(() =>
							createServiceRequestService.HandleAsync(command));

			Assert.That(exception!.Message, Is.EqualTo($"Company customer with ID {command.CompanyCustomerId} does not exist."));

			machineRepositoryMock.Verify(repo => repo.ExistsAsync(
				It.IsAny<int>(),
				It.IsAny<CancellationToken>()), Times.Never);

			machineRepositoryMock.Verify(repo => repo.BelongsToCustomerAsync(
				It.IsAny<int>(),
				It.IsAny<int>(),
				It.IsAny<CancellationToken>()), Times.Never);

			serviceRequestRepositoryMock.Verify(repo => repo.AddAsync(
				It.IsAny<ServiceRequest>(),
				It.IsAny<CancellationToken>()), Times.Never);


		}

		[Test]
		public async Task HandleAsync_ShouldThrowInvalidOperationException_WhenMachineDoesNotExist()
		{
			var companyCustomerRepositoryMock = new Mock<ICompanyCustomerRepository>();
			var machineRepositoryMock = new Mock<IMachineRepository>();
			var serviceRequestRepositoryMock = new Mock<IServiceRequestRepository>();

			var command = new CreateServiceRequestCommand()
			{
				Title = "Machine spindle issue",
				Description = "The spindle of machine X is making unusual noise.",
				CompanyCustomerId = 1,
				MachineId = 10,
				Type = (int)ServiceRequestType.Maintenance
			};

			machineRepositoryMock
				.Setup(repo => repo.ExistsAsync(command.MachineId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(false);

			companyCustomerRepositoryMock
				.Setup(repo => repo.ExistsAsync(command.CompanyCustomerId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(true);

			var createServiceRequestService = new CreateServiceRequestService(
				companyCustomerRepositoryMock.Object,
				machineRepositoryMock.Object,
				serviceRequestRepositoryMock.Object);

			var exception = Assert.ThrowsAsync<InvalidOperationException>
				(() => createServiceRequestService.HandleAsync(command));

			Assert.That(exception!.Message, Is.EqualTo($"Machine with ID {command.MachineId} does not exist."));

			machineRepositoryMock.Verify(
				repo => repo.BelongsToCustomerAsync(
					It.IsAny<int>(),
					It.IsAny<int>(),
					It.IsAny<CancellationToken>()), Times.Never);

			serviceRequestRepositoryMock.Verify(
				repo => repo.AddAsync(
					It.IsAny<ServiceRequest>(),
					It.IsAny<CancellationToken>()), Times.Never);
		}

		[Test]
		public async Task HandleAsync_ShouldThrowInvalidOperationException_WhenMachineDoesNotBelongToCustomer()
		{
			var companyCustomerRepositoryMock = new Mock<ICompanyCustomerRepository>();
			var machineRepositoryMock = new Mock<IMachineRepository>();
			var serviceRequestRepositoryMock = new Mock<IServiceRequestRepository>();

			var command = new CreateServiceRequestCommand()
			{
				Title = "Machine spindle issue",
				Description = "The spindle of machine X is making unusual noise.",
				CompanyCustomerId = 1,
				MachineId = 10,
				Type = (int)ServiceRequestType.Maintenance
			};

			companyCustomerRepositoryMock
				.Setup(repo => repo.ExistsAsync(command.CompanyCustomerId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(true);

			machineRepositoryMock
				.Setup(repo => repo.ExistsAsync(command.MachineId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(true);

			machineRepositoryMock
				.Setup(repo => repo.BelongsToCustomerAsync(command.MachineId, command.CompanyCustomerId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(false);

			var createServiceRequestService = new CreateServiceRequestService(
				companyCustomerRepositoryMock.Object,
				machineRepositoryMock.Object,
				serviceRequestRepositoryMock.Object);

			var exception = Assert.ThrowsAsync<InvalidOperationException>
				(() => createServiceRequestService.HandleAsync(command));

			Assert.That(exception!.Message, Is.EqualTo($"Machine with ID {command.MachineId} does not belong to company customer with ID {command.CompanyCustomerId}"));
		}
	}
}
