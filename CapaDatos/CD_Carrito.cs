using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;
using System.Globalization;

namespace CapaDatos
{
    public class CD_Carrito
    {

        //CultureInfo guatemalaCulture = new CultureInfo("es-GT");

        //procedimiento almacenado para registrar usuarios 
        public bool ExisteCarrito(int idcliente, int idproducto)
        {
            bool resultado = true;
     

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_ExisteCarrito", oconexion); //Ejecuta el procedimiento almacenado
                    cmd.Parameters.AddWithValue("IdCliente", idcliente);//Paramétros de entrada
                    cmd.Parameters.AddWithValue("IdProducto", idproducto);
                    //Paramétros de salida 
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;//tipo de comando, procedimiento almacenado



                    oconexion.Open();//Abrimos la conexion

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);


                }

            }
            catch (Exception ex)
            {
                resultado = false;

            }

            return resultado;
        }


        //obtener correlativo de venta 
        public int ObtenerCorrelativo()
        {
            int idcorrelativo = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {

                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count(*) + 1 from VENTA");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    idcorrelativo = Convert.ToInt32(cmd.ExecuteScalar());

                }
                catch (Exception ex)
                {
                    idcorrelativo = 0;
                }
            }
            return idcorrelativo;
        }




        //procedimiento almacenado para registrar usuarios 
        public bool OperacionCarrito(int idcliente, int idproducto, bool sumar, out string Mensaje)
        {
            bool resultado = true;
            Mensaje = string.Empty;//Mensaje vacio

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_OperacionCarrito", oconexion); //Ejecuta el procedimiento almacenado
                    cmd.Parameters.AddWithValue("IdCliente", idcliente);//Paramétros de entrada
                    cmd.Parameters.AddWithValue("IdProducto", idproducto);
                    cmd.Parameters.AddWithValue("Sumar", sumar);
                    //Paramétros de salida 
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
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



        public int CantidadEnCarrito(int idcliente)
        {
            int resultado = 0;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("select count(*) from carrito where idcliente = @idcliente", oconexion); //Ejecuta el procedimiento almacenado
                    cmd.Parameters.AddWithValue("@idcliente", idcliente);//Paramétros de entrada
                    //Paramétros de salida 
                    cmd.CommandType = CommandType.Text;//tipo de comando, texto
                    oconexion.Open();//Abrimos la conexion
                    resultado = Convert.ToInt32(cmd.ExecuteScalar());//Ejecutamos el comando
                    resultado = Convert.ToInt32(cmd.ExecuteScalar());//Ejecutamos el comando

                }
            }
            catch (Exception ex)
            {
                resultado = 0;
            }
            return resultado;
        }



        //Sumar stock de carrito
       



        public List<Carrito> ListarProducto(int idcliente)
        {
            List<Carrito> lista = new List<Carrito>();
            try
            {

                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    string query = "select * from fn_obtenerCarritoCliente(@idcliente)";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@idcliente", idcliente);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Carrito()
                            {


                                oProducto = new Producto()
                                {
                                    IdProducto = Convert.ToInt32(dr["IdProducto"].ToString()),
                                    Nombre = dr["Nombre"].ToString(),
                                    Precio = Convert.ToDecimal(dr["Precio"].ToString(), new CultureInfo("es-GT")),
                                    RutaImagen = dr["RutaImagen"].ToString(),
                                    NombreImagen = dr["NombreImagen"].ToString(),
                                    oMarca = new Marca() { Descripcion = dr["DesMarca"].ToString() }
                                   

                                 },

                                Cantidad = Convert.ToInt32(dr["Cantidad"].ToString())

                            });
                        }
                    }
                }
            }
            catch
            {

                lista = new List<Carrito>();
            }
            return lista;
            
        }


        public bool EliminarCarrito(int idcliente, int idproducto)
        {
            bool resultado = true;


            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_EliminarCarrito", oconexion); //Ejecuta el procedimiento almacenado
                    cmd.Parameters.AddWithValue("IdCliente", idcliente);//Paramétros de entrada
                    cmd.Parameters.AddWithValue("IdProducto", idproducto);
                    //Paramétros de salida 
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;//tipo de comando, procedimiento almacenado



                    oconexion.Open();//Abrimos la conexion

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);


                }

            }
            catch (Exception ex)
            {
                resultado = false;

            }

            return resultado;
        }

    }
}
