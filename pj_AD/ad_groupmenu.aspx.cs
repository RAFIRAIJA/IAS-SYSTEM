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


public partial class pj_ad_ad_groupmenu : System.Web.UI.Page
{
    //ModuleGeneralSystem mlOBJGS = new ModuleGeneralSystem();
    //ModuleGeneralFunction mlOBJGF = new ModuleGeneralFunction();
    //ModuleDBFunction mlOBJDBF = new ModuleDBFunction();
    //ModuleInitialization mlOBJI = new ModuleInitialization();
    //ucmGeneralSystem mlOBJGS = new ucmGeneralSystem();
    //ucmGeneralFunction mlOBJGF = new ucmGeneralFunction();
    //FunctionLocal mlOBJPJ = new FunctionLocal();

    OleDbDataReader mlREADER;
    OleDbDataReader mlREADER2;

    ModuleGeneralFunction oMGF = new ModuleGeneralFunction();
    ModuleDBFunction oMDBF = new ModuleDBFunction();
    ModuleGeneralSystem oMGS = new ModuleGeneralSystem();
    ModuleInitialization oMI = new ModuleInitialization();
    ModuleFunctionLocal mlOBJPJ = new ModuleFunctionLocal();

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
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"].ToString() + " GROUP MENU V2.02";
        mlTITLE.Text = "GROUP MENU V2.02";
        Session["mgDateTime"] = System.DateTime.Now;

        CekSession();

        oEnt.mpCompanyID = "AD";
        oEnt.mpModule = "AD";
        oVar.mpFormID = "AD004";

        if (!IsPostBack)
        {
            PageSize = int.Parse(pagingGroupMenu.PageSize.ToString());
            currentPage = 1;
            totalPages = 0;
            cmdWhere = "";
            sortBy = "";
            GroupMenu = "";

            dtSelectedDetail = new DataTable();
            dtSelectedDetail.Columns.Add("MenuID");
            dtSelectedDetail.Columns.Add("MenuName");
            dtSelectedDetail.Columns.Add("FlagCreate");
            dtSelectedDetail.Columns.Add("FlagRead");
            dtSelectedDetail.Columns.Add("FlagWrite");
            dtSelectedDetail.Columns.Add("FlagDelete");
            dtSelectedDetail.Columns.Add("FlagApproval");
            dtSelectedDetail.Columns.Add("ApprovalTypeID");


            if (btSearchRecord.Visible)
            {
                ClickSearch();
            }

        }
    }
    protected void CekSession()
    {
        if (Session["mgUSERID"] == null)
        {
            Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=34FC35D4");
            return;
        }
    }
    protected void AksesButton()
    {
        oVar.strConnection = oMDBF.GetConnectionString();
        oVar.MenuID = "AD004";
        oVar.LoginId = LoginID;
        oFunc.CekAksesMenu(oVar);
        btNewRecord.Visible = oVar.imbCreate;
        btSearchRecord.Visible = oVar.imbRead;
    }
    protected void ResetRecord()
    {
        Response.Redirect("ad_groupmenu.aspx");
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
    protected void NewRecord()
    {
        FlagAction = "ADD";
        txtGroupID.ReadOnly = false;
        txtGroupName.ReadOnly = false;
        BindGridDetail();
        btNewRecord.Visible = false;
        btSaveRecord.Visible = true;
        btSearchRecord.Visible = false;
    }
    protected void SaveRecord()
    {
        oEnt.FlagAction = FlagAction;
        oEnt.ErrorMesssage = "";
        oEnt.GroupMenuID = txtGroupID.Text;
        oEnt.Description = txtGroupName.Text;
        oEnt.dtListData = dtSelectedDetail.Copy();
        oEnt.LoginId = Session["mgUSERID"].ToString();
        oDA.SaveGroupMenu(oEnt);
        if (oEnt.ErrorMesssage != "")
        {
            mlMessage.Text = oEnt.ErrorMesssage;
            return;
        }
        mlMessage.Text = ModuleSystemConstant.MESSAGE_INSERT_SUCCESS;
        btSaveRecord.Visible = false;
        btNewRecord.Visible = true;
        btSearchRecord.Visible = true;
        pnlGrid.Visible = true;
        pnlDetail.Visible = false; 
    }
    protected void BindGrid()
    {
        oEnt.ErrorMesssage = "";
        oEnt.PageSize = PageSize;
        oEnt.CurrentPage = currentPage;
        oEnt.WhereCond = cmdWhere;
        oEnt.SortBy = sortBy;
        oDA.GetDataGroupMenuPaging (oEnt);
        if (oEnt.ErrorMesssage != "")
        {
            mlMessage.Text = oEnt.ErrorMesssage;
            return;
        }
        dtListData= oEnt.dtListData;
        dgListData.DataSource = oEnt.dtListData;
        dgListData.DataBind();
        totalRecords = oEnt.TotalRecord;
        pagingGroupMenu.PageSize = long.Parse(PageSize.ToString());
        pagingGroupMenu.currentPage = currentPage;
        pagingGroupMenu.PagingFooter(totalRecords, PageSize);

    }
    protected void GetDetailGroup()
    {
        BindGridDetail();

        oEnt.ErrorMesssage = "";
        oEnt.GroupMenuID = GroupMenu;
        oEnt.Description = GroupName;
        oDA.GetDetailGroupMenu(oEnt);
        if(oEnt.ErrorMesssage != "")
        {
            mlMessage.Text = oEnt.ErrorMesssage;
            return;
        }
        if (oEnt.dtListData.Rows.Count == 0)
        {
            mlMessage.Text = ModuleSystemConstant.MESSAGE_DATA_DETAIL_NOT_FOUND;
            return;
        }
        dtListDetail = oEnt.dtListData.Copy();

        for(int i=0;i<dtgDetailGroup.Items.Count;i++)
        {
            for(int n=0;n<dtListDetail.Rows.Count;n++)
            {
                if (((HyperLink)dtgDetailGroup.Items[i].FindControl("hlmenuid")).Text.ToString() == dtListDetail.Rows[n]["menu_id"].ToString())
                {
                    ((CheckBox)dtgDetailGroup.Items[i].FindControl("chkSelect")).Checked = true;
                    ((Label)dtgDetailGroup.Items[i].FindControl("lblSelect")).Text= "0";
                    break;
                }
            }

        }

        PopulateSelected();
    }
    protected void dgListData_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
        {
            switch (e.CommandName)
            {
                case "Edit":
                    FlagAction = "EDIT";
                    GroupMenu = ((HyperLink)e.Item.FindControl("hlgroupid")).Text.ToString();
                    GroupName = ((Label)e.Item.FindControl("lblgroupname")).Text.ToString();
                    txtGroupID.Text = GroupMenu;
                    txtGroupName.Text = GroupName;
                    
                    PageSize = int.Parse(pagingDtGroup.PageSize.ToString());
                    currentPage = 1;
                    totalPages = 1;
                    totalRecords = 0;
                    cmdWhere = "";
                    GetDetailGroup();
                    
                    btNewRecord.Visible = false;
                    btSaveRecord.Visible = false;
                    btSearchRecord.Visible = false;
                    pnlGrid.Visible = false;
                    pnlDetail.Visible = true; 
                    break;
                case "Replicate":
                    FlagAction = "REPLICATE";
                    GroupMenu = ((HyperLink)e.Item.FindControl("hlgroupid")).Text.ToString();
                    GroupName = ((Label)e.Item.FindControl("lblgroupname")).Text.ToString();
                    txtGroupID.ReadOnly = false;
                    txtGroupName.ReadOnly = false;
                    GetDetailGroup();
                    break;
            }
        }
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

    protected void BindGridDetail()
    {
        oEnt.ErrorMesssage = "";
        oEnt.WhereCond = cmdWhere;
        oEnt.GroupMenuID = GroupMenu;
        oEnt.SortBy = sortBy;
        oEnt.CurrentPage = currentPage;
        oEnt.PageSize = PageSize;
        oDA.GetDetailGroupMenuPaging(oEnt);
        if (oEnt.ErrorMesssage != "")
        {
            mlMessage.Text = oEnt.ErrorMesssage;
            return;
        }
        if (oEnt.dtListData.Rows.Count == 0)
        {
            mlMessage.Text = ModuleSystemConstant.MESSAGE_DATA_DETAIL_NOT_FOUND;
            return;
        }
        dtListData = new DataTable();
        dtListData = oEnt.dtListData.Copy();
        dtgDetailGroup.DataSource = dtListData;
        dtgDetailGroup.DataBind();

        totalRecords = oEnt.TotalRecord;
        pagingDtGroup.PageSize = long.Parse(PageSize.ToString());
        pagingDtGroup.currentPage = currentPage;
        pagingDtGroup.PagingFooter(totalRecords, PageSize);

    }
    protected void dtgDetailGroup_ItemCommand(object source, DataGridCommandEventArgs e)
    {

    }
    protected void dtgDetailGroup_SortCommand(object source, DataGridSortCommandEventArgs e)
    {
        if (!this.sortBy.Contains("desc"))
        {
            this.sortBy = e.SortExpression + " desc";
        }
        else
        {
            this.sortBy = e.SortExpression + " asc ";
        }
        BindGridDetail();
    }
    protected void dtgDetailGroup_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemIndex >=0)
        {
            GetApprovalType();
            DropDownList ddlApprovalType = ((DropDownList)e.Item.FindControl("ddlApprovalType"));
            String MenuID = ((HyperLink)e.Item.FindControl("hlmenuid")).Text.ToString();
            String flagCreate = ((Label)e.Item.FindControl("lblAccessCreate")).Text.ToString();
            String flagRead = ((Label)e.Item.FindControl("lblAccessRead")).Text.ToString();
            String flagWrite = ((Label)e.Item.FindControl("lblAccessWrite")).Text.ToString();
            String flagDelete = ((Label)e.Item.FindControl("lblAccessDelete")).Text.ToString();
            String flagApproval = ((Label)e.Item.FindControl("lblAccessApproval")).Text.ToString();
            String flagApprovalType = ((Label)e.Item.FindControl("lblApprovalTypeID")).Text.ToString();
            String flagMenu = ((Label)e.Item.FindControl("lblflagMenu")).Text.ToString();

            CheckBox chkSelect = ((CheckBox)e.Item.FindControl("chkSelect"));
            CheckBox chkCreate = ((CheckBox)e.Item.FindControl("chkAccessCreate"));
            CheckBox chkRead = ((CheckBox)e.Item.FindControl("chkAccessRead"));     
            CheckBox chkWrite = ((CheckBox)e.Item.FindControl("chkAccessWrite"));
            CheckBox chkDelete = ((CheckBox)e.Item.FindControl("chkAccessDelete"));
            CheckBox chkApproval = ((CheckBox)e.Item.FindControl("chkAccessApproval"));
            
            ddlApprovalType.Items.Add(new ListItem("Select One", ""));
            for (int i = 0; i < dtListApproval.Rows.Count; i++)
            {
                ddlApprovalType.Items.Add(new ListItem(dtListApproval.Rows[i]["Approval_TypeName"].ToString(), dtListApproval.Rows[i]["ApprovalTypeID"].ToString()));

            }

            if(flagMenu == "SM")
            {
                flagCreate = "1";
                flagRead = "1";
                flagWrite = "1";
                flagDelete = "1";
                flagApproval = "0";

            }
            #region //// Remark coding Cek Access
            /*
            if (flagCreate == "1")
            {
                chkCreate.Checked = true;
            }
            else
            {
                chkCreate.Checked = false;
            }

            if (flagRead == "1")
            {
                chkRead.Checked = true;
            }
            else
            {
                chkRead.Checked = false;
            }

            if (flagWrite == "1")
            {
                chkWrite.Checked = true;
            }
            else
            {
                chkWrite.Checked = false;
            }

            if (flagDelete == "1")
            {
                chkDelete.Checked = true;
            }
            else
            {
                chkDelete.Checked = false;
            }

            if (flagApproval == "1")
            {
                chkApproval.Checked = true;
            }
            else
            {
                chkApproval.Checked = false;
                ddlApprovalType.SelectedIndex = 0;
            }
            */
            #endregion
            chkSelect.Checked = CekAccessDetail("0");
            chkCreate.Checked = CekAccessDetail(flagCreate);
            chkRead.Checked = CekAccessDetail(flagRead);
            chkWrite.Checked = CekAccessDetail(flagWrite);
            chkDelete.Checked = CekAccessDetail(flagDelete);
            chkApproval.Checked = CekAccessDetail(flagApproval);

            for (int x = 0; x < dtSelectedDetail.Rows.Count; x++)
            {
                if (dtSelectedDetail.Rows[x]["MenuID"].ToString() == MenuID)
                {
                    chkSelect.Checked=true;
                    break;
                }
            }
        }
    }
    protected Boolean CekAccessDetail(String Flag)
    {
        Boolean FlagCheck = false;
        if(Flag == "1")
        {
            FlagCheck = true;
        }
        return FlagCheck;
    }
    protected void PopulateSelected()
    {
        mlMessage.Text = "";
        string lblischecked = "";
        string MenuID = "";
        Boolean ischecked = false;

        for (int i = 0; i < dtgDetailGroup.Items.Count; i++)
        {
            lblischecked = ((Label)dtgDetailGroup.Items[i].FindControl("lblSelect")).Text;
            ischecked = ((CheckBox)dtgDetailGroup.Items[i].FindControl("chkSelect")).Checked;

            if (ischecked == true && lblischecked == "0")
            {
                String lblAccessCreate = ((Label)dtgDetailGroup.Items[i].FindControl("lblAccessCreate")).Text;
                String lblAccessRead = ((Label)dtgDetailGroup.Items[i].FindControl("lblAccessRead")).Text;
                String lblAccessWrite = ((Label)dtgDetailGroup.Items[i].FindControl("lblAccessWrite")).Text;
                String lblAccessDelete = ((Label)dtgDetailGroup.Items[i].FindControl("lblAccessDelete")).Text;
                String lblAccessApproval = ((Label)dtgDetailGroup.Items[i].FindControl("lblAccessApproval")).Text;
                String lblApprovalTypeID = ((Label)dtgDetailGroup.Items[i].FindControl("lblApprovalTypeID")).Text;

                Boolean CheckAccessCreate = ((CheckBox)dtgDetailGroup.Items[i].FindControl("chkAccessCreate")).Checked;
                Boolean CheckAccessRead = ((CheckBox)dtgDetailGroup.Items[i].FindControl("chkAccessRead")).Checked;
                Boolean CheckAccessWrite = ((CheckBox)dtgDetailGroup.Items[i].FindControl("chkAccessWrite")).Checked;
                Boolean CheckAccessDelete = ((CheckBox)dtgDetailGroup.Items[i].FindControl("chkAccessDelete")).Checked;
                Boolean CheckAccessApproval = ((CheckBox)dtgDetailGroup.Items[i].FindControl("chkAccessApproval")).Checked;

                lblAccessCreate = CekAksesSelected(CheckAccessCreate, lblAccessCreate);
                lblAccessRead = CekAksesSelected(CheckAccessRead, lblAccessRead);
                lblAccessWrite = CekAksesSelected(CheckAccessWrite, lblAccessWrite);
                lblAccessDelete = CekAksesSelected(CheckAccessDelete, lblAccessDelete);
                lblAccessApproval = CekAksesSelected(CheckAccessApproval, lblAccessApproval);

                lblApprovalTypeID = ((DropDownList)dtgDetailGroup.Items[i].FindControl("ddlApprovalType")).SelectedValue.ToString();

                MenuID = ((HyperLink)dtgDetailGroup.Items[i].FindControl("hlmenuid")).Text;
                for (int x = 0; x < dtSelectedDetail.Rows.Count; x++)
                {
                    if (dtSelectedDetail.Rows[x]["MenuID"].ToString() == MenuID)
                    {
                        dtSelectedDetail.Rows.RemoveAt(x);
                        break;
                    }
                }
                DataRow drDetail = dtSelectedDetail.NewRow();
                drDetail["MenuID"] = ((HyperLink)dtgDetailGroup.Items[i].FindControl("hlmenuid")).Text;
                drDetail["MenuName"] = (((Label)dtgDetailGroup.Items[i].FindControl("lblmenuname")).Text).Replace("&nbsp;","");
                drDetail["FlagCreate"] = lblAccessCreate;
                drDetail["FlagRead"] = lblAccessRead;
                drDetail["FlagWrite"] = lblAccessWrite;
                drDetail["FlagDelete"] = lblAccessDelete;
                drDetail["FlagApproval"] = lblAccessApproval;
                drDetail["ApprovalTypeID"] = lblApprovalTypeID;
                dtSelectedDetail.Rows.Add(drDetail);

                lblischecked = "1";
            }
            else
            {
                MenuID = ((HyperLink)dtgDetailGroup.Items[i].FindControl("hlmenuid")).Text;
                for (int x = 0; x < dtSelectedDetail.Rows.Count; x++)
                {
                    if (dtSelectedDetail.Rows[x]["MenuID"].ToString() == MenuID)
                    {
                        dtSelectedDetail.Rows.RemoveAt(x);
                        lblischecked = "0";
                        break;
                    }
                }

            }
        }

    }
    protected string CekAksesSelected(Boolean CheckAccess,String lblAcces)
    {
        String lblAccesSelected = "1";
        if(CheckAccess == true)
        {
            if (lblAcces == "0" || lblAcces == "")
            {
                lblAccesSelected = "1";
            }
        }
        else if (CheckAccess == false)
        {
            if (lblAcces == "1")
            {
                lblAccesSelected = "0";
            }
        }
        else
        {
            lblAccesSelected = lblAcces;
        }

        return lblAccesSelected;
    }
    protected void GetApprovalType()
    {
        oEnt.mpCompanyID = "AD";
        oEnt.mpModule = "AD";
        oEnt.ErrorMesssage = "";
        oDA.GetApprovalType(oEnt);
        if (oEnt.ErrorMesssage != "")
        {
            mlMessage.Text = oEnt.ErrorMesssage;
            return;
        }
        dtListApproval = new DataTable();
        dtListApproval = oEnt.dtListData;
    }
    protected void retrievepagingDetail()
    {
        currentPage = pagingDtGroup.currentPage;
        PageSize = int.Parse(pagingDtGroup.PageSize.ToString());
        GetDetailGroup();
        //BindGridDetail();
        PopulateSelected();
    }
    protected void NavigationButtonClickedDt(usercontroller_ucPaging.NavigationButtonEventArgs e)
    {
        retrievepagingDetail();
        imbNext.Visible = false;
    }

    protected void imbSelected_Click(object sender, ImageClickEventArgs e)
    {
        PopulateSelected();
        imbNext.Visible = true;
    }
    protected void imbNext_Click(object sender, ImageClickEventArgs e)
    {
        if(dtSelectedDetail.Rows.Count <= 0)
        {
            mlMessage.Text = "Tidak ada data yang dipilih...silahkan masukkan data yang dipilih..";
            return;
        }
        btSaveRecord.Visible = true;
    }
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        ClickSearch();
    }
    protected void btCancelOperation_Click(object sender, ImageClickEventArgs e)
    {
        ResetRecord();
    }
    protected void btSaveRecord_Click(object sender, ImageClickEventArgs e)
    {
        SaveRecord();
    }
    protected void btNewRecord_Click(object sender, ImageClickEventArgs e)
    {
        NewRecord();
        pnlDetail.Visible = true;
        pnlGrid.Visible = false;
        pnlSearch.Visible = false;
        btNewRecord.Visible = false;
        btSaveRecord.Visible = false;
        btSearchRecord.Visible = false;

        //PopulateSelected();

    }
    protected void btSearchRecord_Click(object sender, ImageClickEventArgs e)
    {
        pnlSearch.Visible = true;
    }

}