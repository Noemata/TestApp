using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Services
{
    public enum InstanceMode
    {
        /// <summary>
        /// Not registered
        /// </summary>
        None = 0,

        /// <summary>
        /// Single
        /// </summary>
        Singleton,

        /// <summary>
        /// Each instance
        /// </summary>
        PerRequest
    }


    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class RegisterCMAttributeAttribute : Attribute
    {
        public InstanceMode Mode { get; set; }

        public RegisterCMAttributeAttribute(InstanceMode mode) {
            this.Mode = mode;
        }

    }
}
