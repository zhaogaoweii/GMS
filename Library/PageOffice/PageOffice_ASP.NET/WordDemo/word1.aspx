<%@ Page Language="C#" AutoEventWireup="true" CodeFile="word1.aspx.cs" Inherits="Word_FileTransfer_word1" %>

<%@ Register Assembly="PageOffice, Version=2.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>核稿模式</title>
    <link href="images/csstg.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        // $ = function(id) { return (typeof (id) == 'object') ? id : document.getElementById(id); };
        //        function Save() {
        //            $("PageOfficeCtrl1").WebSave();
        //        }

        function Save() {
            document.getElementById("PageOfficeCtrl1").WebSave();
        }

        function ShowRevisions() {
            document.getElementById("PageOfficeCtrl1").ShowRevisions = true;
        }
        function HiddenRevisions() {
            document.getElementById("PageOfficeCtrl1").ShowRevisions = false;
        }
        function Show_HidRevisions() {
            document.getElementById("PageOfficeCtrl1").ShowRevisions = !document.getElementById("PageOfficeCtrl1").ShowRevisions;
        }

        //领导圈阅签字
        function StartHandDraw() {
            document.getElementById("PageOfficeCtrl1").HandDraw.SetPenWidth(5);
            document.getElementById("PageOfficeCtrl1").HandDraw.Start();
        }

        //接受所有修订
        function AcceptAllRevisions() {
            document.getElementById("PageOfficeCtrl1").AcceptAllRevisions();
        }

        //分层显示手写批注
        function ShowHandDrawDispBar() {
            document.getElementById("PageOfficeCtrl1").HandDraw.ShowLayerBar();
        }

        //全屏/还原
        function IsFullScreen() {
            document.getElementById("PageOfficeCtrl1").FullScreen = !document.getElementById("PageOfficeCtrl1").FullScreen;
        }

        //显示菜单
        function ShowTitle() {
            alert("该菜单的标题是：" + document.getElementById("PageOfficeCtrl1").caption);
        }

        //插入电子印章
        function InsertSeal() {
            alert("请使用此用户的印章测试\r\n用户名：李志 \r\n初始密码：111111");

            var zoomseal = document.getElementById("PageOfficeCtrl1").ZoomSeal;
            if (zoomseal != null)
                zoomseal.AddSeal();


        }
        // 签批
        function InsertHandSign() {
            alert("请使用此用户测试\r\n用户名：李志 \r\n初始密码：111111");

            var zoomseal = document.getElementById("PageOfficeCtrl1").ZoomSeal;
            if (zoomseal != null)
                zoomseal.AddHandSign();
        }

        //文档另存为Html，并发布到web服务器
        function SaveAsHtml() {
            document.getElementById("PageOfficeCtrl1").WebSaveAsMHT();
            window.open("htmldoc.aspx?type=word&ID=<%=DocID%>");
        }
      
    </script>

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
            <div class="flow4">
                <a href="index.aspx">
                    <img alt="返回" src="images/return.gif" border="0" />文件列表</a> <span style="width: 100px;">
                    </span><strong>文档名称：</strong> <span style="color: Red;">
                        <asp:Literal ID="Literal_Subject" runat="server"></asp:Literal></span> <span style="width: 100px;">
                        </span><strong>当前流程：</strong> <span style="color: Red;">
                            <asp:Literal ID="Literal_Lc" runat="server"></asp:Literal></span> <span style="width: 100px;">
                            </span>&nbsp;&nbsp;<strong>流转：</strong>
                <img alt="流转" src="images/arrow2.gif" border="0" />&nbsp;&nbsp;&nbsp;<asp:LinkButton
                    ID="LinkBtn" runat="server" OnClick="LinkBtn_Click">
                    <asp:Literal ID="Literal_Lz" runat="server"></asp:Literal></asp:LinkButton>
                <span style="color: Red;"></span>
            </div>
            <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server">
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