using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using IAS.Core.CSCode;
using IAS.Initialize;
using oNAVDemo;

public partial class pj_WS_ws_testingNavision : System.Web.UI.Page
{
    
    VariableCore mlVarCore = new VariableCore();

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
    protected void Page_Load(object sender, EventArgs e)
    {
        oNAVDemo.NAV nav = new NAV(new Uri("http://wcf-uat.nav.issworld.com:8058/NAV71_UATWS/OData/Company('ID_007%20IFS%20Test%20140215')"));  

        nav.Credentials = CredentialCache.DefaultNetworkCredentials;
        var customer = from c in nav.Customer select c;

    }
    protected void btNewRecord_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btSaveRecord_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btSearchRecord_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btCancelOperation_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {

    }
}