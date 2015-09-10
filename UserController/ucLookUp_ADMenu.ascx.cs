using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;

public partial class UserController_ucLookUp_ADMenu : System.Web.UI.UserControl
{
    public bool FlagEnabledTextBox
    {
        get { return txtMenuParentID.ReadOnly; }
        set { txtMenuParentID.ReadOnly = value; }
    }
    public bool FlagEnabledImageButton
    {
        get { return hpLookup.Enabled; }
        set { hpLookup.Enabled = value; }
    }
    public string MenuParentIDHid
    {
        get { return (string)ViewState["MenuParentIDHid"]; }
        set { ViewState["MenuParentIDHid"] = value; }
    }
    public string MenuParentID
    {
        get { return hdnMenuParentID.Value; }
        set
        {
            hdnMenuParentID.Value = value;
            txtMenuParentID.Text = value;
        }
    }
    public string MenuParentName
    {
        get { return hdnMenuParentName.Value; }
        set
        {
            hdnMenuParentName.Value = value;
            txtMenuParentName.Text = value;
        }
    }
    public string Style
    {
        get { return (string)ViewState["style"]; }
        set { ViewState["style"] = value; }
    }
    public int ChangeTextBoxWidth
    {
        set { txtMenuParentID.Width = value; }
    }

    public void UpdateHidden()
    {
        if (hdnMenuParentID.Value != null)
        {
            if (hdnMenuParentID.Value.Trim() != "")
            {
                txtMenuParentID.Text = hdnMenuParentID.Value;
            }
        }
        if (hdnMenuParentName.Value != null)
        {
            if (hdnMenuParentName.Value.Trim() != "")
            {
                txtMenuParentName.Text = hdnMenuParentName.Value;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        UpdateHidden();
        hpLookup.NavigateUrl = "javascript:OpenWinLookUp('" + hdnMenuParentID.ClientID + "','" + txtMenuParentID.ClientID + "','" + hdnMenuParentName.ClientID + "','" + txtMenuParentName.ClientID + "','" + this.Style + "')";
    }
}