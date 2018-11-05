using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace api_mobvendas.DTOS.Respostas
{
    public  class Retorno
    {
        enum Codigo { CadastroInexistente = 1, CadastroExistente, ErroCartao, ErroInterno, Ok, ErroGeral };

        public  object UsuarioExiste()
        {
            return new 
            {
                idResposta = (int) Codigo.CadastroExistente,
                msgResposta = "Ja existe um cadastro com esse e-mail!"
            };
        }
        public object UsuarioInexistente()
        {
            return new
            {
                idResposta = (int)Codigo.CadastroInexistente,
                msgResposta = "Cadastro Inexistente"
            };
        }
        public object ErroGeral(string msg)
        {
            return new
            {
                idResposta = (int)Codigo.ErroGeral,
                msgResposta =  msg
            };
        }
        public object ErroInterno(string msg)
        {
            return new
            {
                idResposta = (int)Codigo.ErroInterno,
                msgResposta = msg
            };
        }
        public object ErroCartao(string msg)
        {
            return new
            {
                idResposta = (int)Codigo.ErroCartao,
                msgResposta = msg
            };
        }
        public object Ok(string msg)
        {
            return new
            {
                idResposta = (int)Codigo.Ok,
                msgResposta = msg
            };

          
        }
        public object OkCadastro(int idUsuario)
        {
            return new
            {
                idResposta = (int)Codigo.Ok,
                idUsuario = idUsuario
            };
        }
        public object ErroCadastro(int idUsuario, string msg)
        {
            return new
            {
                idResposta = (int)Codigo.ErroGeral,
                idUsuario = idUsuario,
                msgResposta = msg
            };
        }
        public object OkCarregaDadosConta(object dados)
        {
            return dados;
        }
    
    }
}