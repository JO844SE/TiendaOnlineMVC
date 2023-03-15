using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Marca
    {
        //instancia a laclase CD_Usuario de la capa de datos
        private CD_Marca objMarca = new CD_Marca();
        public List<Marca> Listar()
        {
            return objMarca.Listar();
        }




        public int Registrar(Marca obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripcion de la marca no puede ser vacio";
            }


            if (string.IsNullOrEmpty(Mensaje))
            {
                return objMarca.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
        }




        public bool Editar(Marca obj, out String Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripcion de la marca no puede ser vacio";
            }


            if (string.IsNullOrEmpty(Mensaje))
            {

                return objMarca.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }


        public bool Eliminar(int id, out string Mensaje)
        {
            return objMarca.Eliminar(id, out Mensaje);
        }
    }
}
