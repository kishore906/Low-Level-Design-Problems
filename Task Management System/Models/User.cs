using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management_System.Models
{
    // This class represents 'User' entity 
    public class User
    {
        private readonly string id;
        private readonly string name;
        private readonly string email;

        // Constructor
        public User(string id, string name, string email) { 
            this.id = id;
            this.name = name;
            this.email = email;
        }

        // Methods
        public string GetId() => id;
        public string GetName() => name;
        public string GetEmail() => email;
    }
}
