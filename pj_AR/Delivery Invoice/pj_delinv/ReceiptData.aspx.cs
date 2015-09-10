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
using System.Globalization;
using Microsoft.VisualBasic;

public partial class pj_delinv_ReceiptData : System.Web.UI.Page
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
    DataTable mlDATATABLESEARCH = new DataTable();
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

    bool mlDGCHECK = false;
    int mlJMLDGCHECK = 0;
    string mlPESEAN = "";

    #region " User Defined Function "

    private void DisableCancel()
    {
        btNewRecord.Visible = false;
        btSaveRecord.Visible = false;
        btCancelOperation.Visible = false;
        btSearchRecord.Visible = false;
        mlPNLGRID.Visible = true;

    }

    private void RetrieveFieldsDetail(int mpPAGESIZE)
    {
        mlSql_2 = "SELECT * FROM INV_DELIVERY WHERE Disabled = 0 AND InvReceiptFlag = 'R' ORDER BY InvModifiedDate Desc";

        mlREADER2 = mlOBJGS.DbRecordset(mlSql_2, "PB", mlCOMPANYID);
        mlDATATABLE = InsertReaderToDatatable(mlDATATABLE, mlDATAROW, mlREADER2);

        mlDGRECEIPT.PageSize = mpPAGESIZE;
        mlDGRECEIPT.DataSource = mlDATATABLE;
        mlDGRECEIPT.DataBind();
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

    public string FormatDateyyyyMMdd(String mpDATE)
    {
        string mlTAHUN;
        string mlBULAN;
        string mlHARI;

        mlTAHUN = Microsoft.VisualBasic.Strings.Right(mpDATE, 4);
        mlBULAN = Microsoft.VisualBasic.Strings.Mid(mpDATE, 4, 2);
        mlHARI = Microsoft.VisualBasic.Strings.Left(mpDATE, 2);

        return mlTAHUN + "-" + mlBULAN + "-" + mlHARI;
    }

    private void deleteData()
    {
        mlDGCHECK = false;
        foreach (DataGridItem mlDG in mlDGRECEIPT.Items)
        {
            CheckBox mlBOX = (CheckBox)mlDG.FindControl("mlCHECKBOX");
            if (mlBOX.Checked == true)
            {
                mlSQL = "UPDATE INV_DELIVERY SET InvReceiptFlag = 'I', InvStatus = 'PROCEEDS', InvReceiptCode = '', " +
                        "InvModifiedDate = GETDATE(), InvModifiedBy = '" + Session["mgUSERID"] + "-" + Session["mgNAME"] + "' " +
                        " WHERE InvNo = '" + mlDG.Cells[12].Text + "' AND InvReceiptCode = '" + mlDG.Cells[2].Text + "' AND CompanyCode = '" + mlDG.Cells[1].Text + "' AND Disabled = '0'";
                mlOBJGS.ExecuteQuery(mlSQL, "PB", mlCOMPANYID);

                mlDGCHECK = true;
            }
        }

        if (mlDGCHECK == false)
        {
            mlPESEAN = "Silahkan pilih data terlebih dahulu";
            ((System.Web.UI.Page)Page).ClientScript.RegisterStartupScript(((System.Web.UI.Page)Page).GetType(), "Pesan", "<script type=\"text/javascript\" language=\"javascript\">alert('" + mlPESEAN + "');</script>");
        }
        else
        {
            //Response.Redirect("../pj_delinv/Monitoring.aspx?mpFP=1");
        }
    }

    private void previewData()
    {
        string mlURL = "";
        string mlINVDATE = "";
        string mlINVPROCDATE = "";
        string mlINVDELIVDATE = "";
        string mlINVRETURNDATE = "";
        string mlINVDONEDATE = "";
        string mlINVPREPDATE = "";
        string mlINVCREATEDATE = "";
        string mlINVMODIFYDATE = "";
        string mlDESC = "";
        string mlRESI = "";
        string mlCUSTPENERIMA = "";
        string mlSITECARD = "";
        string mlPRODOFFER = "";
        string mlCREATEDBY = "";
        string mlMODIFIEDBY = "";

        mlDATATABLE1 = createTable(mlDATATABLE1);
        mlJMLDGCHECK = 0;
        foreach (DataGridItem mlDG in mlDGRECEIPT.Items)
        {
            CheckBox mlBOX = (CheckBox)mlDG.FindControl("mlCHECKBOX");
            if (mlBOX.Checked == true)
            {
                mlDATAROW = mlDATATABLE1.NewRow();
                mlDATAROW["CompanyCode"] = mlDG.Cells[1].Text;
                mlDATAROW["InvReceiptCode"] = mlDG.Cells[2].Text;
                mlDATAROW["InvStatus"] = mlDG.Cells[3].Text;
                if (mlDG.Cells[4].Text == "&nbsp;") { mlDESC = ""; } else { mlDESC = mlDG.Cells[4].Text; }
                mlDATAROW["InvDesc"] = mlDESC;

                mlINVPREPDATE = FormatDateyyyyMMdd(mlDG.Cells[5].Text);
                mlDATAROW["InvPreparedForDate"] = mlINVPREPDATE;
                mlINVDELIVDATE = FormatDateyyyyMMdd(mlDG.Cells[6].Text);
                mlDATAROW["InvDeliveredDate"] = mlINVDELIVDATE;

                if (mlDG.Cells[7].Text == "&nbsp;") { mlRESI = ""; } else { mlRESI = mlDG.Cells[7].Text; }
                mlDATAROW["InvResiTiki"] = mlRESI;
                mlDATAROW["InvCodeMess"] = mlDG.Cells[8].Text;
                mlDATAROW["InvMessName"] = mlDG.Cells[9].Text;
                mlDATAROW["UserID"] = mlDG.Cells[10].Text;
                mlDATAROW["UserName"] = mlDG.Cells[11].Text;

                mlDATAROW["InvNo"] = mlDG.Cells[12].Text;
                mlDATAROW["InvCustCode"] = mlDG.Cells[13].Text;
                mlDATAROW["InvCustName"] = mlDG.Cells[14].Text;

                mlINVDATE = FormatDateyyyyMMdd(mlDG.Cells[15].Text);
                mlDATAROW["InvDate"] = mlINVDATE;

                mlDATAROW["InvBranch"] = mlDG.Cells[16].Text;
                mlDATAROW["InvAmount"] = mlDG.Cells[17].Text;

                mlINVPROCDATE = FormatDateyyyyMMdd(mlDG.Cells[18].Text);
                mlDATAROW["InvProceedsDate"] = mlINVPROCDATE;
                mlINVRETURNDATE = FormatDateyyyyMMdd(mlDG.Cells[19].Text);
                mlDATAROW["InvReturnedDate"] = mlINVRETURNDATE;
                mlINVDONEDATE = FormatDateyyyyMMdd(mlDG.Cells[20].Text);
                mlDATAROW["InvDoneDate"] = mlINVDONEDATE;

                if (mlDG.Cells[21].Text == "&nbsp;") { mlCUSTPENERIMA = ""; } else { mlCUSTPENERIMA = mlDG.Cells[21].Text; }
                mlDATAROW["InvCustPenerima"] = mlCUSTPENERIMA;
                if (mlDG.Cells[22].Text == "&nbsp;") { mlSITECARD = ""; } else { mlSITECARD = mlDG.Cells[22].Text; }
                mlDATAROW["InvSiteCard"] = mlSITECARD;
                if (mlDG.Cells[23].Text == "&nbsp;") { mlPRODOFFER = ""; } else { mlPRODOFFER = mlDG.Cells[23].Text; }
                mlDATAROW["InvProdOffer"] = mlPRODOFFER;
                mlDATAROW["InvOCM"] = mlDG.Cells[24].Text;
                mlDATAROW["InvFCM"] = mlDG.Cells[25].Text;
                mlDATAROW["InvCollector"] = mlDG.Cells[26].Text;

                mlINVCREATEDATE = FormatDateyyyyMMdd(mlDG.Cells[27].Text);
                mlDATAROW["InvCreatedDate"] = mlINVCREATEDATE;
                if (mlDG.Cells[28].Text == "&nbsp;") { mlCREATEDBY = ""; } else { mlCREATEDBY = mlDG.Cells[28].Text; }
                mlDATAROW["InvCreatedBy"] = mlCREATEDBY;
                mlINVMODIFYDATE = FormatDateyyyyMMdd(mlDG.Cells[29].Text);
                mlDATAROW["InvModifiedDate"] = mlINVMODIFYDATE;
                if (mlDG.Cells[30].Text == "&nbsp;") { mlMODIFIEDBY = ""; } else { mlMODIFIEDBY = mlDG.Cells[30].Text; }
                mlDATAROW["InvModifiedBy"] = mlMODIFIEDBY;

                mlDATAROW["InvReceiptFlag"] = mlDG.Cells[31].Text;

                mlDATATABLE1.Rows.Add(mlDATAROW);

                mlJMLDGCHECK = mlJMLDGCHECK + 1;
            }
        }

        if (mlJMLDGCHECK == 1)
        {
            if (mlDATATABLE1 == null)
            {
                mlPESEAN = "Silahkan pilih data terlebih dahulu";
                ((System.Web.UI.Page)Page).ClientScript.RegisterStartupScript(((System.Web.UI.Page)Page).GetType(), "Pesan", "<script type=\"text/javascript\" language=\"javascript\">alert('" + mlPESEAN + "');</script>");
            }
            else
            {
                if (mlDATATABLE1.Rows.Count == 0)
                {
                    mlPESEAN = "Silahkan pilih data terlebih dahulu";
                    ((System.Web.UI.Page)Page).ClientScript.RegisterStartupScript(((System.Web.UI.Page)Page).GetType(), "Pesan", "<script type=\"text/javascript\" language=\"javascript\">alert('" + mlPESEAN + "');</script>");
                }
                else
                {
                    Session["FormEditReceiptData"] = mlDATATABLE1;
                    mlURL = "../pj_delinv/FormEditReceipt.aspx?mpFP=1&data=" + Session["FormEditReceiptData"] + "&userid=" + Session["mgUSERID"] + "&name=" + Session["mgNAME"] + "";

                    string popup = "<script language='javascript'>" +
                       "var left = (screen.width/2)-(600/2); var top = (screen.height/2)-(600/2);" +
                       "var newwin; newwin=window.open('" + mlURL + "', 'FormEditReceipt', " +
                       "'width=600, height=600, top='+top+', left='+left+', scrollbars=yes, resizable=no');" +
                       "if (window.focus) {newwin.focus()}" +
                       "</script>";
                    ((System.Web.UI.Page)Page).ClientScript.RegisterStartupScript(((System.Web.UI.Page)Page).GetType(), "Popup", popup);

                    //((System.Web.UI.Page)Page).ClientScript.RegisterStartupScript(((System.Web.UI.Page)Page).GetType(), "popup", "pop_up(" + mlURL + ", 'FormEditReceipt', '600', '600');", true);
                    //Page.ClientScript.RegisterStartupScript(GetType(), "popup", "openwindow(" + mlURL + ", 'FormEditReceipt', '600', '600');", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "popup", "javascript:openwindow(" + mlURL + ", 'FormEditReceipt', 600, 600);", true);
                }
            }
        }
        else
        {
            mlPESEAN = "Silahkan pilih satu baris!!";
            ((System.Web.UI.Page)Page).ClientScript.RegisterStartupScript(((System.Web.UI.Page)Page).GetType(), "Pesan", "<script type=\"text/javascript\" language=\"javascript\">alert('" + mlPESEAN + "');</script>");
        }
    }

    private void confirmDelivered()
    {
        mlDGCHECK = false;
        foreach (DataGridItem mlDG in mlDGRECEIPT.Items)
        {
            CheckBox mlBOX = (CheckBox)mlDG.FindControl("mlCHECKBOX");
            if (mlBOX.Checked == true)
            {
                if (mlDG.Cells[3].Text == "PROCEEDS" && mlDG.Cells[31].Text == "R")
                {
                    mlSQL = "UPDATE INV_DELIVERY SET InvStatus = 'DELIVERED', InvDeliveredDate = CONVERT(DATETIME,CONVERT(nvarchar(10),GETDATE(),120)), " +
                            "InvModifiedDate = GETDATE(), InvModifiedBy = '" + Session["mgUSERID"] + "-" + Session["mgNAME"] + "' " +
                            "WHERE InvReceiptCode = '" + mlDG.Cells[2].Text + "' AND InvNo = '" + mlDG.Cells[13].Text + "' AND CompanyCode = '" + mlDG.Cells[1].Text + "'";
                    mlOBJGS.ExecuteQuery(mlSQL, "PB", mlCOMPANYID);
                }
                mlDGCHECK = true;
            }
        }

        if (mlDGCHECK == false)
        {
            mlPESEAN = "Silahkan pilih data terlebih dahulu";
            ((System.Web.UI.Page)Page).ClientScript.RegisterStartupScript(((System.Web.UI.Page)Page).GetType(), "Pesan", "<script type=\"text/javascript\" language=\"javascript\">alert('" + mlPESEAN + "');</script>");
        }
        else
        {
        }

        RetrieveFieldsDetail(Convert.ToInt32(mlDDLPAGESIZE.SelectedItem.Text));
    }

    protected string GetPostBackScript()
    {
        PostBackOptions opts = new PostBackOptions(btnPostback);
        //Page.ClientScript.RegisterForEventValidation(options);
        //Return Page.ClientScript.GetPostBackEventReference(options);
        Page.ClientScript.RegisterForEventValidation(opts);
        return Page.ClientScript.GetPostBackEventReference(opts);
    }

    private void SearchDataGrid(int mpPAGESIZE)
    {
        string mlSEARCHFIELD = "";

        mlSEARCHFIELD = mlDDLSEARCH.SelectedItem.Value;

        mlSql_2 = "SELECT * FROM INV_DELIVERY WHERE Disabled = '0' AND InvReceiptFlag = 'R' AND " + mlSEARCHFIELD + " LIKE '%" + mlTXTSEARCH.Text + "%' ORDER BY InvDate Desc";
        mlREADER2 = mlOBJGS.DbRecordset(mlSql_2, "PB", mlCOMPANYID);
        mlDATATABLESEARCH = InsertReaderToDatatable(mlDATATABLESEARCH, mlDATAROW, mlREADER2);

        mlDGRECEIPT.PageSize = mpPAGESIZE;
        mlDGRECEIPT.DataSource = mlDATATABLESEARCH;
        mlDGRECEIPT.DataBind();
    }

    private void PageSizeGrid(int mpPAGESIZE)
    {

        mlSql_2 = "SELECT * FROM INV_DELIVERY WHERE Disabled = '0' AND InvReceiptFlag = 'R' ORDER BY InvDate Desc";
        mlREADER2 = mlOBJGS.DbRecordset(mlSql_2, "PB", mlCOMPANYID);
        mlDATATABLESEARCH = InsertReaderToDatatable(mlDATATABLESEARCH, mlDATAROW, mlREADER2);

        mlDGRECEIPT.PageSize = mpPAGESIZE;
        mlDGRECEIPT.DataSource = mlDATATABLESEARCH;
        mlDGRECEIPT.DataBind();

    }

    private void SortingDataGrid(string mpSORTINGFIELD, int mpPAGESIZE)
    {
        mlSql_2 = "SELECT * FROM INV_DELIVERY WHERE Disabled = 0 AND InvReceiptFlag = 'R' ORDER BY " + mpSORTINGFIELD + "";
        mlREADER2 = mlOBJGS.DbRecordset(mlSql_2, "PB", mlCOMPANYID);
        mlDATATABLESEARCH = InsertReaderToDatatable(mlDATATABLESEARCH, mlDATAROW, mlREADER2);

        mlDGRECEIPT.PageSize = mpPAGESIZE;
        mlDGRECEIPT.DataSource = mlDATATABLESEARCH;
        mlDGRECEIPT.DataBind();
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        mlTITLE.Text = "Monitoring Invoice Distribution";
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"] + "Monitoring Invoice Distribution";
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
            Response.Redirect("~/pageconfirmation.aspx?mpMESSAGE=34FC35D4"); //CASE 31
        }

        if (Page.IsPostBack == false)
        {
            DisableCancel();
            RetrieveFieldsDetail(Convert.ToInt32(mlDDLPAGESIZE.SelectedItem.Text));
            mlSHOWTOTAL = false;
            mlSHOWPRICE = false;
            
            mlOBJGS.XM_UserLog(Session["mgUSERID"].ToString(), Session["mgNAME"].ToString(), "monitoring_delivery", "Monitoring Delivery", "");
        }

        mlDGRECEIPT.PagerStyle.HorizontalAlign = HorizontalAlign.Left;
        mlDGRECEIPT.SortCommand += new DataGridSortCommandEventHandler(mlDGRECEIPT_SortCommand);
    }

    protected void mlBTNPREVIEW_Click(object sender, EventArgs e)
    {
        previewData();
    }

    protected void mlBTNCONFIRMDELIVERED_Click(object sender, EventArgs e)
    {
        confirmDelivered();
    }

    protected void mlBTNDELETE_Click(object sender, EventArgs e)
    {
        deleteData();
        RetrieveFieldsDetail(Convert.ToInt32(mlDDLPAGESIZE.SelectedItem.Text));
    }

    protected void btClear_Click(object sender, EventArgs e)
    {
        mlTXTSEARCH.Text = "";
        RetrieveFieldsDetail(Convert.ToInt32(mlDDLPAGESIZE.SelectedItem.Text));
    }

    protected void btSearch_Click(Object sender, System.Web.UI.ImageClickEventArgs e)
    {
        SearchDataGrid(Convert.ToInt32(mlDDLPAGESIZE.SelectedItem.Text));
    }

    protected void mlDGRECEIPT_PageIndexChanged(Object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        mlDGRECEIPT.CurrentPageIndex = e.NewPageIndex;

        RetrieveFieldsDetail(Convert.ToInt32(mlDDLPAGESIZE.SelectedItem.Text));
    }

    private void mlDGRECEIPT_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
    {
        SortingDataGrid(e.SortExpression, Convert.ToInt32(mlDDLPAGESIZE.SelectedItem.Text));
    }

    protected void mlCHECKALL_CheckedChanged(object source, System.EventArgs e)
    {
        if (mlCHECKALL.Checked == true)
        {
            foreach (DataGridItem mlDG in mlDGRECEIPT.Items)
            {
                CheckBox mlBOX = (CheckBox)mlDG.FindControl("mlCHECKBOX");
                mlBOX.Checked = true;
            }
        }
        else
        {
            foreach (DataGridItem mlDG in mlDGRECEIPT.Items)
            {
                CheckBox mlBOX = (CheckBox)mlDG.FindControl("mlCHECKBOX");
                mlBOX.Checked = false;
            }
        }
    }

    protected void mlDDLPAGESIZE_SelectedIndexChanged(Object sender, EventArgs e)
    {
        DropDownList mlDDL = (DropDownList)sender;

        PageSizeGrid(Convert.ToInt32(mlDDL.SelectedItem.Text));

    }

}