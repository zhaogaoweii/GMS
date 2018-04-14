using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Ajax;
using ZGW.GMS.Core;

namespace ZGW.GMS.Core.Mvc.Html
{
    /// <summary>
    /// 分页的的扩展方法
    /// </summary>
    public static class PagerHelper
    {
        #region Html Pager
        /// <summary>
        /// 分页扩展方法
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="totalItemCount">总记录数</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="actionName">Action</param>
        /// <param name="controllerName">Controller</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="routeValues">路由值</param>
        /// <param name="htmlAttributes">HtmlAttributes</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString Pager(this HtmlHelper helper, long totalItemCount, int pageSize, int pageIndex, string actionName, string controllerName,
            PagerOptions pagerOptions, string routeName, object routeValues, object htmlAttributes)
        {
            var totalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
            var builder = new PagerBuilder
                (
                    helper,
                    actionName,
                    controllerName,
                    totalPageCount,
                    pageIndex,
                    pagerOptions,
                    routeName,
                    new RouteValueDictionary(routeValues),
                    new RouteValueDictionary(htmlAttributes)
                );
            return builder.RenderPager();
        }

        /// <summary>
        /// 分页扩展方法
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="totalItemCount">总记录数</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="actionName">Action</param>
        /// <param name="controllerName">Controller</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="routeValues">路由值</param>
        /// <param name="htmlAttributes">HtmlAttributes</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString Pager(this HtmlHelper helper, long totalItemCount, int pageSize, int pageIndex, string actionName, string controllerName,
            PagerOptions pagerOptions, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            var totalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
            var builder = new PagerBuilder
                (
                    helper,
                    actionName,
                    controllerName,
                    totalPageCount,
                    pageIndex,
                    pagerOptions,
                    routeName,
                    routeValues,
                    htmlAttributes
                );
            return builder.RenderPager();
        }

        /// <summary>
        /// 分页扩展方法
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="htmlAttributes">HtmlAttributes</param>
        /// <returns>MvcHtmlString</returns>
        private static MvcHtmlString Pager(HtmlHelper helper, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes)
        {
            return new PagerBuilder(helper, null, pagerOptions, htmlAttributes).RenderPager();
        }

        /// <summary>
        /// 分页扩展方法
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pagedList">分页数据</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList)
        {
            if (pagedList == null)
                return Pager(helper, (PagerOptions)null, null);
            return Pager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, null, null, null);
        }

        /// <summary>
        /// 分页扩展方法
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions)
        {
            if (pagedList == null)
                return Pager(helper, pagerOptions, null);
            return Pager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, pagerOptions, null, null, null);
        }

        /// <summary>
        /// 分页扩展方法
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="htmlAttributes">HtmlAttributes</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, object htmlAttributes)
        {
            if (pagedList == null)
                return Pager(helper, pagerOptions, new RouteValueDictionary(htmlAttributes));
            return Pager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, pagerOptions, null, null, htmlAttributes);
        }

        /// <summary>
        /// 分页扩展方法
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="htmlAttributes">HtmlAttributes</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
                return Pager(helper, pagerOptions, htmlAttributes);
            return Pager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, pagerOptions, null, null, htmlAttributes);
        }

        /// <summary>
        /// 分页扩展方法
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="routeValues">路由值</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, string routeName, object routeValues)
        {
            if (pagedList == null)
                return Pager(helper, pagerOptions, null);
            return Pager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, pagerOptions, routeName, routeValues, null);
        }

        /// <summary>
        /// 分页扩展方法
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="routeValues">路由值</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, string routeName, RouteValueDictionary routeValues)
        {
            if (pagedList == null)
                return Pager(helper, pagerOptions, null);
            return Pager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, pagerOptions, routeName, routeValues, null);
        }

        /// <summary>
        /// 分页扩展方法
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="routeValues">路由值</param>
        /// <param name="htmlAttributes">HtmlAttribute</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, string routeName, object routeValues, object htmlAttributes)
        {
            if (pagedList == null)
                return Pager(helper, pagerOptions, new RouteValueDictionary(htmlAttributes));
            return Pager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, pagerOptions, routeName, routeValues, htmlAttributes);
        }

        /// <summary>
        /// 分页扩展方法
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="routeValues">路由值</param>
        /// <param name="htmlAttributes">HtmlAttribute</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, string routeName,
            RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
                return Pager(helper, pagerOptions, htmlAttributes);
            return Pager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, pagerOptions, routeName, routeValues, htmlAttributes);
        }

        /// <summary>
        /// 分页扩展方法
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="routeValues">路由值</param>
        /// <param name="htmlAttributes">HtmlAttribute</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, string routeName, object routeValues, object htmlAttributes)
        {
            if (pagedList == null)
                return Pager(helper, null, new RouteValueDictionary(htmlAttributes));
            return Pager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, routeName, routeValues, htmlAttributes);
        }

        /// <summary>
        /// 分页扩展方法
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="routeValues">路由值</param>
        /// <param name="htmlAttributes">HtmlAttribute</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, string routeName, RouteValueDictionary routeValues,
            IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
                return Pager(helper, null, htmlAttributes);
            return Pager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, routeName, routeValues, htmlAttributes);
        }

        #endregion

        #region jQuery Ajax Pager

        private static MvcHtmlString AjaxPager(HtmlHelper html, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes)
        {
            return new PagerBuilder(html, null, pagerOptions, htmlAttributes).RenderPager();
        }

        /// <summary>
        /// Ajax分页扩展方法
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="totalItemCount">总记录数</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="actionName">Action</param>
        /// <param name="controllerName">Controller</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="routeValues">路由值</param>
        /// <param name="ajaxOptions">Ajax参数</param>
        /// <param name="htmlAttributes">Html参数</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString AjaxPager(this HtmlHelper html, long totalItemCount, int pageSize, int pageIndex, string actionName, string controllerName,
            string routeName, PagerOptions pagerOptions, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            if (pagerOptions == null)
                pagerOptions = new PagerOptions();
            pagerOptions.UseJqueryAjax = true;

            var totalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
            var builder = new PagerBuilder(html, actionName, controllerName, totalPageCount, pageIndex, pagerOptions,
                                           routeName, new RouteValueDictionary(routeValues), ajaxOptions, new RouteValueDictionary(htmlAttributes));
            return builder.RenderPager();
        }

        /// <summary>
        /// Ajax分页扩展方法
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="totalItemCount">总记录数</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="actionName">Action</param>
        /// <param name="controllerName">Controller</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="routeValues">路由值</param>
        /// <param name="ajaxOptions">Ajax参数</param>
        /// <param name="htmlAttributes">Html参数</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString AjaxPager(this HtmlHelper html, long totalItemCount, int pageSize, int pageIndex, string actionName, string controllerName,
            string routeName, PagerOptions pagerOptions, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            if (pagerOptions == null)
                pagerOptions = new PagerOptions();
            pagerOptions.UseJqueryAjax = true;

            var totalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
            var builder = new PagerBuilder(html, actionName, controllerName, totalPageCount, pageIndex, pagerOptions,
                                           routeName, routeValues, ajaxOptions, htmlAttributes);
            return builder.RenderPager();
        }

        /// <summary>
        /// Ajax分页扩展方法
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="ajaxOptions">Ajax参数</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, AjaxOptions ajaxOptions)
        {
            if (pagedList == null)
                return AjaxPager(html, (PagerOptions)null, null);
            return AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, null, null, ajaxOptions,
                             null);
        }

        /// <summary>
        /// Ajax分页扩展方法
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="ajaxOptions">Ajax参数</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, string routeName, AjaxOptions ajaxOptions)
        {
            if (pagedList == null)
                return AjaxPager(html, (PagerOptions)null, null);
            return AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, routeName, null, null, ajaxOptions,
                             null);
        }

        /// <summary>
        /// Ajax分页扩展方法
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="ajaxOptions">Ajax参数</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, PagerOptions pagerOptions, AjaxOptions ajaxOptions)
        {
            if (pagedList == null)
                return AjaxPager(html, pagerOptions, null);
            return AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, pagerOptions, null, ajaxOptions,
                             null);
        }

        /// <summary>
        /// Ajax分页扩展方法
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="ajaxOptions">Ajax参数</param>
        /// <param name="htmlAttributes">Html参数</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, PagerOptions pagerOptions, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            if (pagedList == null)
                return AjaxPager(html, pagerOptions, new RouteValueDictionary(htmlAttributes));
            return AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, pagerOptions, null,
                             ajaxOptions, htmlAttributes);
        }

        /// <summary>
        /// Ajax分页扩展方法
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="ajaxOptions">Ajax参数</param>
        /// <param name="htmlAttributes">Html参数</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, PagerOptions pagerOptions, AjaxOptions ajaxOptions,
            IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
                return AjaxPager(html, pagerOptions, htmlAttributes);
            return AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, pagerOptions, null,
                             ajaxOptions, htmlAttributes);
        }

        /// <summary>
        /// Ajax分页扩展方法
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="routeValues">路由值</param>
        /// <param name="ajaxOptions">Ajax参数</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, string routeName, object routeValues, PagerOptions pagerOptions, AjaxOptions ajaxOptions)
        {
            if (pagedList == null)
                return AjaxPager(html, pagerOptions, null);
            return AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, routeName, pagerOptions, routeValues, ajaxOptions,
                             null);
        }

        /// <summary>
        /// Ajax分页扩展方法
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="routeValues">路由值</param>
        /// <param name="ajaxOptions">Ajax参数</param>
        /// <param name="htmlAttributes">Html参数</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, string routeName, object routeValues,
            PagerOptions pagerOptions, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            if (pagedList == null)
                return AjaxPager(html, pagerOptions, new RouteValueDictionary(htmlAttributes));
            return AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, routeName, pagerOptions,
                             routeValues, ajaxOptions, htmlAttributes);
        }

        /// <summary>
        /// Ajax分页扩展方法
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="routeValues">路由值</param>
        /// <param name="ajaxOptions">Ajax参数</param>
        /// <param name="htmlAttributes">Html参数</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, string routeName, RouteValueDictionary routeValues,
            PagerOptions pagerOptions, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
                return AjaxPager(html, pagerOptions, htmlAttributes);
            return AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, routeName, pagerOptions,
                             routeValues, ajaxOptions, htmlAttributes);
        }

        /// <summary>
        /// Ajax分页扩展方法
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="ajaxOptions">参数</param>
        /// <param name="controllerName">Controller</param>
        /// <param name="actionName">Action</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, string actionName, string controllerName,
            PagerOptions pagerOptions, AjaxOptions ajaxOptions)
        {
            if (pagedList == null)
                return AjaxPager(html, pagerOptions, null);
            return AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, actionName, controllerName, null, pagerOptions, null, ajaxOptions,
                             null);
        }

        #endregion

        #region Microsoft Ajax Pager

        /// <summary>
        /// 基于AjaxHelper的分页扩展方法
        /// </summary>
        /// <param name="ajax">AjaxHelper</param>
        /// <param name="totalItemCount">总记录数</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="actionName">Action</param>
        /// <param name="controllerName">Controller</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="routeValues">路由值</param>
        /// <param name="ajaxOptions">Ajax参数</param>
        /// <param name="htmlAttributes">Html参数</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString Pager(this AjaxHelper ajax, long totalItemCount, int pageSize, int pageIndex, string actionName, string controllerName,
            string routeName, PagerOptions pagerOptions, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var totalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
            var builder = new PagerBuilder(ajax, actionName, controllerName, totalPageCount, pageIndex, pagerOptions,
                                           routeName, new RouteValueDictionary(routeValues), ajaxOptions, new RouteValueDictionary(htmlAttributes));
            return builder.RenderPager();
        }

        /// <summary>
        /// 基于AjaxHelper的分页扩展方法
        /// </summary>
        /// <param name="ajax">AjaxHelper</param>
        /// <param name="totalItemCount">总记录数</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="actionName">Action</param>
        /// <param name="controllerName">Controller</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="routeValues">路由值</param>
        /// <param name="ajaxOptions">Ajax参数</param>
        /// <param name="htmlAttributes">Html参数</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString Pager(this AjaxHelper ajax, long totalItemCount, int pageSize, int pageIndex, string actionName, string controllerName,
            string routeName, PagerOptions pagerOptions, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            var totalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
            var builder = new PagerBuilder(ajax, actionName, controllerName, totalPageCount, pageIndex, pagerOptions,
                                           routeName, routeValues, ajaxOptions, htmlAttributes);
            return builder.RenderPager();
        }

        /// <summary>
        /// 基于AjaxHelper的分页扩展方法
        /// </summary>
        /// <param name="ajax">AjaxHelper</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="htmlAttributes">Html参数</param>
        /// <returns>MvcHtmlString</returns>
        private static MvcHtmlString Pager(AjaxHelper ajax, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes)
        {
            return new PagerBuilder(null, ajax, pagerOptions, htmlAttributes).RenderPager();
        }

        /// <summary>
        /// 基于AjaxHelper的分页扩展方法
        /// </summary>
        /// <param name="ajax">AjaxHelper</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="ajaxOptions">ajax参数</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, AjaxOptions ajaxOptions)
        {
            return pagedList == null ? Pager(ajax, (PagerOptions)null, null) : Pager(ajax, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, null, null, ajaxOptions, null);
        }

        /// <summary>
        /// 基于AjaxHelper的分页扩展方法
        /// </summary>
        /// <param name="ajax">AjaxHelper</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="ajaxOptions">ajax参数</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, PagerOptions pagerOptions, AjaxOptions ajaxOptions)
        {
            return pagedList == null ? Pager(ajax, pagerOptions, null) : Pager(ajax, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex,
                null, null, null, pagerOptions, null, ajaxOptions, null);
        }

        /// <summary>
        /// 基于AjaxHelper的分页扩展方法
        /// </summary>
        /// <param name="ajax">AjaxHelper</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="ajaxOptions">ajax参数</param>
        /// <param name="htmlAttributes">Html参数</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, PagerOptions pagerOptions, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            if (pagedList == null)
                return Pager(ajax, pagerOptions, new RouteValueDictionary(htmlAttributes));
            return Pager(ajax, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, pagerOptions, null, ajaxOptions, htmlAttributes);
        }

        /// <summary>
        /// 基于AjaxHelper的分页扩展方法
        /// </summary>
        /// <param name="ajax">AjaxHelper</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="ajaxOptions">ajax参数</param>
        /// <param name="htmlAttributes">Html参数</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, PagerOptions pagerOptions, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
                return Pager(ajax, pagerOptions, htmlAttributes);
            return Pager(ajax, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, null, pagerOptions, null, ajaxOptions, htmlAttributes);
        }

        /// <summary>
        /// 基于AjaxHelper的分页扩展方法
        /// </summary>
        /// <param name="ajax">AjaxHelper</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="routeValues">路由值</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="ajaxOptions">ajax参数</param>
        /// <param name="htmlAttributes">Html参数</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, string routeName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            if (pagedList == null)
                return Pager(ajax, null, new RouteValueDictionary(htmlAttributes));
            return Pager(ajax, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, routeName, null, routeValues, ajaxOptions, htmlAttributes);
        }

        /// <summary>
        /// 基于AjaxHelper的分页扩展方法
        /// </summary>
        /// <param name="ajax">AjaxHelper</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="routeValues">路由值</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="ajaxOptions">ajax参数</param>
        /// <param name="htmlAttributes">Html参数</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, string routeName, RouteValueDictionary routeValues,
            AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
                return Pager(ajax, null, htmlAttributes);
            return Pager(ajax, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, routeName, null, routeValues, ajaxOptions, htmlAttributes);
        }

        /// <summary>
        /// 基于AjaxHelper的分页扩展方法
        /// </summary>
        /// <param name="ajax">AjaxHelper</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="routeValues">路由值</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="ajaxOptions">ajax参数</param>
        /// <param name="htmlAttributes">Html参数</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, string routeName, object routeValues, PagerOptions pagerOptions,
            AjaxOptions ajaxOptions, object htmlAttributes)
        {
            if (pagedList == null)
                return Pager(ajax, pagerOptions, new RouteValueDictionary(htmlAttributes));
            return Pager(ajax, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, routeName, pagerOptions, routeValues, ajaxOptions, htmlAttributes);
        }

        /// <summary>
        /// 基于AjaxHelper的分页扩展方法
        /// </summary>
        /// <param name="ajax">AjaxHelper</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="routeValues">路由值</param>
        /// <param name="pagedList">分页数据</param>
        /// <param name="pagerOptions">分页参数</param>
        /// <param name="ajaxOptions">ajax参数</param>
        /// <param name="htmlAttributes">Html参数</param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, string routeName, RouteValueDictionary routeValues,
            PagerOptions pagerOptions, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
                return Pager(ajax, pagerOptions, htmlAttributes);
            return Pager(ajax, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null, null, routeName, pagerOptions, routeValues, ajaxOptions, htmlAttributes);
        }
        #endregion
    }
}
