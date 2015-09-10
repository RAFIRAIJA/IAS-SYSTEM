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

using IASClass;
//using IAS.Core.CSCode;
//using IAS.Initialize;
//using ISS.App.Entities.pj_ad.administration;
//using ISS.App.Entities.pj_ar;
//using ISS.App.DataAccess.pj_ar;

public partial class usercontroller_form_LookUp_INVMessanger : System.Web.UI.Page
{
    ucmGeneralSystem mlOBJGS = new ucmGeneralSystem();
    ucmGeneralFunction mlOBJGF = new ucmGeneralFunction();
    ModuleFunctionLocal mlOBJPJ = new ModuleFunctionLocal();

    OleDbDataReader mlREADER;
    OleDbDataReader mlREADER2;

    //GeneralSetting GS = new GeneralSetting();
    //EntitiesMenu oEntMenu = new EntitiesMenu();
    //EntitiesInvoiceDelivery oEnt = new EntitiesInvoiceDelivery();
    //DataAccessInvoiceDelivery oDA = new DataAccessInvoiceDelivery();

    VariableCore mlVarCore = new VariableCore();
    VariableAR_InvoiceDelivery oEnt = new VariableAR_InvoiceDelivery();
    FunctionAR_InvoiceDelivery oDA = new FunctionAR_InvoiceDelivery();


    ModuleGeneralFunction oMGF = new ModuleGeneralFunction();


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

    protected string ReciptCode
    {
        get { return ((string)ViewState["ReciptCode"]); }
        set { ViewState["ReciptCode"] = value; }
    }
    protected string CustCode
    {
        get { return ((string)ViewState["CustCode"]); }
        set { ViewState["CustCode"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"].ToString() + " LIST MESSANGER V2.02";
        Session["mgDateTime"] = System.DateTime.Now;

        if (Session["mgUSERID"] == null)
        {
            Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=34FC35D4");
            return;
        }

        oEnt.CompanyID = "ISS";
        oEnt.ModuleID = "PB";
        if (!IsPostBack)
        {
            PageSize = 10;
            currentPage = 1;
            cmdWhere = "";
            sortBy = "";

            SearchRecord();
            ddlSearchBy.SelectedIndex = 1;
        }
    }
    protected void RetrieveData()
    {
        oEnt.ErrorMesssage = "";
        oEnt.PageSize = PageSize;
        oEnt.CurrentPage = currentPage;
        oEnt.WhereCond = cmdWhere;
        oEnt.SortBy = sortBy;
        oDA.GetMessangerPaging(oEnt);
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

        pagingReceiptCode.PageSize = PageSize;
        pagingReceiptCode.currentPage = currentPage;
        pagingReceiptCode.totalRecords = Convert.ToInt64(totalRecords.ToString());
        pagingReceiptCode.PagingFooter(totalRecords, PageSize);
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
    protected void NavigationButtonClicked(usercontroller_ucPaging.NavigationButtonEventArgs e)
    {
        retrievepaging();
    }
    protected void retrievepaging()
    {
        currentPage = pagingReceiptCode.currentPage;
        RetrieveData();
    }

    protected void dgListData_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchRecord();
    }
}