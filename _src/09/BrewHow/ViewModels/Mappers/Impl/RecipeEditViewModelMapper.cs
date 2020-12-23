using System;
using System.Web.Mvc;
using BrewHow.Domain.Entities;
using BrewHow.Domain.Repositories;

namespace BrewHow.ViewModels.Mappers.Impl
{
    public class RecipeEditViewModelMapper
        : IRecipeEditViewModelMapper
    {
        private readonly IStyleRepository _styleRepository;
        private readonly IUserProfileEntityFactory _userProfileFactory;

        public RecipeEditViewModelMapper(
            IStyleRepository styleRepository,
            IUserProfileEntityFactory userProfileFactory)
        {
            this._styleRepository = styleRepository;
            this._userProfileFactory = userProfileFactory;
        }

        public RecipeEntity ViewModelToEntity(RecipeEditViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(
                    "viewModel",
                    "Must have something to convert to an entity.");
            }

            var style =
                this
                ._styleRepository
                .GetStyle(viewModel.StyleId);

            return new RecipeEntity
            {
                FinalGravity = viewModel.FinalGravity,
                GrainBill = viewModel.GrainBill,
                Instructions = viewModel.Instructions,
                Name = viewModel.Name,
                OriginalGravity = viewModel.OriginalGravity,
                RecipeId = viewModel.RecipeId,
                Slug = viewModel.Slug,
                Style = style,
                Contributor = this._userProfileFactory.Create()
            };
        }

        public RecipeEditViewModel EntityToViewModel(RecipeEntity entity)
        {
            var editModel = new RecipeEditViewModel();

            var styles = this._styleRepository.GetStyles();


            editModel.StyleList = new SelectList(
                styles,
                "StyleId",
                "Name",
                0);

            if (entity != null)
            {
                editModel.FinalGravity = entity.FinalGravity;
                editModel.GrainBill = entity.GrainBill;
                editModel.Instructions = entity.Instructions;
                editModel.Name = entity.Name;
                editModel.OriginalGravity = entity.OriginalGravity;
                editModel.RecipeId = entity.RecipeId;
                editModel.StyleId = entity.Style.StyleId;
                editModel.Slug = entity.Slug;
            }

            return editModel;
        }
    }
}