using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;

public partial class Word_FileTransfer_word1 : System.Web.UI.Page
{
    public string DocID = "";
    string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo_word.mdb";
    string strSql = "";
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
        if ("文员清稿" == dt.Rows[0]["Status"].ToString())
        {
            Literal_Subject.Text = dt.Rows[0]["Subject"].ToString();//文件名称
            Literal_Lc.Text = dt.Rows[0]["Status"].ToString();//当前文件的流程
            Literal_Lz.Text = "正式发文";//流转
        }
        else
        {
            Literal_Lc.Text = "已流转到“" + dt.Rows[0]["Status"].ToString() + "”，当前是“核稿模式”打开文件的效果。";
        }

        string fileName = dt.Rows[0]["FileName"].ToString();

        PageOfficeCtrl1.CustomMenuCaption = "自定义菜单";
        PageOfficeCtrl1.AddCustomMenuItem("显示痕迹", "ShowRevisions", true);
        PageOfficeCtrl1.AddCustomMenuItem("隐藏痕迹", "HiddenRevisions", true);
        PageOfficeCtrl1.AddCustomMenuItem("-", "", false);
        PageOfficeCtrl1.AddCustomMenuItem("显示标题", "ShowTitle", true);
        PageOfficeCtrl1.AddCustomMenuItem("-", "", false);
        PageOfficeCtrl1.AddCustomMenuItem("领导签批", "InsertHandSign", true);
        PageOfficeCtrl1.AddCustomMenuItem("插入印章", "InsertSeal", true);
        PageOfficeCtrl1.AddCustomMenuItem("接受所有修订", "AcceptAllRevisions", true);
        PageOfficeCtrl1.AddCustomMenuItem("-", "", false);
        PageOfficeCtrl1.AddCustomMenuItem("分层显示手写批注", "ShowHandDrawDispBar", true);

        PageOfficeCtrl1.AddCustomToolButton("保存", "Save", 1);
        PageOfficeCtrl1.AddCustomToolButton("另存为Html", "SaveAsHtml", 0);
        PageOfficeCtrl1.AddCustomToolButton("显示/隐藏痕迹", "Show_HidRevisions", 5);
        PageOfficeCtrl1.AddCustomToolButton("插入印章/签名", "InsertSeal", 2);
        PageOfficeCtrl1.AddCustomToolButton("领导签批", "InsertHandSign", 3);
        PageOfficeCtrl1.AddCustomToolButton("接受所有修订", "AcceptAllRevisions", 5);
        PageOfficeCtrl1.AddCustomToolButton("分层显示手写批注", "ShowHandDrawDispBar", 7);
        PageOfficeCtrl1.AddCustomToolButton("全屏/还原", "IsFullScreen", 4);

        PageOfficeCtrl1.ServerPage = "pageoffice/server.aspx";
        PageOfficeCtrl1.SaveFilePage = "savedoc.aspx";
        PageOfficeCtrl1.WebOpen(Server.MapPath("doc/") + fileName, PageOffice.OpenModeType.docAdmin, "张佚名");

    }

    /// <summary>
    /// 流转
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinkBtn_Click(object sender, EventArgs e)
    {
        DocID = Request.QueryString["ID"];

        strSql = "Update word set Status = '正式发文' where ID = " + DocID;
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
                Literal1.Text = "<script>alert('流转失败！失败原因为：" + ex.Message + "');</script>";
            }
        }
    }
}
