using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_mobvendas.DTOS
{
    public class notificacoes_mobvendas
    {
        public int id_notificacoes { get; set; }
        public string assunto { get; set; }
        public string mensagem_texto { get; set; }
        public string mensagem_html { get; set; }
        public string dtemissao { get; set; }
        public string dtsaida { get; set; }
        public string tipo { get; set; }
    }
}