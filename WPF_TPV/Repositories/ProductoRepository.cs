using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WPF_LoginForm.Repositories;
using WPF_TPV.Model;

namespace WPF_TPV.Repositories
{
    public class ProductoRepository : RepositoryBase
    {
        //Recupera productos para botones de CajaWindow
        public List<ProductoModel> GetProductosByFamilia(int idFamilia)
        {
            List<ProductoModel> productos = new List<ProductoModel>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "SELECT idProducto, Nombre FROM [mnt_Productos] WHERE idFamiliaProd = @idFamilia";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idFamilia", idFamilia);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductoModel producto = new ProductoModel
                            {
                                idProducto = reader.GetInt32(0),
                                Nombre = reader.GetString(1)
                            };
                            productos.Add(producto);
                        }
                    }
                }
            }

            return productos;
        }

        public decimal GetPrecioProducto(int idProducto)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT Precio FROM [mnt_Productos] WHERE idProducto = @idProducto", connection))
                {
                    command.Parameters.AddWithValue("@idProducto", idProducto);

                    var result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToDecimal(result);
                    }

                    return 0;
                }
            }
        }

        //Recupera productos para ProductosWindow
        public ObservableCollection<ProductoModel> GetProductosGrid()
        {
            ObservableCollection<ProductoModel> productosGrid = new ObservableCollection<ProductoModel>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM [mnt_Productos]";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var productoGrid = new ProductoModel
                            {
                                idProducto = Convert.ToInt32(reader["idProducto"]),
                                Nombre = reader["Nombre"].ToString(),
                                Precio = Convert.ToDecimal(reader["Precio"]),
                                Descripcion = reader["Descripcion"].ToString(),
                                TipoIVA = Convert.ToInt32(reader["TipoIVA"]),
                                idFamiliaProd = Convert.ToInt32(reader["idFamiliaProd"]),
                                idProveedor = Convert.ToInt32(reader["idProveedor"]),
                                Stock = Convert.ToInt32(reader["Stock"])
                            };

                            productosGrid.Add(productoGrid);
                        }
                    }
                }
            }

            return productosGrid;
        }

        public void AgregarProducto(ProductoModel producto)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "INSERT INTO [mnt.Productos] (Nombre, Precio, Descripcion, TipoIVA, idFamiliaProd, idProveedor, Stock) " +
                    "VALUES (@Nombre, @Precio, @Descripcion, @TipoIVA, @idFamiliaProd, @idProveedor, @Stock)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    command.Parameters.AddWithValue("@Precio", producto.Precio);
                    command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                    command.Parameters.AddWithValue("@TipoIVA", producto.TipoIVA);
                    command.Parameters.AddWithValue("@idFamiliaProd", producto.idFamiliaProd);
                    command.Parameters.AddWithValue("@idProveedor", producto.idProveedor);
                    command.Parameters.AddWithValue("@Stock", producto.Stock);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void EliminarProducto(int idProducto)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "DELETE FROM [mnt_Productos] WHERE idProducto = @idProducto";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idProducto", idProducto);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void EditarProducto(ProductoModel producto)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "UPDATE [mnt_Productos] SET Nombre = @Nombre, Precio = @Precio, " +
                    "Descripcion = @Descripcion, TipoIVA = @TipoIVA, idFamiliaProd = @idFamiliaProd, " +
                    "idProveedor = @idProveedor, Stock = @Stock WHERE idProducto = @idProdcuto";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idProducto", producto.idProducto);
                    command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    command.Parameters.AddWithValue("@Precio", producto.Precio);
                    command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                    command.Parameters.AddWithValue("@TipoIVA", producto.TipoIVA);
                    command.Parameters.AddWithValue("@idFamiliaProd", producto.idFamiliaProd);
                    command.Parameters.AddWithValue("@idProveedor", producto.idProveedor);
                    command.Parameters.AddWithValue("@Stock", producto.Stock);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
