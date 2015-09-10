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

public partial class pj_AD_ad_systemuser_listsitecard : System.Web.UI.Page
{
    ModuleDBFunction oMDBF = new ModuleDBFunction();
    ModuleGeneralFunction oMGF = new ModuleGeneralFunction();
    ModuleGeneralSystem oMGS = new ModuleGeneralSystem();
    ModuleInitialization oMI = new ModuleInitialization();
    ModuleEncryptDecrypt oMEnc = new ModuleEncryptDecrypt();


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
    protected string mlCMDWHERE2
    {
        get { return ((string)ViewState["mlCMDWHERE2"]); }
        set { ViewState["mlCMDWHERE2"] = value; }
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
    protected DataTable mlDT_SITECARD
    {
        get { return ((DataTable)ViewState["mlDT_SITECARD"]); }
        set { ViewState["mlDT_SITECARD"] = value; }
    }
    protected DataTable mlDT_BRANCH
    {
        get { return ((DataTable)ViewState["mlDT_BRANCH"]); }
        set { ViewState["mlDT_BRANCH"] = value; }
    }
    protected String mlBRANCHID
    {
        get { return ((String)ViewState["mlBRANCHID"]); }
        set { ViewState["mlBRANCHID"] = value; }
    }
    protected String mlENTITY
    {
        get { return ((String)ViewState["mlENTITY"]); }
        set { ViewState["mlENTITY"] = value; }
    }
    protected String mlGROUPMENU
    {
        get { return ((String)ViewState["mlGROUPMENU"]); }
        set { ViewState["mlGROUPMENU"] = value; }
    }
    protected String mlNIK
    {
        get { return ((String)ViewState["mlNIK"]); }
        set { ViewState["mlNIK"] = value; }
    }
    protected String mlNAMA
    {
        get { return ((String)ViewState["mlNAMA"]); }
        set { ViewState["mlNAMA"] = value; }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"].ToString() + " SYSTEM USER V2.02";
        Session["mgDateTime"] = System.DateTime.Now;

        if (Session["mgUSERID"] == null)
        {
            Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=34FC35D4");
            return;
        }

        oEnt.mpCompanyID = "AD";
        oEnt.mpModule = "AD";

        if (Request.QueryString["GroupMenu"].ToString() != "")
        {
            mlGROUPMENU = Request.QueryString["GroupMenu"].ToString();
        }
        if(Request.QueryString["BranchID"].ToString() != "")
        {
            mlBRANCHID = Request.QueryString["BranchID"].ToString();
        }
        if (Request.QueryString["EntityID"].ToString() != "")
        {
            mlENTITY = Request.QueryString["EntityID"].ToString();
        }
        if (Request.QueryString["NIK"].ToString() != "")
        {
            mlNIK = Request.QueryString["NIK"].ToString();
        }
        if (Request.QueryString["NAMA"].ToString() != "")
        {
            mlNAMA = Request.QueryString["NAMA"].ToString();
        }


        if (!IsPostBack)
        {
            mlPAGESIZE = int.Parse(pagingSitecard.PageSize.ToString());
            mlCURRENTPAGE = 1;
            mlCMDWHERE = "";
            mlSORTBY = "";
            mlCMDWHERE2 = "";

            mlDT_SITECARD = new DataTable();
            mlDT_SITECARD.Columns.Add("sitecardID");
            mlDT_SITECARD.Columns.Add("sitecardName");

            SearchRecord();
            mlFLAGACTION = "ADD";
        }
    }
    protected void SearchRecord()
    {
        GetWhereCond();
        BindGrid();
        lblGroupMenu.Text = mlGROUPMENU;
        lblBranch.Text = mlBRANCHID;
        lblEntity.Text = mlENTITY;
        lblNIK.Text = mlNIK;
        lblNama.Text = mlNAMA;
    }
    protected void GetWhereCond()
    {
        mlCMDWHERE = "  and a.Entity = '"+mlENTITY+"' ";
        mlCMDWHERE += " and a.BranchID = '" + mlBRANCHID + "' ";
        mlCMDWHERE2 = " and b.userID = '"+ mlNIK +"' ";

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
        oEnt.WhereCond2 = mlCMDWHERE2;
        oEnt.SortBy = mlSORTBY;
        oDA.GetSystemUser_GroupMenuListSiteCardpaging(oEnt);

        if (oEnt.ErrorMesssage != "")
        {
            return;
        }

        dgSitecardList.DataSource = oEnt.dtListData;
        dgSitecardList.DataBind();
        mlTOTALRECORDS = oEnt.TotalRecord;

        pagingSitecard.PageSize = long.Parse(mlPAGESIZE.ToString());
        pagingSitecard.currentPage = mlCURRENTPAGE;
        pagingSitecard.PagingFooter(mlTOTALRECORDS, mlPAGESIZE);
    }
    protected void NavigationButtonClicked(usercontroller_ucPaging.NavigationButtonEventArgs e)
    {
        retrievepaging();
    }
    protected void retrievepaging()
    {
        mlCURRENTPAGE = pagingSitecard.currentPage;
        mlPAGESIZE = int.Parse(pagingSitecard.PageSize.ToString());
        BindGrid();
    }
    protected void PopulateGroupMenu()
    {
        for (int i = 0; i < dgSitecardList.Items.Count; i++)
        {
            if (((CheckBox)dgSitecardList.Items[i].FindControl("chkSelect")).Checked && ((Label)dgSitecardList.Items[i].FindControl("lblSelect")).Text == "0")
            {
                String sitecardID = ((Label)dgSitecardList.Items[i].FindControl("lblsitecardID")).Text;                
                for (int x = 0; x < mlDT_SITECARD.Rows.Count; x++)
                {
                    if (mlDT_SITECARD.Rows[x]["sitecardID"].ToString() == sitecardID)
                    {
                        mlDT_SITECARD.Rows.RemoveAt(x);
                        break;
                    }
                }

                DataRow drGroupMenu = mlDT_SITECARD.NewRow();
                drGroupMenu["sitecardID"] = ((Label)dgSitecardList.Items[i].FindControl("lblsitecardid")).Text;
                drGroupMenu["sitecardName"] = ((Label)dgSitecardList.Items[i].FindControl("lblsitecardName")).Text;                         
                mlDT_SITECARD.Rows.Add(drGroupMenu);

                ((Label)dgSitecardList.Items[i].FindControl("lblSelect")).Text = "1";
            }
            else
            {
                for (int n = 0; n < mlDT_SITECARD.Rows.Count; n++)
                {
                    if (((Label)dgSitecardList.Items[i].FindControl("lblsitecardid")).Text == mlDT_SITECARD.Rows[n]["sitecardID"].ToString())
                    {
                        mlDT_SITECARD.Rows.RemoveAt(n);
                        ((Label)dgSitecardList.Items[i].FindControl("lblSelect")).Text = "0";
                    }
                }
            }
        }
    }
    protected void SaveRecord()
    {
        oEnt.ErrorMesssage  = "";
        oEnt.FlagAction     = mlFLAGACTION;
        oEnt.dtListData     = mlDT_SITECARD.Copy();
        oEnt.mpEntityID     = lblEntity.Text;
        oEnt.NIK            = mlNIK;
        oEnt.Name           = mlNAMA;
        oEnt.GroupMenuID    = lblGroupMenu.Text;
        oEnt.BranchID       = lblBranch.Text;
        oEnt.LoginId        = Session["mgUSERID"].ToString();
        oDA.SystemUser_ListSiteCardSave(oEnt);
        if(oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }

        SearchRecord();
        mlMESSAGE.Text = ModuleSystemConstant.MESSAGE_INSERT_SUCCESS;
        imbSave.Visible = false;
        imbExit.Visible = false;
        imbnext.Visible = false;
    }
    protected void dgSitecardList_ItemCommand(object source, DataGridCommandEventArgs e)
    {

    }
    protected void dgSitecardList_SortCommand(object source, DataGridSortCommandEventArgs e)
    {

    }
    protected void dgSitecardList_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemIndex>=0)
        {
            String FlagSelect = ((Label)e.Item.FindControl("lblFlagSelect")).Text;
            if(FlagSelect == "1")
            {
                ((CheckBox)e.Item.FindControl("chkSelect")).Checked = true;
            }
            else
            {
                ((CheckBox)e.Item.FindControl("chkSelect")).Checked = false;
            }
        }
    }
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchRecord();
    }
    protected void btnReset_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ad_systemuser_listsitecard.aspx");
    }
    protected void imbSaveSelected_Click(object sender, ImageClickEventArgs e)
    {
        mlMESSAGE.Text = "";
        PopulateGroupMenu();
        imbnext.Visible = true;
    }
    protected void imbSave_Click(object sender, ImageClickEventArgs e)
    {
        SaveRecord();
    }
    protected void imbnext_Click(object sender, ImageClickEventArgs e)
    {
        imbSave.Visible = true;
        imbExit.Visible = true;
    }
    protected void imbExit_Click(object sender, ImageClickEventArgs e)
    {
        
    }
}