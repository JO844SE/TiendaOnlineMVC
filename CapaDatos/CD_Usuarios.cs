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
    public class CD_Usuarios
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    string query = "select IdUsuario, Nombres, Apellidos, Correo, Clave, Reestablecer, Activo from USUARIO";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text; /*Se define el tipo de conexion a texto*/

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Usuario()
                                {
                                    IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                    Nombres = dr["Nombres"].ToString(),
                                    Apellidos = dr["Apellidos"].ToString(),
                                    Correo = dr["Correo"].ToString(),
                                    Clave = dr["Clave"].ToString(),
                                    Reestablecer = Convert.ToBoolean(dr["Reestablecer"]),
                                    Activo = Convert.ToBoolean(dr["Activo"])
                                }
                                );
                        }
                    }
                }
            }
            catch 
            {
                lista = new List<Usuario>(); /*Que sea una lista basia*/
            }
            return lista;
        }


        //procedimiento almacenado para registrar usuarios 
        public int Registrar(Usuario obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;//Mensaje vacio

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_RegistrarUsuario", oconexion); //Ejecuta el procedimiento almacenado
                    cmd.Parameters.AddWithValue("Nombres", obj.Nombres);//Paramétros de entrada
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
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
        public bool Editar(Usuario obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;//Mensaje vacio

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_EditarUsuario", oconexion); //Ejecuta el procedimiento almacenado
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);//Paramétros de entrada
                    cmd.Parameters.AddWithValue("Nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
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


        //procedimiento almacenado para Eliminar usuarios
        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;//Mensaje vacio

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("delete top(1) from USUARIO where IdUsuario = @id", oconexion); //Ejecuta el procedimiento almacenado
                    cmd.Parameters.AddWithValue("id", id);//Paramétros de entrada
                    //Paramétros de salida 
                    cmd.CommandType = CommandType.Text;//tipo de comando, texto
                    oconexion.Open();//Abrimos la conexion
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;//Ejecutamos el comando

                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }


        public bool CambiarClave(int idusuario, string nuevaclave, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;//Mensaje vacio

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("update usuario set clave = @nuevaclave, reestablecer = 0 where idusuario = @id", oconexion); //Ejecuta el procedimiento almacenado
                    cmd.Parameters.AddWithValue("@id", idusuario);//Paramétros de entrada
                    cmd.Parameters.AddWithValue("@nuevaclave", nuevaclave);//Paramétros de entrada
                    //Paramétros de salida 
                    cmd.CommandType = CommandType.Text;//tipo de comando, texto
                    oconexion.Open();//Abrimos la conexion
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;//Ejecutamos el comando

                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }


        public bool ReestablecerClave(int idusuario, string clave, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;//Mensaje vacio

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("update usuario set clave = @clave, reestablecer = 1 where idusuario = @id", oconexion); //Ejecuta el procedimiento almacenado
                    cmd.Parameters.AddWithValue("@id", idusuario);//Paramétros de entrada
                    cmd.Parameters.AddWithValue("@clave", clave);//Paramétros de entrada
                    //Paramétros de salida 
                    cmd.CommandType = CommandType.Text;//tipo de comando, texto
                    oconexion.Open();//Abrimos la conexion
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;//Ejecutamos el comando

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
