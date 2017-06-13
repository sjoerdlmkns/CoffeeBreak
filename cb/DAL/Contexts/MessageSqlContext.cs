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
            try
            {
                SqlConnection con = new SqlConnection(Env.ConnectionString);

                con.Open();
                var cmdString = "INSERT INTO [Message] (senderid, recieverid, [message]) VALUES (@senderid, @recieverid, @message);";
                var command = new SqlCommand(cmdString, con);
                command.Parameters.AddWithValue("@senderid", message.Sender.Id);
                command.Parameters.AddWithValue("@recieverid", message.Receiver.Id);
                command.Parameters.AddWithValue("@message", message.Text);

                command.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            List<Message> returnMessageList = new List<Message>();
            try
            {
                UserSqlContext ucontext = new UserSqlContext();
                UserRepository ur = new UserRepository(ucontext);

                using (var con = new SqlConnection(Env.ConnectionString))
                {
                    var query = "SELECT * FROM [Message] WHERE senderid = @useridone OR senderid = @useridtwo AND recieverid = @useridone OR recieverid = @useridtwo";
                    var cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@useridone", useridone);
                    cmd.Parameters.AddWithValue("@useridtwo", useridtwo);
                    con.Open();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        User sender = ur.GetGebruikerById(reader.GetInt32(1));
                        User reciever = ur.GetGebruikerById(reader.GetInt32(2));

                        Message message = new Message(
                            reader.GetInt32(0), //Id
                            sender,
                            reciever,
                            reader.GetString(3) // Message
                           );

                        returnMessageList.Add(message);
                    }
                    con.Close();
                }
                return returnMessageList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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