using System;
using System.Collections.Generic;
using cb.DAL.Interfaces;
using cb.Models;

namespace cb.DAL.Repositories
{
    public class UserRepository
    {
        private readonly IUser _context;

        public UserRepository(IUser context)
        {
            _context = context;
        }

        public List<User> GetAllUsers()
        {
            return _context.GetAllUsers();
        }

        public User GetGebruikerById(int id)
        {
            return _context.GetUserById(id);
        }

        public void CreateUser(User user)
        {
            _context.CreateUser(user);
        }

        public bool AuthUser(string username, string password)
        {
            return _context.AuthUser(username, password);
        }

        public int GetUserIdByUsername(string username)
        {
            return _context.GetUserIdByUsername(username);
        }

        public List<User> GetFriendsByUserId(int id)
        {
            return _context.GetFriendsByUserId(id);
        }

        public void ChangeUserPasswordById(int userid, string newpassword)
        {
            _context.ChangeUserPasswordById(userid, newpassword);
        }

        public void SendFriendRequest(int fromuserid, int touserid)
        {
            _context.SendFriendRequest(fromuserid, touserid);
        }

        public List<User> GetFriendRequestsByUserId(int id)
        {
            return _context.GetFriendRequestsByUserId(id);
        }

        public void AcceptFriendRequest(int useridone, int useridtwo)
        {
            _context.AcceptFriendRequest(useridone,useridtwo);
        }
    }
}