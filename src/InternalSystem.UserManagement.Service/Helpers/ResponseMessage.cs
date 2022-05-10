using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalSystem.UserManagement.Services.Helpers
{
    public class ResponseMessage
    {
        protected ResponseMessage()
        {
        }

        // Authhentication
        public const string ValidationSuccess = "username and password is valid";
        public const string ValidationFailure = "username and password is not valid";
    }
}
