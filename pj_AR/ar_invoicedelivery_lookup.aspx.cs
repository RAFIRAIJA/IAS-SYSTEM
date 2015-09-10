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

public partial class pj_ar_ar_invoicedelivery_lookup : System.Web.UI.Page
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

    protected string ReceiptCode
    {
        get { return ((string)ViewState["ReceiptCode"]); }
        set { ViewState["ReceiptCode"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"].ToString() + " INVOICE DELIVERY V2.02";
        mlTITLE.Text = "LOOKUP INFORMATION RECEIPTCODE V2.02";
        Session["mgDateTime"] = System.DateTime.Now;

        if (Session["mgUSERID"] == null)
        {
            Response.Redirect("~/pageconfirmation.aspx?mpMESSAGE=34FC35D4");
            return;
        }

        oEnt.CompanyID = "ISSP3";
        oEnt.ModuleID = "PB";
        imbClose.Attributes.Add("onclick", "return window.close();");
        if (!IsPostBack)
        {
            ReceiptCode = Request.QueryString["ReceiptCode"].ToString();
    
            SearchRecord();
        }
    }   
    protected void dgListData_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
        {
            
        }
    }

    //protected void NavigationButtonClicked(usercontroller_ucPaging.NavigationButtonEventArgs e)
    //{
    //    retrievepaging();
    //}
    //protected void retrievepaging()
    //{
    //    currentPage = pagingReceiptCode.currentPage;
    //    RetrieveData();
    //}
    protected void RetrieveData()
    {
        oEnt.ErrorMesssage = "";
        oEnt.ReceiptCode = ReceiptCode;
        oDA.GetLookUpInvoiceDelivery(oEnt);
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

        lblReceiptCode.Text = oEnt.dtListData.Rows[0]["inv_receiptcode"].ToString();
        lblReceiptCodeStatus.Text = oEnt.dtListData.Rows[0]["inv_receiptstatus"].ToString();
        lblProceedDate.Text = oEnt.dtListData.Rows[0]["inv_proceedsdate"].ToString();
        lblSendingDate.Text = oEnt.dtListData.Rows[0]["inv_sendingdate"].ToString();
        lblDeliveryDate.Text = oEnt.dtListData.Rows[0]["inv_delivereddate"].ToString();
        lblReturnDate.Text = oEnt.dtListData.Rows[0]["inv_returneddate"].ToString();
        lblCustName.Text = oEnt.dtListData.Rows[0]["CustName"].ToString();
        lblCustAddress.Text = oEnt.dtListData.Rows[0]["CustAddress"].ToString();
        lblMessangerName.Text = oEnt.dtListData.Rows[0]["inv_messangername"].ToString();
        lblMessangerType.Text = oEnt.dtListData.Rows[0]["MessangerType"].ToString();

        //pagingReceiptCode.PageSize = PageSize;
        //pagingReceiptCode.currentPage = currentPage;
        //pagingReceiptCode.totalRecords = Convert.ToInt64(totalRecords.ToString());
        //pagingReceiptCode.PagingFooter(totalRecords, PageSize);
    }
    protected void SearchRecord()
    {
        cmdWhere = "";
        //if (ddlSearchBy.SelectedValue != "")
        //{
        //    if (txtSearchBy.Text.Contains("%"))
        //    {
        //        cmdWhere += " " + ddlSearchBy.SelectedValue + " like '%" + txtSearchBy.Text + "%'";
        //    }
        //    else
        //    {
        //        cmdWhere += " " + ddlSearchBy.SelectedValue + " = '" + txtSearchBy.Text + "'";
        //    }
        //}

        RetrieveData();
    }
}