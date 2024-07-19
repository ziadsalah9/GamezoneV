using Microsoft.AspNetCore.Mvc.Rendering;

namespace GamezoneV.Services
{
	public interface IDevicesService
	{

		public IEnumerable<SelectListItem> GetListItems();
	}
}
