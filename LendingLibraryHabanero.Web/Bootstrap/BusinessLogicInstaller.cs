using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using LendingLibrary.Habanero.DB.Interfaces;
using LendingLibrary.Habanero.DB.Repository;

namespace LendingLibrary.Habanero.Web.Bootstrap
{
    public class BusinessLogicInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IPersonRepository>().ImplementedBy<PersonRepository>());
            container.Register(Component.For<ILendingsRepository>().ImplementedBy<LendingsRepository>());
            container.Register(Component.For<IItemRepository>().ImplementedBy<ItemRepository>());
        }
    }
}