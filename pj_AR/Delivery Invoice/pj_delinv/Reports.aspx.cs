using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Net;

public partial class pj_delinv_Reports : System.Web.UI.Page
{
    IASClass.ucmGeneralSystem mlOBJGS = new IASClass.ucmGeneralSystem();
    IASClass.ucmGeneralFunction mlOBJGF = new IASClass.ucmGeneralFunction();
    IASClass.ucmFileSystem mlOBJFS = new IASClass.ucmFileSystem();
    FunctionLocal mlOBJPJ = new FunctionLocal();

    System.Data.OleDb.OleDbDataReader mlREADER;
    string mlSQL = "";
    System.Data.OleDb.OleDbDataReader mlREADER2;
    string mlSQL2 = "";
    System.Data.OleDb.OleDbDataReader mlRSTEMP;
    string mlSQLTEMP = "";

    DataTable mlDATATABLE = new DataTable();
    DataTable mlDATATABLE1 = new DataTable();
    DataRow mlDATAROW;
    DataColumn mlDCOL = new DataColumn();

    string ddENTITY = "";
    string mlFUNCTIONPARAMETER = "";
    string mlCOMPANYTABLENAME = "";
    string mlCOMPANYID = "";
    string mlPARAM_COMPANY = "";
    string mlKEY = "";
    string mlMEMBERGROUP = "";
    int mlI = 0;
    string mlMRUSERLEVEL = "";
    string mlCOMPANYNAME = "";
    string mlCOMPANYADDRESS = "";

    #region " User Defined Function "

    private void DisableCancel()
    {
        btNewRecord.Visible = false;
        btSaveRecord.Visible = false;
        btCancelOperation.Visible = false;
        btSearchRecord.Visible = false;
        mlPNLGRID.Visible = true;

    }

    private void LoadComboData(string mpDISPLAYMEMBER, string mpVALUEMEMBER)
    {
        mlDDLBRANCH.Items.Clear();

        System.Web.UI.WebControls.ListItem mlList = new System.Web.UI.WebControls.ListItem();
        mlList.Text = "-- Pilih --";
        mlList.Value = "-- Pilih --";
        mlDDLBRANCH.Items.Add(mlList);

        mlSQLTEMP = "SELECT DISTINCT InvBranch FROM PROD_ISS_NAV.dbo.INV_DELIVERY";
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", mlCOMPANYID);
        while (mlRSTEMP.Read())
        {
            System.Web.UI.WebControls.ListItem mlLI = new System.Web.UI.WebControls.ListItem();
            mlLI.Text = mlRSTEMP[mpDISPLAYMEMBER].ToString();
            mlLI.Value = mlRSTEMP[mpVALUEMEMBER].ToString();

            mlDDLBRANCH.Items.Add(mlLI);
        }

    }

    public void RetrieveCompanyInfo()
    {

        mlCOMPANYNAME = System.Configuration.ConfigurationManager.AppSettings["mgCOMPANYDESC"].ToString();
        mlCOMPANYADDRESS = System.Configuration.ConfigurationManager.AppSettings["mgCOMPANYADDR1"].ToString() + "\r\n" +
                                System.Configuration.ConfigurationManager.AppSettings["mgCOMPANYADDR2"].ToString() + ", " +
                                System.Configuration.ConfigurationManager.AppSettings["mgCOMPANYTOWN"].ToString() + "-" +
                                System.Configuration.ConfigurationManager.AppSettings["mgCOMPANYPOISSODE"].ToString() + "\r\n" +
                                "Phone: " + System.Configuration.ConfigurationManager.AppSettings["mgCOMPANYPHONE1"].ToString() + " " +
                                "Faxs: " + System.Configuration.ConfigurationManager.AppSettings["mgCOMPANYFAXS"].ToString() + "\r\n" +
                                "Email:" + System.Configuration.ConfigurationManager.AppSettings["mgCOMPANYEMAIL"].ToString() + " " +
                                "Website:" + System.Configuration.ConfigurationManager.AppSettings["mgCOMPANYWEB"].ToString();

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

        //mlTAHUN = Microsoft.VisualBasic.Strings.Right(mpDATE, 4);
        //mlHARI = Microsoft.VisualBasic.Strings.Mid(mpDATE, 4, 2);
        //mlBULAN = Microsoft.VisualBasic.Strings.Left(mpDATE, 2);

        return mlTAHUN + "-" + mlBULAN + "-" + mlHARI;
    }

    private System.Data.DataTable createTable(System.Data.DataTable mpTBLFIELD)
    {
        mpTBLFIELD = new DataTable();
        if (mlDDLREPORTNAME.SelectedItem.Value != "InvDeliverySummary.rdlc")
        {
            mlSQLTEMP = "SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'INV_DELIVERY_STATUS_BRANCH'";
        }
        else
        {
            mlSQLTEMP = "SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'INV_DELIVERY_STATUS'";
        }
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", mlCOMPANYID);
        if (mlRSTEMP.HasRows)
        {
            //mlDATAROW = mpTBLFIELD.NewRow();

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
                    //mlDATAROW[mlRSTEMP["COLUMN_NAME"].ToString()] = mlDCOL.DefaultValue;
                }
            }

            //mpTBLFIELD.Rows.Add(mlDATAROW);
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

                if (mlDDLREPORTNAME.SelectedItem.Value == "InvDeliverySummary.rdlc")
                {
                    mpDATAROW["PROCEEDS"] = Convert.ToInt32(mpREADER["PROCEEDS"].ToString());
                    mpDATAROW["DELIVERED"] = Convert.ToInt32(mpREADER["DELIVERED"].ToString());
                    mpDATAROW["RETURNED"] = Convert.ToInt32(mpREADER["RETURNED"].ToString());
                    mpDATAROW["DONE"] = Convert.ToInt32(mpREADER["DONE"].ToString());

                    DateTime mlPROCDATE = DateTime.MinValue;
                    if (mpREADER["PROCEEDSDATE"].ToString() == "") { mlPROCDATE = DateTime.MinValue; } else { mlPROCDATE = Convert.ToDateTime(mpREADER["PROCEEDSDATE"]); }
                    mpDATAROW["PROCEEDSDATE"] = mlPROCDATE;

                    DateTime mlDELIVDATE = DateTime.MinValue;
                    if (mpREADER["DELIVEREDDATE"].ToString() == "") { mlDELIVDATE = DateTime.MinValue; } else { mlDELIVDATE = Convert.ToDateTime(mpREADER["DELIVEREDDATE"]); }
                    mpDATAROW["DELIVEREDDATE"] = mlDELIVDATE;

                    DateTime mlRETURNDATE = DateTime.MinValue;
                    if (mpREADER["RETURNEDDATE"].ToString() == "") { mlRETURNDATE = DateTime.MinValue; } else { mlRETURNDATE = Convert.ToDateTime(mpREADER["RETURNEDDATE"]); }
                    mpDATAROW["RETURNEDDATE"] = mlRETURNDATE;

                    DateTime mlDONEDATE = DateTime.MinValue;
                    if (mpREADER["DONEDATE"].ToString() == "") { mlDONEDATE = DateTime.MinValue; } else { mlDONEDATE = Convert.ToDateTime(mpREADER["DONEDATE"]); }
                    mpDATAROW["DONEDATE"] = mlDONEDATE;
                }
                else
                {
                    mpDATAROW["PROCEEDS"] = Convert.ToInt32(mpREADER["PROCEEDS"].ToString());
                    mpDATAROW["DELIVERED"] = Convert.ToInt32(mpREADER["DELIVERED"].ToString());
                    mpDATAROW["RETURNED"] = Convert.ToInt32(mpREADER["RETURNED"].ToString());
                    mpDATAROW["DONE"] = Convert.ToInt32(mpREADER["DONE"].ToString());

                    DateTime mlPROCDATE = DateTime.MinValue;
                    if (mpREADER["PROCEEDSDATE"].ToString() == "") { mlPROCDATE = DateTime.MinValue; } else { mlPROCDATE = Convert.ToDateTime(mpREADER["PROCEEDSDATE"]); }
                    mpDATAROW["PROCEEDSDATE"] = mlPROCDATE;

                    DateTime mlDELIVDATE = DateTime.MinValue;
                    if (mpREADER["DELIVEREDDATE"].ToString() == "") { mlDELIVDATE = DateTime.MinValue; } else { mlDELIVDATE = Convert.ToDateTime(mpREADER["DELIVEREDDATE"]); }
                    mpDATAROW["DELIVEREDDATE"] = mlDELIVDATE;

                    DateTime mlRETURNDATE = DateTime.MinValue;
                    if (mpREADER["RETURNEDDATE"].ToString() == "") { mlRETURNDATE = DateTime.MinValue; } else { mlRETURNDATE = Convert.ToDateTime(mpREADER["RETURNEDDATE"]); }
                    mpDATAROW["RETURNEDDATE"] = mlRETURNDATE;

                    DateTime mlDONEDATE = DateTime.MinValue;
                    if (mpREADER["DONEDATE"].ToString() == "") { mlDONEDATE = DateTime.MinValue; } else { mlDONEDATE = Convert.ToDateTime(mpREADER["DONEDATE"]); }
                    mpDATAROW["DONEDATE"] = mlDONEDATE;

                    mpDATAROW["BRANCH"] = mpREADER["BRANCH"].ToString();
                }

                mpTABLE.Rows.Add(mpDATAROW);

            }
        }

        return mpTABLE;
    }

    private void RetrieveFieldsDetailLocal()
    {
        string mlCRITERIA = "";
        string mlTANGGALFROM = "";
        string mlTANGGALTO = "";
        
        mlTANGGALFROM = FormatDateyyyyMMdd(mlTXTTANGGALFROM.Text);
        mlTANGGALTO = FormatDateyyyyMMdd(mlTXTTANGGALTO.Text);

        if ((mlTANGGALFROM != "") || (mlTANGGALTO != ""))
        {
            mlCRITERIA = "((PROCEEDSDATE BETWEEN '" + mlTANGGALFROM + "' AND '" + mlTANGGALTO + "') OR (DELIVEREDDATE BETWEEN '" + mlTANGGALFROM + "' AND '" + mlTANGGALTO + "') " +
                         "OR (RETURNEDDATE BETWEEN '" + mlTANGGALFROM + "' AND '" + mlTANGGALTO + "') OR (DONEDATE BETWEEN '" + mlTANGGALFROM + "' AND '" + mlTANGGALTO + "')) ";
        }

        if (mlDDLREPORTNAME.SelectedItem.Value != "InvDeliverySummary.rdlc")
        {
            if (mlDDLBRANCH.SelectedItem.Value != "-- Pilih --")
            {
                mlCRITERIA = mlCRITERIA + " AND BRANCH = '" + mlDDLBRANCH.SelectedItem.Value + "'";
            }

            mlSQL2 = "SELECT * FROM INV_DELIVERY_STATUS_BRANCH " +
                 "WHERE " + mlCRITERIA + "";
            mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", mlCOMPANYID);
            mlDATATABLE = InsertReaderToDatatable(mlDATATABLE, mlDATAROW, mlREADER2);

        }
        else
        {
            mlSQL2 = "SELECT * FROM INV_DELIVERY_STATUS " +
                     "WHERE " + mlCRITERIA + "";
            mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", mlCOMPANYID);
            mlDATATABLE = InsertReaderToDatatable(mlDATATABLE, mlDATAROW, mlREADER2);
        }

        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/pj_delinv/" + mlDDLREPORTNAME.SelectedItem.Value + "");

        Microsoft.Reporting.WebForms.ReportParameter mlCOMPNAME = new Microsoft.Reporting.WebForms.ReportParameter("CompanyName", mlCOMPANYNAME);
        Microsoft.Reporting.WebForms.ReportParameter mlCOMPADDR = new Microsoft.Reporting.WebForms.ReportParameter("CompanyAddress", mlCOMPANYADDRESS);
        Microsoft.Reporting.WebForms.ReportParameter mlTITLE = new Microsoft.Reporting.WebForms.ReportParameter("Title", "REPORTING MONITORING DELIVERY INVOICE");

        //Microsoft.Reporting.WebForms.ReportDataSource mlDATASOURCE = new Microsoft.Reporting.WebForms.ReportDataSource("INV_RECEIPT_DATAH", mlDATATABLE);
        Microsoft.Reporting.WebForms.ReportDataSource mlDATASOURCE = new Microsoft.Reporting.WebForms.ReportDataSource();
        Microsoft.Reporting.WebForms.LocalReport mlREPORTH = new Microsoft.Reporting.WebForms.LocalReport();

        if (mlDDLREPORTNAME.SelectedItem.Value != "InvDeliverySummary.rdlc")
        {
            mlDATASOURCE.Name = "INV_DELIVERY_STATUSBRANCH";
        }
        else
        {
            mlDATASOURCE.Name = "INV_DELIVERY_STATUS";
        }
        mlDATASOURCE.Value = mlDATATABLE;
        mlREPORTH.ReportEmbeddedResource = "~/pj_delinv/" + mlDDLREPORTNAME.SelectedItem.Value + "";
        mlREPORTH.DataSources.Add(mlDATASOURCE);

        //ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDataSource);
        ReportViewer1.LocalReport.ReportEmbeddedResource = mlREPORTH.ReportEmbeddedResource;
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(mlDATASOURCE);
        ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { mlCOMPNAME, mlCOMPADDR, mlTITLE });

        ReportViewer1.LocalReport.Refresh();

    }

    private void RetrieveFieldsDetailPDF()
    {
        string mlTANGGAL = "";
        string mlRECEIVEDCODE = "";
        string mlRECEIVEDBY = "";

        Warning[] warnings;
        string[] streamIds;
        string mimeType = string.Empty;
        string encoding = string.Empty;
        string extension = string.Empty;
        string filename = "";

        mlDATATABLE = (DataTable)Session["FormEditReceiptData"];
        mlTANGGAL = FormatDateyyyyMMdd(Convert.ToDateTime(mlDATATABLE.Rows[0]["InvPreparedForDate"]));
        mlRECEIVEDCODE = mlDATATABLE.Rows[0]["InvCodeMess"].ToString();
        mlRECEIVEDBY = mlDATATABLE.Rows[0]["InvMessName"].ToString();

        if (mlDATATABLE.Rows[0]["InvStatus"].ToString() == "PROCEEDS" && mlDATATABLE.Rows[0]["InvReceiptFlag"].ToString() == "R")
        {
            mlSQL2 = "SELECT * FROM INV_DELIVERY " +
                      "WHERE Disabled = 0 AND InvReceiptFlag = 'R' AND InvPreparedForDate = '" + mlTANGGAL + "' AND InvCodeMess = '" + mlRECEIVEDCODE + "' AND InvMessName = '" + mlRECEIVEDBY + "' " +
                      "ORDER BY InvPreparedForDate Desc";
            mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", mlCOMPANYID);
            mlDATATABLE1 = InsertReaderToDatatable(mlDATATABLE1, mlDATAROW, mlREADER2);
        }

        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/pj_delinv/InvReceiptH.rdlc");

        Microsoft.Reporting.WebForms.ReportParameter mlRECEIPTCODE = new Microsoft.Reporting.WebForms.ReportParameter("ReceiptCode", mlDATATABLE.Rows[0]["InvReceiptCode"].ToString());
        Microsoft.Reporting.WebForms.ReportParameter mlCODEMESS = new Microsoft.Reporting.WebForms.ReportParameter("CodeMess", mlDATATABLE.Rows[0]["InvCodeMess"].ToString());
        Microsoft.Reporting.WebForms.ReportParameter mlCOMPNAME = new Microsoft.Reporting.WebForms.ReportParameter("CompanyName", mlCOMPANYNAME);
        Microsoft.Reporting.WebForms.ReportParameter mlCOMPADDR = new Microsoft.Reporting.WebForms.ReportParameter("CompanyAddress", mlCOMPANYADDRESS);
        Microsoft.Reporting.WebForms.ReportParameter mlTITLE = new Microsoft.Reporting.WebForms.ReportParameter("Title", "INVOICE / RECEIPT");

        Microsoft.Reporting.WebForms.ReportDataSource mlDATASOURCE = new Microsoft.Reporting.WebForms.ReportDataSource();
        Microsoft.Reporting.WebForms.LocalReport mlREPORTH = new Microsoft.Reporting.WebForms.LocalReport();

        mlDATASOURCE.Name = "INV_RECEIPT_DATAH";
        mlDATASOURCE.Value = mlDATATABLE;
        mlREPORTH.ReportEmbeddedResource = "~/pj_delinv/InvReceiptH.rdlc";
        mlREPORTH.DataSources.Add(mlDATASOURCE);

        ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDataSource);
        ReportViewer1.LocalReport.ReportEmbeddedResource = mlREPORTH.ReportEmbeddedResource;
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(mlDATASOURCE);
        ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { mlRECEIPTCODE, mlCODEMESS, mlCOMPNAME, mlCOMPADDR, mlTITLE });

        byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

        filename = "InvReceipt";
        extension = "pdf";
        // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
        Response.Buffer = true;
        Response.Clear();
        Response.ContentType = mimeType;
        Response.AddHeader("content-disposition", "attachment; filename=" + filename + "." + extension);
        Response.BinaryWrite(bytes); // create the file
        Response.Flush(); // send it to the client to download
        Response.End();
        //Body.Size.Width + Report.Margins.Left + Report.Margins.Right <= Report.PageSize.Width

    }

    private void RetrieveFieldsDetailPDF1()
    {
        string mlTANGGAL = "";
        string mlRECEIVEDCODE = "";
        string mlRECEIVEDBY = "";

        Warning[] warnings;
        string[] streamIds;
        string mimeType = string.Empty;
        string encoding = string.Empty;
        string extension = string.Empty;
        string filename = "";
        string deviceInfo =
        "<DeviceInfo>" +
        "  <OutputFormat>EMF</OutputFormat>" +
        "  <PageWidth>8.27in</PageWidth>" +
        "  <PageHeight>11.69in</PageHeight>" +
        "  <MarginTop>0.7874in</MarginTop>" +
        "  <MarginLeft>0.98425in</MarginLeft>" +
        "  <MarginRight>0.98425in</MarginRight>" +
        "  <MarginBottom>0.7874in</MarginBottom>" +
        "</DeviceInfo>";

        mlDATATABLE = (DataTable)Session["FormEditReceiptData"];
        mlTANGGAL = FormatDateyyyyMMdd(Convert.ToDateTime(mlDATATABLE.Rows[0]["InvPreparedForDate"]));
        mlRECEIVEDCODE = mlDATATABLE.Rows[0]["InvCodeMess"].ToString();
        mlRECEIVEDBY = mlDATATABLE.Rows[0]["InvMessName"].ToString();

        if (mlDATATABLE.Rows[0]["InvStatus"].ToString() == "PROCEEDS" && mlDATATABLE.Rows[0]["InvReceiptFlag"].ToString() == "R")
        {
            mlSQL2 = "SELECT * FROM INV_DELIVERY " +
                      "WHERE Disabled = 0 AND InvReceiptFlag = 'R' AND InvPreparedForDate = '" + mlTANGGAL + "' AND InvCodeMess = '" + mlRECEIVEDCODE + "' AND InvMessName = '" + mlRECEIVEDBY + "' " +
                      "ORDER BY InvPreparedForDate Desc";
            mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", mlCOMPANYID);
            mlDATATABLE1 = InsertReaderToDatatable(mlDATATABLE1, mlDATAROW, mlREADER2);
        }

        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/pj_delinv/InvReceiptH.rdlc");

        Microsoft.Reporting.WebForms.ReportParameter mlRECEIPTCODE = new Microsoft.Reporting.WebForms.ReportParameter("ReceiptCode", mlDATATABLE.Rows[0]["InvReceiptCode"].ToString());
        Microsoft.Reporting.WebForms.ReportParameter mlCODEMESS = new Microsoft.Reporting.WebForms.ReportParameter("CodeMess", mlDATATABLE.Rows[0]["InvCodeMess"].ToString());
        Microsoft.Reporting.WebForms.ReportParameter mlCOMPNAME = new Microsoft.Reporting.WebForms.ReportParameter("CompanyName", mlCOMPANYNAME);
        Microsoft.Reporting.WebForms.ReportParameter mlCOMPADDR = new Microsoft.Reporting.WebForms.ReportParameter("CompanyAddress", mlCOMPANYADDRESS);
        Microsoft.Reporting.WebForms.ReportParameter mlTITLE = new Microsoft.Reporting.WebForms.ReportParameter("Title", "INVOICE / RECEIPT");

        Microsoft.Reporting.WebForms.ReportDataSource mlDATASOURCE = new Microsoft.Reporting.WebForms.ReportDataSource();
        Microsoft.Reporting.WebForms.LocalReport mlREPORTH = new Microsoft.Reporting.WebForms.LocalReport();

        mlDATASOURCE.Name = "INV_RECEIPT_DATAH";
        mlDATASOURCE.Value = mlDATATABLE;
        mlREPORTH.ReportEmbeddedResource = "~/pj_delinv/InvReceiptH.rdlc";
        mlREPORTH.DataSources.Add(mlDATASOURCE);

        ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDataSource);
        ReportViewer1.LocalReport.ReportEmbeddedResource = mlREPORTH.ReportEmbeddedResource;
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(mlDATASOURCE);
        ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { mlRECEIPTCODE, mlCODEMESS, mlCOMPNAME, mlCOMPADDR, mlTITLE });

        //byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
        byte[] bytes = ReportViewer1.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out extension, out streamIds, out warnings);

        string path = Server.MapPath("~/pj_delinv/Print_Files");

        // Open PDF File in Web Browser 
        filename = "InvReceipt";
        extension = "pdf";

        FileStream file = new FileStream(path + "/" + filename + "." + extension, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        file.Write(bytes, 0, bytes.Length);
        file.Dispose();

        WebClient client = new WebClient();
        Byte[] buffer = client.DownloadData(path + "/" + filename + "." + extension);
        if (buffer != null)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", buffer.Length.ToString());
            Response.BinaryWrite(buffer);
        }

    }

    public void SetSubDataSource(object sender, SubreportProcessingEventArgs e)
    {
        //Microsoft.Reporting.WebForms.LocalReport mlREPORTD = new Microsoft.Reporting.WebForms.LocalReport();
        //mlREPORTD.ReportEmbeddedResource = "~/pj_delinv/InvReceiptD.rdlc";
        e.DataSources.Add(new ReportDataSource("INV_RECEIPT_DATAD", mlDATATABLE1));
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        mlTITLE.Text = "Reporting Monitoring Invoice Distribution";
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"] + "Monitoring Invoice Distribution";
        RetrieveCompanyInfo();

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
            Response.Redirect("~/pageconfirmation.aspx?mpMESSAGE=34FC35D4");
        }

        ReportViewer1.ShowPrintButton = true;
        ReportViewer1.ReportRefresh += new System.ComponentModel.CancelEventHandler(ReportViewer1_ReportRefresh);

        if (Page.IsPostBack == false)
        {
            LoadComboData("InvBranch", "InvBranch");
            DisableCancel();
            mlTXTTANGGALFROM.Text = FormatDateddMMyyyy(DateTime.Now);
            mlTXTTANGGALTO.Text = FormatDateddMMyyyy(DateTime.Now);
            RetrieveFieldsDetailLocal();
            //RetrieveFieldsDetailPDF();
            //RetrieveFieldsDetailPDF1();

            if (mlDDLREPORTNAME.SelectedItem.Value == "InvDeliverySummary.rdlc")
            {
                //mlTABLEBRANCH.Visible = false;
                mlLBLBRANCH.Visible = false;
                mlLBLTITIK3.Visible = false;
                mlDDLBRANCH.Visible = false;
            }

            mlOBJGS.XM_UserLog(Session["mgUSERID"].ToString(), Session["mgNAME"].ToString(), "monitoring_delivery", "Monitoring Delivery", "");
        }

    }

    protected void ReportViewer1_ReportRefresh(Object sender, System.ComponentModel.CancelEventArgs e)
    {
        RetrieveFieldsDetailLocal();
    }

    protected void mlDDLREPORTNAME_SelectedIndexChanged(Object sender, EventArgs e)
    {
        DropDownList mlDDL = (DropDownList)sender;

        if (mlDDL.SelectedItem.Value == "InvDeliverySummary.rdlc")
        {
            //mlTABLEBRANCH.Visible = false;
            mlLBLBRANCH.Visible = false;
            mlLBLTITIK3.Visible = false;
            mlDDLBRANCH.Visible = false;
        }
        else
        {
            //mlTABLEBRANCH.Visible = true;
            mlLBLBRANCH.Visible = true;
            mlLBLTITIK3.Visible = true;
            mlDDLBRANCH.Visible = true;
        }
    }

    protected void btReport_Click(Object sender, System.Web.UI.ImageClickEventArgs e)
    {
        RetrieveFieldsDetailLocal();
    }

}