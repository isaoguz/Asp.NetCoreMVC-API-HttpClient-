using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using System.Reflection;

namespace NLayer.Repository
{
    //Burası benim veritabanıma karşılık geliyor.
    public class AppDbContext : DbContext
    {
        //DbContextOptions ile veritabanı yolunu program.cs dan vericez.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }



        //veriyi veritbanına göndermeden önce verinin update veya created edilip edilmediğini öğrenip işleme sokacağız. bu alltaaki kod asenkron olmayan yapılar için.
        public override int SaveChanges()
        {

            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReferance)
                {
                    switch (item.Entity)
                    {
                        case EntityState.Added:
                            {
                                entityReferance.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                entityReferance.UpdateDate = DateTime.Now;
                                break;
                            }
                    }
                }
            }

            return base.SaveChanges();
        }
        //veriyi veritbanına göndermeden önce verinin update veya created edilip edilmediğini öğrenip işleme sokacağız.
        public override Task<int> SaveChangesAsync( CancellationToken cancellationToken = default)
        {

            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReferance)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReferance.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReferance).Property(x => x.CreatedDate).IsModified = false;
                                entityReferance.UpdateDate = DateTime.Now;
                                break;
                            }
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        //Gerçek dünyada ProductFeaturesı eklemeyiz bunun yerine
        //var p = new Product() { ProductFeature = new ProductFeature() {Product=1 }; yaparız


        //Entitiler ile ilgili ayarları migrations esnasında yapabilmemiz için OnModelCreating kullunırız.
        //VE OnModelCreatingi kirletmemek için ise bunları Configurations da ayarlarız ve burada çağırırız.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Burada diyoruz ki çalışmış olduğumuz assemply deki configurationsları bana ver
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //tek bir tanesini uygula dersek. bunu diyebiliriz. Ama çok fazla olursa sadece bunu deriz.
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());




            //Normalde bu işlemi seed içerisinde yaparız ama buradada görmek için yapabiliriz.
            modelBuilder.Entity<ProductFeature>().HasData(new ProductFeature() { Id = 1, Color = "Kırmızı", Width = 100, Height = 200, ProductId = 1 }, new ProductFeature() { Id = 2, Color = "Mavi", Width = 300, Height = 300, ProductId = 2 });



            base.OnModelCreating(modelBuilder);
        }
    }
}
