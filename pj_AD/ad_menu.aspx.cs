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
public partial class pj_ad_ad_menu : System.Web.UI.Page
{

    ModuleDBFunction oMDBF = new ModuleDBFunction();
    ModuleGeneralFunction oMGF = new ModuleGeneralFunction();
    ModuleGeneralSystem oMGS = new ModuleGeneralSystem();
    ModuleInitialization oMI = new ModuleInitialization();
    ModuleFunctionLocal mlOBJPJ = new ModuleFunctionLocal();

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
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"].ToString() + " MENU V2.02";
        mlTITLE.Text = "MENU V2.02";
        Session["mgDateTime"] = System.DateTime.Now;

        CekSession();

        oEnt.mpCompanyID = "AD";
        oEnt.mpModule = "AD";

        if(!IsPostBack)
        {
            mlPAGESIZE = int.Parse(pagingMenu.PageSize.ToString());
            mlCURRENTPAGE = 1;
            mlCMDWHERE = "";
            mlSORTBY = "";
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
        Response.Redirect("ad_menu.aspx");
    }
    protected void retrievepaging()
    {
        mlCURRENTPAGE = pagingMenu.currentPage;
        mlPAGESIZE = int.Parse(pagingMenu.PageSize.ToString());
        BindGrid();
    }
    protected void SaveRecord()
    {
        if (mlFLAGACTION == "ADD")
        {
            oEnt.MenuID = "";
        }
        else if (mlFLAGACTION == "EDIT")
        {
            oEnt.MenuID = lblMenuID.Text;
        }
        oEnt.ErrorMesssage = "";
        oEnt.FlagAction = mlFLAGACTION;
        oEnt.mpEntityID = ddlEntity.SelectedValue;
       // oEnt.MenuParentID = ddlParentMenu.SelectedValue;
        oEnt.MenuParentID = ucLUMenu.MenuParentID;
        oEnt.MenuName = txtMenuName.Text;
        oEnt.MenuPath = txtMenuPath.Text;
        oEnt.MenuLevel = "0";
        oEnt.MenuTransType = ddlTransType.SelectedValue;
        oEnt.MenuSysID = txtMenuSysID.Text;
        oEnt.MenuFlag = ddlFlagMenu.SelectedValue;
        oEnt.MenuParam = txtParam.Text;
        oEnt.LoginId = Session["mgUSERID"].ToString();
        oDA.MenuSave(oEnt);

        if (oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }
        SearchRecord();
        pnlInput.Visible = false;
        pnlGrid.Visible = true;
        btSaveRecord.Visible = false;
        btNewRecord.Visible = true;
        btSearchRecord.Visible = true;
        mlMESSAGE.Text = ModuleSystemConstant.MESSAGE_INSERT_SUCCESS;

    }
    protected void NewRecord()
    {
        mlFLAGACTION = "ADD";
        pnlInput.Visible = true;
        pnlGrid.Visible = false;
        pnlSearch.Visible = false;
        btSearchRecord.Visible = false;
        btNewRecord.Visible = false;
        btSaveRecord.Visible = true;
        ClearComponent();
    }
    protected void GetDetailData()
    {
        ClearComponent();

        oEnt.ErrorMesssage = "";
        oEnt.MenuID = mlMenuID;
        oDA.GetMenuDetail(oEnt);
        if(oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }
        if(oEnt.dtListData.Rows.Count <=0)
        {
            mlMESSAGE.Text = ModuleSystemConstant.MESSAGE_DATA_NOT_FOUND;
            return;
        }

        mlDT_LISTDATA = oEnt.dtListData.Copy();
        lblMenuID.Text = mlDT_LISTDATA.Rows[0]["menu_id"].ToString();
        txtMenuName.Text = mlDT_LISTDATA.Rows[0]["menu_name"].ToString();
        txtMenuPath.Text = mlDT_LISTDATA.Rows[0]["menu_path"].ToString();
        txtMenuSysID.Text = mlDT_LISTDATA.Rows[0]["menu_sysid"].ToString();
        txtParam.Text = mlDT_LISTDATA.Rows[0]["menu_param"].ToString();
        GetDDL(ddlEntity,mlDT_LISTDATA.Rows[0]["entity_id"].ToString());
        GetDDL(ddlFlagMenu,mlDT_LISTDATA.Rows[0]["menu_flag"].ToString());
       // GetDDL(ddlParentMenu,mlDT_LISTDATA.Rows[0]["menu_parent_id"].ToString());
        ucLUMenu.MenuParentID = mlDT_LISTDATA.Rows[0]["menu_parent_id"].ToString();
        ucLUMenu.MenuParentName = mlDT_LISTDATA.Rows[0]["parentMenu"].ToString();
        GetDDL(ddlTransType,mlDT_LISTDATA.Rows[0]["menu_transtype"].ToString());

    }
    protected void GetDDL(DropDownList ddl,String ID)
    {
        for(int i=0;i<ddl.Items.Count;i++)
        {
            if(ddl.Items[i].Value == ID)
            {
                ddl.SelectedIndex = i;
                break;
            }
        }
    }
    protected void EnableComponent()
    {

    }
    protected void DisableComponent()
    {

    }
    protected void ClearComponent()
    {
        ddlEntity.SelectedIndex = 0;
        ddlFlagMenu.SelectedIndex = 0;
       // ddlParentMenu.SelectedIndex = 0;
        ddlTransType.SelectedIndex = 0;
        txtMenuName.Text = "";
        txtMenuPath.Text = "";
        txtMenuSysID.Text = "";
        txtParam.Text = "";
        
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

                if(((Label)dtg.Item.FindControl("lblMenuFlag")).Text.ToString()=="SM")
                {
                    mlMESSAGE.Text = ModuleSystemConstant.MESSAGES_NO_ACCESS;
                    return;
                }

                GetDetailData();
                pnlInput.Visible = true;
                pnlGrid.Visible = false;
                pnlSearch.Visible = false;
                btSearchRecord.Visible = false;
                btNewRecord.Visible = false;
                btSaveRecord.Visible = true; 
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
    protected void btNewRecord_Click(object sender, ImageClickEventArgs e)
    {
        NewRecord();
    }

    protected void btSearchRecord_Click(object sender, ImageClickEventArgs e)
    {
        pnlSearch.Visible = true;
    }
    protected void btCancelOperation_Click(object sender, ImageClickEventArgs e)
    {
        ResetRecord();
    }
    protected void btSaveRecord_Click(object sender, ImageClickEventArgs e)
    {
        SaveRecord();
    }
}