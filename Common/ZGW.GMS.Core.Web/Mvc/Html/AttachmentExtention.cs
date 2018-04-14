using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web;



namespace CIIC.OMS.Mvc.Html
{
    /// <summary>
    /// 附件的扩展方法
    /// </summary>
    public static class AttachmentExtention
    {
        /// <summary>
        /// 功能：通用客户选择控件
        /// 日期：2013-05-31
        /// 调用该控件需在页面引用oms.Dialog.js
        /// </summary>
        /// <param name="html"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IHtmlString SelectCustomerData(this HtmlHelper html,string url,SelectCustomerEntity model)
        {
            string info = "<input type='text' id = '"+ model.Id +"' readonly=\"readonly\" name='"+ model.Name +"' class = '"+ model.Class + "'";
            if (!string.IsNullOrEmpty(model.Style))
            {
                info += "  style = '"+ model.Style +"'";
            }
            info += " value = '"+ model.Value_Name +"'";
            info += " />";
            info += "<input type='hidden' id = '" + model.SaveIdControlId + "' name = '" + model.SaveIdControlName + "' value = '" + model.Value_Id + "' readonly=\"readonly\" />";
            info += "<span class=\"span_select\" " + (model.Disabled ? " disabled = 'disabled' " : "") + " onclick = 'CommonCustomerSelect();' >选择</span>";
            //添加脚本
            info += "<script type=\"text/javascript\">";
            #region 组合参数
            string par = "Id:" + model.Id;
            par += ",Value_Id:" + model.Value_Id;
            par += ",SaveIdControlId:" + model.SaveIdControlId;
            par += ",SaveCodeControlId:" + model.SaveCodeControlId;
            par += ",MultiSelect:" + (model.MultiSelect ? 1 : 0);
            par += ",UserId:" + model.UserId;
            par += ",DataRang:" + model.DataRang;
            par += ",CallbackFun:" + model.CallbackFun;
            #endregion
            if (model.WithPermission)
            {
                info += @"function CommonCustomerSelect()
                      {
                            $(this).showDialog(""" + url + @"CommonSelect/ToCommonSelectCustomerWithPermission?par=" + par + @""", 750, 535, ""选择客户信息"");
                      }";
            }
            else
            {
                info += @"function CommonCustomerSelect()
                      {
                            $(this).showDialog(""" + url + @"CommonSelect/ToCommonSelectCustomer?par=" + par + @""", 750, 535, ""选择客户信息"");
                      }";
            }
            info += "</script>";
            return html.Raw(info);
        }

        /// <summary>
        /// 功能：通用员工选择控件
        /// 日期：2013-06-3
        /// 调用该控件需在页面引用oms.Dialog.js
        /// </summary>
        /// <param name="html"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IHtmlString SelectEmployeeData(this HtmlHelper html, string url, SelectEmployeeEntity model)
        {
            string info = "<input type='text' id = '" + model.Id + "' readonly=\"readonly\" name='" + model.Name + "' class = '" + model.Class + "'";
            if (!string.IsNullOrEmpty(model.Style))
            {
                info += "  style = '" + model.Style + "'";
            }
            info += " value = '" + model.Value_Name + "'";
            info += " readonly=\"readonly\" />";
            info += "<input type='hidden' id = '" + model.SaveIdControlId + "' name = '" + model.SaveIdControlName + "' value = '" + model.Value_Id + "'  />";
            info += "<span class=\"span_select\" " + (model.Disabled ? " disabled = 'disabled' " : "") + " onclick = 'CommonEmployeeSelect();' >选择</span>";
            //添加脚本
            info += "<script type=\"text/javascript\">";
            #region 组合参数
            string par = "Id:" + model.Id;
            par += ",Value_Id:" + model.Value_Id;
            par += ",SaveIdControlId:" + model.SaveIdControlId;
            par += ",SaveCodeControlId:" + model.SaveCodeControlId;
            par += ",SaveHireIdControlId:" + model.SaveHireIdControlId;
            par += ",MultiSelect:" + (model.MultiSelect ? 1 : 0);
            par += ",CustomerId:" + model.CustomerId;
            par += ",UserId:" + model.UserId;
            par += ",DataRang:" + model.DataRang;
            par += ",CallbackFun:" + model.CallbackFun;
            #endregion
            info += @"function CommonEmployeeSelect()
                      {
                            $(this).showDialog(""" + url + @"CommonSelect/ToCommonSelectEmployee?par=" + par + @""", 750, 535, ""选择员工信息"");
                      }";
            info += "</script>";
            return html.Raw(info);
        }

        /// <summary>
        /// 功能：通用城市选择控件
        /// 日期：2013-06-3
        /// 调用该控件需在页面引用oms.Dialog.js
        /// </summary>
        /// <param name="html"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IHtmlString SelectCityData(this HtmlHelper html, string url,SelectCityEntity model)
        {
            string info = "<input type='text' id = '" + model.Id + "' readonly=\"readonly\" name='" + model.Name + "' class = '" + model.Class + "'";
            if (!string.IsNullOrEmpty(model.Style))
            {
                info += "  style = '" + model.Style + "'";
            }
            info += " value = '" + model.Value_Name + "'";
            info += " readonly=\"readonly\" />";
            info += "<input type='hidden' id = '" + model.SaveIdControlId + "' name = '" + model.SaveIdControlName + "' value = '" + model.Value_Id + "'  />";
            info += "<span class=\"span_select\" " + (model.Disabled ? " disabled = 'disabled' " : "") + " onclick = 'CommonCitySelect();' >选择</span>";
            //添加脚本
            info += "<script type=\"text/javascript\">";
            #region 组合参数
            string par = "Id:" + model.Id;
            par += ",Value_Id:" + model.Value_Id;
            par += ",SaveIdControlId:" + model.SaveIdControlId;
            par += ",SaveCodeControlId:" + model.SaveCodeControlId;
            par += ",MultiSelect:" + (model.MultiSelect ? 1 : 0);
            par += ",CallbackFun:" + model.CallbackFun;
            #endregion
            info += @"function CommonCitySelect()
                      {
                            $(this).showDialog(""" + url + @"CommonSelect/ToCommonSelectCity?par=" + par + @""", 750, 535, ""选择城市信息"");
                      }";
            info += "</script>";
            return html.Raw(info);
        }

        /// <summary>
        /// 功能：通用机构选择控件
        /// 日期：2013-06-3
        /// 调用该控件需在页面引用oms.Dialog.js
        /// </summary>
        /// <param name="html"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IHtmlString SelectOrgData(this HtmlHelper html, string url, SelectOrgEntity model)
        {
            string info = "<input type='text' id = '" + model.Id + "' readonly=\"readonly\" name='" + model.Name + "' class = '" + model.Class + "'";
            if (!string.IsNullOrEmpty(model.Style))
            {
                info += "  style = '" + model.Style + "'";
            }
            info += " value = '" + model.Value_Name + "'";
            info += " readonly=\"readonly\" />";
            info += "<input type='hidden' id = '" + model.SaveIdControlId + "' name = '" + model.SaveIdControlName + "' value = '" + model.Value_Id + "'  />";
            info += "<span class=\"span_select\" " + (model.Disabled ? " disabled = 'disabled' " : "") + " onclick = 'CommonOrgSelect();' >选择</span>";
            //添加脚本
            info += "<script type=\"text/javascript\">";
            #region 组合参数
            string par = "Id:" + model.Id;
            par += ",Value_Id:" + model.Value_Id;
            par += ",SaveIdControlId:" + model.SaveIdControlId;
            par += ",SaveCodeControlId:" + model.SaveCodeControlId;
            par += ",MultiSelect:" + (model.MultiSelect ? 1 : 0);
            par += ",UserId:" + model.UserId;            
            par += ",CallbackFun:" + model.CallbackFun;
            #endregion
            info += @"function CommonOrgSelect()
                      {
                            $(this).showDialog(""" + url + @"CommonSelect/ToCommonSelectOrg?par=" + par + @""", 750, 535, ""选择机构信息"");
                      }";
            info += "</script>";
            return html.Raw(info);
        }
        /// <summary>
        /// 功能：通用机构选择控件
        /// 日期：2013-07-5
        /// 调用该控件需在页面引用oms.Dialog.js
        /// </summary>
        /// <param name="html"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IHtmlString SelectOrgStaffData(this HtmlHelper html, string url, SelectOrgStaffEntity model)
        {
            string info = "<input type='text' id = '" + model.Id + "' name='" + model.Name + "' class = '" + model.Class + "'";
            if (!string.IsNullOrEmpty(model.Style))
            {
                info += "  style = '" + model.Style + "'";
            }
            info += " value = '" + model.Value_Name + "'";
            info += " readonly=\"readonly\" />\r\n";
            info += "<input type='hidden' id = '" + model.SaveIdControlId + "' name = '" + model.SaveIdControlName + "' value = '" + model.Value_Id + "'  />\r\n";
            info += "<span class=\"span_select\" " + (model.Disabled ? " disabled = 'disabled' " : "") + " onclick = 'CommonOrgStaffSelect();' >选择</span>\r\n";
            //添加脚本
            info += "<script type=\"text/javascript\">";
            #region 组合参数
            string par = "Id:" + model.Id;
            par += ",Value_Id:" + model.Value_Id;
            par += ",SaveIdControlId:" + model.SaveIdControlId;
            par += ",SaveCodeControlId:" + model.SaveCodeControlId;
            par += ",MultiSelect:" + (model.MultiSelect ? 1 : 0);
            par += ",CallbackFun:" + model.CallbackFun;
            #endregion
            info += @"function CommonOrgStaffSelect()
                      {
                            $(this).showDialog(""" + url + @"CommonSelect/ToCommonSelectOrgStaff?par=" + par + @""", 750, 535, ""选择机构负责人信息"");
                      }";
            info += "</script>";
            return html.Raw(info);
        }
        /// <summary>
        /// 功能：通用支出单编号选择控件
        /// 日期：2013-07-5
        /// 调用该控件需在页面引用oms.Dialog.js
        /// </summary>
        /// <param name="html"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IHtmlString SelectPayoutCodeData(this HtmlHelper html, string url, SelectPayoutCodeEntity model)
        {
            string info = "<input type='text' id = '" + model.Id + "' name='" + model.Name + "' class = '" + model.Class + "'";
            if (!string.IsNullOrEmpty(model.Style))
            {
                info += "  style = '" + model.Style + "'";
            }
            info += " value = '" + model.Value_Name + "'";
            info += " readonly=\"readonly\" />\r\n";
            info += "<input type='hidden' id = '" + model.SaveIdControlId + "' name = '" + model.SaveIdControlName + "' value = '" + model.Value_Id + "'  />\r\n";
            info += "<span class=\"span_select\" " + (model.Disabled ? " disabled = 'disabled' " : "") + " onclick = 'CommonPayoutCodeSelect();' >选择</span>\r\n";
            //添加脚本
            info += "<script type=\"text/javascript\">";
            #region 组合参数
            string par = "Id:" + model.Id;
            par += ",Value_Id:" + model.Value_Id;
            par += ",SaveIdControlId:" + model.SaveIdControlId;
            par += ",SaveCodeControlId:" + model.SaveCodeControlId;
            par += ",MultiSelect:" + (model.MultiSelect ? 1 : 0);
            par += ",CallbackFun:" + model.CallbackFun;
            #endregion
            info += @"function CommonPayoutCodeSelect()
                      {
                            $(this).showDialog(""" + url + @"CommonSelect/ToCommonSelectPayoutCode?par=" + par + @""", 750, 535, ""选择支出单信息"");
                      }";
            info += "</script>";
            return html.Raw(info);
        }
        /// <summary>
        /// 功能：通用调整政策名称选择控件
        /// 日期：2013-07-23
        /// 调用该控件需在页面引用oms.Dialog.js
        /// </summary>
        /// <param name="html"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IHtmlString SelectPolocyChangeNameData(this HtmlHelper html, string url, SelectPayoutCodeEntity model)
        {
            string info = "<input type='text' id = '" + model.Id + "' name='" + model.Name + "' class = '" + model.Class + "'";
            if (!string.IsNullOrEmpty(model.Style))
            {
                info += "  style = '" + model.Style + "'";
            }
            info += " value = '" + model.Value_Name + "'";
            info += " readonly=\"readonly\" />\r\n";
            info += "<input type='hidden' id = '" + model.SaveIdControlId + "' name = '" + model.SaveIdControlName + "' value = '" + model.Value_Id + "'  />\r\n";
            info += "<span class=\"span_select\" " + (model.Disabled ? " disabled = 'disabled' " : "") + " onclick = 'CommonPolocyChangeNameSelect();' >选择</span>\r\n";
            //添加脚本
            info += "<script type=\"text/javascript\">";
            #region 组合参数
            string par = "Id:" + model.Id;
            par += ",Value_Id:" + model.Value_Id;
            par += ",SaveIdControlId:" + model.SaveIdControlId;
            par += ",SaveCodeControlId:" + model.SaveCodeControlId;
            par += ",MultiSelect:" + (model.MultiSelect ? 1 : 0);
            par += ",CallbackFun:" + model.CallbackFun;
            #endregion
            info += @"function CommonPolocyChangeNameSelect()
                      {
                            $(this).showDialog(""" + url + @"CommonSelect/ToCommonSelectPolocyChangeName?par=" + par + @""", 750, 535, ""选择调整政策信息"");
                      }";
            info += "</script>";
            return html.Raw(info);
        }
        /// <summary>
        /// 单文件上传
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="name">控件的Name</param>
        /// <param name="recordId">当前的AttachmentInfo的Id</param>
        /// <param name="options">FileUploadOption参数</param>
        /// <returns>MvcHtmlString</returns>
        //public static MvcHtmlString FileUpload(this HtmlHelper html, string name, int? recordId, FileUploadOption options)
        //{
        //    string tplPath = "~/Views/Html/Attachment/FileUpload.cshtml";
        //    dynamic o = new ExpandoObject();
        //    o.Name = name;
        //    o.Option = options ?? new FileUploadOption();
        //    if (StringHelper.IsNullOrEmpty(o.Option.FileExt))
        //    {
        //        o.Option.FileExt = SiteConfig.FileExt;
        //    }
        //    o.Info = null;
        //    o.IsDB = false;
        //    o.Info = recordId != null && recordId.Value!=0
        //        ? ObjectContainer.ResolveService<IAttachmentService>().GetAttachment(recordId.Value)
        //        : null;

        //    return html.Partial(tplPath, (object)o);
        //}

        ///// <summary>
        ///// 多文件上传
        ///// </summary>
        ///// <param name="html">HtmlHelper</param>
        ///// <param name="name">控件的Name</param>
        ///// <param name="recordId">当前的AttachmentInfo的Id</param>
        ///// <param name="options">FileUploadOption参数</param>
        ///// <returns>MvcHtmlString</returns>
        //public static MvcHtmlString MultipleFileUpload(this HtmlHelper html, string name, int? recordId, FileUploadOption options)
        //{
        //    string tplPath = "~/Views/Html/Attachment/MultipleFileUpload.cshtml";
        //    dynamic o = new ExpandoObject();
        //    o.Name = name;
        //    o.Option = options ?? new FileUploadOption();
        //    if(StringHelper.IsNullOrEmpty(o.Option.FileExt)){
        //        o.Option.FileExt = SiteConfig.FileExt;
        //    }
        //    o.IsDB = false;
        //    o.Info = recordId != null
        //        ? ObjectContainer.ResolveService<IAttachmentService>().GetAttachment(recordId.Value)
        //        : null;

        //    return html.Partial(tplPath, (object)o);
        //}

        /// <summary>
        /// 附件的下载路径
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="id">AttachmentItem的Id</param>
        /// <returns>文件的下载路径</returns>
        public static string Download(this UrlHelper url, int id)
        {
            return url.Action("DownLoad", "Attachment", new { id = id, area = "CIIC.OMS.Infrastructure.Web" });
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="id">AttachmentItem的ID</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString FileDownload(this HtmlHelper html, int id)
        {
            return html.Action("FileDownload", "Attachment", new { id = id });
        }
    }

    /// <summary>
    /// 文件上传配置项
    /// </summary>
    public class FileUploadOption
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 功能名称
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// 上传的文件后辍
        /// </summary>
        public string FileExt { get; set; }

        /// <summary>
        /// 必填项
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 控件的Css样式
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// 上传模式
        /// </summary>
       // public UploadMode Mode { get; set; }

        /// <summary>
        /// 上传后制定的相对路径（目录）
        /// </summary>
        public string UploadDir { get; set; }
        
    }
}
