using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using WebStore.Domain.Concrete;
using Moq;
using Ninject;
using WebStore.Domain.Abstract;
using WebStore.Domain.Entities;
using GameStore.Domain.Concrete;
using WebStore.WebUI.Infrastructure.Abstract;
using WebStore.WebUI.Infrastructure.Concrete;

namespace WebStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {   
            kernel.Bind<IWebRepository>().To<EFWebRepository>();

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                    .AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);

            kernel.Bind<IAuthProvider>().To<FormAuthProvider>();
        }
    }
}