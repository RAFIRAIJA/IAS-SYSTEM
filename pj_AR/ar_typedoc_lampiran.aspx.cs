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

using IAS.Core.CSCode;
using IAS.Initialize;
using IAS.APP.DataAccess.AR;
//using IASClass;
//using ISS.App.Entities.pj_ad.administration;
//using ISS.App.Entities.pj_ar;
//using ISS.App.DataAccess.pj_ar;

public partial class pj_ar_ar_typedoc_lampiran : System.Web.UI.Page
{
    OleDbDataReader mlREADER;
    OleDbDataReader mlREADER2;

    VariableCore mlVarCore = new VariableCore();
    VariableAR_InvoiceDelivery oEnt = new VariableAR_InvoiceDelivery();
    FunctionAR_InvoiceDelivery oDA = new FunctionAR_InvoiceDelivery();

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


    protected string FlagAction
    {
        get { return ((string)ViewState["FlagAction"]); }
        set { ViewState["FlagAction"] = value; }
    }
    protected string TypeDocID
    {
        get { return ((string)ViewState["TypeDocID"]); }
        set { ViewState["TypeDocID"] = value; }
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
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"].ToString() + " TYPE DOCUMENT LAMPIRAN V2.02";
        mlTITLE.Text = "TYPE DOCUMENT LAMPIRAN V2.02";
        Session["mgDateTime"] = System.DateTime.Now;

        CekSession();

        oEnt.CompanyID = "ISS";
        oEnt.ModuleID = "PB";
        if (!IsPostBack)
        {
            PageSize = int.Parse(pagingDocType.PageSize.ToString());
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
    protected void dgListData_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
        {
            TypeDocID = ((Label)e.Item.FindControl("lblTypeDocID")).Text;
            switch (e.CommandName)
            {
                case "edit":
                    FlagAction = "EDIT";
                    RetrieveDataDetail();
                    break;
            }
        }
    }
    protected void dgListData_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

    }
    protected void NavigationButtonClicked(usercontroller_ucPaging.NavigationButtonEventArgs e)
    {
        retrievepaging();
    }
    protected void retrievepaging()
    {
        currentPage = pagingDocType.currentPage;
        PageSize = int.Parse(pagingDocType.PageSize.ToString());
        RetrieveData();
    }
    protected void RetrieveData()
    {
        oEnt.ErrorMesssage = "";
        oEnt.PageSize = PageSize;
        oEnt.CurrentPage = currentPage;
        oEnt.WhereCond = cmdWhere;
        oEnt.SortBy = sortBy;
        oDA.GetTypeDocumentPaging(oEnt);
        if (oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }
        if (oEnt.dtListData.Rows.Count == 0)
        {
            mlMESSAGE.Text = ModuleSystemConstant.MESSAGE_DATA_NOT_FOUND;
            return;
        }
        dgListData.DataSource = oEnt.dtListData;
        dgListData.DataBind();

        totalRecords = oEnt.TotalRecord;

        pagingDocType.PageSize = PageSize;
        pagingDocType.currentPage = currentPage;
        pagingDocType.totalRecords = Convert.ToInt64(totalRecords.ToString());
        pagingDocType.PagingFooter(totalRecords, PageSize);
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

        RetrieveData();
    }
    protected void NewRecord()
    {
        FlagAction = "ADD";
        btSaveRecord.Visible = true;
        btNewRecord.Visible = false;
        btSearchRecord.Visible = false;

        pnlListData.Visible = false;
        pnlSearch.Visible = false;
        pnlInputData.Visible = true;
    }
    protected void RetrieveDataDetail()
    {
        oEnt.ErrorMesssage = "";
        oEnt.TypeDocID = TypeDocID;
        oDA.GetTypeDocumentDetail(oEnt);
        if (oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }
        if (oEnt.dtListData.Rows.Count == 0)
        {
            mlMESSAGE.Text = ModuleSystemConstant.MESSAGE_DATA_NOT_FOUND;
            return;
        }
        txtDocTypeName.Text = oEnt.dtListData.Rows[0]["inv_TypeDoc_Name"].ToString();

        btSaveRecord.Visible = true;
        btNewRecord.Visible = false;
        btSearchRecord.Visible = false;

        pnlInputData.Visible = true;
        pnlListData.Visible = false;
        pnlSearch.Visible = false;
    }
    protected void SaveRecord()
    {
        
        oEnt.ErrorMesssage = "";
        oEnt.FlagAction = FlagAction;
        oEnt.TypeDocID = TypeDocID;
        oEnt.TypeDocName = txtDocTypeName.Text;
        oEnt.LoginID = Session["mgUSERID"].ToString();
        oDA.SaveTypeDocument(oEnt);
        if (oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }

        mlMESSAGE.Text = ModuleSystemConstant.MESSAGE_INSERT_SUCCESS;
        SearchRecord();
        pnlInputData.Visible = false;
        pnlListData.Visible = true;
        pnlSearch.Visible = true;
        btSaveRecord.Visible = false;
        btNewRecord.Visible = true;
        btSearchRecord.Visible = true;
    }
    protected void ResetRecord()
    {
        Response.Redirect("ar_typedoc_lampiran.aspx");
    }

    protected void btNewRecord_Click(object sender, ImageClickEventArgs e)
    {
        NewRecord();
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
    protected void btSaveRecord_Click(object sender, ImageClickEventArgs e)
    {
        SaveRecord();

    }
    protected void btCancelOperation_Click(object sender, ImageClickEventArgs e)
    {
        ResetRecord();
    }
}