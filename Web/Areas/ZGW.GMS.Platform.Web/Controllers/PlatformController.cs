using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZGW.GMS.Platform.Web.Controllers
{
    public class PlatformController : Controller
    {
        //
        // GET: /Platform/

        /// <summary>
        /// 系统登录页面
        /// </summary>
        /// <returns></returns>

        public ActionResult Index()
        {
            if (this.Session["user"] == null)
            {
                return RedirectToAction("Login", new { controller = "Verification" });
            }
            return View();
        }

    }
}
