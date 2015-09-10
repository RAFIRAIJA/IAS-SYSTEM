using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;     
using IAS.Core.CSCode;
using IAS.Initialize;
using IASClass;  
public partial class pj_FA_fa_moving_entry : System.Web.UI.Page
{

    ucmGeneralSystem mlOBJGS = new ucmGeneralSystem();
    ucmGeneralFunction mlOBJGF = new ucmGeneralFunction();

    ModuleFunctionLocal mlOBJPJ = new ModuleFunctionLocal();

    OleDbDataReader mlREADER ;
    OleDbDataReader mlREADER2 ;
    OleDbDataReader mlRSTEMP ;

    String mlCURRENTDATE = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString();
    String mlCURRENTBVMONTH = DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString();   
    
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

    protected string FlagAction
    {
        get { return ((string)ViewState["FlagAction"]); }
        set { ViewState["FlagAction"] = value; }
    }
    protected string UserID
    {
        get { return ((string)ViewState["UserID"]); }
        set { ViewState["UserID"] = value; }
    }
    protected string mlMENUSTYLE
    {
        get { return ((String)ViewState["mlMENUSTYLE"]); }
        set { ViewState["mlMENUSTYLE"] = value; }
    }
    protected string mlFUNCTIONPARAMETER
    {
        get { return ((string)ViewState["mlFUNCTIONPARAMETER"]); }
        set { ViewState["mlFUNCTIONPARAMETER"] = value; }
    }
    protected string mlPARAM_COMPANY
    {
        get { return ((string)ViewState["mlPARAM_COMPANY"]); }
        set { ViewState["mlPARAM_COMPANY"] = value; }
    }
    protected string mlCOMPANYID
    {
        get { return ((string)ViewState["mlCOMPANYID"]); }
        set { ViewState["mlCOMPANYID"] = value; }
    }
    protected string mlCOMPANYTABLENAME
    {
        get { return ((string)ViewState["mlCOMPANYTABLENAME"]); }
        set { ViewState["mlCOMPANYTABLENAME"] = value; }
    }    
    protected string mlUSERLEVEL
    {
        get { return ((string)ViewState["mlUSERLEVEL"]); }
        set { ViewState["mlUSERLEVEL"] = value; }
    }
    protected string mlSQL
    {
        get { return ((string)ViewState["mlSQL"]); }
        set { ViewState["mlSQL"] = value; }
    }
    protected string mlSQLTEMP
    {
        get { return ((string)ViewState["mlSQLTEMP"]); }
        set { ViewState["mlSQLTEMP"] = value; }
    }
    protected string mlKEY
    {
        get { return ((string)ViewState["mlKEY"]); }
        set { ViewState["mlKEY"] = value; }
    }
    protected DataTable mlDATATABLE
    {
        get { return ((DataTable)ViewState["mlDATATABLE"]); }
        set { ViewState["mlDATATABLE"] = value; }
    }
    protected DataTable mlDTASSETLIST
    {
        get { return ((DataTable)ViewState["mlDTASSETLIST"]); }
        set { ViewState["mlDTASSETLIST"] = value; }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        CekSession();
        mlMENUSTYLE = mlOBJPJ.AD_CHECKMENUSTYLE(Session["mgMENUSTYLE"].ToString(), this.MasterPageFile);
        this.MasterPageFile = mlMENUSTYLE;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"].ToString() + " MOVING ASSET V1.0";
        mlTITLE.Text = "MOVING ASSET V1.0";
        Session["mgDateTime"] = System.DateTime.Now;

        CekSession();

        //oEnt.CompanyID = "ISSP3";
        //oEnt.ModuleID = "PB";
        mlCOMPANYID = "ISSN3";
        mlCOMPANYTABLENAME = "ISS Servisystem, PT$";

        if (!IsPostBack)
        {
            PageSize = int.Parse(pagingRequest.PageSize.ToString());
            currentPage = 1;
            cmdWhere = "";
            sortBy = "";
            ucReqDate.dateValue = DateTime.Now.ToString("dd/MM/yyyy");
            ucReqDate.isEnabled = false;

            ucStartDate.dateValue = DateTime.Now.ToString("dd/MM/yyyy");
            ucStartDate.isEnabled = false;
            ucEndDate.dateValue = DateTime.Now.ToString("dd/MM/yyyy");
            ucSendingDate.dateValue = DateTime.Now.ToString("dd/MM/yyyy");


            FillDDL(); 
            SearchRecord();

            CreateDataTable();
            RetrieveAssetAdd();
        }

        hpLookupFrom.NavigateUrl = "javascript:OpenWinLookUpSiteCard_From('" + mpSITECARDFrom.ClientID + "','" + mpSITEDESCFrom.ClientID + "','" + hdnSiteCardID_From.ClientID + "','" + hdnSiteCardName_From.ClientID + "','" + mpJobNoFrom.ClientID + "','" + mpJobTaskNoFrom.ClientID + "','" + hdnJobNo_From.ClientID + "','" + hdnJobTaskNo_From.ClientID + "','" + ddlEntity.ClientID + "','AccMnt')";
        hpLookupTo.NavigateUrl = "javascript:OpenWinLookUpSiteCard_To('" + mpSITECARDTo.ClientID + "','" + mpSITEDESCTo.ClientID + "','" + hdnSiteCardID_TO.ClientID + "','" + hdnSiteCardName_TO.ClientID + "','" + mpJobNoTo.ClientID + "','" + mpJobTaskNoTo.ClientID + "','" + hdnJobNo_TO.ClientID + "','" + hdnJobTaskNo_TO.ClientID + "','" + ddlEntity.ClientID + "','AccMnt')";
    
    }
    protected void CreateDataTable()
    {
        mlDTASSETLIST = new DataTable();
        mlDTASSETLIST.Columns.Add("Nomor");
        mlDTASSETLIST.Columns.Add("AssetID");
        mlDTASSETLIST.Columns.Add("AssetDesc");
        mlDTASSETLIST.Columns.Add("Qty");
        mlDTASSETLIST.Columns.Add("Keterangan");

    }
    protected void RetrieveAssetAdd()
    {
        int i = mlDTASSETLIST.Rows.Count;
        DataRow drAsset = mlDTASSETLIST.NewRow();
        drAsset["Nomor"] = i+1;
        drAsset["AssetID"] = "";
        drAsset["AssetDesc"] = "";
        drAsset["Qty"] = "";
        drAsset["Keterangan"] = "";
        mlDTASSETLIST.Rows.Add(drAsset);

        mlDATAGRIDITEMLIST.DataSource = mlDTASSETLIST;
        mlDATAGRIDITEMLIST.DataBind();
    }    
    protected void SaveTemporaryListAsset()
    {
        mlDTASSETLIST.Rows.Clear();
        for (int i = 0; i < mlDATAGRIDITEMLIST.Items.Count; i++)
        {
            DataRow drDetail = mlDTASSETLIST.NewRow();
            drDetail["Nomor"] = i + 1;
            drDetail["AssetID"] = ((TextBox)mlDATAGRIDITEMLIST.Items[i].FindControl("txtAssetKey")).Text.ToString();
            drDetail["AssetDesc"] = ((TextBox)mlDATAGRIDITEMLIST.Items[i].FindControl("txtAssetName")).Text.ToString();
            drDetail["Qty"] = ((TextBox)mlDATAGRIDITEMLIST.Items[i].FindControl("txtQty")).Text.ToString();
            drDetail["Keterangan"] = ((TextBox)mlDATAGRIDITEMLIST.Items[i].FindControl("txtKeterangan")).Text;
            mlDTASSETLIST.Rows.Add(drDetail);
        }

    }
    protected void CekSession()
    {
        if (Session["mgUSERID"] == null)
        {
            Response.Redirect("~/pageconfirmation.aspx?mpMESSAGE=34FC35D4");
            return;
        }
    }
    protected void FillDDL()
    {
        DataTable mlDTTEMP = new DataTable() ;
 
        mpMR_TEMPLATE.Items.Clear();
        mpMR_TEMPLATE.Items.Add(new ListItem("Pilih", ""));
        mpMR_TEMPLATE.Items.Add(new ListItem("No Template", "-"));
  
        mlSQLTEMP = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID='MR_FORM' ORDER BY LinCode";
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISS");
        mlDTTEMP.Load(mlRSTEMP);
        for (int i = 0; i < mlDTTEMP.Rows.Count;i++ )
        {
            mpMR_TEMPLATE.Items.Add(new ListItem(mlDTTEMP.Rows[i]["LinCode"].ToString() + "-" + mlDTTEMP.Rows[i]["Description"].ToString(), mlOBJGF.GetStringAtPosition(mlDTTEMP.Rows[i]["LinCode"].ToString(), 0, "-")));
        }

        ////fill Dept DDL
        mlDTTEMP = new DataTable();
        mpDEPTFrom.Items.Clear();
        mpDEPTFrom.Items.Add(new ListItem("Pilih", ""));
        mpDEPTTo.Items.Clear();
        mpDEPTTo.Items.Add(new ListItem("Pilih", ""));
        mlSQLTEMP = "SELECT DISTINCT Name FROM  [" + mlCOMPANYTABLENAME + "Resource] ORDER BY Name";
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", mlCOMPANYID);
        mlDTTEMP.Load(mlRSTEMP);
        for (int i = 0; i < mlDTTEMP.Rows.Count;i++ )
        {
            mpDEPTFrom.Items.Add(new ListItem(mlDTTEMP.Rows[i]["Name"].ToString(), ""));
            mpDEPTTo.Items.Add(new ListItem(mlDTTEMP.Rows[i]["Name"].ToString(), ""));
        }
        mpDEPTFrom.Items.Add(new ListItem("Lainnya", ""));
        mpDEPTTo.Items.Add(new ListItem("Lainnya", ""));

        //// fill DeptCode DDL
        mlDTTEMP = new DataTable();
        ddDEPTCODE.Items.Clear();
        ddDEPTCODE.Items.Add(new ListItem("Pilih", ""));
        mlSQLTEMP = "SELECT * FROM  [" + mlCOMPANYTABLENAME + "Dimension Value] WHERE [DIMENSION CODE]='DEPT' ORDER BY NAME";
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", mlCOMPANYID);
        mlDTTEMP.Load(mlRSTEMP);
        for (int i = 0; i < mlDTTEMP.Rows.Count;i++ )
        {
            ddDEPTCODE.Items.Add(new ListItem(mlDTTEMP.Rows[i]["Code"].ToString() + "-" + mlDTTEMP.Rows[i]["Name"].ToString(), mlDTTEMP.Rows[i]["Code"].ToString()));
        }
        ddDEPTCODE.Items.Add(new ListItem("Lainnya", ""));

        //// fill State DDL
        mlDTTEMP = new DataTable();
        ddSTATE.Items.Clear();
        ddSTATE.Items.Add(new ListItem("Pilih", ""));
        mlSQL = "SELECT LinCode,Description FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID = 'PROPINSI'";
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3");
        mlDTTEMP.Load(mlRSTEMP);
        for (int i = 0; i < mlDTTEMP.Rows.Count; i++)
        {
            ddSTATE.Items.Add(new ListItem(mlDTTEMP.Rows[i]["LinCode"].ToString() + "-" + mlDTTEMP.Rows[i]["Description"].ToString(), mlDTTEMP.Rows[i]["LinCode"].ToString()));
        }

        //// fill Country DDL
        mlDTTEMP = new DataTable();
        ddCOUNTRY.Items.Clear();
        ddCOUNTRY.Items.Add(new ListItem("Pilih", ""));
        mlSQL = "SELECT LinCode,Description FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID = 'NEGARA'";
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3");
        mlDTTEMP.Load(mlRSTEMP);
        for (int i = 0; i < mlDTTEMP.Rows.Count; i++)
        {
            ddCOUNTRY.Items.Add(new ListItem(mlDTTEMP.Rows[i]["LinCode"].ToString() + "-" + mlDTTEMP.Rows[i]["Description"].ToString(), mlDTTEMP.Rows[i]["LinCode"].ToString()));
        }
        ddCOUNTRY.SelectedValue = "IDN";

    }
    protected void NavigationButtonClicked(usercontroller_ucPaging.NavigationButtonEventArgs e)
    {
        retrievepaging();
    }
    protected void retrievepaging()
    {
        currentPage = pagingRequest.currentPage;
        PageSize = int.Parse(pagingRequest.PageSize.ToString());
        RetrieveData();
    }
    protected void RetrieveData()
    {
        //oEnt.ErrorMesssage = "";
        //oEnt.PageSize = PageSize;
        //oEnt.CurrentPage = currentPage;
        //oEnt.WhereCond = cmdWhere;
        //oEnt.SortBy = sortBy;
        //oDA.GetInvoiceDeliveryPaging(oEnt);
        //if (oEnt.ErrorMesssage != "")
        //{
        //    mlMESSAGE.Text = oEnt.ErrorMesssage;
        //    return;
        //}
        //if (oEnt.dtListData.Rows.Count == 0)
        //{
        //    mlMESSAGE.Text = ModuleSystemConstant.MESSAGE_DATA_NOT_FOUND;
        //    return;
        //}
        //mlDGDATALIST.DataSource = oEnt.dtListData;
        //mlDGDATALIST.DataBind();

        //totalRecords = oEnt.TotalRecord;
        totalRecords = 0;
        pagingRequest.PageSize = PageSize;
        pagingRequest.currentPage = currentPage;
        pagingRequest.totalRecords = Convert.ToInt64(totalRecords.ToString());
        pagingRequest.PagingFooter(totalRecords, PageSize);
    }
    protected void NewRecord()
    {
        pnlSearch.Visible = false;
        pnlDATALIST.Visible = false;        
        pnlTemplate.Visible = true;
    }
    protected void SearchRecord()
    {
        cmdWhere = "";
        if (ddlSearchBy.SelectedValue != "")
        {
            if (txtSearchBy.Text.Contains("%"))
            {
                cmdWhere += " " + ddlSearchBy.SelectedValue + " like '%" + txtSearchBy.Text + "%'";
            }
            else
            {
                cmdWhere += " " + ddlSearchBy.SelectedValue + " = '" + txtSearchBy.Text + "'";
            }
        }

        RetrieveData();
    }
    protected void btSUBMITTEMPLATE_Click(object sender, ImageClickEventArgs e)
    {
        pnlFILL.Visible = true;
        pnlSearch.Visible = false;
    }
    protected void imbCalcDay_Click(object sender, ImageClickEventArgs e)
    {
        mlMESSAGE.Text = "";

        if(Convert.ToDateTime(ucEndDate.dateValue) < Convert.ToDateTime(ucStartDate.dateValue))
        {
            mlMESSAGE.Text = "End Date must have greater than Start Date";
            return;
        }
        txtLama.Text = int.Parse((Convert.ToDateTime(ucEndDate.dateValue) - Convert.ToDateTime(ucStartDate.dateValue)).ToString("dd")).ToString()  ;
    }
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchRecord();
    }
    protected void mlDGDATALIST_ItemCommand(object source, DataGridCommandEventArgs e)
    {

    }
    protected void mlDGDATALIST_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

    }
    protected void mlDATAGRIDITEMLIST_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if(e.Item.ItemIndex >= 0)
        {
            switch(e.CommandName)
            {
                case"Delete":
                    SaveTemporaryListAsset();
                    mlDTASSETLIST.Rows.RemoveAt(e.Item.ItemIndex);
                    mlDATAGRIDITEMLIST.DataSource = mlDTASSETLIST;
                    mlDATAGRIDITEMLIST.DataBind();
                    break;
                case "Add":
                    SaveTemporaryListAsset();
                    RetrieveAssetAdd();
                    break;

            }
        }
    }
    protected void mlDATAGRIDITEMLIST_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemIndex >=0)
        {
            HyperLink hlLookUpAsset = ((HyperLink)e.Item.FindControl("hlLookUpAsset"));
            TextBox txtAssetID = ((TextBox)e.Item.FindControl("txtAssetKey"));
            TextBox txtAssetDesc= ((TextBox)e.Item.FindControl("txtAssetName"));

            hlLookUpAsset.NavigateUrl = "javascript:OpenWinLookUpAsset('" + txtAssetID.ClientID + "','" + txtAssetDesc.ClientID + "','" + hdnAssetID.ClientID + "','" + hdnAssetDesc.ClientID + "','" + ddlEntity.SelectedItem.Text + "','AccMnt')";

        }
    }
    protected void btNewRecord_Click(object sender, ImageClickEventArgs e)
    {
        NewRecord();
    }
    protected void rdblTypeRequest_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (rdblTypeRequest.SelectedItem.Value)
        {
            case "Mutasi":
                pnlMovingEntry.Visible = false;
                break;
            case "Moving":
                pnlMovingEntry.Visible = true; 
                break;
        }
    }
}