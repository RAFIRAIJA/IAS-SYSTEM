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

public partial class FormRangeDate : System.Web.UI.Page
{
    IASClass.ucmGeneralSystem mlOBJGS = new IASClass.ucmGeneralSystem();
    IASClass.ucmGeneralFunction mlOBJGF = new IASClass.ucmGeneralFunction();
    FunctionLocal mlOBJPJ = new FunctionLocal();

    System.Data.OleDb.OleDbDataReader mlREADER;
    string mlSQL = "";
    System.Data.OleDb.OleDbDataReader mlREADER2;
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
    System.Data.OleDb.OleDbDataReader mlRSTEMP;
    string mlCURRENTDATE = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString();
    string mlCURRENTBVMONTH = DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString();
    bool mlSHOWTOTAL = false;
    bool mlSHOWPRICE = false;
    string mlUSERLEVEL = "";

    string mlCOMPANYTABLENAME = "";
    string mlCOMPANYID = "";
    string mlPARAM_COMPANY = "";

    bool mlFORMRANGEDATE = false;

    #region " User Defined Function "

    private void LoadComboStatus()
    {
        mlDDLSTATUS.Items.Clear();

        //mlDDLSTATUS.Items.Add("-- Pilih --");
        mlDDLSTATUS.Items.Add("PROCEEDS");
        mlDDLSTATUS.Items.Add("DELIVERED");
        mlDDLSTATUS.Items.Add("RETURNED");
        mlDDLSTATUS.Items.Add("DONE");

        mlDDLCOMPCODE.Items.Clear();

        mlSQLTEMP = "SELECT * FROM INV_MSTCOMPANY WHERE Disabled = 0 ORDER BY InvCompanyCode";
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", mlCOMPANYID);
        while (mlRSTEMP.Read())
        {
            ListItem mlLI = new ListItem();
            mlLI.Text = mlRSTEMP["CompanyName"].ToString();
            mlLI.Value = mlRSTEMP["CompanyCode"].ToString();

            mlDDLCOMPCODE.Items.Add(mlLI);
        }

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

    private void rangeDate(string mpSTATUS, string mpCOMPCODE, string mpFROM, string mpTO)
    {
        string mlFROM ="";
        string mlTO ="";
        List<string> mlDATE = new List<string>();

        mlFROM = FormatDateyyyyMMdd(mpFROM);
        mlTO = FormatDateyyyyMMdd(mpTO);

        try
        {

            switch (mpSTATUS)
            {
                case "PROCEEDS":
                    mlSql_2 = "SELECT * FROM INV_DELIVERY WHERE InvStatus = '" + mpSTATUS + "' AND CompanyCode = '" + mpCOMPCODE + "' AND InvProceedsDate BETWEEN '" + mlFROM + "' AND '" + mlTO + "' AND [Disabled] = 0 ORDER BY InvDate Desc";
                    break;
                case "DELIVERED":
                    mlSql_2 = "SELECT * FROM INV_DELIVERY WHERE InvStatus = '" + mpSTATUS + "' AND CompanyCode = '" + mpCOMPCODE + "' AND InvDeliveredDate BETWEEN '" + mlFROM + "' AND '" + mlTO + "' AND [Disabled] = 0 ORDER BY InvDate Desc";
                    break;
                case "RETURNED":
                    mlSql_2 = "SELECT * FROM INV_DELIVERY WHERE InvStatus = '" + mpSTATUS + "' AND CompanyCode = '" + mpCOMPCODE + "' AND InvReturnedDate BETWEEN '" + mlFROM + "' AND '" + mlTO + "' AND [Disabled] = 0 ORDER BY InvDate Desc";
                    break;
                case "DONE":
                    mlSql_2 = "SELECT * FROM INV_DELIVERY WHERE InvStatus = '" + mpSTATUS + "' AND CompanyCode = '" + mpCOMPCODE + "' AND InvDoneDate BETWEEN '" + mlFROM + "' AND '" + mlTO + "' AND [Disabled] = 0 ORDER BY InvDate Desc";
                    break;
            }

            mlDATE.Add (mlFROM);
            mlDATE.Add (mlTO);
            mlDATE.Add ("true");

            mlREADER2 = mlOBJGS.DbRecordset(mlSql_2, "PB", mlCOMPANYID);
            mlDATATABLE = InsertReaderToDatatable(mlDATATABLE, mlDATAROW, mlREADER2);
            Session["FormRangeDate"] = mlDATATABLE;
            Session["mlFORMRANGEDATE"] = mlDATE;
        }
        catch (Exception ex)
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

        if (Session["mgUSERID"] == null || Session["mgNAME"] == null)
        {
            Response.Redirect("~/pageconfirmation.aspx?mpMESSAGE=34FC3624"); //CASE 35
            //Response.Redirect("../pj_ad/ad_login.aspx");
        }

        if (Page.IsPostBack == false)
        {
            LoadComboStatus();
            //DisableCancel();
            mlSHOWTOTAL = false;
            mlSHOWPRICE = false;
            
            mlOBJGS.XM_UserLog(Session["mgUSERID"].ToString(), Session["mgNAME"].ToString(), "monitoring_delivery", "Monitoring Delivery", "");
            
        }
        else
        {
        }
    }

    protected void btDone_Click(Object sender, EventArgs e)
    {
        rangeDate(mlDDLSTATUS.SelectedItem.Text, mlDDLCOMPCODE.SelectedItem.Text, mlTXTFROMDATE.Text, mlTXTTODATE.Text);
    }

}