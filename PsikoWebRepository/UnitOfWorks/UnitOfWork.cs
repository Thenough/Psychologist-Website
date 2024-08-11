using Core.UnitOfWorks;

namespace Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _contex;
        public UnitOfWork(AppDbContext contex)
        {
            _contex = contex;
        }
        public void Commit()
        {
            _contex.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _contex.SaveChangesAsync();
        }
    }
}
