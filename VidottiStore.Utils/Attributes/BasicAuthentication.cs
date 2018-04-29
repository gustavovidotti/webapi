using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace VidottiStore.Utils.Attributes
{
    public class BasicAuthentication : AuthorizationFilterAttribute
    {
        // Esse é um padrão ele diz como tem que ser um header de autencicação o que eu preciso retornar , se por algum motivo de 401 ele avisara o tipo de autenticação basico usuario e senha
        private const string BasicAuthResponseHeader = "www-autenticate";
        private const string BasicAuthResponseHeaderValue = "Basic";

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // var header = actionContext.Request.Headers; // aqui estou armazenando na variavel o conteudo do cabeçalho na variavel header

            AuthenticationHeaderValue AuthValue = actionContext.Request.Headers.Authorization;
            if (AuthValue != null && !String.IsNullOrEmpty(AuthValue.Parameter) && AuthValue.Scheme == BasicAuthResponseHeaderValue) // se for diferene de nullo e não for vazia e o schema for igual Basic => BasicAuthResponseHeaderValue
            {
                string[] credentials = Encoding.ASCII.GetString(Convert.FromBase64String(AuthValue.Parameter)).Split(new[] { ':' });
                // string é um array que recebe os parametros desemcriptados para fazer a autheticação
            }
            else
            {
                actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);  // Criando uma resposta de não autorizado 
                return;
            }

            base.OnAuthorization(actionContext);
        }
    }
}
