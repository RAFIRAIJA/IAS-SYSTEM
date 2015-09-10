using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using IAS.Core.EncryptDecrypt;
using IAS.Core.CSCode;
using IAS.Initialize;
using IAS.APP.DataAccess.AD;

public partial class pj_ad_ad_changemenustyle : System.Web.UI.Page
{

    ModuleDBFunction oMDBF = new ModuleDBFunction();
    ModuleGeneralFunction oMGF = new ModuleGeneralFunction();
    ModuleGeneralSystem oMGS = new ModuleGeneralSystem();
    ModuleInitialization oMI = new ModuleInitialization();
    ModuleEncryptDecrypt oMEnc = new ModuleEncryptDecrypt();


    FunctionCore oFunc = new FunctionCore();
    VariableCore oVar = new VariableCore();

    VariableAD_SystemUser oEnt = new VariableAD_SystemUser();
    FunctionAD_SystemUser oDA = new FunctionAD_SystemUser();

    #region !--PagingVariable--!
    protected int mlPAGESIZE
    {
        get { return ((int)ViewState["mlPAGESIZE"]); }
        set { ViewState["mlPAGESIZE"] = value; }
    }
    protected int mlCURRENTPAGE
    {
        get { return ((int)ViewState["mlCURRENTPAGE"]); }
        set { ViewState["mlCURRENTPAGE"] = value; }
    }
    protected double mlTOTALPAGES
    {
        get { return ((double)ViewState["mlTOTALPAGES"]); }
        set { ViewState["mlTOTALPAGES"] = value; }
    }
    protected string mlCMDWHERE
    {
        get { return ((string)ViewState["mlCMDWHERE"]); }
        set { ViewState["mlCMDWHERE"] = value; }
    }
    protected string mlSORTBY
    {
        get { return ((string)ViewState["mlSORTBY"]); }
        set { ViewState["mlSORTBY"] = value; }
    }
    protected int mlTOTALRECORDS
    {
        get { return ((int)ViewState["mlTOTALRECORDS"]); }
        set { ViewState["mlTOTALRECORDS"] = value; }
    }
    #endregion

    protected String mlFLAGACTION
    {
        get { return ((String)ViewState["mlFLAGACTION"]); }
        set { ViewState["mlFLAGACTION"] = value; }
    }
    protected DataTable mlDT_LISTDATA
    {
        get { return ((DataTable)ViewState["mlDT_LISTDATA"]); }
        set { ViewState["mlDT_LISTDATA"] = value; }
    }
    protected DataTable mlDT_GROUPMENU
    {
        get { return ((DataTable)ViewState["mlDT_GROUPMENU"]); }
        set { ViewState["mlDT_GROUPMENU"] = value; }
    }
    protected DataTable mlDT_BRANCH
    {
        get { return ((DataTable)ViewState["mlDT_BRANCH"]); }
        set { ViewState["mlDT_BRANCH"] = value; }
    }
    protected String mlNIK
    {
        get { return ((String)ViewState["mlNIK"]); }
        set { ViewState["mlNIK"] = value; }
    }
    protected String mlMENUSTYLE
    {
        get { return ((String)ViewState["mlMENUSTYLE"]); }
        set { ViewState["mlMENUSTYLE"] = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"].ToString() + " CHANGE MENU STYLE V2.02";
        mlTITLE.Text = " CHANGE MENU STYLE V2.02";
        Session["mgDateTime"] = System.DateTime.Now;

        if (Session["mgUSERID"] == null)
        {
            Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=34FC35D4");
            return;
        }
        mlNIK = Request.QueryString["NIK"].ToString();
        mlMENUSTYLE = Request.QueryString["MENUSTYLE"].ToString();

        oEnt.mpCompanyID = "AD";
        oEnt.mpModule = "AD";

        if (!IsPostBack)
        {
            btSaveRecord.Attributes.Add("onClick", "javascript:WinClose()");
        }
    }
    
    protected void SaveRecord()
    {

        //oEnt.ErrorMesssage = "";
        //oEnt.NIK = mlNIK;
        //oEnt.MenuStyle = mlMENUSTYLE;
        //oDA.ChangeMenuStyle_Save(oEnt);
        //if(oEnt.ErrorMesssage != "")
        //{
        //    mlMESSAGE.Text = oEnt.ErrorMesssage;
        //    return;
        //}
        //Response.Redirect("ad_logout.aspx");
    }


    protected void btSaveRecord_Click(object sender, ImageClickEventArgs e)
    {
        SaveRecord();
    }
}