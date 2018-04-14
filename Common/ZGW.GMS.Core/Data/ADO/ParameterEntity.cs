using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZGW.GMS.Core.Data
{
    /// <summary>
    /// 执行参数
    /// </summary>
    public class ParameterEntity
    {
        /// <summary>
        /// 参数名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public DbType DbType { get; set; }
    }
}
