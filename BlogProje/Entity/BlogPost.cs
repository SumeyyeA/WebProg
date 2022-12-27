using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class BlogPost
    {
        public int BlogPostId { get; set; }

#nullable enable
        public string? UserName{get;set;}
#nullable disable
#nullable enable
        public string? Content { get; set; }
        public string? Title { get; set; }
        public DateTime? PostDate { get; set; }
        public int? LikeNumber { get; set; }
        public int? DislikeNumber { get; set; }
        public string? UserId { get; set; }
       // public  User? User { get; set; }
        public int? CategoryId { get; set; }

        public Category? Category { get; set; }
#nullable disable
    }
}
