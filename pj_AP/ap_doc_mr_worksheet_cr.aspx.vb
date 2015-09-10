Imports System.Data.OleDb
Imports System.Data
Imports System.IO
Imports System.Net.Mail
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions
Imports CrystalDecisions.CrystalReports
Imports System.Web.Configuration
Imports IAS.Core.CSCode

Partial Class ap_doc_mr_worksheet
    Inherits System.Web.UI.Page
    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJFS As New IASClass.ucmFileSystem
    Dim mlOBJPJ As New ModuleFunctionLocal

    
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



    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        CekSession()
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Requisition Worksheet Doc (Ver: CR) V2.01"
        mlMEMBERGROUP = System.Configuration.ConfigurationManager.AppSettings("mgMEMBERGROUPMENU")
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlKEY = Request.QueryString("mpID")
        'mlKEY = "AP/ORW/00000064"
        'mlKEY = "AP/ORW/00000060"
        'Session("mgUSERID") = "admin"

        mlMESSAGE.Text = mlKEY

        Select Case Trim(mlOBJGF.GetStringAtPosition(mlKEY, 1, "-"))
            Case "dt2"
                mlSQL2 = "SELECT " & _
                        " [Type],[No.],[Driver],cONVERT(VARCHAR(10),[Delivery Date],101) AS [Delivery Date]," & _
                        " [Created By],[Verified By]," & _
                        " [Agreed By]," & _
                        " [Site Card No.],[Location Code],[Material Requisition No.]," & _
                        " [Description],[Material Requisition Quantity],[Unit of Measure Code]," & _
                        " [Item Journal Batch], [Vendor No.],[Sitecard Reference]," & _
                        " [Qty PO w1],[Qty PO w2],[Qty PO w3],[Qty PO w4],[Qty PO w5],DescriptionDt2" & _
                        " FROM AP_OUT_REQUISITION WHERE ReffNo='" & mlKEY & "';"

            Case Else

                'mlSQL2 = "SELECT " & _
                '        " [Type],[No.],[Driver],cONVERT(VARCHAR(10),[Delivery Date],101) AS [Delivery Date]," & _
                '        " [Created By],[Verified By]," & _
                '        " [Agreed By]," & _
                '        " [Site Card No.],[Location Code],[Material Requisition No.]," & _
                '        " [Description],[Material Requisition Quantity],[Unit of Measure Code]," & _
                '        " [Item Journal Batch], [Vendor No.],[Sitecard Reference]," & _
                '        " [Qty PO w1],[Qty PO w2],[Qty PO w3],[Qty PO w4],[Qty PO w5],DescriptionDt2" & _
                '        " FROM AP_OUT_REQUISITION WHERE ReffNo='" & mlKEY & "';"

                mlSQL2 = " SELECT distinct a.[Material Requisition No.] as [Item Order No],b.Job_No as [Job No],b.Job_TaskNo as [Job Task No] " & vbCrLf
                mlSQL2 += "      ,cONVERT(VARCHAR(10),A.[Delivery Date],103) as [Request Delivery Date], " & vbCrLf
                mlSQL2 += "       a.[Location Code],c.ItemNo as [Item No],a.[Material Requisition Quantity] as Quantity,a.DescriptionDt2 as Size " & vbCrLf
                mlSQL2 += " FROM AP_OUT_REQUISITION  a " & vbCrLf
                mlSQL2 += " left join PROD_IFS_NAV2013.dbo.[IFS$Mapping_JobTaskNo_SiteCard] b " & vbCrLf
                mlSQL2 += "	    on a.[Site Card No.] = b.SiteCard " & vbCrLf
                mlSQL2 += "	left join PROD_IFS_NAV2013.dbo.[IFS$Mapping_Item] c " & vbCrLf
                mlSQL2 += "	    on a.[No.] = c.ItemNo collate SQL_Latin1_General_CP1_CI_AS " & vbCrLf
                mlSQL2 += " WHERE ReffNo='" & mlKEY & "' and b.EndDate <= '2015-12-31' "
                ';"


        End Select

        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "bb_doc_bonusothers_cr_hr.", "Doc Other Bonus Header", "")
        End If

        CrReport(mlDOCNO, True)
    End Sub
    Protected Sub CekSession()
        If Session("mgMENUSTYLE") = "" Then
            Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=34FC35D4")
            Return
        End If
    End Sub
    Public Sub CrReport(ByVal mpDOCNO As String, ByVal mpEXPTOPDF As Boolean)
        Dim mlCR As ReportDocument = New ReportDocument()
        Dim mlCREXPORTOPTIONS As ExportOptions
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

        Dim mlREPORTNAME As String
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

            mlREPORTNAME = "../pj_rpt/rpt_crystalrpt/ap_doc_mr_worksheet_NAV2013.rpt"
            'mlREPORTNAME = "../pj_rpt/rpt_crystalrpt/ap_doc_mr_worksheet.rpt"
            mlFILENAME = "IAS_" & Left(Session("mguserid"), 3) & "_SlipBonus_Sum" & ".pdf"
            mlPATH = Server.MapPath("../output/" & mlFILENAME)
            mlPATH2 = "../output/" & mlFILENAME


            mlCR.Load(Server.MapPath(mlREPORTNAME))

            mlSQLTEMP = "SELECT * FROM AD_DATASOURCE WHERE CompanyID='ISSP3'"
            mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "AD", "AD")
            If mlRSTEMP.HasRows Then
                mlRSTEMP.Read()
                mlSERVERNAME = Trim(mlRSTEMP("DataSource"))
                mlDATABASENAME = Trim(mlRSTEMP("DatabaseName"))
                mlUSERID = mlOBJGF.Decrypt(Trim(mlRSTEMP("SystemUID")), mlENCRYPTCODE)
                mlPASSWORD = mlOBJGF.Decrypt(Trim(mlRSTEMP("SystemPassword")), mlENCRYPTCODE)
            End If
            mlOBJGS.CloseFile(mlRSTEMP)


            'mlSERVERNAME = "ISSIDWKS528\SQLEXPRESS"
            'mlDATABASENAME = "PROD_ISS_NAV"
            'mlUSERID = "sa"
            'mlPASSWORD = "1"

            ''mlSERVERNAME = mlOBJGF.Decrypt(mlSERVERNAME, mlENCRYPTCODE)
            'mlDATABASENAME = mlOBJGF.Decrypt(mlDATABASENAME, mlENCRYPTCODE)
            'mlUSERID = mlOBJGF.Decrypt(mlUSERID, mlENCRYPTCODE)
            'mlPASSWORD = mlOBJGF.Decrypt(mlPASSWORD, mlENCRYPTCODE)



            With mlCRCONNECTIONINFO
                .ServerName = mlSERVERNAME
                .DatabaseName = mlDATABASENAME
                .UserID = mlUSERID
                .Password = mlPASSWORD
            End With

            mlCRTABLES = mlCR.Database.Tables
            For Each mlCRTABLE In mlCRTABLES
                mlCRTABLELOGONINFO = mlCRTABLE.LogOnInfo
                mlCRTABLELOGONINFO.ConnectionInfo = mlCRCONNECTIONINFO
                mlCRTABLE.ApplyLogOnInfo(mlCRTABLELOGONINFO)
            Next


            Dim mlDATAADAPTERPRT_HR As OleDb.OleDbDataAdapter
            Dim mlDATASETRPT_HR As New DataSet
            Dim mlTABLERPT As String
            Dim mlMODULERPT As String
            Dim mlREPORTQUERY As String


            mlMODULERPT = ""
            mlTABLERPT = "tablerpthr"
            mlREPORTQUERY = mlSQL2
            mlOBJGS.CloseDataAdapter(mlDATAADAPTERPRT_HR)
            mlDATAADAPTERPRT_HR = mlOBJGS.DbAdapter(mlREPORTQUERY, "PB", "ISSP3")
            mlDATASETRPT_HR.Clear()
            mlDATAADAPTERPRT_HR.Fill(mlDATASETRPT_HR, mlTABLERPT)
            mlCR.SetDataSource(mlDATASETRPT_HR.Tables(mlTABLERPT))

            'mlCR.SetParameterValue("@DOCNO", mlDOCNO)
            'mlCR.SetParameterValue("@DOCNO", mlDOCNO, "SlipBonus_dtl.rpt")
            'mlCR.SetParameterValue("@DOCNO", mlDOCNO, "SlipBonus_Adj.rpt")
            'mlCR.SetParameterValue("@DOCNO", mlDOCNO, "SlipBonus_ppv.rpt")
            'mlCR.SetParameterValue("@DOCNO", mlDOCNO, "SlipBonus_plv.rpt")

            CRViewer.ReportSource = mlCR

        Catch ex As Exception
            Response.Write(Err.Description)
        End Try
    End Sub


    End Class

