using GamezoneV.CustomAttributes;
using GamezoneV.Settings;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GamezoneV.ViewModels
{
	public class EditGameFormViewModel
	{
		public int Id { get; set; }

		[MaxLength(255)]
		public string Name { get; set; } = string.Empty;



		public int categoryId { get; set; }

		public IEnumerable<SelectListItem> Categoties { get; set; } = Enumerable.Empty<SelectListItem>();



		public List<int> SelectedDevices { get; set; } = default!;
		public IEnumerable<SelectListItem> Devices = Enumerable.Empty<SelectListItem>();



		[MaxLength(2550)]
		public string Description { get; set; } = string.Empty;

		[AllowedExtension(FileSettings.AllowedExtensions), MaxFileSize(FileSettings.MaxFileSizeInBytes)]
		public IFormFile? Cover { get; set; } = default!;

        // to edit cover

        public string? CurrentCover { get; set; }
    }
}
