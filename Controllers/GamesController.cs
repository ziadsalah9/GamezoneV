using GamezoneV.Data;
using GamezoneV.Services;
using GamezoneV.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.WebSockets;

namespace GamezoneV.Controllers
{
    public class GamesController : Controller
    {

        private readonly ICategoriesService _categoriesService;
        private readonly IDevicesService _devicesService;
        private readonly IGamesServices _gamesServices;

        public GamesController( ICategoriesService _categoriesService,  IDevicesService _devicesService, IGamesServices _gamesServices)
        {
            this._categoriesService = _categoriesService;
            this._devicesService = _devicesService;
            this._gamesServices = _gamesServices;
        }



        public IActionResult Index()
        {
            return View(_gamesServices.GetAll());
        }


        [HttpGet]
        public IActionResult Details(int id )
        {
            var result = _gamesServices.GetById(id) ;
            if (result is null)
                return NotFound();
            return View(result);
        }

		[HttpGet]
		public IActionResult Create()
		{
            var categories =_categoriesService.GetSelectList();
         var devices = _devicesService.GetListItems();
            CreateGameFormViewModel viewmodel = new()
            {
                Categoties = categories,
                Devices = devices
			};
			return View(viewmodel);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(CreateGameFormViewModel model)
        {
			


			if (ModelState.IsValid==false)
            {
                model.Categoties = _categoriesService.GetSelectList();
				model.Devices = _devicesService.GetListItems();

				return View(model);

            }
            else
            {
				//save data in database
				// save pic in server

				await _gamesServices.Create(model);

				return RedirectToAction(nameof(Index));
            }

        

            
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {

			var result = _gamesServices.GetById(id);
			if (result is null)
				return NotFound();

            EditGameFormViewModel viewmodel = new()
            {
                Id = id,
                Name = result.Name,
                Description = result.Description,
                categoryId = result.CategoryId,
                SelectedDevices = result.Devices.Select(p => p.DeviceId).ToList(),
                Categoties = _categoriesService.GetSelectList(),
                Devices = _devicesService.GetListItems(),
                CurrentCover = result.Cover

            };

            return View(viewmodel);

		}

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {


			if (ModelState.IsValid == false)
			{
				model.Categoties = _categoriesService.GetSelectList();
				model.Devices = _devicesService.GetListItems();

				return View(model);

			}

            var game = await _gamesServices.Edit(model);

            if(game is null)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));

		}



        [HttpDelete]
        public IActionResult Delete(int id) {

            var isdeleted = _gamesServices.Delete(id);

            if (isdeleted)
            {

                return Ok();
            }

            return BadRequest();
        }

	}

}
