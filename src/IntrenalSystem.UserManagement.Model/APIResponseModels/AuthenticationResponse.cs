using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrenalSystem.UserManagement.Model.APIResponseModels
{
    public class AuthenticationResponse
    {
        public int UserId { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
