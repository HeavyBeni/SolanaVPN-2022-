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

        
        
        // Creating a User, sql connection string is located at ReposityBase
        public bool Add(NetworkCredential credential)
        {
            // Needs to return a value
            bool userCreated;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                
                // Opening connection and inserting credentials
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO User1(username, password) VALUES(@username, @password)";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                
                // Executing insertion, and making our boolean variable true or false
                userCreated = command.ExecuteScalar() == null ? false : true;
                connection.Close();
                
                // Display
                LoginView login = new LoginView();
                RegisterView reg = new RegisterView();
                reg.Close();
                login.Show();
            }
            return userCreated;

        }
        
        // Authenticating User
        public bool AuthenticateUser(NetworkCredential credential)
        {
            // Needs to return a value
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                
                // Opening connection and checking for credential in database
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [user1] where username=@username and [password]=@password";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                
                // Executing Validation, and making our boolean variable true or false
                validUser = command.ExecuteScalar() == null ? false : true;
                connection.Close();

                // Display
                //SolanaVpnView vpn = new SolanaVpnView();
                //LoginView login = new LoginView();
                //login.Close();
                //vpn.Show();

            }
            return validUser;
        }

        // Checking for taken Username before allowing insertion
        public bool IfTakenUsername(string username)
        {
            // Needs to return a value
            bool usernameTaken;
            int UserExist;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                
                // Opening connection and checking for already existing username
                connection.Open();
                SqlCommand check_User_Name = new SqlCommand("SELECT COUNT(*) FROM [User1] WHERE ([Username] = @username)", connection);
                check_User_Name.Parameters.AddWithValue("@username", username);
                
                // Giving a value to UserExist if it founds a match
                UserExist = (int)check_User_Name.ExecuteScalar();
                connection.Close();
            }
            
            // If UserExist found a match, make usernameTaken true
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