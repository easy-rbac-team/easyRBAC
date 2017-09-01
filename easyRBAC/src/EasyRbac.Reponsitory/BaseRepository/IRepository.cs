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

        Task DeleteById(object key);

        Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> condition);

        Task<PagingList<T>> QueryByPagingAsync(Expression<Func<T, bool>> condition, int pageIndex, int pageSize);
    }
}
