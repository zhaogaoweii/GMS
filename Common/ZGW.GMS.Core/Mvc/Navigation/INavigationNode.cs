using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Mvc.Navigation
{
    /// <summary>
    /// 导航元素接口
    /// </summary>
    interface INavigationNode
    {
        /// <summary>
        /// ID
        /// </summary>
        int ID { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 显示文字
        /// </summary>
        string DisplayText { get; set; }

        /// <summary>
        /// 提示
        /// </summary>
        string Tip { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        int Order { get; set; }

        /// <summary>
        /// 是否是组
        /// </summary>
        bool IsGroup { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        string URL { get; set; }

        /// <summary>
        /// 子元素集合
        /// </summary>
        ICollection<INavigationNode> Children { get; set; }
    }
}
