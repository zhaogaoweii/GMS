﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGW.GMS.Core.Utility;

namespace ZGW.GMS.Platform.BusinessEntity
{
    public enum EnumBusinessPermission
    {
        [EnumTitle("[无]", IsDisplay = false)]
        None = 0,
        [EnumTitle("系统管理模块")]
        Sys_Manage = 101,
        /// <summary>
        /// 管理用户
        /// </summary>
        [EnumTitle("管理用户")]
        AccountManage_User = 201,

        /// <summary>
        /// 管理角色
        /// </summary>
        [EnumTitle("管理角色")]
        AccountManage_Role = 202,



        //[EnumTitle("CMS管理文章")]
        //CmsManage_Article = 201,

        //[EnumTitle("CMS管理文章频道")]
        //CmsManage_Channel = 202,


        //[EnumTitle("CRM管理来访来电")]
        //CrmManage_VisitRecord = 301,

        [EnumTitle("CRM管理模块")]
        Crm_Manage = 301,

        [EnumTitle("公司客户")]
        CrmManage_Customer = 401,

        [EnumTitle("个人客户")]
        CrmManage_Person = 402,

        //[EnumTitle("CRM查看统计信息")]
        //CrmManage_Analysis = 304,


        //[EnumTitle("OA管理员工")]
        //OAManage_Staff = 401,

        //[EnumTitle("OA管理部门")]
        //OAManage_Branch = 402,

        //[EnumTitle("组织结构管理")]
        //OAManage_Org = 403,
    }
}
