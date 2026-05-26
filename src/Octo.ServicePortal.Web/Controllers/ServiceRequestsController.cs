using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Octo.ServicePortal.Application.ServiceRequests;
using Octo.ServicePortal.Application.ServiceRequests.ListServiceRequests;
using Octo.ServicePortal.Web.ViewModels.ServiceRequests;

namespace Octo.ServicePortal.Web.Controllers
{
	public class ServiceRequestsController : Controller
	{
		private readonly CreateServiceRequestService _createServiceRequestService;
		private readonly ServiceRequestTypeProvider _serviceRequestTypeProvider;
		private readonly GetServiceRequestsService _getServiceRequestsService;

		public ServiceRequestsController(
			CreateServiceRequestService createServiceRequestService,
			ServiceRequestTypeProvider serviceRequestTypeProvider,
			GetServiceRequestsService getServiceRequestsService
			)
		{
			_createServiceRequestService = createServiceRequestService;
			_serviceRequestTypeProvider = serviceRequestTypeProvider;
			_getServiceRequestsService = getServiceRequestsService;
		}

		private List<SelectListItem> GetServiceRequestTypeSelectList()
		{
			return _serviceRequestTypeProvider.GetOptions().Select(option => new SelectListItem
			{
				Value = option.Value.ToString(),
				Text = option.Text
			}).ToList();
		}

		[HttpGet]
		public async Task<IActionResult> Index(CancellationToken cancellationToken)
		{
			var viewModel = await _getServiceRequestsService.HandleAsync(cancellationToken);
			return View(viewModel);
		}

		[HttpGet]
		public IActionResult Create()
		{
			var viewModel = new CreateServiceRequestViewModel
			{
				ServiceRequestTypes = GetServiceRequestTypeSelectList()
			};

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateServiceRequestViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				viewModel.ServiceRequestTypes = GetServiceRequestTypeSelectList();
				return View(viewModel);
			}

			var command = new CreateServiceRequestCommand()
			{
				Title = viewModel.Title,
				Type = viewModel.Type,
				CompanyCustomerId = viewModel.CompanyCustomerId,
				MachineId = viewModel.MachineId,
				Description = viewModel.Description

			};

			try
			{
				await _createServiceRequestService.HandleAsync(command);
				TempData["SuccessMessage"] = "Service request created successfully.";
				return RedirectToAction(nameof(Create));
			}
			catch(InvalidOperationException ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
				viewModel.ServiceRequestTypes = GetServiceRequestTypeSelectList();
				return View(viewModel);
			}
		}
	}
}
