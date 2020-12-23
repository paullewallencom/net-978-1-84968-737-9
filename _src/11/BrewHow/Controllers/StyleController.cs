using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BrewHow.Domain.Entities;
using BrewHow.Domain.Repositories;
using BrewHow.ViewModels;
using BrewHow.Models;
using System.Threading.Tasks;

namespace BrewHow.Controllers
{
    public class StyleController : Controller
    {
        private readonly IStyleRepository _styleRepository;

        public StyleController(IStyleRepository styleRepository)
        {
            this._styleRepository = styleRepository;
        }

        public async Task<ActionResult> Index(int page = 0)
        {
            var styleList = Task.Factory.StartNew(() =>
            {
                var viewModel = new PagedResult<StyleEntity, StyleDisplayViewModel>(
                    _styleRepository.GetStyles(),
                    page,
                    ToDisplayModel);

                return viewModel;
            });

            return View(await styleList);
        }

        /// <summary>
        /// Converts a recipe entity into the display model sent
        /// to the view.
        /// </summary>
        /// <param name="entity">The entity to convert</param>
        /// <returns>A RecipeDisplayViewModel that represents the underlying domain entity</returns>
        private StyleDisplayViewModel ToDisplayModel(StyleEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(
                    "entity",
                    "Cannot convert an entity to a view model when no entity is passed.");
            }

            return new StyleDisplayViewModel
            {
                StyleId = entity.StyleId,
                Name = entity.Name,
                Slug = entity.Slug,
                Category = entity.Category.ToString()
            };
        }

    }
}
