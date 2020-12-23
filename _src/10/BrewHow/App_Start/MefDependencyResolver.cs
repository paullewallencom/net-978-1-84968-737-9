using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BrewHow
{
    public class MefDependencyResolver : IDependencyResolver
    {
        private ExportProvider _parentContainer;
        private const string RequestContainerKey = "ServiceLocatorConfig.RequestContainer";

        public MefDependencyResolver(ExportProvider parentContainer)
        {
            this._parentContainer = parentContainer;
        }

        public object GetService(Type serviceType)
        {
            var export = this
                .RequestContainer
                .GetExports(serviceType, null, null)
                .SingleOrDefault();

            if (export != null)
            {
                return export.Value;
            }

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var exports = this
                .RequestContainer
                .GetExports(serviceType, null, null);

            foreach (var export in exports)
            {
                yield return export.Value;
            }
        }

        public void Dispose()
        {
            using (RequestContainer as IDisposable) { }
        }

        ExportProvider RequestContainer
        {
            get
            {
                ExportProvider requestContainer = HttpContext.Current.Items[RequestContainerKey] as ExportProvider;

                if (requestContainer == null)
                {
                    requestContainer = new CompositionContainer(this._parentContainer);
                    HttpContext.Current.Items[RequestContainerKey] = requestContainer;
                }

                return requestContainer;
            }
        }
    }
}
