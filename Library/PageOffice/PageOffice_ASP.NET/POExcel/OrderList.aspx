<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderList.aspx.cs" Inherits="OrderList"
    Debug="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/style2.css" rel="stylesheet" type="text/css" />
    <!--[if lte IE 6]>
<script type="text/javascript" src="js/belatedPNG.js"></script>
<script type="text/javascript">
  DD_belatedPNG.fix('.png_bg,.png_bg a:hover,img,li');
</script>
<![endif]-->
    <title>订单列表</title>

    <script type="text/javascript">
        function Delete(id) {
            if (confirm("你确认要删除吗？")) {
                location.href = "OrderList.aspx?op=del&ID=" + id;
                return true;
            }
            else {
                return false;
            }
        }
    </script>

</head>
<body>
    <!--header-->
    <div class="zz-headBox clearfix">
        <div class="zz-head mc">
            <!--logo-->
            <div class="logo fl">
                <a href="#">
                    <img src="images/logo.png" alt="" /></a></div>
            <!--logo end-->
            <ul class="head-rightUl fr">
                <li><a href="http://www.zhuozhengsoft.com" target="_blank">卓正网站</a></li>
                <li><a href="http://www.zhuozhengsoft.com/poask/index.asp" target="_blank">客户问吧</a></li>
                <li class="bor-0"><a href="http://www.zhuozhengsoft.com/contact-us.html" target="_blank">联系我们</a></li>
            </ul>
        </div>
    </div>
    <!--header end-->
    <!--a title-->
    <div class=" topTitle">
        <ul>
            <li class="pd-left">销售订单管理系统示例</li>
            <li><font>当前用户：</font>admin</li>
            <li><font>当前系统日期：</font><asp:Literal ID="LitDate" runat="server"></asp:Literal></li>
            <li><font>当前模块：</font>订单列表</li>
        </ul>
    </div>
    <!--content-->
    <div class="zz-content mc clearfix pd-28">
        <!--left-->
        <div class="zz-contentLeft fl">
            <ul class="left-ul">
                <h2 class="fs-12">
                    用户功能区</h2>
                <li style="background: #d0eaf7; display: block;"><a href="OrderList.aspx">订单列表</a></li>
                <li><a href="NewOrder.aspx">新建订单</a></li>
                <li><a href="OrderStat.aspx">统计图表</a></li>
                <li><a href="OrderStat2.aspx">查询表</a></li>
                <li class="bo-n"><a href="logout.aspx">退出系统</a></li>
            </ul>
        </div>
        <div class="zz-contentRight fl">
            <!--表格内容-->
            <table class="zz-talbe">
                <thead>
                    <tr>
                        <th class="text-cent" style="width: 15%">
                            订单编号
                        </th>
                        <th class="text-cent" style="width: 11%">
                            日期
                        </th>
                        <th class="text-cent">
                            客户名称
                        </th>
                        <th class="text-cent" style="width: 9%">
                            业务员
                        </th>
                        <th class="text-cent" style="width: 12%">
                            订单金额
                        </th>
                        <th class="text-cent" style="width: 26%">
                            操作
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </tbody>
            </table>
        </div>
        <!--内容区-->
    </div>
    <!--content end-->
    <!--footer-->
    <div class="login-footer clearfix">
        Copyright &copy 2012 北京卓正志远软件有限公司</div>
    <!--footer end-->
    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
</body>
</html>
