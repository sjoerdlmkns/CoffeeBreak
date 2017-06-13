using System.Collections.Generic;
using cb.DAL.Interfaces;
using cb.Models;

namespace cb.DAL.Repositories
{
    public class CommentRepository
    {
        private readonly IComment _context;

        public CommentRepository(IComment context)
        {
            _context = context;
        }

        public void AddCommentToPost(int postid, Comment comment)
        {
            _context.AddCommentToPost(postid, comment);
        }

        public List<Comment> GetCommentsByPostId(int postid)
        {
            return _context.GetAllCommentsByPostId(postid);
        }

        public void DeleteComment(int commentid)
        {
            _context.DeleteComment(commentid);
        }
    }
}