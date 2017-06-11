using System.Collections.Generic;
using System.Security.Policy;
using cb.DAL.Interfaces;
using cb.Models;

namespace cb.DAL.Repositories
{
    public class PostRepository
    {
        private readonly IPost _context;

        public PostRepository(IPost context)
        {
            _context = context;
        }

        public void CreatePost(Post post)
        {
            _context.CreatePost(post);
        }

        public Post GetPostById(int postid)
        {
          return  _context.GetPostById(postid);
        }

        public List<Post> GetAllPost()
        {
            return _context.GetAllPosts();
        }

        public List<Post> GetTrendingPosts(int userid)
        {
            return _context.GetTrendingPosts(userid);
        }

        public List<Post> GetPostsByUserId(int userid)
        {
            return _context.GetPostsByUserId(userid);
        }
    }
}