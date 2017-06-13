using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using cb.Dal;
using cb.DAL.Interfaces;
using cb.DAL.Repositories;
using cb.Enums;
using cb.Models;

namespace cb.DAL.Contexts
{
    public class CommentSqlContext : IComment
    {
        public void AddCommentToPost(int postid, Comment comment)
        {
            try
            {
                SqlConnection con = new SqlConnection(Env.ConnectionString);

                con.Open();
                var cmdString = "INSERT INTO Comment (postid, userid ,comment, score) VALUES (@postid, @userid, @comment, 0)";
                var command = new SqlCommand(cmdString, con);
                command.Parameters.AddWithValue("@postid", postid);
                command.Parameters.AddWithValue("@userid", comment.User.Id);
                command.Parameters.AddWithValue("@comment", comment.Message);

                command.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteComment(int commentid)
        {
            try
            {
                SqlConnection con = new SqlConnection(Env.ConnectionString);

                con.Open();
                var cmdString = "DELETE FROM Comment WHERE id = @id";
                var command = new SqlCommand(cmdString, con);
                command.Parameters.AddWithValue("@id", commentid);

                command.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Comment> GetAllCommentsByPostId(int postid)
        {
            List<Comment> returnCommentList = new List<Comment>();

            try
            {
                using (var con = new SqlConnection(Env.ConnectionString))
                {
                    var query = "SELECT * FROM Comment WHERE postid =@id";
                    
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

                        Comment comment = new Comment(
                            reader.GetInt32(0), //Id
                            user,
                            reader.GetString(3), //Message
                            reader.GetInt32(4) //Score
                            );

                        returnCommentList.Add(comment);
                    }
                    con.Close();
                }
                return returnCommentList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}