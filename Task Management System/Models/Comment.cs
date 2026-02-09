using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management_System.Models
{
    public class Comment
    {
        private readonly string id;
        private readonly string content;
        private readonly User author;
        private readonly DateTime timestamp;

        // Constructor
        public Comment(string content, User author) {
            this.id = Guid.NewGuid().ToString();
            this.content = content;
            this.author = author;
            this.timestamp = DateTime.Now;
        }

        public User GetAuthor() => author;
    }
}
