using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IAS.Core.CSCode;
using IAS.Initialize;
using IAS.APP.DataAccess.AD;

public partial class GeneralPage : System.Web.UI.Page
{
    ModuleDBFunction oMDBF= new ModuleDBFunction();
    ModuleGeneralFunction oMGF =new ModuleGeneralFunction();
    ModuleGeneralSystem oMGS = new ModuleGeneralSystem();
    ModuleInitialization oMI = new ModuleInitialization();

    FunctionCore oFunc = new FunctionCore();
    VariableCore oVar = new VariableCore();

    VariableAD oEnt = new VariableAD();
    FunctionAD oDA = new FunctionAD();

    String mlSQL = "";
    DataTable mlDTListData = new DataTable();

    public void MsgBox(String ex, Page pg, Object obj)
    {
        string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
        Type cstype = obj.GetType();
        ClientScriptManager cs = pg.ClientScript;
        cs.RegisterClientScriptBlock(cstype, s, s.ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {

            
        }
    }
    protected void MRLogin_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + "Hello" + "')", true);
        MRLogin();
    }

    protected void MRLogin()
    {
        DataTable mlDTListData = new DataTable();
        mlSQL = "SELECT * FROM AD_USERPROFILE WHERE UserID = '" + Session["mgUserID"].ToString()  + "' ";
        mlDTListData = oMDBF.OpenRecordSet("AD", "AD", mlSQL, CommandType.Text).Tables[0];

        if (mlDTListData.Rows.Count <= 0 )
        {
            MsgBox("Sorry, You don't have acces to this Application",this.Page, this);
            return;
        }

        Session["mgNAME"] = mlDTListData.Rows[0]["Name"].ToString();
        Session["mgGROUPMENU"] = mlDTListData.Rows[0]["GroupID"].ToString();
        Session["mgUSERMAIL"] = mlDTListData.Rows[0]["EmailAddr"].ToString();
        Session["mgUSERHP"] = mlDTListData.Rows[0]["TelHP"].ToString();
        Session["mgACTIVECOMPANY"] = mlDTListData.Rows[0]["LastCompany"].ToString();
        Session["mgACTIVESYSTEM"] = mlDTListData.Rows[0]["LastSystem"].ToString();
        Session["mgMENUSTYLE"] = mlDTListData.Rows[0]["MenuStyle"].ToString();
        Session["mgBRANCHID"] = mlDTListData.Rows[0]["BranchID"].ToString();

        Session["mgServerName"] = ModuleBaseSetting.DATASOURCE.ToString();
        Session["mgDBName"] = ModuleBaseSetting.DATABASE.ToString();
        Session["mgDBUserID"] = ModuleBaseSetting.SYSTEMUID.ToString();

        if (ModuleBaseSetting.SYSTEMPASSWORD.ToString() == "")
        {
            Session["mgPassword"] = "";
        }
        else
        {
            Session["mgPassword"] = ModuleBaseSetting.SYSTEMPASSWORD.ToString();
        }

        Session["mgDatabaseType"] = ModuleBaseSetting.DATABASETYPE.ToString();
        Session["mgDataSource"] = ModuleBaseSetting.DATASOURCE.ToString();
        Session["mgDataBase"] = ModuleBaseSetting.DATABASE.ToString();
        Session["mgSystemUID"] = ModuleBaseSetting.SYSTEMUID.ToString();
        Session["mgSystemPassword"] = ModuleBaseSetting.SYSTEMPASSWORD.ToString();
        Session["mgDatabaseDriver"] = ModuleBaseSetting.DATABASEDRIVER.ToString();
        Session["mgDateTime"] = DateTime.Now.ToString();

            //mlNUMBERLOGIN = IIf(mlOBJGF.IsNone(mlDTListData.Rows[0]["NumberLogin"].ToString()), 0, mlDTListData.Rows[0]["NumberLogin"].ToString()) + 1
            //mlOBJGS.CloseFile(mlREADER)

            //mlSQL = "UPDATE AD_USERPROFILE SET IsLogin = '1'," & vbCrLf
            //mlSQL += "NumberLogin = '" & mlNUMBERLOGIN & "', MenuStyle = 'TREE' " & vbCrLf
            //mlSQL += "WHERE UserID = '" & Session["mgUSERID") & "'" & vbCrLf
            //oMDBF.ExecRecordSet("AD", "AD", mlSQL, CommandType.Text)

            //oDA.XM_UserLog(Session["mgUSERID"), Session["mgNAME"), "ad_login", "Login Admin", mlIPADDRINFO)

            //mlMESSAGE.Text = "Page akan Otomatis di alihkan ... "

        switch(Session["mgMENUSTYLE"].ToString())
        {
            case "MENUSMENU":
                Response.Redirect("PJ_AD/ad_home.aspx");
                break;
            case "TREE":
                Response.Redirect("PJ_AD/ad_menuutama.aspx");
                break;
        }
        

    }
}