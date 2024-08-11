using System.Linq.Expressions;

namespace Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        //Task, asenkron olarak kullanılmasını sağlar.
        Task<T> GetByIdAsync(int id);
        //IQueryable döndüğümüzde, sorgularımız direk veritabanına gitmez. ToList gibi sorgular kullanıldığı zaman veritabanına gider.
        //Yani veritabanı için sorgu yapmıyoruz. Yapılacak sorguyu yazıyoruz.
        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddRangeAysnc(IEnumerable<T> entities);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entites);
    }
}
