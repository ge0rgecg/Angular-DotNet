using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositorio.Interface
{
    public interface IRepositorioBase<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        Task<TEntity> GetById(int id);

        Task Create(TEntity entity);

        Task CreateList(List<TEntity> entities);

        Task Update(int id, TEntity entity);

        Task Delete(int id);
    }
}
