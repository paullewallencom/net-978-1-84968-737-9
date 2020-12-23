using System;

namespace BrewHow.Domain.Entities
{
    public class ReviewEntity
    {
        public int ReviewId { get; set; }
        public int RecipeId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        public UserProfileEntity Reviewer { get; set; }
    }
}