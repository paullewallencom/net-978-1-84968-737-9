using System;
using System.Linq.Expressions;
using BrewHow.Domain.Entities;
using BrewHow.Models;

namespace BrewHow.Infrastructure.Repositories
{
    internal static class EntityMappingExpressions
    {
        /// <summary>
        /// Expression to convert a Recipe model to a 
        /// RecipeEntity domain model.
        /// </summary>
        public static readonly Expression<Func<Recipe, RecipeEntity>> AsRecipeEntity =
            r => new RecipeEntity
            {
                RecipeId = r.RecipeId,
                Name = r.Name,
                OriginalGravity = r.OriginalGravity,
                FinalGravity = r.FinalGravity,
                GrainBill = r.GrainBill,
                Instructions = r.Instructions,
                Slug = r.Slug,
                Contributor = new UserProfileEntity
                {
                    UserId = r.Contributor.UserId,
                    UserName = r.Contributor.UserName
                },
                Style = new StyleEntity
                {
                    Name = r.Style.Name,
                    StyleId = r.Style.StyleId,
                    Category = (CategoryEntity)r.Style.Category,
                    Slug = r.Style.Slug
                }
            };

        public static readonly Expression<Func<Style, StyleEntity>> AsStyleEntity =
            s => new StyleEntity
            {
                StyleId = s.StyleId,
                Name = s.Name,
                Category = (CategoryEntity)s.Category,
                Slug = s.Slug
            };

        public static readonly Expression<Func<UserProfile, UserProfileEntity>> AsUserProfileEntity =
            u => new UserProfileEntity
            {
                UserId = u.UserId,
                UserName = u.UserName
            };


    }
}