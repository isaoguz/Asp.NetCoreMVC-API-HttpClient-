using Autofac;
using NLayer.Caching;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using System.Reflection;
using Module = Autofac.Module;
namespace NLayer.API.Modules
{
    public class RepoServiceModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            //buraya yazdıklarımız tekli olduğu için özel olarak belirtiriz.
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();


            //AutoFac assemplleri taricak ve interfaceye karşılık gelen class'ları alacaktır.

            var apiAssemply = Assembly.GetExecutingAssembly();

            var repoAssemply = Assembly.GetAssembly(typeof(AppDbContext));

            var serviceAssemply = Assembly.GetAssembly(typeof(MapProfile));




            //Burada diyoruz içerisine girdiğimiz classlardan sonu repository ile bitenleri al ve implemente et
            builder.RegisterAssemblyTypes(apiAssemply, repoAssemply, serviceAssemply).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();


            //Burada diyoruz içerisine girdiğimiz classlardan sonu service ile bitenleri al ve implemente et
            builder.RegisterAssemblyTypes(apiAssemply, repoAssemply, serviceAssemply).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();


            //Sen IProductService yi görünce ProductServiceWithCachingi ver diyoruz
            //builder.RegisterType<ProductServiceWithCaching>().As<IProductService>();
        }
    }
}
