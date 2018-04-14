using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIIC.OMS.Mvc.Html
{
    /// <summary>
    /// 选择机构信息实体
    /// </summary>
    public class SelectOrgEntity
    {
        /// <summary>
        /// 文本框ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 文本框Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文本框的CSS
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// 文本框的样式
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public object Value_Name { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        public object Value_Id { get; set; }

        /// <summary>
        /// 是否禁止选择
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// 接收选择的机构ID的控件ID
        /// </summary>
        public string SaveIdControlId { get; set; }


        /// <summary>
        /// 接收选择的机构ID的控件Name
        /// </summary>
        public string SaveIdControlName { get; set; }

        /// <summary>
        /// 绑定机构编号的控件ID
        /// </summary>
        public string SaveCodeControlId { get; set; }

        /// <summary>
        /// 是否是多选
        /// </summary>
        public bool MultiSelect { get; set; }

        /// <summary>
        /// 点击“确定”之后要回调的函数名
        /// </summary>
        public string CallbackFun { get; set; }

        /// <summary>
        /// 用户ID或负责人ID
        /// </summary>
        public int UserId { get; set; }

    }
}
