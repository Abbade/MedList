using ListMed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListMed.Geral
{
    public class Anexo
    {
        private MedListContext db = new MedListContext();

        public static byte[] ArqParaByte(HttpPostedFileBase arq)
        {
            if (arq != null)
            {
                byte[] novoArq = new byte[arq.ContentLength];
                arq.InputStream.Read(novoArq, 0, arq.ContentLength);
                return novoArq;
            }
            else
                return null;
        }

        public static bool EhImagem(HttpPostedFileBase arq)
        {
            if (arq.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formatos = new string[] { ".jpg", ".png", ".gif", ".jpeg" }; // add more if u like...

            // linq from Henrik Stenbæk
            return formatos.Any(item => arq.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

    }
}