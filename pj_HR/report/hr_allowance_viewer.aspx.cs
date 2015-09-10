using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Net;
using System.Collections.Generic;

using IAS.Core.CSCode;
using IAS.APP.DataAccess.HR;


public partial class pj_hr_report_hr_allowance_viewer : System.Web.UI.Page
{
    ReportDataSource ds = new ReportDataSource();

    ModuleDBFunction oMDBF = new ModuleDBFunction();
    ModuleGeneralFunction oMGF = new ModuleGeneralFunction();
    ModuleGeneralSystem oMGS = new ModuleGeneralSystem();
    ModuleInitialization oMI = new ModuleInitialization();

    VariableHR oEnt = new VariableHR();
    FunctionHR oDA = new FunctionHR();

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
        if(!IsPostBack)
        {
            if(Request.QueryString["WhereCond"].ToString() != "")
            {
                WhereCond = Request.QueryString["WhereCond"].ToString();
            }
            if (Request.QueryString["SiteCard"].ToString() != "")
            {
                Sitecard = Request.QueryString["SiteCard"].ToString();
            }
            if (Request.QueryString["StartDate"].ToString() != "")
            {
                StartDate = Request.QueryString["StartDate"].ToString();
            }
            if (Request.QueryString["EndDate"].ToString() != "")
            {
                EndDate = Request.QueryString["EndDate"].ToString();
            }
            
            FileName = "HRAllowance.rdlc";
            rdsName = "dsHRAllowance";
            PreviewReport();
        }
    }
    protected void PreviewReport()
    {
        ReportViewer1.LocalReport.ReportPath =  ConfigurationManager.AppSettings["ReportPath"] + FileName;

        oEnt.CompanyID = "ISSP3";
        oEnt.ModuleID = "PB";
        oEnt.ErrorMesssage = "";
        OleDbParameter[] Params = new OleDbParameter[3];
        try
        {
            oEnt.SPName = "spRpt_HRAllowance";
            Params[0] = new OleDbParameter("@Sitecard", OleDbType.VarChar, 30);
            Params[0].Value = Sitecard;
            Params[1] = new OleDbParameter("@StartDate", OleDbType.VarChar, 30);
            Params[1].Value = StartDate;
            Params[2] = new OleDbParameter("@EndDate", OleDbType.VarChar, 30);
            Params[2].Value = EndDate;
            oEnt.dtListData = oMDBF.OpenRecordSet(oEnt.CompanyID, oEnt.ModuleID, oEnt.SPName, CommandType.StoredProcedure, Params).Tables[0];
        }
        catch (Exception ex)
        {
            
        }
            
        ds.Name = rdsName;
        ds.Value = oEnt.dtListData;
        ReportViewer1.LocalReport.DataSources.Add(ds);

        this.StartDate = oMGF.ConvertDate(StartDate);
        this.EndDate = oMGF.ConvertDate(EndDate);        

        //Untuk Passing Parameter
        List<ReportParameter> parameters = new List<ReportParameter>();
        parameters.Add(new ReportParameter("SiteCard", this.Sitecard));
        parameters.Add(new ReportParameter("StartDate", this.StartDate));
        parameters.Add(new ReportParameter("EndDate", this.EndDate));

        ReportViewer1.LocalReport.SetParameters(parameters);

        ReportViewer1.LocalReport.Refresh();
    }
    protected void imbBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("hr_allowance_report.aspx");
    }
}
