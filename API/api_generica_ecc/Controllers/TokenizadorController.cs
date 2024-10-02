using api_generica_ecc.Models;
using api_generica_ecc.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api_generica_ecc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenizadorController : ControllerBase
    {
        private IConfiguration config;
        public TokenizadorController(IConfiguration iConfig)
        {
            config = iConfig;
        }

        [HttpPost]
        public RetornoAjax GetToken([FromHeader] string apikey)
        {
            RetornoAjax ret = new RetornoAjax();

            if (apikey != null && apikey.Length > 0)
            {
                TokenizadorRepository tokenizador = new TokenizadorRepository();
                string error = "";
                DataSet ds = tokenizador.ValidarApiKey(apikey, ref error);
                if (error == "OK")
                {
                    var usu = ds.Tables[0].AsEnumerable();
                    Usuario usuario = (from item in usu
                                       select new Usuario
                                       {
                                           USUARIO_API = item.Field<string>("USUARIO_API"),
                                       }).FirstOrDefault();
                    if(usuario != null)
                    {
                        string token = CreateToken(usuario);

                        if (token != null && token.Length > 0)
                        {
                            tokenizador.GrabarToken(usuario, token, ref error);
                            if (error == "OK")
                            {
                                ret.ret = "OK";
                                ret.msg = "Token obtenido exitosamente.";
                                ret.debug = String.Empty;
                                ret.values = new List<object>();
                                ret.values.Add(token);
                            }
                            else
                            {
                                ret.ret = "ERROR";
                                ret.msg = "Ha ocurrido un error al registrar token. Por favor contactese con el administrador.";
                                ret.debug = "E-TOKEN-REGISTRAR";
                            }
                        }
                        else
                        {
                            ret.ret = "ERROR";
                            ret.msg = "Ha ocurrido un error al generar el token. Por favor contactese con el administrador.";
                            ret.debug = "E-TOKEN-GENERAL";
                        }
                    }
                }
                else
                {
                    ret.ret = "ERROR";
                    if (error.Contains("ERR001"))
                    {
                        ret.msg = "ApiKey no válida";
                        HttpContext.Response.StatusCode = 401;
                        ret.debug = "E-APIKEY";
                    }
                    else
                    {
                        ret.msg = "Ha ocurrido un error inesperado. Comuniquese con un administrador";
                        HttpContext.Response.StatusCode = 500;
                        ret.debug = "E-GENERAL";
                    }
                }
            }
            return ret;
        }

        private string CreateToken(Usuario usuario)
        {
            string tokenRet = "";
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT").GetSection("key").Value));
                SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.USUARIO_API)
                };

                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    SigningCredentials = creds
                };

                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                tokenRet = tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                tokenRet = "";
            }

            return tokenRet;
        }
    }
}
