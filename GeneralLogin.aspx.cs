using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using ws_ias;
using IASClass;
public partial class GeneralLogin : System.Web.UI.Page
{
    ws_general WS = new ws_general();
    ucmGeneralSystem mlOBJGS = new ucmGeneralSystem();
    ucmGeneralFunction mlOBJGF = new ucmGeneralFunction();


    ModuleFunctionLocal mlOBJPJ = new ModuleFunctionLocal();
    ModuleGeneralFunction mlOBJMGF = new ModuleGeneralFunction();
    ModuleDBFunction mlOBJDBF = new ModuleDBFunction();

    String Err_Message = "";
    String mlENCRYPTCODE = "";
    String Password = "";
    String CrLf = Environment.NewLine;


    protected void Page_Load(object sender, EventArgs e)
    {
        mlENCRYPTCODE = System.Configuration.ConfigurationManager.AppSettings["mgENCRYPTCODE"];

        if (!IsPostBack)
        {
            UpdatePassword();
        }
    }
    protected void UpdatePassword()
    {
        DataTable dtDATALIST = new DataTable();
        String Ssql = "";
        Ssql = "Select replace(convert(varchar(10),DOB,104),'.','') as TglLahir,userID as NIK" + CrLf;
        Ssql += "From ADM_ISS.dbo.AD_USERLOGIN" + CrLf;
        dtDATALIST = mlOBJDBF.OpenRecordSet("AD", "AD", Ssql, CommandType.Text).Tables[0];
        if (dtDATALIST.Rows.Count > 0)
        { 
            for (int i=0;i<dtDATALIST.Rows.Count;i++)
            {
                Password = mlOBJGF.Encrypt(dtDATALIST.Rows[i]["TglLahir"].ToString(), mlENCRYPTCODE);
                Ssql = "Update ADM_ISS.dbo.AD_USERLOGIN " + CrLf;
                Ssql += "Set Password = '" + Password + "'" + CrLf;
                Ssql += "Where UserID = '" + dtDATALIST.Rows[i]["NIK"].ToString() + "'" + CrLf;
                mlOBJDBF.ExecRecordSet("AD", "AD", Ssql, CommandType.Text);
            }
        }


    }
    protected void BtnCancelLogin_Click(object Source, EventArgs e)
    {
        Response.Redirect("generalLogin.aspx");
    }
    protected void BtnLogin_Click(object Source, EventArgs e)
    {
        Session["mgUserID"] = inUserID.Value;
        if (LoginValid())
        {
            Response.Redirect("generalPage.aspx");
        }
        else
        {
            Err_Message = Err_Message + " ";
            MsgBox(Err_Message , this.Page, this);
        }
        
    }
    protected Boolean LoginValid()
    {
        String sJson = "";
        try
        {
            //sJson = WS.GetDataEmployee(inUserID.Value, inPassword.Value);
            sJson = WS.GetDataEmployee2(inUserID.Value, inPassword.Value);

            Session["mgUserID"] = inUserID.Value;
            return true;
        }
        catch(Exception ex)
        {
            Err_Message = ex.Message;
            return true ;
        }

    }
    public void MsgBox(String ex, Page pg, Object obj)
    {
        string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
        Type cstype = obj.GetType();
        ClientScriptManager cs = pg.ClientScript;
        cs.RegisterClientScriptBlock(cstype, s, s.ToString());
    }

        
}