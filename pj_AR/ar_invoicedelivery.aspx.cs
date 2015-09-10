using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using Microsoft.Reporting.WebForms;

using IAS.Core.CSCode;
using IAS.Initialize;
using IAS.APP.DataAccess.AR;

//using IASClass;
//using ISS.App.Entities.pj_ad.administration;
//using ISS.App.Entities.pj_ar;
//using ISS.App.DataAccess.pj_ar;

public partial class pj_ar_ar_invoicedelivery : System.Web.UI.Page
{

    OleDbDataReader mlREADER;
    OleDbDataReader mlREADER2;

    VariableCore mlVarCore = new VariableCore();
    VariableAR_InvoiceDelivery oEnt = new VariableAR_InvoiceDelivery();
    FunctionAR_InvoiceDelivery oDA = new FunctionAR_InvoiceDelivery();

    //ucmGeneralSystem mlOBJGS = new ucmGeneralSystem();
    //ucmGeneralFunction mlOBJGF = new ucmGeneralFunction();
    //FunctionLocal mlOBJPJ = new FunctionLocal();
    //GeneralSetting GS = new GeneralSetting();
    //EntitiesMenu oEntMenu = new EntitiesMenu();
    //EntitiesInvoiceDelivery oEnt = new EntitiesInvoiceDelivery();
    //DataAccessInvoiceDelivery oDA = new DataAccessInvoiceDelivery();

    ModuleGeneralFunction oMGF = new ModuleGeneralFunction();
    ModuleFunctionLocal mlOBJPJ = new ModuleFunctionLocal();
    ReportDataSource ds = new ReportDataSource();

    


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

    protected string ReciptCode
    {
        get { return ((string)ViewState["ReciptCode"]); }
        set { ViewState["ReciptCode"] = value; }
    }
    protected string CustCode
    {
        get { return ((string)ViewState["CustCode"]); }
        set { ViewState["CustCode"] = value; }
    }
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
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"].ToString() + " INVOICE DELIVERY V2.02";
        mlTITLE.Text = "INVOICE DELIVERY V2.02";
        Session["mgDateTime"] = System.DateTime.Now;

        CekSession();

        oEnt.CompanyID = "ISSP3";
        oEnt.ModuleID = "PB";
       
       
        if (!IsPostBack)
        {
            PageSize = int.Parse(pagingReceiptCode.PageSize.ToString());
            currentPage = 1;
            cmdWhere = "";
            sortBy = "";
            ucProceedsDate.dateValue = DateTime.Now.ToString("dd/MM/yyyy");
            ucProceedsDate.isEnabled = false;
            ucSendingDate.dateValue = "01/01/1900";
            ucReturnDate.dateValue = "01/01/1900";
            ucDeliveredDate.dateValue = "01/01/1900";
            ucDoneDate.dateValue = "01/01/1900";

            FillDDL();
            SearchRecord();
            hpLookup.NavigateUrl = "javascript:OpenWinLookUpMessanger('" + hdnMessangerID.ClientID + "','" + txtMessangerID.ClientID + "','" + hdnMessangerName.ClientID + "','" + txtMessangerName.ClientID + "','AccMnt')";

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
        oEnt.ErrorMesssage = "";        
        oDA.GetCustomerWorksheet(oEnt);
        if(oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }
        //ddlCust.Items.Clear();
        //ddlCust.Items.Add(new ListItem("Choose One", ""));
        //for(int i =0;i<oEnt.dtListData.Rows.Count;i++)
        //{
        //    ddlCust.Items.Add(new ListItem(oEnt.dtListData.Rows[i]["DATA"].ToString(), oEnt.dtListData.Rows[i]["ID"].ToString()));
        //}

        oEnt.ErrorMesssage = "";
        oDA.GetMessangerDataList(oEnt);
        //ddlMessanger.Items.Clear();
        //ddlMessanger.Items.Add(new ListItem("Choose One", ""));
        //for (int i = 0; i < oEnt.dtListData.Rows.Count; i++)
        //{
        //    ddlMessanger.Items.Add(new ListItem(oEnt.dtListData.Rows[i]["DATA"].ToString(), oEnt.dtListData.Rows[i]["ID"].ToString()));
        //}

    }
    protected void dgListData_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        mlMESSAGE.Text = "";
        if(e.Item.ItemIndex >=0)
        {
            ReciptCode = ((HyperLink)e.Item.FindControl("hlreceiptcode")).Text;
            switch (e.CommandName)
            {
                case "edit":
                    FlagAction = "EDIT";
                    RetrieveDataDetail();
                    break;
                case "confirm":
                    FlagAction = "CONFIRM";
                    ucSendingDate.dateValue = DateTime.Now.ToString("dd/MM/yyyy");
                    lblStatus.Text = "Delivery";
                    lblReceiptCode.Text = ReciptCode;
                    SaveRecord();
                    break;
                case "printlampiran":
                    FlagAction = "LAMPIRAN";
                    lblReceiptCode.Text = ReciptCode;
                    RetrieveLampiran();
                    break;
                case "print":
                    FlagAction = "PRINT";
                    PrintRecord();
                    break;
            }
            
        }
    }
    protected void dgListData_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemIndex >=0)
        {
            ImageButton imbConfirm = ((ImageButton)e.Item.FindControl("imbConfirm"));
            imbConfirm.Attributes.Add("OnClick", "javascript:return confirm('Are you sure want to Confirm this Receipt Code ??')");

            String lblFlagConfirm = ((Label)e.Item.FindControl("lblflagconfirm")).Text;
            if(lblFlagConfirm == "1")
            {
                ((ImageButton)e.Item.FindControl("imbconfirm")).Visible = true;
                ((ImageButton)e.Item.FindControl("imbPrint")).Visible = false;

            }
            else
            {
                ((ImageButton)e.Item.FindControl("imbconfirm")).Visible = false;
                ((ImageButton)e.Item.FindControl("imbPrint")).Visible = true;
            }

            if (((Label)e.Item.FindControl("lblflaglampiran")).Text == "1")
            {
                ((ImageButton)e.Item.FindControl("imbPrintLampiran")).Visible = true;
            }
            else
            {
                ((ImageButton)e.Item.FindControl("imbPrintLampiran")).Visible = false;
            }

            if(((Label)e.Item.FindControl("lblReceiptStatus")).Text == "Done")
            {
                ((ImageButton)e.Item.FindControl("imbEdit")).Visible = false;
            }
            else
            {
                ((ImageButton)e.Item.FindControl("imbEdit")).Visible = true;
            }


            if(((Label)e.Item.FindControl("lblSendingDate")).Text == "01/01/1900")
            {
                ((Label)e.Item.FindControl("lblSendingDate")).Text = "-";
            }
            if (((Label)e.Item.FindControl("lbldelivereddate")).Text == "01/01/1900")
            {
                ((Label)e.Item.FindControl("lbldelivereddate")).Text = "-";
            }
            if (((Label)e.Item.FindControl("lblreturndate")).Text == "01/01/1900")
            {
                ((Label)e.Item.FindControl("lblreturndate")).Text = "-";
            }
            if (((Label)e.Item.FindControl("lbldonedate")).Text == "01/01/1900")
            {
                ((Label)e.Item.FindControl("lbldonedate")).Text = "-";
            }

            String ReceiptCode = ((HyperLink)e.Item.FindControl("hlReceiptCode")).Text;
            ((HyperLink)e.Item.FindControl("hlReceiptCode")).NavigateUrl = "javascript:OpenWinLookUp('" + ReceiptCode + "')";      
            //((HyperLink)e.Item.FindControl("hlReceiptCode")).NavigateUrl = "javascript:OpenWinLookUp('/pj_ar/ar_invoicedelivery_lookup.aspx?ReceiptCode='" + ReceiptCode + "'')";      
            
        }
    }
    protected void NavigationButtonClicked(usercontroller_ucPaging.NavigationButtonEventArgs e)
    {        
        retrievepaging();
    }
    protected void retrievepaging()
    {
        currentPage = pagingReceiptCode.currentPage;
        PageSize = int.Parse(pagingReceiptCode.PageSize.ToString());
        RetrieveData();
    }
    protected void RetrieveData()
    {
        oEnt.ErrorMesssage = "";
        oEnt.PageSize = PageSize;
        oEnt.CurrentPage = currentPage;
        oEnt.WhereCond = cmdWhere;
        oEnt.SortBy = sortBy;
        oDA.GetInvoiceDeliveryPaging(oEnt);
        if(oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }
        if(oEnt.dtListData.Rows.Count == 0)
        {
            mlMESSAGE.Text = ModuleSystemConstant.MESSAGE_DATA_NOT_FOUND;
            return;
        }
        dgListData.DataSource = oEnt.dtListData;
        dgListData.DataBind();

        totalRecords = oEnt.TotalRecord;

        pagingReceiptCode.PageSize = PageSize;
        pagingReceiptCode.currentPage = currentPage;
        pagingReceiptCode.totalRecords = Convert.ToInt64(totalRecords.ToString());
        pagingReceiptCode.PagingFooter(totalRecords, PageSize);
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
    protected void NewRecord()
    {
        FlagAction = "ADD";
        mlMESSAGE.Text = "";
        btSaveRecord.Visible = true;
        btNewRecord.Visible = false;
        btSearchRecord.Visible = false;

        pnlListReceiptCode.Visible = false;
        pnlSearch.Visible = false;
        pnlInputData.Visible = true;
    }
    protected void RetrieveDataDetail()
    {
        oEnt.ErrorMesssage = "";
        oEnt.ReceiptCode = ReciptCode;
        oDA.GetInvoiceDeliveryDetail(oEnt);
        if(oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }
        if(oEnt.dtListData.Rows.Count == 0)
        {
            mlMESSAGE.Text = ModuleSystemConstant.MESSAGE_DATA_NOT_FOUND;
            return;
        }

        for (int i = 0; i < oEnt.dtListData.Rows.Count; i++)
        {
            lblReceiptCode.Text = oEnt.dtListData.Rows[i]["receiptcode"].ToString();
            txtNoResi.Text = oEnt.dtListData.Rows[i]["noresi"].ToString();
            txtPIC.Text = oEnt.dtListData.Rows[i]["custpenerima"].ToString();
            lblStatus.Text = oEnt.dtListData.Rows[i]["receiptstatus"].ToString();
            ucProceedsDate.dateValue = oEnt.dtListData.Rows[i]["proceedsdate"].ToString();
            ucSendingDate.dateValue = oEnt.dtListData.Rows[i]["sendingdate"].ToString();
            ucDeliveredDate.dateValue = oEnt.dtListData.Rows[i]["delivereddate"].ToString();
            ucReturnDate.dateValue = oEnt.dtListData.Rows[i]["returneddate"].ToString();
            ucDoneDate.dateValue = oEnt.dtListData.Rows[i]["donedate"].ToString();
            //GetDDL(ddlMessanger, oEnt.dtListData.Rows[i]["messangerid"].ToString());
            //GetDDL(ddlCust, oEnt.dtListData.Rows[i]["custcode"].ToString());
            txtMessangerID.Text = oEnt.dtListData.Rows[i]["messangerid"].ToString();
            txtMessangerName.Text = oEnt.dtListData.Rows[i]["messangername"].ToString(); 

            ucCustomer.CustomerID = oEnt.dtListData.Rows[i]["custcode"].ToString();
            ucCustomer.CustomerName = oEnt.dtListData.Rows[i]["CustName"].ToString();
            if (oEnt.dtListData.Rows[i]["isdoclampiran"].ToString() == "1")
            {
                chkDocLampiran.Checked = true;
            }
            else
            {
                chkDocLampiran.Checked = false;
            }
        }
        ucProceedsDate.isEnabled = false;

        //if (lblStatus.Text == "Proceeds")
        //{
        //    ucProceedsDate.isEnabled = false;
        //    ucSendingDate.isEnabled = true;
        //    if (ucSendingDate.dateValue != "01/01/1900")
        //    {
        //        ucDeliveredDate.isEnabled = true;
        //    }
        //    if(ucReturnDate.dateValue != "01/01/1900")
        //    {
        //        ucReturnDate.isEnabled = true;
        //    }
        //}
        if(lblStatus.Text == "Delivery")
        {
            ucDeliveredDate.isEnabled = true;
            ucReturnDate.isEnabled = false;
        }


        dgInvoice.DataSource = oEnt.dtListData;
        dgInvoice.DataBind();

        btSaveRecord.Visible = true;
        btNewRecord.Visible = false;
        btSearchRecord.Visible = false;

        pnlInputData.Visible = true;
        pnlInvoice.Visible = true;
        pnlListReceiptCode.Visible = false;
        pnlSearch.Visible = false;
    }
    protected void SaveRecord()
    {
        DataTable dtInvoice = new DataTable();
        oEnt.IsDocumentLampiran = "1";
        if (FlagAction == "ADD" || FlagAction == "EDIT")
        {
            dtInvoice.Columns.Add("InvoiceNo");
            dtInvoice.Columns.Add("invdate");
            dtInvoice.Columns.Add("Branch");
            dtInvoice.Columns.Add("SiteCard");
            dtInvoice.Columns.Add("CustCode");
            dtInvoice.Columns.Add("InvoiceAmount");
            dtInvoice.Columns.Add("InvoiceStatus");
            dtInvoice.Columns.Add("CancelDescription");

            for (int i = 0; i < dgInvoice.Items.Count; i++)
            {
                if (((CheckBox)dgInvoice.Items[i].FindControl("chkSelect")).Checked == true)
                {
                    DataRow drInvoice = dtInvoice.NewRow();
                    drInvoice["InvoiceNo"] = ((Label)dgInvoice.Items[i].FindControl("lblinvoiceno")).Text;
                    drInvoice["invdate"] = ((Label)dgInvoice.Items[i].FindControl("lblinvdate")).Text;
                    drInvoice["Branch"] = ((Label)dgInvoice.Items[i].FindControl("lblbranch")).Text;
                    drInvoice["SiteCard"] = ((Label)dgInvoice.Items[i].FindControl("lblsitecard")).Text;
                    drInvoice["CustCode"] = ((Label)dgInvoice.Items[i].FindControl("lblcustcode")).Text;
                    //drInvoice["CustName"] = ((Label)dgInvoice.Items[i].FindControl("lblcustname")).Text;
                    drInvoice["InvoiceAmount"] = ((Label)dgInvoice.Items[i].FindControl("lblTotalInvoice")).Text.Replace(",00","") ;
                    drInvoice["InvoiceStatus"] = ((Label)dgInvoice.Items[i].FindControl("lblInvoiceStatus")).Text;
                    drInvoice["CancelDescription"] = ((TextBox)dgInvoice.Items[i].FindControl("txtDescription")).Text;

                    dtInvoice.Rows.Add(drInvoice);
                }
            }

            if (dtInvoice.Rows.Count <= 0)
            {
                mlMESSAGE.Text = "Sorry...Data Can't Save...Please Select Invoice...";
                return;
            }
            if (chkDocLampiran.Checked == true)
            {
                oEnt.IsDocumentLampiran = "1";
            }
            else
            {
                oEnt.IsDocumentLampiran = "0";
            }
        }
        if(FlagAction == "LAMPIRAN")
        {
            dtInvoice.Columns.Add("DocTypeID");
            dtInvoice.Columns.Add("DocTypeName");

            for(int i = 0 ; i<dgLampiran.Items.Count ; i++)
            {
                if (((CheckBox)dgLampiran.Items[i].FindControl("chkSelect")).Checked == true)
                {
                    DataRow drLampiran = dtInvoice.NewRow();
                    drLampiran["DocTypeID"] = ((Label)dgLampiran.Items[i].FindControl("lbldoctypeid")).Text;
                    drLampiran["DocTypeName"] = ((Label)dgLampiran.Items[i].FindControl("lbldoctypename")).Text;
                    dtInvoice.Rows.Add(drLampiran);
                }
            }

            if (dtInvoice.Rows.Count <= 0)
            {
                mlMESSAGE.Text = "Sorry...Data Can't Save...Please Select Document Lampiran...";
                return;
            }

        }

        oEnt.ErrorMesssage = "";
        oEnt.FlagAction = FlagAction;
        oEnt.ReceiptCode = lblReceiptCode.Text;
        oEnt.Entity_id = ddlEntity.SelectedValue.ToString();
        oEnt.CustCode = ucCustomer.CustomerID;    // ddlCust.SelectedValue.ToString();
        oEnt.MessangerID = txtMessangerID.Text;   //ddlMessanger.SelectedValue.ToString();
        oEnt.ProceedsDate = ucProceedsDate.dateValue;
        oEnt.SendingDate = ucSendingDate.dateValue;
        oEnt.DeliverDate = ucDeliveredDate.dateValue;
        oEnt.Status = lblStatus.Text;        
        if(FlagAction == "ADD")
        {
            oEnt.Status = "Proceed";
        }        
        if(ucDeliveredDate.dateValue != "01/01/1900")
        {
            oEnt.Status = "Done";
            ucDoneDate.dateValue = DateTime.Now.ToString("dd/MM/yyyy");
            ucReturnDate.dateValue = DateTime.Now.ToString("dd/MM/yyyy"); 
        }
        oEnt.ReturnDate = ucReturnDate.dateValue;
        oEnt.DoneDate = ucDoneDate.dateValue;
        //if (ucDoneDate.dateValue != "01/01/1900")
        //{
        //    oEnt.Status = "Done";
        //}
        oEnt.PICCustomer = txtPIC.Text;
        oEnt.NoResi = txtNoResi.Text;
        oEnt.dtListDetail = dtInvoice;
        oEnt.LoginID = Session["mgUSERID"].ToString();        
        oDA.SaveInvoiceDelivery(oEnt);
        if(oEnt.ErrorMesssage != "")
        {
            mlMESSAGE.Text = oEnt.ErrorMesssage;
            return;
        }
        mlMESSAGE.Text = ModuleSystemConstant.MESSAGE_INSERT_SUCCESS;
        SearchRecord();

        pnlInputData.Visible = false;
        pnlLampiran.Visible = false;
        pnlListReceiptCode.Visible = true;
        pnlSearch.Visible = false;
        btSaveRecord.Visible = false;
        btNewRecord.Visible = true;
        btSearchRecord.Visible = true;
    }
    protected void PrintRecord()
    {
        pnlPrintPreview.Visible = true;
        pnlLampiran.Visible = false;
        pnlListReceiptCode.Visible = false;
        pnlInputData.Visible = false;

        String FileName = "rptINV_InvoiceDeliveryReceiptCode.rdlc";
        rptViewer.LocalReport.ReportPath = ConfigurationManager.AppSettings["ReportPath"] + FileName;

        oEnt.ErrorMesssage = "";
        oEnt.ReceiptCode = ReciptCode;
        oDA.PrintInvoiceDelivery(oEnt);

        ds.Name = "dsInvoiceDelivery";
        ds.Value = oEnt.dtListData;
        rptViewer.LocalReport.DataSources.Add(ds);

        //Untuk Passing Parameter
        List<ReportParameter> parameters = new List<ReportParameter>();
        parameters.Add(new ReportParameter("inv_receiptcode", this.ReciptCode));

        rptViewer.LocalReport.SetParameters(parameters);
        rptViewer.LocalReport.Refresh();

        ////untuk Export to PDF (Open)
        string mimeType;
        string encoding;
        string fileNameExtension;
        Warning[] warnings;
        string[] streamids;
        byte[] exportBytes = rptViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);

       
        Response.Buffer = true;
        Response.Clear();
        Response.ContentType = mimeType;
        Response.AddHeader("Content-Length", exportBytes.Length.ToString());
        Response.AddHeader("Content-Disposition", "attachment; filename=" + "PrintPDF." + fileNameExtension);
        Response.BinaryWrite(exportBytes);
        Response.Flush();
        Response.End();
        Response.Close();
    }
    protected void ResetRecord()
    {
        Response.Redirect("ar_invoicedelivery.aspx");
    }
    protected void GetDDL(DropDownList ddl,String Data)
    {
        for(int i = 0;i<ddl.Items.Count;i++)
        {
            if(ddl.Items[i].Value == Data)
            {
                ddl.SelectedIndex = i;
                break;
            }
        }
    }
    protected void btNewRecord_Click(object sender, ImageClickEventArgs e)
    {
        NewRecord();
    }
    protected void btSearchRecord_Click(object sender, ImageClickEventArgs e)
    {
        pnlSearch.Visible = true;
    }
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchRecord();
    }
    protected void btCancelOperation_Click(object sender, ImageClickEventArgs e)
    {
        ResetRecord();
    }

    protected void btSaveRecord_Click(object sender, ImageClickEventArgs e)
    {
        SaveRecord();

    }

    ///* List Invoice Area */
    //protected void ddlCust_IndexChanged(object sender, EventArgs e)
    //{
    //    CustCode = ddlCust.SelectedValue.ToString();
    //    RetrieveInvoice();
    //    pnlInvoice.Visible=true;
    //}
    protected void RetrieveInvoice()
    {
        oEnt.ErrorMesssage = "";
        oEnt.CustCode = CustCode;
        oDA.GetInvoiceWorksheet(oEnt);
        dgInvoice.DataSource = oEnt.dtListData;
        dgInvoice.DataBind();
       
    }
    protected void dgInvoice_ItemCommand(object source, DataGridCommandEventArgs e)
    {

    }
    protected void dgInvoice_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemIndex >= 0)
        {
            if(FlagAction == "EDIT")
            {
                if (((Label)e.Item.FindControl("lblFlagCheck")).Text == "1")
                {
                    ((CheckBox)e.Item.FindControl("chkSelect")).Checked = true;
                }
                else
                {
                    ((CheckBox)e.Item.FindControl("chkSelect")).Checked = false;
                }
            }
        }
    }   
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkAll = sender as CheckBox;

        if(ChkAll.Checked)
        {
            for(int i =0 ; i<dgInvoice.Items.Count;i++)
            {
                CheckBox chkItem = ((CheckBox) dgInvoice.Items[i].FindControl("chkSelect"));
                chkItem.Checked=true;
            }
        }
        else
        {
            for (int i = 0; i < dgInvoice.Items.Count; i++)
            {
                CheckBox chkItem = ((CheckBox)dgInvoice.Items[i].FindControl("chkSelect"));
                chkItem.Checked = false;
            }
        }

        
    }

    ///* List Lampiran Area */
    protected void RetrieveLampiran()
    {
        oEnt.ErrorMesssage = "";
        oEnt.ReceiptCode = ReciptCode;
        oDA.GetDocumentLampiran(oEnt);
        dgLampiran.DataSource = oEnt.dtListData;
        dgLampiran.DataBind();

        btSaveRecord.Visible = true;
        btNewRecord.Visible = false;
        btPrintRecord.Visible = true;
        btSearchRecord.Visible = false;

        pnlInputData.Visible = false;
        pnlListReceiptCode.Visible = false;
        pnlSearch.Visible = false;
        pnlLampiran.Visible = true;
    }
    protected void dgLampiran_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemIndex >= 0)
        {
            if(((Label)e.Item.FindControl("lblFlagCheck")).Text == "1" )
            {
                ((CheckBox)e.Item.FindControl("ChkSelect")).Checked = true;
            }
            else
            {
                ((CheckBox)e.Item.FindControl("ChkSelect")).Checked = false;
            }
        }
    }
    protected void chkSelectAllLampiran_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkAll = sender as CheckBox;

        if (ChkAll.Checked)
        {
            for (int i = 0; i < dgLampiran.Items.Count; i++)
            {
                CheckBox chkItem = ((CheckBox)dgLampiran.Items[i].FindControl("chkSelect"));
                chkItem.Checked = true;
            }
        }
        else
        {
            for (int i = 0; i < dgLampiran.Items.Count; i++)
            {
                CheckBox chkItem = ((CheckBox)dgLampiran.Items[i].FindControl("chkSelect"));
                chkItem.Checked = false;
            }
        }
    }

    protected void btPrintRecord_Click(object sender, ImageClickEventArgs e)
    {
        pnlPrintPreview.Visible = true;
        pnlLampiran.Visible = false;

        String FileName = "rptINV_InvoiceDeliveryLampiran.rdlc";
        rptViewer.LocalReport.ReportPath = ConfigurationManager.AppSettings["ReportPath"] + FileName;
        
        oEnt.ErrorMesssage = "";
        oEnt.ReceiptCode = ReciptCode;
        oDA.PrintInvoiceDeliveryLampiran(oEnt);


        ds.Name = "dsINV_Lampiran";
        ds.Value = oEnt.dtListData;
        rptViewer.LocalReport.DataSources.Add(ds);

        //Untuk Passing Parameter
        List<ReportParameter> parameters = new List<ReportParameter>();
        parameters.Add(new ReportParameter("inv_receiptcode", this.ReciptCode));       

        rptViewer.LocalReport.SetParameters(parameters);
        rptViewer.LocalReport.Refresh();
    }
    protected void imbRefreshINV_Click(object sender, ImageClickEventArgs e)
    {
       
        CustCode = ucCustomer.CustomerID;
        RetrieveInvoice();
        pnlInvoice.Visible = true;
      
    }
    protected void imbBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ar_invoicedelivery.aspx");
    }
}