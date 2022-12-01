using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<Category> GetSingleCategoryByIdWithProductsAsync(int categoryId)
        {
            //Burada yaptığımız işlem categorileri include ile productlara dahil ettik ve id leri eşit olanları getirdik. SingleOrDefatult ile belirtilen id den sadece bir tane vardır eğer birden fazla varsa hata döner. FirstOrDefault ile birden fazla varsa ilkini bize verir.
            return await _context.Categories.Include(x => x.Products).Where(x => x.Id == categoryId).SingleOrDefaultAsync();
        }
    }
}
