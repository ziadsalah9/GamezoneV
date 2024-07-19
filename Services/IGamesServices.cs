using GamezoneV.Models;
using GamezoneV.ViewModels;

namespace GamezoneV.Services
{
	public interface IGamesServices
	{
		Task Create(CreateGameFormViewModel model);
		IEnumerable<Game> GetAll();

		Game? GetById(int id);

		Task<Game?> Edit(EditGameFormViewModel game);


		bool Delete(int id);
	}
}
