using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIIC.OMS.Mvc.Html
{
    /// <summary>
    /// 选择员工信息实体
    /// </summary>
    public class SelectEmployeeEntity
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
        /// 员工名称
        /// </summary>
        public object Value_Name { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public object Value_Id { get; set; }

        /// <summary>
        /// 是否禁止选择
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// 接收选择的员工ID的控件ID
        /// </summary>
        public string SaveIdControlId { get; set; }


        /// <summary>
        /// 接收选择的员工ID的控件Name
        /// </summary>
        public string SaveIdControlName { get; set; }

        /// <summary>
        /// 绑定员工编号的控件ID
        /// </summary>
        public string SaveCodeControlId { get; set; }

        /// <summary>
        /// 接收选择的雇佣关系ID
        /// </summary>
        public string SaveHireIdControlId { get; set; }

        /// <summary>
        /// 雇佣关系id 是否叠加  0否 1是
        /// </summary>
        public string isHireIdSuperposition { get; set; }

        /// <summary>
        /// 是否是多选
        /// </summary>
        public bool MultiSelect { get; set; }

        


        /// <summary>
        /// 点击“确定”之后要回调的函数名
        /// </summary>
        public string CallbackFun { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// 客户负责人ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 可查看数据范围 0全部 1自己 2本小组 3本部门
        /// </summary>
        public int DataRang { get; set; }

        /// <summary>
        /// 雇佣关系状态 0 无效 1有效
        /// </summary>
        public int HireStatus { get; set; }



    }
}
