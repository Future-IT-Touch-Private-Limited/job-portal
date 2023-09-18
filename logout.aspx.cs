using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Response.Cookies["userli"].Domain = "agovtjobs.in";
            Response.Cookies["userli"].Expires = DateTime.Now.AddYears(-5);
        }
        catch (Exception ex)
        { }
        Response.Redirect("https://www.agovtjobs.in/adm/login.aspx");
    }
}