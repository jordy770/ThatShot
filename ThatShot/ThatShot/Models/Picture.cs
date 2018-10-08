﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThatShot.Models
{
    public class Picture
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public int User_Id { get; set; }
        public int Genre.Id { get; set; }
    }
}