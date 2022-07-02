using Microsoft.EntityFrameworkCore;
using RedisTest.Models.DBEntity;
using RedisTest.Repository.Interface;
using System.Linq;

namespace RedisTest.Repository
{
    public class DBRepository : IDBRepository
    {
        private readonly NumberDBContext _context;

        public DBRepository(NumberDBContext context)
        {
            _context = context;
        }
        public NumberDBContext Context { get { return _context; } }

        public void Create<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return _context.Set<T>();
        }

        public void Save()
        {
            _context.SaveChanges();
        }


    }
}
