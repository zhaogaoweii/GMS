using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;
using System.Data;

public partial class UpdateOrder : System.Web.UI.Page
{
    protected string strErrHtml = "";//错误提示
    public string BaseUrl = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null || Session["UserName"].ToString().Length <= 0)
        {
            Response.Redirect("Login.aspx");
        }

        string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo_excelorder.mdb";
        OleDbConnection conn = new OleDbConnection(connectionString);
        conn.Open();
        OleDbCommand cmd = new OleDbCommand();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.Text;

        PageOffice.ExcelReader.Workbook workBook = new PageOffice.ExcelReader.Workbook(); ;
        PageOffice.ExcelReader.Sheet sheet = workBook.OpenSheet("销售订单");
        string sql = "";
        
        if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Length > 0)
        {
            #region 修改保存
            string id = "";
            id = Request.QueryString["ID"];
            int num;
            //保存客户信息
            num = UpdateOrInsertCustInfo(cmd, id, workBook, sheet, 0);
            if (num > 0)//保存成功
            {
                int resDelete = 0;//要删除的记录条数

                //删除当前orderID下的产品数据
                sql = "delete from OrderDetail where OrderId = " + id;
                try
                {
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    strErrHtml += "删除客户ID为" + id + "的产品订单信息失败，失败原因为：" + ex.Message + "\n";
                    resDelete = -1;
                }

                //删除成功或无数据可删除时
                if (resDelete >= 0)
                {
                    //插入产品信息
                    InsertProductInfo(cmd, workBook, sheet, id);
                }
            }
            else
            {
                strErrHtml += "<br>客户信息保存失败！";
            }
            #endregion
        }
        else
        {
            #region 新建保存
            int maxId = 0;//OrderMaster表中最大ID号
            sql = "select max(ID) from OrderMaster ";
            cmd.CommandText = sql;
            try
            {
                object obj = cmd.ExecuteScalar();
                if (obj != null)
                {
                    maxId = int.Parse(obj.ToString());
                    //保存客户信息
                    if (UpdateOrInsertCustInfo(cmd, "", workBook, sheet, maxId) > 0)
                    {
                        //插入产品信息
                        InsertProductInfo(cmd, workBook, sheet, (maxId + 1).ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                strErrHtml += "新建订单失败，失败原因为：" + ex.Message;
            }
            #endregion
        }
        

        //保存失败,弹出提示框
        if (strErrHtml.Length > 0)
        {
            strErrHtml = "\n" + strErrHtml;
            workBook.ShowPage(410, 260);
            workBook.CustomSaveResult = "error";
        }
        workBook.Close();
        conn.Close();

        string mScriptName = "updateorder.aspx";
        string mHttpUrl = "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ServerVariables["SCRIPT_NAME"];
        BaseUrl = mHttpUrl.Substring(0, mHttpUrl.Length - mScriptName.Length);
    }

    /// <summary>
    /// 插入产品信息
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="workBook"></param>
    /// <param name="sheet"></param>
    /// <param name="orderId">和产品相对应的客户ID</param>
    private void InsertProductInfo(OleDbCommand cmd, PageOffice.ExcelReader.Workbook workBook, PageOffice.ExcelReader.Sheet sheet, string orderId)
    {
        PageOffice.ExcelReader.Table table = sheet.OpenTable("OrderDetail");
        while (!table.EOF)
        {
            //根据当前OrderID重新插入产品数据
            string sql = "insert into OrderDetail(OrderID, ProductCode, ProductName, ProductType, Unit, Quantity, Price) values(" + orderId;
            if (!table.DataFields.IsEmpty)//数据字段非空时
            {
                int qua = 0;//数量
                if (table.DataFields[4].Value.Trim().Length > 0 && int.TryParse(table.DataFields[4].Value.Trim(), out qua))
                {
                    qua = int.Parse(table.DataFields[4].Value.Trim());
                }
                float price = 0.00f;//单价
                if (float.TryParse(table.DataFields[5].Value.Trim(), out price))
                {
                    price = float.Parse(table.DataFields[5].Value.Trim());
                }
                sql += ",'" + table.DataFields[0].Value + "','" + table.DataFields[1].Value + "','" +
                    table.DataFields[2].Value.Trim() + "','" + table.DataFields[3].Value.Trim() + "'," +
                    qua + ",'" + price + "')";
                try
                {
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    strErrHtml += "客户ID为" + orderId + "的产品订单信息添加失败，失败原因为：" + ex.Message + "\n"; ;
                }
            }

            table.NextRow();//跳到下一行
        }
        table.Close();//关闭table

    }

    /// <summary>
    /// 更新或插入客户信息
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="cid">客户信息ID</param>
    /// <param name="workBook"></param>
    /// <param name="sheet"></param>
    /// <param name="maxId">当前客户信息表中最大的ID</param>
    /// <returns></returns>
    private int UpdateOrInsertCustInfo(OleDbCommand cmd, string cid, PageOffice.ExcelReader.Workbook workBook, PageOffice.ExcelReader.Sheet sheet, int maxId)
    {
        string sql = "";
        string custName = sheet.OpenCell("CustName").Value.Trim();//获取提交信息，客户名称
        string orderId = sheet.OpenCell("OrderNum").Value;//获取提交信息，订单编号
        string district = sheet.OpenCell("CustDistrict").Value;//获取提交信息，客户所在区域
        double date = 0;
        if (sheet.OpenCell("OrderDate").Value.Length > 0)
        {
            date = Double.Parse(sheet.OpenCell("OrderDate").Value);//获取提交信息，订单日期
        }
        else
        {
            date = int.Parse(DateTime.Now.ToShortDateString());
        }

        string salesName = sheet.OpenCell("SalesName").Value;//获取提交信息，销售人员姓名
        string amount = sheet.OpenCell("Amount").Value;//获取提交信息，销售金额
        int num = 0;

        if (custName.Trim().Length > 0)
        {
            if (cid.Trim() != "")
            {
                sql = "Update OrderMaster set orderNum = '" + orderId + "',OrderDate = " + date
        + ",CustName='" + custName + "',CustDistrict='" + district + "',SalesName = '" + salesName + "' ,Amount= " + amount;

                sql += "  where Id = " + cid;
            }
            else
            {
                sql = "Insert into OrderMaster values(" + (maxId + 1) + ",'" + orderId + "'," + date + ",'" + custName + "','"
                    + district + "','" + Convert.ToString(Session["UserName"]) + "','" + salesName + "'," + amount + ")";
            }

            try
            {
                cmd.CommandText = sql;
                num = cmd.ExecuteNonQuery();//更新客户信息
            }
            catch (Exception ex)
            {

                strErrHtml += "保存失败，失败原因为：" + ex.Message + "\n";
            }
        }
        else
        {
            if (custName.Trim().Length <= 0)
            {
                strErrHtml += "请输入用户名！\n";
            }
        }

        return num;
    }
}
