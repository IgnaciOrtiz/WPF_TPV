using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WPF_LoginForm.Repositories;
using WPF_TPV.Model;

namespace WPF_TPV.Repositories
{
    public class FamiliaRepository : RepositoryBase
    {
        //Recupera familias para botones de CajaWindow
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

        //Recupera familias para el grid de FamiliasWindow
        public ObservableCollection<FamiliaProdModel> GetFamiliasGrid()
        {
            ObservableCollection<FamiliaProdModel> familiasGrid = new ObservableCollection<FamiliaProdModel>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM [mnt_FamiliasProductos]";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var familiaGrid = new FamiliaProdModel
                            {
                                idFamiliaProd = Convert.ToInt32(reader["idFamiliaProd"]),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString()
                            };

                            familiasGrid.Add(familiaGrid);
                        }
                    }
                }
            }

            return familiasGrid;
        }

        public void AgregarFamilia(FamiliaProdModel familia)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "INSERT INTO [mnt_FamiliasProductos] (Nombre, Descripcion) " +
                    "VALUES (@Nombre, @Descripcion)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", familia.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", familia.Descripcion);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void EliminarFamilia(int idFamiliaProd)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "DELETE FROM [mnt_FamiliasProductos] WHERE idFamiliaProd = @IdFamilia";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdFamilia", idFamiliaProd);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void EditarFamilia(FamiliaProdModel familia)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "UPDATE [mnt_FamiliasProductos] SET Nombre = @Nombre, Descripcion = @Descripcion " +
                               "WHERE idFamiliaProd = @IdFamilia";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdFamilia", familia.idFamiliaProd);
                    command.Parameters.AddWithValue("@Nombre", familia.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", familia.Descripcion);
                }
            }
        }
    }
}
