﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MVC_assignment.Models
{
    public class Contact
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
