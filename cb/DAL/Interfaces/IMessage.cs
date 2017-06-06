using System.Collections.Generic;
using cb.Models;

namespace cb.DAL.Interfaces
{
    public interface IMessage
    {
        void CreateMessage(Message message);
        Message GetMessageById(int messageid);
        List<Message> GetAllMessagesByUserIds(int useridone, int useridtwo);
        List<string> GetAllBadWords();
        void AddBadWord(string word);
        void RemoveBadWord(string word);
    }
}