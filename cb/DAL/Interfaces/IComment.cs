using System.Collections.Generic;
using cb.Models;

namespace cb.DAL.Interfaces
{
    public interface IComment
    {
        void AddCommentToPost(int postid, Comment comment);
        List<Comment> GetAllCommentsByPostId(int postid);
        void DeleteComment(int commentid);
    }
}