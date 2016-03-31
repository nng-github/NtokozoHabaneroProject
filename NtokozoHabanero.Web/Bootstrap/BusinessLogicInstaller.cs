using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NtokozoHabanero.DB.Interfaces;
using NtokozoHabanero.DB.Repository;

namespace NtokozoHabanero.Web.Bootstrap
{
    public class BusinessLogicInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IPersonRepository>().ImplementedBy<PersonRepository>());
        }
    }
}