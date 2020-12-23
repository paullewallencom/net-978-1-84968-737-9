using System;
using System.Web;
using System.Web.Mvc;
using BrewHow.Domain.Entities;
using BrewHow.Domain.Repositories;
using WebMatrix.WebData;

namespace BrewHow.ViewModels.Mappers.Impl
{
    public class RecipeDisplayViewModelMapper
        : IRecipeDisplayViewModelMapper
    {
        private readonly IStyleRepository _styleRepository;
        private readonly IUserProfileEntityFactory _userProfileFactory;

        public RecipeDisplayViewModelMapper(
            IStyleRepository styleRepository
            ,IUserProfileEntityFactory userProfileFactory)
        {
            this._styleRepository = styleRepository;
            this._userProfileFactory = userProfileFactory;
        }


        public RecipeDisplayViewModel EntityToViewModel(RecipeEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(
                    "entity",
                    "Cannot convert an entity to a view model when no entity is passed.");
            }

            bool canEdit = false;

            if (HttpContext.Current != null
                && HttpContext.Current.Request.IsAuthenticated)
            {
                canEdit = WebSecurity.CurrentUserId == entity.Contributor.UserId;
            }

            return new RecipeDisplayViewModel
            {
                RecipeId = entity.RecipeId,
                Name = entity.Name,
                Category = entity.Style.Category.ToString(),
                Style = entity.Style.Name,
                OriginalGravity = entity.OriginalGravity,
                FinalGravity = entity.FinalGravity,
                PercentAlcoholByVolume = entity.PercentAlcoholByVolume,
                GrainBill = entity.GrainBill,
                Instructions = entity.Instructions,
                Slug = entity.Slug,
                StyleSlug = entity.Style.Slug,
                ContributedBy = entity.Contributor.UserName,
                CanEdit = canEdit
            };
        }
    }
}