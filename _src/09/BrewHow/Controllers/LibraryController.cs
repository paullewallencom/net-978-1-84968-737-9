using System;
using System.Web.Mvc;
using BrewHow.Domain.Entities;
using BrewHow.Domain.Repositories;
using BrewHow.ViewModels;
using BrewHow.ViewModels.Mappers;
using WebMatrix.WebData;

namespace BrewHow.Controllers
{
    [Authorize]
    public class LibraryController : Controller
    {
        private readonly ILibraryRepository _libraryRepository;
        private readonly IUserProfileEntityFactory _userProfileEntityFactory;
        private readonly IRecipeDisplayViewModelMapper _displayModelMapper;

        public LibraryController(ILibraryRepository libraryRepository,
            IUserProfileEntityFactory userProfileEntityFactory,
            IRecipeDisplayViewModelMapper displayModelMapper)
        {
            this._libraryRepository = libraryRepository;
            this._userProfileEntityFactory = userProfileEntityFactory;
            this._displayModelMapper = displayModelMapper;
        }

        public ActionResult Index(int page = 0)
        {
            var recipesInLibrary = this
                ._libraryRepository
                .GetRecipesInLibrary(WebSecurity.CurrentUserId);

            var viewModel = new PagedResult<RecipeEntity, RecipeDisplayViewModel>(
                recipesInLibrary,
                page,
                this._displayModelMapper.EntityToViewModel);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id)
        {
            int userId = WebSecurity.CurrentUserId;

            this
                ._libraryRepository
                .AddRecipeToLibrary(id, userId);

            return Json(new { result = "ok" } );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            int userId = WebSecurity.CurrentUserId;

            this
                ._libraryRepository
                .RemoveRecipeFromLibrary(id, userId);

            return Json(new { result = "ok" });
        }
    }
}
