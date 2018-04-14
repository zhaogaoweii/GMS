using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using System.Data;
using System.Data.OleDb;

public partial class OpenOrder : System.Web.UI.Page
{
    string id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null || Session["UserName"].ToString().Length <= 0)
        {
            Response.Redirect("Login.aspx");
        }

        if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim().Length > 0)
        {
            id = Request.QueryString["ID"];
        }
        else
        {
            Response.Redirect("OrderList.aspx");
        }

        LitDate.Text = DateTime.Now.ToShortDateString() + "    " + "星期" + DateTime.Now.DayOfWeek.ToString(("D"));

        // 数据库连接
        string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo_excelorder.mdb";
        string strSql = "select * from OrderMaster where ID = " + id;

        DataSet ds = new DataSet();
        using (OleDbConnection connection = new OleDbConnection(connectionString))
        {
            try
            {
                connection.Open();
                OleDbDataAdapter command = new OleDbDataAdapter(strSql, connection);
                command.Fill(ds, "ds");
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // 填充数据
        PageOffice.ExcelWriter.Workbook workBook = new PageOffice.ExcelWriter.Workbook();
        //workBook.DisableSheetDoubleClick = true;
        PageOffice.ExcelWriter.Sheet sheet = workBook.OpenSheet("销售订单");
        if (ds.Tables.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                #region 客户信息

                sheet.OpenCell("D5").Value = dt.Rows[0]["CustName"].ToString();
                sheet.OpenCell("D5").SubmitName = "CustName";//单元格提交数据 
                sheet.OpenCell("I5").Value = dt.Rows[0]["OrderNum"].ToString();
                sheet.OpenCell("I5").SubmitName = "OrderNum";//单元格提交数据
                sheet.OpenCell("D6").Value = dt.Rows[0]["CustDistrict"].ToString();
                sheet.OpenCell("D6").SubmitName = "CustDistrict";//单元格提交数据
                sheet.OpenCell("I6").Value = dt.Rows[0]["OrderDate"].ToString();
                sheet.OpenCell("I6").SubmitName = "OrderDate";//单元格提交数据
                sheet.OpenCell("D18").Value = dt.Rows[0]["MakerName"].ToString();
                sheet.OpenCell("D18").SubmitName = "UserName";//单元格提交数据
                sheet.OpenCell("H18").Value = dt.Rows[0]["SalesName"].ToString();
                sheet.OpenCell("H18").SubmitName = "SalesName";//单元格提交数据
                sheet.OpenCell("I16").SubmitName = "Amount";//单元格提交数据
                sheet.OpenCell("I16").ReadOnly = true;//将Excel模版中有公式的单元格设置为只读格式，以免覆盖掉公式
                #endregion

                #region 产品信息
                strSql = "select * from OrderDetail where OrderID =" + dt.Rows[0]["ID"];

                DataSet ds2 = new DataSet();
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        OleDbDataAdapter command = new OleDbDataAdapter(strSql, connection);
                        command.Fill(ds2, "ds2");
                    }
                    catch (System.Data.OleDb.OleDbException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }

                //定义table对象
                PageOffice.ExcelWriter.Table tableD = sheet.OpenTable("D9:D15");//定义table对象
                tableD.ReadOnly = true;//将table设置成只读

                PageOffice.ExcelWriter.Table table = sheet.OpenTable("C9:H15");
                table.SubmitName = "OrderDetail"; //表提交数据

                if (ds2.Tables.Count > 0)
                {
                    DataTable dt2 = ds2.Tables[0];
                    if (dt2.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt2.Rows.Count; j++)
                        {
                            for (int i = 0; i < table.DataFields.Count; i++)
                            {
                                if (i != 1)//跳过Excel模版中有公式的列，让Excel自动计算该列的值
                                {
                                    table.DataFields[i].Value = dt2.Rows[j][i + 1].ToString();
                                }
                            }
                            table.NextRow();//换行
                        }
                    
                    }
                }
                table.Close();//关闭table
                #endregion
            }
        }
       
        PageOfficeCtrl1.SetWriter(workBook);

    }
    protected void PageOfficeCtrl1_Load(object sender, EventArgs e)
    {
        string fileName = "OrderForm.xls";
        PageOfficeCtrl1.AddCustomToolButton("保存", "Store", 1);
        PageOfficeCtrl1.AddCustomToolButton("全屏/还原", "SetScreen", 4);
        PageOfficeCtrl1.BorderStyle = PageOffice.BorderStyleType.BorderThin;
        PageOfficeCtrl1.ServerPage = "pageoffice/server.aspx";
        PageOfficeCtrl1.SaveDataPage = "UpdateOrder.aspx?ID=" + id ;
        PageOfficeCtrl1.WebOpen(Server.MapPath("doc/") + fileName, PageOffice.OpenModeType.xlsSubmitForm, "admin");
    }
}
