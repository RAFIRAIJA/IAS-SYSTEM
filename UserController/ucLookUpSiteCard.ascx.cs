using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class UserController_ucLookUpSiteCard : System.Web.UI.UserControl
{
    public bool FlagEnabledTextBox {
        get { return txtJobNo.ReadOnly;
        return txtJobTaskNo.ReadOnly;
            }
        set { txtJobNo.ReadOnly = value;
        txtJobTaskNo.ReadOnly = value;
        }
    }
    public bool FlagEnabledImageButton
    {
        get { return hpLookup.Enabled; }
        set { hpLookup.Enabled = value; }
    }
    public string JobNo {
        get { return hdnJobNo.Value; }
        set 
        { hdnJobNo.Value = value;
            txtJobNo.Text = value;
        }
    }
    public string JobTaskNo
    {
        get { return hdnJobTaskNo.Value; }
        set
        {
            hdnJobTaskNo.Value = value;
            txtJobTaskNo.Text = value;
        }
    }
    public string SiteCardID
    {
        get { return hdnSiteCardID.Value; }
        set
        {
            hdnSiteCardID.Value = value;
            lblSiteCard.Text = value;
        }
    }
    public string SiteCardName
    {
        get { return hdnSiteCardName.Value; }
        set
        {
            hdnSiteCardName.Value = value;
            lblSiteCardName.Text = value;
        }
    }

    public string Style {
        get { return (string)ViewState["style"]; }
        set { ViewState["style"] = value; }
    }
    public int ChangeTextBoxWidth
    {
        set { txtJobNo.Width = value; }
    }

    public void UpdateHidden()
    {
        if (hdnJobNo.Value != null)
        {
            if (hdnJobNo.Value.Trim() != "")
            {
                txtJobNo.Text = hdnJobNo.Value;
            }
        }
        if (hdnJobTaskNo.Value != null)
        {
            if (hdnJobTaskNo.Value.Trim() != "")
            {
                txtJobTaskNo.Text = hdnJobTaskNo.Value;
            }
        }
        if (hdnSiteCardID.Value != null)
        {
            if (hdnSiteCardID.Value.Trim() != "")
            {
                lblSiteCard.Text = hdnSiteCardID.Value;
            }
        }
        if (hdnSiteCardName.Value != null)
        {
            if (hdnSiteCardName.Value.Trim() != "")
            {
                lblSiteCardName.Text = hdnSiteCardName.Value;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        UpdateHidden();
        hpLookup.NavigateUrl = "javascript:OpenWinLookUp('" + lblSiteCard.ClientID + "','" + lblSiteCardName.ClientID + "','" + hdnSiteCardID.ClientID + "','" + hdnSiteCardName.ClientID + "','" + txtJobNo.ClientID + "','" + txtJobTaskNo.ClientID + "','" + hdnJobNo.ClientID + "','" + hdnJobTaskNo.ClientID + "," + this.Style + "')";
    }

}
