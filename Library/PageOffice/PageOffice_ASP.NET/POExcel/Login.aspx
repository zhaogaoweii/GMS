﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>PageOffice销售管理系统登录</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta content="PageOffice网络OFFICE组件，微软OFFICE控件" name="description">
    <meta content="PageOffice,网络OFFICE控件,痕迹保留,强制痕迹保留,全文批注,手写批注,OFFICE文档,电子印章,手写签名,word留痕,留痕,公文留痕,在线编辑,在线保存,办公自动化,OA,电子签名,数字签名,手工批注,打印预览"
        name="keywords">
    <link href="images/css(2).css" rel="stylesheet" type="text/css" />
    <link href="images/csstg2.css" rel="stylesheet" type="text/css" />
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body bgcolor="#ffffff" leftmargin="0" topmargin="0" style="margin: 0; padding: 0;">
    <div id="header">
        <div style="float: left; margin-left: 20px;">
            <img src="images/logo.jpg" height="30" /></div>
        <div>
            <ul style=" list-style-type:none;">
                <li><a target="_blank" href="http://www.zhuozhengsoft.com">卓正网站</a></li>
                <li><a target="_blank" href="http://www.zhuozhengsoft.com/poask/index.asp">客户问吧</a></li>
                <li><a href="#">在线帮助</a></li>
                <li><a target="_blank" href="http://www.zhuozhengsoft.com/contact-us.html">联系我们</a></li>
            </ul>
        </div>
    </div>
    <form id="formData" method="post" runat="server">
    <table cellspacing="0" cellpadding="0" width="760" border="0" height="1" bgcolor="#ffffff"
        align="center">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td valign="top">
                            </td>
                        </tr>
                        <tr valign="top" align="center" height="120">
                            <td height="38" valign="middle">
                                <h1 class="font2 style3">
                                    销售系统示例</h1>
                            </td>
                        </tr>
                        <tr valign="top" align="right">
                            <td height="38" valign="middle">
                                <a href="login.asp"></a>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td width="100%" height="150">
                                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                    <tbody>
                                        <tr valign="top">
                                            <td width="1">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td width="12" background="images/img_f_14.gif">
                                                            <img src="images/img_f_13.gif" width="12" height="29" />
                                                        </td>
                                                        <td width="150" background="images/img_f_14.gif" class="font3">
                                                            <font color="#FFFFFF">系统登录</font>
                                                        </td>
                                                        <td width="12" align="left" background="images/img_f_16.gif">
                                                            <img src="images/img_f_15.gif" width="8" height="29" />
                                                        </td>
                                                        <td align="right" valign="bottom" background="images/img_f_16.gif">
                                                            &nbsp;
                                                        </td>
                                                        <td width="67" align="right" valign="bottom" background="images/img_f_16.gif">
                                                            <img src="images/img_f_39.gif" name="Image2521" width="67" height="29" border="0"
                                                                id="Image2521" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="100%" height="140" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td width="1" bgcolor="E2E2E2">
                                                        </td>
                                                        <td width="5">
                                                            &nbsp;
                                                        </td>
                                                        <td valign="top">
                                                            <table width="100%" height="140" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td width="100%" valign="top" colspan="2">
                                                                        <table class="text" cellspacing="0" cellpadding="4" width="95%" border="0" align="center">
                                                                            <tr>
                                                                                <td colspan="2" height="10">
                                                                                    <p>
                                                                                        &nbsp;</p>
                                                                                    <p>
                                                                                        &nbsp;</p>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" width="40%">
                                                                                    <strong>用户名：</strong>
                                                                                </td>
                                                                                <td width="60%">
                                                                                    <input name="TextUserName" type="text" id="TextUserName" class="boder" style="width: 150px;"
                                                                                        value="admin">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" height="27">
                                                                                    <strong>密&nbsp; 码：</strong>
                                                                                </td>
                                                                                <td>
                                                                                    <input name="TextPassword" type="password" id="TextPassword" class="boder" style="width: 150px;"
                                                                                        value="123" />
                                                                                    （密码默认123， 无需输入）
                                                                                </td>
                                                                            </tr>
                                                                            <tr align="center">
                                                                                <td colspan="2">
                                                                                    <input type="image" name="ImgBtnLogin" id="ImgBtnLogin" src="images/index2_31.gif"
                                                                                        alt="" border="0" />
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;<a href="index.htm"><img height="26" alt="" src="images/index2_33.gif"
                                                                                        width="64" border="0"></a>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="380">
                                                                    </td>
                                                                    <td>
                                                                        <a href="functions.htm"></a>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td width="5">
                                                            &nbsp;
                                                        </td>
                                                        <td width="1" bgcolor="E2E2E2">
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <img src="images/img_f_22.gif" width="100%" height="9" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
    <div id="footer" style="margin-top: 100px;">
        <hr width="1000" />
        <div>
            北京卓正志远软件有限公司 (c) 2011-2012
        </div>
    </div>
    </form>
</body>
</html>
