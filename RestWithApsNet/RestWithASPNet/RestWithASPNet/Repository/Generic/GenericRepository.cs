
using Microsoft.EntityFrameworkCore;
using RestWithASPNet.Model.Base;
using RestWithASPNet.Model.Context;

namespace RestWithASPNet.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private MySqlContext _sqlContext;
        private DbSet<T> _dbSet;
        public GenericRepository(MySqlContext sqlContext)
        {
            _sqlContext = sqlContext;
            _dbSet = _sqlContext.Set<T>();
        }
        public T Create(T item)
        {
            try
            {
                _dbSet.Add(item);
                _sqlContext.SaveChanges();
                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(long id)
        {
            var result = _dbSet.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    _dbSet.Remove(result);
                    _sqlContext.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool Exists(long id)
        {
            return _dbSet.Any(p => p.Id.Equals(id));
        }

        public List<T> FindAll()
        {
            return _dbSet.ToList();
        }

        public T FindById(long id)
        {
            return _dbSet.SingleOrDefault(p => p.Id.Equals(id));
        }

        public T Update(T item)
        {
            var result = _dbSet.SingleOrDefault(p => p.Id.Equals(item.Id));
            if (result != null)
            {
                try
                {
                    _sqlContext.Entry(result).CurrentValues.SetValues(item);
                    _sqlContext.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
