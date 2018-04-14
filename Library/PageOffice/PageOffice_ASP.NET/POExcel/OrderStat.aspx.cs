using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using System.Data;
using System.Data.OleDb;

public partial class OrderStat : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null || Session["UserName"].ToString().Length <= 0)
        {
            Response.Redirect("Login.aspx");
        }

        LitDate.Text = DateTime.Now.ToShortDateString() + "    " + "星期" + DateTime.Now.DayOfWeek.ToString(("D"));

        PageOffice.ExcelWriter.Workbook wordBook = new PageOffice.ExcelWriter.Workbook();
        PageOffice.ExcelWriter.Sheet sheet = wordBook.OpenSheet("统计图表");

        string connectionString =  "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo_excelorder.mdb";
        StringBuilder strSql = new StringBuilder();
        strSql.Append("SELECT OrderMaster.SalesName, OrderDetail.ProductName, sum(OrderDetail.Quantity) as Quantity, sum(OrderDetail.Price * OrderDetail.Quantity) as amount ");
        strSql.Append(" from OrderMaster,OrderDetail ");
        strSql.Append(" where OrderMaster.ID = OrderDetail.OrderID and OrderMaster.SalesName in('阿土伯','金贝贝','钱夫人','孙小美') ");
        strSql.Append(" group by OrderMaster.SalesName, OrderDetail.ProductName ");

        DataSet ds = new DataSet();
        using (OleDbConnection connection = new OleDbConnection(connectionString))
        {
            try
            {
                connection.Open();
                OleDbDataAdapter command = new OleDbDataAdapter(strSql.ToString(), connection);
                command.Fill(ds, "ds");
                connection.Close();
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        DataTable dt = new DataTable();
        int n = 0;
        if (ds != null)
        {
            dt = ds.Tables[0];
            int columnId = 5;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (1 == columnId % 4)
                {
                    sheet.OpenCell("B" + columnId).Value = dt.Rows[i]["SalesName"].ToString();
                }

                switch (dt.Rows[i]["ProductName"].ToString())
                {
                    case "笔记本":
                        {
                            sheet.OpenCell("C" + (columnId).ToString()).Value = dt.Rows[i]["ProductName"].ToString();
                            sheet.OpenCell("D" + (columnId).ToString()).Value = dt.Rows[i]["Quantity"].ToString();
                            sheet.OpenCell("E" + (columnId).ToString()).Value = dt.Rows[i]["amount"].ToString();
                            break;
                        }
                    case "服务器":
                        {
                            sheet.OpenCell("C" + (columnId + 1).ToString()).Value = dt.Rows[i]["ProductName"].ToString();
                            sheet.OpenCell("D" + (columnId + 1).ToString()).Value = dt.Rows[i]["Quantity"].ToString();
                            sheet.OpenCell("E" + (columnId + 1).ToString()).Value = dt.Rows[i]["amount"].ToString();
                            break;
                        }
                    case "路由器":
                        {
                            sheet.OpenCell("C" + (columnId + 2).ToString()).Value = dt.Rows[i]["ProductName"].ToString();
                            sheet.OpenCell("D" + (columnId + 2).ToString()).Value = dt.Rows[i]["Quantity"].ToString();
                            sheet.OpenCell("E" + (columnId + 2).ToString()).Value = dt.Rows[i]["amount"].ToString();
                            break;
                        }
                }

                if (i < dt.Rows.Count - 1
                    && !dt.Rows[i]["SalesName"].ToString().Equals(dt.Rows[i + 1]["SalesName"].ToString()))
                {
                    n++;
                    columnId = 5 + n * 4;
                }
            }
        }

        PageOfficeCtrl1.SetWriter(wordBook);

    }
    protected void PageOfficeCtrl1_Load(object sender, EventArgs e)
    {
        string fileName = "OrderReport.xls";//打开文件名称

        //添加自定义按钮
        PageOfficeCtrl1.AddCustomToolButton("打印", "Print", 6);
        PageOfficeCtrl1.AddCustomToolButton("打印预览", "PrintPreView", 7);
        PageOfficeCtrl1.AddCustomToolButton("页面设置", "SetPage", 3);
        PageOfficeCtrl1.AddCustomMenuItem("|", "", true);
        PageOfficeCtrl1.AddCustomToolButton("另存到本机", "StoreAs", 1);
        PageOfficeCtrl1.AddCustomMenuItem("|", "", true);
        PageOfficeCtrl1.AddCustomToolButton("全屏/还原", "SetScreen", 4);

        PageOfficeCtrl1.Caption = "统计图表";
        PageOfficeCtrl1.BorderStyle = PageOffice.BorderStyleType.BorderThin;

        PageOfficeCtrl1.ServerPage = "pageoffice/server.aspx";
        PageOfficeCtrl1.WebOpen(Server.MapPath("doc/") + fileName, PageOffice.OpenModeType.xlsReadOnly, "admin");

    }
}
