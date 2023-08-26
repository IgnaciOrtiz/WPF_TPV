using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using WPF_LoginForm.Repositories;
using WPF_TPV.Model;

namespace WPF_TPV.Repositories
{
    public class FamiliaRepository : RepositoryBase
    {
        public List<FamiliaProdModel> GetAllFamilias()
        {
            List<FamiliaProdModel> familias = new List<FamiliaProdModel>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "select idFamiliaProd, Nombre from[mnt_FamiliasProductos]";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FamiliaProdModel familia = new FamiliaProdModel
                            {
                                idFamiliaProd = reader.GetInt32(0),
                                Nombre = reader.GetString(1)
                            };
                            familias.Add(familia);
                        }
                    }
                }
            }

            return familias;
        }
    }
}

