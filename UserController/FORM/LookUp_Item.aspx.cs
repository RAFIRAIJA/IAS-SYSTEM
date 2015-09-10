using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.OleDb;

using IAS.Core.CSCode;
using IAS.Initialize;
using IAS.APP.DataAccess.FA;


public partial class UserController_FORM_LookUp_Item : System.Web.UI.Page
{
    OleDbDataReader mlREADER;
    OleDbDataReader mlREADER2;

    VariableCore mlVarCore = new VariableCore();
    VariableFA_LookUp oEnt = new VariableFA_LookUp();
    FunctionFA_LookUp oDA = new FunctionFA_LookUp();

    //ucmGeneralSystem mlOBJGS = new ucmGeneralSystem();
    //ucmGeneralFunction mlOBJGF = new ucmGeneralFunction();
    //FunctionLocal mlOBJPJ = new FunctionLocal();
    //GeneralSetting GS = new GeneralSetting();
    //EntitiesMenu oEntMenu = new EntitiesMenu();
    //EntitiesInvoiceDelivery oEnt = new EntitiesInvoiceDelivery();
    //DataAccessInvoiceDelivery oDA = new DataAccessInvoiceDelivery();

    ModuleGeneralFunction oMGF = new ModuleGeneralFunction();
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
    protected String mlCompanyID
    {
        get { return ((String)ViewState["mlCompanyID"]); }
        set { ViewState["mlCompanyID"] = value; }
    }
    protected String mlTemplateItem
    {
        get { return ((String)ViewState["mlTemplateItem"]); }
        set { ViewState["mlTemplateItem"] = value; }
    }


    protected void Page_PreInit(object sender, EventArgs e)
    {
        CekSession();
        mlMENUSTYLE = mlOBJPJ.AD_CHECKMENUSTYLE(Session["mgMENUSTYLE"].ToString(), this.MasterPageFile);
        this.MasterPageFile = mlMENUSTYLE;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"].ToString() + " LOOKUP MAPPING ITEM V2.02";
        mlTITLE.Text = "LOOKUP MAPPING ITEM V2.02";
        Session["mgDateTime"] = System.DateTime.Now;
        
        if(Request.QueryString["templateItem"].ToString()  != "")
        {
            mlTemplateItem = Request.QueryString["templateItem"].ToString();
        }

        CekSession();
        mlCompanyID = ddlEntity.SelectedValue.ToString();
        if (!IsPostBack)
        {

            PageSize = int.Parse(pagingLookUp.PageSize.ToString());
            currentPage = 1;
            cmdWhere = "";
            sortBy = "";

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
    protected void NavigationButtonClicked(usercontroller_ucPaging.NavigationButtonEventArgs e)
    {
        retrievepaging();
    }
    protected void retrievepaging()
    {
        currentPage = pagingLookUp.currentPage;
        PageSize = int.Parse(pagingLookUp.PageSize.ToString());
        RetrieveData();
    }
    protected void RetrieveData()
    {
        oEnt.MappingType = "Item";
        oEnt.CompanyID = mlCompanyID;
        oEnt.ModuleID = "PB";
        oEnt.ErrorMesssage = "";
        oEnt.PageSize = PageSize;
        oEnt.CurrentPage = currentPage;
        oEnt.WhereCond = cmdWhere;
        oEnt.SortBy = sortBy;
        oDA.GetLookUpAssetPaging(oEnt);
        if (oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
        }

        dgListData.Visible = true;
        dgListData.DataSource = oEnt.dtListData;
        dgListData.DataBind();

        totalRecords = oEnt.TotalRecord;
        pagingLookUp.PageSize = long.Parse(PageSize.ToString());
        pagingLookUp.currentPage = currentPage;
        pagingLookUp.PagingFooter(totalRecords, PageSize);
    }
    protected void SearchRecord()
    {
        cmdWhere = "";
        if (mlTemplateItem != "" && mlTemplateItem != null)
        {
            cmdWhere = " and a.DocNo='"+ mlTemplateItem +"'";
        }

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

        RetrieveData();
    }
    protected void ResetRecord()
    {
        Response.Redirect("LookUpItem.aspx");
    }

    protected void dgListData_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

    }
    protected void imbReset_Click(object sender, ImageClickEventArgs e)
    {
        ResetRecord();
    }
    protected void imgsearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchRecord();
    }
}