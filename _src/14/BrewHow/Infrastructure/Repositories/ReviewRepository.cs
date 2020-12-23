using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using BrewHow.Domain.Entities;
using BrewHow.Domain.Repositories;
using BrewHow.Models;

namespace BrewHow.Infrastructure.Repositories
{
    public class ReviewRepository : RepositoryBase, IReviewRepository
    {
        public ReviewRepository(IBrewHowContext context)
            : base(context)
        {
        }

        public ReviewEntity GetReview(int reviewId)
        {
            return this
                .ReviewEntities
                .FirstOrDefault(r => r.ReviewId == reviewId);
        }

        public IQueryable<ReviewEntity> GetReviewsForRecipe(int recipeId)
        {
            return
            (
                from recipe in this.Context.Recipes
                where recipe.RecipeId == recipeId
                from review in recipe.Reviews
                select review
            ).Select(AsReviewEntity);
        }

        public void SaveReview(ReviewEntity review)
        {
            // It's not a new review.  Update the value.
            var recipe = Context
                .Recipes
                .FirstOrDefault(r => r.RecipeId == review.RecipeId);

            if (recipe == null)
            {
                throw new InvalidOperationException(
                    "Cannot save review without a recipe.");
            }


            // It's a new review.
            if (review.ReviewId == 0)
            {
                var modelReview = new Review();
                AssignEntityToModel(review, modelReview);

                var userProfile = new UserProfile();
                userProfile.UserId = review.Reviewer.UserId;
                userProfile.UserName = review.Reviewer.UserName;

                this
                    .Context
                    .UserProfiles
                    .Attach(userProfile);

                modelReview.Reviewer = userProfile;
                recipe.Reviews.Add(modelReview);
                Context.SaveChanges();

                review.ReviewId = modelReview.ReviewId;

                return;
            }

            // Add the review to the recipe.
            var oldReviewModel = recipe
                .Reviews
                .FirstOrDefault(r => r.ReviewId == review.ReviewId);

            if (oldReviewModel == null)
            {
                throw new ArgumentOutOfRangeException("review",
                    "Cannot edit the review as it cannot be located.");
            }

            AssignEntityToModel(review, oldReviewModel);

            Context.SaveChanges();
        }

        private IQueryable<ReviewEntity> ReviewEntities
        {
            get
            {
                return this
                    .Context
                    .Reviews
                    .Include("Reviewer")
                    .OrderByDescending(r => r.ReviewId)
                    .Select(AsReviewEntity);
            }
        }

        private void AssignEntityToModel(ReviewEntity reviewEntity, Review reviewModel)
        {
            if (reviewEntity == null)
            {
                return;
            }

            if (reviewModel == null)
            {
                throw new ArgumentNullException(
                    "dbReview",
                    "You cannot assign a review to a null model.");
            }

            reviewModel.Rating = reviewEntity.Rating;
            reviewModel.Comment = reviewEntity.Comment;
        }

        private static readonly Expression<Func<Review, ReviewEntity>> AsReviewEntity =
            r => new ReviewEntity
            {
                ReviewId = r.ReviewId,
                Comment = r.Comment,
                Rating = r.Rating,
                Reviewer = new UserProfileEntity { UserId = r.Reviewer.UserId, UserName = r.Reviewer.UserName }
            };
    }
}