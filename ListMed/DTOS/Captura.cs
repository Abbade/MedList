using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_mobvendas.DTOS
{
    public class Captura
    {
        public Captura()
        {
            Links = new List<Link>();
        }
        public int Status { get; set; }
        public string Tid { get; set; }
        public string ProofOfSale { get; set; }
        public string AuthorizationCode { get; set; }
        public string ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public List<Link> Links { get; set; }
    }
}