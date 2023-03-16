using CapaEntidad;
using CapaNegocio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacionAdmin.Controllers
{
    public class MantenedorController : Controller
    {
        // GET: Mantenedor
        public ActionResult Categoria()
        {
            return View();
        }


        //Controlador para listar una categoria
        [HttpGet]
        public JsonResult ListarCategoria()
        {
            List<Categoria> oLista = new List<Categoria>();
            oLista = new CN_Categoria().Listar();

            //return Json(oLista, JsonRequestBehavior.AllowGet);

            //Estructura que recibe la libreria de Json
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }


        //Método que permite guardar y editar una categoria
        [HttpPost]
        public JsonResult GuardarCategoria(Categoria objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdCategoria == 0)
            {
                resultado = new CN_Categoria().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Categoria().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        //Método que permite eliminar categoria
        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            object respuesta;
            string mensaje = string.Empty;

            respuesta = new CN_Categoria().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }




        //Controlador para listar una marca
        [HttpGet]
        public JsonResult ListarMarca()
        {
            List<Marca> oLista = new List<Marca>();
            oLista = new CN_Marca().Listar();

            //return Json(oLista, JsonRequestBehavior.AllowGet);

            //Estructura que recibe la libreria de Json
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }


        //Método que permite guardar y editar una marca
        [HttpPost]
        public JsonResult GuardarMarca(Marca objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdMarca == 0)
            {
                resultado = new CN_Marca().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Marca().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        //Método que permite eliminar marca
        [HttpPost]
        public JsonResult EliminarMarca(int id)
        {
            object respuesta;
            string mensaje = string.Empty;

            respuesta = new CN_Marca().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }





        //Controlador para listar una unidad de medida
        [HttpGet]
        public JsonResult ListarUnidad()
        {
            List<Unidad> oLista = new List<Unidad>();
            oLista = new CN_Unidad().Listar();

            //return Json(oLista, JsonRequestBehavior.AllowGet);

            //Estructura que recibe la libreria de Json
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }


        //Método que permite guardar y editar una unidad de medida
        [HttpPost]
        public JsonResult GuardarUnidad(Unidad objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdUnidad == 0)
            {
                resultado = new CN_Unidad().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Unidad().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        //Método que permite eliminar una unidad de medida
        [HttpPost]
        public JsonResult EliminarUnidad(int id)
        {
            object respuesta;
            string mensaje = string.Empty;

            respuesta = new CN_Unidad().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }



        //+++++++++++++++++++++++++++++++++++++ PRODUCTO ++++++++++++++++++++++++++++++++++++++++++++++++++++++
        #region PRODUCTO
        [HttpGet]
        public JsonResult ListarProducto()
        {
            List<Producto> oLista = new List<Producto>();
            oLista = new CN_Producto().Listar();

            //return Json(oLista, JsonRequestBehavior.AllowGet);

            //Estructura que recibe la libreria de Json
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult GuardarProducto(string objeto, HttpPostedFileBase archivoImagen)
        {
            object resultado;
            string mensaje = string.Empty;
            bool operacion_exitosa = true;
            bool guardar_imagen_exito = true;


            Producto oProducto = new Producto();
            oProducto = JsonConvert.DeserializeObject<Producto>(objeto);

            decimal precio;

            if (decimal.TryParse(oProducto.PrecioTexto,NumberStyles.AllowDecimalPoint,new CultureInfo("es-GT"), out precio))
            {
                oProducto.Precio = precio;
            }
            else
            {
                return Json(new { operacion_exitosa = false, mensaje = "El formato del precio debe ser ##.##" }, JsonRequestBehavior.AllowGet);
            }


            if (oProducto.IdProducto == 0)
            {
                int idproductoGenerado = new CN_Producto().Registrar(oProducto, out mensaje);
                if (idproductoGenerado != 0)
                {
                    oProducto.IdProducto = idproductoGenerado;
                }
                else
                {
                    operacion_exitosa = false;
                }
            }
            else
            {
                resultado = new CN_Producto().Editar(oProducto, out mensaje);
            }


            if (operacion_exitosa)
            {
                if (archivoImagen != null)
                {

                }
            }



            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult EliminarProducto(int id)
        {
            object respuesta;
            string mensaje = string.Empty;

            respuesta = new CN_Producto().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion















        public ActionResult Marca()
        {
            return View();
        }

        public ActionResult UnidadMedida()
        {
            return View();
        }
        public ActionResult Producto()
        {
            return View();
        }
    }
}