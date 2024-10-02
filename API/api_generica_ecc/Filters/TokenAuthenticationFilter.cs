using api_generica_ecc.Models;
using api_generica_ecc.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace api_generica_ecc.Filters
{
    public class TokenAuthenticationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var result = true;
            TokenizadorRepository tokenizador = new TokenizadorRepository();
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                result = false;
            }

            string token = "";
            RetornoAjax ret = new RetornoAjax(); ;
            if (result)
            {
                token = context.HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
                token = token.Replace("Bearer ", "");
                string error = "";
                if (!tokenizador.ValidarToken(token, ref error))
                {
                    result = false;
                    ret.ret = "ERROR";
                    if (error.Contains("ERR001"))
                    {
                        ret.msg = "Su token ha expirado.";
                    }
                    else if (error.Contains("ERR002"))
                    {
                        ret.msg = "Los datos de autorización ingresados no son validos";
                    }
                    else
                    {
                        ret.msg = "Ha ocurrido un error inesperado. Contactese con un administrador.";
                    }
                    ret.debug = "";
                }
            }
            else
            {
                ret.ret = "ERROR";
                ret.msg = "Faltan parametros de autorización en la cabecera.";
                ret.debug = "";
            }

            if (!result)
            {
                context.HttpContext.Response.StatusCode = 401;
                context.Result = new UnauthorizedObjectResult(ret);
            }
        }
    }
}
