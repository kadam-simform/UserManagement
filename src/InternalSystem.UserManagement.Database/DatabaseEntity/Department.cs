﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalSystem.UserManagement.Database.DatabaseEntity
{
    public class Department 
    {
        [Key]
        public int Id { get; set; }
        public string? DepartmentName { get; set; }
    }
}
