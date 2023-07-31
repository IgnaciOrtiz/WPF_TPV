using System.Collections.Generic;
using System.Net;

namespace WPF_TPV.Model
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        void Add(UserModel userModel);
        void Edit(UserModel userModel);
        void Remove(int id);
        UserModel GetById(int idUsuario);
        UserModel GetByUsername(string UserName);
        IEnumerable<UserModel> GetByAll();
    }
}
