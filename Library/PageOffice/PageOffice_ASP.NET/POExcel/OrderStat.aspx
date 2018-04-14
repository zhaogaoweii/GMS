﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderStat.aspx.cs" Inherits="OrderStat" %>

<%@ Register Assembly="PageOffice, Version=2.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
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
    <title>统计图表</title>

    <script language="javascript">
        //打印
        function Print() {
            document.getElementById("PageOfficeCtrl1").ShowDialog(4);
        }

        //打印预览
        function PrintPreView() {
            document.getElementById("PageOfficeCtrl1").PrintPreview();
        }

        //页面设置
        function SetPage() {
            document.getElementById("PageOfficeCtrl1").ShowDialog(5);
        }

        //另存到本机
        function StoreAs() {
            document.getElementById("PageOfficeCtrl1").ShowDialog(2);
        }

        //全屏/还原
        function SetScreen() {
            document.getElementById("PageOfficeCtrl1").FullScreen = !document.getElementById("PageOfficeCtrl1").FullScreen;
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
            <li><font>当前模块：</font>统计图表</li>
        </ul>
    </div>
    <!--content-->
    <div class="zz-content mc clearfix pd-28">
        <!--left-->
        <div class="zz-contentLeft fl">
            <ul class="left-ul">
                <h2 class="fs-12">
                    用户功能区</h2>
                <li><a href="OrderList.aspx">订单列表</a></li>
                <li><a href="NewOrder.aspx">新建订单</a></li>
                <li style="background: #d0eaf7; display: block;"><a href="OrderStat.aspx">统计图表</a></li>
                <li><a href="OrderStat2.aspx">查询表</a></li>
                <li class="bo-n"><a href="logout.aspx">退出系统</a></li>
            </ul>
        </div>
        <div class="zz-contentRight fl">
            <div style="width: 890px; height: 600px;">
                <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" CustomToolbar="True" OfficeToolbars="False"
                    Titlebar="True" Menubar="False" OnLoad="PageOfficeCtrl1_Load" 
                    Theme="Office2010">
                </po:PageOfficeCtrl>
            </div>
        </div>
        <!--内容区-->
    </div>
    <!--content end-->
    <!--footer-->
    <div class="login-footer clearfix">
        Copyright © 2012 北京卓正志远软件有限公司</div>
    <!--footer end-->
</body>
</html>
