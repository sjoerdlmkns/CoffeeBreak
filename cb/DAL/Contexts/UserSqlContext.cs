using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using cb.Dal;
using cb.DAL.Interfaces;
using cb.Enums;
using cb.Models;

namespace cb.DAL.Contexts
{
    public class UserSqlContext : IUser
    {
        public void AcceptFriendRequest(int useridone, int useridtwo)
        {
            try
            {
                using (var con = new SqlConnection(Env.ConnectionString))
                {
                    var query = "DELETE FROM Friendrequest WHERE (fromuserid = @useridone OR fromuserid = @useridtwo ) AND (touserid = @useridone OR touserid = @useridtwo); INSERT INTO Relation(friend_one, friend_two, relationcreationdate) VALUES(@useridone, @useridtwo, @creationdate)";
                    var cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@useridone", useridone);
                    cmd.Parameters.AddWithValue("@useridtwo", useridtwo);
                    cmd.Parameters.AddWithValue("@creationdate", DateTime.Now);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AuthUser(string username, string password)
        {
            var isAuth = false;
            try
            {
                using (var con = new SqlConnection(Env.ConnectionString))
                {
                    var query = "SELECT COUNT(*) FROM [User] WHERE username = @username AND [password] = @password";
                    var cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        isAuth = Convert.ToBoolean(reader.GetInt32(0));
                    }
                    con.Close();
                }
                return isAuth;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ChangeUserPasswordById(int userid, string newpassword)
        {
            try
            {
                using (var con = new SqlConnection(Env.ConnectionString))
                {
                    var query = "UPDATE [User] SET Password = @password WHERE id = @userid";
                    var cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@password", newpassword);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CreateUser(User user)
        {
            try
            {
                SqlConnection con = new SqlConnection(Env.ConnectionString);

                con.Open();
                var cmdString = "INSERT INTO [User] (username, password, ismanager, gender, image, isdeleted) VALUES (@username, @password, @ismanager, @gender, @image, @isdeleted)";
                var command = new SqlCommand(cmdString, con);
                command.Parameters.AddWithValue("@username", user.Username);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@ismanager", 0);
                command.Parameters.AddWithValue("@gender", user.Gender.ToString());
                command.Parameters.AddWithValue("@image", "testimagestring");
                command.Parameters.AddWithValue("@isdeleted", 0);

                command.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<User> GetAllUsers()
        {
            List<User> returnUserList = new List<User>();
            try
            {
                using (var con = new SqlConnection(Env.ConnectionString))
                {
                    var query = "SELECT u.id, u.username, u.ismanager, u.gender, u.image FROM [User] u";
                    var cmd = new SqlCommand(query, con);
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        User user = new User(
                            reader.GetInt32(0), //ID
                            reader.GetString(1), //Username
                            Convert.ToBoolean(reader.GetInt32(2)), //IsManager
                            (Gender)Enum.Parse(typeof(Gender), reader.GetString(3)), //Gender
                            reader.GetString(4)); //Image

                        returnUserList.Add(user);
                    }
                    con.Close();
                }
                return returnUserList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<User> GetFriendRequestsByUserId(int id)
        {
            var returnList = new List<User>();
            try
            {
                SqlConnection con = new SqlConnection(Env.ConnectionString);

                con.Open();
                var cmdString = "SELECT u.id, u.username, u.ismanager, u.gender, u.image FROM Friendrequest fr INNER JOIN[User] u ON fr.fromuserid = u.id;";
                var command = new SqlCommand(cmdString, con);
                command.Parameters.AddWithValue("@id", id);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    User user = new User(
                        reader.GetInt32(0), //ID
                        reader.GetString(1), //Username
                        Convert.ToBoolean(reader.GetInt32(2)), //IsManager
                        (Gender)Enum.Parse(typeof(Gender), reader.GetString(3)), //Gender
                        reader.GetString(4)); //Image

                    returnList.Add(user);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnList;
        }

        public List<User> GetFriendsByUserId(int id)
        {
            var returnList = new List<User>();
            try
            {
                SqlConnection con = new SqlConnection(Env.ConnectionString);

                con.Open();
                var cmdString = "SELECT  u.id, u.username, u.ismanager, u.gender, u.image FROM [User] u WHERE u.id = (SELECT CASE WHEN r.friend_one = @id THEN friend_two WHEN r.friend_two = @id THEN friend_one END AS userid FROM Relation r)";
                var command = new SqlCommand(cmdString, con);
                command.Parameters.AddWithValue("@id", id);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    User user = new User(
                            reader.GetInt32(0), //ID
                            reader.GetString(1), //Username
                            Convert.ToBoolean(reader.GetInt32(2)), //IsManager
                            (Gender)Enum.Parse(typeof(Gender), reader.GetString(3)), //Gender
                            reader.GetString(4)); //Image

                    returnList.Add(user);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnList;
        }

        public User GetUserById(int userid)
        {
            User returnUser = null;
            try
            {
                using (var con = new SqlConnection(Env.ConnectionString))
                {
                    var query = "SELECT u.id, u.username, u.ismanager, u.gender, u.image FROM [User] u where u.id = @id";
                    var cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", userid);
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                         returnUser = new User(
                             reader.GetInt32(0), //ID
                             reader.GetString(1), //Username
                             Convert.ToBoolean(reader.GetInt32(2)), //IsManager
                             (Gender)Enum.Parse(typeof(Gender), reader.GetString(3)), //Gender
                             reader.GetString(4)); //Image
                    }
                    con.Close();
                }
                return returnUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetUserIdByUsername(string username)
        {
            int returnUserId = 0;
            try
            {
                using (var con = new SqlConnection(Env.ConnectionString))
                {
                    var query = "SELECT u.id FROM [User] u where u.username = @username";
                    var cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        returnUserId = reader.GetInt32(0);
                    }
                    con.Close();
                }
                return returnUserId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SendFriendRequest(int fromuserid, int touserid)
        {
            try
            {
                SqlConnection con = new SqlConnection(Env.ConnectionString);

                con.Open();
                var cmdString = "INSERT INTO Friendrequest (fromuserid, touserid) VALUES (@fromuserid, @touserid)";
                var command = new SqlCommand(cmdString, con);
                command.Parameters.AddWithValue("@fromuserid", fromuserid);
                command.Parameters.AddWithValue("@touserid", touserid);

                command.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}