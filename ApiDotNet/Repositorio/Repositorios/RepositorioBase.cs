using Repositorio.Contexto;
using Repositorio.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositorio.Repositorios
{
    public abstract class RepositorioBase<TEntity> : IRepositorioBase<TEntity> where TEntity : class
    {
        protected readonly ContextoDb _dbContexto;

        public RepositorioBase(ContextoDb dbContexto)
        {
            if (dbContexto == null)
                throw new ArgumentNullException(nameof(dbContexto));
            _dbContexto = dbContexto;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbContexto.Set<TEntity>();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _dbContexto.Set<TEntity>()
                .FindAsync(id);
        }

        public async Task Create(TEntity entity)
        {
            await _dbContexto.Set<TEntity>().AddAsync(entity);
            await _dbContexto.SaveChangesAsync();
        }

        public async Task CreateList(List<TEntity> entities)
        {
            await _dbContexto.Set<TEntity>().AddRangeAsync(entities);
            await _dbContexto.SaveChangesAsync();
        }

        public async Task Update(int id, TEntity entity)
        {
            _dbContexto.Set<TEntity>().Update(entity);
            await _dbContexto.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            _dbContexto.Set<TEntity>().Remove(entity);
            await _dbContexto.SaveChangesAsync();
        }
    }
}
