using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalSystem.UserManagement.Service.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public  class ServiceLifeTimeAttribute : Attribute
    {
        public ServiceLifetime LifeTime { get; set; }

        public ServiceLifeTimeAttribute(ServiceLifetime serviceLifetime)
        {
            LifeTime = serviceLifetime;
        }
    }
}
