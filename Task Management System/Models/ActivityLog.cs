using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management_System.Models
{
    public class ActivityLog
    {
        private readonly string description;
        private readonly DateTime timestamp;

        // Constructor
        public ActivityLog(string description) {
            this.description = description;
            this.timestamp = DateTime.Now;
        }

        // overrided ToString() method
        public override string ToString()
        {
            return $"[{timestamp}] {description}";
        }
    }
}
