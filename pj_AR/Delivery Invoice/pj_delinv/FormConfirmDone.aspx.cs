using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;

public partial class FormConfirmDone : System.Web.UI.Page
{
    IASClass.ucmGeneralSystem mlOBJGS = new IASClass.ucmGeneralSystem();
    IASClass.ucmGeneralFunction mlOBJGF = new IASClass.ucmGeneralFunction();
    FunctionLocal mlOBJPJ = new FunctionLocal();

    System.Data.OleDb.OleDbDataReader mlREADER = null;
    string mlSQL = "";

    DataTable mlDATATABLE = new DataTable();
    DataRow mlDATAROW;
    DataColumn mlDCOL = new DataColumn();
    int mlI = 0;

    string mlCOMPANYTABLENAME = "";
    string mlCOMPANYID = "";
    string mlPARAM_COMPANY = "";
    string mlFUNCTIONPARAMETER = "";
    string ddENTITY = "";

    #region " User Defined Function "

    private void LoadComboStatus()
    {
        mlDDLSTATUS.Items.Clear();

        //mlDDLSTATUS.Items.Add("-- Pilih --");
        mlDDLSTATUS.Items.Add("PROCEEDS");
        //mlDDLSTATUS.Items.Add("DELIVERED");
        mlDDLSTATUS.Items.Add("RETURNED");
        mlDDLSTATUS.Items.Add("DONE");
    }

    public string FormatDateyyyyMMdd(DateTime mpDATE)
    {
        string mlTAHUN;
        string mlBULAN;
        string mlHARI;

        mlTAHUN = Microsoft.VisualBasic.DateAndTime.Year(mpDATE).ToString();
        if (Microsoft.VisualBasic.DateAndTime.Month(mpDATE).ToString().Length == 1) { mlBULAN = "0" + Microsoft.VisualBasic.DateAndTime.Month(mpDATE).ToString(); } else { mlBULAN = Microsoft.VisualBasic.DateAndTime.Month(mpDATE).ToString(); }
        if (Microsoft.VisualBasic.DateAndTime.Day(mpDATE).ToString().Length == 1) { mlHARI = "0" + Microsoft.VisualBasic.DateAndTime.Day(mpDATE).ToString(); } else { mlHARI = Microsoft.VisualBasic.DateAndTime.Day(mpDATE).ToString(); }

        return mlTAHUN + "-" + mlBULAN + "-" + mlHARI;
    }

    public string FormatDateyyyyMMdd(string mpDATE)
    {
        string mlTAHUN;
        string mlBULAN;
        string mlHARI;

        mlTAHUN = Microsoft.VisualBasic.Strings.Right(mpDATE, 4);
        mlBULAN = Microsoft.VisualBasic.Strings.Mid(mpDATE, 4, 2);
        mlHARI = Microsoft.VisualBasic.Strings.Left(mpDATE, 2);

        return mlTAHUN + "-" + mlBULAN + "-" + mlHARI;
    }

    private void confirmDone()
    {
        string mlDONEDATE = "";

        if (Session["FormWorksheetData"] == null)
        {
            Response.Redirect("~/pageconfirmation.aspx?mpMESSAGE=34FC3624"); //CASE 35
        }

        try
        {
            mlDONEDATE = FormatDateyyyyMMdd(mlTXTCONFIRMDATE.Text);

            mlDATATABLE = (DataTable)Session["FormWorksheetData"];
            foreach (DataRow mlDATAROW in mlDATATABLE.Rows)
            {
                switch (mlDDLSTATUS.SelectedItem.Text)
                {
                    case "DONE" :
                        mlSQL = "UPDATE INV_DELIVERY SET InvStatus = '" + mlDDLSTATUS.SelectedItem.Text + "', " +
                        "InvDoneDate = '" + mlDONEDATE + "', InvCustPenerima = '" + mlTXTPENERIMA.Text + "', " +
                        "InvDesc = '" + mlTXTDESCR.Text + "', " +
                        "InvModifiedDate = GETDATE(), InvModifiedBy = '" + Session["mgUSERID"] + "-" + Session["mgNAME"] + "' " +
                        "WHERE InvNo = '" + mlDATAROW["InvNo"].ToString() + "' AND CompanyCode = '" + mlDATAROW["CompanyCode"].ToString() + "' AND [Disabled] = '0'";
                        break;
                    case "RETURNED" :
                        mlSQL = "UPDATE INV_DELIVERY SET InvStatus = '" + mlDDLSTATUS.SelectedItem.Text + "', " +
                        "InvReturnedDate = '" + mlDONEDATE + "', InvCustPenerima = '" + mlTXTPENERIMA.Text + "', " +
                        "InvDesc = '" + mlTXTDESCR.Text + "', " +
                        "InvModifiedDate = GETDATE(), InvModifiedBy = '" + Session["mgUSERID"] + "-" + Session["mgNAME"] + "' " +
                        "WHERE InvNo = '" + mlDATAROW["InvNo"].ToString() + "' AND CompanyCode = '" + mlDATAROW["CompanyCode"].ToString() + "' AND [Disabled] = '0'";
                        break;
                    case "PROCEEDS" :
                        mlSQL = "UPDATE INV_DELIVERY SET InvStatus = '" + mlDDLSTATUS.SelectedItem.Text + "', " +
                        "InvProceedsDate = '" + mlDONEDATE + "', InvCustPenerima = '" + mlTXTPENERIMA.Text + "', " +
                        "InvDesc = '" + mlTXTDESCR.Text + "', InvReceiptFlag = 'C'" +
                        "InvModifiedDate = GETDATE(), InvModifiedBy = '" + Session["mgUSERID"] + "-" + Session["mgNAME"] + "' " +
                        "WHERE InvNo = '" + mlDATAROW["InvNo"].ToString() + "' AND CompanyCode = '" + mlDATAROW["CompanyCode"].ToString() + "' AND [Disabled] = '0'";
                        break;
                }
                
                mlOBJGS.ExecuteQuery(mlSQL, "PB", mlCOMPANYID);
            }
        }
        catch(Exception ex)
        {
            mlMESSAGE.Text = "OLEDB error : " + ex.Message.ToString();
        }

        string popup = "<script language='javascript'>" +
                   "window.opener.location.href = window.opener.location.href;" +
                   "window.close();" +
                   "</script>";
        ((System.Web.UI.Page)Page).ClientScript.RegisterStartupScript(((System.Web.UI.Page)Page).GetType(), "Popup", popup);

    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        mlTITLE.Text = "Select Range Date";
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"] + "Select Range Date";
        mlOBJGS.Main();
        if (mlOBJGS.ValidateExpiredDate() == true)
        {
            return;
        }

        if (Session["mgACTIVECOMPANY"] != null)
        {
            mlOBJGS.mgACTIVECOMPANY = Session["mgACTIVECOMPANY"].ToString();
        }
        else
        {
            Session["mgACTIVECOMPANY"] = mlOBJGS.mgCOMPANYDEFAULT;
        }

        if (Request["mpFP"] != null)
        {
            mlPARAM_COMPANY = Request["mpFP"].ToString();
        }
        else
        {
            mlPARAM_COMPANY = "";
        }
        if (string.IsNullOrEmpty(mlPARAM_COMPANY))
            mlPARAM_COMPANY = "1";
        mlFUNCTIONPARAMETER = mlPARAM_COMPANY;

        switch (mlPARAM_COMPANY)
        {
            case "":
            case "1":
                //ddENTITY.Items.Clear();
                ddENTITY = "ISS";
                //ddENTITY.Items.Add("ISS");
                mlTITLE.Text = mlTITLE.Text + " (ISS)";
                break;
            case "2":
                //ddENTITY.Items.Clear();
                ddENTITY = "IFS";
                //ddENTITY.Items.Add("IFS");
                mlTITLE.Text = mlTITLE.Text + " (IFS - Facility Services)";
                break;
            case "3":
                //ddENTITY.Items.Clear();
                ddENTITY = "IPM";
                //ddENTITY.Items.Add("IPM");
                mlTITLE.Text = mlTITLE.Text + " (IPM - Parking Management)";
                break;
            case "4":
                //ddENTITY.Items.Clear();
                ddENTITY = "ICS";
                //ddENTITY.Items.Add("ICS");
                mlTITLE.Text = mlTITLE.Text + " (ICS - Catering Services)";
                break;
        }

        mlCOMPANYTABLENAME = "ISS Servisystem, PT$";
        mlCOMPANYID = mlCOMPANYID;
        switch (ddENTITY)
        {
            case "ISS":
                mlCOMPANYTABLENAME = "ISS Servisystem, PT$";
                //mlCOMPANYID = "ISSN3";
                mlCOMPANYID = "ISSP3";
                break;
            case "IPM":
                mlCOMPANYTABLENAME = "ISS Parking Management$";
                mlCOMPANYID = "IPM3";
                break;
            case "ICS":
                mlCOMPANYTABLENAME = "ISS CATERING SERVICES$";
                mlCOMPANYTABLENAME = "ISS Catering Service 5SP1$";
                mlCOMPANYID = "ICS5";
                break;
            case "IFS":
                mlCOMPANYTABLENAME = "INTEGRATED FACILITY SERVICES$";
                mlCOMPANYID = "IFS3";
                break;
        }

        if (Session["FormWorksheetData"] == null || Session["mgUSERID"] == null || Session["mgNAME"] == null)
        {
            Response.Redirect("~/pageconfirmation.aspx?mpMESSAGE=34FC3624"); //CASE 35
            //Response.Redirect("../pj_ad/ad_login.aspx");
        }

        if (Page.IsPostBack == false)
        {
            LoadComboStatus();
            mlOBJGS.XM_UserLog(Session["mgUSERID"].ToString(), Session["mgNAME"].ToString(), "monitoring_delivery", "Monitoring Delivery", "");
        }
        else
        {
        }
    }

    protected void btDone_Click(object sender, EventArgs e)
    {
        confirmDone();
    }

}