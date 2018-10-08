﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThatShot.Models
{
    public class Photos
    {

        public int Id{ get; set; }

        public string Url{ get; set; }

        public string Description { get; set; }

        public virtual Genre ThisGenre { get; set; }

        public TSUser User { get; set; }
    }
}