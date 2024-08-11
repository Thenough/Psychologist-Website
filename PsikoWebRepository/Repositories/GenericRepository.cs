using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _contex;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(AppDbContext contex)
        {
            _contex = contex;
            _dbSet = _contex.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            //, Entity Framework Core gibi bir ORM (Object-Relational Mapping) aracılığıyla bir veritabanına asenkron olarak bir varlık
            //eklemeye çalıştığınızı gösterir. AddAsync metodu, varlığı veritabanına eklerken, await anahtar kelimesi bu işlemin
            //tamamlanmasını bekler ve bu sırada diğer işlemlerin devam etmesine izin verir.
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAysnc(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public IQueryable<T> GetAll()
        {
            //AsNoTracking() metodu, sorgulanan varlıkların izlenmemesini sağlar, böylece daha hızlı sorgular ve daha az bellek
            //kullanımı elde edilir.
            //AsQueryable(), bir veri kümesinin IQueryable arayüzüne dönüştürülmesini sağlar. IQueryable, sorguların yapılandırılabilir
            //ve veritabanına gönderilebilir hale getirilmesini sağlar. Bu, sorgu operatörlerinin (filtering, sorting, etc.) veritabanı
            //tarafından işlenmesine izin verir ve verimli veri alımı sağlar.
            //AsNoTracking() ve AsQueryable() yöntemlerini birlikte kullanmak, veritabanı sorgularının izlenmeden ve yapılandırılabilir
            //hale getirilmesini sağlar. Bu, genellikle okuma amaçlı sorgular için performansı artırır ve esneklik sağlar.
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entites)
        {
            _dbSet.RemoveRange(entites);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
    }
}
