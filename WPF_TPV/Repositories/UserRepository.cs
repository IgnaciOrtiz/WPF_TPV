using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using WPF_LoginForm.Repositories;
using WPF_TPV.Model;

namespace WPF_TPV.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            throw new System.NotImplementedException();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select*from[mnt_Usuarios] where UserName=@username and [Password]=@password";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                validUser = command.ExecuteScalar() == null ? false : true;

            }
            return validUser;
        }

        public void Edit(UserModel userModel)
        {
            throw new System.NotImplementedException();
        }
        public IEnumerable<UserModel> GetByAll()
        {
            throw new System.NotImplementedException();
        }
        public UserModel GetById(int idUsuario)
        {
            throw new System.NotImplementedException();
        }
        public UserModel GetByUsername(string UserName)
        {
            UserModel user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select*from[mnt_Usuarios] where UserName=@username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = UserName;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            idUsuario = Convert.ToInt32(reader[0]),
                            idEmpleado = Convert.ToInt32(reader[1]),
                            Password = String.Empty,
                            UserName = reader[3].ToString(),
                        };

                    }
                }
            }
            return user;
        }
        public void Remove(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
