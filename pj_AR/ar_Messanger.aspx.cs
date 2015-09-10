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
using IAS.APP.DataAccess.AR;


public partial class pj_ar_ar_Messanger : System.Web.UI.Page
{
    #region LOCAL PARAMETER
    //EntitiesInvoiceDelivery oEnt = new EntitiesInvoiceDelivery();
    //DataAccessInvoiceDelivery oDA = new DataAccessInvoiceDelivery();

    OleDbDataReader mlREADER;
    OleDbDataReader mlREADER2;

    VariableCore mlVarCore = new VariableCore();
    FunctionAkses mlFuncAkses = new FunctionAkses();
    VariableAR_InvoiceDelivery oEnt = new VariableAR_InvoiceDelivery();
    FunctionAR_InvoiceDelivery oDA = new FunctionAR_InvoiceDelivery();

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
    protected string flagAction
    {
        get { return ((string)ViewState["flagAction"]); }
        set { ViewState["flagAction"] = value; }
    }
    protected string MessangerID
    {
        get { return ((string)ViewState["MessangerID"]); }
        set { ViewState["MessangerID"] = value; }
    }
    protected DataTable mlDTLISTDATA
    {
        get { return ((DataTable)ViewState["mlDTLISTDATA"]); }
        set { ViewState["mlDTLISTDATA"] = value; }
    }
    #endregion

    #region FORM and BUTTON

    protected void Page_PreInit(object sender, EventArgs e)
    {
        CekSession();
        mlMENUSTYLE = mlOBJPJ.AD_CHECKMENUSTYLE(Session["mgMENUSTYLE"].ToString(), this.MasterPageFile);
        this.MasterPageFile = mlMENUSTYLE;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"].ToString() + " MESSANGER V2.02";
        mlTITLE.Text = " MESSANGER V2.02";
        oEnt.CompanyID = "ISS";
        oEnt.ModuleID = "PB";
        CekSession();
        if (!IsPostBack)
        {
            GetAksesMenu();
            PageSize = int.Parse(pagingMessanger.PageSize.ToString());
            currentPage = 1;
            cmdWhere = "";
            sortBy = "";
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
    protected void GetAksesMenu()
    {
        mlVarCore.FlagType = "ENTITY";
        mlVarCore.mpFormID = "AR003";
        mlVarCore.LoginId = Session["mgUSERID"].ToString();
        mlFuncAkses.GetAksesMenu(mlVarCore);
        if(mlVarCore.dtListData.Rows.Count <=0)
        {
            mlMESSAGE.Text = ModuleSystemConstant.MESSAGE_DATA_NOT_FOUND;
            return;
        }
        mlDTLISTDATA = mlVarCore.dtListData.Copy();

        ddlEntity.Items.Clear();
        for(int i = 0;i<mlDTLISTDATA.Rows.Count;i++)
        {
            ddlEntity.Items.Add(new ListItem(mlDTLISTDATA.Rows[i]["DATA"].ToString(), mlDTLISTDATA.Rows[i]["ID"].ToString()));
        }
    }
    protected void btNewRecord_Click(object sender, ImageClickEventArgs e)
    {
        NewRecord();
    }
    protected void btSaveRecord_Click(object sender, ImageClickEventArgs e)
    {
        SaveRecord(); 
    }
    protected void btSearchRecord_Click(object sender, ImageClickEventArgs e)
    {
        pnlSearch.Visible = true;
    }
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchRecord();

    }
    protected void btnReset_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ar_invoicedelivery.aspx");
    }
    protected void dgMessanger_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

    }
    protected void dgMessanger_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
        {
            MessangerID = ((Label)e.Item.FindControl("lblMsCode")).Text;
            switch (e.CommandName)
            {
                case "edit":
                    flagAction = "EDIT";
                    RetrieveDataDetail();
                    break;
            }
        }
    }
    protected void NavigationButtonClicked(usercontroller_ucPaging.NavigationButtonEventArgs e)
    {
        retrievepaging();
    }
    protected void retrievepaging()
    {
        currentPage = pagingMessanger.currentPage;
        PageSize = int.Parse(pagingMessanger.PageSize.ToString());
        RetrieveData();
    }

    #endregion

    protected void FillDDL()
    {
        oEnt.ErrorMesssage = "";
        oDA.GetMessangerType_DataList(oEnt);
        ddlMessangerType.Items.Clear();
        ddlMessangerType.Items.Add(new ListItem("Choose One",""));
        for(int i=0;i<oEnt.dtListData.Rows.Count;i++)
        {
            ddlMessangerType.Items.Add(new ListItem(oEnt.dtListData.Rows[i]["DATA"].ToString(), oEnt.dtListData.Rows[i]["ID"].ToString()));
        }
    }
    protected void SaveRecord()
    {
        
        oEnt.ErrorMesssage = "";
        oEnt.MessangerID = txtMsCode.Text;
        oEnt.MessangerName  =txtMsName.Text;
        oEnt.MessangerType = ddlMessangerType.SelectedValue.ToString();
        oEnt.FlagAction = flagAction;
        oEnt.LoginID = Session["mgUSERID"].ToString();

        oDA.SaveMessanger(oEnt);
        if (oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }
        mlMESSAGE.Text = ModuleSystemConstant.MESSAGE_INSERT_SUCCESS;
        SearchRecord();
        Refresh_On();
    }
    protected void Refresh_On()
    {
        pnlMessanger.Visible = true;
        pnlInputData.Visible = false;
        btNewRecord.Visible = true;
        btSaveRecord.Visible = false;
        btSaveRecord.Visible = true;

        txtMsCode.Text = "";
        txtMsName.Text = "";
    }
    protected void NewRecord()
    {
        flagAction = "ADD";
        btSaveRecord.Visible = true;
        btNewRecord.Visible = false;
        btSearchRecord.Visible = false;

        pnlMessanger.Visible = false;
        pnlSearch.Visible = false;
        pnlInputData.Visible = true;
        RetrieveData();
    }
    protected void SearchRecord()
    {
        cmdWhere = "";
        if (ddlSearchBy.SelectedValue != "")
        {
            if (txtSearchBy.Text.Contains("%"))
            {
                cmdWhere += " " + ddlSearchBy.SelectedValue + " like '%" + txtSearchBy.Text + "%'";
            }
            else
            {
                cmdWhere += " " + ddlSearchBy.SelectedValue + " = '" + txtSearchBy.Text + "'";
            }
        }
        pnlMessanger.Visible = true;
        RetrieveData();
    }
    protected void RetrieveData()
    {
        oEnt.ErrorMesssage = "";
        oEnt.PageSize = PageSize;
        oEnt.CurrentPage = currentPage;
        oEnt.WhereCond = cmdWhere;
        oEnt.SortBy = sortBy;
        oDA.GetMessangerPaging(oEnt);
        dgMessanger.DataSource = oEnt.dtListData;
        dgMessanger.DataBind();

        totalRecords = oEnt.TotalRecord;

        pagingMessanger.PageSize = PageSize;
        pagingMessanger.currentPage = currentPage;
        pagingMessanger.totalRecords = Convert.ToInt64(totalRecords.ToString());
        pagingMessanger.PagingFooter(totalRecords, PageSize);
    }
    protected void RetrieveDataDetail()
    {
        oEnt.ErrorMesssage = "";
        oEnt.MessangerID = MessangerID;
        oDA.GetMessangerDetail(oEnt);
        if(oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }
        txtMsCode.Text = oEnt.dtListData.Rows[0]["inv_messangerid"].ToString();
        txtMsName.Text = oEnt.dtListData.Rows[0]["inv_messangername"].ToString();
        
        pnlInputData.Visible = true;
        pnlMessanger.Visible = false;
        pnlSearch.Visible = false;
        btSaveRecord.Visible = true;
        btNewRecord.Visible = false;
        btSearchRecord.Visible = false;
    }
    protected void ResetRecord()
    {
        Response.Redirect("ar_Messanger.aspx");
    }
    protected void btCancelOperation_Click(object sender, ImageClickEventArgs e)
    {
        ResetRecord();
    }
}