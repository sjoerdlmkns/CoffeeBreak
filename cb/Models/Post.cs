using System;
using System.Collections.Generic;
using cb.Enums;

namespace cb.Models
{
    public class Post
    {
        public int Id { get; private set; }
        public User User { get; set; }
        public string Titel { get; private set; }
        public string Image { get; private set; }
        public Category Category { get; private set; }
        public List<Comment> Comments { get; set; }
        public DateTime CreationDate { get; private set; }
        public int Score { get; private set; }

        public Post(int id, User user, string titel, Category category, DateTime creationdate, int score, string image)
        {
            Id = id;
            User = user;
            Titel = titel;
            Category = category;
            CreationDate = creationdate;
            Score = score;
            Image = image;
        }

        public Post(User user, string titel, Category category, string image)
        {
            User = user;
            Titel = titel;
            Category = category;
            Image = image;
        }
    }
}