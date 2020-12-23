using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Reflection;
using System.Web.Mvc;

using BrewHow.Domain.Repositories;
using BrewHow.Domain.Entities;

using BrewHow.Models;
using BrewHow.ViewModels.Mappers;

namespace BrewHow
{
    public class ServiceLocatorConfig
    {
        private static CompositionContainer _container = null;

        public static void RegisterTypes()
        {
            RegistrationBuilder rb = new RegistrationBuilder();

            RegisterDbContexts(rb);
            RegisterRepositories(rb);
            RegisterControllers(rb);
            RegisterFactories(rb);
            RegisterMappers(rb);

            ServiceLocatorConfig._container = new CompositionContainer(
                new AssemblyCatalog(
                    Assembly.GetExecutingAssembly(),
                    rb
                )
            );

            var resolver = new MefDependencyResolver(ServiceLocatorConfig._container);

            DependencyResolver.SetResolver(resolver);
        }

        private static void RegisterDbContexts(RegistrationBuilder rb)
        {
            rb.ForTypesDerivedFrom<IBrewHowContext>()
                .Export<IBrewHowContext>()
                .SetCreationPolicy(CreationPolicy.NonShared);
        }

        private static void RegisterRepositories(RegistrationBuilder rb)
        {
            rb.ForTypesDerivedFrom<IRecipeRepository>()
                .Export<IRecipeRepository>()
                .SetCreationPolicy(CreationPolicy.NonShared);

            rb.ForTypesDerivedFrom<IStyleRepository>()
                .Export<IStyleRepository>()
                .SetCreationPolicy(CreationPolicy.NonShared);

            rb.ForTypesDerivedFrom<IReviewRepository>()
                .Export<IReviewRepository>()
                .SetCreationPolicy(CreationPolicy.NonShared);

            rb.ForTypesDerivedFrom<IUserProfileRepository>()
                .Export<IUserProfileRepository>()
                .SetCreationPolicy(CreationPolicy.NonShared);

            rb.ForTypesDerivedFrom<ILibraryRepository>()
                .Export<ILibraryRepository>()
                .SetCreationPolicy(CreationPolicy.NonShared);
        }

        private static void RegisterControllers(RegistrationBuilder rb)
        {
            rb.ForTypesDerivedFrom<Controller>()
                .Export()
                .SetCreationPolicy(CreationPolicy.NonShared);
        }

        private static void RegisterFactories(RegistrationBuilder rb)
        {
            rb.ForTypesDerivedFrom<IUserProfileEntityFactory>()
                .Export<IUserProfileEntityFactory>()
                .SetCreationPolicy(CreationPolicy.NonShared);
        }

        private static void RegisterMappers(RegistrationBuilder rb)
        {
            rb.ForTypesDerivedFrom<IRecipeDisplayViewModelMapper>()
                .Export<IRecipeDisplayViewModelMapper>()
                .SetCreationPolicy(CreationPolicy.NonShared);

            rb.ForTypesDerivedFrom<IRecipeEditViewModelMapper>()
                .Export<IRecipeEditViewModelMapper>()
                .SetCreationPolicy(CreationPolicy.NonShared);

            rb.ForTypesDerivedFrom<IStyleDisplayViewModelMapper>()
                .Export<IStyleDisplayViewModelMapper>()
                .SetCreationPolicy(CreationPolicy.NonShared);
        }
    }
}