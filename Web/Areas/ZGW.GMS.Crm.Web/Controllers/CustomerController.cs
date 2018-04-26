using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGW.GMS.Core.PagedListUser;
using ZGW.GMS.Crm.BusinessEntity;
using ZGW.GMS.Crm.Service.Interface;

namespace ZGW.GMS.Crm.Web.Controllers
{
    [ZGW.GMS.Core.Mvc.Filters.AuthorizeFilter]
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/
        ICrmService _iCrmService;
        public CustomerController(ICrmService iCrmService)
        {
            this._iCrmService = iCrmService;
        }
        /// <summary>
        /// 获取公司用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyIndex()
        {
            int pageIndex = 0;
            if (!string.IsNullOrEmpty(Request["pageIndex"]))
            {
                pageIndex = Convert.ToInt32(Request["pageIndex"]);
            }
            List<Company> list = _iCrmService.GetListCompany(string.Empty, string.Empty, 0, 0, true);// _iCrmService.GetPersonmodel("", "", pageIndex, 12);

            return View(list.ToPagedList<Company>(pageIndex, 20));

        }
        /// <summary>
        /// 新增或者编辑公司用户
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult CompanyEdit(int ID = 0)
        {
            Company model = new Company();
            if (ID != 0)
            {
                model = _iCrmService.GetCompanymodel(ID);
            }
            return View(model);
        }
        /// <summary>
        /// 新增或者编辑公司用户
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CompanyEdit(Company model)
        {
            string result = "失败";
            model.lastOperateTime = DateTime.Now;
            model.operatorTime = DateTime.Now;
            int count = _iCrmService.AddCompany(model);
            if (count > 0)
            {
                result = "成功";
            }
            ViewData["info"] = "" + (count > 0 ? "1" : "0") + "|" + result + "|Customer/CompanyIndex";
            return View("SuccessScript");
        }
        /// <summary>
        /// 获取一般用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ElectricityUserIndex()
        {
            int pageIndex = 0;
            if (!string.IsNullOrEmpty(Request["pageIndex"]))
            {
                pageIndex = Convert.ToInt32(Request["pageIndex"]);
            }
            List<ElectricityUser> list = _iCrmService.GetListElectricityUser(string.Empty, string.Empty, 0, 0, true);// _iCrmService.GetPersonmodel("", "", pageIndex, 12);

            return View(list.ToPagedList<ElectricityUser>(pageIndex, 20));

        }
        /// <summary>
        /// 新增或者编辑一般客户
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult ElectricityUserEdit(int ID = 0)
        {
            ElectricityUser model = new ElectricityUser();
            if (ID != 0)
            {
                model = _iCrmService.GetPersonmodel(ID);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult ElectricityUserEdit(ElectricityUser model)
        {
            string result = "失败";
            model.isDel = 0;
            model.lastOperateTime = DateTime.Now;
            model.operatorTime = DateTime.Now;
            int count = _iCrmService.AddPersonuser(model);
            if (count > 0)
            {
                result = "成功";
            }
            ViewData["info"] = "" + (count > 0 ? "1" : "0") + "|" + result + "|ElectricityUserIndex|Edit";
            return View("SuccessScript");
        }
        [HttpPost]
        public JsonResult Del(string ids, string opType = "")
        {
            string result = "失败";
            bool isOk = false;
            switch (opType)
            {
                case "person":
                    isOk = _iCrmService.DelPersonuser(ids);
                    break;
                case "company":
                    isOk = _iCrmService.DelCompany(ids);
                    break;
                default:
                    break;
            }
            if (isOk)
            {
                result = "成功";
            }
            return Json(new { success = "" + (isOk ? "ok" : "no") + "", info = "" + result.ToString() + "" });
        }

    }
}
