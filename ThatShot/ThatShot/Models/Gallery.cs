using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThatShot.Models
{
    public class Gallery
    {
        public int ID { get; set; }

       public virtual List< Picture> Photos{ get; set; }
    }
}
