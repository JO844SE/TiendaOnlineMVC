using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Categoria
    {
        //instancia a laclase CD_Usuario de la capa de datos
        private CD_categoria objCategooria = new CD_categoria();
        public List<Categoria> Listar()
        {
            return objCategooria.Listar();
        }




        public int Registrar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripcion de la categoría no puede ser vacio";
            }
            

            if (string.IsNullOrEmpty(Mensaje))
            {
                    return objCategooria.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
        }




        public bool Editar(Categoria obj, out String Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripcion de la categoría no puede ser vacio";
            }


            if (string.IsNullOrEmpty(Mensaje))
            {

                return objCategooria.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }


        public bool Eliminar(int id, out string Mensaje)
        {
            return objCategooria.Eliminar(id, out Mensaje);
        }

    }
}
