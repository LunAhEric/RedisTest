using RedisTest.Models.DBEntity;
using System.Linq;

namespace RedisTest.Repository.Interface
{
    public interface IDBRepository
    {
        public NumberDBContext Context { get; }
        public void Create<T>(T entity) where T : class;
        public void Update<T>(T entity) where T : class;
        public void Delete<T>(T entity) where T : class;
        public IQueryable<T> GetAll<T>() where T : class;
        public void Save();
    }
}
