using System;
using System.IO;
using System.Text.RegularExpressions;

namespace cb.Models
{
    public class Message
    {
        public int Id { get; private set; }
        public User Sender { get; private set; }
        public User Receiver { get; private set; }
        public string Text { get; private set; }

        public Message(int id, User sender,User receiver, string text)
        {
            Id = id;
            Sender = sender;
            Receiver = receiver;
            Text = text;
        }

        public static string FilterBadWords(string s)
        {
            string message = s;

            try
            {
                string[] badwords = File
                    .ReadAllText(
                        @"C:\Users\Sjoerd\Documents\Visual Studio 2015\Projects\CoffeeBreak\CoffeeBreak\DAL\bad_words.csv")
                    .Split(',');

                foreach (string word in badwords)
                {
                    message = Regex.Replace(message, word, "***");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return message;
        }
    }
}