using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Ubicacion
    {
        public List<Departamento> ObtenerDepartamento()
        {
            List<Departamento> lista = new List<Departamento>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    string query = "select * from departamento";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text; /*Se define el tipo de conexion a texto*/

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Departamento()
                                {
                                    IdDepartamento = dr["IdDepartamento"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Departamento>(); /*Que sea una lista basia*/
            }
            return lista;
        }


        public List<Provincia> ObtenerProvincia(string iddepartamento)
        {
            List<Provincia> lista = new List<Provincia>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    string query = "select * from PROVINCIA where IdDepartamento = @iddepartamento";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@iddepartamento", iddepartamento);
                    cmd.CommandType = CommandType.Text; /*Se define el tipo de conexion a texto*/

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Provincia()
                                {
                                    IdProvincia = dr["IdProvincia"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Provincia>(); /*Que sea una lista basia*/
            }
            return lista;
        }


        public List<Distrito> ObtenerDistrito(string iddepartamento, string idprovincia)
        {
            List<Distrito> lista = new List<Distrito>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    string query = "select * from DISTRITO where IdProvincia = @idprovincia and IdDepartamento = @iddepartamento";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@idprovincia", idprovincia);
                    cmd.Parameters.AddWithValue("@iddepartamento", iddepartamento);
                    cmd.CommandType = CommandType.Text; /*Se define el tipo de conexion a texto*/

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Distrito()
                                {
                                    IdDistrito = dr["IdDistrito"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Distrito>(); /*Que sea una lista basia*/
            }
            return lista;
        }

    }
}
