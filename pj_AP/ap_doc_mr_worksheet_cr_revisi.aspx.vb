Imports System.Data.OleDb
Imports System.Data
Imports System.IO
Imports System.Net.Mail
Imports System.Collections.Generic
Imports Microsoft.Reporting.WebForms
Imports IAS.Core.CSCode
Partial Class pj_AP_ap_doc_mr_worksheet_cr_revisi
    Inherits System.Web.UI.Page
    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJFS As New IASClass.ucmFileSystem
    Dim mlOBJPJ As New ModuleFunctionLocal
    Dim mlRDS As New ReportDataSource


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
    Dim FSize As String

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        CekSession()
        'Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
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
        FSize = Request.QueryString("FSize")

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
                If FSize = "0" Then
                    mlSQL2 = " SELECT distinct a.[Material Requisition No.] as [Item Order No],b.Job_No as [Job No],b.Job_TaskNo as [Job Task No] " & vbCrLf
                    mlSQL2 += "      ,cONVERT(VARCHAR(10),A.[Delivery Date],103) as [Request Delivery Date], " & vbCrLf
                    mlSQL2 += "       a.[Location Code],c.ItemNo as [Item No],a.[Material Requisition Quantity] as Quantity,a.DescriptionDt2 as Size " & vbCrLf
                    mlSQL2 += " FROM AP_OUT_REQUISITION  a " & vbCrLf
                    mlSQL2 += " inner join PROD_IFS_NAV2013.dbo.[IFS$Mapping_JobTaskNo_SiteCard] b " & vbCrLf
                    mlSQL2 += "	    on a.[Site Card No.] = b.SiteCard " & vbCrLf
                    mlSQL2 += "	inner join PROD_IFS_NAV2013.dbo.[IFS$Mapping_Item] c " & vbCrLf
                    mlSQL2 += "	    on a.[No.] = c.ItemNo collate SQL_Latin1_General_CP1_CI_AS " & vbCrLf
                    mlSQL2 += " WHERE ReffNo='" & mlKEY & "' "
                Else
                    mlSQL2 = " SELECT distinct a.[Material Requisition No.] as [Item Order No],b.Job_No as [Job No],b.Job_TaskNo as [Job Task No], " & vbCrLf
                    mlSQL2 += "       cONVERT(VARCHAR(10),A.[Delivery Date],103) as [Request Delivery Date], " & vbCrLf
                    mlSQL2 += "       a.[Location Code],a.[No.] as [Item No],d.Qty as Quantity," & vbCrLf
                    mlSQL2 += "       case when d.fSize = '0' then '' else d.fsize end as Size, " & vbCrLf
                    mlSQL2 += "       case when a.[no.] in (Select LinCode from PROD_ISS.dbo.XM_UNIVERSALLOOKUPLIN Where universalID = 'FLAGNAMA_ON_ITEM') then a.DescriptionDt2 end as Description" & vbCrLf
                    mlSQL2 += " FROM AP_OUT_REQUISITION  a " & vbCrLf
                    mlSQL2 += " inner join PROD_IFS_NAV2013.dbo.[IFS$Mapping_JobTaskNo_SiteCard] b " & vbCrLf
                    mlSQL2 += "	    on a.[Site Card No.] = b.SiteCard " & vbCrLf
                    mlSQL2 += "	inner join AP_MR_REQUESTDT2 d" & vbCrLf
                    mlSQL2 += "     on a.[No.] = d.ItemKey " & vbCrLf
                    mlSQL2 += "     and a.[Material Requisition No.] = d.DocNo" & vbCrLf
                    mlSQL2 += " WHERE ReffNo='" & mlKEY & "' "

                End If


        End Select

        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "bb_doc_bonusothers_cr_hr.", "Doc Other Bonus Header", "")
        End If

        ReportViewer(mlKEY)
    End Sub
    Protected Sub CekSession()
        If Session("mgMENUSTYLE") = "" Then
            Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=34FC35D4")
            Return
        End If
    End Sub

    Protected Sub ReportViewer(ByVal DocNo As String)
        Dim FileName As String
        Dim mlDATATABLEREPORT As New DataTable()
        Dim mlDATAADAPTERPRT_HR As OleDb.OleDbDataAdapter
        Dim mlDATASETRPT_HR As New DataSet

        If FSize = "0" Then
            FileName = "rptAP_WorksheetMR.rdlc"
        Else
            FileName = "rptAP_WorksheetMR2.rdlc"
        End If

        mlDATAADAPTERPRT_HR = mlOBJGS.DbAdapter(mlSQL2, "PB", "ISSP3")
        mlDATASETRPT_HR.Clear()
        mlDATAADAPTERPRT_HR.Fill(mlDATASETRPT_HR)
        mlDATATABLEREPORT = mlDATASETRPT_HR.Tables(0)

        Me.rptViewer.LocalReport.ReportPath = ConfigurationManager.AppSettings("ReportPath") + FileName
        Me.rptViewer.ProcessingMode = ProcessingMode.Local

        mlRDS = New ReportDataSource()
        mlRDS.Name = "DsWorksheetMR"
        mlRDS.Value = mlDATATABLEREPORT
        Me.rptViewer.LocalReport.DataSources.Clear()
        Me.rptViewer.LocalReport.DataSources.Add(mlRDS)

        'Untuk Passing Parameter
        Dim Parameters As New List(Of ReportParameter)()

        Parameters.Add(New ReportParameter("mlDOCNO", DocNo))
        Me.rptViewer.LocalReport.SetParameters(Parameters)

        Try
            Me.rptViewer.DataBind()
            Me.rptViewer.LocalReport.Refresh()

        Catch ex As Exception
            mlMESSAGE.Text = ex.Message

        End Try


    End Sub

End Class
