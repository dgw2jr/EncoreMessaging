using Autofac;
using DataAccess;

namespace DependencyResolution
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DomainModel>().AsSelf().AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(GenericRepository<>)).AsImplementedInterfaces();
        }
    }
}
