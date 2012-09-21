using System.ComponentModel.Composition;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Oravaci;
using Raven.Client.Embedded;
using vlko.core;
using vlko.core.Commands;
using vlko.core.InversionOfControl;
using vlko.core.Repository;

[assembly: WebActivator.PreApplicationStartMethod(typeof(RegisterCoreRaven), "Register", Order = 1)]
namespace Oravaci
{

    [Export(typeof(IAppInit))]
    [Order(1)]
    public class RegisterCoreRaven : IAppInit
    {
        public static void Register()
        {
            IoC.AddCatalogAssembly(Assembly.Load("vlko.core.RavenDB"));
            IoC.AddCatalogAssembly(Assembly.Load("OravaciData"));
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "ConfirmRegistration", // Route name
                "Account/ConfirmRegistration/{verifyToken}", // URL with parameters
                new { controller = "Account", action = "ConfirmRegistration" } // Parameter defaults
                );

            routes.MapRoute(
                "ConfirmResetPassword", // Route name
                "Account/ConfirmResetPassword/{verifyToken}", // URL with parameters
                new { controller = "Account", action = "ConfirmResetPassword" } // Parameter defaults
                );
        }

        public void ConfigureDb(bool registerNewDatabase)
        {
            ConfigureForRavenDb(registerNewDatabase);

            if (registerNewDatabase)
            {
                RepositoryFactory.Command<IUserCommands>().CreateAdmin("vlko", "vlko@zilina.net", "test");
            }
        }

        /// <summary>
        /// Configures for raven db.
        /// </summary>
        /// <param name="registerNewDatabase">if set to <c>true</c> [register new database].</param>
        private void ConfigureForRavenDb(bool registerNewDatabase)
        {
            var documentStore = new EmbeddableDocumentStore()
            {
                Configuration =
                {
                    DataDirectory = HttpContext.Current.Server.MapPath(MvcApplication.IndexLocationConst),
                }
            };

            documentStore.Initialize();

            vlko.core.RavenDB.DBInit.RegisterDocumentStore(documentStore);
        }
    }
}