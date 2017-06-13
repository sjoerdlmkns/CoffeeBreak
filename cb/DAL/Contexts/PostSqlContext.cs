using System;
using System.Collections.Generic;
using System.Data;
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

                        //Get Comments by posts
                        CommentSqlContext ccontext = new CommentSqlContext();
                        CommentRepository cp = new CommentRepository(ccontext);
                        post.Comments = cp.GetCommentsByPostId(post.Id);

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

        public List<Post> GetFreshPosts()
        {
            List<Post> returnPostList = new List<Post>();

            try
            {
                using (var con = new SqlConnection(Env.ConnectionString))
                {
                    var query = "SELECT * FROM Post where creationdate = GETDATE() ORDER BY creationdate";
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

                        //Get Comments by posts
                        CommentSqlContext ccontext = new CommentSqlContext();
                        CommentRepository cp = new CommentRepository(ccontext);

                        post.Comments = cp.GetCommentsByPostId(post.Id);

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

        public List<Post> GetHotPosts()
        {
            List<Post> returnPostList = new List<Post>();

            try
            {
                using (var con = new SqlConnection(Env.ConnectionString))
                {
                    var query = "SELECT * FROM Post order by score desc";
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

                        //Get Comments by posts
                        CommentSqlContext ccontext = new CommentSqlContext();
                        CommentRepository cp = new CommentRepository(ccontext);

                        post.Comments = cp.GetCommentsByPostId(post.Id);

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

                        //Get Comments by posts
                        CommentSqlContext ccontext = new CommentSqlContext();
                        CommentRepository cp = new CommentRepository(ccontext);

                        returnPost.Comments = cp.GetCommentsByPostId(returnPost.Id);
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

        public List<Post> GetPostsByUserId(int userid)
        {
            List<Post> returnPostList = new List<Post>();

            try
            {
                using (var con = new SqlConnection(Env.ConnectionString))
                {
                    var query = "SELECT * FROM Post WHERE userid =@id";
                    var cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", userid);
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

                        //Get Comments by posts
                        CommentSqlContext ccontext = new CommentSqlContext();
                        CommentRepository cp = new CommentRepository(ccontext);

                        post.Comments = cp.GetCommentsByPostId(post.Id);

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

        public List<Post> GetTrendingPosts(int userid)
        {
            List<Post> posts = new List<Post>();

            var friends = new DataTable();
            friends.Columns.Add("friendid", typeof(int));

            var context = new UserSqlContext();
            var ur = new UserRepository(context);

            var friendlist = new List<User>();
            friendlist = ur.GetFriendsByUserId(userid);

            foreach (var user in friendlist)
            {
                friends.Rows.Add(user.Id);
            }

            try
            {
                using (SqlConnection con = new SqlConnection(Env.ConnectionString))
                {
                    con.Open();
                    var cmd = new SqlCommand("spGetTrendingPosts", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var tvparam = cmd.Parameters.AddWithValue("@Friends", friends);
                    tvparam.SqlDbType = SqlDbType.Structured;

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        //Get User by id
                        User user = ur.GetGebruikerById(reader.GetInt32(2));


                      var  post = new Post(
                            reader.GetInt32(0), //ID
                            user, //User
                            reader.GetString(1), //Titel
                            Category.Classic, //Category
                            reader.GetDateTime(3), //Creatindate
                            reader.GetInt32(4), //Score
                            reader.GetString(6));

                        posts.Add(post);
                    }

                    con.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return posts;
        }

        public void LikePost(int postid, int userid)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Env.ConnectionString))
                {
                    con.Open();
                    var cmd = new SqlCommand("spLikePost", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@postid",postid);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}