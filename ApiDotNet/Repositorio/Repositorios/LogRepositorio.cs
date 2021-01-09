using Dominio;
using Repositorio.Contexto;
using Repositorio.Interface;

namespace Repositorio.Repositorios
{
    public class LogRepositorio : RepositorioBase<Log>, ILogRepositorio
    {
        public LogRepositorio(ContextoDb dbContexto) : base(dbContexto) { }
    }
}
