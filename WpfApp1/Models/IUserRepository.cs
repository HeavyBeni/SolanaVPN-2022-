using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public interface IUserRepository
    {
        
        bool AuthenticateUser(NetworkCredential credential);
        bool IfTakenUsername(string username);
        bool Add(NetworkCredential credential);
        void Edit(UserModel userModel);
        void Remove(int id);
        UserModel GetById(int id);
        IEnumerable<UserModel> GetByAll();
        //...
    }
}