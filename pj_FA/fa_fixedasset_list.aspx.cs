using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Drawing;
using System.Configuration;
using Microsoft.VisualBasic;

public partial class backoffice_fa_fixedasset_list : System.Web.UI.Page
{    
   
    IASClass.ucmGeneralSystem mlOBJGS = new IASClass.ucmGeneralSystem();
    IASClass.ucmGeneralFunction mlOBJGF = new IASClass.ucmGeneralFunction();
    FunctionLocal mlOBJPJ = new FunctionLocal();
    FunctionOther mlOBJPO = new FunctionOther();
    FunctionCS mlOBJPJCS = new FunctionCS();   
    


    OleDbDataReader mlREADER;
    string mlSQL;
    OleDbDataReader mlRSTEMP;
    string mlSQLTEMP;
    OleDbDataAdapter mlDATAADAPTER;
    DataSet mlDATASET;

    string mlKEY;
    string mlFUNCTIONPARAMETERORI;
    string mlFUNCTIONPARAMETER;
    string mlRECORDSTATUS;
    Int32  mlI;
    string mlSTOCKISTGROUP;
    String mlCURRENTDATE = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString();



    protected void Page_Load(object sender, EventArgs e)
    {
        mlTITLE.Text = "Fixed Asset List V2.01";
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"] + "Fixed Asset List V2.00";
        mlSTOCKISTGROUP = System.Configuration.ConfigurationManager.AppSettings["mgSTOCKISTGROUPMENU"];
     

        string mla;
        bool mlB;
        Int32 mlC;


        mla = mlOBJPJCS.aas();
        Context.Response.Write("<br>" + mla);

        mla = mlOBJGF.TestCS_string("1") ;        
        Context.Response.Write("<br>" + mla);

        mlC = mlOBJGF.TestCS2_integer(1);
        Context.Response.Write("<br>" + mlC);

        //mla = mlOBJPO.aasvb;
        //Context.Response.Write("<br>" + mla);

        mla = aas2();
        Context.Response.Write("<br>" + mla);


        mla = " ad   " + "dsf";
        if (IsNone2(mla) == false)
        {
            Context.Response.Write("<br>" + mla);
        }        

    //    switch (mlFUNCTIONPARAMETER) {        
    //        case "S":
    //            mpLOCID.Text = Session("mgUSERID");
    //            mlOBJPJ.SetTextBox(True, mpLOCID);
    //            btLOCID_Click(Nothing, Nothing);
    //    }

    //    switch (UCase(Trim(Session("mgGROUPMENU")))) {
    //        case mlSTOCKISTGROUP:
    //            mpLOCID.Text = Session("mgUSERID");
    //            mlOBJPJ.SetTextBox(True, mpLOCID);
    //            btLOCID_Click(Nothing, Nothing);
    //    }


    }

    public bool IsNone2(string mpTEMP)
    {
        if (mpTEMP == null)
        {
            return true;
        }

        else if (string.IsNullOrEmpty(mpTEMP))
        {
            return true;
        }
        else if (mpTEMP == "")
        {
            return true;
        }
        return false;
    }

    public string aas2()
    {
        return "bbb";
    }
}
