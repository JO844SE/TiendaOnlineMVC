using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
   public class CN_Usuarios
    {
        //instancia a laclase CD_Usuario de la capa de datos
        private CD_Usuarios objUsuarios = new CD_Usuarios();

        public List<Usuario> Listar()
        {
            return objUsuarios.Listar();
        }
    }
}
