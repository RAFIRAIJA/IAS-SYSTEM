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

public partial class Template_Template : System.Web.UI.Page
{

    ModuleDBFunction oMDBF = new ModuleDBFunction();
    ModuleGeneralFunction oMGF = new ModuleGeneralFunction();
    ModuleGeneralSystem oMGS = new ModuleGeneralSystem();
    ModuleInitialization oMI = new ModuleInitialization();

    FunctionCore oFunc = new FunctionCore();
    VariableCore oVar = new VariableCore();

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


    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {

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
        /*
        oEnt.ErrorMesssage = "";
        oEnt.PageSize = PageSize;
        oEnt.CurrentPage = currentPage;
        oEnt.WhereCond = cmdWhere;
        oEnt.SortBy = sortBy;
        oEnt.CompanyID = "ISSP3";
        oEnt.ModuleID = "PB";
        oDA.xxxxxxxxxxx(oEnt);
        
        if (oEnt.ErrorMesssage != "")
        {
            return;
        }

        dgListData.DataSource = "";
        dgListData.DataBind();
        totalRecords = oEnt.TotalRecord;
        */
        pagingAllow.PageSize = long.Parse(mlPAGESIZE.ToString());
        pagingAllow.currentPage = mlCURRENTPAGE;
        pagingAllow.PagingFooter(mlTOTALRECORDS, mlPAGESIZE);
    }
    protected void ResetRecord()
    {
        Response.Redirect("");
    }
    protected void retrievepaging()
    {
        mlCURRENTPAGE = pagingAllow.currentPage; 
        
        BindGrid();
    }
    protected void SaveRecord()
    {
        if(mlFLAGACTION == "ADD")
        {

        }
        else if(mlFLAGACTION == "EDIT")
        {

        }

    }
    protected void NewRecord()
    {
        mlFLAGACTION = "ADD";
    }
    protected void GetDetailData()
    {

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
            ImageButton imbEdit = ((ImageButton)e.Item.FindControl("imbEdit"));
            ImageButton imbDelete = ((ImageButton)e.Item.FindControl("imbDelete"));
            ImageButton imbReview = ((ImageButton)e.Item.FindControl("imbReview"));
            ImageButton imbAuth = ((ImageButton)e.Item.FindControl("imbAuth"));
            String Status = ((Label)e.Item.FindControl("lblStatus")).Text.ToString();

            imbDelete.Attributes.Add("OnClick", "javascript:return confirm('" + ModuleSystemConstant.MESSAGE_CONFIRM_DELETE + "')");
            imbReview.Attributes.Add("OnClick", "javascript:return confirm('" + ModuleSystemConstant.MESSAGE_CONFIRM_REVIEW + "')");
            imbAuth.Attributes.Add("OnClick", "javascript:return confirm('" + ModuleSystemConstant.MESSAGE_CONFIRM_AUTHORIZE + "')");

            imbEdit.Visible = false;
            imbDelete.Visible = false;
            imbReview.Visible = false;
            imbAuth.Visible = false;

            if (oVar.imbApprove)
            {
                if (oVar.ApprovalTypeID == "APR0001")   //// Status Review
                {
                    if (Status == "REQUEST")
                    {
                        imbReview.Visible = oVar.imbApprove;
                        imbEdit.Visible = oVar.imbWrite;
                        imbDelete.Visible = oVar.imbDelete;
                    }

                }
                if (oVar.ApprovalTypeID == "APR0002")   //// Status Authorize
                {
                    if (Status == "REVIEW")
                    {
                        imbAuth.Visible = oVar.imbApprove;
                        imbEdit.Visible = oVar.imbWrite;
                        imbDelete.Visible = oVar.imbDelete;
                    }
                }
            }


        }
    }
    protected void DataGridItemCommand(DataGridCommandEventArgs dtg)
    {
        switch (dtg.CommandName)
        {
            case "Edit":
                mlFLAGACTION = "EDIT";
                break;
            case "Delete":
                mlFLAGACTION = "DEL";
                break;
            case "Review":
                mlFLAGACTION = "RVW";
                break;
            case "Authorize":
                mlFLAGACTION = "AUTH";
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

    protected void imbSave_Click(object sender, ImageClickEventArgs e)
    {
        SaveRecord();
    }
    protected void imbBack_Click(object sender, ImageClickEventArgs e)
    {
        ResetRecord();
    }
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchRecord();
    }
    protected void btnReset_Click(object sender, ImageClickEventArgs e)
    {
        ResetRecord();
    }
    protected void btNewRecord_Click(object sender, ImageClickEventArgs e)
    {
        NewRecord();
    }

}