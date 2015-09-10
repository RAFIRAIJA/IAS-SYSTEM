using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usercontroller_ucLookUp_INVCustomer : System.Web.UI.UserControl
{
    public bool FlagEnabledTextBox
    {
        get { return txtCustomerID.ReadOnly; }
        set { txtCustomerID.ReadOnly = value; }
    }
    public bool FlagEnabledImageButton
    {
        get { return hpLookup.Enabled; }
        set { hpLookup.Enabled = value; }
    }
    public string CustomerIDHid
    {
        get { return (string)ViewState["CustomerIDHid"]; }
        set { ViewState["CustomerIDHid"] = value; }
    }
    public string CustomerID
    {
        get { return hdnCustomerID.Value; }
        set
        {
            hdnCustomerID.Value = value;
            txtCustomerID.Text = value;
        }
    }
    public string CustomerName
    {
        get { return hdnCustomerName.Value; }
        set { hdnCustomerName.Value = value;
              txtCustomerName.Text = value;
        }
    }
    public string Style
    {
        get { return (string)ViewState["style"]; }
        set { ViewState["style"] = value; }
    }
    public int ChangeTextBoxWidth
    {
        set { txtCustomerID.Width = value; }
    }


    //public void Validator(bool value)
    // {
    //     RVFCustomer.Enabled = value;
    //     RVFCustomer.Visible = value;
    // }
    //public void ValidatorTrue()
    //{
    //    RVFCustomer.Enabled = true;
    //}

    public void UpdateHidden()
    {
        if (hdnCustomerID.Value != null)
        {
            if (hdnCustomerID.Value.Trim() != "")
            {
                txtCustomerID.Text = hdnCustomerID.Value;
            }
        }
        if (hdnCustomerName.Value != null)
        {
            if (hdnCustomerName.Value.Trim() != "")
            {
                txtCustomerName.Text = hdnCustomerName.Value;
            } 
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        UpdateHidden(); 
        hpLookup.NavigateUrl = "javascript:OpenWinLookUp('" + hdnCustomerID.ClientID + "','" + txtCustomerID.ClientID + "','" + hdnCustomerName.ClientID + "','"+ txtCustomerName.ClientID +"','" + this.Style + "')";
    }


}