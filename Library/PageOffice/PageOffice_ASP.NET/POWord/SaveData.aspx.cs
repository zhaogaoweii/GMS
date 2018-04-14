using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

public partial class SaveData : System.Web.UI.Page
{
    public string ErrorMsg = "";
    public string BaseUrl = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string strID = Request.QueryString["ID"];

        string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo.mdb";
        OleDbConnection conn = new OleDbConnection(connString);
        conn.Open();

        string strsql;
        OleDbCommand cmd = new OleDbCommand();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.Text;

        //-----------  PageOffice 服务器端编程开始  -------------------//
        PageOffice.WordReader.WordDocument doc = new PageOffice.WordReader.WordDocument();
        string sName = doc.OpenDataRegion("PO_name").Value;
        string sDept = doc.OpenDataRegion("PO_dept").Value;
        string sCause = doc.OpenDataRegion("PO_cause").Value;
        string sNum = doc.OpenDataRegion("PO_num").Value;
        string sDate = doc.OpenDataRegion("PO_date").Value;

        if (sName == "") 
	    {
		    ErrorMsg = ErrorMsg + "<li>申请人</li>";
	    } 
	    if (sDept == "") 
	    {
		    ErrorMsg = ErrorMsg + "<li>部门名称</li>";
	    } 
	    if (sCause == "") 
	    {
		    ErrorMsg = ErrorMsg + "<li>请假原因</li>";
	    }
        if (sDate == "")
        {
            ErrorMsg = ErrorMsg + "<li>日期</li>";
        } 
	    try 
	    { 
		    if (sNum != "")
		    {
			    if (Int32.Parse(sNum) < 0) 
			    {
				    ErrorMsg = ErrorMsg + "<li>请假天数不能是负数</li>";
			    } 
		    }
		    else
		    {
			    ErrorMsg = ErrorMsg + "<li>请假天数</li>";
		    }
	    }
	    catch(Exception Ex) 
	    {
		    ErrorMsg = ErrorMsg + "<li><font color=red>注意：</font>请假天数必须是数字</li>";
	    }

        if (ErrorMsg == "")
        {
            strsql = "update leaveRecord set Name='"
                + sName + "', Dept='" + sDept + "', Cause='" + sCause + "', Num=" + sNum + ", SubmitTime='"+sDate+"' where  ID=" + strID;
            cmd.CommandText = strsql;
            cmd.ExecuteNonQuery();
        }
        else
        {
            doc.ShowPage(578, 380); 
        }
        doc.Close();
        conn.Close();
        //-----------  PageOffice 服务器端编程结束  -------------------//
        string mScriptName = "savedata.aspx";
        string mHttpUrl = "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ServerVariables["SCRIPT_NAME"];
        BaseUrl = mHttpUrl.Substring(0, mHttpUrl.Length - mScriptName.Length); 
    }
}
