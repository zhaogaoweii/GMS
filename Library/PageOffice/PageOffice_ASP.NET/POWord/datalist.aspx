<%@ Page Language="C#" AutoEventWireup="true" CodeFile="datalist.aspx.cs" Inherits="datalist" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>PageOffice 平台 演示程序</TITLE>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="images/csstg.css" type="text/css" rel="stylesheet"/>
		<link rel="stylesheet" type="text/css" href="css/style.css">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
		</form>
	
					<table width='98%' class="zz-talbe" style=" margin-top:20px; width:460px;" >
					    <thead>
						<tr>
							<th width='20%' height='26'  valign="middle">数据库字段</td>
							<th width='80%' height='23' valign="middle">字段值</td>
						</tr>
						</thead>
						<tr>
							<td width='20%' height='26'  valign="middle" style=" background-color:#D7FFEE">主题</td>
							<td width='80%' height='23' valign="middle">&nbsp;&nbsp;&nbsp;&nbsp;<%=docSubject%></td>
						</tr>
						<tr>
							<td width='20%' height='26'  valign="middle" style=" background-color:#D7FFEE">姓名</td>
							<td width='80%' height='23' valign="middle">&nbsp;&nbsp;&nbsp;&nbsp;<%=docName%></td>
						</tr>
						<tr>
							<td width='20%' height='26'  valign="middle" style=" background-color:#D7FFEE">部门</td>
							<td width='80%' height='23' valign="middle">&nbsp;&nbsp;&nbsp;&nbsp;<%=docDept%></td>
						</tr>
						<tr>
							<td width='20%' height='26'  valign="middle" style=" background-color:#D7FFEE">请假原因</td>
							<td width='80%' height='23' valign="middle">&nbsp;&nbsp;&nbsp;&nbsp;<%=docCause%></td>
						</tr>
						<tr>
							<td width='20%' height='26'  valign="middle" style=" background-color:#D7FFEE">请假天数</td>
							<td width='80%' height='23' valign="middle">&nbsp;&nbsp;&nbsp;&nbsp;<%=docNum%></td>
						</tr>
						<tr>
							<td width='20%' height='26'  valign="middle" style=" background-color:#D7FFEE">申请日期</td>
							<td width='80%' height='23' valign="middle">&nbsp;&nbsp;&nbsp;&nbsp;<%=docDate%></td>
						</tr>
						
					
					</table>

	</body>
</HTML>
