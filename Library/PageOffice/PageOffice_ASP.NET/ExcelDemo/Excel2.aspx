<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Excel2.aspx.cs" Inherits="Excel2" %>

<%@ Register Assembly="PageOffice, Version=2.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" type="text/css" href="css/style.css">
<!--[if lte IE 6]>
<script type="text/javascript" src="js/belatedPNG.js"></script>
<script type="text/javascript">
  DD_belatedPNG.fix('.png_bg,.png_bg a:hover,img,li');
</script>
<![endif]-->
<title>只读打开文件</title>
    <script type="text/javascript">
        function saveAs() {
            document.getElementById("PageOfficeCtrl1").ShowDialog(2);
        }
        function setPrint() {
            document.getElementById("PageOfficeCtrl1").ShowDialog(5);
        }
        function setFullScreen() {
            document.getElementById("PageOfficeCtrl1").FullScreen = !document.getElementById("PageOfficeCtrl1").FullScreen;
        }
    </script>
</head>
<body>
<!--header-->
<div class="zz-headBox clearfix">
	<div class="zz-head mc">
    <!--logo-->
    	<div class="logo fl"><a href="#"><img src="images/logo.png" alt="" /></a></div>
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
    	<li class="pd-left"><a href="Default.aspx" style="color:White;"><font >返回文件列表</font></a></li>
        <li><font>当前模式：</font>只读模式</li>
        <li><font>当前系统日期：</font><%=DateTime.Now.ToString("yyyy年MM月dd日 dddd") %>  </li>
    </ul>
</div>
<!--content-->
<div class="zz-content mc clearfix pd-28">
    <form id="form1" runat="server">
    <div style=" height:700px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" Menubar="False" 
            OfficeToolbars="False" Theme="Office2010">
        </po:PageOfficeCtrl>
    </div>
    </form>
</div>
<!--content end-->
<!--footer-->
<div class="login-footer clearfix">Copyright &copy 2012 北京卓正志远软件有限公司</div>
<!--footer end-->
</body>
</html>
