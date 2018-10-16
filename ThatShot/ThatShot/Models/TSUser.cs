using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThatShot.Models
{
    public class TSUser: IdentityUser<Guid> //specify your database primary key type. in this case GUID or uniqueidentifier
    {
        //add your properties like Birthday, RegisterDate, etc.
        public TSRole Role { get; set; }

        //add your relationships
        public virtual ICollection<Picture> Photos { get; set; }
    }
    
    
}
