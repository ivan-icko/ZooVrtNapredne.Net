﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class User: IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Package> Packages { get; set; } = new List<Package>();

    }
}
