Imports System.Data.OleDb
Imports System.Data
Imports System.IO
Imports System.Net.Mail
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions
Imports CrystalDecisions.CrystalReports
Imports System.Web.Configuration

Partial Class pj_ap_ap_rpt_mr_viewer
    Inherits System.Web.UI.Page
    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJFS As New IASClass.ucmFileSystem
    Dim mlOBJPJ As New FunctionLocal

    Dim mlDOCNO As String

    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlRSTEMP As OleDb.OleDbDataReader
    Dim mlSQLTEMP As String
    Dim mlREADER2 As OleDb.OleDbDataReader
    Dim mlSQL2 As String
    Dim mlKEY As String
    Dim mlMEMBERGROUP As String
    Dim mlI As Integer
    Dim mlREPORTNAME As String



    Protected Sub mpFORM_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles mpFORM.Load
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "AP MR Report (V2.02)"
        mlMEMBERGROUP = System.Configuration.ConfigurationManager.AppSettings("mgMEMBERGROUPMENU")
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        If Request.QueryString("mpQuery") Is Nothing = False Then
            mlSQL = Request.QueryString("mpQuery")
        End If
        If Request.QueryString("mpReportType") Is Nothing = False Then
            mlKEY = Request.QueryString("mpReportType")
        End If

        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "bb_doc_bonusothers_cr_hr.", "Doc Other Bonus Header", "")
        End If

        CrReport(mlKEY, mlSQL, False)

    End Sub
    Public Sub CrReport(ByVal mpReportType As String, ByVal mlSQLQuery As String, ByVal mpEXPTOPDF As Boolean)
        Dim mlCR As ReportDocument = New ReportDocument()
        Dim mlCREXPORTOPTIONS As ReportOptions
        Dim mlCRDESTOPTIONS As DiskFileDestinationOptions = New DiskFileDestinationOptions()
        Dim mlSUBREPORTNAME As String
        Dim mlCRCONNECTIONINFO As New ConnectionInfo

        Dim mlCRTABLELOGONINFOS As New TableLogOnInfos()
        Dim mlCRTABLELOGONINFO As New TableLogOnInfo()
        Dim mlCRTABLES As Tables
        Dim mlCRTABLE As Table

        Dim mlSERVERNAME As String
        Dim mlDATABASENAME As String
        Dim mlUSERID As String
        Dim mlPASSWORD As String
        Dim mlPAGE As Integer

        Dim mlFILENAME As String
        Dim mlPATH As String
        Dim mlPATH2 As String

        Dim mlENCRYPTCODE As String



        Try
            mlENCRYPTCODE = System.Configuration.ConfigurationManager.AppSettings("mgENCRYPTCODE")
            mlENCRYPTCODE = ""
            mlSERVERNAME = ""
            mlDATABASENAME = ""
            mlUSERID = ""
            mlPASSWORD = ""

            If mlCR Is Nothing = False Then
                mlCR.Close()
                mlCR.Dispose()
            End If
            mlCR = New ReportDocument

            Select Case mpReportType
                Case "Summary"
                    mlREPORTNAME = "../pj_rpt/rpt_crystalrpt/ap_mr_req_Summary.rpt"
                Case "Detail"
                    mlREPORTNAME = "../pj_rpt/rpt_crystalrpt/ap_mr_req_Detail.rpt"
                Case "Product Qty Request"
                    mlREPORTNAME = "../pj_rpt/rpt_crystalrpt/ap_mr_req_ProductQty.rpt"
                Case "Summary by SiteCard"
                    mlREPORTNAME = "../pj_rpt/rpt_crystalrpt/ap_mr_req_SummarybySC.rpt"
            End Select

            'mlMESSAGE.Text = "Cek Report : " & Server.MapPath(mlREPORTNAME)
            mlCR.Load(Server.MapPath(mlREPORTNAME))

            Dim mlDATAADAPTERPRT_HR As OleDb.OleDbDataAdapter
            Dim mlDATASETRPT_HR As New DataSet
            Dim mlTABLERPT As String
            Dim mlMODULERPT As String
            Dim mlREPORTQUERY As String


            mlMODULERPT = ""
            mlTABLERPT = "tablerpthr"
            mlREPORTQUERY = mlSQLQuery
            mlDATAADAPTERPRT_HR = mlOBJGS.DbAdapter(mlREPORTQUERY, "PB", "ISSP3")
            mlDATASETRPT_HR.Clear()
            mlDATAADAPTERPRT_HR.Fill(mlDATASETRPT_HR, mlTABLERPT)

            mlMESSAGE.Text = "Jumlah Item : " & mlDATASETRPT_HR.Tables(0).Rows.Count

            mlCR.SetDataSource(mlDATASETRPT_HR.Tables(mlTABLERPT))

            CRViewer.ReportSource = mlCR


        Catch ex As Exception
            mlMESSAGE.Text += ex.Message
        End Try
    End Sub
End Class
