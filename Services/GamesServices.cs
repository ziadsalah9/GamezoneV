using GamezoneV.Data;
using GamezoneV.Models;
using GamezoneV.Settings;
using GamezoneV.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.IdentityModel.Abstractions;
using System.Security.AccessControl;

namespace GamezoneV.Services
{
	public class GamesServices : IGamesServices
	{

		private readonly AppDbContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public readonly string _ImagePath;
        public GamesServices(AppDbContext _context, IWebHostEnvironment _webHostEnvironment)
        {
            this._context = _context;
			this._webHostEnvironment = _webHostEnvironment;
			_ImagePath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }
        public async Task Create(CreateGameFormViewModel model)
		{
			var coverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";


			var path = Path.Combine(_ImagePath,coverName);
			using var stream = File.Create(path);
			await model.Cover.CopyToAsync(stream);


			Game game = new()
			{
				Name = model.Name,
				Description = model.Description,
				CategoryId =model.categoryId,
				Cover = coverName,

				Devices = model.SelectedDevices.Select(p => new GameDevice(){ DeviceId = p }).ToList(),
			};
			_context.Add(game);
			_context.SaveChanges();
		}

		public IEnumerable<Game> GetAll()
		{
			var result = _context.Games.Include(p=>p.Category).Include(p=>p.Devices).ThenInclude(p=>p.Device).AsNoTracking().ToList();
			return result;
		}

        public Game? GetById(int id)
        {
			return  _context.Games.Include(p=>p.Category).Include(p=>p.Devices).ThenInclude(p=>p.Device).SingleOrDefault(p => p.Id == id);

        }

		public async Task<Game?> Edit(EditGameFormViewModel model)
		{

			var game = _context.Games.Include(p=>p.Devices).SingleOrDefault(p=>p.Id== model.Id);
			if (game == null) { 
			
				return null;
			
			}

			var hasNewCover = model.Cover is not null;
			var oldCover = game.Cover;

			game.Name = model.Name;
			game.Description = model.Description;
			game.CategoryId = model.categoryId;
			game.Devices = model.SelectedDevices.Select(p=> new GameDevice { DeviceId = p}).ToList();

			if (hasNewCover) { 
			
				game.Cover = await SaveImage(model.Cover!);
			}


			var effectedRows = _context.SaveChanges();

			if (effectedRows > 0)
			{
				if (hasNewCover)
				{

					var cover = Path.Combine(_ImagePath, oldCover);
					File.Delete(cover);
				}
				return game;
			}

			else
			{
				var cover = Path.Combine(_ImagePath, game.Cover);
				File.Delete(cover);
				return null;
			}

		}

      private async Task<string> SaveImage(IFormFile cover)
		{
			var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";


			var path = Path.Combine(_ImagePath, coverName);
			using var stream = File.Create(path);
			await cover.CopyToAsync(stream);

			return coverName;
		}

		public bool Delete(int id)
		{

			var isdeleted = false;


			var game = _context.Games.Find(id);

			if (game is null) 
			
				return isdeleted;
			
			
			_context.Games.Remove(game);

			var effectedrows = _context.SaveChanges();

			if (effectedrows > 0) {
			
			isdeleted = true;
				var cover = Path.Combine(_ImagePath, game.Cover);
				File.Delete(cover);
			
			}

			return isdeleted;

		}
	}
	
}
