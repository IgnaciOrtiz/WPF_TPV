using Microsoft.Data.SqlClient;
using System;
using System.Collections.ObjectModel;
using WPF_LoginForm.Repositories;
using WPF_TPV.Model;

namespace WPF_TPV.Repositories
{
    public class ClienteRepository : RepositoryBase
    {
        public void AgregarCliente(ClienteModel cliente)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "INSERT INTO [mnt_Clientes] (Nombre, Apellidos, Direccion, Telefono, Email, DNI) " +
                               "VALUES (@Nombre, @Apellidos, @Direccion, @Telefono, @Email, @DNI)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    command.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                    command.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                    command.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    command.Parameters.AddWithValue("@Email", cliente.Email);
                    command.Parameters.AddWithValue("@DNI", cliente.DNI);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void EliminarCliente(int idCliente)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "DELETE FROM [mnt_Clientes] WHERE IdCliente = @IdCliente";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCliente", idCliente);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void EditarCliente(ClienteModel cliente)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "UPDATE [mnt_Clientes] SET Nombre = @Nombre, Apellidos = @Apellidos, " +
                               "Direccion = @Direccion, Telefono = @Telefono, Email = @Email, DNI = @DNI " +
                               "WHERE IdCliente = @IdCliente";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);
                    command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    command.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                    command.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                    command.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    command.Parameters.AddWithValue("@Email", cliente.Email);
                    command.Parameters.AddWithValue("@DNI", cliente.DNI);

                    command.ExecuteNonQuery();
                }
            }
        }

        public ObservableCollection<ClienteModel> GetAllClientes()
        {
            ObservableCollection<ClienteModel> clientes = new ObservableCollection<ClienteModel>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM [mnt_Clientes]";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cliente = new ClienteModel
                            {
                                IdCliente = Convert.ToInt32(reader["IdCliente"]),
                                Nombre = reader["Nombre"].ToString(),
                                Apellidos = reader["Apellidos"].ToString(),
                                Direccion = reader["Direccion"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                Email = reader["Email"].ToString(),
                                DNI = reader["DNI"].ToString()
                            };

                            clientes.Add(cliente);
                        }
                    }
                }
            }

            return clientes;
        }
    }
}