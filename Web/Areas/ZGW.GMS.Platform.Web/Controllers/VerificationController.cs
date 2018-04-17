using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ZGW.GMS.Platform.Web.Models;
using ZGW.GMS.Platform.Service.Interface;
using ZGW.GMS.Platform.BusinessEntity;
namespace ZGW.GMS.Platform.Web.Controllers
{

    public class VerificationController : Controller
    {
        //
        // GET: /Verification/
        IMembershipService _iMembershipService;
        public VerificationController(IMembershipService iMembershipService)
        {
            this._iMembershipService = iMembershipService;
        }
        #region 登陆

        /// <summary>
        /// 系统登录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            this.Session.Clear();
            FormsAuthentication.SignOut();
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModel model)
        {

            var item = _iMembershipService.Login(model.LoginName, model.Password);
            if (item.Equals(ValidationStatus.UserNotExist))
            {
                //return "用户名不存在";
                ModelState.AddModelError("LoginName", "用户名不存在");
                return View("Login");
            }
            else if (item.Equals(ValidationStatus.WrongPassword))
            {
                // return "密码错误";
                ModelState.AddModelError("Password", "用户名或密码错误");
                return View("Login");
            }
            else if (item.Equals(ValidationStatus.AccountLocked))
            {
                // return "用户已被锁定，请联系管理员。";
                ModelState.AddModelError("LoginName", "用户已被锁定，请联系管理员");
                return View("Login");
            }
            else if (item.Equals(ValidationStatus.OrgNotExist))
            {
                ModelState.AddModelError("LoginName", "用户所在部门已被删除或禁用，请联系管理员");
                return View("Login");

            }
            else
            {
                FormsAuthentication.SetAuthCookie(model.LoginName, true);
                //这个地方赋值权限
                User user = _iMembershipService.GetModelByStrWhere(" and LoginName='" + model.LoginName + "' ");

                Session["user"] = user;
                //检查Application
                Session["userName"] = user.LoginName;
                
                //if (this.HttpContext.Application["ModuleRootList"] == null)
                //{
                //    this.HttpContext.Application["ModuleRootList"] = moduleService.GetModuleRoots();
                //}
                //if (this.HttpContext.Application["OrganizationRootList"] == null)
                //{
                //    this.HttpContext.Application["OrganizationRootList"] = organizationService.GetOrganizationTreeRootList();
                //}
                //if (this.HttpContext.Application["BaseItemList"] == null)
                //{
                //    this.HttpContext.Application["BaseItemList"] = baseService.GetAllBaseItemList();
                //}

                ////为Session赋值 
                //Staff staff = staffService.GetStaffByLoginName(model.LoginName);
                //staff.MergedPermissions = membershipService.GetMergedPermissionsByStaffId(staff.Id);

                //this.Session["User"] = staff;
                //this.Session["UserDepartmentName"] = staff.DepartmentName;
                //this.Session["UserGroupName"] = staff.GroupName;
                //this.Session["UserName"] = staff.UserName;
                //this.Session["LoginName"] = staff.LoginName;
                //this.Session["DutyName"] = ((BaseItemList)this.HttpContext.Application["BaseItemList"]).GetItemNameByCode("72", staff.Duty);

                string ReturnUrl = this.HttpContext.Request.UrlReferrer == null ? string.Empty : Server.UrlDecode(System.Text.RegularExpressions.Regex.Match(this.HttpContext.Request.UrlReferrer.Query, @"ReturnUrl=([^&]*)?").Groups[1].ToString()) == "/" ? string.Empty : Server.UrlDecode(System.Text.RegularExpressions.Regex.Match(this.HttpContext.Request.UrlReferrer.Query, @"ReturnUrl=([^&]*)?").Groups[1].ToString());
                if (string.IsNullOrEmpty(ReturnUrl))
                {
                    ReturnUrl = "../Platform/Index";
                }
                return View(ReturnUrl);
                //this.HttpContext.Request.
                //return "登陆成功" + "|" + (string.IsNullOrEmpty(ReturnUrl) ? ReturnUrl : "http://" + this.HttpContext.Request.Url.Host + ":" + this.HttpContext.Request.Url.Port + ReturnUrl);
            }

        }

        public JsonResult SsoSyncLogin(LoginModel model)
        {
            var item = _iMembershipService.Login(model.LoginName, model.Password);
            if (item.Equals(ValidationStatus.UserNotExist))
            {
                return new JsonResult() { Data = new { Result = "Error", Message = "用户名不存在" } };
            }
            else if (item.Equals(ValidationStatus.WrongPassword))
            {
                return new JsonResult() { Data = new { Result = "Error", Message = "密码错误" } };
            }
            else if (item.Equals(ValidationStatus.AccountLocked))
            {
                return new JsonResult() { Data = new { Result = "Error", Message = "用户已被锁定，请联系管理员" } };
            }
            else if (item.Equals(ValidationStatus.OrgNotExist))
            {
                return new JsonResult() { Data = new { Result = "Error", Message = "用户所在部门已被删除或禁用，请联系管理员" } };
            }
            else
            {
                FormsAuthentication.SetAuthCookie(model.LoginName, false);
                ////检查Application
                //if (this.HttpContext.Application["ModuleRootList"] == null)
                //{
                //    this.HttpContext.Application["ModuleRootList"] = moduleService.GetModuleRoots();
                //}
                //if (this.HttpContext.Application["OrganizationRootList"] == null)
                //{
                //    this.HttpContext.Application["OrganizationRootList"] = organizationService.GetOrganizationTreeRootList();
                //}
                //if (this.HttpContext.Application["BaseItemList"] == null)
                //{
                //    this.HttpContext.Application["BaseItemList"] = baseService.GetAllBaseItemList();
                //}

                ////为Session赋值 
                //Staff staff = staffService.GetStaffByLoginName(model.LoginName);
                //staff.MergedPermissions = membershipService.GetMergedPermissionsByStaffId(staff.Id);

                //this.Session["User"] = staff;
                //this.Session["UserDepartmentName"] = staff.DepartmentName;
                //this.Session["UserGroupName"] = staff.GroupName;
                //this.Session["UserName"] = staff.UserName;
                //this.Session["LoginName"] = staff.LoginName;
                //this.Session["DutyName"] = ((BaseItemList)this.HttpContext.Application["BaseItemList"]).GetItemNameByCode("72", staff.Duty);

                return new JsonResult() { Data = new { Result = "Success", Message = "登录成功" } };
            }
        }

        public void LoginMain()
        {
            // Staff staff = (Staff)Session["User"];
            FormsAuthentication.RedirectFromLoginPage("admin", true);
        }

        #region 获取密码（通过登陆账户和邮箱判断员工是公司的员工，如果是，邮件发送密码）
        /// <summary>
        /// 进入获取密码页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ResetPassword(string dialog_Id)
        {
            ViewData["dialog_Id"] = dialog_Id;
            return View();
        }


        /// <summary>
        /// 获取密码验证方法
        /// </summary>
        /// <param name="loginId">登陆用户</param>
        /// <param name="email">邮箱</param>
        /// <param name="checkCode">验证码</param>
        /// <returns></returns>
        private string ResetPasswordCheck(string loginId, string email, string checkCode)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(loginId))
            {
                result += "登陆账户不允许为空！<br/>";
            }
            if (string.IsNullOrEmpty(email))
            {
                result += "邮箱不允许为空！<br/>";
            }
            else
            {
                string regexEmail = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]+$";

                System.Text.RegularExpressions.RegexOptions options = ((System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace | System.Text.RegularExpressions.RegexOptions.Multiline) | System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Regex regEmial = new System.Text.RegularExpressions.Regex(regexEmail, options);
                if (!regEmial.IsMatch(email))
                {
                    result += "邮箱格式不正确！<br/>";
                }
            }

            if (string.IsNullOrEmpty(checkCode))
            {
                result += "验证码不允许为空！<br/>";
            }
            else if (checkCode.ToLower() != Session["checkcode"].ToString().ToLower())
            {
                result += "验证码不正确！<br/>";
            }

            return result;
        }

        #endregion

        #endregion


        #region 密码修改
        ///// <summary>
        ///// 进入密码修改页面
        ///// </summary>
        ///// <param name="dialog_Id"></param>
        ///// <returns></returns>
        //public ActionResult ChangePassword(string dialog_Id)
        //{
        //    Staff staff = (Staff)Session["User"];

        //    ViewData["dialog_Id"] = dialog_Id;
        //    ViewData["UserName"] = staff.UserName ?? "";
        //    return View();
        //}
        ///// <summary>
        ///// 提交修改密码
        ///// </summary>
        ///// <param name="oldpassword">旧密码</param>
        ///// <param name="newpassword1">新密码1</param>
        ///// <param name="newpassword2">新密码2</param>
        ///// <returns></returns>
        //public ActionResult SubmitChangePassword(string oldpassword, string newpassword1, string newpassword2)
        //{
        //    Staff staff = (Staff)Session["User"];
        //    int status = 0;//默认验证失败
        //    string message = ChangePasswordCheck(staff, oldpassword, newpassword1, newpassword2);

        //    if (string.IsNullOrWhiteSpace(message))
        //    {

        //        if (staffService.ChangeUserPassword(staff.Id, newpassword1) >= 1)
        //        {
        //            status = 1;
        //            message = "密码修改成功,请重新登录！";
        //        }
        //        else
        //        {
        //            message = "密码修改失败,请稍后操作！";
        //        }
        //    }

        //    return Json(new { Status = status, Message = message }); ;
        //}
        ///// <summary>
        ///// 验证修改密码数据
        ///// </summary>
        ///// <param name="staff">用户信息</param>
        ///// <param name="oldpassword">旧密码</param>
        ///// <param name="newpassword1">新密码1</param>
        ///// <param name="newpassword2">新密码2</param>
        ///// <returns></returns>
        //private string ChangePasswordCheck(Staff staff, string oldpassword, string newpassword1, string newpassword2)
        //{
        //    if (string.IsNullOrEmpty(newpassword1))
        //    {
        //        return "新密码不允许为空！";
        //    }
        //    if (newpassword1.Contains(" "))
        //    {
        //        return "新密码包含特殊字符！";
        //    }
        //    if (!(newpassword1.Length >= 4 && newpassword1.Length <= 16))
        //    {
        //        return "新密码长度不在4-16之间！";
        //    }
        //    if (staff.Password != oldpassword)
        //    {
        //        return "旧密码错误！";
        //    }
        //    if (newpassword1 != newpassword2)
        //    {
        //        return "二次输入新密码不一致请重新输入！";
        //    }
        //    if (staff.Password == newpassword1)
        //    {
        //        return "新密码和旧密码不能一样！";
        //    }

        //    return "";
        //}

        #endregion

    }
}
