using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Unidad
    {
        //instancia a laclase CD_Usuario de la capa de datos
        private CD_Unidad objUnidad = new CD_Unidad();
        public List<Unidad> Listar()
        {
            return objUnidad.Listar();
        }




        public int Registrar(Unidad obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripcion de la unidad de medida no puede ser vacio";
            }


            if (string.IsNullOrEmpty(Mensaje))
            {
                return objUnidad.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
        }




        public bool Editar(Unidad obj, out String Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripcion de la nidad de medida no puede ser vacio";
            }


            if (string.IsNullOrEmpty(Mensaje))
            {

                return objUnidad.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }


        public bool Eliminar(int id, out string Mensaje)
        {
            return objUnidad.Eliminar(id, out Mensaje);
        }
    }
}
