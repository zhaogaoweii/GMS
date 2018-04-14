<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenDoc.aspx.cs" Inherits="GenDoc" %>

<%@ Register Assembly="PageOffice, Version=2.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>动态生成文件</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
	    <script type="text/javascript">
	        function poPrint() {
	            document.getElementById("PageOfficeCtrl1").ShowDialog(4);
	        }
	        function poSetFullScreen() {
	            document.getElementById("PageOfficeCtrl1").FullScreen = !document.getElementById("PageOfficeCtrl1").FullScreen;
	        }
	    </script>
		<form id="Form1" method="post" runat="server">
		<div style=" width:1000px; height:1000px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" Menubar="False" 
            OfficeToolbars="False" Theme="Office2007">
        </po:PageOfficeCtrl>
        </div>
		</form>
	</body>
</HTML>

