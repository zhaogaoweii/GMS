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
using System.Drawing;

public partial class SubmitDataOfDoc : System.Web.UI.Page
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
        PageOffice.WordWriter.DataRegion drName = doc.OpenDataRegion("PO_name");
        drName.Value = docName;
        drName.Editing = true;
        PageOffice.WordWriter.DataRegion drDept = doc.OpenDataRegion("PO_dept");
        drDept.Value = docDept;
        drDept.Shading.BackgroundPatternColor = Color.Silver;
        //drDept.Editing = true;
        PageOffice.WordWriter.DataRegion drCause = doc.OpenDataRegion("PO_cause");
        drCause.Value = docCause;
        drCause.Editing = true;
        PageOffice.WordWriter.DataRegion drNum = doc.OpenDataRegion("PO_num");
        drNum.Value = docNum;
        drNum.Editing = true;
        PageOffice.WordWriter.DataRegion drDate = doc.OpenDataRegion("PO_date");
        drDate.Value = docSubmitTime.ToLongDateString();
        drDate.Shading.BackgroundPatternColor = Color.Pink;
        //drDate.Editing = true;
        PageOffice.WordWriter.DataRegion drTip = doc.OpenDataRegion("PO_tip");
        drTip.Font.Italic = true;
        drTip.Value = "提示：带背景色的文字是只能通过选择设置，[]中的文字是可以录入编辑的。";

        // 设置PageOffice组件服务页面
        PageOfficeCtrl1.ServerPage = "pageoffice/server.aspx";

        // 设置界面样式
        PageOfficeCtrl1.Caption = "用户填写请假条";
        PageOfficeCtrl1.BorderStyle = PageOffice.BorderStyleType.BorderThin;
        // 添加自定义工具条按钮
        PageOfficeCtrl1.AddCustomToolButton("保存", "poSave", 1);
        PageOfficeCtrl1.AddCustomToolButton("全屏/还原", "poSetFullScreen", 4);

        PageOfficeCtrl1.JsFunction_OnWordDataRegionClick = "OnWordDataRegionClick()";
        // 设置保存文档的服务器页面
        PageOfficeCtrl1.SaveDataPage = "SaveData.aspx?ID=" + docID;
        ////获取数据对象
        PageOfficeCtrl1.SetWriter(doc);
        // 打开文档
        PageOfficeCtrl1.WebOpen("doc/template.doc",PageOffice.OpenModeType.docSubmitForm, "Tom");
        //-----------  PageOffice 服务器端编程结束  -------------------//
    }
}
