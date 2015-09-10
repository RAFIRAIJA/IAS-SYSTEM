using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;

public partial class Worksheet : System.Web.UI.Page
{
    IASClass.ucmGeneralSystem mlOBJGS = new IASClass.ucmGeneralSystem();
    IASClass.ucmGeneralFunction mlOBJGF = new IASClass.ucmGeneralFunction();
    FunctionLocal mlOBJPJ = new FunctionLocal();

    System.Data.OleDb.OleDbDataReader mlREADER = null;
    string mlSQL = null;
    System.Data.OleDb.OleDbDataReader mlREADER2 = null;
    string mlSql_2 = null;
    string mlKEY = null;
    string mlKEY2 = null;
    string mlKEY3 = null;
    string mlRECORDSTATUS = null;
    string mlSPTYPE = null;
    string mlFUNCTIONPARAMETER = null;
    string ddENTITY = "";

    DataTable mlDATATABLE = null;
    DataRow mlDATAROW = null;
    DataColumn mlDCOL = new DataColumn();
    int mlI = 0;

    string mlSQLTEMP = null;
    System.Data.OleDb.OleDbDataReader mlRSTEMP = null;
    string mlCURRENTDATE = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString();
    string mlCURRENTBVMONTH = DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString();
    bool mlSHOWTOTAL = false;
    bool mlSHOWPRICE = false;
    string mlUSERLEVEL = null;

    string mlCOMPANYTABLENAME = null;
    string mlCOMPANYID = null;
    string mlPARAM_COMPANY = null;

    #region " User Defined Function "

        public void importdatafromexcel(string excelfilepath)
        {
            //declare variables - edit these based on your particular situation
            //string ssqltable = "tdatamigrationtable";
            // make sure your sheet name is correct, here sheet name is sheet1, so you can change your sheet name if have different
            string myexceldataquery = "SELECT [Invoice No.], [Cust. Code], [Cust. name], [Invoice Date], [Branch], [Invoice],	[Site Card], [Product Offering], [OCM], [FCM], [collector], [code Messenger] FROM [Data]";
            try
            {

                //create our connection strings
                string sexcelconnectionstring = @"provider=microsoft.jet.oledb.4.0;data source=" + excelfilepath + ";extended properties=" + "\"excel 8.0;hdr=yes;\"";
                //string ssqlconnectionstring = "server=mydatabaseservername;user id=dbuserid;password=dbuserpassword;database=databasename;connection reset=false";
                //execute a query to erase any previous data from our destination table
                //string sclearsql = "delete from " + ssqltable;
                //OleDbConnection sqlconn = new OleDbConnection(ssqlconnectionstring);
                //OleDbCommand sqlcmd = new OleDbCommand(sclearsql, sqlconn);
                //sqlconn.Open();
                //sqlcmd.ExecuteNonQuery();
                //sqlconn.Close();
                //series of commands to bulk copy data from the excel file into our sql table
                OleDbConnection oledbconn = new OleDbConnection(sexcelconnectionstring);
                OleDbCommand oledbcmd = new OleDbCommand(myexceldataquery, oledbconn);
                oledbconn.Open();
                OleDbDataReader dr = oledbcmd.ExecuteReader();
                if (dr.HasRows)
                { 

                }

                //SqlBulkCopy bulkcopy = new SqlBulkCopy(ssqlconnectionstring);
                //bulkcopy.DestinationTableName = ssqltable;
                //while (dr.Read())
                //{
                //    bulkcopy.WriteToServer(dr);
                //}
     
                oledbconn.Close();
            }
            catch //(Exception ex)
            {
                //handle exception
            }
        }

        private void DisableCancel()
        {
            btNewRecord.Visible = true;
            btSaveRecord.Visible = false;
            mlPNLGRID.Visible = true;
            
        }

        public void RetrieveFieldsDetail()
        {
            mlSql_2 = "SELECT InvNo, InvCustCode, InvCustName, InvDate, InvBranch, InvAmount, InvStatus, InvReceiptCode, InvProceedsDate, InvDeliveredDate, InvReturnedDate, InvDoneDate, InvCustPenerima, InvMessName, InvCodeMess, UserName, UserID FROM INV_DELIVERY ORDER BY InvDate Desc";
            OleDbConnection con = new OleDbConnection(mlOBJGS.mgCONNECTIONSTRING);
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand mlDBCMD = new OleDbCommand(mlSql_2, con);
            OleDbDataAdapter mlDA = new OleDbDataAdapter(mlDBCMD);
            mlDA.Fill(mlDATATABLE);
            mlDA.Dispose();
            con.Close();
            con.Dispose();
            //mlREADER2 = mlOBJGS.DbRecordset(mlSql_2, "PB", "ISSP3");
            mlDGWORKSHEET.DataSource = mlDATATABLE; //mlREADER2;
            mlDGWORKSHEET.DataBind();
        }

        public System.Data.DataTable createTable(System.Data.DataTable mpTBLFIELD)
        {
            mlSQLTEMP = "SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'INV_DELIVERY'";
            mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", mlCOMPANYID);
            if (mlRSTEMP.HasRows )
            {
                //mlDATAROW = mpTBLFIELD.NewRow();

                while (mlRSTEMP.Read())
                {
                    if (mlRSTEMP["DATA_TYPE"].ToString() == "varchar" || mlRSTEMP["DATA_TYPE"].ToString() == "nvarchar" || mlRSTEMP["DATA_TYPE"].ToString() == "char" || mlRSTEMP["DATA_TYPE"].ToString() == "nchar" || mlRSTEMP["DATA_TYPE"].ToString() == "ntext")
                    {
                        mlDCOL = new DataColumn(mlRSTEMP["COLUMN_NAME"].ToString(), typeof(System.String));
                        mlDCOL.DefaultValue = "";
                    }
                    else if (mlRSTEMP["DATA_TYPE"].ToString() == "decimal" || mlRSTEMP["DATA_TYPE"].ToString() == "numeric" || mlRSTEMP["DATA_TYPE"].ToString() == "float")
                    {
                        mlDCOL = new DataColumn(mlRSTEMP["COLUMN_NAME"].ToString(), typeof(System.Decimal));
                        mlDCOL.DefaultValue = 0;
                    }
                    else if (mlRSTEMP["DATA_TYPE"].ToString() == "bigint" || mlRSTEMP["DATA_TYPE"].ToString() == "int" || mlRSTEMP["DATA_TYPE"].ToString() == "smallint")
                    {
                        mlDCOL = new DataColumn(mlRSTEMP["COLUMN_NAME"].ToString(), typeof(System.Int64));
                        mlDCOL.DefaultValue = 0;
                    }
                    else if (mlRSTEMP["DATA_TYPE"].ToString() == "tinyint")
                    {
                        mlDCOL = new DataColumn(mlRSTEMP["COLUMN_NAME"].ToString(), typeof(System.Boolean));
                        mlDCOL.DefaultValue = 0;
                    }
                    else if (mlRSTEMP["DATA_TYPE"].ToString() == "datetime" || mlRSTEMP["DATA_TYPE"].ToString() == "smalldatetime" || mlRSTEMP["DATA_TYPE"].ToString() == "timestamp")
                    {
                        mlDCOL = new DataColumn(mlRSTEMP["COLUMN_NAME"].ToString(), typeof(System.DateTime));
                        mlDCOL.DefaultValue = DateTime.Now;
                    }

                    if (!string.IsNullOrEmpty(mlRSTEMP["DATA_TYPE"].ToString()))
                    {
                        mpTBLFIELD.Columns.Add(mlDCOL);
                        //mlDATAROW[mlRSTEMP["COLUMN_NAME"].ToString()] = mlDCOL.DefaultValue;
                    }
                }

                //mpTBLFIELD.Rows.Add(mlDATAROW);
            }

            return mpTBLFIELD;
        }

    #endregion

    protected void Page_Load(object sender, System.EventArgs e)
    {
        mlTITLE.Text = "Monitoring Invoice Distribution";
        this.Title = System.Configuration.ConfigurationManager.AppSettings["mgTITLE"] + "Monitoring Invoice Distribution";

        mlOBJGS.Main();
        if (mlOBJGS.ValidateExpiredDate() == true)
        {
            return;
        }
        if (string.IsNullOrEmpty(Session["mgACTIVECOMPANY"].ToString()))
            Session["mgACTIVECOMPANY"] = mlOBJGS.mgCOMPANYDEFAULT;

        mlOBJGS.mgACTIVECOMPANY = Session["mgACTIVECOMPANY"].ToString();


        mlPARAM_COMPANY = Request["mpFP"].ToString();
        if (string.IsNullOrEmpty(mlPARAM_COMPANY))
            mlPARAM_COMPANY = "1";
        mlFUNCTIONPARAMETER = mlPARAM_COMPANY;

        switch (mlPARAM_COMPANY)
        {
            case "":
            case "1":
                //ddENTITY.Items.Clear();
                ddENTITY = "ISS";
                //ddENTITY.Items.Add("ISS");
                mlTITLE.Text = mlTITLE.Text + " (ISS)";
                break;
            case "2":
                //ddENTITY.Items.Clear();
                ddENTITY = "IFS";
                //ddENTITY.Items.Add("IFS");
                mlTITLE.Text = mlTITLE.Text + " (IFS - Facility Services)";
                break;
            case "3":
                //ddENTITY.Items.Clear();
                ddENTITY = "IPM";
                //ddENTITY.Items.Add("IPM");
                mlTITLE.Text = mlTITLE.Text + " (IPM - Parking Management)";
                break;
            case "4":
                //ddENTITY.Items.Clear();
                ddENTITY = "ICS";
                //ddENTITY.Items.Add("ICS");
                mlTITLE.Text = mlTITLE.Text + " (ICS - Catering Services)";
                break;
        }

        mlCOMPANYTABLENAME = "ISS Servisystem, PT$";
        mlCOMPANYID = mlCOMPANYID;
        switch (ddENTITY)
        {
            case "ISS":
                mlCOMPANYTABLENAME = "ISS Servisystem, PT$";
                //mlCOMPANYID = "ISSN3";
                mlCOMPANYID = "ISSP3";
                break;
            case "IPM":
                mlCOMPANYTABLENAME = "ISS Parking Management$";
                mlCOMPANYID = "IPM3";
                break;
            case "ICS":
                mlCOMPANYTABLENAME = "ISS CATERING SERVICES$";
                mlCOMPANYTABLENAME = "ISS Catering Service 5SP1$";
                mlCOMPANYID = "ICS5";
                break;
            case "IFS":
                mlCOMPANYTABLENAME = "INTEGRATED FACILITY SERVICES$";
                mlCOMPANYID = "IFS3";
                break;
        }



        if (Page.IsPostBack == false)
        {
            //LoadComboData2();
            //tr2.Visible = false;
            //pnSEARCHSITECARD.Visible = false;
            //pnSEARCHITEMKEY.Visible = false;
            //tbTABLE1.Visible = false;
            DisableCancel();
            mlSHOWTOTAL = false;
            mlSHOWPRICE = false;
            //btITEMKEYADD.Visible = false;
            mlOBJGS.XM_UserLog(Session["mgUSERID"].ToString(), Session["mgNAME"].ToString(), "monitoring_delivery", "Monitoring Delivery", "");
        }
        else
        {
            RetrieveFieldsDetail();
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (mlCOMPANYID == null || mlCOMPANYID == "") { mlCOMPANYID = "ISSP3"; }
        mlDATATABLE = new DataTable();
        mlDATATABLE = this.createTable(mlDATATABLE);
        if (FileUpload1.PostedFile.ContentType == "application/vnd.ms-excel" ||
        FileUpload1.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            try
            {
                string fileName = Server.MapPath("~/file/upload_deliv") + "\\" + FileUpload1.PostedFile.FileName;//Path.Combine(Server.MapPath("~/file/upload_deliv"), Guid.NewGuid().ToString() + Path.GetExtension(FileUpload1.PostedFile.FileName));
                FileUpload1.PostedFile.SaveAs(fileName);
                string conString = "";
                string ext = Path.GetExtension(FileUpload1.PostedFile.FileName);
                if (ext.ToLower() == ".xls")
                {
                    conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\""; ;
                }
                else if (ext.ToLower() == ".xlsx")
                {
                    conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                }
                //Invoice No#, Cust# Code, Cust# name, Invoice Date, Branch, Invoice, Site Card, Product Offering, OCM, FCM, collector, code Messenger
                mlSQLTEMP = "SELECT * FROM [Data$]";
                OleDbConnection con = new OleDbConnection(conString);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                OleDbCommand mlDBCMD = new OleDbCommand(mlSQLTEMP, con);
                OleDbDataAdapter mlDA = new OleDbDataAdapter(mlDBCMD);
                DataSet ds = new DataSet();
                mlDA.Fill(ds);
                mlDA.Dispose();
                con.Close();
                con.Dispose();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (!string.IsNullOrEmpty(dr[0].ToString()))
                    {
                        mlSQL = "INSERT INTO INV_DELIVERY (InvNo, InvCustCode, InvCustName, InvDate, InvBranch, InvAmount, InvSiteCard, " +
                                "InvProdOffer, InvOCM, InvFCM, InvCollector, InvCodeMess, InvMessName, InvStatus, InvDesc, InvReceiptCode, " +
                                "InvProceedsDate, InvDeliveredDate, InvReturnedDate, InvDoneDate, InvCustPenerima, UserID, UserName, [Disabled]) " +
                                "VALUES ('" + dr[0].ToString() + "', '" + dr[1].ToString() + "', " +
                                "'" + dr[2].ToString() + "', '" + dr[3].ToString() + "', '" + dr[4].ToString() + "', " +
                                "'" + Convert.ToDecimal(dr[5]) + "', '" + dr[6].ToString() + "', " +
                                "'" + dr[7].ToString() + "', '" + dr[8].ToString() + "', " +
                                "'" + dr[9].ToString() + "', '" + dr[10].ToString() + "', '" + dr[11].ToString() + "', " +
                                "'', 'PROCEEDS', '', '', GETDATE(), null, null, null, '', '" + Session["mgUSERID"].ToString() + "', '" + Session["mgNAME"].ToString() + "', '0')";

                        //mlSQL = "INSERT INTO INV_DELIVERY (InvNo, InvCustCode, InvCustName, InvDate, InvBranch, InvAmount, InvSiteCard, " +
                        //        "InvProdOffer, InvOCM, InvFCM, InvCollector, InvCodeMess, InvMessName, InvStatus, InvDesc, InvReceiptCode, " +
                        //        "InvProceedsDate, InvDeliveredDate, InvReturnedDate, InvDoneDate, InvCustPenerima, UserID, UserName, [Disabled]) " +
                        //        "VALUES ('" + dr["Invoice No#"].ToString() + "', '" + dr["Cust# Code"].ToString() + "', " +
                        //        "'" + dr["Cust# name"].ToString() + "', '" + dr["Invoice Date"].ToString() + "', '" + dr["Branch"].ToString() + "', " +
                        //        "'" + Convert.ToDecimal(dr["Invoice"]) + "', '" + dr["Site Card"].ToString() + "', " +
                        //        "'" + dr["Product Offering"].ToString() + "', '" + dr["OCM"].ToString() + "', " +
                        //        "'" + dr["FCM"].ToString() + "', '" + dr["collector"].ToString() + "', '" + dr["code Messenger"].ToString() + "', " +
                        //        "'', 'PROCEEDS', '', '', GETDATE(), null, null, null, '', '" + Session["mgUSERID"].ToString() + "', '" + Session["mgNAME"].ToString() + "', '0')";

                        mlOBJGS.ExecuteQuery(mlSQL, "PB", mlCOMPANYID, false, null);
                    }
                }

                //OleDbDataReader dr = oledbcmd.ExecuteReader();
                //if (dr.HasRows)
                //{
                //    while (dr.Read())
                //    {
                //        //mlDATAROW = mlDATATABLE.NewRow();

                //        //mlDATAROW["InvNo"] = dr["[Invoice No.]"].ToString();
                //        //mlDATAROW["InvCustCode"] = dr["[Cust. Code]"].ToString();
                //        //mlDATAROW["InvCustName"] = dr["[Cust. name]"].ToString();
                //        //mlDATAROW["InvDate"] = dr["[Invoice Date]"].ToString();
                //        //mlDATAROW["InvBranch"] = dr["[Branch]"].ToString();
                //        //mlDATAROW["InvAmount"] = Convert.ToDecimal(dr["[Invoice]"]);
                //        //mlDATAROW["InvSiteCard"] = dr["[Site Card]"].ToString();
                //        //mlDATAROW["InvProdOffer"] = dr["[Product Offering]"].ToString();
                //        //mlDATAROW["InvOCM"] = dr["[OCM]"].ToString();
                //        //mlDATAROW["InvFCM"] = dr["[FCM]"].ToString();
                //        //mlDATAROW["InvCollector"] = dr["[collector]"].ToString();
                //        //mlDATAROW["InvCodeMess"] = dr["[code Messenger]"].ToString();

                //        //mlDATATABLE.Rows.Add(mlDATAROW);
                //        mlSQL = "INSERT INTO (InvNo, InvCustCode, InvCustName, InvDate, InvBranch, InvAmount, InvSiteCard, " +
                //                "InvProdOffer, InvOCM, InvFCM, InvCollector, InvCodeMess)" +
                //                "VALUES ('" + dr["[Invoice No.]"].ToString() + "', '" + dr["[Cust. Code]"].ToString() + "', " +
                //                "'" + dr["[Cust. name]"].ToString() + "', '" + dr["[Branch]"].ToString() + "', " +
                //                "'" + Convert.ToDecimal(dr["[Invoice]"]) + "', '" + dr["[Site Card]"].ToString() + "', " +
                //                "'" + dr["[Product Offering]"].ToString() + "', '" + dr["[OCM]"].ToString() + "', " +
                //                "'" + dr["[FCM]"].ToString() + "', '" + dr["[collector]"].ToString() + "', '" + dr["[code Messenger]"].ToString() + "')";

                //        mlOBJGS.ExecuteQuery(mlSQL, "PB", mlCOMPANYID, false, null);

                //    }
                //}

                mlMESSAGE.Text = "Uploading data success.";

                RetrieveFieldsDetail();
            }
            catch (Exception ex)
            {
                mlMESSAGE.Text = "Uploading data error : " + ex.Message.ToString();
            }
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    { 
    }
}