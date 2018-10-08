using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThatShot.Models
{
    public class Genre
    {
<<<<<<< HEAD
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
=======

        public int ID { get; set; }

        public string Name { get; set; }

        public virtual List<Photos> Photos { get; set; }
    }
}   
>>>>>>> 1620dd13cd0ec7982e58c7b3a55cbc6d68da577b
