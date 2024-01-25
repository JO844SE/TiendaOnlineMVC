using CapaEntidad;
using CapaNegocio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaPresentacionAdmin.Filter;

namespace CapaPresentacionAdmin.Controllers
{
    public class MantenedorController : Controller
    {
        // GET: Mantenedor
        //se valida la sesion, unicamente con aquellos que han iniciado sesión
        [ValidarSession]
        [Authorize]
        public ActionResult Categoria()
        {
            return View();
        }

        //se valida la sesion, unicamente con aquellos que han iniciado sesión
        [ValidarSession]
        [Authorize]
        public ActionResult Marca()
        {
            return View();
        }

        //se valida la sesion, unicamente con aquellos que han iniciado sesión
        [ValidarSession]
        [Authorize]
        public ActionResult UnidadMedida()
        {
            return View();
        }
        //se valida la sesion, unicamente con aquellos que han iniciado sesión
        [ValidarSession]
        [Authorize]
        public ActionResult Producto()
        {
            return View();
        }




        //+++++++++++++++++++++++++++++++++++++ CATEGORIA ++++++++++++++++++++++++++++++++++++++++++++++++
        #region CATEGORIA
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

        #endregion

        //+++++++++++++++++++++++++++++++++++++ MARCA ++++++++++++++++++++++++++++++++++++++++++++++++++++
        #region MARCA
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

        #endregion

        //+++++++++++++++++++++++++++++++++++++ UNIDAD DE MEDIDA +++++++++++++++++++++++++++++++++++++++++
        #region UNIDAD DE MEDIDA
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

        #endregion

        //+++++++++++++++++++++++++++++++++++++ PRODUCTO +++++++++++++++++++++++++++++++++++++++++++++++++
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
            string mensaje = string.Empty;
            bool operacion_exitosa = true;
            bool guardar_imagen_exito = true;


            Producto oProducto = new Producto();
            oProducto = JsonConvert.DeserializeObject<Producto>(objeto);

            decimal precio;

            if (decimal.TryParse(oProducto.PrecioTexto, NumberStyles.AllowDecimalPoint, new CultureInfo("es-GT"), out precio))
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
                operacion_exitosa = new CN_Producto().Editar(oProducto, out mensaje);
            }


            if (operacion_exitosa)
            {
                if (archivoImagen != null)
                {
                    string ruta_guardar = ConfigurationManager.AppSettings["ServidorFotos"];
                    string extension = Path.GetExtension(archivoImagen.FileName);
                    string nombre_imagen = string.Concat(oProducto.IdProducto.ToString(), extension);


                    try
                    {
                        archivoImagen.SaveAs(Path.Combine(ruta_guardar, nombre_imagen));
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        guardar_imagen_exito = false;
                       
                    }

                    if (guardar_imagen_exito)
                    {
                        oProducto.RutaImagen = ruta_guardar;
                        oProducto.NombreImagen = nombre_imagen;
                        bool respuesta = new CN_Producto().GuardarDatosImagen(oProducto, out mensaje);
                    }
                    else
                    {
                        mensaje = "Se guardo el producto pero hubo problemas con la imágen";
                    }
                }
            }


            return Json(new { operacionExitosa = operacion_exitosa, idGenerado=oProducto.IdProducto, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        //Recupera la imagen de cada producto 
        [HttpPost]
        public JsonResult ImagenProducto(int id)
        {
            bool conversion;
            Producto oProducto = new CN_Producto().Listar().Where(p => p.IdProducto == id).FirstOrDefault();
            string textoBase64 = CN_Recursos.ConvertirBase64(Path.Combine(oProducto.RutaImagen,oProducto.NombreImagen), out conversion);


            return Json(new
            {
                conversion = conversion,
                textobase64 = textoBase64,
                extension = Path.GetExtension(oProducto.NombreImagen)
            },
            JsonRequestBehavior.AllowGet
            );
        }


        //Elimina un producto
        [HttpPost]
        public JsonResult EliminarProducto(int id)
        {
            object respuesta;
            string mensaje = string.Empty;

            respuesta = new CN_Producto().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion


    }
}