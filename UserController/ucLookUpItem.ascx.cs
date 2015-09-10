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

public partial class UserController_ucLookUpItem : System.Web.UI.UserControl
{
    public bool FlagEnabledTextBox
    {
        get
        {
            return txtItemNo.ReadOnly;
            return txtItemName.ReadOnly;
        }
        set
        {
            txtItemNo.ReadOnly = value;
            txtItemName.ReadOnly = value;
        }
    }
    public bool FlagEnabledImageButton
    {
        get { return hpLookup.Enabled; }
        set { hpLookup.Enabled = value; }
    }
    public string ItemNo
    {
        get { return hdnItemNo.Value; }
        set
        {
            hdnItemNo.Value = value;
            txtItemNo.Text = value;
        }
    }
    public string ItemName
    {
        get { return hdnItemName.Value; }
        set
        {
            hdnItemName.Value = value;
            txtItemName.Text = value;
        }
    }
    public string Style
    {
        get { return (string)ViewState["style"]; }
        set { ViewState["style"] = value; }
    }
    public int ChangeTextBoxWidth
    {
        set { txtItemNo.Width = value; }
    }

    public void UpdateHidden()
    {
        if (hdnItemNo.Value != null)
        {
            if (hdnItemNo.Value.Trim() != "")
            {
                txtItemNo.Text = hdnItemNo.Value;
            }
        }
        if (hdnItemName.Value != null)
        {
            if (hdnItemName.Value.Trim() != "")
            {
                txtItemName.Text = hdnItemName.Value;
            }
        }        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        UpdateHidden();
        hpLookup.NavigateUrl = "javascript:OpenWinLookUp('" + txtItemNo.ClientID + "','" + txtItemName.ClientID + "','" + hdnItemNo.ClientID + "','" + hdnItemName.ClientID + "','" + this.Style + "')";
    }
}