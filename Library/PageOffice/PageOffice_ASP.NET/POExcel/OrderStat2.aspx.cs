using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Drawing;

public partial class OrderStat2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null || Session["UserName"].ToString().Length <= 0)
        {
            Response.Redirect("Login.aspx");
        }

        LitDate.Text = DateTime.Now.ToShortDateString() + "    " + "星期" + DateTime.Now.DayOfWeek.ToString(("D"));

        PageOffice.ExcelWriter.Workbook wordBook = new PageOffice.ExcelWriter.Workbook();
        PageOffice.ExcelWriter.Sheet sheet = wordBook.OpenSheet("查询表");

        // 数据库连接
        string connectionString =  "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo_excelorder.mdb";
        String strSql = "SELECT OrderNum,OrderDate,CustName,SalesName,Amount from OrderMaster order by ID desc ";
        DataSet ds = new DataSet();
        using (OleDbConnection connection = new OleDbConnection(connectionString))
        {
            try
            {
                connection.Open();
                OleDbDataAdapter command = new OleDbDataAdapter(strSql, connection);
                command.Fill(ds, "ds");
                connection.Close();
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        DataTable dt = new DataTable();
        decimal totalMoney = 0;
        if (ds != null)
        {
            dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sheet.OpenCell("B" + (5 + i)).Value = dt.Rows[i]["OrderNum"].ToString();
                if (dt.Rows[0]["OrderDate"] != null && dt.Rows[i]["OrderDate"].ToString().Length > 0)
                {
                    sheet.OpenCell("C" + (5 + i)).Value = DateTime.Parse(dt.Rows[0]["OrderDate"].ToString()).ToShortDateString();
                }
                sheet.OpenCell("D" + (5 + i)).Value = dt.Rows[i]["CustName"].ToString();
                sheet.OpenCell("E" + (5 + i)).Value = dt.Rows[i]["SalesName"].ToString();
                sheet.OpenCell("F" + (5 + i)).Value = string.Format("{0:C2}", Convert.ToDecimal(dt.Rows[i]["Amount"].ToString()));
                totalMoney += decimal.Parse(dt.Rows[i]["Amount"].ToString());

                if (i % 2 == 0)
                {
                    //设置背景色
                    sheet.OpenTable("B" + (5 + i) + ":F" + (5 + i)).BackColor = Color.FromArgb(253, 233, 217);
                }
            }

            //设置前景色
            sheet.OpenTable("B5:F" + (dt.Rows.Count + 4)).ForeColor = Color.FromArgb(148, 138, 84);

            //水平方向对齐方式
            sheet.OpenTable("B5:F" + (dt.Rows.Count + 4)).HorizontalAlignment = PageOffice.ExcelWriter.XlHAlign.xlHAlignLeft;
            sheet.OpenTable("C5:C" + (dt.Rows.Count + 4)).HorizontalAlignment = PageOffice.ExcelWriter.XlHAlign.xlHAlignCenter;
            sheet.OpenTable("E5:E" + (dt.Rows.Count + 4)).HorizontalAlignment = PageOffice.ExcelWriter.XlHAlign.xlHAlignCenter;
            sheet.OpenTable("F5:F" + (dt.Rows.Count + 4)).HorizontalAlignment = PageOffice.ExcelWriter.XlHAlign.xlHAlignRight;
            //竖直方向对齐方式
            sheet.OpenTable("B5:F" + (dt.Rows.Count + 4)).VerticalAlignment = PageOffice.ExcelWriter.XlVAlign.xlVAlignCenter;

        }

        //合计：

        //合并单元格
        sheet.OpenTable("B" + (dt.Rows.Count + 5) + ":F" + (dt.Rows.Count + 5)).Merge();
        //行高
        sheet.OpenTable("B5:F" + (dt.Rows.Count + 6)).RowHeight = 18;
        sheet.OpenTable("B" + (dt.Rows.Count + 5) + ":F" + (dt.Rows.Count + 6)).HorizontalAlignment = PageOffice.ExcelWriter.XlHAlign.xlHAlignLeft;
        sheet.OpenTable("B" + (dt.Rows.Count + 5) + ":F" + (dt.Rows.Count + 6)).VerticalAlignment = PageOffice.ExcelWriter.XlVAlign.xlVAlignCenter;

        sheet.OpenCell("B" + (dt.Rows.Count + 6)).Value = "合计";
        sheet.OpenTable("C" + (dt.Rows.Count + 6) + ":E" + (dt.Rows.Count + 6)).Merge();
        sheet.OpenCell("F" + (dt.Rows.Count + 6)).Value = string.Format("{0:C2}", totalMoney);
        sheet.OpenTable("F" + (dt.Rows.Count + 6) + ":F" + (dt.Rows.Count + 6)).HorizontalAlignment = PageOffice.ExcelWriter.XlHAlign.xlHAlignRight;
        sheet.OpenTable("B" + (dt.Rows.Count + 6) + ":F" + (dt.Rows.Count + 6)).VerticalAlignment = PageOffice.ExcelWriter.XlVAlign.xlVAlignCenter;

        //设置字体：大小、名称
        sheet.OpenTable("B5:F" + (dt.Rows.Count + 6)).Font.Size = 9;
        sheet.OpenTable("B5:F" + (dt.Rows.Count + 6)).Font.Name = "宋体";

        //设置Table的边框样式：样式、宽度、颜色(多种边框样式重叠时，需创建Table对象才可实现样式的叠加覆盖)
        PageOffice.ExcelWriter.Table table = sheet.OpenTable("B" + (dt.Rows.Count + 6) + ":F" + (dt.Rows.Count + 6));
        table.Border.BorderType = PageOffice.ExcelWriter.XlBorderType.xlTopEdge;
        table.Border.Weight = PageOffice.ExcelWriter.XlBorderWeight.xlThin;
        table.Border.LineColor = Color.FromArgb(148, 138, 84);

        PageOfficeCtrl1.SetWriter(wordBook);
    }
    protected void PageOfficeCtrl1_Load(object sender, EventArgs e)
    {
        string fileName = "OrderQuery.xls";//打开文件名称

        //添加自定义按钮
        PageOfficeCtrl1.AddCustomToolButton("打印", "Print", 6);
        PageOfficeCtrl1.AddCustomToolButton("打印预览", "PrintPreView", 7);
        PageOfficeCtrl1.AddCustomToolButton("页面设置", "SetPage", 3);
        PageOfficeCtrl1.AddCustomMenuItem("|", "", true);
        PageOfficeCtrl1.AddCustomToolButton("另存到本机", "StoreAs", 1);
        PageOfficeCtrl1.AddCustomMenuItem("|", "", true);
        PageOfficeCtrl1.AddCustomToolButton("全屏/还原", "SetScreen", 4);

        PageOfficeCtrl1.Caption = "查询表";
        PageOfficeCtrl1.BorderStyle = PageOffice.BorderStyleType.BorderThin;

        PageOfficeCtrl1.ServerPage = "pageoffice/server.aspx";
        PageOfficeCtrl1.WebOpen(Server.MapPath("doc/") + fileName, PageOffice.OpenModeType.xlsReadOnly, "admin");
    }
}
