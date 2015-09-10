using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using System.Data.OleDb;
using IAS.Core.CSCode;
using IAS.Initialize;
using IAS.APP.DataAccess.AD;


public partial class pj_AD_ad_menustyle : System.Web.UI.Page
{

    ModuleDBFunction oMDBF = new ModuleDBFunction();
    ModuleGeneralFunction oMGF = new ModuleGeneralFunction();
    ModuleGeneralSystem oMGS = new ModuleGeneralSystem();
    ModuleInitialization oMI = new ModuleInitialization();
    ModuleFunctionLocal mlOBJPJ = new ModuleFunctionLocal();

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
    protected DataTable mlDT_LISTDETAIL
    {
        get { return ((DataTable)ViewState["mlDT_LISTDETAIL"]); }
        set { ViewState["mlDT_LISTDETAIL"] = value; }
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

    protected void Page_PreInit(object sender, EventArgs e)
    {
        CekSession();
        mlMENUSTYLE = mlOBJPJ.AD_CHECKMENUSTYLE(Session["mgMENUSTYLE"].ToString(), this.MasterPageFile);
        this.MasterPageFile = mlMENUSTYLE;
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

        oEnt.mpCompanyID = "AD";
        oEnt.mpModule = "AD";
        mlNIK = Session["mgUSERID"].ToString();
        mlMENUSTYLE = Session["mgMENUSTYLE"].ToString();
        
        if (!IsPostBack)
        {
            getDataDDL(ddlMenuStyle, mlMENUSTYLE, "");
        }
        //Response.Flush();
        
    }
    protected void CekSession()
    {
        if (Session["mgMENUSTYLE"] == null)
        {
            Response.Redirect("~/pageconfirmation.aspx?mpMESSAGE=34FC35D4");
            return;
        }
    }
    protected void getDataDDL(DropDownList ddl, String ID, String Teks)
    {

        for (int i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Value == ID)
            {
                ddl.SelectedIndex = i;
                break;
            }
            if (ddl.Items[i].Text == Teks)
            {
                ddl.SelectedIndex = i;
                break;
            }
        }
    }
    protected void SaveRecord()
    {

        oEnt.ErrorMesssage = "";
        oEnt.NIK = mlNIK;
        oEnt.MenuStyle = ddlMenuStyle.SelectedValue;
        oDA.ChangeMenuStyle_Save(oEnt);
        if (oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }
    }   
    protected void btSaveRecord_Click(object sender, ImageClickEventArgs e)
    {
        SaveRecord();
        btSaveRecord.Visible = false;

    }
}