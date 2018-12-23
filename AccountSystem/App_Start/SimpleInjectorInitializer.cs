[assembly: WebActivator.PostApplicationStartMethod(typeof(AccountSystem.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace AccountSystem.App_Start
{
    using System;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Linq;
    using Service;
    using Service.Interface;
    using SimpleInjector;
    using SimpleInjector.Advanced;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using AccountSystem.Models;
    using Microsoft.AspNet.Identity;

    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            //llamando para permitir que mi contructor vacio
            container.Options.ConstructorResolutionBehavior = new LeastParametersConstructorBehavior();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            //container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            // For instance:
            // container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);
            container.Register<IClientService, ClientService>(Lifestyle.Transient);
            container.Register<IAccountClientService, AccountClientService>(Lifestyle.Transient);
            container.Register<IDebService, DebService>(Lifestyle.Transient);
            container.Register<IHomeService, HomeService>(Lifestyle.Transient);
            container.Register<IAccountService, ApplicationUserService>(Lifestyle.Transient);
            container.Register<IRequestService, RequestService>(Lifestyle.Transient);

            //definir siempre estos debido a los constructores vacios
            container.Register<ApplicationSignInManager>();
            container.Register<ApplicationUserManager>();


            //container.Register<IUserStore<ApplicationUser>>(Lifestyle.Transient);
        }
    }

    //Corregir constructores vacios
    public class LeastParametersConstructorBehavior : IConstructorResolutionBehavior
    {
        public ConstructorInfo GetConstructor(Type implementationType) => (
            from ctor in implementationType.GetConstructors()
            orderby ctor.GetParameters().Length ascending
            select ctor)
            .First();
    }

}
