﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="excel.aspx.cs" Inherits="excel3" %>

<%@ Register Assembly="PageOffice, Version=2.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>演示：用程序生成Excel表格</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style=" text-align:center; color:Red;">演示：完全使用程序生成Excel表格</div>
    <div style=" width:1000px; height:700px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" CustomToolbar="False" 
            Menubar="False" Theme="Office2010">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
