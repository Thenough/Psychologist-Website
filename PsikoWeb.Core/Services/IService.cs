using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IService<T> where T : class
    {
        //Task, asenkron olarak kullanılmasını sağlar.
        Task<T> GetByIdAsync(int id);
        //IQueryable döndüğümüzde, sorgularımız direk veritabanına gitmez. ToList gibi sorgular kullanıldığı zaman veritabanına gider.
        //Yani veritabanı için sorgu yapmıyoruz. Yapılacak sorguyu yazıyoruz.
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>>  AddRangeAysnc(IEnumerable<T> entities);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entites);
    }
}
