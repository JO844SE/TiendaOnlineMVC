using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Carrito
    {
        //instancia a laclase CD_Usuario de la capa de datos
        private CD_Carrito objCarrito = new CD_Carrito();

        public bool ExisteCarrito(int idcliente, int idproducto){
            return objCarrito.ExisteCarrito(idcliente, idproducto);
        }


        public bool OperacionCarrito(int idcliente, int idproducto, bool sumar, out string Mensaje){
            return objCarrito.OperacionCarrito(idcliente, idproducto, sumar, out Mensaje);
        }

        public int CantidadEnCarrito(int idcliente){
            return objCarrito.CantidadEnCarrito(idcliente);
        }

    }
}
