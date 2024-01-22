using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Producto
    {

        #region Listar Comentado
        //public List<Producto> Listar()
        //{
        //    Variable lista del tipo List Producto
        //    List<Producto> lista = new List<Producto>();

        //    using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
        //    {

        //        try
        //        {

        //            StringBuilder query = new StringBuilder();
        //            query.AppendLine("select p.IdProducto, p.Nombre, p.Descripcion, m.IdProducto, m.Descripcion[DesProducto],c.IdCategoria, c.Descripcion[DesCategoria],u.IdUnidad, u.Descripcion[DesUnidad],p.Precio, p.Stock,p.RutaImagen,p.NombreImagen,p.Activo from PRODUCTO p");
        //            query.AppendLine("inner join Producto m on m.IdProducto = p.IdProducto");
        //            query.AppendLine("inner join CATEGORIA c on c.IdCategoria = p.IdCategoria");
        //            query.AppendLine("inner join UNIDAD u on u.IdUnidad = p.IdUnidad");



        //            SqlCommand se encarga de ejecutar los comandos
        //            SqlCommand cmd = new SqlCommand(query.ToString(), oconexion) //Convierto el query en una cadena de texto
        //            {
        //                El tipo de comando que se ejecuta es texto
        //                CommandType = CommandType.Text
        //            };
        //            oconexion.Open();//Abrimos la conexion


        //            using (SqlDataReader dr = cmd.ExecuteReader())
        //            {
        //                while (dr.Read())
        //                {
        //                    lista.Add(
        //                        new Producto()
        //                        {
        //                            IdProducto = Convert.ToInt32(dr["IdProducto"]),
        //                            Nombre = dr["Nombre"].ToString(),
        //                            Descripcion = dr["Descripcion"].ToString(),
        //                            oMarca = new Marca() { IdMarca = Convert.ToInt32(dr["IdMarca"]), Descripcion = dr["DesMarca"].ToString() },
        //                            oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Descripcion = dr["DesCategoria"].ToString() },
        //                            oUnidad = new Unidad() { IdUnidad = Convert.ToInt32(dr["IdUnidad"]), Descripcion = dr["DesUnidad"].ToString() },
        //                            Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-GT")),
        //                            Stock = Convert.ToInt32(dr["Stock"].ToString()),
        //                            RutaImagen = dr["RutaImagen"].ToString(),
        //                            NombreImagen = dr["NombreImagen"].ToString(),
        //                            Activo = Convert.ToBoolean(dr["Activo"])
        //                        }
        //                        );
        //                }
        //            }

        //        }
        //        catch (Exception)
        //        {

        //            lista = new List<Producto>(); //Si hay error,que la lista sea vacia
        //        }
        //    }

        //    return lista;
        //}
        #endregion

        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_ListarProducto", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                
                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        lista.Add(new Producto()
                        {
                            IdProducto = Convert.ToInt32(dr["IdProducto"].ToString()),
                            Nombre = dr["Nombre"].ToString(),
                            Descripcion = dr["Descripcion"].ToString(),
                            oMarca = new Marca() { IdMarca = Convert.ToInt32(dr["IdMarca"].ToString()), Descripcion = dr["DesMarca"].ToString() },
                            oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"].ToString()), Descripcion = dr["DesCategoria"].ToString() },
                            oUnidad = new Unidad() { IdUnidad = Convert.ToInt32(dr["IdUnidad"].ToString()), Descripcion = dr["DesUnidad"].ToString() },
                            Precio = Convert.ToDecimal(dr ["Precio"].ToString(), new CultureInfo("es-GT")),
                            Stock = Convert.ToInt32(dr["Stock"].ToString()),
                            RutaImagen = dr["RutaImagen"].ToString(),
                            NombreImagen = dr["NombreImagen"].ToString(),
                            Activo = Convert.ToBoolean(dr["Activo"].ToString())
                        });
                    }
                    dr.Close();

                    return lista;

                }
                catch (Exception ex)
                {
                    lista = null;
                    return lista;
                }
            }
        }



        //procedimiento almacenado para registrar usuarios 
        public int Registrar(Producto obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;//Mensaje vacio

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_RegistrarProducto", oconexion); //Ejecuta el procedimiento almacenado
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);//Paramétros de entrada
                    cmd.Parameters.AddWithValue("IdMarca", obj.oMarca.IdMarca);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("IdUnidad", obj.oUnidad.IdUnidad);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
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
        public bool Editar(Producto obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;//Mensaje vacio

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("sp_EditarProducto", oconexion); //Ejecuta el procedimiento almacenado
                    cmd.Parameters.AddWithValue("IdProducto", obj.IdProducto);//Paramétros de entrada
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("IdMarca", obj.oMarca.IdMarca);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("IdUnidad", obj.oUnidad.IdUnidad);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
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


        public bool GuardarDatosImagen(Producto obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    string query = "update PRODUCTO set RutaImagen = @rutaimagen, NombreImagen = @nombreimagen where IdProducto = @idproducto ";
                    SqlCommand cmd = new SqlCommand(query, oconexion); //Ejecuta el procedimiento almacenado
                    cmd.Parameters.AddWithValue("@rutaimagen", obj.RutaImagen);
                    cmd.Parameters.AddWithValue("@nombreimagen", obj.NombreImagen);
                    cmd.Parameters.AddWithValue("@idproducto", obj.IdProducto);
                    //Paramétros de salida
                    cmd.CommandType = CommandType.Text;//tipo de comando, procedimiento almacenado
                    oconexion.Open();//Abrimos la conexion

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        Mensaje = "No se pudo actualizar la imagen";
                    }
                } 
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }



        //procedimiento almacenado para editar producto
        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;//Mensaje vacio

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_EliminarProducto", oconexion); //Ejecuta el procedimiento almacenado
                    cmd.Parameters.AddWithValue("IdProducto", id);//Paramétros de entrada
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
