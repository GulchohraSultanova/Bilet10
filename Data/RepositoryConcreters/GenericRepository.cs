using Core.RepositoryAbstracts;
using Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.RepositoryConcreters
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        AppDbContext _db;

        public GenericRepository(AppDbContext db)
        {
            _db = db;
        }

        public void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }

        public void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public T Get(Func<T, bool>? func)
        {
            return func == null ?
                _db.Set<T>().FirstOrDefault() :
                _db.Set<T>().FirstOrDefault(func);
        }

        public List<T> GetAll(Func<T, bool>? func)
        {
           return func==null? _db.Set<T>().ToList():
                _db.Set<T>().Where(func).ToList();
        }
    }
}
