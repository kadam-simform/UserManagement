using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalSystem.UserManagement.Database.DatabaseEntity
{
    public class User : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfiileImage{ get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsDeleted { get; set; }

    }


}
