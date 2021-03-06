﻿using System;
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
            else
            {
                model.Password = "888888";
            }
            RoleList(model);
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
            ViewData["info"] = "" + (count > 0 ? "1" : "0") + "|" + result + "|Index|Edit";
            return View("SuccessScript");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Del(string ids, string opType = "")
        {
            bool isOk = _iMembershipService.DeleteList(ids);
            string result = "失败";
            if (isOk)
            {
                result = "成功";
            }
            return Json(new { success = "" + (isOk ? "ok" : "no") + "", info = "" + result.ToString() + "" });

        }
        /// <summary>
        /// 获取所有的角色
        /// </summary>
        public void RoleList(User model)
        {
            List<Role> list = _iMembershipService.GetListByPageToList(string.Empty, string.Empty, 0, 0, true);
            this.ViewBag.RoleIds = new SelectList(list, "ID", "Name", string.Join(",", model.Roles.Select(m => m.ID)));
        }
    }
}
