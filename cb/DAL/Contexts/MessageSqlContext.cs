using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using cb.Dal;
using cb.DAL.Interfaces;
using cb.Enums;
using cb.Models;

namespace cb.DAL.Contexts
{
    public class MessageSqlContext : IMessage
    {
        public void AddBadWord(string word)
        {
            try
            {
                SqlConnection con = new SqlConnection(Env.ConnectionString);

                con.Open();
                var cmdString = "INSERT INTO Badword (word) VALUES (@word)";
                var command = new SqlCommand(cmdString, con);
                command.Parameters.AddWithValue("@word", word);

                command.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CreateMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllBadWords()
        {
            List<string> returnBadWordList = new List<string>();
            try
            {
                using (var con = new SqlConnection(Env.ConnectionString))
                {
                    var query = "SELECT word FROM Badword";
                    var cmd = new SqlCommand(query, con);
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        returnBadWordList.Add(reader.GetString(0));
                    }
                    con.Close();
                }
                return returnBadWordList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Message> GetAllMessagesByUserIds(int useridone, int useridtwo)
        {
            throw new NotImplementedException();
        }

        public Message GetMessageById(int messageid)
        {
            throw new NotImplementedException();
        }

        public void RemoveBadWord(string word)
        {
            try
            {
                SqlConnection con = new SqlConnection(Env.ConnectionString);

                con.Open();
                var cmdString = "DELETE FROM BadWord WHERE word = @word";
                var command = new SqlCommand(cmdString, con);
                command.Parameters.AddWithValue("@word", word);

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