using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using IAS.Core.CSCode;
  
public partial class LinkXML : System.Web.UI.Page
{
    ModuleDBFunction oMDBF = new ModuleDBFunction();
    SqlConnection oConn = new SqlConnection();
    SqlConnection oConnDest = new SqlConnection();
    SqlCommand oCmd;
    SqlDataReader oReader;
    String Ssql = "";
	String EntityID = "";
	String CRLF = Environment.NewLine ;
	
    DataTable dtResult;
    DataTable dtTransfer;

    protected void Page_Load(object sender, EventArgs e)
    {
		EntityID = Request.QueryString["Entity"];
		
        if (!IsPostBack)
        {
            oConn = new SqlConnection();
            oConn.ConnectionString = OpenConnectionSource();
            if (oConn.State == ConnectionState.Closed)
            {
                oConn.Open();
            }

            GetDataXML();
        }
    }
    protected void GetDataXML()
    {
        String mlSQL = "";
        DataTable dtXML = new DataTable();
        String XMLFile = "";
        String XMLPath = "";
        DataSet dsXML = new DataSet();

		String mlTABLENAME = "";
		String mlENTITY = "";
		
        //Ssql = "SELECT a.userid,a.name,a.EmailAddr " + Environment.NewLine ; 
        //Ssql += "FROM AD_USERPROFILE a" + Environment.NewLine ;
        //dtXML = oMDBF.OpenRecordSet("ISSNV", "PB", mlSQL, CommandType.Text).Tables[0];
        
		if (EntityID == "1")
			{
				mlTABLENAME = "[dbo].[ID_001 ISS Indonesia$";
				mlENTITY = "ISS";
			}
		else if (EntityID == "2")
		{
			mlTABLENAME = "[dbo].[ID_002 ISS Facility Services$";
			mlENTITY = "IFS";
		}
		else if (EntityID == "3")
		{
			mlTABLENAME = "[dbo].[ID_003 ISS Catering Services$";
			mlENTITY = "ICS";
		}
		else if (EntityID == "4")
		{
			mlTABLENAME = "[dbo].[ID_004 ISS Parking Management$";
			mlENTITY = "IPM";
		}

			Ssql += " Declare @Ssql varchar(8000),@TableName varchar(100),@SqlJoin varchar(8000),@TableTemp varchar(100), @Entity varchar(20) " + CRLF;
			Ssql += " Declare @i int,@Max int " + CRLF;
            Ssql += " create table #Temp_JobTaskD ( [TimeStamp] timestamp,JOBNO varchar(20),JOBTASKNO varchar(20),BRANCH varchar(20),CM varchar(100), " + CRLF;
			Ssql += " 								COSTCENTER varchar(100)," + CRLF;
			Ssql += " 								HK varchar(100),HKM varchar(100),JOBTYPE varchar(100),OPERATIONAL_SEGMENT varchar(100),SERVICETYPE varchar(100), " + CRLF;
            Ssql += " 								SHKM varchar(100),ZONE_MANAGER varchar(100),ZONE_SUPERVISOR varchar(100),CUSTOMER_CATEGORY varchar(100) " + CRLF;
			Ssql += " 								) " + CRLF;
			Ssql += " set @TableTemp = '#Temp_JobTaskD' " + CRLF;
			Ssql += " set @TableName = ' "+ mlTABLENAME  +"Job Task Dimension] (Nolock) ' " + CRLF;
			Ssql += " set @SqlJoin = ' Left Join ' + @TableName + ' ' " + CRLF;
            Ssql += " set @Ssql = ' insert into #Temp_JobTaskD (JOBNO ,JOBTASKNO ,BRANCH ,CM , COSTCENTER ,HK ,HKM ,JOBTYPE ,OPERATIONAL_SEGMENT ,SERVICETYPE , SHKM ,ZONE_MANAGER ,ZONE_SUPERVISOR ,CUSTOMER_CATEGORY ) " + CRLF;
			Ssql += " SELECT DISTINCT JT.[Job No_],A.[Job Task No_], " + CRLF;
 			Ssql += " 	A.[Dimension Value Code] AS BRANCH,B.[Dimension Value Code] AS CM,C.[Dimension Value Code] AS [COSTCENTER], " + CRLF;
			Ssql += "	H.[Dimension Value Code] AS HK, " + CRLF;
 			Ssql += " 	I.[Dimension Value Code] AS HKM,K.[Dimension Value Code] AS JOBTYPE,L.[Dimension Value Code] AS [OPERATIONAL SEGMENT], " + CRLF;
 			Ssql += " 	M.[Dimension Value Code] AS [SERVICETYPE],N.[Dimension Value Code] AS [SHKM], " + CRLF;
 			Ssql += " 	Q.[Dimension Value Code] AS [ZONE MANAGER],R.[Dimension Value Code] AS [ZONE SUPERVISOR], " + CRLF;
            Ssql += "	O.[Dimension Value Code] AS CUSTOMER_CATEGORY " + CRLF;
			Ssql += " FROM '+ @TableName +' JT'  " + CRLF;
			Ssql += " + @SqlJoin + ' A " + CRLF;
			Ssql += "   ON A.[Job No_] = JT.[Job No_] " + CRLF;
			Ssql += "   AND A.[Job Task No_] = JT.[Job Task No_] " + CRLF;
			Ssql += "   AND A.[Dimension Code] = ''BRANCH'' ' " + CRLF;
			Ssql += " + @SqlJoin + ' B " + CRLF;
			Ssql += "   ON JT.[Job No_] = B.[Job No_] " + CRLF;
			Ssql += "   AND JT.[Job Task No_] = B.[Job Task No_] " + CRLF;
			Ssql += "   AND B.[Dimension Code] = ''CM'' ' " + CRLF;
			Ssql += " + @SqlJoin + ' C " + CRLF;
			Ssql += "   ON JT.[Job No_] = C.[Job No_] " + CRLF;
			Ssql += "   AND JT.[Job Task No_] = C.[Job Task No_] " + CRLF;
			Ssql += "   AND C.[Dimension Code] = ''COSTCENTER'' ' " + CRLF;
			Ssql += " + @SqlJoin + ' H " + CRLF;
			Ssql += "   ON JT.[Job No_] = H.[Job No_] " + CRLF;
			Ssql += "   AND JT.[Job Task No_] = H.[Job Task No_] " + CRLF;
			Ssql += "   AND H.[Dimension Code] = ''HK'' ' " + CRLF;
			Ssql += " + @SqlJoin + ' I " + CRLF;
			Ssql += "   ON JT.[Job No_] = I.[Job No_] " + CRLF;
			Ssql += "   AND JT.[Job Task No_] = I.[Job Task No_] " + CRLF;
			Ssql += "   AND I.[Dimension Code] = ''HKM'' ' " + CRLF;
			Ssql += " + @SqlJoin + ' K " + CRLF;
			Ssql += "   ON JT.[Job No_] = K.[Job No_] " + CRLF;
			Ssql += "   AND JT.[Job Task No_] = K.[Job Task No_] " + CRLF;
			Ssql += "   AND K.[Dimension Code] = ''JOBTYPE'' ' " + CRLF;
			Ssql += " + @SqlJoin + ' L " + CRLF;
			Ssql += "   ON JT.[Job No_] = L.[Job No_] " + CRLF;
			Ssql += "   AND JT.[Job Task No_] = L.[Job Task No_] " + CRLF;
			Ssql += "   AND L.[Dimension Code] = ''OPERATIONAL SEGMENT'' ' " + CRLF;
			Ssql += " + @SqlJoin + ' M " + CRLF;
			Ssql += "   ON JT.[Job No_] = M.[Job No_] " + CRLF;
			Ssql += "   AND JT.[Job Task No_] = M.[Job Task No_] " + CRLF;
			Ssql += "   AND M.[Dimension Code] = ''SERVICETYPE'' ' " + CRLF;
			Ssql += " + @SqlJoin + ' N " + CRLF;
			Ssql += "   ON JT.[Job No_] = N.[Job No_] " + CRLF;
			Ssql += "   AND JT.[Job Task No_] = N.[Job Task No_] " + CRLF;
			Ssql += "   AND N.[Dimension Code] = ''SHKM'' ' " + CRLF;
            Ssql += " + @SqlJoin + ' O " + CRLF;
            Ssql += "   ON JT.[Job No_] = O.[Job No_] " + CRLF;
            Ssql += "   AND JT.[Job Task No_] = O.[Job Task No_] " + CRLF;
            Ssql += "   AND O.[Dimension Code] = ''CUSTOMER CATEGORY'' ' " + CRLF;
            //Ssql += " + @SqlJoin + ' P " + CRLF;
            //Ssql += "   ON JT.[Job No_] = P.[Job No_] " + CRLF;
            //Ssql += "   AND JT.[Job Task No_] = P.[Job Task No_] " + CRLF;
            //Ssql += "   AND P.[Dimension Code] = ''ZINVLINE'' ' " + CRLF;
			Ssql += " + @SqlJoin + ' Q " + CRLF;
			Ssql += "   ON JT.[Job No_] = Q.[Job No_] " + CRLF;
			Ssql += "   AND JT.[Job Task No_] = Q.[Job Task No_] " + CRLF;
			Ssql += "   AND Q.[Dimension Code] = ''ZONE MANAGER'' ' " + CRLF;
			Ssql += " + @SqlJoin + ' R " + CRLF;
			Ssql += "   ON JT.[Job No_] = R.[Job No_] " + CRLF;
			Ssql += "   AND JT.[Job Task No_] = R.[Job Task No_] " + CRLF;
			Ssql += "   AND R.[Dimension Code] = ''ZONE SUPERVISOR'' ' " + CRLF;
			Ssql += " " + CRLF;
			Ssql += " exec (@Ssql) " + CRLF;
			Ssql += " " + CRLF;				
            Ssql += " SELECT A.timestamp, A.JOBNO,A.JOBTASKNO,A.BRANCH,ISNULL(A.CM,'') AS CM,A.COSTCENTER,ISNULL(A.HK,'') AS HK,ISNULL(A.HKM,'') AS HKM,ISNULL(A.SHKM,'') AS SHKM,ISNULL(A.ZONE_MANAGER,'') AS ZONEMANAGER," + CRLF;
            Ssql += "        ISNULL(A.ZONE_SUPERVISOR,'') AS ZONESUPERVISOR,ISNULL(A.CUSTOMER_CATEGORY,'') AS CUSTOMER_CATEGORY, " + CRLF;
            Ssql += "        B.[Bill-to Customer No_] AS CustomerNo_, B.Description,B.[Bill-to Name] as [Search_Description],B.[Bill-to Address]+' '+ B.[Bill-to Address 2]+' '+B.[Bill-to City]+' '+B.[Bill-to Post Code] as [Address],  " + CRLF;
            Ssql += "        C.Name AS ProductOffering,A.OPERATIONAL_SEGMENT AS MarketSeg_,case when b.Blocked = 0 then 'Active' else 'Not Active' end as Status " + CRLF;
            Ssql += " Into #Temp_LinkXML " + CRLF;
			Ssql += " FROM #Temp_JobTaskD A " + CRLF;
            Ssql += " LEFT JOIN " + mlTABLENAME + "Job Task] (Nolock) B " + CRLF;
			Ssql += " 	ON A.JOBNO = B.[Job No_] " + CRLF;
			Ssql += "   and A.JOBTASKNO = B.[Job Task No_]" + CRLF;
            Ssql += " LEFT JOIN " + mlTABLENAME + "Dimension Value] (Nolock) C " + CRLF;
            Ssql += "  	ON A.SERVICETYPE = C.[code]  " + CRLF;
            Ssql += "  	AND C.[Dimension code] = 'SERVICETYPE' " + CRLF;
            Ssql += " wHERE 1=1 " + CRLF; //(CM is not null or HK is not null or HKM is not null OR SHKM IS NOT NULL) " + crlf;
            Ssql += " and jobtaskno is not null " + CRLF;
			Ssql += " " + CRLF;
            Ssql += " Select * from #Temp_LinkXML" + CRLF;
            Ssql += " " + CRLF;
            Ssql += " DROP TABLE #Temp_JobTaskD " + CRLF;
            Ssql += " DROP TABLE #Temp_LinkXML " + CRLF;
            //dtXML = oMDBF.OpenRecordSet("ISSNV", "PB", Ssql, CommandType.Text).Tables[0];

            oCmd = new SqlCommand(Ssql, oConn);
            oReader = oCmd.ExecuteReader();
            if (oReader.HasRows)
            {
                dtXML.Load(oReader);
            }
            else
            {
                //MessageBox.Show("Data Not Found");
                return;
            }

        if(dtXML.Rows.Count > 0  )
        {

            XMLFile = "JJT.xml";
            XMLPath = "C:\\inetpub\\wwwroot\\IAS\\XML\\JJT.xml";
            if(File.Exists(XMLPath))
            {
                File.Delete(XMLPath); 
            }
            DataTable dtNewXML = new DataTable();
            dtNewXML = dtXML.Copy();
            dsXML.Tables.Add(dtNewXML);   
            dsXML.Tables[0].TableName = "JJT";
            dsXML.WriteXml(XMLPath);

            LoadFileXML();
        }
        else
        {
            mlMESSAGE.Text = "Data Not Found";
        }

    }
    protected void LoadFileXML()
    {
        String XMLPath = "";
        Response.Clear();
        Response.Buffer = true ;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/xml";
        //Response.WriteFile(Server.MapPath("~/XML/QRCode/" & XMLPath))
        XMLPath = "C:\\inetpub\\wwwroot\\IAS\\XML\\JJT.xml";
        Response.WriteFile(XMLPath);
        Response.Flush();
        Response.End(); 

        
    }
    public string OpenConnectionSource()
    {
        //return "Provider=SQLNCLI10;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=global_nav71_prod;Data Source=ISSCASAAR162\\NAV2013PROD";
        return "Data Source=10.62.100.53;database=PROD_ISS_NAV2013;user id=IASENGINE;password=Raja155SlanG";
							
    }
    
}