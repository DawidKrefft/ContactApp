using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactApp.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string Category { get; set; }
        public string DateOfBirth { get; set; }
        public string PhotoFileName { get; set; }
        public string PhoneNumber { get; set; }

    }
}
