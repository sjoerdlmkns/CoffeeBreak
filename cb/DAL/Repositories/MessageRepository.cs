using System.Collections.Generic;
using cb.DAL.Interfaces;
using cb.Models;

namespace cb.DAL.Repositories
{
    public class MessageRepository
    {
        private readonly IMessage _context;

        public MessageRepository(IMessage context)
        {
            _context = context;
        }

        public List<string> GetAllBadWords()
        {
            return _context.GetAllBadWords();
        }

        public void AddBadWord(string word)
        {
            _context.AddBadWord(word);
        }

        public void RemoveBadWord(string word)
        {
            _context.RemoveBadWord(word);
        }

    }
}