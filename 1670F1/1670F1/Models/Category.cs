using System.Collections.Generic;

namespace _1670F1.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public ICollection<JobPost> JobPosts { get; set; }
    }
}