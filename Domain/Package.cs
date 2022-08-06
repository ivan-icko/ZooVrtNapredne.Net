﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Package
    {
        public int PackageId { get; set; }
        public string Name { get; set; }
        public double DurationInHours { get; set; }
        public double Price { get; set; }
        public string ImagePath { get; set; }


        public List<Animal> Animals { get; set; }
    }
}