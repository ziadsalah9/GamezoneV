using GamezoneV.Data;
using GamezoneV.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GamezoneV.Services
{
	public class CategoriesService : ICategoriesService
	{

		private readonly AppDbContext _context;

        public CategoriesService(AppDbContext _context)
        {
			this._context = _context;   
        }
        public IEnumerable<SelectListItem> GetSelectList()
		{

			var result = _context.Categories.Select(p=>new SelectListItem { Value = p.Id.ToString() ,  Text = p.Name}).OrderBy(p=>p.Text).AsNoTracking().ToList();
			return result;
		
			
		}
	}
}
