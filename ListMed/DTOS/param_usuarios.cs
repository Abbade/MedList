using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_mobvendas.DTOS
{
    public class param_usuarios
    {
        public int id_usuario_mobvendas { get; set; }
        public string dt_ult_mensagem { get; set; }
        public string dt_ult_backup { get; set; }
        public bool? habconsultclieativo { get; set; }
        public bool? habcontrolcreditclie { get; set; }
        public bool? habbloqcadclieexistente { get; set; }
        public bool? habbloqconsultwebcliecnpj { get; set; }
        public bool? habbloqconsultwebcep { get; set; }
        public bool? habexcluirclie { get; set; }
        public bool? habbloqobservacaoclie { get; set; }
        public bool? habbloqcondpgtoxclie { get; set; }
        public bool? habconsultprodativo { get; set; }
        public bool? habcontrolqtdminvenda { get; set; }
        public bool? habcontrolqtdminestoque { get; set; }
        public bool? habcontroldescontoxqtdminvenda { get; set; }
        public bool? habexcluirproduto { get; set; }
        public bool? habbloqcontrolvariacao { get; set; }
        public bool? habbloqfoto { get; set; }
        public bool? habbloqcontrolpeso { get; set; }
        public bool? habbloqcontrolprodxfornec { get; set; }
        public bool? habbloqcadcodigobarra { get; set; }
        public bool? habbloqobservacaoprod { get; set; }
        public bool? habbloqdescitempedido { get; set; }
        public bool? habbloqacresitempedido { get; set; }
        public bool? habbloqcontrollogisticafretepedido { get; set; }
        public bool? habbloqdesctotalpedido { get; set; }
        public bool? habbloqacrestotalpedido { get; set; }
        public bool? habbloqorcestoque { get; set; }
        public bool? habvisumargemproduto { get; set; }
        public bool? bloqitemduplicadopedido { get; set; }
        public bool? habbloqvendaestoque { get; set; }
        public bool? bloqconsultcalcfrete { get; set; }
    }
}