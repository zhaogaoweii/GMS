using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ZGW.GMS.Core.Mvc.ModelBinders
{
    //public class PagingInfoModelBinder : IModelBinder
    //{
    //    private const string pageSizeRouteDataName = "pageSize";
    //    private const string pageNumberRouteDataName = "pageNumber";

    //    public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    //    {
    //        var valueProvider = bindingContext.ValueProvider;

    //        int size = 10;
    //        int index = 0;

    //        string pageSize = valueProvider.GetAttemptedValue(pageSizeRouteDataName);
    //        string pageNumber = valueProvider.GetAttemptedValue(pageNumberRouteDataName);

    //        if (!pageSize.IsNullOrWhiteSpace())
    //        {
    //            int.TryParse(pageSize, out size);
    //            //如果非法输入每页显示数，重置为10
    //            if (size < 1)
    //                size = 10;
    //        }

    //        if (!pageNumber.IsNullOrWhiteSpace() && int.TryParse(pageNumber, out index))
    //        {
    //            index--;
    //            //如果非法输入页数，重置为0
    //            if (index < 0)
    //                index = 0;
    //        }

    //        return new PagingInfo(index, size);
    //    }
    //}
}
