using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using IAS.Core.CSCode;
using IAS.Initialize;
using IAS.APP.DataAccess.AD;
public partial class UserController_FORM_LookUp_ADMenu : System.Web.UI.Page
{
    ModuleDBFunction oMDBF = new ModuleDBFunction();
    ModuleGeneralFunction oMGF = new ModuleGeneralFunction();
    ModuleGeneralSystem oMGS = new ModuleGeneralSystem();
    ModuleInitialization oMI = new ModuleInitialization();

    FunctionCore oFunc = new FunctionCore();
    VariableCore oVar = new VariableCore();

    VariableAD_Menu oEnt = new VariableAD_Menu();
    FunctionAD_Menu oDA = new FunctionAD_Menu();

    #region !--PagingVariable--!
    protected int mlPAGESIZE
    {
        get { return ((int)ViewState["mlPAGESIZE"]); }
        set { ViewState["mlPAGESIZE"] = value; }
    }
    protected int mlCURRENTPAGE
    {
        get { return ((int)ViewState["mlCURRENTPAGE"]); }
        set { ViewState["mlCURRENTPAGE"] = value; }
    }
    protected double mlTOTALPAGES
    {
        get { return ((double)ViewState["mlTOTALPAGES"]); }
        set { ViewState["mlTOTALPAGES"] = value; }
    }
    protected string mlCMDWHERE
    {
        get { return ((string)ViewState["mlCMDWHERE"]); }
        set { ViewState["mlCMDWHERE"] = value; }
    }
    protected string mlSORTBY
    {
        get { return ((string)ViewState["mlSORTBY"]); }
        set { ViewState["mlSORTBY"] = value; }
    }
    protected int mlTOTALRECORDS
    {
        get { return ((int)ViewState["mlTOTALRECORDS"]); }
        set { ViewState["mlTOTALRECORDS"] = value; }
    }
    #endregion

    protected String mlFLAGACTION
    {
        get { return ((String)ViewState["mlFLAGACTION"]); }
        set { ViewState["mlFLAGACTION"] = value; }
    }
    protected DataTable mlDT_LISTDATA
    {
        get { return ((DataTable)ViewState["mlDT_LISTDATA"]); }
        set { ViewState["mlDT_LISTDATA"] = value; }
    }
    protected DataTable mlDT_LISTDETAIL
    {
        get { return ((DataTable)ViewState["mlDT_LISTDETAIL"]); }
        set { ViewState["mlDT_LISTDETAIL"] = value; }
    }
    protected String mlMenuID
    {
        get { return ((String)ViewState["mlMenuID"]); }
        set { ViewState["mlMenuID"] = value; }
    }    

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"].ToString() + " MENU V2.02";
        Session["mgDateTime"] = System.DateTime.Now;

        oEnt.mpCompanyID = "AD";
        oEnt.mpModule = "AD";

        if (!IsPostBack)
        {
            mlPAGESIZE = int.Parse(pagingMenu.PageSize.ToString());
            mlCURRENTPAGE = 1;
            mlCMDWHERE = "";
            mlSORTBY = "";
            SearchRecord();
        }
    }
    protected void dgListData_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        DataGridItemCommand(e);
    }
    protected void dgListData_SortCommand(object source, DataGridSortCommandEventArgs e)
    {
        DataGridSortCommand(e);
    }
    protected void dgListData_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        DataGridItemDataBind(e);
    }
    protected void NavigationButtonClicked(usercontroller_ucPaging.NavigationButtonEventArgs e)
    {
        retrievepaging();
    }

    protected void SearchRecord()
    {
        GetWhereCond();
        BindGrid();
    }
    protected void GetWhereCond()
    {
        mlCMDWHERE = "";
        if (ddlSearchBy.SelectedValue != "")
        {
            if (txtSearchBy.Text.Contains("%"))
            {
                mlCMDWHERE += " " + ddlSearchBy.SelectedValue + " like '%" + txtSearchBy.Text + "%' ";
            }
            else
            {
                mlCMDWHERE += " " + ddlSearchBy.SelectedValue + " = '" + txtSearchBy.Text + "' ";
            }
        }
    }
    protected void BindGrid()
    {

        oEnt.ErrorMesssage = "";
        oEnt.PageSize = mlPAGESIZE;
        oEnt.CurrentPage = mlCURRENTPAGE;
        oEnt.WhereCond = mlCMDWHERE;
        oEnt.SortBy = mlSORTBY;
        oDA.GetMenuPaging(oEnt);

        if (oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }
        mlDT_LISTDATA = oEnt.dtListData.Copy();
        dgListData.DataSource = mlDT_LISTDATA;
        dgListData.DataBind();
        mlTOTALRECORDS = oEnt.TotalRecord;

        pagingMenu.PageSize = long.Parse(mlPAGESIZE.ToString());
        pagingMenu.currentPage = mlCURRENTPAGE;
        pagingMenu.PagingFooter(mlTOTALRECORDS, mlPAGESIZE);
    }
    protected void ResetRecord()
    {
        Response.Redirect("LookUp_ADMenu.aspx");
    }
    protected void retrievepaging()
    {
        mlCURRENTPAGE = pagingMenu.currentPage;
        mlPAGESIZE = int.Parse(pagingMenu.PageSize.ToString());
        BindGrid();
    }
    
    protected void EnableComponent()
    {

    }
    protected void DisableComponent()
    {

    }
    

    protected void DataGridItemDataBind(DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
        {

        }
    }
    protected void DataGridItemCommand(DataGridCommandEventArgs dtg)
    {
        mlMESSAGE.Text = "";
        switch (dtg.CommandName)
        {
            case "Edit":
                mlFLAGACTION = "EDIT";
                mlMenuID = ((Label)dtg.Item.FindControl("lblMenuID")).Text.ToString();

                if (((Label)dtg.Item.FindControl("lblMenuFlag")).Text.ToString() == "SM")
                {
                    mlMESSAGE.Text = ModuleSystemConstant.MESSAGES_NO_ACCESS;
                    return;
                }
                break;
            case "Delete":
                mlFLAGACTION = "DEL";
                break;

        }
    }
    protected void DataGridSortCommand(DataGridSortCommandEventArgs dtg)
    {
        if (!this.mlSORTBY.Contains("desc"))
        {
            this.mlSORTBY = dtg.SortExpression + " desc";
        }
        else
        {
            this.mlSORTBY = dtg.SortExpression + " asc ";
        }
        BindGrid();
    }
    
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchRecord();
    }
    protected void btnReset_Click(object sender, ImageClickEventArgs e)
    {
        ResetRecord();
    }
}