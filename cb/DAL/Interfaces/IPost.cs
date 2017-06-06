using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cb.Models;

namespace cb.DAL.Interfaces
{
    public interface IPost
    {
        void CreatePost(Post post);
        Post GetPostById(int postid);
        List<Post> GetAllPosts();
    }
}