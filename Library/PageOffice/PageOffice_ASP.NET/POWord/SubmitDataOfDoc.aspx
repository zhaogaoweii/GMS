<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubmitDataOfDoc.aspx.cs" Inherits="SubmitDataOfDoc" %>

<%@ Register Assembly="PageOffice, Version=2.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>用户填写请假条</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
	    <script language="JavaScript" >
	        function poSave() {
	            document.getElementById("PageOfficeCtrl1").WebSave();
	        }
	        function poSetFullScreen() {
	            document.getElementById("PageOfficeCtrl1").FullScreen = !document.getElementById("PageOfficeCtrl1").FullScreen;
	        }
	        function OnWordDataRegionClick(Name, Value, Left, Bottom) {
	            if (Name == "PO_date") {
	                dv = window.showModalDialog("datetimer.htm", "44", "center:1;help:no;status:no;dialogLeft:" + Left + ";dialogTop:" + Bottom + ";dialogHeight:246px;dialogWidth:216px;scroll:no")
	                if (dv) { if (dv == "null") obj.value = ''; else return dv; }
	            }
	            if (Name == "PO_dept") {
	                var mObject = new Object();
	                mObject.SelectValue = "";
	                window.showModalDialog("selectDept.htm", mObject, "dialogLeft:" + Left + "px; dialogTop:" + Bottom + "px; dialogHeight:120px; dialogWidth:200px;center:no;scroll:no;status:no;");
	                if (mObject.SelectValue != "") {
	                    return (mObject.SelectValue);
	                }
	            }
	        }
	    </script>
		<form id="Form1" method="post" runat="server">

        <!--   PageOffice 客户端引用    -->
        <div style=" width:1000px; height:1000px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" Menubar="False" 
                OfficeToolbars="False" Theme="Office2010">
        </po:PageOfficeCtrl>
        </div>   
		</form>
	</body>
</HTML>
