using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<Product>> GetProductsWithCategory()
        {
            //Burada ki contexti genericrepository de protected olarak tanımladığımız için direk çağırabildik.
            //Include ile eager loading kullandık. Product ile beraber bağlı olduğu kategorileri çektik.
            return await _context.Products.Include(x => x.Category).ToListAsync();
        }
    }
}
