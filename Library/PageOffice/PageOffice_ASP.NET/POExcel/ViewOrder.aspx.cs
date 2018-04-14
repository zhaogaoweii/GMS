using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Configuration;

public partial class ViewOrder : System.Web.UI.Page
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
                connection.Close();
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        PageOffice.ExcelWriter.Workbook workBook = new PageOffice.ExcelWriter.Workbook();
        workBook.DisableSheetDoubleClick = true;
        workBook.DisableSheetRightClick = true;
        PageOffice.ExcelWriter.Sheet sheet = workBook.OpenSheet("销售订单");
        if (ds != null)
        {
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                #region 客户信息
                sheet.OpenCell("D5").Value = dt.Rows[0]["CustName"].ToString();
                sheet.OpenCell("I5").Value = dt.Rows[0]["OrderNum"].ToString();
                sheet.OpenCell("D6").Value = dt.Rows[0]["CustDistrict"].ToString();
                sheet.OpenCell("I6").Value = dt.Rows[0]["OrderDate"].ToString();
                sheet.OpenCell("D18").Value = dt.Rows[0]["MakerName"].ToString();
                sheet.OpenCell("H18").Value = dt.Rows[0]["SalesName"].ToString();
                #endregion

                #region 产品信息
                strSql = "select * from OrderDetail  where OrderID = " + dt.Rows[0]["ID"];
                DataSet ds2 = new DataSet();
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        OleDbDataAdapter command = new OleDbDataAdapter(strSql, connection);
                        command.Fill(ds2, "ds2");
                        connection.Close();
                    }
                    catch (System.Data.OleDb.OleDbException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                if (ds2 != null)
                {
                    DataTable dt2 = ds2.Tables[0];
                    if (dt2.Rows.Count > 0)
                    {
                        PageOffice.ExcelWriter.Table table = sheet.OpenTable("C9:H15");
                        for (int j = 0; j < dt2.Rows.Count; j++)
                        {
                            for (int i = 0; i < table.DataFields.Count; i++)
                            {
                                if (dt2.Rows[j][i + 1] != null)
                                {
                                    table.DataFields[i].Value = dt2.Rows[j][i + 1].ToString();
                                }
                                else
                                {
                                    table.DataFields[i].Value = "";
                                }
                            }
                            int result = 0;
                            if (int.TryParse(dt2.Rows[j]["Quantity"].ToString(), out result)
                                && int.TryParse(dt2.Rows[j]["Price"].ToString(), out result))
                            {
                                sheet.OpenCell("I" + (9 + j)).Value = (int.Parse(dt2.Rows[j]["Quantity"].ToString()) * int.Parse(dt2.Rows[j]["Price"].ToString())).ToString();
                            }
                            table.NextRow();
                        }
                        table.Close();
                    }
                }
                #endregion
            }
        }
        else
        {
            Response.Write("<script>alert('订单号为"+ id +"'的订单信息不存在！);location.href='OrderList.aspx'</script>");
        }
        workBook.DisableSheetSelection = true;
        PageOfficeCtrl1.SetWriter(workBook);
    }
    protected void PageOfficeCtrl1_Load(object sender, EventArgs e)
    {
        string fileName = "OrderForm.xls";

        //添加自定义菜单
        PageOfficeCtrl1.AddCustomToolButton("打印", "Print", 6);
        PageOfficeCtrl1.AddCustomToolButton("打印预览", "PrintPreView", 7);
        PageOfficeCtrl1.AddCustomToolButton("页面设置", "SetPage", 3);
        PageOfficeCtrl1.AddCustomMenuItem("|", "", true);
        PageOfficeCtrl1.AddCustomToolButton("另存到本机", "StoreAs", 1);
        PageOfficeCtrl1.AddCustomMenuItem("|", "", true);
        PageOfficeCtrl1.AddCustomToolButton("全屏/还原", "SetScreen", 4);

        PageOfficeCtrl1.ServerPage = "pageoffice/server.aspx";
        PageOfficeCtrl1.WebOpen(Server.MapPath("doc/") + fileName, PageOffice.OpenModeType.xlsReadOnly, "admin");
    }
}
