using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoginUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // menu ini buat daftarin User E-Paper
        Response.Redirect("~/pj_AD/ad_userlogin.aspx");
    }
}