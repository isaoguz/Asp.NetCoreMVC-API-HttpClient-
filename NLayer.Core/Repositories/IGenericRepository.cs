using System.Linq.Expressions;

namespace NLayer.Core.Repositories
{
    //GENEL HAVUZ => veri tabanı ile kodumuz arasında bir katman oluşturur ve temel CRUD operasyonlarının işlevini görür.
    public interface IGenericRepository<T> where T : class
    {
        //id'ye göre getir.
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        //veritabanına ulaşmıyoruz, veritabanına yapılacak sorguyu yazıyoruz.
        //productRepository.where(x=>x.id>5).OrderBy.ToListAsync(); dersem db ye sorgu yazar
        //Burada ki T=> x e bool doğru veya yanlışlığa tekabül eder.
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        //update ve deletenin işlemleri uzun sürmüyor. Bu sebeble asenkron tasarlamaya gerek yok. Ef Core onu hallediyor.
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task AddRangeAsync(IEnumerable<T> entities);


    }
}
