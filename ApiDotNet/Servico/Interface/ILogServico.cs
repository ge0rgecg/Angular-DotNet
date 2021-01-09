using Dominio;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servico.Interface
{
    public interface ILogServico
    {
        Task<Retorno<Log>> ObterLog(int id);

        Task<Retorno<IEnumerable<Log>>> ObterLogs();

        Task<Retorno<SemConteudo>> AdicionarLog(Log log);

        Task<Retorno<SemConteudo>> AdicionarListaLogs(List<Log> logs);

    }
}
