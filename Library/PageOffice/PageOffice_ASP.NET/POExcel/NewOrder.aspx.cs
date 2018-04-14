using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewOrder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null || Session["UserName"].ToString().Length <= 0)
        {
            Response.Redirect("Login.aspx");
        }
        LitDate.Text = DateTime.Now.ToShortDateString() + "    " + "星期" + DateTime.Now.DayOfWeek.ToString(("D"));


        PageOffice.ExcelWriter.Workbook workBook = new PageOffice.ExcelWriter.Workbook();
        PageOffice.ExcelWriter.Sheet sheet = workBook.OpenSheet("销售订单");
        #region 客户信息

        sheet.OpenCell("D5").SubmitName = "CustName";
        sheet.OpenCell("I5").SubmitName = "OrderNum";
        sheet.OpenCell("D6").SubmitName = "CustDistrict";
        sheet.OpenCell("I6").SubmitName = "OrderDate";
        sheet.OpenCell("I6").Value = DateTime.Now.ToShortDateString();
        sheet.OpenCell("D18").Value = Convert.ToString(Session["UserName"]);
        sheet.OpenCell("D18").SubmitName = "UserName";
        sheet.OpenCell("H18").SubmitName = "SalesName";
        #endregion

        #region 产品信息
        sheet.OpenTable("C9:H15").SubmitName = "OrderDetail";
        sheet.OpenCell("I16").SubmitName = "Amount";
        #endregion

        sheet.OpenCell("I6").ReadOnly = true;//将Excel模版中有公式的单元格设置为只读格式，以免覆盖掉公式
        PageOfficeCtrl1.SetWriter(workBook);
    }
    protected void PageOfficeCtrl1_Load(object sender, EventArgs e)
    {
        string fileName = "OrderForm.xls";
        PageOfficeCtrl1.AddCustomToolButton("保存", "Store", 1);
        PageOfficeCtrl1.AddCustomToolButton("全屏/还原", "SetScreen", 4);
        PageOfficeCtrl1.ServerPage = "pageoffice/server.aspx";
        PageOfficeCtrl1.SaveDataPage = "UpdateOrder.aspx?";
        PageOfficeCtrl1.WebOpen(Server.MapPath("doc/") + fileName, PageOffice.OpenModeType.xlsSubmitForm, "admin");
    }
}
