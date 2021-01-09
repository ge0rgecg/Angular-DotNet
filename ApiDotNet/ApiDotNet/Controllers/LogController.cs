using Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servico.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ApiDotNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogServico _logServico;

        public LogController(ILogServico logServico)
        {
            _logServico = logServico == null ? throw new ArgumentNullException("logServico") : logServico;
        }

        [HttpGet("id")]
        public IActionResult Get(int id)
        {
            if (id < 0)
            {
                return BadRequest("Campo Id inválido.");
            }

            return Ok(_logServico.ObterLogs());
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_logServico.ObterLogs());
        }

        [HttpPost]
        public IActionResult Post(Log log)
        {
            var notificacao = log.Validacao();

            if (notificacao.Any())
            {
                return BadRequest(notificacao);
            }

            return Ok(_logServico.AdicionarLog(log));
        }

        [HttpPost("UploadFIle")]
        public IActionResult Post(List<IFormFile> files)
        {
            foreach (var item in files)
            {
                if (item.ContentType != "text/plain")
                {
                    return BadRequest("Apenas arquivos .txt são válidos");
                }
            }

            var logs = new List<Log>();

            foreach (var formFile in files)
            {
                using (var reader = new StreamReader(formFile.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                    {
                        var linha = reader.ReadLine();

                        logs.Add(mapStringToLog(linha));
                    }
                }
            }

            return Ok(_logServico.AdicionarListaLogs(logs));
        }

        private Log mapStringToLog(string line)
        {
            var campo1 = line.IndexOf("[") + 1;
            var campo2 = line.IndexOf("\"") + 1;
            var campo3 = line.LastIndexOf("\"") + 1;
            var campo4 = line.LastIndexOf(" ") + 1;

            var codigo = line.Substring(0, line.IndexOf(" "));

            var data = line.Substring(campo1, line.IndexOf("]") - campo1);

            var texto = line.Substring(campo2, campo3 - campo2 - 1);

            var codigoRetorno = line.Substring(campo3, campo4 - campo3);

            var retorno = line.Substring(campo4);

            var diaMesAno = data.Split("/");
            int dia = int.Parse(diaMesAno[0]);
            int mes = monthInInt(diaMesAno[1]);

            var anoHoras = diaMesAno[2].Split(":");

            int ano = int.Parse(anoHoras[0]);
            int hora = int.Parse(anoHoras[1]);
            int minuto = int.Parse(anoHoras[2]);

            var segundoUtc = anoHoras[3].Split(" ");

            int segundo = int.Parse(segundoUtc[0]);

            var dataUtc = new DateTime(ano, mes, dia, hora, minuto, segundo);

            dataUtc = dataUtc.AddHours(int.Parse(segundoUtc[1].Substring(0, 3))).AddMinutes(int.Parse(segundoUtc[1].First() +segundoUtc[1].Substring(3, 2)));

            var log = new Log()
            {
                CodigoRetorno = int.Parse(codigoRetorno),
                Chave = codigo,
                DataLog = dataUtc,
                Texto = texto
            };

            if (!retorno.Equals("-"))
            {
                log.Retorno = int.Parse(retorno);
            }

            return log;
        }

        private int monthInInt(string monthString)
        {
            Dictionary<string, int> months = new Dictionary<string, int>()
            {
                { "jan", 01},
                { "feb", 02},
                { "mar", 03},
                { "apr", 04},
                { "may", 05},
                { "jun", 06},
                { "jul", 07},
                { "aug", 08},
                { "sep", 09},
                { "oct", 10},
                { "nov", 11},
                { "dec", 12},
            };
            foreach (var month in months)
            {
                if (monthString.ToLower().Equals(month.Key))
                {
                    return month.Value;
                }
            }

            return 0;
        }
    }
}
