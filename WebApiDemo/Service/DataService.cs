using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApiDemo.Entity;
using WebApiDemo.Interface;

namespace WebApiDemo.Service
{
    public class DataService<T> : IDataService<T> where T : class, IEnitity
    {
        private WebApiDemoContext _dbcontext;
        public DataService(WebApiDemoContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task Delete(int id)
        {
            var entity = await _dbcontext.FindAsync<T>(id);
            await Delete(entity);
        }
        public async Task Delete(T entity)
        {
            _dbcontext.Remove<T>(entity);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task<List<T>> GetAll()
        {
            return await _dbcontext.Set<T>().ToListAsync();
        }
        public async Task<T> Get(int id)
        {
            return await _dbcontext.FindAsync<T>(id);
        }
        public async Task<int> Create(T entity)
        {
            await _dbcontext.AddAsync(entity);
            await _dbcontext.SaveChangesAsync();

            return entity.Id;
        }
        public async Task<int> Update(T entity)
        {
            _dbcontext.Update<T>(entity);
            await _dbcontext.SaveChangesAsync();
            return entity.Id;
        }
        public IQueryable<T> Query(Expression<Func<T, bool>> condition)
        {
            return _dbcontext.Set<T>().Where(condition);
        }
        public IQueryable<T> Query()
        {
            return _dbcontext.Set<T>();
        }
    }
}
