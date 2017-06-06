using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using cb.Dal;
using cb.DAL.Interfaces;
using cb.DAL.Repositories;
using cb.Enums;
using cb.Models;

namespace cb.DAL.Contexts
{
    public class PostSqlContext : IPost
    {
        public void CreatePost(Post post)
        {
            try
            {
                SqlConnection con = new SqlConnection(Env.ConnectionString);

                con.Open();
                var cmdString = "INSERT INTO Post (titel, userid, creationdate, score, categoryid, image) VALUES (@titel, @userid, @creationdate, @score, @categoryid, @image)";
                var command = new SqlCommand(cmdString, con);
                command.Parameters.AddWithValue("@titel", post.Titel);
                command.Parameters.AddWithValue("@userid", post.User.Id);
                command.Parameters.AddWithValue("@creationdate", DateTime.Now);
                command.Parameters.AddWithValue("@score", 0);
                command.Parameters.AddWithValue("@categoryid", (int)post.Category);
                command.Parameters.AddWithValue("@image", post.Image);

                command.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }

        public List<Post> GetAllPosts()
        {
            List<Post> returnPostList = new List<Post>();

            try
            {
                using (var con = new SqlConnection(Env.ConnectionString))
                {
                    var query = "SELECT * FROM Post";
                    var cmd = new SqlCommand(query, con);
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //Get User by id
                        UserSqlContext context = new UserSqlContext();
                        UserRepository ur = new UserRepository(context);

                        User user = ur.GetGebruikerById(reader.GetInt32(2));

                        Post post = new Post(
                            reader.GetInt32(0), //ID
                            user, //User
                            reader.GetString(1), //Titel
                            Category.Classic, //Category
                            reader.GetDateTime(3), //Creatindate
                            reader.GetInt32(4), //Score
                            reader.GetString(6)); //image

                        returnPostList.Add(post);
                    }
                    con.Close();
                }
                return returnPostList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Post GetPostById(int postid)
        {
            Post returnPost = null;
            try
            {
                using (var con = new SqlConnection(Env.ConnectionString))
                {
                    var query = "SELECT * FROM Post WHERE Id = @id";
                    var cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", postid);
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //Get User by id
                        UserSqlContext context = new UserSqlContext();
                        UserRepository ur = new UserRepository(context);

                        User user = ur.GetGebruikerById(reader.GetInt32(2));


                        returnPost = new Post(
                            reader.GetInt32(0), //ID
                            user, //User
                            reader.GetString(1), //Titel
                            Category.Classic, //Category
                            reader.GetDateTime(3), //Creatindate
                            reader.GetInt32(4), //Score
                            reader.GetString(6));
                    }
                    con.Close();
                }
                return returnPost;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}