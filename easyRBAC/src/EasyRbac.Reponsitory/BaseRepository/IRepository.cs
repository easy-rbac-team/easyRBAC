using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EasyRbac.Dto;

namespace EasyRbac.Reponsitory.BaseRepository
{
    public interface IRepository<T>
    {
        Task InsertAsync(T obj);

        Task DeleteAsync(Expression<Func<T, bool>> expression);        

        Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> condition);

        Task<PagingList<T>> QueryByPagingAsync(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderBy, int pageIndex, int pageSize);

        Task UpdateAsync(Expression<Func<T>> update, Expression<Func<T, bool>> condition);

        Task<T> QueryFirstAsync(Expression<Func<T, bool>> condition);
    }
}
