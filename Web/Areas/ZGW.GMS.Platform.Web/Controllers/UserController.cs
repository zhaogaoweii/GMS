using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGW.GMS.Platform.BusinessEntity;
using ZGW.GMS.Core;
using ZGW.GMS.Core.PagedListUser;

using ZGW.GMS.Platform.Service.Interface;
namespace ZGW.GMS.Platform.Web.Controllers
{
    [ZGW.GMS.Core.Mvc.Filters.AuthorizeFilter]
    public class UserController : Controller
    {
        //
        // GET: /User/
        IMembershipService _iMembershipService;
        public UserController(IMembershipService iMembershipService)
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
            List<User> list = _iMembershipService.GetListuser("", "", 0, 0, true);

            return View(list.ToPagedList<User>(pageIndex, 20));
        }
        public ActionResult Edit(int id = 0)
        {
            User model = new BusinessEntity.User();
            if (id != 0)
            {
                model = _iMembershipService.GetModelByStrWhere(string.Format(" and ID={0} ", id));
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(User model)
        {
            int count = _iMembershipService.Add(model);
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
            bool isOk = _iMembershipService.DeleteList(ids);
            string result = "失败";
            if (isOk)
            {
                result = "成功";
            }
            return this.RefreshParent(result);
        }

    }
}
