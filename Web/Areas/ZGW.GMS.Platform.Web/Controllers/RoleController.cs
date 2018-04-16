using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGW.GMS.Platform.BusinessEntity;
using ZGW.GMS.Platform.Service.Interface;
using ZGW.GMS.Core.PagedListUser;
using ZGW.GMS.Core.Utility;
namespace ZGW.GMS.Platform.Web.Controllers
{
    [ZGW.GMS.Core.Mvc.Filters.AuthorizeFilter]
    public class RoleController : Controller
    {
        //
        // GET: /User/
        IMembershipService _iMembershipService;
        public RoleController(IMembershipService iMembershipService)
        {
            this._iMembershipService = iMembershipService;
        }
        public ActionResult Index()
        {
            int pageIndex = 0;
            if (!string.IsNullOrEmpty(Request["pageIndex"]))
            {
                pageIndex = Convert.ToInt32(Request["pageIndex"]);
            }
            List<Role> list = _iMembershipService.GetListByPageToList("", "", pageIndex, 20, true);

            return View(list.ToPagedList<Role>(pageIndex, 20));
        }
        public ActionResult Edit(int id = 0)
        {
            Role model = new BusinessEntity.Role();
            if (id != 0)
            {
                model = _iMembershipService.GetModelRole(id);
            }
            GetBusinessPermissionList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Role model)
        {
            int count = 0;
            if (model.ID != 0)
            {
                count = _iMembershipService.UpdateRole(model) ? 1 : 0;
            }
            else
            {
                count = _iMembershipService.AddRole(model);
            }
            string result = "失败";
            if (count > 0)
            {
                result = "成功";

            }
            return this.RefreshParent(result);
        }
        /// <summary>
        /// 当弹出DIV弹窗时，需要刷新浏览器整个页面
        /// </summary>
        /// <returns></returns>
        public ContentResult RefreshParent(string alert = null)
        {
            var script = string.Format("<script>{0}; parent.location.reload(1)</script>", string.IsNullOrEmpty(alert) ? string.Empty : "alert('" + alert + "')");
            return this.Content(script);
        }
        [HttpPost]
        public ActionResult Del(string ids)
        {
            bool isOk = _iMembershipService.DeleteListRole(ids);
            string result = "失败";
            if (isOk)
            {
                result = "成功";
            }
            return this.RefreshParent(result);
        }

        public void GetBusinessPermissionList()
        {
            var businessPermissionList = EnumHelper.GetItemValueList<EnumBusinessPermission>();
            this.ViewBag.BusinessPermissionList = new SelectList(businessPermissionList, "Key", "Value");
        }
    }
}
