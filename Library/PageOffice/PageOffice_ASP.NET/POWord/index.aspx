﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" type="text/css" href="css/style.css">
<link href="images/csstg.css" type="text/css" rel="stylesheet"/>
<!--[if lte IE 6]>
<script type="text/javascript" src="js/belatedPNG.js"></script>
<script type="text/javascript">
  DD_belatedPNG.fix('.png_bg,.png_bg a:hover,img,li');
</script>
<![endif]-->
<title>在线演示-请假条示例</title>
</head>
<body>
<script type="text/javascript">
    function onColor(td) {
        td.style.backgroundColor = '#D7FFEE';
    }
    function offColor(td) {
        td.style.backgroundColor = '';
    }
    function SetLinkUrl(svrpage, fileid) {
        //location.href = svrpage+'?ID='+fileid;
        window.open(svrpage + '?ID=' + fileid);
    }
    function SetLinkUrl2(svrpage, fileid) {
        location.href = svrpage + '?ID=' + fileid;
        //window.open(svrpage+'?ID='+fileid);
    }
    function openDataList(svrpage, fileid) {
        window.open(svrpage + '?ID=' + fileid, "", "fullscreen=0,toolbar=0,location=1,directories=0,status=0,menubar=0,scrollbars=1,resizable=0,width=" + 500 + ",height=" + 320 + ",top=200,left=100", true);
    }
</script>
<!--header-->
<div class="zz-headBox br-5 clearfix">
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

<form name="form1" id="form1" action="index.aspx"  method="post" runat="server">
<input id="FileSubject" name="FileSubject" type="hidden" />
<input id="TemplateName" name="TemplateName" type="hidden" />
</form>


<div id="content">

<div style=" margin:25px 0; font-size:20px; font-weight:bold;"> PageOffice 使用模板生成文件</div>
<div id="textcontent"> 
    <p><strong>演示说明: </strong> </p>
    <div class="flow1">此示例演示：读取数据库中的数据填充模板文件，动态生成文件的效果。</div>							
    <div class="flow1">
      <ul style=" margin-left:0;">
            <li>提示1：正确运行本演示前，您首先要确定您的机器上已安装MS Word。
            </li><li>
			提示2：如果您使用的是 <b><font color="#FF0000"><u>vista</u> 、 <u>windows 7</u></font></b> 操作系统，请添加 http://<%=Request.ServerVariables["SERVER_NAME"]%> 到可信站点。
			</li>
      </ul> 
    </div>
    <div class="flow5"><img src="images/img_f_34.gif" />  </div>   
</div>


<div class="flow1">
<table  class="zz-talbe"  style=" width:98%;">
  <caption style="font-size:12px;">请假条列表</caption>
  <thead>
  <tr>
    <th width="50">类型</th>
    <th width="370">文档名称</th>
    <th width="80">创建日期</th>
    <th width="200">操作</th>
    </thead>

  <tbody>
   <asp:Literal id="LiteralGrid" runat="server"></asp:Literal>
   </tbody></table></div>
 </div>   
   

<!--footer-->
<div class="login-footer clearfix">Copyright &copy 2012 北京卓正志远软件有限公司</div>
<!--footer end-->
</body>
</html>
