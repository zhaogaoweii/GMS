using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Configuration;
using System.IO;

public partial class word : System.Web.UI.Page
{
    string DocID = "";
    string userName = "";
    string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo_word.mdb";
    string strSql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        DocID = Request.QueryString["ID"];
        userName = Request.QueryString["user"].ToString();

        strSql = "select * from word  where ID = " + DocID;
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
        if ("李四批阅" == dt.Rows[0]["Status"].ToString() && "李四" == userName
                || "张三批阅" == dt.Rows[0]["Status"].ToString() && "张三" == userName)
        {
            Literal_Lc.Text = dt.Rows[0]["Status"].ToString();//当前文件的流程
            if ("张三" == userName) Literal_Lz.Text = "李四批阅";
            if ("李四" == userName) Literal_Lz.Text = "文员清稿";
        }
        else
        {
            Literal_Lc.Text = "已流转到“" + dt.Rows[0]["Status"].ToString() + "”，当前是“强制留痕模式”打开文件的效果。";
        }

        string fileName = dt.Rows[0]["FileName"].ToString();
        string fileSubject = dt.Rows[0]["Subject"].ToString();

        PageOfficeCtrl1.Caption = fileSubject;
        PageOfficeCtrl1.CustomMenuCaption = "自定义菜单";
        PageOfficeCtrl1.AddCustomMenuItem("显示痕迹", "ShowRevisions", false);
        PageOfficeCtrl1.AddCustomMenuItem("隐藏痕迹", "HiddenRevisions", false);
        PageOfficeCtrl1.AddCustomMenuItem("-", "", false);
        PageOfficeCtrl1.AddCustomMenuItem("显示标题", "ShowTitle", true);
        PageOfficeCtrl1.AddCustomMenuItem("-", "", false);
        PageOfficeCtrl1.AddCustomMenuItem("领导圈阅", "StartHandDraw", true);
        PageOfficeCtrl1.AddCustomMenuItem("插入印章", "InsertSeal", false);
        PageOfficeCtrl1.AddCustomMenuItem("-", "", false);
        PageOfficeCtrl1.AddCustomMenuItem("分层显示手写批注", "ShowHandDrawDispBar", true);

        PageOfficeCtrl1.AddCustomToolButton("保存", "Save", 1);
        PageOfficeCtrl1.AddCustomToolButton("显示痕迹", "ShowRevisions", 5);
        PageOfficeCtrl1.AddCustomToolButton("隐藏痕迹", "HiddenRevisions", 5);
        PageOfficeCtrl1.AddCustomToolButton("列举所有痕迹", "jsGetAllRevisions", 0);
        PageOfficeCtrl1.AddCustomToolButton("领导圈阅", "StartHandDraw", 3);
        PageOfficeCtrl1.AddCustomToolButton("插入键盘批注", "StartRemark", 3);
        PageOfficeCtrl1.AddCustomToolButton("分层显示手写批注", "ShowHandDrawDispBar", 7);
        PageOfficeCtrl1.AddCustomToolButton("全屏/还原", "IsFullScreen", 4);

        PageOfficeCtrl1.ServerPage = "pageoffice/server.aspx";
        PageOfficeCtrl1.SaveFilePage = "savedoc.aspx";
        PageOfficeCtrl1.WebOpen(Server.MapPath("doc/") + fileName, PageOffice.OpenModeType.docRevisionOnly, userName);

    }

    /// <summary>
    /// 流转
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinkBtn_Click(object sender, EventArgs e)
    {
        DocID = Request.QueryString["ID"];
        userName = Request.QueryString["user"].ToString();

        if ("李四" == userName)
        {
            strSql = " Update word set Status = '文员清稿' where ID = " + DocID;
        }
        else
        {
            strSql = " Update word set Status = '李四批阅' where ID = " + DocID;
        }

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
