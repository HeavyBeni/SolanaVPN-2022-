using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using WpfApp1.Views;

namespace WpfApp1.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {

        
        
        public bool Add(NetworkCredential credential)
        {
            bool userCreated;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO User1(username, password) VALUES(@username, @password)";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                userCreated = command.ExecuteScalar() == null ? false : true;
                connection.Close();
                SolanaVpnView vpn = new SolanaVpnView();
                RegisterView reg = new RegisterView();
                reg.Close();
                vpn.Show();
            }
            return userCreated;

        }
        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [user1] where username=@username and [password]=@password";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                validUser = command.ExecuteScalar() == null ? false : true;
                connection.Close();
                SolanaVpnView vpn = new SolanaVpnView();
                LoginView login = new LoginView();
                login.Close();
                vpn.Show();

            }
            return validUser;
        }

        public bool IfTakenUsername(string username)
        {
            bool usernameTaken;
            int UserExist;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                SqlCommand check_User_Name = new SqlCommand("SELECT COUNT(*) FROM [User1] WHERE ([Username] = @username)", connection);
                check_User_Name.Parameters.AddWithValue("@username", username);
                UserExist = (int)check_User_Name.ExecuteScalar();
                connection.Close();
            }
            if (UserExist > 0)
            {
                usernameTaken = true;
            }
            else
            {
                usernameTaken = false;
            }
            return usernameTaken;

        }

        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }
        public UserModel GetById(int id)
        {
            throw new NotImplementedException();
        }
        public UserModel GetByUsername(string username)
        {
            UserModel user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [User] where username=@username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Id = reader[0].ToString(),
                            Username = reader[1].ToString(),
                            Password = string.Empty,
                            Name = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            Email = reader[5].ToString(),
                        };
                    }
                }
            }
            return user;
        }
        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}