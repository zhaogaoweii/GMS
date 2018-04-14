<%@ Page Language="C#" AutoEventWireup="true" CodeFile="word.aspx.cs" Inherits="word" %>

<%@ Register Assembly="PageOffice, Version=2.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改有痕迹</title>
    <link href="images/csstg.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function Save() {
            document.getElementById("PageOfficeCtrl1").WebSave();
        }

        //显示痕迹
        function ShowRevisions() {
            document.getElementById("PageOfficeCtrl1").ShowRevisions = true;
        }

        //隐藏痕迹
        function HiddenRevisions() {
            document.getElementById("PageOfficeCtrl1").ShowRevisions = false;
        }
        
        //领导圈阅签字
        function StartHandDraw() {
            document.getElementById("PageOfficeCtrl1").HandDraw.SetPenWidth(5);
            document.getElementById("PageOfficeCtrl1").HandDraw.Start();
        }
        // 插入键盘批注
        function StartRemark() {
            var appObj = document.getElementById("PageOfficeCtrl1").Document.Application;
            appObj.Selection.Comments.Add(appObj.Selection.Range);
        }
        //分层显示手写批注
        function ShowHandDrawDispBar() {
            document.getElementById("PageOfficeCtrl1").HandDraw.ShowLayerBar(); ;
        }

        //全屏/还原
        function IsFullScreen() {
            document.getElementById("PageOfficeCtrl1").FullScreen = !document.getElementById("PageOfficeCtrl1").FullScreen;
        }

        //显示标题
        function ShowTitle() {
            alert("该菜单的标题是：" + document.getElementById("PageOfficeCtrl1").caption);
        }

        //插入电子印章
        function InsertSeal() {
            //document.getElementById("PageOfficeCtrl1").InsertSealFromURL("images/seal02.esf");//不通过选择，直接插入指定印章
            var mDialogUrl = "images/selectSeal.htm";
            var mObject = new Object();
            mObject.SelectValue = "";
            window.showModalDialog(mDialogUrl, mObject, "dialogHeight:180px; dialogWidth:340px;center:yes;scroll:no;status:no;");
            //判断用户是否选择印章
            if (mObject.SelectValue != "") {
                document.getElementById("PageOfficeCtrl1").InsertSealFromURL("images/" + mObject.SelectValue);
            }
        }

        //获取并显示所有痕迹
        function jsGetAllRevisions() {
            var i;
            var str = "";
            for (i = 1; i <= document.getElementById("PageOfficeCtrl1").Document.Revisions.Count; i++) {
                str = str + document.getElementById("PageOfficeCtrl1").Document.Revisions.Item(i).Author;
                if (document.all("PageOfficeCtrl1").Document.Revisions.Item(i).Type == "1") {
                    str = str + ' 插入：' + document.getElementById("PageOfficeCtrl1").Document.Revisions.Item(i).Range.Text + "\r\n";
                }
                else if (document.all("PageOfficeCtrl1").Document.Revisions.Item(i).Type == "2") {
                    str = str + ' 删除：' + document.getElementById("PageOfficeCtrl1").Document.Revisions.Item(i).Range.Text + "\r\n";
                }
                else {
                    str = str + ' 调整格式或样式。';
                }
            }
            alert("当前文档的所有修改痕迹如下：\r\n" + str);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
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
                            </span>&nbsp;&nbsp;<strong>流转给：</strong>
                <img alt="流转" src="images/arrow2.gif" border="0" />&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkBtn" runat="server" OnClick="LinkBtn_Click"><asp:Literal ID="Literal_Lz" runat="server"></asp:Literal></asp:LinkButton>
                <span style="color: Red;"></span>
            </div>
            <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" CustomToolbar="True">
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
