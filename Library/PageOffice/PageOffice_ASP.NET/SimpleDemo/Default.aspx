<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style=" font-size:12px;">
    <form id="form1" runat="server">
    <div style=" border:solid 1px RoyalBlue; width:500px; text-align:center;  height:200px; margin:100px auto;">
        <div style=" margin-top:50px; height:170px; ">
        方法1：<a href="word1.aspx">使用服务器磁盘路径打开word文件</a><span style="color:Red;">（标准版不支持）</span><br /><br />
        方法2：<a href="word.aspx">使用相对URL路径打开word文件</a><span style="color:Red;">（所有版本都支持）</span>
        </div>
        <div style=" color:Gray;">Copyright &copy 2012 北京卓正志远软件有限公司 </div>
    </div>
    </form>
</body>
</html>
