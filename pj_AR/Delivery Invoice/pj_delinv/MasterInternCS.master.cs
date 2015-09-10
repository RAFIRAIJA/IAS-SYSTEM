using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class MasterInternCS_00 : System.Web.UI.MasterPage
{
    IASClass.ucmGeneralSystem mlOBJGS = new IASClass.ucmGeneralSystem();

    IASClass.ucmGeneralFunction mlOBJGF = new IASClass.ucmGeneralFunction();
    public System.Data.OleDb.OleDbDataReader mlREADERM;
    string mlSQLM;
    string mlUSERID;

    string mlGROUPMENU;
    string mlVALIDATE;
    string mlVALIDATE2;

    string mlSPECIALPARAM;
    public string mlMENUCODE;
    public string mlMENUCODETEMP;
    public string mlMENUID;
    public byte mlMENUINT;
    public bool mlFIRST;
    public bool mlCLOSETAG;
    public bool mlMENUFILL;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            mlOBJGS.Main();
            if (string.IsNullOrEmpty(Convert.ToString(Session["mgACTIVECOMPANY"])))
                Session["mgACTIVECOMPANY"] = mlOBJGS.mgCOMPANYDEFAULT;
            mlOBJGS.mgACTIVECOMPANY = Convert.ToString(Session["mgACTIVECOMPANY"]);

            mlUSERID = Convert.ToString(Session["mgUSERID"]);
            mlGROUPMENU = Convert.ToString(Session["mgGROUPMENU"]);

            //AuthorizeMenu()
            RetrieveFieldsDetail();
            lbTEXT.Text = "<font size=1px>Login ID : " + Session["mgUSERID"] + "- " + Session["mgNAME"] + ", Current Time : " + DateTime.Now + " </font>";

            //If mlUSERID <> "" Then
            //    If Not Page.IsPostBack Then
            //        RetrieveFieldsDetail()
            //    End If
            //End If

        }
        catch
        {
        }
    }

    public void RetrieveFieldsDetail()
    {
        try
        {
            mlSQLM = " SELECT '01' AS ColInd, 'Admin' AS Description,'' AS ExecFile,'' AS SpecialParameter UNION ALL " + 
                     " SELECT '02' AS ColInd, Description, ExecFile, SpecialParameter FROM AD_MENU WHERE " + 
                     " MenuID IN (SELECT MenuID FROM AD_GROUPMENU WHERE GroupMenu = '" + mlGROUPMENU + "')" + 
                     " AND ExecFile <> '' AND (SysID='IA') AND MenuTransType LIKE 'AD%' " + 
                     "" + 
                     " UNION ALL SELECT '03' AS ColInd,'^','','' " + 
                     " UNION ALL  SELECT '04'AS ColInd,'Master Data','','' UNION ALL " + 
                     " SELECT '05',Description, ExecFile, SpecialParameter FROM AD_MENU WHERE " + 
                     " MenuID IN (SELECT MenuID FROM AD_GROUPMENU WHERE GroupMenu = '" + mlGROUPMENU + "')" + 
                     " AND ExecFile <> '' AND (SysID='IA') AND MenuTransType LIKE 'MS%' " + 
                     "" + 
                     " UNION ALL SELECT '06' AS ColInd,'^','','' " + 
                     " UNION ALL  SELECT '07' AS ColInd,'Transaction','','' UNION ALL " + 
                     " SELECT '08', Description, ExecFile, SpecialParameter FROM AD_MENU WHERE " + 
                     " MenuID IN (SELECT MenuID FROM AD_GROUPMENU WHERE GroupMenu = '" + mlGROUPMENU + "')" + 
                     " AND ExecFile <> '' AND (SysID='IA') AND MenuTransType LIKE 'TR%' " + 
                     "" + 
                     " UNION ALL SELECT '09' AS ColInd,'^','','' " + 
                     " UNION ALL  SELECT '10' AS ColInd,'Posting','','' UNION ALL " + 
                     " SELECT '11',Description, ExecFile, SpecialParameter FROM AD_MENU WHERE " + 
                     " MenuID IN (SELECT MenuID FROM AD_GROUPMENU WHERE GroupMenu = '" + mlGROUPMENU + "')" + 
                     " AND ExecFile <> '' AND (SysID='IA') AND MenuTransType LIKE 'PS%' " + 
                     "" + 
                     " UNION ALL SELECT '12' AS ColInd,'^','','' " + 
                     " UNION ALL  SELECT '13' AS ColInd,'Report','','' UNION ALL " + 
                     " SELECT '14',Description, ExecFile, SpecialParameter FROM AD_MENU WHERE " + 
                     " MenuID IN (SELECT MenuID FROM AD_GROUPMENU WHERE GroupMenu = '" + mlGROUPMENU + "')" + 
                     " AND ExecFile <> '' AND (SysID='IA') AND MenuTransType LIKE 'RP%' " + 
                     "" + 
                     " UNION ALL SELECT '15' AS ColInd,'^','','' " + 
                     " UNION ALL  SELECT '16' AS ColInd,'Utility','','' UNION ALL " + 
                     " SELECT '17',Description, ExecFile, SpecialParameter FROM AD_MENU WHERE " + 
                     " MenuID IN (SELECT MenuID FROM AD_GROUPMENU WHERE GroupMenu = '" + mlGROUPMENU + "')" + 
                     " AND ExecFile <> '' AND (SysID='IA') AND MenuTransType LIKE 'UT%' " +
                     "" +
                     " UNION ALL SELECT '18' AS ColInd,'^','','' " +
                     " UNION ALL  SELECT '19' AS ColInd,'Monitoring Delivery','','' UNION ALL " +
                     " SELECT '20',Description, ExecFile, SpecialParameter FROM AD_MENU WHERE " +
                     " MenuID IN (SELECT MenuID FROM AD_GROUPMENU WHERE GroupMenu = '" + mlGROUPMENU + "')" +
                     " AND ExecFile <> '' AND (SysID='IA') AND MenuTransType LIKE 'MD%' " +
                     "" + 
                     " UNION ALL SELECT '21' AS ColInd,'^','','' " + 
                     " UNION ALL  SELECT '22' AS ColInd,'','','' UNION ALL " + 
                     " SELECT '23',Description, ExecFile, SpecialParameter FROM AD_MENU WHERE " + 
                     " MenuID IN (SELECT MenuID FROM AD_GROUPMENU WHERE GroupMenu = '" + mlGROUPMENU + "')" + 
                     " AND ExecFile <> '' AND (SysID='IA') AND MenuTransType LIKE 'BN%' " + 
                     "" + 
                     " ORDER BY ColInd,Description" + 
                     "";
            mlREADERM = mlOBJGS.DbRecordset(mlSQLM, "AD", "AD");

        }
        catch
        {
        }
    }

    public void AuthorizeMenu()
    {
        string mlSQLVAL = null;
        OleDbDataReader mlRSVALIDATE = default(OleDbDataReader);

        try
        {
            if (string.IsNullOrEmpty(mlUSERID))
            {
                mlVALIDATE = "31";
                mlVALIDATE = mlOBJGF.Encrypt(mlVALIDATE);
                Response.Write("Empty UserID ");
                Response.Write("<br>");
                mlOBJGS.XMtoLog("1", "AD", "Empty UserID", "Empty UserID " + Request.Url.AbsoluteUri, "New", mlUSERID, Convert.ToDateTime(mlOBJGF.FormatDate(DateTime.Now)));
                Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=" + mlVALIDATE);
            }
            else if (string.IsNullOrEmpty(mlGROUPMENU))
            {
                mlVALIDATE = "31";
                mlVALIDATE = mlOBJGF.Encrypt(mlVALIDATE);
                Response.Write("Empty GroupMenu");
                Response.Write("<br>");
                mlOBJGS.XMtoLog("1", "AD", "Empty GroupMenu", "Empty GroupMenu " + Request.Url.AbsoluteUri, "New", mlUSERID, Convert.ToDateTime(mlOBJGF.FormatDate(DateTime.Now)));
                Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=" + mlVALIDATE);
            }

            mlVALIDATE = Path.GetFileName(Request.Path);
            mlVALIDATE2 = Request.Url.AbsoluteUri;
            if (mlOBJGF.IsNone(mlVALIDATE2) == false)
            {
                mlVALIDATE2 = "?" + mlOBJGF.GetStringAtPosition(mlVALIDATE2, 1, "?");
            }

            mlSQLVAL = "SELECT GM.GroupMenu, MN.MenuID, MN.Description, MN.ExecFile, MN.SpecialParameter FROM AD_MENU MN, AD_GROUPMENU GM " + " WHERE MN.MenuID = GM.MenuID AND GM.GroupMenu ='" + mlGROUPMENU + "' AND MN.EXECFILE LIKE '%" + mlVALIDATE + "%'";

            if (mlVALIDATE2.IndexOf("FP") != 0)
            {
                mlSQLVAL = "SELECT GM.GroupMenu, MN.MenuID, MN.Description, MN.ExecFile, MN.SpecialParameter FROM AD_MENU MN, AD_GROUPMENU GM " + " WHERE MN.MenuID = GM.MenuID AND GM.GroupMenu ='" + mlGROUPMENU + "' AND MN.EXECFILE LIKE '%" + mlVALIDATE + "%'" + " AND SpecialParameter LIKE '%" + mlVALIDATE2 + "%'";
            }
            mlRSVALIDATE = mlOBJGS.DbRecordset(mlSQLVAL, "AD", "AD");
            if (mlRSVALIDATE.HasRows == false)
            {
                mlVALIDATE = "32";
                Response.Write("<br>" + mlVALIDATE + "<br>" + mlVALIDATE2);
                mlVALIDATE = mlOBJGF.Encrypt(mlVALIDATE);
                Response.Write("NotAuthorize1");
                Response.Write("<br>");
                mlOBJGS.XMtoLog("1", "AD", "MenuAuthorize", "Not Authorize " + Request.Url.AbsoluteUri, "New", mlUSERID, Convert.ToDateTime(mlOBJGF.FormatDate(DateTime.Now)));
                Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=" + mlVALIDATE);
            }
            else
            {
                if (mlVALIDATE2.IndexOf("FP") != 0)
                {
                    if (mlRSVALIDATE.HasRows == true)
                    {
                        mlRSVALIDATE.Read();
                        mlSPECIALPARAM = mlRSVALIDATE["SpecialParameter"].ToString() + "";
                        if (mlVALIDATE2.IndexOf(mlSPECIALPARAM) == 0)
                        {
                            mlVALIDATE = "32";
                            Response.Write("<br>" + mlVALIDATE + "<br>" + mlVALIDATE2);
                            mlVALIDATE = mlOBJGF.Encrypt(mlVALIDATE);
                            Response.Write("NotAuthorize2");
                            Response.Write("<br>");
                            mlOBJGS.XMtoLog("1", "AD", "MenuAuthorize", "Not Authorize " + Request.Url.AbsoluteUri, "New", mlUSERID, Convert.ToDateTime(mlOBJGF.FormatDate(DateTime.Now)));
                            Response.Write(mlVALIDATE2 + "<br>" + mlSPECIALPARAM);
                            Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=" + mlVALIDATE);
                        }
                    }
                }
            }


        }
        catch
        {
        }
    }

}
