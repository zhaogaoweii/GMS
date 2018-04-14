using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Text;
using System.Data;

public partial class Excel : System.Web.UI.Page
{
    public string sID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
  
        string strID = Request.QueryString["id"].ToString();
        sID = strID;
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

        PageOfficeCtrl1.ServerPage = "pageoffice/server.aspx";
        PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx";

        PageOfficeCtrl1.CustomMenuCaption = "自定义菜单(&N)";
        PageOfficeCtrl1.AddCustomMenuItem("显示标题(&T)", "CustomMenuItem1_Click()", true);
        PageOfficeCtrl1.AddCustomMenuItem("-", "", false);
        PageOfficeCtrl1.AddCustomMenuItem("领导圈阅(&D)", "CustomMenuItem2_Click()", true);

        PageOfficeCtrl1.AddCustomToolButton("保存", "CustomToolBar_Save()", 1);
        PageOfficeCtrl1.AddCustomToolButton("另存为...", "CustomToolBar_SaveAs()", 1);
        PageOfficeCtrl1.AddCustomToolButton("另存为Html", "CustomToolBar_SaveAsHtml()", 1);
        PageOfficeCtrl1.AddCustomToolButton("插入印章", "CustomToolBar_InsertSeal()", 2);
        PageOfficeCtrl1.AddCustomToolButton("领导圈阅", "CustomToolBar_HandDraw()", 3);
        PageOfficeCtrl1.AddCustomToolButton("全屏/还原", "CustomToolBar_FullScreen()", 4);
        PageOfficeCtrl1.BorderStyle = PageOffice.BorderStyleType.BorderThin;
        //Response.Write(Server.MapPath("doc/") + docFile);
        PageOfficeCtrl1.WebOpen(Server.MapPath("doc/") + docFile, PageOffice.OpenModeType.xlsNormalEdit, "张佚名");
        
    }
}
