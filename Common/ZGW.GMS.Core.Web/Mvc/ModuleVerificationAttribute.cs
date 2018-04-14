using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Common.Core.Mvc.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class ModuleVerificationAttribute : FilterAttribute, IActionFilter
    {
        public string ModuleCode { get; set; }
        public Staff User
        {
            get
            {
                return HttpContext.Current.Session["User"] as Staff;
            }
        }
        public virtual void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!HasModulePermission(ModuleCode))
            {
                filterContext.HttpContext.Response.Redirect("~/Platform/Forbidden");
                filterContext.HttpContext.Response.End();
            }
            HttpContext.Current.Session["ModuleCode"] = ModuleCode;
        }
        public virtual void OnActionExecuted(ActionExecutedContext filterContext)
        { 
        }
        private bool HasModulePermission(string moduleCode)
        {
            return this.User.MergedPermissions.HasModulePermission(moduleCode);
        }
    }
}