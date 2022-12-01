using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Seeds
{
    public class CategorySeed : IEntityTypeConfiguration<Category>
    {
        //İçerisinde veri tabanı oluşurken hazır gelicek veriler yer almaktadır.
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category() { Id = 1, Name = "Kalemler" }, new Category() { Id = 2, Name = "Kitaplar" }, new Category() { Id = 3, Name = "Defterler" });
        }
    }
}
