using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Text;


public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {
            string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo.mdb";
            string sql = "select * from leaveRecord order by ID DESC";
            OleDbConnection conn = new OleDbConnection(connString);
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            conn.Open();
            cmd.CommandType = CommandType.Text;
            OleDbDataReader Reader = cmd.ExecuteReader();
            StringBuilder strGrid = new StringBuilder();
            if (!Reader.HasRows)
            {
                strGrid.Append("<tr class='XYDataGrid1-table-data-tr'>\r\n");
                strGrid.Append("<td colspan=4 width='100%' height='100' class='XYDataGrid1-data-cell' align='center'>对不起，暂时没有可以操作的文档。\r\n");
                strGrid.Append("</td></tr>\r\n");
            }
            else
            {
                while (Reader.Read())
                {
                    strGrid.Append("<tr onmouseover='onColor(this)' onmouseout='offColor(this)' class='XYDataGrid1-table-data-tr'>\r\n");
                    strGrid.Append("<td width='7%' height='16' bgcolor='' class='XYDataGrid1-data-cell'><div align='center'><image src='images/word.gif' border='0'></image></div></td>\r\n");
                    strGrid.Append("<td width='28%' height='16' bgcolor='' class='XYDataGrid1-data-cell'><div align='left'>" + Reader["Subject"].ToString() + "</div></td>\r\n");
                    strGrid.Append("<td width='20%' height='16' bgcolor='' class='XYDataGrid1-data-cell'><div align='center'><font face='宋体'>" + Reader["SubmitTime"].ToString() + "</font></div></td>\r\n");
                    strGrid.Append("<td width='45%' height='16' bgcolor='' class='XYDataGrid1-data-cell'><div align='center'>\r\n");
                    strGrid.Append("<a class=OPLink href=\"javascript:openDataList('datalist.aspx','" + Reader["ID"].ToString() + "');\")>数据库中字段内容</a>&nbsp;\r\n");
                    strGrid.Append("<a class=OPLink href=\"javascript:SetLinkUrl2('SubmitDataOfDoc.aspx','" + Reader["ID"].ToString() + "');\">用户填写请假条</a>&nbsp;\r\n");
                    strGrid.Append("<a class=OPLink href=\"javascript:SetLinkUrl('GenDoc.aspx','" + Reader["ID"].ToString() + "');\">动态生成格式文档</a>&nbsp;\r\n");
                    strGrid.Append("</div></td></tr>\r\n");
                }
            }
            Reader.Close();
            conn.Close();
            LiteralGrid.Text = strGrid.ToString();

        }
    }
}
