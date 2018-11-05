using api_mobvendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_mobvendas.DTOS.Respostas
{
    public class CarregaParametros
    {
        public CarregaParametros()
        {
            parametros = new List<PARAM_MOBVENDAS>();
        }
        public int idResposta { get; set; }
        public string msgResposta { get; set; }
        public List<PARAM_MOBVENDAS> parametros { get; set; }
    }
}