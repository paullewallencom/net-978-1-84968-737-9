using System;
using System.Linq;
using System.Web.Mvc;

using BrewHow.Areas.Review.ViewModels;
using BrewHow.Domain.Entities;
using BrewHow.Domain.Repositories;

namespace BrewHow.Areas.Review.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserProfileEntityFactory _userProfileEntityFactory;

        public RecipeController(IReviewRepository reviewRepository,
            IUserProfileEntityFactory userProfileEntityFactory)
        {
            this._reviewRepository = reviewRepository;
            this._userProfileEntityFactory = userProfileEntityFactory;
        }

        public ActionResult Index(int id)
        {
            var reviews = this
                ._reviewRepository
                .GetReviewsForRecipe(id);

            this.ViewBag.RecipeId = id;

            return PartialView(reviews.AsEnumerable().Select(s => ToListModel(s)));
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create(int id)
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, ReviewEditViewModel reviewEditViewModel)
        {
            try
            {
                var review = ToEntity(
                    id,
                    reviewEditViewModel
                );

                this
                    ._reviewRepository
                    .SaveReview(review);

                return this
                    .RedirectToAction(
                        "Details",
                        "Recipe",
                        new
                        {
                            id = id,
                            area = ""
                        });
            }
            catch
            {
                return View(reviewEditViewModel);
            }
        }

        private ReviewListViewModel ToListModel(ReviewEntity review)
        {
            return new ReviewListViewModel
            {
                Comment = review.Comment,
                Rating = review.Rating,
                Reviewer = review.Reviewer.UserName,
            };
        }

        private ReviewEntity ToEntity(int recipeId, ReviewEditViewModel viewModel)
        {
            return new ReviewEntity
            {
                RecipeId = recipeId,
                Comment = viewModel.Comment,
                Rating = viewModel.Rating,
                Reviewer = this._userProfileEntityFactory.Create()
            };
        }
    }
}
