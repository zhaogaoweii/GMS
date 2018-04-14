using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Exceptions
{
    /// <summary>
    /// 定义Fault Code的Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class FaultCodeAttribute : Attribute
    {
        /// <summary>
        /// Code名称
        /// </summary>
        public string Name { get; set; }
    }
}
