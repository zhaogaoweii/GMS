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

public partial class GenDoc : System.Web.UI.Page
{
    public string docID;
    public string docFile;
    public string docName;
    public string docDept;
    public string docCause;
    public string docNum;
    public DateTime docSubmitTime;
    protected void Page_Load(object sender, EventArgs e)
    {
        docID = Request.QueryString["ID"];

        string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo.mdb";
        OleDbConnection conn = new OleDbConnection(connString);
        conn.Open();

        string sql = "select * from leaveRecord where ID=" + docID;
        OleDbCommand cmd = new OleDbCommand(sql, conn);
        cmd.CommandType = CommandType.Text;
        OleDbDataReader Reader = cmd.ExecuteReader();
        if (Reader.Read())
        {
            docFile = Reader["FileName"].ToString();
            docName = Reader["Name"].ToString();
            docDept = Reader["Dept"].ToString();
            docCause = Reader["Cause"].ToString();
            docNum = Reader["Num"].ToString();
            docSubmitTime = DateTime.Parse(Reader["SubmitTime"].ToString());
        }
        Reader.Close();
        conn.Close();

        //-----------  PageOffice 服务器端编程开始  -------------------//

        PageOffice.WordWriter.WordDocument doc = new PageOffice.WordWriter.WordDocument();
        doc.DisableWindowRightClick = true;
        doc.OpenDataRegion("PO_name").Value = docName;
        doc.OpenDataRegion("PO_dept").Value = docDept;
        doc.OpenDataRegion("PO_cause").Value = docCause;
        doc.OpenDataRegion("PO_num").Value = docNum;
        doc.OpenDataRegion("PO_date").Value = docSubmitTime.ToLongDateString();
        doc.OpenDataRegion("PO_tip").Value = "";;

        // 设置PageOffice组件服务页面
        PageOfficeCtrl1.ServerPage = "pageoffice/server.aspx";

        // 设置界面样式
        PageOfficeCtrl1.Caption = "动态生成格式文件";
        PageOfficeCtrl1.BorderStyle = PageOffice.BorderStyleType.BorderThin;
        // 添加自定义工具条按钮
        PageOfficeCtrl1.AddCustomToolButton("打印", "poPrint", 6);
        PageOfficeCtrl1.AddCustomToolButton("全屏/还原", "poSetFullScreen", 4);
        
        ////获取数据对象
        PageOfficeCtrl1.SetWriter(doc);
        // 打开文档
        PageOfficeCtrl1.WebOpen("doc/template.doc", PageOffice.OpenModeType.docReadOnly, "Tom");
        //-----------  PageOffice 服务器端编程结束  -------------------//
    }
}
