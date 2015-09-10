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
using IASClass;
using IAS.Core.CSCode;
using IAS.Initialize;
using ISS.App.Entities.pj_ad.administration;
using ISS.App.Entities.pj_ar;
using ISS.App.DataAccess.pj_ar;
using Microsoft.Reporting.WebForms;
using ISS.App.DataAccess;


public partial class pj_ar_report_ar_dynamicreport : System.Web.UI.Page
{
    ucmGeneralSystem mlOBJGS = new ucmGeneralSystem();
    ucmGeneralFunction mlOBJGF = new ucmGeneralFunction();
    FunctionLocal mlOBJPJ = new FunctionLocal();

    OleDbDataReader mlREADER;
    OleDbDataReader mlREADER2;

    GeneralSetting GS = new GeneralSetting();
    EntitiesMenu oEntMenu = new EntitiesMenu();
    EntitiesInvoiceDelivery oEnt = new EntitiesInvoiceDelivery();
    DataAccessInvoiceDelivery oDA = new DataAccessInvoiceDelivery();

    ModuleGeneralFunction oMGF = new ModuleGeneralFunction();
    ReportDataSource ds = new ReportDataSource();
    reportgenerator rg = new reportgenerator();

    protected String WhereCond
    {
        get { return ((String)ViewState["WhereCond"]); }
        set { ViewState["WhereCond"] = value; }
    }
    protected String Sitecard
    {
        get { return ((String)ViewState["Sitecard"]); }
        set { ViewState["Sitecard"] = value; }
    }
    protected String StartDate
    {
        get { return ((String)ViewState["StartDate"]); }
        set { ViewState["StartDate"] = value; }
    }
    protected String EndDate
    {
        get { return ((String)ViewState["EndDate"]); }
        set { ViewState["EndDate"] = value; }
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

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"].ToString() + " MONITORING INVOICE DELIVERY BY MESSANGER V2.02";
        //Session["mgDateTime"] = System.DateTime.Now;

        //if (Session["mgUSERID"] == null)
        //{
        //    Response.Redirect("~/pageconfirmation.aspx?mpMESSAGE=34FC35D4");
        //    return;
        //}

        oEnt.CompanyID = "ISSP3";
        oEnt.ModuleID = "PB";
        if (!IsPostBack)
        {
            runRptViewer();
        }
    }
    private DataTable getData()
    {
        DataSet dss = new DataSet();

        oDA.PrintDynamicColumns(oEnt);
        DataTable dt = oEnt.dtListData;
        return dt;
    }

    private void runRptViewer()
    {
        //FileName = "rptINV_DynamicColumn.rdlc";
        //this.rptViewer.Reset();
        //rptViewer.LocalReport.ReportPath = ConfigurationManager.AppSettings["ReportPath"] + FileName;
        //ReportDataSource rds = new ReportDataSource("dsDynamic", getData());
        //this.rptViewer.LocalReport.DataSources.Clear();
        //this.rptViewer.LocalReport.DataSources.Add(rds);
        //this.rptViewer.DataBind();
        //this.rptViewer.LocalReport.Refresh();

        DataTable dt = new DataTable();
        dt = getData();
        String dsName = "dsDynamic";
        rg.ReportGeneratorVar(dt, dsName);
        ReportDataSource ds = new ReportDataSource(dsName, dt);
        rptViewer.Reset();
        rptViewer.LocalReport.DataSources.Add(ds);
        rptViewer.LocalReport.DisplayName = dsName;
        rptViewer.LocalReport.LoadReportDefinition(rg.GenerateReport());

    }
}