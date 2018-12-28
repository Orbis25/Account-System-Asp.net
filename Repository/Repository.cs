using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        bool Add(TEntity entity);
        bool Delete(int id);
        bool Update(TEntity entity);
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll(int page);

    }
}
