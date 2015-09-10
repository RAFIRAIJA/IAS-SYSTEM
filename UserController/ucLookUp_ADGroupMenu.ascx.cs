using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;


public partial class UserController_ucLookUp_ADGroupMenu : System.Web.UI.UserControl
{
    public bool FlagEnabledTextBox
    {
        get { return txtGroupMenu.ReadOnly; }
        set { txtGroupMenu.ReadOnly = value; }
    }
    public bool FlagEnabledImageButton
    {
        get { return hpLookup.Enabled; }
        set { hpLookup.Enabled = value; }
    }
    public string GroupMenuHid
    {
        get { return (string)ViewState["GroupMenuHid"]; }
        set { ViewState["GroupMenuHid"] = value; }
    }
    public string GroupMenu
    {
        get { return hdnGroupMenu.Value; }
        set
        {
            hdnGroupMenu.Value = value;
            txtGroupMenu.Text = value;
        }
    }
    
    public string Style
    {
        get { return (string)ViewState["style"]; }
        set { ViewState["style"] = value; }
    }
    public int ChangeTextBoxWidth
    {
        set { txtGroupMenu.Width = value; }
    }

    public void UpdateHidden()
    {
        if (hdnGroupMenu.Value != null)
        {
            if (hdnGroupMenu.Value.Trim() != "")
            {
                txtGroupMenu.Text = hdnGroupMenu.Value;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        UpdateHidden();
        hpLookup.NavigateUrl = "javascript:OpenWinLookUp('" + hdnGroupMenu.ClientID + "','" + txtGroupMenu.ClientID + "','" + this.Style + "')";
    }
}