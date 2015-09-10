using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using IAS.Core.EncryptDecrypt;
using IAS.Core.CSCode;
using IAS.Initialize;
using IAS.APP.DataAccess.AD;

public partial class pj_ad_ad_systemuser : System.Web.UI.Page
{

    ModuleDBFunction oMDBF = new ModuleDBFunction();
    ModuleGeneralFunction oMGF = new ModuleGeneralFunction();
    ModuleGeneralSystem oMGS = new ModuleGeneralSystem();
    ModuleInitialization oMI = new ModuleInitialization();
    ModuleEncryptDecrypt oMEnc = new ModuleEncryptDecrypt();
    ModuleFunctionLocal mlOBJPJ = new ModuleFunctionLocal();

    FunctionCore oFunc = new FunctionCore();
    VariableCore oVar = new VariableCore();

    VariableAD_SystemUser oEnt = new VariableAD_SystemUser();
    FunctionAD_SystemUser oDA = new FunctionAD_SystemUser();

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
    protected DataTable mlDT_GROUPMENU
    {
        get { return ((DataTable)ViewState["mlDT_GROUPMENU"]); }
        set { ViewState["mlDT_GROUPMENU"] = value; }
    }
    protected DataTable mlDT_BRANCH
    {
        get { return ((DataTable)ViewState["mlDT_BRANCH"]); }
        set { ViewState["mlDT_BRANCH"] = value; }
    }
    protected String mlNIK
    {
        get { return ((String)ViewState["mlNIK"]); }
        set { ViewState["mlNIK"] = value; }
    }
    protected String mlURLNIK
    {
        get { return ((String)ViewState["mlURLNIK"]); }
        set { ViewState["mlURLNIK"] = value; }
    }
    protected String mlGROUPMENU
    {
        get { return ((String)ViewState["mlGROUPMENU"]); }
        set { ViewState["mlGROUPMENU"] = value; }
    }
    protected String mlFLAGSITECARD
    {
        get { return ((String)ViewState["mlFLAGSITECARD"]); }
        set { ViewState["mlFLAGSITECARD"] = value; }
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
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"].ToString() + " SYSTEM USER V2.02";
        mlTITLE.Text = " SYSTEM USER V2.02";
        Session["mgDateTime"] = System.DateTime.Now;

        CekSession();

        oEnt.mpCompanyID = "AD";
        oEnt.mpModule = "AD";

        if (!IsPostBack)
        {
            mlPAGESIZE = int.Parse(pagingSystemUser.PageSize.ToString());
            mlCURRENTPAGE = 1;
            mlCMDWHERE = "";
            mlSORTBY = "";

            mlDT_GROUPMENU = new DataTable();
            mlDT_GROUPMENU.Columns.Add("groupmenu");
            mlDT_GROUPMENU.Columns.Add("entityid");
            mlDT_GROUPMENU.Columns.Add("branchid");
            mlDT_GROUPMENU.Columns.Add("flagSiteCard");


            FillDDL();
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
    protected void FillDDL()
    {
        oEnt.ErrorMesssage = "";
        oEnt.FlagType = "DEPARTEMENT";
        oDA.GetSystemuserDataHR(oEnt);
        if(oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }
        ddlDept.Items.Clear();
        for(int i=0;i<oEnt.dtListData.Rows.Count;i++)
        {
            ddlDept.Items.Add(new ListItem(oEnt.dtListData.Rows[i]["DATA"].ToString(), oEnt.dtListData.Rows[i]["ID"].ToString()));
        }


        oEnt.ErrorMesssage = "";
        oEnt.FlagType = "BRANCH";
        oDA.GetSystemuserDataHR(oEnt);
        if (oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }
        mlDT_BRANCH = oEnt.dtListData.Copy();       // di Copy datatablenya, buat ngisi BranchID yg ada di GRID Group Menu
        ddlBranch.Items.Clear();
        ddlBranchGroupMenu.Items.Clear();

        ddlBranchGroupMenu.Items.Add(new ListItem("Select One", ""));
        ddlBranchGroupMenu.Items.Add(new ListItem("ALL", "ALL"));

        for (int i = 0; i < oEnt.dtListData.Rows.Count; i++)
        {
            ddlBranch.Items.Add(new ListItem(oEnt.dtListData.Rows[i]["DATA"].ToString(), oEnt.dtListData.Rows[i]["ID"].ToString()));
            ddlBranchGroupMenu.Items.Add(new ListItem(oEnt.dtListData.Rows[i]["DATA"].ToString(), oEnt.dtListData.Rows[i]["ID"].ToString()));
        }
    }
    protected void getDataDDL(DropDownList ddl,String ID,String Teks)
    {

        for(int i=0;i<ddl.Items.Count;i++)
        {
            if(ddl.Items[i].Value == ID )
            {
                ddl.SelectedIndex = i;
                break;
            }
            if (ddl.Items[i].Text == Teks)
            {
                ddl.SelectedIndex = i;
                break;
            }
        }
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
        oDA.GetSystemUserPaging(oEnt);
        
        if (oEnt.ErrorMesssage != "")
        {
            return;
        }

        dgListData.DataSource = oEnt.dtListData;
        dgListData.DataBind();
        mlTOTALRECORDS = oEnt.TotalRecord;
        
        pagingSystemUser.PageSize = long.Parse(mlPAGESIZE.ToString());
        pagingSystemUser.currentPage = mlCURRENTPAGE;
        pagingSystemUser.PagingFooter(mlTOTALRECORDS, mlPAGESIZE);
    }
    protected void ResetRecord()
    {
        Response.Redirect("ad_systemuser.aspx");
    }
    protected void retrievepaging()
    {
        mlCURRENTPAGE = pagingSystemUser.currentPage;
        mlPAGESIZE = int.Parse(pagingSystemUser.PageSize.ToString());        
        BindGrid();
    }
    protected void SaveRecord()
    {
        if(mlDT_GROUPMENU.Rows.Count <=0 && mlFLAGACTION != "DELETE")
        {
            mlMESSAGE.Text = "Sorry you can't Save, Please Add List Group Menu...";
            return;
        }

        if (mlFLAGACTION == "DELETE")
        {
            oEnt.FlagAction = mlFLAGACTION;
            oEnt.NIK = mlNIK;
        }
        else
        {
            oEnt.FlagAction = mlFLAGACTION;
            oEnt.NIK = txtNIK.Text;
            oEnt.Name = txtNama.Text;
            oEnt.PasswordUser = oMEnc.Encrypt(ModuleBaseSetting.DEFAULTPASSWORDUSER, System.Configuration.ConfigurationManager.AppSettings["mgENCRYPTCODE"].ToString());
            oEnt.Company = ddlEntity.SelectedValue;
            oEnt.GroupMenuID = mlDT_GROUPMENU.Rows[0]["groupmenu"].ToString();
            oEnt.DeptID = ddlDept.SelectedValue;
            oEnt.NoHP = txtNoHP.Text;
            oEnt.Email = txtEmail.Text;
            oEnt.LoginId = Session["mgUSERID"].ToString();
            oEnt.BranchID = ddlBranch.SelectedValue;
            oEnt.MenuStyle = ddlmenustyle.SelectedValue;
            oEnt.dtGroupMenu = mlDT_GROUPMENU.Copy();
        }
        oDA.SystemUser_GroupMenuSave(oEnt);
        if (oEnt.ErrorMesssage != "" && oEnt.ErrorMesssage != null)
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }
        ddlSearchBy.SelectedIndex = 0;
        txtSearchBy.Text = "";
        SearchRecord();
        pnlGrid.Visible = true;
        pnTOOLBAR.Visible = true;
        pnlInput.Visible = false;
        pnlSave.Visible = false;
        mlMESSAGE.Text = ModuleSystemConstant.MESSAGE_INSERT_SUCCESS;         

    }
    protected void NewRecord()
    {
        mlFLAGACTION = "ADD";
        pnlGrid.Visible = false;
        pnlSearch.Visible = false;
        pnlADDNew.Visible = true;
        btNewRecord.Visible = false;
        btSaveRecord.Visible = false;
        btSearchRecord.Visible = false;
    }
    protected void GetDetailData()
    {
        
        oEnt.ErrorMesssage = "";
        oEnt.NIK = mlNIK;
        oDA.GetSystemUserDetail(oEnt);
        if(oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }

        mlDT_LISTDATA = oEnt.dtListData.Copy();
        txtNIK.Text = mlDT_LISTDATA.Rows[0]["NIK"].ToString();
        txtNama.Text = mlDT_LISTDATA.Rows[0]["Name"].ToString();
        txtEmail.Text = mlDT_LISTDATA.Rows[0]["EmailAddr"].ToString();
        txtNoHP.Text = mlDT_LISTDATA.Rows[0]["TelHP"].ToString();
        txtGroupMenu.Text = mlDT_LISTDATA.Rows[0]["groupmenu"].ToString();
        getDataDDL(ddlDept, mlDT_LISTDATA.Rows[0]["DeptID"].ToString(), "");
        getDataDDL(ddlDept, mlDT_LISTDATA.Rows[0]["BranchIDUser"].ToString(), "");
        getDataDDL(ddlmenustyle, mlDT_LISTDATA.Rows[0]["Menustyle"].ToString(), "");

        mlCURRENTPAGE = 1;
        mlPAGESIZE = 10;
        mlCMDWHERE = " and ug.userid = '" + mlNIK + "'";
        mlSORTBY = "";
        BindGridGroup();
        for (int i = 0; i < dgGroupMenu.Items.Count; i++)
        {
            DataRow drGroupMenu = mlDT_GROUPMENU.NewRow();
            drGroupMenu["groupmenu"] = ((Label)dgGroupMenu.Items[i].FindControl("lblGroupMenu")).Text;
            drGroupMenu["entityid"] = ((Label)dgGroupMenu.Items[i].FindControl("lblentityid")).Text;
            drGroupMenu["branchid"] = ((Label)dgGroupMenu.Items[i].FindControl("lblbranchid")).Text;
            drGroupMenu["flagSiteCard"] = ((Label)dgGroupMenu.Items[i].FindControl("lblflagSiteCard")).Text;
            mlDT_GROUPMENU.Rows.Add(drGroupMenu);
        }

        //for(int i = 0; i < mlDT_LISTDATA.Rows.Count; i++)
        //{
        //    for(int n = 0; n < dgGroupMenu.Items.Count;n++)
        //    {
        //        if(mlDT_LISTDATA.Rows[i]["groupmenu"].ToString() == ((Label)dgGroupMenu.Items[n].FindControl("lblgroupmenu")).Text)
        //        {
        //            ((CheckBox)dgGroupMenu.Items[n].FindControl("chkSelect")).Checked = true;
        //            ((Label)dgGroupMenu.Items[n].FindControl("lblSelect")).Text = "1";
        //            break;
        //        }
        //    }
        //}

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
            ImageButton imbDelete = ((ImageButton)e.Item.FindControl("imbDelete"));
            imbDelete.Attributes.Add("OnClick", "javascript:return confirm('Are You Sure want to Delete This Record..??')");


        }
    }
    protected void DataGridItemCommand(DataGridCommandEventArgs e)
    {
        mlNIK = ((Label)e.Item.FindControl("lblNIK")).Text.ToString();
        mlMESSAGE.Text = "";
        switch (e.CommandName)
        {
            case "Edit":
                mlFLAGACTION = "EDIT";                
                GetDetailData();
                pnlInput.Visible = true;
                pnlGridGroupMenu.Visible = true;
                pnlGrid.Visible = false;
                pnlSearch.Visible = false;
                btNewRecord.Visible = false;
                btSaveRecord.Visible = true;
                btSearchRecord.Visible = false;
                break;
            case "Delete":
                mlFLAGACTION = "DELETE";
                SaveRecord();
                break;            
        }
    }
    protected void DataGridSortCommand(DataGridSortCommandEventArgs e)
    {
        if (!this.mlSORTBY.Contains("desc"))
        {
            this.mlSORTBY = e.SortExpression + " desc";
        }
        else
        {
            this.mlSORTBY = e.SortExpression + " asc ";
        }
        BindGrid();
    }

    protected void imbSave_Click(object sender, ImageClickEventArgs e)
    {
        SaveRecord();
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
    protected void btSearchRecord_Click(object sender, ImageClickEventArgs e)
    {
        pnlSearch.Visible = true;
    }
    protected void btCancelOperation_Click(object sender, ImageClickEventArgs e)
    {
        ResetRecord();
    }

    protected void imbLookUpNIK_Click(object sender, ImageClickEventArgs e)
    {
        DataSet mlDATASET = new DataSet();

        mlURLNIK = "http://10.62.0.43/iss/a2a/?key=eadgb_c33ff210_ccff00&nik=" + txtNIKEmployee.Text;
        mlDATASET.ReadXml(mlURLNIK, XmlReadMode.Auto);
        if (mlDATASET.Tables[0].Rows.Count <= 0)
        {
            mlMESSAGE.Text = "Sorry, NIK Not Found...";
            return;
        }

        pnlInput.Visible = true;
        pnlADDNew.Visible = false;
        pnlGridGroupMenu.Visible = true;
        btSaveRecord.Visible = true;

        mlDT_LISTDATA = mlDATASET.Tables[0].Copy();
        txtNIK.Text = mlDT_LISTDATA.Rows[0]["nik"].ToString();
        txtNama.Text = mlDT_LISTDATA.Rows[0]["nama"].ToString();
        txtEmail.Text = mlDT_LISTDATA.Rows[0]["email"].ToString();
        txtGroupMenu.Text = "";
        txtNoHP.Text = mlDT_LISTDATA.Rows[0]["hp"].ToString();
        getDataDDL(ddlDept, "", mlDT_LISTDATA.Rows[0]["Dept"].ToString());
        getDataDDL(ddlBranch, mlDT_LISTDATA.Rows[0]["Branch"].ToString(),"");

        mlCURRENTPAGE = 1;
        mlCMDWHERE = " and ug.userid = '"+ txtNIK.Text +"'";
        mlSORTBY = "";        
        BindGridGroup();

    }


    protected void dgGroupMenu_SortCommand(object source, DataGridSortCommandEventArgs e)
    {

    }
    protected void dgGroupMenu_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        switch(e.CommandName)
        {
            case "DELETE":
                mlGROUPMENU = ((Label)e.Item.FindControl("lblGroupmenu")).Text;
                DeleteGroupMenu(e.Item.ItemIndex);
                break;
        }
    }
    protected void dgGroupMenu_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemIndex >=0)
        {
            String BranchID = ((Label)e.Item.FindControl("lblBranchID")).Text;
            String EntityID = ((Label)e.Item.FindControl("lblentityid")).Text;
            String GroupMenu = ((Label)e.Item.FindControl("lblgroupmenu")).Text;

            String FlagSiteCard = ((Label)e.Item.FindControl("lblFlagSiteCard")).Text.ToString();
            if(FlagSiteCard == "1")
            {
                ((HyperLink)e.Item.FindControl("hlSiteCard")).Visible = true;
            }       
            else
            {
                ((HyperLink)e.Item.FindControl("hlSiteCard")).Visible = false;
            }

            ((HyperLink)e.Item.FindControl("hlSiteCard")).NavigateUrl = "javascript:OpenWinLookUp('"+ txtNIK.Text +"','"+ txtNama.Text +"','"+ GroupMenu +"','" + BranchID + "','"+ EntityID +"')";      


        }
    }    
    protected void DeleteGroupMenu(int i)
    {
        mlDT_GROUPMENU.Rows.RemoveAt(i);
        dgGroupMenu.DataSource = mlDT_GROUPMENU;
        dgGroupMenu.DataBind();
    }
    protected void BindGridGroup()
    {

        oEnt.ErrorMesssage = "";
        oEnt.PageSize = mlPAGESIZE;
        oEnt.CurrentPage = mlCURRENTPAGE;
        oEnt.WhereCond = mlCMDWHERE;
        oEnt.SortBy = mlSORTBY;
        oDA.GetSystemUserGroupMenuPaging(oEnt);

        if (oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = "Data Group Menu Not Found...";
            return;
        }

        mlDT_LISTDATA = oEnt.dtListData.Copy();
        dgGroupMenu.DataSource = oEnt.dtListData;
        dgGroupMenu.DataBind();
        mlTOTALRECORDS = oEnt.TotalRecord;

    }
    //protected void PopulateGroupMenu()
    //{
    //    for(int i =0;i<dgGroupMenu.Items.Count;i++)
    //    {
    //        if(((CheckBox)dgGroupMenu.Items[i].FindControl("chkSelect")).Checked && ((Label)dgGroupMenu.Items[i].FindControl("lblSelect")).Text == "0")
    //        {
    //            String GroupMenu    = ((Label)dgGroupMenu.Items[i].FindControl("lblgroupmenu")).Text;
    //            String Entity       = ((Label)dgGroupMenu.Items[i].FindControl("lblentityid")).Text;
    //            String BranchID     = ((Label)dgGroupMenu.Items[i].FindControl("lblbranchid")).Text;
    //            for (int x = 0; x < mlDT_GROUPMENU.Rows.Count; x++)
    //            {
    //                if (mlDT_GROUPMENU.Rows[x]["groupmenu"].ToString() == GroupMenu &&
    //                    mlDT_GROUPMENU.Rows[x]["entityid"].ToString()  == Entity &&
    //                    mlDT_GROUPMENU.Rows[x]["branchid"].ToString()  == BranchID)
    //                {
    //                    mlDT_GROUPMENU.Rows.RemoveAt(x);
    //                    break;
    //                }
    //            }


    //            DataRow drGroupMenu = mlDT_GROUPMENU.NewRow();
    //            drGroupMenu["groupmenu"]= ((Label)dgGroupMenu.Items[i].FindControl("lblgroupmenu")).Text ;
    //            drGroupMenu["entityid"] = ((Label)dgGroupMenu.Items[i].FindControl("lblentityid")).Text ;
    //            drGroupMenu["branchid"] = ((Label)dgGroupMenu.Items[i].FindControl("lblbranchid")).Text;
    //            mlDT_GROUPMENU.Rows.Add(drGroupMenu);

    //            ((Label)dgGroupMenu.Items[i].FindControl("lblSelect")).Text = "1";
    //        }
    //        else
    //        {
    //            for(int n = 0;n < mlDT_GROUPMENU.Rows.Count;n++)
    //            {
    //                if (((Label)dgGroupMenu.Items[i].FindControl("lblgroupmenu")).Text == mlDT_GROUPMENU.Rows[n]["groupmenu"].ToString())
    //                {
    //                    mlDT_GROUPMENU.Rows.RemoveAt(n);
    //                    ((Label)dgGroupMenu.Items[i].FindControl("lblSelect")).Text = "0";
    //                }
    //            }
    //        }
    //    }
    //}

    protected void imbInsertGroupMenu_Click(object sender, ImageClickEventArgs e)
    {
        DataRow drGroup = mlDT_GROUPMENU.NewRow();
        drGroup["groupmenu"] = ucGroupMenu.GroupMenu;
        drGroup["entityid"] = ddlEntityGroupMenu.SelectedValue;
        drGroup["branchid"] = ddlBranchGroupMenu.SelectedValue;
        if (ddlBranchGroupMenu.SelectedValue == "ALL")
        {
            mlFLAGSITECARD = "1";   // jika branchID di set=ALL, maka lgsg dianggap punya SiteCard
        }
        drGroup["flagSiteCard"] = mlFLAGSITECARD;
        mlDT_GROUPMENU.Rows.Add(drGroup);
        dgGroupMenu.DataSource = mlDT_GROUPMENU;
        dgGroupMenu.DataBind();

        pnlAddGroupMenu.Visible = false;
        pnlGridGroupMenu.Visible = true;
        btSaveRecord.Visible = true;
    }
    protected void imbAdd_Click(object sender, ImageClickEventArgs e)
    {
        mlMESSAGE.Text = "";
        pnlAddGroupMenu.Visible = true;
        pnlGridGroupMenu.Visible = false;
        pnlSave.Visible = false;
        btSaveRecord.Visible = true;
        ddlBranchGroupMenu.SelectedIndex = 0;
        ddlEntityGroupMenu.SelectedIndex = 0;
        ucGroupMenu.GroupMenu = "";
    }
    protected void chkIsSiteCard_CheckedChanged(object sender, EventArgs e)
    {
        if(((CheckBox)sender).Checked == true)
        {
            mlFLAGSITECARD = "1";
        }
        else
        {
            mlFLAGSITECARD = "0";
        }

     }
    protected void btSaveRecord_Click(object sender, ImageClickEventArgs e)
    {
        SaveRecord();
    }
}