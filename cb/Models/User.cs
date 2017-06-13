using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cb.Enums;

namespace cb.Models
{
    public class User
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool IsManager { get; private set; }
        public Gender Gender {get; private set; }
        public Address Address { get; private set; }
        public List<User> Friends { get; private set; }
        public string Image { get; private set; }

        public User(int id,string username, bool ismangager, Gender gender,string image)
        {
            Id = id;
            Username = username;
            IsManager = ismangager;
            Gender = gender;
            Image = image;
        }

        public User(string username, string password, Gender gender, string image)
        {
            Username = username;
            Password = password; 
            Gender = gender;
            Image = image;
        }

        public User(int id)
        {
            Id = id;
        }
    }
}
