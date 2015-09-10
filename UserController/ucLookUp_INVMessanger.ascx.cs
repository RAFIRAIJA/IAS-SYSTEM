using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usercontroller_ucLookUp_INVMessanger : System.Web.UI.UserControl
{
    public bool FlagEnabledTextBox
    {
        get { return txtMessangerID.ReadOnly; }
        set { txtMessangerID.ReadOnly = value; }
    }
    public bool FlagEnabledImageButton
    {
        get { return hpLookup.Enabled; }
        set { hpLookup.Enabled = value; }
    }
    public string MessangerIDHid
    {
        get { return (string)ViewState["MessangerIDHid"]; }
        set { ViewState["MessangerIDHid"] = value; }
    }
    public string MessangerID
    {
        get { return hdnMessangerID.Value; }
        set
        {
            hdnMessangerID.Value = value;
            txtMessangerID.Text = value;
        }
    }
    public string MessangerName
    {
        get { return hdnMessangerName.Value; }
        set { hdnMessangerName.Value = value; }
    }
    public string Style
    {
        get { return (string)ViewState["style"]; }
        set { ViewState["style"] = value; }
    }
    public int ChangeTextBoxWidth
    {
        set { txtMessangerID.Width = value; }
    }


    //public void Validator(bool value)
    // {
    //     RVFMessanger.Enabled = value;
    //     RVFMessanger.Visible = value;
    // }
    //public void ValidatorTrue()
    //{
    //    RVFMessanger.Enabled = true;
    //}

    public void UpdateHidden()
    {
        if (hdnMessangerID.Value != null)
        {
            if (hdnMessangerID.Value.Trim() != "")
            {
                txtMessangerID.Text = hdnMessangerID.Value;
            }
        }
        if (hdnMessangerName.Value != null)
        {
            if (hdnMessangerName.Value.Trim() != "")
            {
                txtMessangerName.Text = hdnMessangerName.Value;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        UpdateHidden();
        hpLookup.NavigateUrl = "javascript:OpenWinLookUp('" + hdnMessangerID.ClientID + "','" + txtMessangerID.ClientID + "','" + hdnMessangerName.ClientID + "','" + txtMessangerName.ClientID + "','" + this.Style + "')";
    }
}