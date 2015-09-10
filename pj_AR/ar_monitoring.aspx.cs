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

public partial class pj_ar_ar_monitoring : System.Web.UI.Page
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
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"].ToString() + " MONITORING INVOICE DELIVERY V2.02";
        mlTITLE.Text = "MONITORING INVOICE DELIVERY V2.02";
        Session["mgDateTime"] = System.DateTime.Now;

        CekSession();
    
        oEnt.CompanyID = "ISSP3";
        oEnt.ModuleID = "PB";
        if (!IsPostBack)
        {
            PageSize = int.Parse(pagingMonitoring.PageSize.ToString());
            currentPage = 1;
            cmdWhere = "";
            sortBy = "";

            ucStartDate.dateValue = DateTime.Now.ToString("dd/MM/yyyy");
            ucStartDate.isEnabled = false;

            ucEndDate.dateValue = DateTime.Now.ToString("dd/MM/yyyy");
            ucEndDate.isEnabled = false;

            ddlEntity.SelectedIndex = 1;
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
        currentPage = pagingMonitoring.currentPage;
        PageSize = int.Parse(pagingMonitoring.PageSize.ToString());
        RetrieveData();
    }
    protected void RetrieveData()
    {
        mlMESSAGE.Text = "";
        oEnt.ErrorMesssage = "";
        oEnt.PageSize = PageSize;
        oEnt.CurrentPage = currentPage;
        oEnt.WhereCond = cmdWhere;
        oEnt.SortBy = sortBy;
        oDA.GetMonitoringPaging(oEnt);
        if (oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }
        if (oEnt.dtListData.Rows.Count == 0)
        {
            mlMESSAGE.Text = ModuleSystemConstant.MESSAGE_DATA_NOT_FOUND;
        }
        else
        {
            dgListData.DataSource = oEnt.dtListData;
            dgListData.DataBind();

            totalRecords = oEnt.TotalRecord;

            pagingMonitoring.PageSize = PageSize;
            pagingMonitoring.currentPage = currentPage;
            pagingMonitoring.totalRecords = Convert.ToInt64(totalRecords.ToString());
            pagingMonitoring.PagingFooter(totalRecords, PageSize);
        }


        /////// databind untuk grid summary 
        mlMESSAGE.Text = "";
        cmdWhere = "";
        if(chkPeriode.Checked)
        {
            cmdWhere = " and a.InvDate between '" + oMGF.ConvertDate(ucStartDate.dateValue) + "' and '" + oMGF.ConvertDate(ucEndDate.dateValue) + "' ";
        }
        oEnt.ErrorMesssage = "";
        oEnt.FlagAction = "SUMMARY";
        oEnt.Entity_id = ddlEntity.SelectedValue;
        oEnt.WhereCond = cmdWhere;
        oDA.GetMonitoringSummary(oEnt);
        if (oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }
        if (oEnt.dtListData.Rows.Count == 0)
        {
            mlMESSAGE.Text = "Summary " + ModuleSystemConstant.MESSAGE_DATA_NOT_FOUND;
            
        }
        dgSummary.DataSource = oEnt.dtListData;
        dgSummary.DataBind();

        /////// databind untuk grid summary Detail
        mlMESSAGE.Text = "";
        oEnt.ErrorMesssage = "";
        oEnt.FlagAction = "SUMMARYDETAIL";
        oEnt.Entity_id = ddlEntity.SelectedValue;
        oEnt.WhereCond = cmdWhere;
        oDA.GetMonitoringSummary(oEnt);
        if (oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }
        if (oEnt.dtListData.Rows.Count == 0)
        {
            mlMESSAGE.Text = "Summary Detail " + ModuleSystemConstant.MESSAGE_DATA_NOT_FOUND;

        }
        dgSummaryDetail.DataSource = oEnt.dtListData;
        dgSummaryDetail.DataBind();


    }
    protected void SearchRecord()
    {
        cmdWhere = " and a.Entity = '"+ ddlEntity.SelectedValue +"' ";
        if (ddlSearchBy.SelectedValue != "")
        {
            if (txtSearchBy.Text.Contains("%"))
            {
                cmdWhere += " and " + ddlSearchBy.SelectedValue + " like '%" + txtSearchBy.Text + "%'";
            }
            else
            {
                    cmdWhere += " and " + ddlSearchBy.SelectedValue + " = '" + txtSearchBy.Text + "'";
            }
        }
        if (chkPeriode.Checked)
        {
            if (ddlSearchBy.SelectedIndex < 4 && ddlSearchBy.SelectedIndex > 6) //// khusus untuk selain Opsi Status Invoice
            {
                cmdWhere += " and a.InvDate between '" + oMGF.ConvertDate(ucStartDate.dateValue) + "' and '" + oMGF.ConvertDate(ucEndDate.dateValue) + "' ";
            }
        }
        RetrieveData();
    }
    protected void ResetRecord()
    {
        Response.Redirect("ar_monitoring.aspx");
    }
    protected void ChkPeriodeCheckedChanged(object sender, EventArgs e)
    {
        if (chkPeriode.Checked)
        {
            ucStartDate.isEnabled = true;
            ucEndDate.isEnabled = true;
        }
        else
        {
            ucStartDate.isEnabled = false;
            ucEndDate.isEnabled = false;
        }
    }
    protected void dgSummary_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemIndex >= 0)
        {
            if(((Label)e.Item.FindControl("lblProceeds")).Text == "")
            {
                ((Label)e.Item.FindControl("lblProceeds")).Text = "0";
            }
            if (((Label)e.Item.FindControl("lblDelivered")).Text == "")
            {
                ((Label)e.Item.FindControl("lblDelivered")).Text = "0";
            }
            if (((Label)e.Item.FindControl("lblDone")).Text == "")
            {
                ((Label)e.Item.FindControl("lblDone")).Text = "0";
            }

        }
    }
    protected void dgSummaryDetail_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

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
        Response.Redirect("ar_monitoring.aspx");
    }
    protected void btCancelOperation_Click(object sender, ImageClickEventArgs e)
    {
        ResetRecord(); 
    }
    
}