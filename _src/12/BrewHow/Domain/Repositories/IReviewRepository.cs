using System;
using System.Linq;

using BrewHow.Domain.Entities;

namespace BrewHow.Domain.Repositories
{
    public interface IReviewRepository
    {
        ReviewEntity GetReview(int reviewId);
        IQueryable<ReviewEntity> GetReviewsForRecipe(int recipeId);
        void SaveReview(ReviewEntity review);
    }
}
