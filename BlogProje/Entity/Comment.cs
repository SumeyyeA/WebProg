using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public int BlogPostId { get; set; }
       // public User User { get; set; }
        public int UserId { get; set; }
        public BlogPost BlogPost { get; set; }
    }
}
