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
using Microsoft.Reporting.WebForms;

using IASClass;
using IAS.Core.CSCode;
using IAS.Initialize;

  
public partial class pj_GL_gl_rpt_profitandloss_cb1 : System.Web.UI.Page
{
    OleDbDataReader mlREADER;
    OleDbDataReader mlREADER2;

    VariableCore mlVarCore = new VariableCore();
        

    ucmGeneralSystem mlOBJGS = new ucmGeneralSystem();
    //ucmGeneralFunction mlOBJGF = new ucmGeneralFunction();
    //FunctionLocal mlOBJPJ = new FunctionLocal();
    //GeneralSetting GS = new GeneralSetting();
    //EntitiesMenu oEntMenu = new EntitiesMenu();
    //EntitiesInvoiceDelivery oEnt = new EntitiesInvoiceDelivery();
    //DataAccessInvoiceDelivery oDA = new DataAccessInvoiceDelivery();

    ModuleGeneralFunction oMGF = new ModuleGeneralFunction();
    ModuleFunctionLocal mlOBJPJ = new ModuleFunctionLocal();
    ReportDataSource ds = new ReportDataSource();
    PivotTableCode pivot = new PivotTableCode();

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

    protected string mlSQL
    {
        get { return ((string)ViewState["mlSQL"]); }
        set { ViewState["mlSQL"] = value; }
    }    
    protected string UserID
    {
        get { return ((string)ViewState["UserID"]); }
        set { ViewState["UserID"] = value; }
    }
    protected String mlMENUSTYLE
    {
        get { return ((String)ViewState["mlMENUSTYLE"]); }
        set { ViewState["mlMENUSTYLE"] = value; }
    }
    protected DataTable mlDTCombo
    {
        get { return ((DataTable)ViewState["mlDTCombo"]); }
        set { ViewState["mlDTCombo"] = value; }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        CekSession();
        mlMENUSTYLE = mlOBJPJ.AD_CHECKMENUSTYLE(Session["mgMENUSTYLE"].ToString(), this.MasterPageFile);
        this.MasterPageFile = mlMENUSTYLE;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"].ToString() + " GL Profit and Loss Report T1 V2.02";
        mlTITLE.Text = "GL Profit and Loss Report T1 V2.02";
        Session["mgDateTime"] = System.DateTime.Now;

        CekSession();

        //oEnt.CompanyID = "ISSP3";
        //oEnt.ModuleID = "PB";


        if (!IsPostBack)
        {
            currentPage = 1;
            cmdWhere = "";
            sortBy = "";
            UcStartDate.dateValue = DateTime.Now.ToString("01/MM/yyyy");
            UcEndDate.dateValue = DateTime.Now.ToString("dd/MM/yyyy");

            LoadCombo();            

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
    protected void LoadCombo()
    {
        mlDTCombo = new DataTable();

        ddlBranch.Items.Clear();
        ddlBranch.Items.Add("10PB # Ware house - Pekan Baru");
        mlSQL = "SELECT [Branch Location], Name FROM [ISS Servisystem, PT$Location] ORDER BY Name";
        mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSN3");
        mlDTCombo.Load(mlREADER);
        for(int i = 0;i<mlDTCombo.Rows.Count;i++ )
        {
            ddlBranch.Items.Add(new ListItem(mlDTCombo.Rows[i]["Branch Location"].ToString()+" # "+ mlDTCombo.Rows[i]["Name"].ToString() ,mlDTCombo.Rows[i]["Branch Location"].ToString());
        }
        
        mlDTCombo = new DataTable();

        ddlSitecard.Items.Clear();
        ddlSitecard.Items.Add("All");
        mlSQL = "SELECT [Lineno_], SearchName FROM [ISS Servisystem, PT$CustServiceSite] ORDER BY SearchName";
        mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSN3");
        mlDTCombo.Load(mlREADER);
        for(int i = 0;i<mlDTCombo.Rows.Count;i++ )
        {
            ddlBranch.Items.Add(new ListItem(mlDTCombo.Rows[i]["Lineno_"].ToString()+" # "+ mlDTCombo.Rows[i]["SearchName"].ToString() ,mlDTCombo.Rows[i]["Lineno_"].ToString() );
        }    

    }
    protected void SearchRecord()
    {
        mlDTCombo = new DataTable();

        mlSQL = "Exec spGL_CB1 '" + txtPIC.Text + "' ";
        mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSN3");
        mlDTCombo.Load(mlREADER);

    }
    protected void dgListData_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
        {
            e.Item.Cells[4].Visible = false;
            e.Item.Cells[5].Visible = false;
            e.Item.Cells[6].Visible = false;
            e.Item.Cells[7].Visible = false;
            e.Item.Cells[8].Visible = false;

        }
    }
}