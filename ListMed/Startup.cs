using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Hangfire;
using System.Diagnostics;
using api_mobvendas.Models;
using System.Linq;
using api_mobvendas.Utils;
using System.Data.Entity;

[assembly: OwinStartup(typeof(api_mobvendas.Startup))]

namespace api_mobvendas
{
    public class Startup
    {
        private MobVendasContext db = new MobVendasContext();
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("MobVendasContext");

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Autenticacao/Login"),
                ExpireTimeSpan = TimeSpan.FromMinutes(20)

                
            });
            app.UseHangfireDashboard("/painel/tarefas", new DashboardOptions
            {
                Authorization = new[] { new AutorizacaoHangFire() }
            });
            app.UseHangfireServer();
            RecurringJob.AddOrUpdate(() => VerificaPagamentoMensal(), Cron.Daily(23));
        }
        public void VerificaPagamentoMensal()
        {
            Pagamento p = new Pagamento();
            var usuarios = db.USUARIOS_MOBVENDAS.Where(a => a.TIPOPLANO == 3 && a.SITUACAOCONTA == 1 && a.DataProxPagamento > DateTime.Now).ToList();
            foreach(var usu in usuarios)
            {
                string idRecorrencia = usu.ID_RECORRENCIA;
                if (!string.IsNullOrEmpty(idRecorrencia))
                {
                    var recorrencia = p.verificarPagamento(idRecorrencia);
                    if(recorrencia != null)
                    {
                        if (recorrencia.RecurrentPayment.Status == 1)
                        {
                            var modificaUsuario = db.USUARIOS_MOBVENDAS.Find(usu.ID_USUARIO_MOBVENDAS);
                            if(Convert.ToDateTime(recorrencia.RecurrentPayment.NextRecurrency) > modificaUsuario.DataProxPagamento)
                            {
                                var ultimaRecorrencia = recorrencia.RecurrentPayment.RecurrentTransactions.LastOrDefault();
                                if(ultimaRecorrencia != null)
                                {
                                    var ultPag = p.verificarPagamentoMes(ultimaRecorrencia.PaymentId);
                                    if(ultPag != null)
                                    {
                                        PAGAMENTOS_MOBVENDAS pag = new PAGAMENTOS_MOBVENDAS();
                                        pag.MerchantOrderId = ultPag.MerchantOrderId;
                                        pag.DataPagamento = ultPag.Payment.ReceivedDate;
                                        pag.Cartao = ultPag.Payment.CreditCard.CardNumber;
                                        pag.Bandeira = ultPag.Payment.CreditCard.Brand;
                                        pag.Valor = ultPag.Payment.Amount;
                                        pag.DataExpiracao = ultPag.Payment.CreditCard.ExpirationDate;
                                        pag.ID_USUARIO_MOBVENDAS = modificaUsuario.ID_USUARIO_MOBVENDAS;
                                        db.PAGAMENTOS_MOBVENDAS.Add(pag);
                                        db.SaveChanges();

                                    }
                                }
                                // enviar um email de comprovante
                            }
                            modificaUsuario.DataProxPagamento = Convert.ToDateTime(recorrencia.RecurrentPayment.NextRecurrency);

                            db.Entry(modificaUsuario).State = EntityState.Modified;
                            db.SaveChanges();
                            GerarNotificacao(true, modificaUsuario.ID_USUARIO_MOBVENDAS);
                        }
                        else
                        {
                            var modificaUsuario = db.USUARIOS_MOBVENDAS.Find(usu.ID_USUARIO_MOBVENDAS);
                            modificaUsuario.SITUACAOCONTA = recorrencia.RecurrentPayment.Status;
                            modificaUsuario.DataInadimplencia = DateTime.Now;
                            db.Entry(modificaUsuario).State = EntityState.Modified;
                            db.SaveChanges();
                            GerarNotificacao(false, modificaUsuario.ID_USUARIO_MOBVENDAS);
                        }
                    }
        
                }
     
            }
        }
        private void GerarNotificacao(bool ativo, int idUsuario)
        {
            NOTIFICACOES_MOBVENDAS not = new NOTIFICACOES_MOBVENDAS();
            not.USUARIOS_MOBVENDAS.Add(db.USUARIOS_MOBVENDAS.Find(idUsuario));
            if (ativo)
            {
                not.IdTipoMensagem = 4;
                not.TIPO = "E";
                not.ASSUNTO = "Pagamento confirmado";
                not.MENSAGEM_TEXTO = "Confirmamos o pagamento de sua assinatura mensal do Plano Premium";
                not.DTEMISSAO = not.DTSAIDA = DateTime.Now.ToLocalTime();


            }
            else
            {
                not.IdTipoMensagem = 4;
                not.TIPO = "E";
                not.ASSUNTO = "Pagamento não confirmado";
                not.MENSAGEM_TEXTO = "Não confirmamos o pagamento de sua assinatura mensal do Plano Premium";
                not.DTEMISSAO = not.DTSAIDA = DateTime.Now.ToLocalTime();
            }
            db.NOTIFICACOES_MOBVENDAS.Add(not);

            db.SaveChanges();
        }
    }
}
