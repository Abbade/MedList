using Hangfire.Dashboard;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_mobvendas.Utils
{
    public class AutorizacaoHangFire : IDashboardAuthorizationFilter
    {
       
        public bool Authorize(DashboardContext context)
        {
    
            var owinContext = new OwinContext(context.GetOwinEnvironment());


            return owinContext.Authentication.User.Identity.IsAuthenticated;
        }
       
    }
}