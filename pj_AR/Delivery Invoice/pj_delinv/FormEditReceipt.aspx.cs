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

public partial class FormEditReceipt : System.Web.UI.Page
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
    DataTable mlDATATABLE1 = new DataTable();
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
        if (mlDATATABLE.Rows[0]["InvStatus"].ToString() == "PROCEEDS")
        {
            btNewRecord.Visible = false;
            btSaveRecord.Visible = true;
            btCancelOperation.Visible = true;
            btSearchRecord.Visible = false;
            btPrintRecord.Visible = true;
            //mlPNLGRID.Visible = true;
        }
        else
        {
            btNewRecord.Visible = false;
            btSaveRecord.Visible = false;
            btCancelOperation.Visible = false;
            btSearchRecord.Visible = false;
            btPrintRecord.Visible = true;
        }

    }

    public string FormatDateddMMyyyy(DateTime mpDATE)
    {
        string mlTAHUN;
        string mlBULAN;
        string mlHARI;

        mlTAHUN = Microsoft.VisualBasic.DateAndTime.Year(mpDATE).ToString();
        if (Microsoft.VisualBasic.DateAndTime.Month(mpDATE).ToString().Length == 1) { mlBULAN = "0" + Microsoft.VisualBasic.DateAndTime.Month(mpDATE).ToString(); } else { mlBULAN = Microsoft.VisualBasic.DateAndTime.Month(mpDATE).ToString(); }
        if (Microsoft.VisualBasic.DateAndTime.Day(mpDATE).ToString().Length == 1) { mlHARI = "0" + Microsoft.VisualBasic.DateAndTime.Day(mpDATE).ToString(); } else { mlHARI = Microsoft.VisualBasic.DateAndTime.Day(mpDATE).ToString(); }

        return mlHARI + "/" + mlBULAN + "/" + mlTAHUN;
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

        mlTAHUN = Microsoft.VisualBasic.Strings.Right(mpDATE, 4);
        mlBULAN = Microsoft.VisualBasic.Strings.Mid(mpDATE, 4, 2);
        mlHARI = Microsoft.VisualBasic.Strings.Left(mpDATE, 2);

        return mlTAHUN + "-" + mlBULAN + "-" + mlHARI;
    }

    private void RetrieveFieldsDetail()
    {

        mlDATATABLE = (DataTable)Session["FormEditReceiptData"];
        mlTXTRECEIPT.Text = mlDATATABLE.Rows[0]["InvReceiptCode"].ToString();
        mlTXTDATE.Text = FormatDateddMMyyyy(Convert.ToDateTime(mlDATATABLE.Rows[0]["InvPreparedForDate"]));
        mlDDLNIKMESS.SelectedItem.Text = mlDATATABLE.Rows[0]["InvCodeMess"].ToString();
        mlTXTNAMEMESS.Text = mlDATATABLE.Rows[0]["InvMessName"].ToString();
        mlTXTNIKUSER.Text = Convert.ToString(Session["mgUSERID"]);
        mlTXTUSERNAME.Text = Convert.ToString(Session["mgNAME"]);

        if (mlDATATABLE.Rows[0]["InvStatus"].ToString() == "PROCEEDS" && mlDATATABLE.Rows[0]["InvReceiptFlag"].ToString() == "R")
        {
            mlSql_2 = "SELECT * FROM INV_DELIVERY " +
                      "WHERE Disabled = '0' AND InvReceiptFlag = 'R' AND InvReceiptCode = '" + mlDATATABLE.Rows[0]["InvReceiptCode"].ToString()  + "'" +
                      " AND InvPreparedForDate = '" + FormatDateyyyyMMdd(mlTXTDATE.Text) + "' AND InvCodeMess = '" + mlDDLNIKMESS.SelectedItem.Text + "'" +
                      " AND InvMessName = '" + mlTXTNAMEMESS.Text + "' AND CompanyCode = '" + mlDATATABLE.Rows[0]["CompanyCode"].ToString() + "' " +
                      "ORDER BY InvPreparedForDate Desc";
            mlREADER2 = mlOBJGS.DbRecordset(mlSql_2, "PB", mlCOMPANYID);
            mlDATATABLE1 = InsertReaderToDatatable(mlDATATABLE1, mlDATAROW, mlREADER2);
            mlDGEDITRECEIPT.DataSource = mlDATATABLE1;
            mlDGEDITRECEIPT.DataBind();
        }

    }

    private void LoadComboNIK(string mpDISPLAYMEMBER, string mpVALUEMEMBER)
    {
        mlDDLNIKMESS.Items.Clear();

        ListItem mlList = new ListItem();
        mlList.Text = "-- Pilih --";
        mlList.Value = "-- Pilih --";
        mlDDLNIKMESS.Items.Add(mlList);

        mlSQLTEMP = "SELECT * FROM INV_MSTMESS WHERE Disabled = 0 ORDER BY InvCodeMess";
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", mlCOMPANYID);
        while (mlRSTEMP.Read())
        {
            ListItem mlLI = new ListItem();
            mlLI.Text = mlRSTEMP[mpDISPLAYMEMBER].ToString();
            mlLI.Value = mlRSTEMP[mpVALUEMEMBER].ToString();

            mlDDLNIKMESS.Items.Add(mlLI);
        }

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

    private DataTable InsertReaderToDatatable(DataTable mpTABLE, DataRow mpDATAROW, OleDbDataReader mpREADER)
    {
        mpTABLE = createTable(mpTABLE);

        if (mpREADER.HasRows)
        {
            while (mpREADER.Read())
            {
                mpDATAROW = mpTABLE.NewRow();

                mpDATAROW["CompanyCode"] = mpREADER["CompanyCode"].ToString();
                mpDATAROW["InvNo"] = mpREADER["InvNo"].ToString();
                mpDATAROW["InvCustCode"] = mpREADER["InvCustCode"].ToString();
                mpDATAROW["InvCustName"] = mpREADER["InvCustName"].ToString();
                mpDATAROW["InvDate"] = mpREADER["InvDate"].ToString();
                mpDATAROW["InvBranch"] = mpREADER["InvBranch"].ToString();
                mpDATAROW["InvAmount"] = Convert.ToDecimal(mpREADER["InvAmount"]);
                mpDATAROW["InvStatus"] = mpREADER["InvStatus"].ToString();
                mpDATAROW["InvDesc"] = mpREADER["InvDesc"].ToString();
                mpDATAROW["InvReceiptCode"] = mpREADER["InvReceiptCode"].ToString();
                DateTime mlPROCDATE = DateTime.MinValue;
                if (mpREADER["InvProceedsDate"].ToString() == "") { mlPROCDATE = DateTime.MinValue; } else { mlPROCDATE = Convert.ToDateTime(mpREADER["InvProceedsDate"]); }
                mpDATAROW["InvProceedsDate"] = mlPROCDATE;

                DateTime mlPREPDATE = DateTime.MinValue;
                if (mpREADER["InvPreparedForDate"].ToString() == "") { mlPREPDATE = DateTime.MinValue; } else { mlPREPDATE = Convert.ToDateTime(mpREADER["InvPreparedForDate"]); }
                mpDATAROW["InvPreparedForDate"] = mlPREPDATE;

                DateTime mlDELIVDATE = DateTime.MinValue;
                if (mpREADER["InvDeliveredDate"].ToString() == "") { mlDELIVDATE = DateTime.MinValue; } else { mlDELIVDATE = Convert.ToDateTime(mpREADER["InvDeliveredDate"]); }
                mpDATAROW["InvDeliveredDate"] = mlDELIVDATE;

                DateTime mlRETURNDATE = DateTime.MinValue;
                if (mpREADER["InvReturnedDate"].ToString() == "") { mlRETURNDATE = DateTime.MinValue; } else { mlRETURNDATE = Convert.ToDateTime(mpREADER["InvReturnedDate"]); }
                mpDATAROW["InvReturnedDate"] = mlRETURNDATE;

                DateTime mlDONEDATE = DateTime.MinValue;
                if (mpREADER["InvDoneDate"].ToString() == "") { mlDONEDATE = DateTime.MinValue; } else { mlDONEDATE = Convert.ToDateTime(mpREADER["InvDoneDate"]); }
                mpDATAROW["InvDoneDate"] = mlDONEDATE;

                mpDATAROW["InvCustPenerima"] = mpREADER["InvCustPenerima"].ToString();
                mpDATAROW["InvResiTiki"] = mpREADER["InvResiTiki"].ToString();
                mpDATAROW["InvMessName"] = mpREADER["InvMessName"].ToString();
                mpDATAROW["InvCodeMess"] = mpREADER["InvCodeMess"].ToString();
                mpDATAROW["UserName"] = mpREADER["UserName"].ToString();
                mpDATAROW["UserID"] = mpREADER["UserID"].ToString();

                DateTime mlCREATEDDATE = DateTime.MinValue;
                if (mpREADER["InvCreatedDate"].ToString() == "") { mlRETURNDATE = DateTime.MinValue; } else { mlRETURNDATE = Convert.ToDateTime(mpREADER["InvCreatedDate"]); }
                mpDATAROW["InvCreatedDate"] = mlCREATEDDATE;
                mpDATAROW["InvCreatedBy"] = mpREADER["InvCreatedBy"].ToString();

                DateTime mlMODIFIEDDATE = DateTime.MinValue;
                if (mpREADER["InvModifiedDate"].ToString() == "") { mlDONEDATE = DateTime.MinValue; } else { mlDONEDATE = Convert.ToDateTime(mpREADER["InvModifiedDate"]); }
                mpDATAROW["InvModifiedDate"] = mlMODIFIEDDATE;
                mpDATAROW["InvModifiedBy"] = mpREADER["InvModifiedBy"].ToString();

                mpDATAROW["InvSiteCard"] = mpREADER["InvSiteCard"].ToString();
                mpDATAROW["InvProdOffer"] = mpREADER["InvProdOffer"].ToString();
                mpDATAROW["InvOCM"] = mpREADER["InvOCM"].ToString();
                mpDATAROW["InvFCM"] = mpREADER["InvFCM"].ToString();
                mpDATAROW["InvCollector"] = mpREADER["InvCollector"].ToString();
                mpDATAROW["InvReceiptFlag"] = mpREADER["InvReceiptFlag"].ToString();

                mpTABLE.Rows.Add(mpDATAROW);

            }
        }

        return mpTABLE;
    }

    private void SaveRecord()
    {
        string mlPREPDATE = "";

        mlPREPDATE = FormatDateyyyyMMdd(mlTXTDATE.Text);

        try
        {
            mlDATATABLE = (DataTable)Session["FormEditReceiptData"];
            foreach (DataRow mlDATAROW in mlDATATABLE.Rows)
            {
                mlSQL = "UPDATE INV_DELIVERY SET InvReceiptCode = '" + mlTXTRECEIPT.Text + "', " +
                        "InvPreparedForDate = '" + mlPREPDATE + "', InvResiTiki = '" + mlTXTRESITIKI.Text + "', " +
                        "InvCodeMess = '" + mlDDLNIKMESS.SelectedItem.Text + "', InvMessName = '" + mlTXTNAMEMESS.Text + "', " +
                        "InvModifiedDate = GETDATE(), InvModifiedBy = '" + Session["mgUSERID"] + "-" + Session["mgNAME"] + "', " + 
                        "InvReceiptFlag = 'R' " +
                        "WHERE InvNo = '" + mlDATAROW["InvNo"].ToString() + "' AND CompanyCode = '" + mlDATAROW["CompanyCode"].ToString() + "' AND Disabled = '0'";
                mlOBJGS.ExecuteQuery(mlSQL, "PB", mlCOMPANYID);
            }
        }
        catch (Exception ex)
        {
            mlMESSAGE.Text = "OLEDB error : " + ex.Message.ToString();
        }

        if (Session["FormReceiptData"] == null)
        {
            Response.Redirect("~/pageconfirmation.aspx?mpMESSAGE=34FC3624"); //CASE 35
            //Response.Redirect("../pj_ad/ad_login.aspx");
        }
        DisableCancel();
        RetrieveFieldsDetail();

        //mlURL = "../pj_delinv/ReceiptData.aspx?mpFP=1"; //&data=" + Session["FormReceiptData"] + "&userid=" + Session["mgUSERID"] + "&name=" + Session["mgNAME"] + "";

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

    private void printRecord()
    {
        string mlURL = "";

        //mlURL = "../pj_delinv/FormPrint.aspx?mpFP=1&data=" + Session["FormEditReceiptData"] + "&userid=" + Session["mgUSERID"] + "&name=" + Session["mgNAME"] + "";
        mlURL = "../pj_delinv/FormPrintRDLC.aspx?mpFP=1&data=" + Session["FormEditReceiptData"] + "&userid=" + Session["mgUSERID"] + "&name=" + Session["mgNAME"] + "";

        string popup = "<script language='javascript'>" +
            "var left = (screen.width/2)-(600/2); var top = (screen.height/2)-(600/2);" +
            "var newwin; newwin=window.showModalDialog('" + mlURL + "', null, " +
            "'width=600, height=600, top='+top+', left='+left+', scrollbars=yes, resizable=no');" +
            "if (window.focus) { newwin.focus() }" +
            "</script>";
        ((System.Web.UI.Page)Page).ClientScript.RegisterStartupScript(((System.Web.UI.Page)Page).GetType(), "popup", popup);
    
    }

    private void batalReceipt(string mpCOMPCODE, string mpKEYID)
    {
        mlSQLTEMP = "UPDATE INV_DELIVERY SET InvReceiptCode = '', InvCodeMess = '', InvMessName = '', InvReceiptFlag = 'I', " +
                    "InvModifiedDate = GETDATE(), InvModifiedBy = '" + Session["mgUSERID"] + "-" + Session["mgNAME"] + "' " +
                    "WHERE InvNo = '" + mpKEYID + "' AND CompanyCode = '" + mpCOMPCODE + "' AND Disabled = '0'";
        mlOBJGS.ExecuteQuery(mlSQLTEMP, "PB", mlCOMPANYID);

        RetrieveFieldsDetail();
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        mlTITLE.Text = "Form Edit Receipt";
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"] + "Form Edit Receipt";
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

        if (Session["FormEditReceiptData"] == null)
        {
            Response.Redirect("~/pageconfirmation.aspx?mpMESSAGE=34FC3624"); //CASE 35
            //Response.Redirect("../pj_ad/ad_login.aspx");
        }

        if (Page.IsPostBack == false)
        {
            LoadComboNIK("InvCodeMess", "InvMessName");
            RetrieveFieldsDetail();
            DisableCancel();
            mlSHOWTOTAL = false;
            mlSHOWPRICE = false;
            if (Session["mgUSERID"] == null || Session["mgNAME"] == null)
            {
                Response.Redirect("~/pageconfirmation.aspx?mpMESSAGE=34FC3624"); //CASE 35
                //Response.Redirect("../pj_ad/ad_login.aspx");
            }
            else
            {
                mlOBJGS.XM_UserLog(Session["mgUSERID"].ToString(), Session["mgNAME"].ToString(), "monitoring_delivery", "Monitoring Delivery", "");
            }
        }
        else
        {
        }

        string mlURL = "";
        //mlURL = "../pj_delinv/FormPrint.aspx?mpFP=1&data=" + Session["FormEditReceiptData"] + "&userid=" + Session["mgUSERID"] + "&name=" + Session["mgNAME"] + "";
        //btPrintRecord.Attributes.Add("onclick", "pop_up('../pj_delinv/FormPrint.aspx?mpFP=1&data=" + Session["FormEditReceiptData"] + "&userid=" + Session["mgUSERID"] + "&name=" + Session["mgNAME"] + "','FormPrint', 600, 600);return false;");
        mlURL = "../pj_delinv/FormPrintRDLC.aspx?mpFP=1&data=" + Session["FormEditReceiptData"] + "&userid=" + Session["mgUSERID"] + "&name=" + Session["mgNAME"] + "";
        btPrintRecord.Attributes.Add("onclick", "pop_up('../pj_delinv/FormPrintRDLC.aspx?mpFP=1&data=" + Session["FormEditReceiptData"] + "&userid=" + Session["mgUSERID"] + "&name=" + Session["mgNAME"] + "','FormPrint', 800, 600);return false;"); 
    }

    protected void btSaveRecord_Click(Object sender, System.Web.UI.ImageClickEventArgs e)
    {
        SaveRecord();
    }

    protected void btCancelOperation_Click(Object sender, System.Web.UI.ImageClickEventArgs e)
    {
        deleteData();
    }

    protected void btPrintRecord_Click(Object sender, System.Web.UI.ImageClickEventArgs e)
    {
        printRecord();
    }

    protected void mlDDLNIKMESS_SelectedIndexChanged(Object sender, EventArgs e)
    {
        DropDownList mlDDL = (DropDownList)sender;

        mlTXTNAMEMESS.Text = mlDDL.SelectedItem.Value;
    }

    protected void mlDGEDITRECEIPT_PageIndexChanged(Object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        mlDGEDITRECEIPT.CurrentPageIndex = e.NewPageIndex;

        RetrieveFieldsDetail();
    }

    protected void mlDGEDITRECEIPT_ItemCommand(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        
        mlKEY = e.CommandArgument.ToString();
        switch (e.CommandName)
        {
            case "Batal" :
                batalReceipt(e.Item.Cells[1].Text, e.Item.Cells[2].Text);
                break;

            case "CheckedRecord" :
                //mlMESSAGE.Text = "Delete Request for  " & e.CommandArgument;
                break;
            default :
                // Ignore Other
                break;
        }

    }

}