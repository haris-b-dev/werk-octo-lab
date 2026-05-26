using Microsoft.AspNetCore.Mvc.Rendering;

namespace Octo.ServicePortal.Web.ViewModels.ServiceRequests
{
	public class CreateServiceRequestViewModel
	{
		public string Title { get; set; } = string.Empty;
		public int Type { get; set; }
		public int CompanyCustomerId { get; set; }
		public int MachineId { get; set; }
		public string Description { get; set; } = string.Empty;
		public List<SelectListItem> ServiceRequestTypes { get; set; } = new();
		public List<SelectListItem> Machines { get; set; } = new();
	}
}
