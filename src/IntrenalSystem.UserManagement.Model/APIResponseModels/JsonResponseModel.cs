using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IntrenalSystem.UserManagement.Model.APIResponseModels
{
    public class JsonResponseModel<T>
    {
        public T? Result { get; set; }
        private bool Status { get { return !HasError; } }
        public bool HasError { private get; set; }
        public string? Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public Dictionary<string, object> ExtraData { get; } = new Dictionary<string, object>();
    }
}

