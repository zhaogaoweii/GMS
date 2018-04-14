using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ZGW.GMS.Core.Exceptions
{
    /// <summary>
    /// Fault Code的监视器
    /// </summary>
    public static class DuplicateFaultCodeMonitor
    {
        /// <summary>
        /// 执行检查
        /// </summary>
        public static void Perform()
        {
            Type attributeType = typeof(FaultCodeAttribute);
            var duplicate = SystemHelper.LoadAppAssemblies()
                .SelectMany(m => m.GetTypes().Where(t => t.IsDefined(attributeType, false)))
                .Select(m =>
                 {
                     return m.GetCustomAttribute<FaultCodeAttribute>();
                 })
                .GroupBy(f => f.Name)
                .Where(g => g.Count() > 1)
                .Select(g => g.ElementAt(0));
            if (duplicate.Count() > 0)
            {
                throw new InvalidOperationException(string.Format("FaultCode {0} 已经被使用过，请重新设置一个新的FaultCode。", duplicate.FirstOrDefault().Name));
            }
        }
    }
}
