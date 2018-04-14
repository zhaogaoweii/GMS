using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Text;
using System.Data;

public partial class Excel2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strID = Request.QueryString["id"].ToString();

        //Create the command and the connection
        string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo.mdb";
        string sql = "select * from excel where ID=" + strID;
        OleDbConnection conn = new OleDbConnection(connString);
        OleDbCommand cmd = new OleDbCommand(sql, conn);
        conn.Open();
        cmd.CommandType = CommandType.Text;
        OleDbDataReader Reader = cmd.ExecuteReader();
        string docFile = "";
        string docSubject = "";
        if (Reader.Read())
        {
            docFile = Reader["FileName"].ToString();
            docSubject = Reader["Subject"].ToString();
        }
        Reader.Close();
        conn.Close();

        PageOffice.ExcelWriter.Workbook wb = new PageOffice.ExcelWriter.Workbook();
        wb.DisableSheetDoubleClick = true;
        wb.DisableSheetRightClick = true;

        PageOfficeCtrl1.ServerPage = "pageoffice/server.aspx";
        PageOfficeCtrl1.AddCustomToolButton("另存为...","saveAs",1);
        PageOfficeCtrl1.AddCustomToolButton("打印设置", "setPrint", 0);
        PageOfficeCtrl1.AddCustomToolButton("全屏/还原", "setFullScreen", 4);
        PageOfficeCtrl1.Caption = "Excel文件:" + docSubject;
        PageOfficeCtrl1.SetWriter(wb);
        PageOfficeCtrl1.BorderStyle = PageOffice.BorderStyleType.BorderThin;
        PageOfficeCtrl1.WebOpen(Server.MapPath("doc/") + docFile, PageOffice.OpenModeType.xlsReadOnly, "张佚名");
    }
}
