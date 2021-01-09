using Dominio;
using Repositorio.Interface;
using Servico.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servico.Servicos
{
    public class LogServico : ILogServico
    {
        private ILogRepositorio _logRepositorio { get; set; }

        public LogServico(ILogRepositorio logRepositorio)
        {
            _logRepositorio = logRepositorio == null ?
                throw new ArgumentNullException("logRepositorio") :
                logRepositorio;
        }

        public async Task<Retorno<Log>> ObterLog(int id)
        {
            var log = await _logRepositorio.GetById(id);

            return new Retorno<Log>
            {
                Ok = true,
                Objeto = log
            };
        }

        public async Task<Retorno<IEnumerable<Log>>> ObterLogs()
        {
            var logs = _logRepositorio.GetAll();

            return new Retorno<IEnumerable<Log>>
            {
                Ok = true,
                Objeto = logs
            };
        }

        public async Task<Retorno<SemConteudo>> AdicionarLog(Log log)
        {
            await _logRepositorio.Create(log);

            return new Retorno<SemConteudo> { Ok = true };
        }

        public async Task<Retorno<SemConteudo>> AtualizarLog(int id, Log log)
        {
            await _logRepositorio.Update(id, log);

            return new Retorno<SemConteudo> { Ok = true };
        }

        public async Task<Retorno<SemConteudo>> AdicionarListaLogs(List<Log> logs)
        {
            await _logRepositorio.CreateList(logs);

            return new Retorno<SemConteudo> { Ok = true };
        }
    }
}
