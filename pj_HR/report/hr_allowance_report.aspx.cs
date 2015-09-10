using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data.OleDb;
//using System.Windows.Forms;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System.IO;
using System.Net;

using IAS.Core.CSCode;
using IAS.APP.DataAccess.HR;
using IASClass;
using IAS.Initialize;

public partial class pj_hr_report_hr_allowance_report : System.Web.UI.Page
{
    ucmGeneralSystem mlOBJGS = new ucmGeneralSystem();
    ucmGeneralFunction mlOBJGF = new ucmGeneralFunction();
    ReportDataSource ds = new ReportDataSource();

    OleDbDataReader mlREADER;
    OleDbDataReader mlREADER2;

    ModuleDBFunction oMDBF = new ModuleDBFunction();
    ModuleGeneralFunction oMGF = new ModuleGeneralFunction();
    ModuleGeneralSystem oMGS = new ModuleGeneralSystem();
    ModuleInitialization oMI = new ModuleInitialization();

    VariableHR oEnt = new VariableHR();
    FunctionHR oDA = new FunctionHR();


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

    protected String FileName
    {
        get { return ((String)ViewState["FileName"]); }
        set { ViewState["FileName"] = value; }
    }
    protected String rdsName
    {
        get { return ((String)ViewState["rdsName"]); }
        set { ViewState["rdsName"] = value; }
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

    protected void Page_Load(object sender, EventArgs e)
    {
        oMI.Main();

        if (oMGS.ValidateExpiredDate())
        {
            return;
        }
        if(Session["mgACTIVECOMPANY"] == "")
        {
            Session["mgACTIVECOMPANY"] = ModuleBaseSetting.COMPANYDEFAULT;
        }

        LoginID = oMDBF.Loginid;

        if (!IsPostBack)
        {
            DateTime ToDay = DateTime.Now;
            ucStartDate.dateValue = String.Format("{0:01/MM/yyyy}", ToDay.Date);
            ucEndDate.dateValue = String.Format("{0:dd/MM/yyyy}", ToDay);

            FileName = "HRAllowance.rdlc";
            rdsName = "dsHRAllowance";            

        }

    }
    protected void btnPreview_Click(object sender, ImageClickEventArgs e)
    {
        String SiteCard = txtSiteCard.Text;
        String StartDate = oMGF.ConvertDate(ucStartDate.dateValue);
        String EndDate = oMGF.ConvertDate(ucEndDate.dateValue);
        Response.Redirect("hr_allowance_viewer.aspx?WhereCond=" + cmdWhere + "&sitecard=" + SiteCard + "&StartDate=" + StartDate + "&EndDate=" + EndDate);
        //pnlSearch.Visible = false;
        //pnlDataList.Visible = false;
        //pnlReport.Visible = true;
      
        //PreviewReport();
    }
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        GetWhereCond();
    }
    protected void GetWhereCond()
    {
        cmdWhere = "";
        if (txtSiteCard.Text != "")
        {
            if (txtSiteCard.Text.Contains("%"))
            {
                cmdWhere += " and ah.sitecard like '%" + txtSiteCard.Text + "%' ";
            }
            else
            {
                cmdWhere += " and ah.sitecard = '" + txtSiteCard.Text+"' " ;
            }
        }
        //if (txtNIK.Text != "")
        //{
        //    if (txtNIK.Text.Contains("%"))
        //    {
        //        cmdWhere += " and ad.NIK like %'" + txtNIK.Text + "%' ";
        //    }
        //    else
        //    {
        //        cmdWhere += " and ad.NIK = " + txtNIK.Text ;
        //    }
        //}
        cmdWhere += " and ah.transaction_date between '" + oMGF.ConvertDate(ucStartDate.dateValue) + "' and '" + oMGF.ConvertDate(ucEndDate.dateValue)+"' " ;

        BindGrid();
    }
    protected void BindGrid()
    {
        //oEnt.StrConnection = GetConnectionString();
        oEnt.CompanyID = "ISSP3";
        oEnt.ModuleID = "PB";
        oEnt.ErrorMesssage = "";
        oEnt.WhereCond = cmdWhere;
        oDA.GetDataReportAllowance(oEnt);
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
        btnPreview.Visible = true;
    }
    protected void dgListData_SortCommand(object source, DataGridSortCommandEventArgs e)
    {

    }
    protected void dgListData_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
        {
            
        }
    }
    protected void dgListData_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
        {
            

        }
    }
    protected void imbRefresh_Click(object sender, ImageClickEventArgs e)
    {
        mlMESSAGE.Text = "";
        lblSitecard.Text = oMGS.ISS_XMGeneralLostFocus("SITECARD_DESC", txtSiteCard.Text.Trim(),"");
        btnPreview.Visible = false;
        oEnt.dtListData = new DataTable();
        dgListData.DataSource = oEnt.dtListData;
        dgListData.DataBind();
    }
    protected void PreviewReport()
    {
        ReportViewer1.LocalReport.ReportPath = ConfigurationManager.AppSettings["ReportPath"] + FileName;

       // oEnt.StrConnection = GetConnectionString();
        oEnt.CompanyID = "ISSP3";
        oEnt.ModuleID = "PB";
        oEnt.ErrorMesssage = "";
        oEnt.WhereCond = cmdWhere;
        oEnt.Sitecard = txtSiteCard.Text;
        oEnt.AllowanceStartdate = oMGF.ConvertDate2(ucStartDate.dateValue);
        oEnt.AllowanceEnddate = oMGF.ConvertDate2(ucEndDate.dateValue);
        oDA.PreviewReportAllowance(oEnt);
        if(oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }

        //SqlConnection oConn = new SqlConnection(oEnt.StrConnection);
        //SqlTransaction Trans;
        //SqlParameter[] Params = new SqlParameter[3];
        //if (oConn.State == ConnectionState.Closed)
        //{
        //    oConn.Open();
        //}
        //Trans = oConn.BeginTransaction();
        //try
        //{
        //    oEnt.SPName = "PROD_ISS_NAV.dbo.spRpt_HRAllowance";
        //    Params[0] = new SqlParameter("@Sitecard", SqlDbType.VarChar, 30);
        //    Params[0].Value = txtSiteCard.Text;
        //    Params[1] = new SqlParameter("@StartDate", SqlDbType.VarChar, 30);
        //    Params[1].Value = ucStartDate.dateValue;
        //    Params[2] = new SqlParameter("@EndDate", SqlDbType.VarChar, 30);
        //    Params[2].Value = ucEndDate.dateValue;
        //    oEnt.dtListData = SqlHelper.ExecuteDataset(Trans, CommandType.StoredProcedure, oEnt.SPName, Params).Tables[0];
        //    Trans.Commit();
        //}
        //catch (Exception ex)
        //{
        //    Trans.Rollback();
        //}

        ds.Name = rdsName;
        ds.Value = oEnt.dtListData;
        ReportViewer1.LocalReport.DataSources.Add(ds);

        String StartDate = oMGF.ConvertDate(ucStartDate.dateValue);
        String EndDate = oMGF.ConvertDate(ucEndDate.dateValue);

        //Untuk Passing Parameter
        List<ReportParameter> parameters = new List<ReportParameter>();
        parameters.Add(new ReportParameter("SiteCard", txtSiteCard.Text));
        parameters.Add(new ReportParameter("StartDate", StartDate));
        parameters.Add(new ReportParameter("EndDate", EndDate));

        ReportViewer1.LocalReport.SetParameters(parameters);

        ReportViewer1.LocalReport.Refresh();
    }

    protected void imbBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("hr_allowance_report.aspx");
    }
}
