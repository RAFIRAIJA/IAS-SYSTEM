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

public partial class FormAddMoreReceipt : System.Web.UI.Page
{
    IASClass.ucmGeneralSystem mlOBJGS = new IASClass.ucmGeneralSystem();
    IASClass.ucmGeneralFunction mlOBJGF = new IASClass.ucmGeneralFunction();
    FunctionLocal mlOBJPJ = new FunctionLocal();

    System.Data.OleDb.OleDbDataReader mlREADER = null;
    string mlSQL = "";
    System.Data.OleDb.OleDbDataReader mlREADER2 = null;
    string mlSql_2 = "";
    string mlKEY = "";
    string mlKEY2 = "";
    string mlKEY3 = "";
    string mlRECORDSTATUS = "";
    string mlSPTYPE = "";
    string mlFUNCTIONPARAMETER = "";
    string ddENTITY = "";

    DataTable mlDATATABLE = new DataTable();
    DataRow mlDATAROW;
    DataColumn mlDCOL = new DataColumn();
    int mlI = 0;

    string mlSQLTEMP = "";
    System.Data.OleDb.OleDbDataReader mlRSTEMP = null;
    string mlCURRENTDATE = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString();
    string mlCURRENTBVMONTH = DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString();
    bool mlSHOWTOTAL = false;
    bool mlSHOWPRICE = false;
    string mlUSERLEVEL = "";

    string mlCOMPANYTABLENAME = "";
    string mlCOMPANYID = "";
    string mlPARAM_COMPANY = "";

    #region " User Defined Function "

    private void DisableCancel()
    {
        btNewRecord.Visible = false;
        btSaveRecord.Visible = false;
        btCancelOperation.Visible = false;
        btSearchRecord.Visible = false;
        mlTABLEADDMORERECEIPT.Visible = false;

    }

    public string FormatDateddMMyyyy(DateTime mpDATE)
    {
        string mlTAHUN;
        string mlBULAN;
        string mlHARI;

        mlTAHUN = Microsoft.VisualBasic.DateAndTime.Year(mpDATE).ToString();
        if (Microsoft.VisualBasic.DateAndTime.Month(mpDATE).ToString().Length == 1) { mlBULAN = "0" + Microsoft.VisualBasic.DateAndTime.Month(mpDATE).ToString(); } else { mlBULAN = Microsoft.VisualBasic.DateAndTime.Month(mpDATE).ToString(); }
        if (Microsoft.VisualBasic.DateAndTime.Day(mpDATE).ToString().Length == 1) { mlHARI = "0" + Microsoft.VisualBasic.DateAndTime.Day(mpDATE).ToString(); } else { mlHARI = Microsoft.VisualBasic.DateAndTime.Day(mpDATE).ToString(); }
        
        return mlHARI  + "/" + mlBULAN  + "/" + mlTAHUN ;
    }

    public string FormatDateddMMyyyy(string mpDATE)
    {
        string mlTAHUN;
        string mlBULAN;
        string mlHARI;

        mlTAHUN = Microsoft.VisualBasic.Strings.Left(mpDATE, 4);
        mlBULAN = Microsoft.VisualBasic.Strings.Mid(mpDATE, 4, 2);
        mlHARI = Microsoft.VisualBasic.Strings.Right(mpDATE, 2);

        return mlHARI + "/" + mlBULAN + "/" + mlTAHUN;
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

        mlTAHUN = Microsoft.VisualBasic.Strings.Right(mpDATE,4);
        mlBULAN = Microsoft.VisualBasic.Strings.Mid(mpDATE,4,2);
        mlHARI = Microsoft.VisualBasic.Strings.Left(mpDATE, 2);
        
        return mlTAHUN + "-" + mlBULAN + "-" + mlHARI;
    }

    private void RetrieveFieldsDetail()
    {
        mlDATATABLE = (DataTable)Session["FormAddMoreReceiptData"];
    }    

    private System.Data.DataTable createTable(System.Data.DataTable mpTBLFIELD)
    {
        mpTBLFIELD = new DataTable();
        mlSQLTEMP = "SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'INV_DELIVERY'";
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", mlCOMPANYID);
        if (mlRSTEMP.HasRows)
        {
            mlDCOL = new DataColumn("CHECK", typeof(System.Boolean));
            mlDCOL.DefaultValue = false;
            mpTBLFIELD.Columns.Add(mlDCOL);

            while (mlRSTEMP.Read())
            {
                if (mlRSTEMP["DATA_TYPE"].ToString() == "varchar" || mlRSTEMP["DATA_TYPE"].ToString() == "nvarchar" || mlRSTEMP["DATA_TYPE"].ToString() == "char" || mlRSTEMP["DATA_TYPE"].ToString() == "nchar" || mlRSTEMP["DATA_TYPE"].ToString() == "ntext")
                {
                    mlDCOL = new DataColumn(mlRSTEMP["COLUMN_NAME"].ToString(), typeof(System.String));
                    mlDCOL.DefaultValue = "";
                }
                else if (mlRSTEMP["DATA_TYPE"].ToString() == "decimal" || mlRSTEMP["DATA_TYPE"].ToString() == "numeric" || mlRSTEMP["DATA_TYPE"].ToString() == "float")
                {
                    mlDCOL = new DataColumn(mlRSTEMP["COLUMN_NAME"].ToString(), typeof(System.Decimal));
                    mlDCOL.DefaultValue = 0;
                }
                else if (mlRSTEMP["DATA_TYPE"].ToString() == "bigint" || mlRSTEMP["DATA_TYPE"].ToString() == "int" || mlRSTEMP["DATA_TYPE"].ToString() == "smallint")
                {
                    mlDCOL = new DataColumn(mlRSTEMP["COLUMN_NAME"].ToString(), typeof(System.Int64));
                    mlDCOL.DefaultValue = 0;
                }
                else if (mlRSTEMP["DATA_TYPE"].ToString() == "tinyint")
                {
                    mlDCOL = new DataColumn(mlRSTEMP["COLUMN_NAME"].ToString(), typeof(System.Boolean));
                    mlDCOL.DefaultValue = 0;
                }
                else if (mlRSTEMP["DATA_TYPE"].ToString() == "datetime" || mlRSTEMP["DATA_TYPE"].ToString() == "smalldatetime" || mlRSTEMP["DATA_TYPE"].ToString() == "timestamp")
                {
                    mlDCOL = new DataColumn(mlRSTEMP["COLUMN_NAME"].ToString(), typeof(System.DateTime));
                    mlDCOL.DefaultValue = DateTime.Now;
                }

                if (!string.IsNullOrEmpty(mlRSTEMP["DATA_TYPE"].ToString()))
                {
                    mpTBLFIELD.Columns.Add(mlDCOL);
                }
            }

        }

        return mpTBLFIELD;
    }

    private void InsertDataFromReader(OleDbDataReader mpREADER)
    {

        if (mpREADER.HasRows)
        {
            while (mpREADER.Read())
            {

                mlTXTRECEIPT.Text  = mpREADER["InvReceiptCode"].ToString();
                
                DateTime mlPREPDATE = DateTime.MinValue;
                if (mpREADER["InvPreparedForDate"].ToString() == "") { mlPREPDATE = DateTime.MinValue; } else { mlPREPDATE = Convert.ToDateTime(mpREADER["InvPreparedForDate"]); }
                mlTXTDATE.Text = FormatDateddMMyyyy(mlPREPDATE);

                mlTXTRESITIKI.Text  = mpREADER["InvResiTiki"].ToString();
                mlTXTNIKMESS.Text = mpREADER["InvCodeMess"].ToString();
                mlTXTNAMEMESS.Text  = mpREADER["InvMessName"].ToString();
                mlTXTNIKUSER .Text=mpREADER["UserID"].ToString();
                mlTXTUSERNAME.Text =mpREADER["UserName"].ToString();

                if (mpREADER["InvStatus"].ToString() == "DELIVERED")
                {
                    btSaveRecord.Visible = false;
                    btCancelOperation.Visible = false;
                    mlLBLMESSAGE.Text = "Status Sudah DELIVERED !!!";
                }
                else if (mpREADER["InvStatus"].ToString() == "DONE")
                {
                    btSaveRecord.Visible = false;
                    btCancelOperation.Visible = false;
                    mlLBLMESSAGE.Text = "Status Sudah DONE !!!";
                }
                else if (mpREADER["InvStatus"].ToString() == "RETURNED")
                {
                    btSaveRecord.Visible = false;
                    btCancelOperation.Visible = false;
                    mlLBLMESSAGE.Text = "Status Sudah RETURNED !!!";
                }
                else
                {
                    btSaveRecord.Visible = true;
                    btCancelOperation.Visible = true;
                }

            }
        }
    }

    private void checkData()
    {
        mlSql_2 = "SELECT DISTINCT InvReceiptCode, InvPreparedForDate, InvResiTiki, InvCodeMess, InvMessName, UserID, UserName, CompanyCode, InvStatus, InvReceiptFlag FROM INV_DELIVERY WHERE Disabled = 0 AND InvReceiptCode = '" + mlTXTADDRECEIPTCODE.Text + "' AND InvReceiptFlag = 'R'";
        mlREADER2 = mlOBJGS.DbRecordset(mlSql_2, "PB", mlCOMPANYID);
        InsertDataFromReader(mlREADER2);

        mlTABLEADDMORERECEIPT.Visible = true;
    }

    private void SaveRecord()
    {
        string mlPREPDATE = "";

        if (Session["FormAddMoreReceiptData"] == null)
        {
            Response.Redirect("~/pageconfirmation.aspx?mpMESSAGE=34FC3624"); //CASE 35
        }

        try
        {
            mlPREPDATE = FormatDateyyyyMMdd(mlTXTDATE.Text);
            mlDATATABLE = (DataTable)Session["FormAddMoreReceiptData"];
            foreach (DataRow mlDATAROW in mlDATATABLE.Rows)
            {
                mlSQL = "UPDATE INV_DELIVERY SET InvReceiptCode = '" + mlTXTADDRECEIPTCODE.Text + "', " +
                        "InvPreparedForDate = '" + mlPREPDATE + "', InvResiTiki = '" + mlTXTRESITIKI.Text + "', " +
                        "InvCodeMess = '" + mlTXTNIKMESS.Text + "', InvMessName = '" + mlTXTNAMEMESS.Text + "', " +
                        "InvModifiedDate = GETDATE(), InvModifiedBy = '" + Session["mgUSERID"] + "-" + Session["mgNAME"] + "', " +
                        "InvReceiptFlag = 'R'" +
                        "WHERE InvNo = '" + mlDATAROW["InvNo"].ToString() + "' AND CompanyCode = '" + mlDATAROW["CompanyCode"].ToString() + "'AND Disabled = '0'";
                mlOBJGS.ExecuteQuery(mlSQL, "PB", mlCOMPANYID);
            }
        }
        catch (Exception ex)
        {
            mlMESSAGE.Text = "OLEDB error : " + ex.Message.ToString();
        }

        //DisableCancel();
        //RetrieveFieldsDetail();

        string popup = "<script language='javascript'>" +
                   "window.opener.location.href = window.opener.location.href;" +
                   "window.close();" +
                   "</script>";
        ((System.Web.UI.Page)Page).ClientScript.RegisterStartupScript(((System.Web.UI.Page)Page).GetType(), "Popup", popup);
    }

    private void deleteData()
    {
        string popup = "<script language='javascript'>" +
                   "window.close();" +
                   "</script>";
        ((System.Web.UI.Page)Page).ClientScript.RegisterStartupScript(((System.Web.UI.Page)Page).GetType(), "Popup", popup);
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        mlTITLE.Text = "Form Add More Receipt";
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"] + "Form Add More Receipt";

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

        if ((Session["FormReceiptData"] == null) || (Session["mgUSERID"] == null) || (Session["mgNAME"] == null))
        {
            Response.Redirect("~/pageconfirmation.aspx?mpMESSAGE=34FC3624"); //CASE 35
        }

        if (Page.IsPostBack == false)
        {
            DisableCancel();
            RetrieveFieldsDetail();
            mlSHOWTOTAL = false;
            mlSHOWPRICE = false;
            mlOBJGS.XM_UserLog(Session["mgUSERID"].ToString(), Session["mgNAME"].ToString(), "monitoring_delivery", "Monitoring Delivery", "");   
        }

    }

    protected void btSaveRecord_Click(Object sender, System.Web.UI.ImageClickEventArgs e)
    {
        SaveRecord();
    }

    protected void btCancelOperation_Click(Object sender, System.Web.UI.ImageClickEventArgs e)
    {
        deleteData();
    }

    protected void btCheck_Click(object sender, EventArgs e)
    {
        checkData();
    }

}