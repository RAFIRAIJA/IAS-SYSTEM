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

public partial class pj_delinv_Monitoring : System.Web.UI.Page
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
    string mlCOMPANYCODE = "";
    string mlPARAM_COMPANY = "";

    bool mlDGCHECK = false;
    bool mlPROCEEDCANCEL = false;
    bool mlDGCHECKRECEIPT = false;
    int mlJMLDGCHECK = 0;
    string mlPESEAN = "";

    #region " User Defined Function "

    private void countProceeds(string mpDATEAWAL, string mpDATEAKHIR)
    {
        mlSQL = "SELECT PROCEEDS FROM INV_DELIVERY_STATUS WHERE PROCEEDSDATE BETWEEN '" + mpDATEAWAL + "' AND '" + mpDATEAKHIR + "'";
        mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", mlCOMPANYID);
        if (mlREADER.HasRows)
        {
            while (mlREADER.Read())
            {
                this.mlPROCEEDS.Text = mlREADER["PROCEEDS"].ToString();
            }
        }
        else
        {
            this.mlPROCEEDS.Text = "0";
        }
    }

    private void countDelivered(string mpDATEAWAL, string mpDATEAKHIR)
    {
        mlSQL = "SELECT DELIVERED FROM INV_DELIVERY_STATUS WHERE DELIVEREDDATE BETWEEN '" + mpDATEAWAL + "' AND '" + mpDATEAKHIR + "'";
        mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", mlCOMPANYID);
        if (mlREADER.HasRows)
        {
            while (mlREADER.Read())
            {
                this.mlDELIVERED.Text = mlREADER["DELIVERED"].ToString();
            }
        }
        else
        {
            this.mlDELIVERED.Text = "0";
        }
    }

    private void countReturned(string mpDATEAWAL, string mpDATEAKHIR)
    {
        mlSQL = "SELECT RETURNED FROM INV_DELIVERY_STATUS WHERE RETURNEDDATE BETWEEN '" + mpDATEAWAL + "' AND '" + mpDATEAKHIR + "'";
        mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", mlCOMPANYID);
        if (mlREADER.HasRows)
        {
            while (mlREADER.Read())
            {
                this.mlRETURNED.Text = mlREADER["RETURNED"].ToString();
            }
        }
        else
        {
            this.mlRETURNED.Text = "0";
        }
    }

    private void countDone(string mpDATEAWAL, string mpDATEAKHIR)
    {
        mlSQL = "SELECT DONE FROM INV_DELIVERY_STATUS WHERE DONEDATE BETWEEN '" + mpDATEAWAL + "' AND '" + mpDATEAKHIR + "'";
        mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", mlCOMPANYID);
        if (mlREADER.HasRows)
        {
            while (mlREADER.Read())
            {
                this.mlDONE.Text = mlREADER["DONE"].ToString();
            }
        }
        else
        {
            this.mlDONE.Text = "0";
        }
    }

    private void UploadDataExcel()
    {
        int mlJUMUPLOAD = 0;
        if (mlCOMPANYID == null || mlCOMPANYID == "") { mlCOMPANYID = "ISSP3"; }
        if (FileUpload1.PostedFile.ContentType == "application/vnd.ms-excel" ||
        FileUpload1.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            try
            {
                string fileName = Server.MapPath("~/file/upload_deliv") + "\\" + FileUpload1.PostedFile.FileName;//Path.Combine(Server.MapPath("~/file/upload_deliv"), Guid.NewGuid().ToString() + Path.GetExtension(FileUpload1.PostedFile.FileName));
                FileUpload1.PostedFile.SaveAs(fileName);
                string conString = "";
                string ext = Path.GetExtension(FileUpload1.PostedFile.FileName);
                if (ext.ToLower() == ".xls")
                {
                    conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\""; ;
                }
                else if (ext.ToLower() == ".xlsx")
                {
                    conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                }
                //Invoice No#, Cust# Code, Cust# name, Invoice Date, Branch, Invoice, Site Card, Product Offering, OCM, FCM, collector, code Messenger, Company Code

                OleDbConnection mlCON = new OleDbConnection(conString);
                if (mlCON.State == System.Data.ConnectionState.Closed)
                {
                    mlCON.Open();
                }

                //mlDDLSHEETS.Items.Clear();
                //mlDDLSHEETS.Items.Add(new ListItem("--Select Sheet--", ""));
                //mlDDLSHEETS.DataSource = mlCON.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                //mlDDLSHEETS.DataTextField = "TABLE_NAME";
                //mlDDLSHEETS.DataValueField = "TABLE_NAME";
                //mlDDLSHEETS.DataBind();

                //mlSQLTEMP = "SELECT * FROM [Data$]";
                //mlSQLTEMP = "SELECT * FROM [Sheet1$]";
                mlSQLTEMP = "SELECT * FROM [" + mlCON.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Columns[0].Table.Rows[0]["TABLE_NAME"].ToString().Trim() + "]" ;
               
                OleDbCommand mlDBCMD = new OleDbCommand(mlSQLTEMP, mlCON);
                OleDbDataAdapter mlDA = new OleDbDataAdapter(mlDBCMD);
                DataSet mlDS = new DataSet();
                mlDA.Fill(mlDS);
                mlDA.Dispose();
                mlCON.Close();
                mlCON.Dispose();

                foreach (DataRow mlDR in mlDS.Tables[0].Rows)
                {
                    if (!string.IsNullOrEmpty(mlDR[0].ToString()))
                    {
                        mlSQL = "INSERT INTO INV_DELIVERY (InvNo, InvCustCode, InvCustName, InvDate, InvBranch, InvAmount, InvSiteCard, InvProdOffer, " +
                                "InvOCM, InvFCM, InvCollector, InvCodeMess, InvMessName, InvStatus, InvDesc, InvReceiptCode, InvResiTiki, InvProceedsDate, " +
                                "InvPreparedForDate, InvDeliveredDate, InvReturnedDate, InvDoneDate, InvCustPenerima, UserID, UserName, CompanyCode, InvCreatedDate, InvCreatedBy, " +
                                "InvModifiedDate, InvModifiedBy, InvReceiptFlag, [Disabled]) " +
                                "VALUES ('" + mlDR[0].ToString() + "', '" + mlDR[1].ToString() + "', '" + mlDR[2].ToString() + "', '" + mlDR[3].ToString() + "', " +
                                "'" + mlDR[4].ToString() + "', '" + Convert.ToDecimal(mlDR[5]) + "', '" + mlDR[6].ToString() + "', '" + mlDR[7].ToString() + "', " +
                                "'" + mlDR[8].ToString() + "', '" + mlDR[9].ToString() + "', '" + mlDR[10].ToString() + "', '" + mlDR[11].ToString() + "', " +
                                "'', 'PROCEEDS', '', '', '', CONVERT(DATETIME,CONVERT(nvarchar(10),GETDATE(),120)), CONVERT(DATETIME,CONVERT(nvarchar(10),GETDATE(),120)), " +
                                "null, null, null, '', '" + Session["mgUSERID"].ToString() + "', '" + Session["mgNAME"].ToString() + "', '" + mlDR[12].ToString() + "', GETDATE(), '" + Session["mgUSERID"].ToString() + "-" + Session["mgNAME"].ToString() + "', " + 
                                "GETDATE(), '', 'P', '0')";

                        mlOBJGS.ExecuteQuery(mlSQL, "PB", mlCOMPANYID, false, null);

                        mlJUMUPLOAD = mlJUMUPLOAD + 1;
                    }
                }

                mlMESSAGE.Text = "Uploading data success. " + mlJUMUPLOAD + " Rows inserted";

            }
            catch (Exception ex)
            {
                mlMESSAGE.Text = "Uploading data error : " + ex.Message.ToString();
            }
        }
    }

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
        if (Session["mlFORMRANGEDATE"] != null && Session["FormRangeDate"] != null)
        {
            List<string> mlDATE = new List<string>();

            mlDATATABLE1 = (DataTable)Session["FormRangeDate"];
            mlDATE = (List<string>)Session["mlFORMRANGEDATE"];

            countProceeds(mlDATE[0], mlDATE[1]);
            countDelivered(mlDATE[0], mlDATE[1]);
            countReturned(mlDATE[0], mlDATE[1]);
            countDone(mlDATE[0], mlDATE[1]);

            mlDGWORKSHEET.PageSize = mpPAGESIZE;
            mlDGWORKSHEET.DataSource = mlDATATABLE1;
            mlDGWORKSHEET.DataBind();
        }
        else
        {
            mlSql_2 = "SELECT * FROM INV_DELIVERY WHERE Disabled = '0' ORDER BY InvDate Desc";
            //mlSql_2 = "SELECT * FROM INV_DELIVERY WHERE Disabled = '0' AND CompanyCode = '" + mlCOMPANYCODE + "' ORDER BY InvDate Desc";
            mlREADER2 = mlOBJGS.DbRecordset(mlSql_2, "PB", mlCOMPANYID);
            mlDATATABLE = InsertReaderToDatatable(mlDATATABLE, mlDATAROW, mlREADER2);

            mlDGWORKSHEET.PageSize = mpPAGESIZE;
            mlDGWORKSHEET.DataSource = mlDATATABLE;
            mlDGWORKSHEET.DataBind();
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

                DateTime mlDELIVDATE = DateTime.MinValue ;
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

    private void addData()
    {
        mlDGCHECKRECEIPT = false;
        foreach (DataGridItem mlDG in mlDGWORKSHEET.Items)
        {
            CheckBox mlBOX = (CheckBox)mlDG.FindControl("mlCHECKBOX");
            if (mlBOX.Checked == true)
            {
                if (mlDG.Cells[8].Text == "PROCEEDS" && mlDG.Cells[31].Text == "P")
                {
                    mlSQL = "UPDATE INV_DELIVERY SET InvReceiptFlag = 'I', InvModifiedDate = GETDATE(), InvModifiedBy = '" + Session["mgUSERID"] + "-" + Session["mgNAME"] + "' WHERE InvNo = '" + mlDG.Cells[2].Text + "' AND CompanyCode = '" + mlDG.Cells[1].Text + "' AND InvStatus = 'PROCEEDS' AND InvReceiptFlag = 'P' AND [Disabled] = '0'";
                    mlOBJGS.ExecuteQuery(mlSQL, "PB", mlCOMPANYID);
                }
                else if (mlDG.Cells[8].Text == "PROCEEDS" && mlDG.Cells[31].Text == "R")
                {
                    mlDGCHECKRECEIPT = true;
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
            if (mlDGCHECKRECEIPT == true)
            {
                mlPESEAN = "Data tersebut sudah ada di Receipt Data menunggu Confirm Delivered";
                ((System.Web.UI.Page)Page).ClientScript.RegisterStartupScript(((System.Web.UI.Page)Page).GetType(), "Pesan", "<script type=\"text/javascript\" language=\"javascript\">alert('" + mlPESEAN + "');</script>");
            }
            else
            {
                Response.Redirect("../pj_delinv/InvReceipt.aspx?mpFP=1");
            }
        }
    }

    private void addMoreData()
    {
        string mlURL = "";

        mlDATATABLE1 = createTable(mlDATATABLE1);

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

        mlJMLDGCHECK = 0;
        //mlDGCHECKRECEIPT = false;

        foreach (DataGridItem mlDG in mlDGWORKSHEET.Items)
        {
            CheckBox mlBOX = (CheckBox)mlDG.FindControl("mlCHECKBOX");
            if (mlBOX.Checked == true)
            {
                if (mlDG.Cells[8].Text == "PROCEEDS" && mlDG.Cells[31].Text == "P")
                {
                    mlDATAROW = mlDATATABLE1.NewRow();
                    mlDATAROW["CompanyCode"] = mlDG.Cells[1].Text;
                    mlDATAROW["InvNo"] = mlDG.Cells[2].Text;
                    mlDATAROW["InvCustCode"] = mlDG.Cells[3].Text;
                    mlDATAROW["InvCustName"] = mlDG.Cells[4].Text;

                    mlINVDATE = FormatDateyyyyMMdd(Convert.ToDateTime(mlDG.Cells[5].Text));
                    mlDATAROW["InvDate"] = mlINVDATE;

                    mlDATAROW["InvBranch"] = mlDG.Cells[6].Text;
                    mlDATAROW["InvAmount"] = mlDG.Cells[7].Text;
                    mlDATAROW["InvStatus"] = mlDG.Cells[8].Text;
                    if (mlDG.Cells[9].Text == "&nbsp;") { mlDESC = ""; } else { mlDESC = mlDG.Cells[9].Text; }
                    mlDATAROW["InvDesc"] = mlDESC;
                    mlDATAROW["InvReceiptCode"] = "";

                    //mlINVPROCDATE = FormatDateyyyyMMdd(Convert.ToDateTime(mlDG.Cells[10].Text));
                    mlINVPROCDATE = FormatDateyyyyMMdd(mlDG.Cells[11].Text);
                    mlDATAROW["InvProceedsDate"] = mlINVPROCDATE;
                    //mlINVDELIVDATE = FormatDateyyyyMMdd(Convert.ToDateTime(mlDG.Cells[11].Text));
                    mlINVDELIVDATE = FormatDateyyyyMMdd(mlDG.Cells[12].Text);
                    mlDATAROW["InvDeliveredDate"] = mlINVDELIVDATE;
                    //mlINVRETURNDATE = FormatDateyyyyMMdd(Convert.ToDateTime(mlDG.Cells[12].Text));
                    mlINVRETURNDATE = FormatDateyyyyMMdd(mlDG.Cells[13].Text);
                    mlDATAROW["InvReturnedDate"] = mlINVRETURNDATE;
                    //mlINVDONEDATE = FormatDateyyyyMMdd(Convert.ToDateTime(mlDG.Cells[13].Text));
                    mlINVDONEDATE = FormatDateyyyyMMdd(mlDG.Cells[14].Text);
                    mlDATAROW["InvDoneDate"] = mlINVDONEDATE;

                    if (mlDG.Cells[15].Text == "&nbsp;") { mlCUSTPENERIMA = ""; } else { mlCUSTPENERIMA = mlDG.Cells[15].Text; }
                    mlDATAROW["InvCustPenerima"] = mlCUSTPENERIMA;
                    mlDATAROW["InvCodeMess"] = mlDG.Cells[16].Text;
                    mlDATAROW["InvMessName"] = mlDG.Cells[17].Text;
                    mlDATAROW["UserID"] = mlDG.Cells[18].Text;
                    mlDATAROW["UserName"] = mlDG.Cells[19].Text;

                    //mlINVCREATEDATE = FormatDateyyyyMMdd(Convert.ToDateTime(mlDG.Cells[19].Text));
                    mlINVCREATEDATE = FormatDateyyyyMMdd(mlDG.Cells[20].Text);
                    mlDATAROW["InvCreatedDate"] = mlINVCREATEDATE;
                    if (mlDG.Cells[21].Text == "&nbsp;") { mlCREATEDBY = ""; } else { mlCREATEDBY = mlDG.Cells[21].Text; }
                    mlDATAROW["InvCreatedBy"] = mlCREATEDBY;
                    //mlINVMODIFYDATE = FormatDateyyyyMMdd(Convert.ToDateTime(mlDG.Cells[21].Text));
                    mlINVMODIFYDATE = FormatDateyyyyMMdd(mlDG.Cells[22].Text);
                    mlDATAROW["InvModifiedDate"] = mlINVMODIFYDATE;
                    if (mlDG.Cells[23].Text == "&nbsp;") { mlMODIFIEDBY = ""; } else { mlMODIFIEDBY = mlDG.Cells[23].Text; }
                    mlDATAROW["InvModifiedBy"] = mlMODIFIEDBY;

                    if (mlDG.Cells[24].Text == "&nbsp;") { mlSITECARD = ""; } else { mlSITECARD = mlDG.Cells[24].Text; }
                    mlDATAROW["InvSiteCard"] = mlSITECARD;
                    if (mlDG.Cells[25].Text == "&nbsp;") { mlRESI = ""; } else { mlRESI = mlDG.Cells[25].Text; }
                    mlDATAROW["InvResiTiki"] = mlRESI;

                    //mlINVPREPDATE = FormatDateyyyyMMdd(Convert.ToDateTime(mlDG.Cells[25].Text));
                    mlINVPREPDATE = FormatDateyyyyMMdd(mlDG.Cells[26].Text);
                    mlDATAROW["InvPreparedForDate"] = mlINVPREPDATE;

                    if (mlDG.Cells[27].Text == "&nbsp;") { mlPRODOFFER = ""; } else { mlPRODOFFER = mlDG.Cells[27].Text; }
                    mlDATAROW["InvProdOffer"] = mlPRODOFFER;
                    mlDATAROW["InvOCM"] = mlDG.Cells[28].Text;
                    mlDATAROW["InvFCM"] = mlDG.Cells[29].Text;
                    mlDATAROW["InvCollector"] = mlDG.Cells[30].Text;
                    mlDATAROW["InvReceiptFlag"] = mlDG.Cells[31].Text;

                    mlDATATABLE1.Rows.Add(mlDATAROW);

                    mlJMLDGCHECK = mlJMLDGCHECK + 1;
                }
                //else if (mlDG.Cells[7].Text == "PROCEEDS" && mlDG.Cells[30].Text == "R")
                //{
                //    mlDGCHECKRECEIPT = true;
                //}
            }
        }

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
                //if (mlDGCHECKRECEIPT == true)
                //{
                //    mlPESEAN = "Data tersebut sudah ada di Receipt Data menunggu Confirm Delivered";
                //    ((System.Web.UI.Page)Page).ClientScript.RegisterStartupScript(((System.Web.UI.Page)Page).GetType(), "Pesan", "<script type=\"text/javascript\" language=\"javascript\">alert('" + mlPESEAN + "');</script>");
                //}
                //else
                //{
                    Session["FormAddMoreReceiptData"] = mlDATATABLE1;
                    mlURL = "../pj_delinv/FormAddMoreReceipt.aspx?mpFP=1&data=" + Session["FormAddMoreReceiptData"] + "&userid=" + Session["mgUSERID"] + "&name=" + Session["mgNAME"] + "";

                    string popup = "<script language='javascript'>" +
                       "var left = (screen.width/2)-(600/2); var top = (screen.height/2)-(600/2);" +
                       "var newwin; newwin=window.open('" + mlURL + "', 'FormCreateReceipt', " +
                       "'width=600, height=600, top='+top+', left='+left+', scrollbars=yes, resizable=no');" +
                       "if (window.focus) {newwin.focus()}" +
                       "</script>";
                    ((System.Web.UI.Page)Page).ClientScript.RegisterStartupScript(((System.Web.UI.Page)Page).GetType(), "Popup", popup);
                //}
            }
        }
       
    }

    private void deleteData()
    {
        foreach (DataGridItem mlDG in mlDGWORKSHEET.Items)
        {
            CheckBox mlBOX = (CheckBox)mlDG.FindControl("mlCHECKBOX");
            if (mlBOX.Checked == true)
            {
                mlSQL = "UPDATE INV_DELIVERY SET Disabled = '1', InvReceiptFlag = 'C', InvModifiedDate = GETDATE(), InvModifiedBy = '" + Session["mgUSERID"] + "-" + Session["mgNAME"] + "' WHERE InvNo = '" + mlDG.Cells[2].Text + "' AND CompanyCode = '" + mlDG.Cells[1].Text + "' AND Disabled = '0'";
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
            RetrieveFieldsDetail(Convert.ToInt32(mlDDLPAGESIZE.SelectedItem.Text));
        }
    }

    private void rangeDate()
    {
        string mlURL = "";

        mlURL = "../pj_delinv/FormRangeDate.aspx?rd=false";

        string popup = "<script language='javascript'>" +
           "var left = (screen.width/2)-(600/2); var top = (screen.height/2)-(400/2);" +
           "var newwin; newwin=window.open('" + mlURL + "', 'FormRangeDate', " +
           "'width=600, height=400, top='+top+', left='+left+', scrollbars=yes, resizable=no');" +
           "if (window.focus) {newwin.focus()}" +
           "</script>";
        ((System.Web.UI.Page)Page).ClientScript.RegisterStartupScript(((System.Web.UI.Page)Page).GetType(), "Popup", popup);
    }

    private void rangeDateQuery()
    {
        if (Session["mlFORMRANGEDATE"] != null && Session["FormRangeDate"] != null)
        {
            List<string> mlDATE = new List<string>();

            mlDATATABLE1 = (DataTable)Session["FormRangeDate"];
            mlDATE = (List<string>)Session["mlFORMRANGEDATE"];

            countProceeds(mlDATE[0], mlDATE[1]);
            countDelivered(mlDATE[0], mlDATE[1]);
            countReturned(mlDATE[0], mlDATE[1]);
            countDone(mlDATE[0], mlDATE[1]);

            mlDGWORKSHEET.DataSource = mlDATATABLE1;
            mlDGWORKSHEET.DataBind();

            Session.Remove("FormRangeDate");
            Session.Remove("mlFORMRANGEDATE");
        }
    }

    private void confirmDone()
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

        mlDATATABLE1 = createTable(mlDATATABLE1);
        mlJMLDGCHECK = 0;
        mlPROCEEDCANCEL = false;
        foreach (DataGridItem mlDG in mlDGWORKSHEET.Items)
        {
            CheckBox mlBOX = (CheckBox)mlDG.FindControl("mlCHECKBOX");
            if (mlBOX.Checked == true)
            {
                if (mlDG.Cells[8].Text != "PROCEEDS" && mlDG.Cells[31].Text != "C")
                {
                    mlDATAROW = mlDATATABLE1.NewRow();
                    mlDATAROW["CompanyCode"] = mlDG.Cells[1].Text;
                    mlDATAROW["InvNo"] = mlDG.Cells[2].Text;
                    mlDATAROW["InvCustCode"] = mlDG.Cells[3].Text;
                    mlDATAROW["InvCustName"] = mlDG.Cells[4].Text;

                    //mlINVDATE = FormatDateyyyyMMdd(Convert.ToDateTime(mlDG.Cells[4].Text));
                    mlINVDATE = FormatDateyyyyMMdd(mlDG.Cells[5].Text);
                    mlDATAROW["InvDate"] = mlINVDATE;

                    mlDATAROW["InvBranch"] = mlDG.Cells[6].Text;
                    mlDATAROW["InvAmount"] = mlDG.Cells[7].Text;
                    mlDATAROW["InvStatus"] = mlDG.Cells[8].Text;
                    mlDATAROW["InvDesc"] = mlDG.Cells[9].Text;
                    mlDATAROW["InvReceiptCode"] = mlDG.Cells[10].Text;

                    //mlINVPROCDATE = FormatDateyyyyMMdd(Convert.ToDateTime(mlDG.Cells[10].Text));
                    mlINVPROCDATE = FormatDateyyyyMMdd(mlDG.Cells[11].Text);
                    mlDATAROW["InvProceedsDate"] = mlINVPROCDATE;
                    //mlINVDELIVDATE = FormatDateyyyyMMdd(Convert.ToDateTime(mlDG.Cells[11].Text));
                    mlINVDELIVDATE = FormatDateyyyyMMdd(mlDG.Cells[12].Text);
                    mlDATAROW["InvDeliveredDate"] = mlINVDELIVDATE;
                    //mlINVRETURNDATE = FormatDateyyyyMMdd(Convert.ToDateTime(mlDG.Cells[12].Text));
                    mlINVRETURNDATE = FormatDateyyyyMMdd(mlDG.Cells[13].Text);
                    mlDATAROW["InvReturnedDate"] = mlINVRETURNDATE;
                    //mlINVDONEDATE = FormatDateyyyyMMdd(Convert.ToDateTime(mlDG.Cells[13].Text));
                    mlINVDONEDATE = FormatDateyyyyMMdd(mlDG.Cells[14].Text);
                    mlDATAROW["InvDoneDate"] = mlINVDONEDATE;

                    mlDATAROW["InvCustPenerima"] = mlDG.Cells[15].Text;
                    mlDATAROW["InvCodeMess"] = mlDG.Cells[16].Text;
                    mlDATAROW["InvMessName"] = mlDG.Cells[17].Text;
                    mlDATAROW["UserID"] = mlDG.Cells[18].Text;
                    mlDATAROW["UserName"] = mlDG.Cells[19].Text;

                    //mlINVCREATEDATE = FormatDateyyyyMMdd(Convert.ToDateTime(mlDG.Cells[19].Text));
                    mlINVCREATEDATE = FormatDateyyyyMMdd(mlDG.Cells[20].Text);
                    mlDATAROW["InvCreatedDate"] = mlINVCREATEDATE;
                    mlDATAROW["InvCreatedBy"] = mlDG.Cells[21].Text;
                    //mlINVMODIFYDATE = FormatDateyyyyMMdd(Convert.ToDateTime(mlDG.Cells[21].Text));
                    mlINVMODIFYDATE = FormatDateyyyyMMdd(mlDG.Cells[22].Text);
                    mlDATAROW["InvModifiedDate"] = mlINVMODIFYDATE;
                    mlDATAROW["InvModifiedBy"] = mlDG.Cells[23].Text;


                    mlDATAROW["InvSiteCard"] = mlDG.Cells[24].Text;
                    mlDATAROW["InvResiTiki"] = mlDG.Cells[25].Text;

                    //mlINVPREPDATE = FormatDateyyyyMMdd(Convert.ToDateTime(mlDG.Cells[25].Text));
                    mlINVPREPDATE = FormatDateyyyyMMdd(mlDG.Cells[26].Text);
                    mlDATAROW["InvPreparedForDate"] = mlINVPREPDATE;

                    mlDATAROW["InvProdOffer"] = mlDG.Cells[27].Text;
                    mlDATAROW["InvOCM"] = mlDG.Cells[28].Text;
                    mlDATAROW["InvFCM"] = mlDG.Cells[29].Text;
                    mlDATAROW["InvCollector"] = mlDG.Cells[30].Text;
                    mlDATAROW["InvReceiptFlag"] = mlDG.Cells[31].Text;

                    mlDATATABLE1.Rows.Add(mlDATAROW);
                }
                else
                {
                    mlPROCEEDCANCEL = true;
                }
                mlJMLDGCHECK = mlJMLDGCHECK + 1;

            }
        }

        if (mlPROCEEDCANCEL == false)
        {
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
                        Session["FormWorksheetData"] = mlDATATABLE1;
                        mlURL = "../pj_delinv/FormConfirmDone.aspx?mpFP=1&data=" + Session["FormWorksheetData"] + "&userid=" + Session["mgUSERID"] + "&name=" + Session["mgNAME"] + "";

                        string popup = "<script language='javascript'>" +
                           "var left = (screen.width/2)-(600/2); var top = (screen.height/2)-(400/2);" +
                           "var newwin; newwin=window.open('" + mlURL + "', 'FormConfirmDone', " +
                           "'width=600, height=400, top='+top+', left='+left+', scrollbars=yes, resizable=no');" +
                           "if (window.focus) {newwin.focus()}" +
                           "</script>";
                        ((System.Web.UI.Page)Page).ClientScript.RegisterStartupScript(((System.Web.UI.Page)Page).GetType(), "Popup", popup);
                    }
                }
            }
            else
            {
                mlPESEAN = "Silahkan pilih satu baris!!";
                ((System.Web.UI.Page)Page).ClientScript.RegisterStartupScript(((System.Web.UI.Page)Page).GetType(), "Pesan", "<script type=\"text/javascript\" language=\"javascript\">alert('" + mlPESEAN + "');</script>");
            }
        }
        else
        {
            mlPESEAN = "Ada Data/Data tersebut sudah tidak bisa diproses karena sudah dicancel!!";
            ((System.Web.UI.Page)Page).ClientScript.RegisterStartupScript(((System.Web.UI.Page)Page).GetType(), "Pesan", "<script type=\"text/javascript\" language=\"javascript\">alert('" + mlPESEAN + "');</script>");
        }
    }

    private void SearchDataGrid(int mpPAGESIZE)
    {
        string mlSEARCHFIELD = "";

        mlSEARCHFIELD = mlDDLSEARCH.SelectedItem.Value;

        mlSql_2 = "SELECT * FROM INV_DELIVERY WHERE Disabled = '0' AND " + mlSEARCHFIELD + " LIKE '%" + mlTXTSEARCH.Text + "%' ORDER BY InvDate Desc";
        mlREADER2 = mlOBJGS.DbRecordset(mlSql_2, "PB", mlCOMPANYID);
        mlDATATABLESEARCH = InsertReaderToDatatable(mlDATATABLESEARCH, mlDATAROW, mlREADER2);
        mlDGWORKSHEET.PageSize = mpPAGESIZE;
        mlDGWORKSHEET.DataSource = mlDATATABLESEARCH;
        mlDGWORKSHEET.DataBind();
    }

    private void SortingDataGrid(string mpSORTINGFIELD, int mpPAGESIZE)
    {
        mlSql_2 = "SELECT * FROM INV_DELIVERY WHERE Disabled = '0' ORDER BY " + mpSORTINGFIELD + "";
        mlREADER2 = mlOBJGS.DbRecordset(mlSql_2, "PB", mlCOMPANYID);
        mlDATATABLESEARCH = InsertReaderToDatatable(mlDATATABLESEARCH, mlDATAROW, mlREADER2);
        mlDGWORKSHEET.PageSize = mpPAGESIZE;
        mlDGWORKSHEET.DataSource = mlDATATABLESEARCH;
        mlDGWORKSHEET.DataBind();
    }

    private void PageSizeGrid(int mpPAGESIZE)
    {
        
        mlSql_2 = "SELECT * FROM INV_DELIVERY WHERE Disabled = '0' ORDER BY InvDate Desc";
        mlREADER2 = mlOBJGS.DbRecordset(mlSql_2, "PB", mlCOMPANYID);
        mlDATATABLESEARCH = InsertReaderToDatatable(mlDATATABLESEARCH, mlDATAROW, mlREADER2);

        mlDGWORKSHEET.PageSize = mpPAGESIZE;
        mlDGWORKSHEET.DataSource = mlDATATABLESEARCH;
        mlDGWORKSHEET.DataBind();

    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        mlTITLE.Text = "Monitoring Invoice Distribution";
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"] + "Monitoring Invoice Distribution";
        //string test = mlOBJGF.Encrypt("35", "");
        
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

        countProceeds(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
        countDelivered(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
        countReturned(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
        countDone(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));

        if (Session["mgUSERID"] == null || Session["mgNAME"] == null)
        {
            Response.Redirect("~/pageconfirmation.aspx?mpMESSAGE=34FC35D4");
        }

        if (Page.IsPostBack == false)
        {
            mlSQL = "SELECT CompanyID FROM ADM_ISS.dbo.AD_USERPROFILE WHERE UserID = '" + Session["mgUSERID"].ToString().ToUpper().Trim() + "'";
            mlREADER = mlOBJGS.DbRecordset(mlSQL, "AD", "AD");
            if (mlREADER.HasRows)
            {
                while (mlREADER.Read())
                {
                    mlCOMPANYCODE = mlREADER["CompanyID"].ToString();
                }
            }
            else
            {
                mlCOMPANYCODE = "ISS";
            }
            DisableCancel();
            RetrieveFieldsDetail(Convert.ToInt32(mlDDLPAGESIZE.SelectedItem.Text));
            mlSHOWTOTAL = false;
            mlSHOWPRICE = false;

            mlOBJGS.XM_UserLog(Session["mgUSERID"].ToString(), Session["mgNAME"].ToString(), "monitoring_delivery", "Monitoring Delivery", "");
        }
        else
        {    
        }

        mlDGWORKSHEET.PagerStyle.HorizontalAlign = HorizontalAlign.Left;
        mlDGWORKSHEET.SortCommand += new DataGridSortCommandEventHandler(mlDGWORKSHEET_SortCommand);
    }

    protected void mlBTNUPLOAD_Click(object sender, EventArgs e)
    {
        UploadDataExcel();
        RetrieveFieldsDetail(Convert.ToInt32(mlDDLPAGESIZE.SelectedItem.Text));
        countProceeds(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
        countDelivered(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
        countReturned(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
        countDone(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
    }

    protected void mlBTNCANCEL_Click(object sender, EventArgs e)
    {
        DisableCancel();
        RetrieveFieldsDetail(Convert.ToInt32(mlDDLPAGESIZE.SelectedItem.Text));
    }

    protected void mlBTNADD_Click(object sender, EventArgs e)
    {
        addData();
    }

    protected void mlBTNADDMORE_Click(object sender, EventArgs e)
    {
        addMoreData();
    }

    protected void mlBTNDELETE_Click(object sender, EventArgs e)
    {
        deleteData();
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

    protected void mlBTNDONE_Click(object sender, EventArgs e)
    {
        confirmDone();
    }

    protected void mlBTNRANGEDATE_Click(object sender, EventArgs e)
    {
        rangeDate();
    }

    protected void mlDGWORKSHEET_PageIndexChanged(Object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e) 
    {
        mlDGWORKSHEET.CurrentPageIndex = e.NewPageIndex;

        RetrieveFieldsDetail(Convert.ToInt32(mlDDLPAGESIZE.SelectedItem.Text));
    }

    private void mlDGWORKSHEET_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
    {
       SortingDataGrid(e.SortExpression, Convert.ToInt32(mlDDLPAGESIZE.SelectedItem.Text));
    }

    protected void mlCHECKALL_CheckedChanged(object source, System.EventArgs e)
    {
        if (mlCHECKALL.Checked == true)
        {
            foreach (DataGridItem mlDG in mlDGWORKSHEET.Items)
            {
                CheckBox mlBOX = (CheckBox)mlDG.FindControl("mlCHECKBOX");
                mlBOX.Checked = true;
            }
        }
        else
        {
            foreach (DataGridItem mlDG in mlDGWORKSHEET.Items)
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