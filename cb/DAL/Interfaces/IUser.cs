using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cb.Models;

namespace cb.DAL.Interfaces
{
    public interface IUser
    {
        void CreateUser(User u);
        User GetUserById(int userid);
        List<User> GetAllUsers();
        void ChangeUserPasswordById(int userid, string newpassword);
        bool AuthUser(string username, string password);
        int GetUserIdByUsername(string username);
        List<User> GetFriendsByUserId(int id);
        void SendFriendRequest(int fromuserid, int touserid);
        List<User> GetFriendRequestsByUserId(int id);
        void AcceptFriendRequest(int useridone, int useridtwo);
    }
}