using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management_System.Models
{
    public class Tag
    {
        private readonly string name;

        // Constructor
        public Tag(string name) {
            this.name = name;
        }

        public string GetName() => name;
    }
}
