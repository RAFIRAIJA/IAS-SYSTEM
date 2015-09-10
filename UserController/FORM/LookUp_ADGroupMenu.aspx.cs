using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using IASClass;
using IAS.Core.CSCode;
using IAS.APP.DataAccess.AD;
public partial class UserController_FORM_LookUp_ADGroupMenu : System.Web.UI.Page
{
    OleDbDataReader mlREADER;
    OleDbDataReader mlREADER2;

    ModuleGeneralFunction oMGF = new ModuleGeneralFunction();
    ModuleDBFunction oMDBF = new ModuleDBFunction();
    ModuleGeneralSystem oMGS = new ModuleGeneralSystem();
    ModuleInitialization oMI = new ModuleInitialization();
    ModuleSystemConstant oConst;

    VariableCore oVar = new VariableCore();
    FunctionCore oFunc = new FunctionCore();

    VariableAD oEnt = new VariableAD();
    FunctionAD oDA = new FunctionAD();


    protected String mlSQL
    {
        get { return ((String)ViewState["mlSQL"]); }
        set { ViewState["mlSQL"] = value; }
    }
    protected String mlKEY
    {
        get { return ((String)ViewState["mlKEY"]); }
        set { ViewState["mlKEY"] = value; }
    }
    protected String mlRECORDSTATUS
    {
        get { return ((String)ViewState["mlRECORDSTATUS"]); }
        set { ViewState["mlRECORDSTATUS"] = value; }
    }
    protected String LoginID
    {
        get { return ((String)ViewState["LoginID"]); }
        set { ViewState["LoginID"] = value; }
    }
    protected String FlagAction
    {
        get { return ((String)ViewState["FlagAction"]); }
        set { ViewState["FlagAction"] = value; }
    }
    protected String GroupMenu
    {
        get { return ((String)ViewState["GroupMenu"]); }
        set { ViewState["GroupMenu"] = value; }
    }
    protected String GroupName
    {
        get { return ((String)ViewState["GroupName"]); }
        set { ViewState["GroupName"] = value; }
    }

    protected DataTable dtListApproval
    {
        get { return ((DataTable)ViewState["dtListApproval"]); }
        set { ViewState["dtListApproval"] = value; }
    }
    protected DataTable dtListData
    {
        get { return ((DataTable)ViewState["dtListData"]); }
        set { ViewState["dtListData"] = value; }
    }
    protected DataTable dtListDetail
    {
        get { return ((DataTable)ViewState["dtListDetail"]); }
        set { ViewState["dtListDetail"] = value; }
    }
    protected DataTable dtSelectedDetail
    {
        get { return ((DataTable)ViewState["dtSelectedDetail"]); }
        set { ViewState["dtSelectedDetail"] = value; }
    }

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

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"].ToString() + " GROUP MENU V2.02";
        Session["mgDateTime"] = System.DateTime.Now;

        if (Session["mgUSERID"] == null)
        {
            Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=34FC35D4");
            return;
        }

        oEnt.mpCompanyID = "AD";
        oEnt.mpModule = "AD";

        if (!IsPostBack)
        {
            PageSize = int.Parse(pagingGroupMenu.PageSize.ToString());
            currentPage = 1;
            totalPages = 0;
            cmdWhere = "";
            sortBy = "";
            GroupMenu = "";
           
            ClickSearch();

        }
    }    
    protected void ClickSearch()
    {
        cmdWhere = "";
        if (ddlSearchBy.SelectedValue != "")
        {
            if (txtSearchBy.Text.Contains("%"))
            {
                cmdWhere += " and " + ddlSearchBy.SelectedValue + " like '" + txtSearchBy.Text + "'";
            }
            else
            {
                cmdWhere += " and " + ddlSearchBy.SelectedValue + " = '" + txtSearchBy.Text + "'";
            }
        }

        BindGrid();
    }
    protected void BindGrid()
    {
        oEnt.ErrorMesssage = "";
        oEnt.PageSize = PageSize;
        oEnt.CurrentPage = currentPage;
        oEnt.WhereCond = cmdWhere;
        oEnt.SortBy = sortBy;
        oDA.GetDataGroupMenuPaging(oEnt);
        if (oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }
        dtListData = oEnt.dtListData;
        dgListData.DataSource = oEnt.dtListData;
        dgListData.DataBind();
        totalRecords = oEnt.TotalRecord;
        pagingGroupMenu.PageSize = long.Parse(PageSize.ToString());
        pagingGroupMenu.currentPage = currentPage;
        pagingGroupMenu.PagingFooter(totalRecords, PageSize);

    }    
    protected void dgListData_SortCommand(object source, DataGridSortCommandEventArgs e)
    {
        if (!this.sortBy.Contains("desc"))
        {
            this.sortBy = e.SortExpression + " desc";
        }
        else
        {
            this.sortBy = e.SortExpression + " asc ";
        }
        BindGrid();
    }
    protected void dgListData_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
        {

        }
    }
    protected void NavigationButtonClicked(usercontroller_ucPaging.NavigationButtonEventArgs e)
    {
        retrievepaging();
    }
    protected void retrievepaging()
    {
        currentPage = pagingGroupMenu.currentPage;
        PageSize = int.Parse(pagingGroupMenu.PageSize.ToString());
        BindGrid();
    }
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        ClickSearch();
    }
    protected void btSearchRecord_Click(object sender, ImageClickEventArgs e)
    {
        pnlSearch.Visible = true;
    }

}