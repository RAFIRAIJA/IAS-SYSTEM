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
using Microsoft.Reporting.WebForms;

//using IASClass;
//using ISS.App.Entities.pj_ad.administration;
//using ISS.App.Entities.pj_ar;
//using ISS.App.DataAccess.pj_ar;

public partial class pj_ar_ar_monitoringmessanger : System.Web.UI.Page
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
    ReportDataSource rds = new ReportDataSource();



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
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"].ToString() + " MONITORING INVOICE DELIVERY BY MESSANGER V2.02";
        mlTITLE.Text = "MONITORING INVOICE DELIVERY BY MESSANGER V2.02";
        Session["mgDateTime"] = System.DateTime.Now;

        CekSession();

        oEnt.CompanyID = "ISSP3";
        oEnt.ModuleID = "PB";
        if (!IsPostBack)
        {
            PageSize = int.Parse(pagingMonitoring.PageSize.ToString());
            currentPage = 1;
            totalPages = 0;
            cmdWhere = "";
            sortBy = "";

            ucStartDate.dateValue = DateTime.Now.ToString("dd/MM/yyyy");
            ucEndDate.dateValue = DateTime.Now.ToString("dd/MM/yyyy");

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
        if(e.Item.ItemIndex >=0)
        {
            String ReceiptCode = ((HyperLink)e.Item.FindControl("hlReceiptCode")).Text;
            ((HyperLink)e.Item.FindControl("hlReceiptCode")).NavigateUrl = "javascript:OpenWinLookUp('"+ ReceiptCode +"')";              
        }
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
        oDA.GetMonitoringMessangerPaging(oEnt);
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

    }
    protected void SearchRecord()
    {
        cmdWhere = " a.Entity = '" + ddlEntity.SelectedValue + "' ";
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
        if (ddlSearchBy.SelectedIndex == 1)
        {
            cmdWhere += " and a.InvDate between '" + oMGF.ConvertDate(ucStartDate.dateValue) + "' and '" + oMGF.ConvertDate(ucEndDate.dateValue) + "' ";
        }

        RetrieveData();
    }
    protected void RetrieveDataPrint()
    {
        oEnt.ErrorMesssage = "";
        oEnt.Entity_id = ddlEntity.SelectedValue.ToString();
        oEnt.MessangerName = txtSearchBy.Text;
        oEnt.StartDate = oMGF.ConvertDate(ucStartDate.dateValue);
        oEnt.EndDate = oMGF.ConvertDate(ucEndDate.dateValue);
        oDA.PrintMonitoringMessanger(oEnt);
        if(oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }

        String FileName = "rptINV_MonitoringMessanger.rdlc";
        rptViewer.LocalReport.ReportPath = ConfigurationManager.AppSettings["ReportPath"] + FileName;

        rds.Name = "dsMonitoring";
        rds.Value = oEnt.dtListData;
        rptViewer.LocalReport.DataSources.Add(rds);

        String StartDate = oMGF.FormatDate(oMGF.ConvertDate3(ucStartDate.dateValue));
        String EndDate = oMGF.FormatDate(oMGF.ConvertDate3(ucEndDate.dateValue));

        //Untuk Passing Parameter
        List<ReportParameter> parameters = new List<ReportParameter>();
        parameters.Add(new ReportParameter("Entity", this.ddlEntity.SelectedValue.ToString()));
        parameters.Add(new ReportParameter("MessangerName", txtSearchBy.Text));
        parameters.Add(new ReportParameter("StartDate", StartDate));
        parameters.Add(new ReportParameter("EndDate", EndDate));

        rptViewer.LocalReport.SetParameters(parameters);
        rptViewer.LocalReport.Refresh();

    }
    protected void ResetRecord()
    {
        Response.Redirect("ar_monitoringmessanger.aspx");
    }
    protected void btSearchRecord_Click(object sender, ImageClickEventArgs e)
    {
        pnlSearch.Visible = true;
        btPrintRecord.Visible = true;
    }
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchRecord();
    }
    protected void btPrintRecord_Click(object sender, ImageClickEventArgs e)
    {
        if(ddlSearchBy.SelectedValue == "")
        {
            mlMESSAGE.Text = "Sorry..you have to Select Messanger";
            return;
        }
        else
        {
            if(txtSearchBy.Text.Contains("%"))
            {
                mlMESSAGE.Text = "Finding Messanger Name doesn't using symbol % ";
                return;
            }
            else
            {
                pnlListData.Visible = false;
                pnlSearch.Visible = false;
                pnlPrintPreview.Visible = true;
                RetrieveDataPrint();
            }
        }
    }

    protected void imbBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ar_monitoringmessanger.aspx");
    }
    protected void btCancelOperation_Click(object sender, ImageClickEventArgs e)
    {
        ResetRecord(); 
    }
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchBy.Text = "";
        if(ddlSearchBy.SelectedIndex == 1)
        {
            tr_PERIODE.Visible = true;
            btPrintRecord.Visible = true;
        }
        else
        {
            tr_PERIODE.Visible = false;
            btPrintRecord.Visible = false;
        }
    }
}