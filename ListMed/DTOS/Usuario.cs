using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_mobvendas.DTOS
{
    public class Usuario
    {
        public int? id { get; set; }
        public string codvend { get; set; }
        public string uf { get; set; }
        public string codempresa { get; set; }
        public string ipservidor { get; set; }
        public List<object> permissoes { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string imei { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string pais { get; set; }
        public string senha { get; set; }
        public string idgoogle { get; set; }
        public string idfacebook { get; set; }
        public string data_nascimento { get; set; }
        public DateTime? data_pgto { get; set; }
        public DateTime? data_ativacao { get; set; }
        public DateTime? datacancelamento { get; set; }
        public int? situacaoconta { get; set; }
        public string sexo { get; set; }
        public string codativacao { get; set; }
        public bool? conta_ativa { get; set; }
        public int? tipoplano { get; set; }
        public string foto { get; set; }
        public List<atividades> atividades { get; set; }
        public List<segmentos> segmentos { get; set; }
    }
}