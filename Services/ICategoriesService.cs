using Microsoft.AspNetCore.Mvc.Rendering;

namespace GamezoneV.Services
{
	public interface ICategoriesService
	{

		IEnumerable<SelectListItem> GetSelectList();

	}
}
