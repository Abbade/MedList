using api_mobvendas.Controllers;
using api_mobvendas.DTOS;
using api_mobvendas.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Net.WebSockets;
using System.Text;
using System.Web;

namespace api_mobvendas.Utils
{
    public class Pagamento
    {
        private HttpClient client = new HttpClient();
        private MobVendasContext db = new MobVendasContext();
        private EnviarNotificacoes e = new EnviarNotificacoes();

        enum Codigo { CadastroInexistente = 1, CadastroExistente, ErroCartao, ErroGeral, Ok };
        private void iniRequisicao()
        {
            client.BaseAddress = new Uri("https://apisandbox.cieloecommerce.cielo.com.br/");
            client.Timeout = TimeSpan.FromMinutes(1);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("MerchantId", "03adf919-a2ac-440f-8560-324455424238");
            client.DefaultRequestHeaders.Add("MerchantKey", "SWOYPMMYOIMARCHBWYMSMNGIVEZNMWFIBHKMNQUS");
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }
        private void iniConsulta()
        {
            client.BaseAddress = new Uri("https://apiquerysandbox.cieloecommerce.cielo.com.br/");
            client.Timeout = TimeSpan.FromMinutes(1);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("MerchantId", "03adf919-a2ac-440f-8560-324455424238");
            client.DefaultRequestHeaders.Add("MerchantKey", "SWOYPMMYOIMARCHBWYMSMNGIVEZNMWFIBHKMNQUS");
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }
        public object DesabilitarPagamento(string id)
        {
            iniRequisicao();


            var response = client.PutAsync("1/RecurrentPayment/"  + id + "/Deactivate", null).Result;
            if (response.IsSuccessStatusCode)
                return new
                {
                    idResposta = (int)Codigo.Ok,
                    msgResposta = "Pagamento desabilitado com sucesso!"
                };

            else
                return new
                {
                    idResposta = (int)Codigo.ErroCartao,
                    msgResposta = "Ocorreu um erro ao desabilitar pagamento!"
                };
        }
        public object ReabilitarPagamento(string id)
        {
            iniRequisicao();


            var response = client.PutAsync("1/RecurrentPayment/" + id + "/Reactivate", null).Result;
            if (response.IsSuccessStatusCode)
                return new
                {
                    idResposta = (int)Codigo.Ok,
                    msgResposta = "Pagamento reabilitado com sucesso!"
                };

            else
                return new
                {
                    idResposta = (int)Codigo.ErroCartao,
                    msgResposta = "Ocorreu um erro ao reabilitar pagamento!"
                };
        }
        public object ModificaPagamento(string id, object m)
        {
            iniRequisicao();
            string json = JsonConvert.SerializeObject(m);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = client.PutAsync("1/RecurrentPayment/" + id + "/Payment", httpContent).Result;
            if (response.IsSuccessStatusCode)
                return new
                {
                    idResposta = (int)Codigo.Ok,
                    msgResposta = "Pagamento Modificado com sucesso!"
                };

            else
            {
                var result = response.Content.ReadAsStringAsync().Result;
                return new
                {
                    idResposta = (int)Codigo.ErroCartao,
                    msgResposta = "Ocorreu um erro ao modificar pagamento!"
                };
            }
             
        }
        public object RealizarPagamento(RequisicaoPagamento p)
        {
            iniRequisicao();

            Recorrencia requisicao = new Recorrencia();
            requisicao.Customer = new Customer();
            requisicao.Payment = new Payment();
            requisicao.Payment.RecurrentPayment = new RecurrentPayment();
            requisicao.Payment.CreditCard = new CreditCard();
            requisicao.MerchantOrderId = DateTime.Now.ToString("ddMMyyyyHHmmss") + p.id_usuario_mobvendas.ToString();
            requisicao.Customer.Name = p.name;
            requisicao.Payment.Type = "CreditCard";
            requisicao.Payment.Amount = Convert.ToDecimal(p.amount.Replace(".", ",")) * 100;
            requisicao.Payment.Installments = 1;
            requisicao.Payment.SoftDescriptor = "MobVendas";
            requisicao.Payment.RecurrentPayment.AuthorizeNow = true;
            requisicao.Payment.CreditCard.CardNumber = p.cardnumber;
            requisicao.Payment.CreditCard.Holder = p.name;
            requisicao.Payment.CreditCard.ExpirationDate = p.expirationdate;
            requisicao.Payment.CreditCard.SecurityCode = p.securityCode;
            requisicao.Payment.CreditCard.Brand = p.brand;

            string json = JsonConvert.SerializeObject(requisicao);
            
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {

                var response = client.PostAsync("1/sales", httpContent).Result;
                if(response.IsSuccessStatusCode)
                {
                    var resposta = JsonConvert.DeserializeObject<Recorrencia>(response.Content.ReadAsStringAsync().Result);
                                      
                    if(resposta.Payment.ReturnCode == "4" || resposta.Payment.ReturnCode == "6")
                    {
                        var resp = JsonConvert.DeserializeObject<Captura>(client.PutAsync("1/sales/"+ resposta.Payment.PaymentId + "/capture", null).Result.Content.ReadAsStringAsync().Result);
                        PAGAMENTOS_MOBVENDAS pag = new PAGAMENTOS_MOBVENDAS();
                        // pagamentos
                        pag.ID_USUARIO_MOBVENDAS = Convert.ToInt32(p.id_usuario_mobvendas);
                        pag.CodRetorno = Convert.ToInt32(resposta.Payment.ReturnCode);
                        pag.Cartao = resposta.Payment.CreditCard.CardNumber;
                        pag.Bandeira = resposta.Payment.CreditCard.Brand;
                        pag.DataPagamento = DateTime.Now;
                        pag.Valor = resposta.Payment.Amount;
                        pag.MerchantOrderId = resposta.MerchantOrderId;
                        pag.DataExpiracao = resposta.Payment.CreditCard.ExpirationDate;
                        db.PAGAMENTOS_MOBVENDAS.Add(pag);
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception ex1)
                        {
                            return new
                            {
                                idResposta = (int)Codigo.ErroCartao,
                                msgResposta = ex1.Message
                            };
                        }
 

                        var usuMob = db.USUARIOS_MOBVENDAS.Find(Convert.ToInt32(p.id_usuario_mobvendas));
                        usuMob.ID_RECORRENCIA = resposta.Payment.RecurrentPayment.RecurrentPaymentId;
                        usuMob.SITUACAOCONTA = 1;
                        usuMob.TIPOPLANO = 3;
                        usuMob.DataProxPagamento = Convert.ToDateTime(resposta.Payment.RecurrentPayment.NextRecurrency);
                        db.Entry(usuMob).State = EntityState.Modified;
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception ex2)
                        {
                            return new
                            {
                                idResposta = (int)Codigo.ErroCartao,
                                msgResposta = ex2.Message
                            };
                        }
     

                        var ex = db.USUARIOS_MOBVENDAS.Find(pag.ID_USUARIO_MOBVENDAS);
                        UsuarioSimples usu = new UsuarioSimples()
                        {
                            email = ex.EMAIL,
                            nome = ex.NOME,
                            dataProxPag = ex.DataProxPagamento
                        };

                        try
                        {
                            enviarEmail(pag, usu);
                        }
                        catch (Exception ex3)
                        {
                            return new
                            {
                                idResposta = (int)Codigo.ErroCartao,
                                msgResposta = ex3.Message
                            };
                        }
                        e.EnviarNotificacao("E", "Pagamento realizado com sucesso!", "Pagamento", usuMob.ID_USUARIO_MOBVENDAS, 4);
                        return new
                        {
                            idResposta = (int) Codigo.Ok,
                            msgResposta = "Pagamento feito com sucesso!",
                            dataPag = pag.DataPagamento,
                            dataProxPag = usuMob.DataProxPagamento,
                            cartaoCredito = pag.Cartao,
                            bandeira = pag.Bandeira,
                            valor = pag.Valor
                        };
                    }
                    else
                    {
                        /* 
                          *  5: Não autorizada
                             57: Cartão expirado
                             78: Cartão bloqueado
                             99: Time out
                             77: Carão cancelado
                             70: Problemas com cartão de crédito
                          */
                        switch (resposta.Payment.ReturnCode)
                        {
                            case "5":
                                return new {
                                    idResposta = (int)Codigo.ErroCartao,
                                    msgResposta = "Compra não autorizada!"
                                };
                            case "57":
                                return new
                                {
                                    idResposta = (int)Codigo.ErroCartao,
                                    msgResposta = "Cartão Expirado!"
                                };
                            case "78":
                                return new
                                {
                                    idResposta = (int)Codigo.ErroCartao,
                                    msgResposta = "Cartão bloqueado!"
                                };
                            case "99":
                                return new
                                {
                                    idResposta = (int)Codigo.ErroCartao,
                                    msgResposta = "Time Out!"
                                };
                            case "77":
                                return new
                                {
                                    idResposta = (int)Codigo.ErroCartao,
                                    msgResposta = "Cartão cancelado!"
                                };
                            case "70":
                                return new
                                {
                                    idResposta = (int)Codigo.ErroCartao,
                                    msgResposta = "Problemas com cartão de crédito!"
                                };
                        }
                    }
                }
                return new
                {
                    idResposta = (int)Codigo.ErroGeral,
                    msgResposta = "Erro no pagamento!"      
                }; 
            }
            catch (Exception ex)
            {
                return new
                {
                    idResposta = (int)Codigo.ErroGeral,
                    msgResposta = ex.Message
                };
            }
 

        
        }
        public ConsultaRecorrencia verificarPagamento(string id)
        {
            iniConsulta();


            var response = client.GetAsync("1/RecurrentPayment/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resposta = JsonConvert.DeserializeObject<ConsultaRecorrencia>(response.Content.ReadAsStringAsync().Result);
                return resposta;
                
            }
            else
            {
                return null;
            }
     
        }
        public Recorrencia verificarPagamentoMes(string id)
        {
            

            var response = client.GetAsync("1/sales/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resposta = JsonConvert.DeserializeObject<Recorrencia>(response.Content.ReadAsStringAsync().Result);
                return resposta;

            }
            else
            {
                return null;
            }
        }
        private void enviarEmail(PAGAMENTOS_MOBVENDAS pag, UsuarioSimples usu)
        {
          var parametrosEmail = db.PARAM_MOBVENDAS.ToList();
            int port = Convert.ToInt32(parametrosEmail.Where(a => a.CAMPO.Contains("PORT")).FirstOrDefault().VALOR);
            string userName = parametrosEmail.Where(a => a.CAMPO.Contains("USERNAME")).FirstOrDefault().VALOR;
            string senha = parametrosEmail.Where(a => a.CAMPO.Contains("PASSWORD")).FirstOrDefault().VALOR;
            string host = parametrosEmail.Where(a => a.CAMPO.Contains("HOST")).FirstOrDefault().VALOR;
            string subject = "Comprovante Pagamento Mob Vendas";

            string FromMail = "automatico@mobvendas.com.br";
            string emailTo = usu.email;
            string body = montarBody(usu, pag);
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(host);
            mail.IsBodyHtml = true;
            mail.From = new MailAddress(FromMail);
            mail.To.Add(emailTo);
            mail.Subject = subject;
            mail.Body = body;
            SmtpServer.Port = port;

            SmtpServer.Credentials = new System.Net.NetworkCredential(userName, senha);
            SmtpServer.EnableSsl = false;
            SmtpServer.Send(mail);
        }

        private string montarBody(UsuarioSimples usu, PAGAMENTOS_MOBVENDAS pag)
        {
            string htmlPagamento = "<html>" +
                    "<head>" +
                    "<title>Corpo Email</title>" +
                    "</head>" +
                    "<style>" +
                    "</style>" +
                    "<body>" +
                    "<div style=\"text-align: center; background-color: #001346;  height: 160px; padding: 30px;\">" +
                    "<img src=\"http://cpro42174.publiccloud.com.br/mobvendas/Imagens/logo_mobvendas.png\" width=\"120\" height=\"120\" alt=\"logo_mobvendas\"><br>" +
                    "<span style=\"color: white; font-size: 34px;\">MOB VENDAS</span>" +
                    "</div>" +
                    "<div style=\"margin-top: 50px; margin-left: 50px; margin-right: 50px; font-size: 25px;\">" +
                    "<span>Olá " + usu.nome + ",</span><br><br>" +
                    "<span>Confirmamos o pagamento de sua assinatura mensal do Plano Premium válida de " + DateTime.Now.Date.ToString("dd/MM/yyyy") + " à " + Convert.ToDateTime(usu.dataProxPag).ToString("dd/MM/yyyy") + "</span><br><br>" +
                    "<strong>Emissão: </strong><span>" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "</span><div style=\"float: right;\"><strong >Recebido em: </strong>" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "</div><br><br>" +
                    "<strong>Método de Pagamento: </strong> <span>Cartão de Crédito " + pag.Bandeira + "</span><br><br>" +
                    "<strong>Número do Pedido: </strong><span> " + pag.MerchantOrderId + "</span><br><br><br>" +
                    "</div>" +
                    "<div style=\"text-align: center; color: #d6951d; font-size: 25px;\" >" +
                    "<span>Para dúvidas e sugestões acesse nosso chat no app!</span>" +
                    "</div>" +
                    "<div style=\"margin-top: 30px;text-align: center; color: #8b838a; font-size: 25px;\" >" +
                    "<span>Atenção! E-mail enviado automaticamente. Não responda.</span>" +
                    "</div>" +
                    "</body>" +
                    "</html>";
            return htmlPagamento;
        }
    }
}