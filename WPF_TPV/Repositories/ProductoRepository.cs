using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using WPF_LoginForm.Repositories;
using WPF_TPV.Model;

namespace WPF_TPV.Repositories
{
    public class ProductoRepository : RepositoryBase
    {
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
    }
}
