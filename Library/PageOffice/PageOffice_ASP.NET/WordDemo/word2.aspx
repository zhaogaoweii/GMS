<%@ Page Language="C#" AutoEventWireup="true" CodeFile="word2.aspx.cs" Inherits="word2" %>

<%@ Register Assembly="PageOffice, Version=2.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改无痕迹</title>
    <link href="images/csstg.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form2" runat="server">
    <div id="header">
        <div style="float: left; margin-left: 20px;">
            <img src="images/logo.jpg" height="30" /></div>
        <ul>
            <li><a target="_blank" href="http://www.zhuozhengsoft.com">卓正网站</a></li>
            <li><a target="_blank" href="http://www.zhuozhengsoft.com/poask/index.asp">客户问吧</a></li>
            <li><a href="#">在线帮助</a></li>
            <li><a target="_blank" href="http://www.zhuozhengsoft.com/contact-us.html">联系我们</a></li>
        </ul>
    </div>
    <div id="content">
        <div id="textcontent" style="width: 1000px; height: 800px;">
            <div class="flow4" >
                <a href="index.aspx">
                    <img alt="返回" src="images/return.gif" border="0" />文件列表</a> <span style="width: 100px;">
                    </span><strong>文档名称：</strong> <span style="color: Red;">
                        <asp:Literal ID="Literal_Subject" runat="server"></asp:Literal></span> <span style="width: 100px;">
                        </span><strong>当前流程：</strong> <span style="color: Red;">
                            <asp:Literal ID="Literal_Lc" runat="server"></asp:Literal></span> <span style="width: 100px;">
                            </span>&nbsp;&nbsp;<strong>流转给：</strong>
                <img alt="流转" src="images/arrow2.gif" border="0" />&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkBtn" runat="server" OnClick="LinkBtn_Click"><asp:Literal ID="Literal_Lz" runat="server"></asp:Literal></asp:LinkButton>
                <span style="color: Red;"></span>
            </div>
            <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" CustomToolbar="False" 
                Menubar="True">
            </po:PageOfficeCtrl>
        </div>
    </div>
    <div id="footer">
        <hr width="1000" />
        <div>
            Copyright (c) 2012 北京卓正志远软件有限公司</div>
    </div>
    </form>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</body>
</html>
