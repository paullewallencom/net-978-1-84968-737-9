using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Reflection;
using System.Web.Mvc;

using BrewHow.Domain.Repositories;
using BrewHow.Models;

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
        }

        private static void RegisterControllers(RegistrationBuilder rb)
        {
            rb.ForTypesDerivedFrom<Controller>()
                .Export()
                .SetCreationPolicy(CreationPolicy.NonShared);
        }
    }
}