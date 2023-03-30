using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacionAdmin.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CambiarClave()
        {
            return View();
        }

        public ActionResult Reestablecer()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            Usuario oUsuario = new Usuario();
            oUsuario = new CN_Usuarios().Listar().Where(u => u.Correo == correo && u.Clave == CN_Recursos.Convertirsha256(clave)).FirstOrDefault();

            if (oUsuario == null)
            {

                ViewBag.Error = "Coreo y contraseña incorectos";
                return View();

            }
            else
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

    }
}