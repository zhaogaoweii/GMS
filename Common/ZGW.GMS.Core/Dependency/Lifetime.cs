using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// 实体生命周期
    /// </summary>
    public enum Lifetime
    {
        /// <summary>
        /// 瞬态
        /// </summary>
        Transient,
        /// <summary>
        /// 单例
        /// </summary>
        Singleton,
        /// <summary>
        /// 容器的生存期
        /// </summary>
        Container
    }
}
