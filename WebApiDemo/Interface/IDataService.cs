using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApiDemo.Interface
{
    public interface IDataService<T> where T : class, IEnitity
    {
        Task<List<T>> GetAll();
        Task<int> Create(T model);
        Task<T> Get(int id);
        Task Delete(T model);
        Task Delete(int id);
        Task<int> Update(T model);
        IQueryable<T> Query(Expression<Func<T, bool>> condition);
        IQueryable<T> Query();
    }
}
