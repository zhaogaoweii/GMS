﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIIC.OMS.Mvc.Html
{
    /// <summary>
    /// 选择城市信息实体
    /// </summary>
    public class SelectCityEntity
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
        /// 城市名称
        /// </summary>
        public object Value_Name { get; set; }

        /// <summary>
        /// 城市ID
        /// </summary>
        public object Value_Id { get; set; }

        /// <summary>
        /// 是否禁止选择
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// 接收选择的城市ID的控件ID
        /// </summary>
        public string SaveIdControlId { get; set; }


        /// <summary>
        /// 接收选择的城市ID的控件Name
        /// </summary>
        public string SaveIdControlName { get; set; }

        /// <summary>
        /// 绑定城市编号的控件ID
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

    }
}
