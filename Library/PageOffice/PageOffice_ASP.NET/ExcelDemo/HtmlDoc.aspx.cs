using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Text;
using System.Data;

public partial class HtmlDoc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strType = Request.QueryString["type"];
        string docID = Request.QueryString["ID"];
        string docFile = "";

        string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo.mdb";
        OleDbConnection conn = new OleDbConnection(connString);
        conn.Open();
        string sql = "select * from " + strType + " where id=" + docID;

        OleDbCommand cmd = new OleDbCommand(sql, conn);
        OleDbDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            docFile = reader["FileName"].ToString();
            reader.Close();
        }

        docFile = docFile.Substring(0, docFile.Length - 3) + "mht";

        sql = "update " + strType + " set  HtmlFile='" + docFile + "' where id=" + docID;

        OleDbCommand cmm = new OleDbCommand(sql, conn);
        cmm.ExecuteNonQuery();
        conn.Close();
        Response.Redirect("doc/" + docFile);
    }
}
