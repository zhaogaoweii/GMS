using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Text;
using System.Data;
using System.Configuration;

public partial class htmldoc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string docID = Request.QueryString["ID"];
        string docFile = "";

        string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo_word.mdb";
        OleDbConnection conn = new OleDbConnection(connectionString);
        conn.Open();
        string sql = "select * from word where id=" + docID;
        
        OleDbCommand cmd = new OleDbCommand(sql, conn);
        OleDbDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            docFile = reader["FileName"].ToString();
            reader.Close();
        }

        docFile = docFile.Substring(0, docFile.Length - 3) + "mht";

        sql = "update word set  HtmlFile='" + docFile + "' where id=" + docID;
        OleDbCommand cmm = new OleDbCommand(sql, conn);
        cmm.ExecuteNonQuery();
        conn.Close();
        Response.Redirect("doc/" + docFile);
    }
}
