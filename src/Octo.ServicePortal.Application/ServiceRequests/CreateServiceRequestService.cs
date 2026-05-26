using Octo.ServicePortal.Application.Common.Interfaces;
using Octo.ServicePortal.Application.ServiceRequests.CreateServiceRequest;
using Octo.ServicePortal.Domain.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octo.ServicePortal.Application.ServiceRequests
{
	public sealed class CreateServiceRequestService
	{
		private readonly ICompanyCustomerRepository _companyCustomerRepository;
		private readonly IMachineRepository _machineRepository;
		private readonly IServiceRequestRepository _serviceRequestRepository;

		public CreateServiceRequestService(
			ICompanyCustomerRepository companyCustomrtRepository,
			IMachineRepository machineRepository,
			IServiceRequestRepository serviceRequestRepository)
		{
			_companyCustomerRepository = companyCustomrtRepository;
			_machineRepository = machineRepository;
			_serviceRequestRepository = serviceRequestRepository;
		}

		public async Task<CreateServiceRequestResult> HandleAsync(CreateServiceRequestCommand command, CancellationToken cancellationToken = default)
		{
			var customerExists = await _companyCustomerRepository.ExistsAsync(command.CompanyCustomerId, cancellationToken);
			if (!customerExists)
				throw new InvalidOperationException($"Company customer with ID {command.CompanyCustomerId} does not exist.");

			var machineExists = await _machineRepository.ExistsAsync(command.MachineId, cancellationToken);
			if (!machineExists)
				throw new InvalidOperationException($"Machine with ID {command.MachineId} does not exist.");

			var machineBelongsToCustomer = await _machineRepository.BelongsToCustomerAsync(command.MachineId, command.CompanyCustomerId, cancellationToken);
			if (!machineBelongsToCustomer)
				throw new InvalidOperationException($"Machine with ID {command.MachineId} does not belong to company customer with ID {command.CompanyCustomerId}");

			var serviceRequest = new ServiceRequest(
				command.Title,
				command.Description,
				(ServiceRequestType)command.Type,
				command.CompanyCustomerId,
				command.MachineId);

			await _serviceRequestRepository.AddAsync(serviceRequest, cancellationToken);

			return new CreateServiceRequestResult
			{
				ServiceRequestId = serviceRequest.Id,
				Status = serviceRequest.ServiceRequestStatus.ToString()
			};
		}
	}
}
