using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Unidad
    {
        public List<Unidad> Listar()
        {
            List<Unidad> lista = new List<Unidad>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    string query = "select IdUnidad, Descripcion, Activo from UNIDAD";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text; /*Se define el tipo de conexion a texto*/

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Unidad()
                                {
                                    IdUnidad = Convert.ToInt32(dr["IdUnidad"]),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    Activo = Convert.ToBoolean(dr["Activo"])
                                }
                                );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Unidad>(); /*Que sea una lista bacia*/
            }
            return lista;
        }



        //procedimiento almacenado para registrar usuarios 
        public int Registrar(Unidad obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;//Mensaje vacio

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_RegistrarUnidad", oconexion); //Ejecuta el procedimiento almacenado
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);//Paramétros de entrada
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    //Paramétros de salida 
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;//tipo de comando, procedimiento almacenado



                    oconexion.Open();//Abrimos la conexion

                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }

            return idautogenerado;
        }



        //procedimiento almacenado para editar usuarios
        public bool Editar(Unidad obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;//Mensaje vacio

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_EditarUnidad", oconexion); //Ejecuta el procedimiento almacenado
                    cmd.Parameters.AddWithValue("IdUnidad", obj.IdUnidad);//Paramétros de entrada
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    //Paramétros de salida 
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;//tipo de comando, procedimiento almacenado

                    oconexion.Open();//Abrimos la conexion
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }


        //procedimiento almacenado para editar usuarios
        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;//Mensaje vacio

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_EliminarUnidad", oconexion); //Ejecuta el procedimiento almacenado
                    cmd.Parameters.AddWithValue("IdUnidad", id);//Paramétros de entrada
                    //Paramétros de salida 
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;//tipo de comando, procedimiento almacenado

                    oconexion.Open();//Abrimos la conexion
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
