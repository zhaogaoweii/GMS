using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace ZGW.GMS.Core.Mvc.Filters
{
    [AuthorizeFilter]
    public class AuthorizeFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["user"] == null)
            {
                filterContext.Result = new RedirectResult("/Verification/Login");
            }

            // //我们先来了解一下这个filterContext参数：我们知道OnActionExecuting方法是在Action执行之前会被触发执行的一个方法，那就意味着，将来我在这里面写代码，想要知道你这一个OnActionExecuting方法到底是由那一个Action被调用的时候触发的 （因为所有的action方法被执行的时候都会触发OnActionExecuting这个过滤器方法，所以我就像要知道到底是哪个action被执行的时候触发的这个OnActionExecuting方法）  

            // //获取触发当前方法（OnActionExecuting）的Action名字（即：哪个Action方法被执行的时候触发的OnActionExecuting(ActionExecutingContext filterContext)）  
            // string actionName = filterContext.ActionDescriptor.ActionName;

            // //获取触发当前方法的的Action所在的控制器名字  
            // string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            // //获取触发当前方法的Action方法的所有参数（因为参数可能有多个，所以它是一个集合,它的返回值类型是IDictionary<string ,object> 下面为了好看，用var替代）  
            // var paramss = filterContext.ActionParameters;

            // string str = "";
            // if (paramss.Any()) //Any是判断这个集合是否包含任何元素，如果包含元素返回true，否则返回false  
            // {
            //     foreach (var key in paramss.Keys) //遍历它的键；(因为我们要获取的是参数的名称s,所以遍历键)  
            //     {
            //         str = key + "的值是" + paramss[key];  //paramss[key] 是key的值  
            //     }
            // }



            // //获取当前请求的上下文  
            // filterContext.HttpContext.Response.Write("你好，我也好");


            // //将触发当前方法的这个Action方法的返回结果视图换成一个JsonResult  （ filterContext.Result的返回类型就是JsonResult）  

            // //filterContext.Result:获取或设置由操作方法返回的结果。(既然是获取或者设置Action方法的返回结果，那么我们就可以在这里篡改触发当前方法的那个Action方法的返回结果  

            // //例如：触发当前方法的Action方法是这个:public ActionResult Add(){return Content("中国");} 这个Action方法的返回值是一个"中国"文本  那么我们在这里可以通过filterContext.Result来篡改它的返回值。比如这我给他返回一个json  

            // //JsonResult json = new JsonResult();
            // //json.Data = new { status = "1", message = "OK" };
            // //json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            // //filterContext.Result = json;


            // //将触发当前方法的这个Action方法的返回结果视图换成一个另外一个Action  
            //// filterContext.Result = new RedirectResult("/Login/Index");



            // //假设我们在MVC项目中添加一个名字为admin的区域，然后再区域下添加一个Home控制器，然后添加一个Index视图。  
            // //那现在我们访问这个视图的路径就是:http://localhost:5219/admin/home/index  
            // //获取区域  
            // var area = filterContext.RouteData.DataTokens;//MVC可以有区域的，这里就是负责存放区域的  

            // //获取区域名称  
            // var areaName = area["area"];//这个["area"]是写死了的。你根据["area"]就可以取到区域名称，因为区域的key固定就是area  所以这里areaName的值为admin  


            // //RouteData  
            // var rd = filterContext.RouteData; //在这里面可以获取控制名称，ation名称，参数名称  

            // var controlName = rd.Values["Controller"].ToString();
            // var actName = rd.Values["Action"].ToString();
        }
    }
}
