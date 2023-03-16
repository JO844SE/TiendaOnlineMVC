using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Producto
    {
        //instancia a laclase  de la capa de datos
        private CD_Producto objProducto = new CD_Producto();
        public List<Producto> Listar()
        {
            return objProducto.Listar();
        }



        public int Registrar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del Producto no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripcion del Producto no puede ser vacio";
            }
            else if (obj.oMarca.IdMarca == 0)
            {
                Mensaje = "Debe seleccionar una marca";
            }
            else if (obj.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Debe seleccionar una categoría";
            }
            else if (obj.oUnidad.IdUnidad == 0)
            {
                Mensaje = "Debe seleccionar una unidad de medida";
            }
            else if (obj.Precio == 0)
            {
                Mensaje = "Debe ingresar el precio del producto";
            }
            else if (obj.Stock == 0)
            {
                Mensaje = "Debe ingresar el stock del producto";
            }


            if (string.IsNullOrEmpty(Mensaje))
            {
                return objProducto.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
        }



        public bool Editar(Producto obj, out String Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del Producto no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripcion del Producto no puede ser vacio";
            }
            else if (obj.oMarca.IdMarca == 0)
            {
                Mensaje = "Debe seleccionar una marca";
            }
            else if (obj.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Debe seleccionar una categoría";
            }
            else if (obj.oUnidad.IdUnidad == 0)
            {
                Mensaje = "Debe seleccionar una unidad de medida";
            }
            else if (obj.Precio == 0)
            {
                Mensaje = "Debe ingresar el precio del producto";
            }
            else if (obj.Stock == 0)
            {
                Mensaje = "Debe ingresar el stock del producto";
            }


            if (string.IsNullOrEmpty(Mensaje))
            {
                return objProducto.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }



        public bool GuardarDatosImagen(Producto obj, out string Mensaje) {
            return objProducto.GuardarDatosImagen(obj, out Mensaje);
        }


        public bool Eliminar(int id, out string Mensaje)
        {
            return objProducto.Eliminar(id, out Mensaje);
        }
    }
}
