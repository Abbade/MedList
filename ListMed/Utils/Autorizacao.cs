using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace api_mobvendas.Utils
{
    public class Autorizacao : AuthorizationFilterAttribute
    {

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string token = "9149392306F6D422C40498A8B9BF92483002384C1547EBC1AFD4FA3E2890C091";
            var authorizationHeader = actionContext.Request.Headers.Authorization;
            if (authorizationHeader == null || authorizationHeader.Scheme != "MobId" || authorizationHeader.Parameter != token)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }
    }
}