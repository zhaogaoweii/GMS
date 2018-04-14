using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Text;
using System.Configuration;
using System.IO;

public partial class word3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string DocID = Request.QueryString["ID"];

        string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo_word.mdb";
        string strSql = "select * from word where ID = " + DocID;
        DataSet ds = new DataSet();
        using (OleDbConnection connection = new OleDbConnection(connectionString))
        {
            connection.Open();
            OleDbDataAdapter command = new OleDbDataAdapter(strSql.ToString(), connection);
            command.Fill(ds, "ds");
            connection.Close();
        }

        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        Literal_Subject.Text = dt.Rows[0]["Subject"].ToString();//文件名称
        if ("正式发文" == dt.Rows[0]["Status"].ToString())
        {
            Literal_Lc.Text = dt.Rows[0]["Status"].ToString();//当前文件流程
        }
        else
        {
            Literal_Lc.Text = "已流转到“" + dt.Rows[0]["Status"].ToString() + "”，当前是“只读模式”打开文件的效果。";
        }
        string fileName = dt.Rows[0]["FileName"].ToString();
        string fileSubject = dt.Rows[0]["Subject"].ToString();

        PageOfficeCtrl1.Caption = fileSubject;
        PageOfficeCtrl1.AddCustomToolButton("另存到本地", "ShowDialog(0)", 5);
        PageOfficeCtrl1.AddCustomToolButton("页面设置", "ShowDialog(1)", 0);
        PageOfficeCtrl1.AddCustomToolButton("打印", "ShowDialog(2)", 6);
        PageOfficeCtrl1.AddCustomToolButton("全屏/还原", "IsFullScreen", 4);

        PageOfficeCtrl1.ServerPage = "pageoffice/server.aspx";
        PageOfficeCtrl1.SaveFilePage = "savedoc.aspx";
        PageOfficeCtrl1.WebOpen(Server.MapPath("doc/") + fileName, PageOffice.OpenModeType.docReadOnly, "张佚名");



    }

}
