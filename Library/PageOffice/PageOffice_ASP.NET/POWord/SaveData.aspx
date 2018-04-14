<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SaveData.aspx.cs" Inherits="SaveData" %>
<HTML>
	<HEAD>
		<title>SaveData</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=BaseUrl%>images/error/error.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<div class="errMainArea" id="error163"><div class="errTopArea" style="TEXT-ALIGN:left">[提示标题：这是一个开发人员可自定义的对话框]</div>
			<div class="errTxtArea" style="HEIGHT:150px; TEXT-ALIGN:left">
				<b class="txt_title">
					<div style="color:#FF0000;">请填写以下信息：</div>
					<ul>
					<%=ErrorMsg%>
					</ul>
					
				</b>
				
			</div>
			<div class="errBtmArea"><input type="button" class="btnFn" value=" 关闭 " onClick="window.opener=null;window.close();"></div>
		</div>
	</body>
</HTML>
