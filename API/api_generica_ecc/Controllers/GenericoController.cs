using api_generica_ecc.Filters;
using api_generica_ecc.Models;
using api_generica_ecc.Repository;
using api_generica_ecc.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace api_generica_ecc.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [TokenAuthenticationFilter]
    public class GenericoController : ControllerBase
    {
        private IConfiguration config;
        private IWebHostEnvironment webHostEnvironment;
        public GenericoController(IConfiguration iConfig, IWebHostEnvironment webHostEnvironment)
        {
            config = iConfig;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public RetornoAjax ProcesarArchivo([FromBody] RequestAPIArchivo request)
        {
            RetornoAjax ret = new RetornoAjax();
            bool datosValidos = true;
            string mensaje = "";
            Validaciones val = new Validaciones();
            try
            {
                ////VALIDACION DE CAMPOS DE REQUEST
                if (request.NOMBRE_ARCHIVO == null || request.NOMBRE_ARCHIVO.Length == 0)
                {
                    datosValidos = false;
                    mensaje += "El campo NOMBRE_ARCHIVO no cuenta con información. ";
                }
                if (datosValidos)
                {
                    string error = "";
                    GenericoRepository oracle = new GenericoRepository(config, webHostEnvironment);
                    DataSet ds = oracle.ProcesarArchivo(request, ref error);
                    if (error == "" || error == "OK")
                    {
                        ret.ret = "OK";
                        ret.msg = String.Empty;
                        ret.debug = String.Empty;
                        ret.values = new List<object>();
                        ret.ret = "OK";
                        ret.msg = "El proceso de carga se ha completado exitosamente.";
                        ret.debug = "";

                    }
                    else
                    {
                        ret.ret = "ERROR";
                        ret.msg = "Error en la ejecución de la API: " + error;
                        ret.debug = "E-PROCESO-BD";
                    }
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.msg = "Errores de validación: " + mensaje;
                    ret.debug = "E-VALIDACION";
                }

            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Ocurrio un error inesperado: " + ex.Message;
                ret.debug = "E-GENERAL";
            }

            return ret;
        }

    }
}
