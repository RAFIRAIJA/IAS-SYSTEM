using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

using IAS.Core.CSCode;
using IAS.Initialize;
using IAS.APP.DataAccess.AR;

//using IASClass;
//using ISS.App.Entities.pj_ad.administration;
//using ISS.App.Entities.pj_ar;
//using ISS.App.DataAccess.pj_ar;
using Microsoft.Reporting.WebForms;

public partial class pj_ar_ar_uploadworksheet : System.Web.UI.Page
{
    OleDbDataReader mlREADER;
    OleDbDataReader mlREADER2;

    VariableCore mlVarCore = new VariableCore();
    VariableAR_InvoiceDelivery oEnt = new VariableAR_InvoiceDelivery();
    FunctionAR_InvoiceDelivery oDA = new FunctionAR_InvoiceDelivery();

    //ucmGeneralSystem mlOBJGS = new ucmGeneralSystem();
    //ucmGeneralFunction mlOBJGF = new ucmGeneralFunction();
    //FunctionLocal mlOBJPJ = new FunctionLocal();
    //GeneralSetting GS = new GeneralSetting();
    //EntitiesMenu oEntMenu = new EntitiesMenu();
    //EntitiesInvoiceDelivery oEnt = new EntitiesInvoiceDelivery();
    //DataAccessInvoiceDelivery oDA = new DataAccessInvoiceDelivery();

    ModuleGeneralFunction oMGF = new ModuleGeneralFunction();
    ReportDataSource Rds = new ReportDataSource();
    ModuleFunctionLocal mlOBJPJ = new ModuleFunctionLocal();

    #region !--PagingVariable--!
    protected int PageSize
    {
        get { return ((int)ViewState["PageSize"]); }
        set { ViewState["PageSize"] = value; }
    }
    protected int currentPage
    {
        get { return ((int)ViewState["currentPage"]); }
        set { ViewState["currentPage"] = value; }
    }
    protected double totalPages
    {
        get { return ((double)ViewState["totalPages"]); }
        set { ViewState["totalPages"] = value; }
    }
    protected string cmdWhere
    {
        get { return ((string)ViewState["cmdWhere"]); }
        set { ViewState["cmdWhere"] = value; }
    }
    protected string sortBy
    {
        get { return ((string)ViewState["sortBy"]); }
        set { ViewState["sortBy"] = value; }
    }
    protected int totalRecords
    {
        get { return ((int)ViewState["totalRecords"]); }
        set { ViewState["totalRecords"] = value; }
    }
    #endregion

    protected String mlMENUSTYLE
    {
        get { return ((String)ViewState["mlMENUSTYLE"]); }
        set { ViewState["mlMENUSTYLE"] = value; }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        CekSession();
        mlMENUSTYLE = mlOBJPJ.AD_CHECKMENUSTYLE(Session["mgMENUSTYLE"].ToString(), this.MasterPageFile);
        this.MasterPageFile = mlMENUSTYLE;
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"].ToString() + " UPLOAD WORKSHEET INVOICE V2.02";
        mlTITLE.Text = "UPLOAD WORKSHEET INVOICE V2.02";
        Session["mgDateTime"] = System.DateTime.Now;

        //if (Session["mgUSERID"] == null)
        //{
        //    Response.Redirect("~/pageconfirmation.aspx?mpMESSAGE=34FC35D4");
        //    return;
        //}
        CekSession();

        if(!IsPostBack)
        {
            PageSize = int.Parse(pagingWorksheet.PageSize.ToString());
            currentPage = 1;
            cmdWhere = "";
            sortBy = "";

            ddlEntity.SelectedIndex = 1;

            ucStartDate.dateValue = DateTime.Now.ToString("dd/MM/yyyy");
            ucStartDate.isEnabled = false;

            ucEndDate.dateValue = DateTime.Now.ToString("dd/MM/yyyy");
            ucEndDate.isEnabled = false;

            SearchRecord();
        }
    }
    protected void CekSession()
    {
        if (Session["mgUSERID"] == null)
        {
            Response.Redirect("~/pageconfirmation.aspx?mpMESSAGE=34FC35D4");
            return;
        }
    }
    protected void dgListData_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

    }
    protected void dgListData_ItemCommand(object source, DataGridCommandEventArgs e)
    {

    }
    protected void NavigationButtonClicked(usercontroller_ucPaging.NavigationButtonEventArgs e)
    {
        retrievepaging();
    }
    protected void retrievepaging()
    {
        currentPage = pagingWorksheet.currentPage;
        PageSize = int.Parse(pagingWorksheet.PageSize.ToString());
        RetrieveData();
    }
    protected void RetrieveData()
    {
        oEnt.CompanyID = "ISSP3";
        oEnt.ModuleID = "PB";
        oEnt.ErrorMesssage = "";
        oEnt.PageSize = PageSize;
        oEnt.CurrentPage = currentPage;
        oEnt.WhereCond = cmdWhere;
        oEnt.SortBy = sortBy;
        oDA.GetWorksheetPaging(oEnt);
        if(oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
        }

        dgListData.DataSource = oEnt.dtListData;
        dgListData.DataBind();
        totalRecords = oEnt.TotalRecord;
        pagingWorksheet.PageSize = long.Parse(PageSize.ToString());
        pagingWorksheet.currentPage = currentPage;
        pagingWorksheet.PagingFooter(totalRecords, PageSize); 
    }
    protected void SearchRecord()
    {
        cmdWhere = "";
        if (ddlSearchBy.SelectedValue != "")
        {
            if (txtSearchBy.Text.Contains("%"))
            {
                cmdWhere += " and " + ddlSearchBy.SelectedValue + " like '%" + txtSearchBy.Text + "%'";
            }
            else
            {
                cmdWhere += " and " + ddlSearchBy.SelectedValue + " = '" + txtSearchBy.Text + "'";
            }
        }

        if(chkPeriode.Checked)
        {
            cmdWhere += " and a.upload_date between '" + oMGF.ConvertDate(ucStartDate.dateValue) + "' and '" + oMGF.ConvertDate(ucEndDate.dateValue) + "' ";
        }

        RetrieveData();
    }
    protected void NewRecord()
    {
        pnlUpload.Visible = true;
        pnlSearch.Visible = false;
        pnlListInvoice.Visible = false;
        btNewRecord.Visible = false;
        btSearchRecord.Visible = false;
    }
    protected void ResetRecord()
    {
        Response.Redirect("ar_uploadworksheet.aspx");
    }
    protected void RetrievePrintRecord()
    {
        oEnt.CompanyID = "ISSP3";
        oEnt.ModuleID = "PB";

        oEnt.ErrorMesssage = "";
        oEnt.Entity_id = ddlEntity.SelectedValue.ToString();
        oEnt.StartDate = oMGF.ConvertDate(ucStartDate.dateValue);
        oEnt.EndDate = oMGF.ConvertDate(ucEndDate.dateValue);
        oDA.PrintListInvoiceUpload(oEnt);


        String FileName = "rptINV_UploadWorksheet.rdlc";
        rptViewer.LocalReport.ReportPath = ConfigurationManager.AppSettings["ReportPath"] + FileName;

        Rds.Name = "dsUpload";
        Rds.Value = oEnt.dtListData;
        rptViewer.LocalReport.DataSources.Add(Rds);

        //Untuk Passing Parameter
        List<ReportParameter> parameters = new List<ReportParameter>();
        parameters.Add(new ReportParameter("Entity", this.ddlEntity.SelectedValue.ToString()));
        parameters.Add(new ReportParameter("StartDate", oMGF.ConvertDate2(ucStartDate.dateValue).ToString("dd/MM/yyyy")));
        parameters.Add(new ReportParameter("EndDate", oMGF.ConvertDate2(ucEndDate.dateValue).ToString("dd/MM/yyyy")));

        rptViewer.LocalReport.SetParameters(parameters);
        rptViewer.LocalReport.Refresh();
    }
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchRecord();
    }    
    protected void btNewRecord_Click(object sender, ImageClickEventArgs e)
    {
        NewRecord();
    }
    protected void btSearchRecord_Click(object sender, ImageClickEventArgs e)
    {
        pnlSearch.Visible = true;
        btPrintRecord.Visible = true;
    }
    protected void btCancelOperation_Click(object sender, ImageClickEventArgs e)
    {
        ResetRecord();
    }
    protected void btPrintRecord_Click(object sender, ImageClickEventArgs e)
    {
        if (chkPeriode.Checked == false)
        {
            mlMESSAGE.Text = "Please...Select Periode Upload...";
            return;
        }
        pnlListInvoice.Visible = false;
        pnlSearch.Visible = false;
        pnlTOOLBAR.Visible = false;
        pnlPrint.Visible = true;
        RetrievePrintRecord();
    }
    protected void btnUpload_Click(object sender, ImageClickEventArgs e)
    {
        if (fuWorksheet.HasFile)
        {

            string FileName = Path.GetFileName(fuWorksheet.PostedFile.FileName);
            string Extension = Path.GetExtension(fuWorksheet.PostedFile.FileName);
            string FolderPath = ConfigurationManager.AppSettings["OutputPath"];

            string FilePath = Server.MapPath(FolderPath + FileName);
            fuWorksheet.SaveAs(FilePath);
            Import_To_Grid(FilePath, Extension);

            pnlSearch.Visible = true;
            pnlListInvoice.Visible = true;
            pnlUpload.Visible = false;
            btNewRecord.Visible = true;
            btSearchRecord.Visible = true;
        }
    }
    private void Import_To_Grid(string FilePath, string Extension)
    {

        string conStr = "";

        switch (Extension)
        {
            case ".xls": //Excel 97-03
                //conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
                conStr = ModuleBaseSetting.STRINGCONNEXCELL_XLS;
                break;
            case ".xlsx": //Excel 07
                //conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
                conStr = ModuleBaseSetting.STRINGCONNEXCELL_XLSX;
                break;
        }

        conStr = String.Format(conStr, FilePath, 1);

        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();

        cmdExcel.Connection = connExcel;

        //Get the name of First Sheet
        connExcel.Open();

        DataTable dtExcelSchema;
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

        string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        connExcel.Close();

        //Read Data from First Sheet
        connExcel.Open();

        cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);

        oEnt.ErrorMesssage = "";
        oEnt.CompanyID = "ISSP3";
        oEnt.ModuleID = "PB";
        oEnt.dtListData = dt;
        oEnt.LoginID = Session["mgUSERID"].ToString();
        oDA.SaveUploadWorksheet(oEnt);
        connExcel.Close();

        if (oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }
        mlMESSAGE.Text = ModuleSystemConstant.MESSAGE_INSERT_SUCCESS;

        //cmdWhere = " a.Upload_Date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
        RetrieveData();

    }

    protected void ChkPeriodeCheckedChanged(object sender, EventArgs e)
    {
        if(chkPeriode.Checked)
        {
            ucStartDate.isEnabled = true;
            ucEndDate.isEnabled = true;
        }
        else
        {
            ucStartDate.isEnabled = false;
            ucEndDate.isEnabled = false;
        }
    }
    protected void imbBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ar_uploadworksheet.aspx");
    }
    
    
}