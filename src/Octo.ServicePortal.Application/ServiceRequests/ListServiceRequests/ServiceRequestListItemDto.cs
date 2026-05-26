
namespace Octo.ServicePortal.Application.ServiceRequests.ListServiceRequests
{
	public sealed class ServiceRequestListItemDto
	{
		public int Id { get; init; }
		public string Titel { get; init; } = string.Empty;
		public string Status { get; init; } = string.Empty;
		public string Type { get; init; } = string.Empty;
		public int CompanyCustomerId { get; init; }
		public int MachineId { get; init; }
		public string Description { get; init; } = string.Empty;
		public DateTime CreatedAt { get; init; }
	}
}
