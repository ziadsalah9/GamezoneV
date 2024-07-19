using GamezoneV.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GamezoneV.Services
{
	public class DevicesService : IDevicesService
	{
		private readonly AppDbContext _context;

        public DevicesService(AppDbContext _context)
        {
            this._context = _context;
        }
        public IEnumerable<SelectListItem> GetListItems()
		{
			return(_context.Devices.Select(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.Name }).OrderBy(p => p.Text).AsNoTracking().ToList());
		}
	}
}
