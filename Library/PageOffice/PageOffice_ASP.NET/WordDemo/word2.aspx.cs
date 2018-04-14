using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.IO;

public partial class word2 : System.Web.UI.Page
{
    string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo_word.mdb";
    string strSql = "";
    string DocID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        DocID = Request.QueryString["ID"];

        strSql = "select * from word where ID = " + DocID;
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
        if ("在线编辑" == dt.Rows[0]["Status"].ToString())
        {
            Literal_Lc.Text = dt.Rows[0]["Status"].ToString();//当前文件的流程
            Literal_Lz.Text = "张三批阅";//流转
        }
        else
        {
            Literal_Lc.Text = "已流转到“" + dt.Rows[0]["Status"].ToString() + "”，当前是“修改无痕迹模式”打开文件的效果。";
        }
        string fileName = dt.Rows[0]["FileName"].ToString();
        string fileSubject = dt.Rows[0]["Subject"].ToString();

        PageOfficeCtrl1.Caption = fileSubject;
        PageOfficeCtrl1.ServerPage = "pageoffice/server.aspx";
        PageOfficeCtrl1.SaveFilePage = "savedoc.aspx";
        PageOfficeCtrl1.WebOpen(Server.MapPath("doc/") + fileName, PageOffice.OpenModeType.docNormalEdit, "张佚名");

    }

    /// <summary>
    /// 流转
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinkBtn_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim().Length > 0)
        {
            DocID = Request.QueryString["ID"];
        }

        strSql= "Update word set Status = '张三批阅' where ID = " + DocID;
        using (OleDbConnection connection = new OleDbConnection(connectionString))
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand(strSql.ToString(), connection);
                connection.Open();
                cmd.CommandType = CommandType.Text;
                object obj = cmd.ExecuteNonQuery();
                if (obj != null)
                {
                    Response.Redirect("index.aspx?ID=" + DocID + "&flag=true");
                }
                connection.Close();
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                Literal1.Text = "<script>alert('流转失败！失败原因为："+ex.Message+"');</script>";
            }
        }
    }
}
