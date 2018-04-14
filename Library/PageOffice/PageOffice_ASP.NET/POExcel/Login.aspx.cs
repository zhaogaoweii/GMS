using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack)
        {
            if ((Request.Form["TextUserName"].ToString() == "admin") && (Request.Form["TextPassword"].ToString() == "123"))
            {
                Session["UserName"] = "admin";
                Response.Redirect("OrderList.aspx", true);
            }
        }
    }
}
