using System;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
//using System.Data.SqlClient;
using System.Data.OleDb;
using IASClass;
using IAS.Core.CSCode;
using IAS.APP.DataAccess.HR;
using IAS.Initialize;

public partial class pj_HR_hr_allowance : System.Web.UI.Page
{
    ucmGeneralSystem mlOBJGS = new ucmGeneralSystem();
    ucmGeneralFunction mlOBJGF = new ucmGeneralFunction();
    OleDbDataReader mlREADER ;
    OleDbDataReader mlREADER2 ;

    ModuleGeneralFunction oMGF = new ModuleGeneralFunction();
    ModuleDBFunction oMDBF = new ModuleDBFunction();
    ModuleGeneralSystem oMGS = new ModuleGeneralSystem();
    ModuleInitialization oMI = new ModuleInitialization();
    ModuleSystemConstant oConst ;

    VariableCore oVar = new VariableCore();
    FunctionCore oFunc = new FunctionCore();

    VariableHR oEnt = new VariableHR();
    FunctionHR oDA = new FunctionHR();

    protected String mlSQL
    {
        get { return ((String)ViewState["mlSQL"]); }
        set { ViewState["mlSQL"] = value; }
    }
    protected String mlKEY
    {
        get { return ((String)ViewState["mlKEY"]); }
        set { ViewState["mlKEY"] = value; }
    }
    protected String mlRECORDSTATUS
    {
        get { return ((String)ViewState["mlRECORDSTATUS"]); }
        set { ViewState["mlRECORDSTATUS"] = value; }
    }
    protected String LoginID
    {
        get { return ((String)ViewState["LoginID"]); }
        set { ViewState["LoginID"] = value; }
    }
    protected String FlagAction
    {
        get { return ((String)ViewState["FlagAction"]); }
        set { ViewState["FlagAction"] = value; }
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
    protected DataTable mlDT_LISTALLOWANCETYPE
    {
        get { return ((DataTable)ViewState["mlDT_LISTALLOWANCETYPE"]); }
        set { ViewState["mlDT_LISTALLOWANCETYPE"] = value; }
    }

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
        mlTITLE.Text = "Menu HR-Allowance V2.02";
        oMI.Main();

        if (oMGS.ValidateExpiredDate())
        {
            return;
        }
        if(Session["mgACTIVECOMPANY"] == "")
        {
            Session["mgACTIVECOMPANY"] = ModuleBaseSetting.COMPANYDEFAULT;
        }

        LoginID = oMDBF.Loginid;
        
        AksesButton();

        if (!IsPostBack)
        {
            PageSize = int.Parse(pagingAllow.PageSize.ToString());
            currentPage = 1;
            totalPages = 0;
            cmdWhere="";
            sortBy="";

            GetPayrollPeriodeType();
            GetMonth();
            PayrollPeriodeChange();

            ucAllowDate.dateValue = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            if (btSearchRecord.Visible)
            { 
                ClickSearch();
            }
            
        }
    }
    protected void AksesButton()
    {
        oVar.CompanyID = "AD";
        oVar.ModuleID = "AD";
        oVar.strConnection = oMDBF.GetConnectionString();
        oVar.MenuID = "HR003";
        oVar.LoginId = LoginID;
        oFunc.CekAksesMenu(oVar);
                
        btNewRecord.Visible = oVar.imbCreate;
        btSearchRecord.Visible = oVar.imbRead;
    }
    protected void ClickSearch()
    {
        cmdWhere = "";
        if (ddlSearchBy.SelectedValue != "")
        {
            if (txtSearchBy.Text.Contains("%"))
            {
                cmdWhere += " and " + ddlSearchBy.SelectedValue + " like '%" + txtSearchBy.Text + "%'";
            }
            else
            {
                cmdWhere += " and " + ddlSearchBy.SelectedValue + " = '" + txtSearchBy.Text + "'";
            }
        }
        if (oVar.imbCreate == true && oVar.imbApprove == false)
        {
            cmdWhere += " and status = 'REQ' ";
        }
        if (oVar.imbApprove)
        {
            if (oVar.ApprovalTypeID == "APR0001")
            {
                cmdWhere += " and status in ('REQ','RVW') ";
            }
            else if (oVar.ApprovalTypeID == "APR0002")
            {
                cmdWhere += " and status in ('RVW','AUTH') ";
            }
        }
        BindGrid();
    }
    protected void BindGrid()
    {        
        //oEnt.StrConnection = oMDBF.GetConnectionString();
        oEnt.ErrorMesssage = "";
        oEnt.PageSize = PageSize;
        oEnt.CurrentPage = currentPage;
        oEnt.WhereCond = cmdWhere;
        oEnt.SortBy = sortBy;
        oEnt.CompanyID = "ISSP3";
        oEnt.ModuleID = "PB";
        oDA.GetListAllowancePaging(oEnt);
        if (oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }
        dgListData.DataSource = oEnt.dtListData;
        dgListData.DataBind();
        totalRecords = oEnt.TotalRecord;
        pagingAllow.PageSize = long.Parse(PageSize.ToString());
        pagingAllow.currentPage = currentPage;
        pagingAllow.PagingFooter(totalRecords, PageSize);        

    }
    
    protected void dgListData_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
        {
            oEnt.AllowanceId = ((HyperLink)e.Item.FindControl("hlAllowID")).Text.ToString();
            oEnt.ErrorMesssage = "";
            oEnt.StrConnection = oMDBF.GetConnectionString();
            oEnt.Transaction_date = Convert.ToDateTime(Session["mgDateTime"].ToString());
            oEnt.AllowanceStartdate = Convert.ToDateTime(Session["mgDateTime"].ToString());
            oEnt.AllowanceEnddate = Convert.ToDateTime(Session["mgDateTime"].ToString());
            oEnt.Approval1Date = Convert.ToDateTime(Session["mgDateTime"].ToString());
            oEnt.Approval2Date = Convert.ToDateTime(Session["mgDateTime"].ToString());
            oEnt.LoginID = LoginID;

            switch (e.CommandName)
            {
                case "Edit":
                    FlagAction = "EDIT";
                    GetDataAllowance();
                    break;
                case "Delete":
                    FlagAction = "DELETE";
                    oEnt.FlagAction = FlagAction;
                    oEnt.CompanyID = "ISSP3";
                    oEnt.ModuleID = "PB";
                    oDA.ApprovalAction(oEnt);
                    if (oEnt.ErrorMesssage != "")
                    {
                        mlMESSAGE.Text = oEnt.ErrorMesssage;
                    }
                    BindGrid();
                    break;
                case "Review":
                    FlagAction = "REVIEW";
                    oEnt.FlagAction = FlagAction;
                    oEnt.Approval1User = LoginID;
                    oEnt.CompanyID = "ISSP3";
                    oEnt.ModuleID = "PB";
                    oDA.ApprovalAction(oEnt);
                    if (oEnt.ErrorMesssage != "")
                    {
                        mlMESSAGE.Text = oEnt.ErrorMesssage;
                    }
                    BindGrid();
                    break;
                case "Authorize":
                    FlagAction = "AUTHORIZE";
                    oEnt.FlagAction = FlagAction;
                    oEnt.Approval2User = LoginID;
                    oEnt.CompanyID = "ISSP3";
                    oEnt.ModuleID = "PB";
                    oDA.ApprovalAction(oEnt);
                    if (oEnt.ErrorMesssage != "")
                    {
                        mlMESSAGE.Text = oEnt.ErrorMesssage;
                    }
                    BindGrid();
                    break;
            }
        }
    }
    protected void dgListData_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
        {
            ImageButton imbEdit = ((ImageButton)e.Item.FindControl("imbEdit"));
            ImageButton imbDelete = ((ImageButton)e.Item.FindControl("imbDelete"));
            ImageButton imbReview = ((ImageButton)e.Item.FindControl("imbReview"));
            ImageButton imbAuth = ((ImageButton)e.Item.FindControl("imbAuth"));
            String Status = ((Label)e.Item.FindControl("lblStatus")).Text.ToString();

            imbDelete.Attributes.Add("OnClick", "javascript:return confirm('Apakah anda yakin untuk Delete Allowance ini ??')");
            imbReview.Attributes.Add("OnClick", "javascript:return confirm('Apakah anda yakin untuk Review Allowance ini ??')");
            imbAuth.Attributes.Add("OnClick", "javascript:return confirm('Apakah anda yakin untuk Authorize Allowance ini ??')");

            imbEdit.Visible = false;
            imbDelete.Visible = false;
            imbReview.Visible = false;
            imbAuth.Visible = false;

            if(oVar.imbApprove)
            {
                if (oVar.ApprovalTypeID == "APR0001")   //// Status Review
                {
                    if (Status == "REQUEST")
                    {
                        imbReview.Visible = oVar.imbApprove;
                        imbEdit.Visible = oVar.imbWrite;
                        imbDelete.Visible = oVar.imbDelete;
                    }

                }
                if (oVar.ApprovalTypeID == "APR0002")   //// Status Authorize
                {
                    if (Status == "REVIEW")
                    {
                        imbAuth.Visible = oVar.imbApprove;
                        imbEdit.Visible = oVar.imbWrite;
                        imbDelete.Visible = oVar.imbDelete;
                    }
                }
            }
            
        }
    }    
    protected void dgListData_SortCommand(object source, DataGridSortCommandEventArgs e)
    {

    }
    protected void GetDataAllowance()
    {
        oEnt.StrConnection = oMDBF.GetConnectionString();
        oEnt.ErrorMesssage = "";
        oEnt.CompanyID = "ISSP3";
        oEnt.ModuleID = "PB";
        oDA.GetDataAllowance(oEnt);
        mlDT_LISTDATA = oEnt.dtListData;
        if (mlDT_LISTDATA.Rows.Count >= 0)
        {
            for (int i = 0; i < mlDT_LISTDATA.Rows.Count; i++)
            {
                lblAllowID.Text = mlDT_LISTDATA.Rows[i]["allow_id"].ToString();
                ddlEntity.SelectedValue = mlDT_LISTDATA.Rows[i]["entity_id"].ToString();
                ucAllowDate.dateValue = mlDT_LISTDATA.Rows[i]["transaction_date"].ToString();
                ucStartDate.dateValue = mlDT_LISTDATA.Rows[i]["allowance_startdate"].ToString();
                ucEndDate.dateValue = mlDT_LISTDATA.Rows[i]["allowance_enddate"].ToString();
                txtKeterangan.Text = mlDT_LISTDATA.Rows[i]["keterangan"].ToString();
                txtSiteCard.Text = mlDT_LISTDATA.Rows[i]["sitecard"].ToString();
                lblSiteCard.Text = mlDT_LISTDATA.Rows[i]["SitecardName"].ToString();

                String DateAwal = ucStartDate.dateValue.Substring(0, 2);
                String DateAkhir = ucEndDate.dateValue.Substring(0, 2);
                if(int.Parse(DateAkhir) >= 28)
                {
                    DateAkhir = "30";
                }
                String Periodepayrol = DateAwal + "-" + DateAkhir;
                for(int n=0;n<ddlPayrolPeriodeType.Items.Count;n++)
                {
                    if(ddlPayrolPeriodeType.Items[n].Text == Periodepayrol)
                    {
                        ddlPayrolPeriodeType.SelectedIndex = n;
                        break;
                    }
                }

                String Bulan = oMGF.ConvertDate2(ucEndDate.dateValue).Month.ToString("00");
                for (int x = 0; x < ddlMonth.Items.Count;x++ )
                {
                    if(ddlMonth.Items[x].Value == Bulan)
                    {
                        ddlMonth.SelectedIndex = x;
                        break;
                    }
                }
            }

            RefreshSiteCard();
        }
        pnlGrid.Visible = false;
        pnTOOLBAR.Visible = false;
        pnlSearch.Visible = false;
        pnlInput.Visible = true;
        pnlDetail.Visible = true;
        imbRefresh.Visible = false;
    }
    protected void RefreshSiteCard()
    {

        DataSet mlDATASET = new DataSet();
        mlDT_LISTDETAIL = new DataTable();

        mlMESSAGE.Text = "";

        String XMLFile = "http://10.62.0.43/iss/a2a/?key=kk9T5_123YH9HH_MPtB0n9&sc=" + txtSiteCard.Text;
        mlDATASET.ReadXml(XMLFile, XmlReadMode.Auto);

        if (mlDATASET.Tables.Count <= 0)
        {
            mlMESSAGE.Text = "Sorry...SiteCard ini tidak ada Employee...";
            return;
        }
        mlDT_LISTDETAIL = mlDATASET.Tables[0];
        dtgSCDetail.DataSource = mlDT_LISTDETAIL;
        dtgSCDetail.DataBind();

        dtgSCTabel.DataSource = mlDT_LISTDETAIL;
        dtgSCTabel.DataBind();
        lblSiteCard.Text = oMGS.ISS_XMGeneralLostFocus("SITECARD_DESC", txtSiteCard.Text.Trim(), "");

        pnlDetail.Visible = true;
        PnlDetailModeDDL.Visible = true;
        pnlDetailModeTabel.Visible = false;
    }
    
    protected void dtgSCDetail_ItemCommand(object source, DataGridCommandEventArgs e)
    {

    }
    protected void dtgSCDetail_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
        {
            GetAllowanceType();
            DropDownList ddlAllow1 = ((DropDownList)e.Item.FindControl("ddlAllowance1"));
            DropDownList ddlAllow2 = ((DropDownList)e.Item.FindControl("ddlAllowance2"));
            ddlAllow1.Items.Add(new ListItem("Select One", ""));
            ddlAllow2.Items.Add(new ListItem("Select One", ""));
            for (int i = 0; i < mlDT_LISTALLOWANCETYPE.Rows.Count; i++)
            {
                ddlAllow1.Items.Add(new ListItem(mlDT_LISTALLOWANCETYPE.Rows[i]["allow_typename"].ToString(), mlDT_LISTALLOWANCETYPE.Rows[i]["allow_typeid"].ToString()));
                ddlAllow2.Items.Add(new ListItem(mlDT_LISTALLOWANCETYPE.Rows[i]["allow_typename"].ToString(), mlDT_LISTALLOWANCETYPE.Rows[i]["allow_typeid"].ToString()));

            }
            if (FlagAction == "EDIT")
            {
                for (int n = 0; n < ddlAllow1.Items.Count; n++)
                {
                    if (ddlAllow1.Items[n].Value == mlDT_LISTDATA.Rows[e.Item.ItemIndex]["allow_typeid1"].ToString())
                    {
                        ddlAllow1.SelectedIndex = n;
                        break;
                    }
                }
                String ucAmount = "";
                ucAmount = mlDT_LISTDATA.Rows[e.Item.ItemIndex]["allow_amount1"].ToString();
                ((ucInputNumber)e.Item.FindControl("ucAmountAllow1")).Text = ucAmount.Replace(",00","");

                for (int n = 0; n < ddlAllow2.Items.Count; n++)
                {
                    if (ddlAllow2.Items[n].Value == mlDT_LISTDATA.Rows[e.Item.ItemIndex]["allow_typeid2"].ToString())
                    {
                        ddlAllow2.SelectedIndex = n;
                        break;
                    }
                }
                ucAmount = mlDT_LISTDATA.Rows[e.Item.ItemIndex]["allow_amount2"].ToString();
                ((ucInputNumber)e.Item.FindControl("ucAmountAllow2")).Text = ucAmount.Replace(",00", "");


            }
        }
    }
    protected void dtgSCDetail_SortCommand(object source, DataGridSortCommandEventArgs e)
    {

    }
    protected void GetAllowanceType()
    {
        oEnt.StrConnection = oMDBF.GetConnectionString();
        oEnt.ErrorMesssage = "";
        oEnt.CompanyID = "ISS";
        oEnt.ModuleID = "PB";
        oDA.GetAllowanceType(oEnt);
        if (oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }
        mlDT_LISTALLOWANCETYPE = new DataTable();
        mlDT_LISTALLOWANCETYPE = oEnt.dtListData;
    }
    protected void GetPayrollPeriodeType()
    {
      //  oEnt.StrConnection = oMDBF.GetConnectionString();
        oEnt.ErrorMesssage = "";
        oEnt.CompanyID = "ISS";
        oEnt.ModuleID = "PB";
        oDA.GetPayrollPeriode(oEnt);
        if(oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }
        
        ddlPayrolPeriodeType.Items.Clear();
        for (int i = 0; i < oEnt.dtListData.Rows.Count; i++)
        {
            ddlPayrolPeriodeType.Items.Add(new ListItem(oEnt.dtListData.Rows[i]["DATA"].ToString(), oEnt.dtListData.Rows[i]["ID"].ToString()));    
        }
        //ddlPayrolPeriodeType.SelectedIndex = 0;
    }
    protected void GetMonth()
    {
        DateTime Tgl = Convert.ToDateTime(DateTime.Now.Year.ToString("0000") + "/01" + "/01");
        String IDMonth = Tgl.Month.ToString("00");
        ddlMonth.Items.Clear();
        for (int i = 1; i <= 12; i++)
        {
            ddlMonth.Items.Add(new ListItem(Tgl.ToLongDateString().Substring(3, Tgl.ToLongDateString().Length - 8), Tgl.Month.ToString("00")));
            Tgl = Tgl.AddMonths(1);
        }

        for(int n=0;n<ddlMonth.Items.Count;n++)
        {
            if(ddlMonth.Items[n].Value == DateTime.Now.Month.ToString("00"))
            {
                ddlMonth.SelectedIndex = n;
                break;
            }
        }
    }
    protected void NavigationButtonClicked(usercontroller_ucPaging.NavigationButtonEventArgs e)
    {
        retrievepaging();
    }
    protected void retrievepaging()
    {
        currentPage = pagingAllow.currentPage;
        PageSize = int.Parse(pagingAllow.PageSize.ToString());
        BindGrid();
    }
    
    #region --/ButtonEvent\--
    protected void btSearchRecord_Click(object sender, ImageClickEventArgs e)
    {
        pnlSearch.Visible = true;
    }
    protected void btnReset_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("hr_allowance.aspx");
    }
    protected void btNewRecord_Click(object sender, ImageClickEventArgs e)
    {
        FlagAction = "ADD";
        pnlInput.Visible = true;
        pnlSearch.Visible = false;
        pnlGrid.Visible = false;
        pnTOOLBAR.Visible = false;
        
    }
    protected void imbRefresh_Click(object sender, ImageClickEventArgs e)
    {
        RefreshSiteCard();
    }
    protected void imbBack_Click(object sender, ImageClickEventArgs e)
    {


        Response.Redirect("hr_allowance.aspx");
    }
    protected void imbSave_Click(object sender, ImageClickEventArgs e)
    {
        Boolean FlagCheck = true;

        oEnt.StrConnection = oMDBF.GetConnectionString();
        oEnt.FlagAction = FlagAction;
        oEnt.ErrorMesssage = "";

        mlDT_LISTDETAIL = new DataTable();
        mlDT_LISTDETAIL.Columns.Add("NIK");
        mlDT_LISTDETAIL.Columns.Add("Nama");
        mlDT_LISTDETAIL.Columns.Add("Posisi");
        mlDT_LISTDETAIL.Columns.Add("AllowTypeID1");
        mlDT_LISTDETAIL.Columns.Add("AllowAmount1");
        mlDT_LISTDETAIL.Columns.Add("AllowTypeID2");
        mlDT_LISTDETAIL.Columns.Add("AllowAmount2");
        mlDT_LISTDETAIL.Columns.Add("Description");

        if (rblSCDetail.SelectedValue == "DDL")
        {
            for (int i = 0; i < dtgSCDetail.Items.Count; i++)
            {
                DataRow drDetail = mlDT_LISTDETAIL.NewRow();
                drDetail["NIK"] = ((Label)dtgSCDetail.Items[i].FindControl("lblNIK")).Text;
                drDetail["Nama"] = ((Label)dtgSCDetail.Items[i].FindControl("lblName")).Text;
                drDetail["Posisi"] = ((Label)dtgSCDetail.Items[i].FindControl("lblPosisi")).Text;
                drDetail["AllowTypeID1"] = ((DropDownList)dtgSCDetail.Items[i].FindControl("ddlAllowance1")).SelectedValue.ToString();
                drDetail["AllowAmount1"] = ((ucInputNumber)dtgSCDetail.Items[i].FindControl("ucAmountAllow1")).Text;
                drDetail["AllowTypeID2"] = ((DropDownList)dtgSCDetail.Items[i].FindControl("ddlAllowance2")).SelectedValue.ToString();
                drDetail["AllowAmount2"] = ((ucInputNumber)dtgSCDetail.Items[i].FindControl("ucAmountAllow2")).Text;
                mlDT_LISTDETAIL.Rows.Add(drDetail);

                String AllowType1 = ((DropDownList)dtgSCDetail.Items[i].FindControl("ddlAllowance1")).SelectedValue.ToString();
                String AllowType2 = ((DropDownList)dtgSCDetail.Items[i].FindControl("ddlAllowance2")).SelectedValue.ToString();

                Decimal AllowAmount1 = Decimal.Parse(((ucInputNumber)dtgSCDetail.Items[i].FindControl("ucAmountAllow1")).Text.ToString());
                Decimal AllowAmount2 = Decimal.Parse(((ucInputNumber)dtgSCDetail.Items[i].FindControl("ucAmountAllow2")).Text.ToString());

                if (AllowAmount1 != 0 || AllowAmount2 != 0)
                {
                    if (AllowType1 == AllowType2 && AllowAmount1 == AllowAmount2)
                    {
                        FlagCheck = false;
                        String MsgBox = ((Label)dtgSCDetail.Items[i].FindControl("lblName")).Text + " memiliki Type Allowance yang sama...Silahkan perbaiki Type Allowancenya..";
                        oMDBF.ShowMessage(MsgBox);
                        break;
                    }
                }
            }
        }
        else if(rblSCDetail.SelectedValue == "TBL")
        {
            String AllowTypeID = "";
            Decimal AllowAmount = 0;
            for(int i = 0;i < dtgSCTabel.Items.Count;i++)
            {
                DataRow drDetail;

                if(((ucInputNumber)dtgSCTabel.Items[i].FindControl("ucAmountAllowType1")).Text.ToString() != "0" )
                {
                    drDetail = mlDT_LISTDETAIL.NewRow();
                    drDetail["NIK"] = ((Label)dtgSCTabel.Items[i].FindControl("lblNIK")).Text;
                    drDetail["Nama"] = ((Label)dtgSCTabel.Items[i].FindControl("lblName")).Text;
                    drDetail["Posisi"] = ((Label)dtgSCTabel.Items[i].FindControl("lblPosisi")).Text;
                    drDetail["AllowTypeID1"] = ((Label)dtgSCTabel.Items[i].FindControl("lblIDAllowType1")).Text.ToString();
                    drDetail["AllowAmount1"] = ((ucInputNumber)dtgSCTabel.Items[i].FindControl("ucAmountAllowType1")).Text;
                    drDetail["AllowTypeID2"] = "-";
                    drDetail["AllowAmount2"] = "0";
                    mlDT_LISTDETAIL.Rows.Add(drDetail);
                }
                if (((ucInputNumber)dtgSCTabel.Items[i].FindControl("ucAmountAllowType2")).Text.ToString() != "0")
                {
                    drDetail = mlDT_LISTDETAIL.NewRow();
                    drDetail["NIK"] = ((Label)dtgSCTabel.Items[i].FindControl("lblNIK")).Text;
                    drDetail["Nama"] = ((Label)dtgSCTabel.Items[i].FindControl("lblName")).Text;
                    drDetail["Posisi"] = ((Label)dtgSCTabel.Items[i].FindControl("lblPosisi")).Text;
                    drDetail["AllowTypeID1"] = ((Label)dtgSCTabel.Items[i].FindControl("lblIDAllowType2")).Text.ToString();
                    drDetail["AllowAmount1"] = ((ucInputNumber)dtgSCTabel.Items[i].FindControl("ucAmountAllowType2")).Text;
                    drDetail["AllowTypeID2"] = "-";
                    drDetail["AllowAmount2"] = "0";
                    mlDT_LISTDETAIL.Rows.Add(drDetail);
                }
                if (((ucInputNumber)dtgSCTabel.Items[i].FindControl("ucAmountAllowType3")).Text.ToString() != "0")
                {
                    drDetail = mlDT_LISTDETAIL.NewRow();
                    drDetail["NIK"] = ((Label)dtgSCTabel.Items[i].FindControl("lblNIK")).Text;
                    drDetail["Nama"] = ((Label)dtgSCTabel.Items[i].FindControl("lblName")).Text;
                    drDetail["Posisi"] = ((Label)dtgSCTabel.Items[i].FindControl("lblPosisi")).Text;
                    drDetail["AllowTypeID1"] = ((Label)dtgSCTabel.Items[i].FindControl("lblIDAllowType3")).Text.ToString();
                    drDetail["AllowAmount1"] = ((ucInputNumber)dtgSCTabel.Items[i].FindControl("ucAmountAllowType3")).Text;
                    drDetail["AllowTypeID2"] = "-";
                    drDetail["AllowAmount2"] = "0";
                    mlDT_LISTDETAIL.Rows.Add(drDetail);
                }
                if (((ucInputNumber)dtgSCTabel.Items[i].FindControl("ucAmountAllowType4")).Text.ToString() != "0")
                {
                    drDetail = mlDT_LISTDETAIL.NewRow();
                    drDetail["NIK"] = ((Label)dtgSCTabel.Items[i].FindControl("lblNIK")).Text;
                    drDetail["Nama"] = ((Label)dtgSCTabel.Items[i].FindControl("lblName")).Text;
                    drDetail["Posisi"] = ((Label)dtgSCTabel.Items[i].FindControl("lblPosisi")).Text;
                    drDetail["AllowTypeID1"] = ((Label)dtgSCTabel.Items[i].FindControl("lblIDAllowType4")).Text.ToString();
                    drDetail["AllowAmount1"] = ((ucInputNumber)dtgSCTabel.Items[i].FindControl("ucAmountAllowType4")).Text;
                    drDetail["AllowTypeID2"] = "-";
                    drDetail["AllowAmount2"] = "0";
                    mlDT_LISTDETAIL.Rows.Add(drDetail);
                }
                if (((ucInputNumber)dtgSCTabel.Items[i].FindControl("ucAmountAllowType5")).Text.ToString() != "0")
                {
                    drDetail = mlDT_LISTDETAIL.NewRow();
                    drDetail["NIK"] = ((Label)dtgSCTabel.Items[i].FindControl("lblNIK")).Text;
                    drDetail["Nama"] = ((Label)dtgSCTabel.Items[i].FindControl("lblName")).Text;
                    drDetail["Posisi"] = ((Label)dtgSCTabel.Items[i].FindControl("lblPosisi")).Text;
                    drDetail["AllowTypeID1"] = ((Label)dtgSCTabel.Items[i].FindControl("lblIDAllowType5")).Text.ToString();
                    drDetail["AllowAmount1"] = ((ucInputNumber)dtgSCTabel.Items[i].FindControl("ucAmountAllowType5")).Text;
                    drDetail["AllowTypeID2"] = "-";
                    drDetail["AllowAmount2"] = "0";
                    mlDT_LISTDETAIL.Rows.Add(drDetail);
                }
                if (((ucInputNumber)dtgSCTabel.Items[i].FindControl("ucAmountAllowType6")).Text.ToString() != "0")
                {
                    drDetail = mlDT_LISTDETAIL.NewRow();
                    drDetail["NIK"] = ((Label)dtgSCTabel.Items[i].FindControl("lblNIK")).Text;
                    drDetail["Nama"] = ((Label)dtgSCTabel.Items[i].FindControl("lblName")).Text;
                    drDetail["Posisi"] = ((Label)dtgSCTabel.Items[i].FindControl("lblPosisi")).Text;
                    drDetail["AllowTypeID1"] = ((Label)dtgSCTabel.Items[i].FindControl("lblIDAllowType6")).Text.ToString();
                    drDetail["AllowAmount1"] = ((ucInputNumber)dtgSCTabel.Items[i].FindControl("ucAmountAllowType6")).Text;
                    drDetail["AllowTypeID2"] = "-";
                    drDetail["AllowAmount2"] = "0";
                    mlDT_LISTDETAIL.Rows.Add(drDetail);
                }
                if (((ucInputNumber)dtgSCTabel.Items[i].FindControl("ucAmountAllowType7")).Text.ToString() != "0")
                {
                    drDetail = mlDT_LISTDETAIL.NewRow();
                    drDetail["NIK"] = ((Label)dtgSCTabel.Items[i].FindControl("lblNIK")).Text;
                    drDetail["Nama"] = ((Label)dtgSCTabel.Items[i].FindControl("lblName")).Text;
                    drDetail["Posisi"] = ((Label)dtgSCTabel.Items[i].FindControl("lblPosisi")).Text;
                    drDetail["AllowTypeID1"] = ((Label)dtgSCTabel.Items[i].FindControl("lblIDAllowType7")).Text.ToString();
                    drDetail["AllowAmount1"] = ((ucInputNumber)dtgSCTabel.Items[i].FindControl("ucAmountAllowType7")).Text;
                    drDetail["AllowTypeID2"] = "-";
                    drDetail["AllowAmount2"] = "0";
                    mlDT_LISTDETAIL.Rows.Add(drDetail);
                }
                if (((ucInputNumber)dtgSCTabel.Items[i].FindControl("ucAmountAllowType8")).Text.ToString() != "0")
                {
                    drDetail = mlDT_LISTDETAIL.NewRow();
                    drDetail["NIK"] = ((Label)dtgSCTabel.Items[i].FindControl("lblNIK")).Text;
                    drDetail["Nama"] = ((Label)dtgSCTabel.Items[i].FindControl("lblName")).Text;
                    drDetail["Posisi"] = ((Label)dtgSCTabel.Items[i].FindControl("lblPosisi")).Text;
                    drDetail["AllowTypeID1"] = ((Label)dtgSCTabel.Items[i].FindControl("lblIDAllowType8")).Text.ToString();
                    drDetail["AllowAmount1"] = ((ucInputNumber)dtgSCTabel.Items[i].FindControl("ucAmountAllowType8")).Text;
                    drDetail["AllowTypeID2"] = "-";
                    drDetail["AllowAmount2"] = "0";
                    mlDT_LISTDETAIL.Rows.Add(drDetail);
                }
            }
            FlagCheck = true;
        }

        if (!FlagCheck)
        {
            return;
        }

        if (FlagAction == "ADD")
        {
            oEnt.Transaction_date = oMGF.ConvertDate2(ucAllowDate.dateValue);
            oEnt.Entity_id = ddlEntity.SelectedValue;
            oEnt.AllowanceStartdate = oMGF.ConvertDate2(ucStartDate.dateValue);
            oEnt.AllowanceEnddate = oMGF.ConvertDate2(ucEndDate.dateValue);
            oEnt.Sitecard = txtSiteCard.Text;
            oEnt.Keterangan = txtKeterangan.Text;
            oEnt.dtListDetail = mlDT_LISTDETAIL;
            oEnt.Status = "REQ";
            oEnt.Approval1Date = oMGF.ConvertDate2("01/01/1900");
            oEnt.Approval2Date = oMGF.ConvertDate2("01/01/1900");
            oEnt.LoginID = LoginID;
            oDA.SaveAllowance(oEnt);

            if (oEnt.ErrorMesssage != "")
            {
                mlMESSAGE.Text = ModuleSystemConstant.MESSAGE_INSERT_FAILED;
                return;
            }
            else
            {
                mlMESSAGE.Text = ModuleSystemConstant.MESSAGE_INSERT_SUCCESS;
                imbSave.Visible = false;
            }

        }
        else
        {
            oEnt.Transaction_date = oMGF.ConvertDate2(ucAllowDate.dateValue);
            oEnt.Entity_id = ddlEntity.SelectedValue;
            oEnt.AllowanceStartdate = oMGF.ConvertDate2(ucStartDate.dateValue);
            oEnt.AllowanceEnddate = oMGF.ConvertDate2(ucEndDate.dateValue);
            oEnt.Sitecard = txtSiteCard.Text;
            oEnt.Keterangan = txtKeterangan.Text;
            oEnt.dtListDetail = mlDT_LISTDATA;
            oEnt.Approval1Date = oMGF.ConvertDate2("01/01/1900");
            oEnt.Approval2Date = oMGF.ConvertDate2("01/01/1900");
            oEnt.LoginID = LoginID;
            oEnt.AllowanceId = lblAllowID.Text;
            oEnt.dtListDetail = mlDT_LISTDETAIL;
            oDA.SaveAllowance(oEnt);

            if (oEnt.ErrorMesssage != "")
            {
                mlMESSAGE.Text = ModuleSystemConstant.MESSAGE_UPDATE_FAILED;
                return;
            }
            else
            {
                mlMESSAGE.Text = ModuleSystemConstant.MESSAGE_UPDATE_SUCCESS;
                imbSave.Visible = false;
            }
        }
    }
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        ClickSearch();
    }

    #endregion  

    #region Datagrid SCDetail Mode Tabel
    protected void dtgSCTabel_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemIndex >= 0)
        {
            GetAllowanceType();
            Label lblAllowance1 = ((Label)e.Item.FindControl("lblAllowType1"));
            Label lblAllowance2 = ((Label)e.Item.FindControl("lblAllowType2"));
            Label lblAllowance3 = ((Label)e.Item.FindControl("lblAllowType3"));
            Label lblAllowance4 = ((Label)e.Item.FindControl("lblAllowType4"));
            Label lblAllowance5 = ((Label)e.Item.FindControl("lblAllowType5"));
            Label lblAllowance6 = ((Label)e.Item.FindControl("lblAllowType6"));
            Label lblAllowance7 = ((Label)e.Item.FindControl("lblAllowType7"));
            Label lblAllowance8 = ((Label)e.Item.FindControl("lblAllowType8"));
            Label lblAllowance9 = ((Label)e.Item.FindControl("lblAllowType9"));

            Label lblIDAllowance1 = ((Label)e.Item.FindControl("lblIDAllowType1"));
            Label lblIDAllowance2 = ((Label)e.Item.FindControl("lblIDAllowType2"));
            Label lblIDAllowance3 = ((Label)e.Item.FindControl("lblIDAllowType3"));
            Label lblIDAllowance4 = ((Label)e.Item.FindControl("lblIDAllowType4"));
            Label lblIDAllowance5 = ((Label)e.Item.FindControl("lblIDAllowType5"));
            Label lblIDAllowance6 = ((Label)e.Item.FindControl("lblIDAllowType6"));
            Label lblIDAllowance7 = ((Label)e.Item.FindControl("lblIDAllowType7"));
            Label lblIDAllowance8 = ((Label)e.Item.FindControl("lblIDAllowType8"));
            Label lblIDAllowance9 = ((Label)e.Item.FindControl("lblIDAllowType9"));

            lblAllowance1.Text = mlDT_LISTALLOWANCETYPE.Rows[0]["allow_typename"].ToString();
            lblAllowance2.Text = mlDT_LISTALLOWANCETYPE.Rows[1]["allow_typename"].ToString();
            lblAllowance3.Text = mlDT_LISTALLOWANCETYPE.Rows[2]["allow_typename"].ToString();
            lblAllowance4.Text = mlDT_LISTALLOWANCETYPE.Rows[3]["allow_typename"].ToString();
            lblAllowance5.Text = mlDT_LISTALLOWANCETYPE.Rows[4]["allow_typename"].ToString();
            lblAllowance6.Text = mlDT_LISTALLOWANCETYPE.Rows[5]["allow_typename"].ToString();
            lblAllowance7.Text = mlDT_LISTALLOWANCETYPE.Rows[6]["allow_typename"].ToString();
            lblAllowance8.Text = mlDT_LISTALLOWANCETYPE.Rows[7]["allow_typename"].ToString();
            //lblAllowance9.Text = mlDT_LISTALLOWANCETYPE.Rows[8]["allow_typename"].ToString();

            lblIDAllowance1.Text = mlDT_LISTALLOWANCETYPE.Rows[0]["allow_typeid"].ToString();
            lblIDAllowance2.Text = mlDT_LISTALLOWANCETYPE.Rows[1]["allow_typeid"].ToString();
            lblIDAllowance3.Text = mlDT_LISTALLOWANCETYPE.Rows[2]["allow_typeid"].ToString();
            lblIDAllowance4.Text = mlDT_LISTALLOWANCETYPE.Rows[3]["allow_typeid"].ToString();
            lblIDAllowance5.Text = mlDT_LISTALLOWANCETYPE.Rows[4]["allow_typeid"].ToString();
            lblIDAllowance6.Text = mlDT_LISTALLOWANCETYPE.Rows[5]["allow_typeid"].ToString();
            lblIDAllowance7.Text = mlDT_LISTALLOWANCETYPE.Rows[6]["allow_typeid"].ToString();
            lblIDAllowance8.Text = mlDT_LISTALLOWANCETYPE.Rows[7]["allow_typeid"].ToString();
            //lblIDAllowance9.Text = mlDT_LISTALLOWANCETYPE.Rows[8]["allow_typeid"].ToString();

            if(FlagAction == "EDIT")
            {
                String ucAmount = "";
                for (int x = 0; x < mlDT_LISTDATA.Rows.Count; x++)
                {
                    ucAmount = mlDT_LISTDATA.Rows[x]["allow_amount1"].ToString();
                    if (((Label)e.Item.FindControl("lblNIK")).Text == mlDT_LISTDATA.Rows[x]["nik"].ToString())
                    {
                        switch (mlDT_LISTDATA.Rows[x]["allow_typeid1"].ToString())
                        {
                            case "AL007":
                                ((ucInputNumber)e.Item.FindControl("ucAmountAllowType1")).Text = ucAmount.Replace(",00", "");
                                break;
                            case "AL004":
                                ((ucInputNumber)e.Item.FindControl("ucAmountAllowType2")).Text = ucAmount.Replace(",00", "");
                                break;
                            case "AL005":
                                ((ucInputNumber)e.Item.FindControl("ucAmountAllowType3")).Text = ucAmount.Replace(",00", "");
                                break;
                            case "AL001":
                                ((ucInputNumber)e.Item.FindControl("ucAmountAllowType4")).Text = ucAmount.Replace(",00", "");
                                break;
                            case "AL002":
                                ((ucInputNumber)e.Item.FindControl("ucAmountAllowType5")).Text = ucAmount.Replace(",00", "");
                                break;
                            case "AL003":
                                ((ucInputNumber)e.Item.FindControl("ucAmountAllowType6")).Text = ucAmount.Replace(",00", "");
                                break;
                            case "AL006":
                                ((ucInputNumber)e.Item.FindControl("ucAmountAllowType7")).Text = ucAmount.Replace(",00", "");
                                break;
                            case "AL008":
                                ((ucInputNumber)e.Item.FindControl("ucAmountAllowType8")).Text = ucAmount.Replace(",00", "");
                                break;
                        }
                    }
                }
            }
        }
    }
    protected void dtgSCTabel_SortCommand(object source, DataGridSortCommandEventArgs e)
    {

    }
    #endregion

    protected void ddlPayrolPeriodeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        PayrollPeriodeChange();
    }
    protected void PayrollPeriodeChange()
    {
        if (ddlPayrolPeriodeType.SelectedValue != "")
        {
            String DateAwal = "";
            String DateAkhir = "";
            if (ddlPayrolPeriodeType.SelectedItem.Text != "01-30" && ddlPayrolPeriodeType.SelectedValue != "")
            {
                DateAwal = ddlPayrolPeriodeType.SelectedItem.Text.Substring(0, 2);
                DateAkhir = ddlPayrolPeriodeType.SelectedItem.Text.Substring(ddlPayrolPeriodeType.SelectedItem.Text.Length - 2, 2);

                DateAkhir = DateAkhir + "/" + ddlMonth.SelectedValue + "/" + DateTime.Now.ToString("yyyy");
                DateAwal = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(DateAkhir).AddMonths(-1).AddDays(1));

                //DateAwal = DateTime.Now.AddMonths(-1).ToString("xx/MM/yyyy").Replace("xx", DateAwal);
                //DateAkhir = DateTime.Now.ToString("xx/MM/yyyy").Replace("xx", DateAkhir);
            }
            else
            {

                String Bulan = (int.Parse(ddlMonth.SelectedValue) + 1).ToString("00");
                DateAkhir = Convert.ToDateTime(DateTime.Now.Year.ToString() + "/" + Bulan + "/01").AddDays(-1).Day.ToString();

                //if(ddlMonth.SelectedValue == "02")
                //{
                //    DateAkhir = Convert.ToDateTime(DateTime.Now.Year.ToString() + "/03/01").AddDays(-1).Day.ToString();
                //}
                //else
                //{
                //    DateAkhir = ddlPayrolPeriodeType.SelectedItem.Text.Substring(ddlPayrolPeriodeType.SelectedItem.Text.Length - 2, 2);
                //}


                DateAwal = ddlPayrolPeriodeType.SelectedItem.Text.Substring(0, 2);

                DateAwal = DateAwal + "/" + ddlMonth.SelectedValue + "/" + DateTime.Now.ToString("yyyy");
                DateAkhir = DateAkhir + "/" + ddlMonth.SelectedValue + "/" + DateTime.Now.ToString("yyyy");

                //DateAwal = DateTime.Now.ToString("xx/MM/yyyy").Replace("xx", DateAwal);
                //DateAkhir = DateTime.Now.ToString("xx/MM/yyyy").Replace("xx", DateAkhir);
            }
            ucStartDate.dateValue = DateAwal;
            ucEndDate.dateValue = DateAkhir;
        }
    }
    protected void rblSCDetailChanged(object sender, EventArgs e)
    {

        switch(rblSCDetail.SelectedValue)
        {
            case "TBL":
                pnlDetailModeTabel.Visible = true;
                PnlDetailModeDDL.Visible = false;
                break;
            case "DDL":
                PnlDetailModeDDL.Visible = true;
                pnlDetailModeTabel.Visible = false;
                break;
        }

    }
}
