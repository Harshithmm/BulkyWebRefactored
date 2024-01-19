using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class  //generic bcz we will use it for all the models like Category,products etc
    {
        IEnumerable<T> GetAll(string? includeProperties = null);
        T Get(Expression <Func<T,bool>> filter, string? includeProperties = null);
        void Add(T entity);
        //void Update(T entity);  not used because PRODUCT may have only some properties to be updated and CATEGORY MAY HAVE SOME OTHER PROPERTIES TO BE UPDATED
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);

    }
}
