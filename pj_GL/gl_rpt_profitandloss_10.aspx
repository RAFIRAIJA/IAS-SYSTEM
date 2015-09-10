<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="gl_rpt_profitandloss_10.aspx.vb" Inherits="backoffice_gl_rpt_profitandloss_10" title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Import Namespace = "System.Data" %>
<%@ Import Namespace = "System.Data.OleDb" %>
<%@ Import Namespace = "System.Web" %>
<%@ Import Namespace = "System.Collections.Generic" %>
<%@ Import Namespace = "System.Drawing" %>
<%@ Import Namespace = "System.IO" %>

<script RUNAT="server" language="vbscript">
    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction

    Dim mlKEY As String
    Dim mlSQL As String
    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlDATAADAPTER As OleDb.OleDbDataAdapter
    Dim mlDATASET As New DataSet
    Dim mlDATATABLE As New DataTable
    Dim mlSQL2 As String
    Dim mlREADER2 As OleDb.OleDbDataReader
    Dim mlSQLSOLD As String
    Dim mlREADERSOLD As OleDb.OleDbDataReader

    Dim mlRECORDSTATUS As String
    Dim mlSQLRECORDSTATUS As String
    Dim mlFUNCTIONPARAMETER As String
    Protected mlSQLSTATP As String = "SELECT * FROM AR_INVHR"
    Dim mlI As Integer
    Dim mlCURRENTDATE As String = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()
    
    'Search Variable
    Dim mlBRANCH2 As String
    Dim mlSITE2 As String

    Dim mlSQL_ADD1 As String
    Dim mlSQL_ADD2 As String
    Dim mlSQL_ADD3 As String
    Dim mlSQL_BRANCH As String
    Dim mlSQL_SITE As String
    Dim mlSQL_DATE As String
    Dim mlTABLENAME As String
        
    Dim mlSQLSITE1 As String
    Dim mlRSSITE1 As OleDbDataReader
    Dim mlSITE As String
    Dim mlSITEDESC As String
    Dim mlJUMLAHSITE As Integer
    Dim mlCURRENTSITE As String
            
    Dim mlSQL_COA As String
    Dim mlRSCOA As OleDbDataReader
    Dim mlSQL_SITE2 As String
    Dim mlRS_SITE As OleDbDataReader
    Dim mlSQLSITETOTAL As String
    Dim mlRSSITETOTAL As OleDbDataReader
    Dim mlSQL_TOTAL As String
    Dim mlRS_TOTAL As OleDbDataReader
    Dim mlFIRST As Boolean
    Dim mlLOOP As Boolean
    
    Dim mlTOTAL_REVENUE As Double
    Dim mlTOTAL_WAGES As Double
    Dim mlTOTAL_COST As Double
    Dim mlTOTAL_WAGES_COST As Double
    Dim mlTOTAL_ As Double
    Dim mlHAVINGDETAIL As Boolean
    
    
    Dim mlJ As Integer
    Dim mlTEMP As String
    Dim mlFIELDT2 As String
    Dim mlLINNO2 As Integer
    
    Dim mlGRIDCOLOR1 As String
    Dim mlGRIDCOLOR2 As String
    
    Dim mlSTARTTIME As String
    Dim mlENDTIME As String
        


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "GL Profit and Loss Report T1 V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "GL Profit and Loss Report T1 V2.00"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlFUNCTIONPARAMETER = Request.QueryString("mpFP")
        mlGRIDCOLOR1 = "FFF8D5"
        mlGRIDCOLOR2 = "FCEFB0"
        
        If Not Page.IsPostBack Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ar_rpt_invoice", "ar_rpt_invoice", "")
            'LoadComboData()
            'SearchRecord()
            LoadComboData2()
            btExCsv.Visible = False
            mlHAVINGDETAIL = False
        End If
    End Sub


    Sub ClearFields()
        mlDOCDATE1.Text = mlCURRENTDATE
        mlDOCDATE2.Text = mlCURRENTDATE
        mlLINKDOC.Text = ""
    End Sub

    Protected Sub mlDATAGRID_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRID.ItemCommand
        mlKEY = e.CommandArgument
        Select Case e.CommandName
            Case "BrowseRecord"
                mlMESSAGE.Text = "View Request for " & e.CommandArgument
                RetrieveFields()
                pnFILL.Visible = True
                pnGRID.Visible = False
            Case "EditRecord"
                mlMESSAGE.Text = "Edit Request for  " & e.CommandArgument
                EditRecord()
            Case "DeleteRecord"
                mlMESSAGE.Text = "Delete Request for  " & e.CommandArgument
                DeleteRecord()
            Case Else
                ' Ignore Other
        End Select
    End Sub

    Protected Sub mlDATAGRID_SortCommand(ByVal Source As Object, ByVal E As DataGridSortCommandEventArgs) Handles mlDATAGRID.SortCommand
        RetrieveFieldsDetail(mlSQLSTATEMENT.Text & " ORDER BY " & E.SortExpression)
    End Sub

    Sub mlDATAGRID_PageIndex(ByVal Source As Object, ByVal E As DataGridPageChangedEventArgs)
        mlDATAGRID.CurrentPageIndex = E.NewPageIndex
        RetrieveFieldsDetail(mlSQLSTATEMENT.Text)
    End Sub


    Protected Sub mlDATAGRID_ItemBound(ByVal Source As Object, ByVal E As DataGridItemEventArgs) Handles mlDATAGRID.ItemDataBound

        If E.Item.ItemType = ListItemType.Item Or E.Item.ItemType = ListItemType.AlternatingItem Then
            'mlI = 5
            'Dim mlDOCDATE1 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
            'E.Item.Cells(mlI).Text = mlDOCDATE1.ToString("d")
            'E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

            'mlI = 5
            'E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

            'mlI = 9
            'Dim mlPOINT1 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
            'E.Item.Cells(mlI).Text = mlPOINT1.ToString("n")
            'E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

            'mlI = 10
            'Dim mlAMOUNT1 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
            'E.Item.Cells(mlI).Text = mlAMOUNT1.ToString("n")
            'E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

            'mlI = 12
            'Dim mlRECDATE1 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
            'E.Item.Cells(mlI).Text = mlRECDATE1.ToString("d")
            'E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right
        End If
    End Sub


    Protected Sub btCancelOperation_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btCancelOperation.Click
        CancelOperation()
    End Sub

    Public Sub RetrieveFields()
        ClearFields()
        DisableCancel()
    End Sub

    Sub RetrieveFieldsDetail(ByVal mpSQL As String)
        mlDATASET.Clear()
        mlDATAADAPTER = mlOBJGS.DbAdapter(mpSQL)
        mlDATAADAPTER.Fill(mlDATASET)
        mlDATATABLE = mlDATASET.Tables("table")
        mlDATAGRID.DataSource = mlDATATABLE
        mlDATAGRID.DataBind()

        mlOBJGS.CloseDataSet(mlDATASET)
        mlOBJGS.CloseDataAdapter(mlDATAADAPTER)
        btExCsv.Visible = True
    End Sub

    Sub DeleteRecord()
        mlRECORDSTATUS = "Delete"
        'mlSQL = "UPDATE FROM AR_INVHR WHERE MenuID= '" & Trim(mlKEY) & "'"
        'mlOBJGS.ExecuteQuery(mlSQL)
        'RetrieveFields()
        'RetrieveFieldsDetail()
    End Sub

    Sub NewRecord()
        EnableCancel()
    End Sub

    Sub EditRecord()
        RetrieveFields()
        EnableCancel()
    End Sub

    Sub SaveRecord()
    End Sub

    Private Sub EnableCancel()
        pnFILL.Visible = True
        pnGRID.Visible = False
        btExCsv.Visible = False
    End Sub

    Private Sub DisableCancel()
        pnFILL.Visible = False
        pnGRID.Visible = True
        btExCsv.Visible = False
    End Sub

    Sub CancelOperation()
        pnFILL.Visible = True
        btSearchRecord.Visible = True
        btCancelOperation.Visible = True
    End Sub

    Sub CalculateTotal()
        Dim mlGRANDTOTALPOINT As Double
        Dim mlGRANDTOTALAMOUNT As Double

        mlGRANDTOTALPOINT = 0
        mlGRANDTOTALAMOUNT = 0
    End Sub

    Sub QuerytoTable()
        Dim mlBVMONTH As String
        
        mlSTARTTIME = Now
        mlTABLENAME = " GL_PL_1 "
        mlBVMONTH = "201111"
        mlSQL = ""
        
        mlBRANCH2 = Trim(mlOBJGF.GetStringAtPosition(mpBRANCH.Text, "0", "#"))
        mlSITE2 = Trim(mlOBJGF.GetStringAtPosition(mpSITE.Text, "0", "#"))
        If mlSITE2 = "All" Then mlSITE2 = ""
            
        mlBRANCH2 = "10PB"
        mlSITE2 = ""
        mlDOCDATE1.Text = "01/11/2012"
        mlDOCDATE2.Text = "30/11/2012"
        

        mlSQL_DATE = ""
        If mlDOCDATE1.Text <> "" And mlDOCDATE2.Text <> "" Then
            mlSQL_DATE = " AND (GLE.[Posting Date] >= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(mlDOCDATE1.Text, "/")) & "'" & _
                         " AND GLE.[Posting Date] <= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(mlDOCDATE2.Text, "/")) & "' )"
        End If


        mlSQL_ADD1 = " SELECT DISTINCT PTSITE.LineNo_" & _
                    " FROM   [ISS-NAV-3].dbo.[ISS Servisystem, PT$CustServiceSite] PTSITE,  [ISS-NAV-3].dbo.[ISS Servisystem, PT$Location] PTLOC" & _
                    " WHERE PTSITE.Branch  = PTLOC.[Branch Location]"
            
        mlSQL_BRANCH = ""
        If mlOBJGF.IsNone(mlBRANCH2) = False Then
            mlSQL_BRANCH = "AND PTSITE.Branch='" & mlBRANCH2 & "'"
        End If

        mlSQL_SITE = ""
        If mlOBJGF.IsNone(mlSITE2) = False Then
            mlSQL_SITE = "AND PTSITE.LineNo_='" & mlSITE2 & "'"
        End If
        
        mlSQL_ADD1 = mlSQL_ADD1 & mlSQL_BRANCH & mlSQL_SITE

        mlSQL_ADD2 = " AND (" & _
                    " GLE.[CustServiceSiteLineNo] IN " & _
                    " (" & mlSQL_ADD1 & " )"
        mlSQL_ADD3 = " OR GLE.[Others Dimension 24 Code] IN " & _
                    " (" & mlSQL_ADD1 & " )" & _
                    " )"
                       
        mlSQL = "SELECT DISTINCT " & _
                " GLE.[G_L Account No_],GLA.Name, GLE.[CustServiceSiteLineNo], GLE.[Others Dimension 24 Code]," & _
                " GLE.Amount, GLE.[Others Dimension 26 Code]" & _
                " INTO " & mlTABLENAME & _
                " FROM  [ISS Servisystem, PT$G_L Account] GLA, [ISS Servisystem, PT$G_L Entry] GLE" & _
                " WHERE " & _
                " GLE.[G_L Account No_] = GLA.No_" & mlSQL_DATE & mlSQL_ADD2 & mlSQL_ADD3
        'mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSN3")
         
        Try
            mlSQL = "DELETE FROM " & mlTABLENAME & " WHERE BVMonth = '" & mlBVMONTH & "'"
            mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
        
            mlSQL = "INSERT INTO " & mlTABLENAME & " (" & _
                    " [G_L Account No_],Name,[CustServiceSiteLineNo],[Others Dimension 24 Code]," & _
                    " SearchName,Amount,Branch,BVMonth,[Posting Date])" & _
                     "SELECT DISTINCT " & _
                    " GLE.[G_L Account No_],GLA.Name, GLE.[CustServiceSiteLineNo], GLE.[Others Dimension 24 Code]," & _
                    " PTSITE.SearchName,GLE.Amount, GLE.[Others Dimension 26 Code],'" & mlBVMONTH & "',[Posting Date]" & _
                    " FROM  [ISS-NAV-3].dbo.[ISS Servisystem, PT$G_L Account] GLA, [ISS-NAV-3].dbo.[ISS Servisystem, PT$G_L Entry] GLE," & _
                    " [ISS-NAV-3].dbo.[ISS Servisystem, PT$CustServiceSite] PTSITE" & _
                    " WHERE " & _
                    " GLE.[G_L Account No_] = GLA.No_ AND PTSITE.Branch = GLE.[Others Dimension 26 Code] " & _
                    " AND PTSITE.[lineno_] =  GLE.[Others Dimension 24 Code]" & _
                    mlSQL_DATE & mlSQL_ADD2 & mlSQL_ADD3
            mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
        
            mlENDTIME = Now
            mlMESSAGE.Text = "Task Transfer Data  : Start at " & mlSTARTTIME & " End on " & mlENDTIME & " "
            
        Catch ex As Exception
            mlMESSAGE.Text = Err.Description

        End Try
        
    End Sub

    Sub SearchRecord()
        
        Try
            mlSTARTTIME = Now
            mlTABLENAME = " GL_PL_1 "
            
            mlTOTAL_REVENUE = 0
            mlTOTAL_WAGES = 0
            mlTOTAL_COST = 0
            mlTOTAL_WAGES_COST = 0
            
            mlSQL_TOTAL = SQL_TOTAL("", mlTABLENAME, "", 0)
            mlRS_TOTAL = mlOBJGS.DbRecordset(mlSQL_TOTAL)
            mlRS_TOTAL.Read()
            mlTOTAL_REVENUE = mlRS_TOTAL("R_Amount")
            mlTOTAL_WAGES = mlRS_TOTAL("W_Amount")
            mlTOTAL_COST = mlRS_TOTAL("C_Amount")
            mlTOTAL_WAGES_COST = mlTOTAL_WAGES
            
            mlSITE = ""
            mlSITEDESC = ""
            mlJUMLAHSITE = "0"
            mlSQLSITE1 = "SELECT DISTINCT  [Others Dimension 24 Code],SearchName FROM" & _
                        "(" & _
                        " SELECT DISTINCT [Others Dimension 24 Code] as [Others Dimension 24 Code],SearchName FROM GL_PL_1 " & _
                        " UNION ALL " & _
                        " SELECT DISTINCT [CustServiceSiteLineNo] as [Others Dimension 24 Code],SearchName FROM GL_PL_1 " & _
                        " )TB1 WHERE [Others Dimension 24 Code] <> ''" & _
                        " ORDER BY [Others Dimension 24 Code]"
            mlRSSITE1 = mlOBJGS.DbRecordset(mlSQLSITE1)
            While mlRSSITE1.Read
                mlSITE = mlSITE & IIf(mlOBJGF.IsNone(mlSITE) = True, "", ",") & Trim(mlRSSITE1("Others Dimension 24 Code"))
                mlSITEDESC = mlSITEDESC & IIf(mlOBJGF.IsNone(mlSITEDESC) = True, "", ",") & Trim(mlRSSITE1("SearchName"))
                mlJUMLAHSITE = mlJUMLAHSITE + 1
            End While
            
            If mpMAXSITEQTY.text <> "" Then
                mlJUMLAHSITE = mpMAXSITEQTY.text
            End If
            
            
            mlSQLSITETOTAL = ""
            For mlJ = 1 To 3
                Select Case mlJ
                    Case "1"
                        mlSQLSITETOTAL = mlSQLSITETOTAL & " SELECT '1' AS Linno, '4999-9999' as [G_L Account No_], 'Total Revenue' as 'Name_'"
                    Case "2"
                        mlSQLSITETOTAL = mlSQLSITETOTAL & ""
                        mlSQLSITETOTAL = mlSQLSITETOTAL & " UNION ALL SELECT '2' AS Linno, 'W01..W99' as [G_L Account No_], 'Total Wages' as 'Name_'"
                    Case "3"
                        mlSQLSITETOTAL = mlSQLSITETOTAL & ""
                        mlSQLSITETOTAL = mlSQLSITETOTAL & " UNION ALL SELECT '3' AS Linno, 'DPC00..DPC9' as [G_L Account No_], 'TOTAL DIRECT PRODUCT COST' as 'Name_'"
                End Select
                   
                
                mlI = 0
                For mlI = 0 To mlJUMLAHSITE - 1
                    mlCURRENTSITE = Trim(mlOBJGF.GetStringAtPosition(mlSITE, mlI, ","))
                    mlSQLSITETOTAL = mlSQLSITETOTAL & IIf(mlOBJGF.IsNone(mlSQLSITETOTAL) = True, "", ",") & "'" & mlCURRENTSITE & "'" & "=" & SQL_TOTAL("S", mlTABLENAME, mlCURRENTSITE, mlJ)
                Next
            Next
            mlSQLSITETOTAL = mlSQLSITETOTAL & " ORDER BY Linno"
            mlRSSITETOTAL = mlOBJGS.DbRecordset(mlSQLSITETOTAL,"PB", "ISSP3")
            
            
            mlI = 0
            mlCURRENTSITE = "1"
            mlSQL_SITE2 = ""
            mlFIRST = True
            mlLOOP = True
            
            
            mlJ = 0
            mlSQL_COA = "SELECT  DISTINCT  [G_L Account No_], Name FROM " & mlTABLENAME & _
                " WHERE [G_L Account No_] in (" & _
                SQL_SerialCOA() & _
                " )" & _
                " ORDER BY [G_L Account No_]"
            mlRSCOA = mlOBJGS.DbRecordset(mlSQL_COA,"PB", "ISSP3")
            While mlRSCOA.Read
                
                If mlFIRST = True Then
                    mlSQL_SITE2 = mlSQL_SITE2 & "SELECT '" & mlRSCOA("G_L Account No_") & "' as [G_L Account No_], '" & mlRSCOA("Name") & "' as Name_, "
                    mlSQL_SITE2 = mlSQL_SITE2 & " Total = (SELECT Sum(Amount) FROM  GL_PL_1  WHERE ([G_L Account No_] = '" & mlRSCOA("G_L Account No_") & "'))"
                    mlLOOP = True
                Else
                    mlSQL_SITE2 = mlSQL_SITE2 & "UNION ALL SELECT '" & mlRSCOA("G_L Account No_") & "' as [G_L Account No_], '" & mlRSCOA("Name") & "' as Name_, "
                    mlSQL_SITE2 = mlSQL_SITE2 & " Total = (SELECT Sum(Amount) FROM  GL_PL_1  WHERE ([G_L Account No_] = '" & mlRSCOA("G_L Account No_") & "'))"
                    mlLOOP = True
                End If
                
                mlJ = mlJ + 1
                mlTEMP = ReportFieldGet2("", "ID-B-10PB", mlJ)
                mlFIELDT2 = mlOBJGF.GetStringAtPosition(mlTEMP, 0, ",")
                mlLINNO2 = mlOBJGF.GetStringAtPosition(mlTEMP, 1, ",")
                
                mlI = 0
                For mlI = 0 To mlJUMLAHSITE - 1
                    mlCURRENTSITE = Trim(mlOBJGF.GetStringAtPosition(mlSITE, mlI, ","))
                    mlSQL_SITE2 = mlSQL_SITE2 & IIf(mlOBJGF.IsNone(mlSQL_SITE2) = True, "", ",") & SQL_SITE(mlTABLENAME, mlCURRENTSITE, "", "ID-B-10PB", mlFIELDT2, mlLINNO2)
                    mlFIRST = False
                Next
            End While
            
            If mlOBJGF.IsNone(mlSQL_SITE2) = False Then
                mlRS_SITE = mlOBJGS.DbRecordset(mlSQL_SITE2,"PB", "ISSP3")
                mlHAVINGDETAIL = True
            End If
            
            
            'If Not mlOBJGF.IsNone(mlSQL) Then
            '    mlSQL = "SELECT DocNo, ReffNo, DocDate as Date,DistID as MemberID,DistName as Name, BVMonth,TotalPoint as Point,TotalAmount as Amount,Recuserid as UserID,Recdate as EntryDate  FROM AR_INVHR WHERE " & mlSQL
            '    mlSQLSTATEMENT.Text = mlSQL
            '    RetrieveFieldsDetail(mlSQLSTATEMENT.Text)
            '    pnFILL.Visible = False
            '    btSearchRecord.Visible = False
            'End If
            
            mlENDTIME = Now
            mlMESSAGE.Text = "Start at " & mlSTARTTIME & " End on " & mlENDTIME & " With Total Site Card = " & mlJUMLAHSITE

        Catch ex As Exception
            Response.Write(mlI & " - " & mlJ & " - " & mlCURRENTSITE & " -  " & Err.Description)
            
            mlOBJGS.CloseFile(mlRSSITE1)
            mlOBJGS.CloseFile(mlRSCOA)
            mlOBJGS.CloseFile(mlRS_SITE)
            mlOBJGS.CloseFile(mlRSSITETOTAL)
            mlOBJGS.CloseFile(mlRS_TOTAL)
        End Try
    End Sub

    Sub LoadComboData()
        mlDOCDATE1.Text = mlCURRENTDATE
        mlDOCDATE2.Text = mlCURRENTDATE

        mpBRANCH.Items.Clear()
        mpBRANCH.Items.Add("10PB # Ware house - Pekan Baru")
        mlSQL = "SELECT [Branch Location], Name FROM [ISS Servisystem, PT$Location] ORDER BY Name"
        mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSN3")
        While mlREADER.Read
            mpBRANCH.Items.Add(mlREADER("Branch Location") & " # " & mlREADER("Name"))
        End While

        mpSITE.Items.Clear()
        mpSITE.Items.Add("All")
        mlSQL = "SELECT [Lineno_], SearchName FROM [ISS Servisystem, PT$CustServiceSite] ORDER BY SearchName"
        mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSN3")
        While mlREADER.Read
            mpSITE.Items.Add(mlREADER("Lineno_") & " # " & mlREADER("SearchName"))
        End While
        
        mpREPORTTYPE.Items.Clear()
        mpREPORTTYPE.Items.Add("PL010")
        
       
    End Sub
    
    Sub LoadComboData2()
        mpMAXSITEQTY.Items.Clear()
        mpMAXSITEQTY.Items.Add("")
        mpMAXSITEQTY.Items.Add("2")
        mpMAXSITEQTY.Items.Add("3")
        mpMAXSITEQTY.Items.Add("10")
        mpMAXSITEQTY.Items.Add("50")
        mpMAXSITEQTY.Items.Add("80")
        mpMAXSITEQTY.Items.Add("100")
        mpMAXSITEQTY.Items.Add("150")
        mpMAXSITEQTY.Items.Add("200")
    End Sub

    Protected Sub btSearchRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSearchRecord.Click
        SearchRecord()
    End Sub


    Protected Sub btExCsv_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btExCsv.Click
        ExportToCSV(mlSQLSTATEMENT.Text)
    End Sub

    Private Sub ExportToCSV(ByVal mpSQL As String)

        Dim mlDATAADAPTERCSV As OleDb.OleDbDataAdapter
        Dim mlDATASETCSV As New DataSet
        Dim mlDATATABLECSV As New DataTable
        Dim mlPATH As String
        Dim mlOBJGS_CSV As New IASClass.ucmGeneralSystem_ExporterCSV()
        Dim mlFILENAME As String
        Dim mlPATH2 As String

        mlDATASETCSV.Clear()
        mlDATAADAPTERCSV = mlOBJGS.DbAdapter(mpSQL)
        mlDATAADAPTERCSV.Fill(mlDATASETCSV)
        mlDATATABLECSV = mlDATASETCSV.Tables("table")

        mlFILENAME = "IAS_" & Left(Session("mguserid"), 3) & "_INV" & ".csv"
        mlPATH = Server.MapPath("../output/" & mlFILENAME)
        mlPATH2 = "../output/" & mlFILENAME
        Using mlOBJGS_CSVWRITER As New StreamWriter(mlPATH)
            mlOBJGS_CSVWRITER.Write(mlOBJGS_CSV.CsvFromDatatable(mlDATATABLECSV))
        End Using

        mlOBJGS.CloseDataSet(mlDATASETCSV)
        mlOBJGS.CloseDataAdapter(mlDATAADAPTERCSV)

        mlLINKDOC.Visible = True
        mlLINKDOC.Text = "<font Color=blue> Click to Download your Document (.csv) </font>"
        mlLINKDOC.NavigateUrl = mlPATH2
        mlLINKDOC.Attributes.Add("onClick", "window.open('" & mlPATH2 & "','','');")

        'System.Diagnostics.Process.Start(mlPATH)
    End Sub
   
    Function ReportField(ByVal mpTYPE As String, ByVal mpREPORTTYPE As String, ByVal mpFIELDTYPE As String, ByVal mpLINNO As Integer) As String
        ReportField = ""
        
        Select Case mpREPORTTYPE
            Case "ID-B-10PB"
                Select Case mpFIELDTYPE
                    Case "R"
                        Select Case mpLINNO
                            Case "1"
                                ReportField = " ([G_L Account No_] = '4010-0001')"
                                If mpTYPE = "COA" Then
                                    ReportField = "4010-0001"
                                End If
                            Case "2"
                                ReportField = " ([G_L Account No_] = '4020-0002')"
                                If mpTYPE = "COA" Then
                                    ReportField = "4020-0002"
                                End If
                            Case "3"
                                ReportField = " ([G_L Account No_] = '4030-0003')"
                                If mpTYPE = "COA" Then
                                    ReportField = "4030-0003"
                                End If
                            Case "4"
                                ReportField = " ([G_L Account No_] = '4050-0005')"
                                If mpTYPE = "COA" Then
                                    ReportField = "4050-0005"
                                End If
                            Case "5"
                                ReportField = " ([G_L Account No_] = '4100-0001' OR [G_L Account No_] ='4100-0002' OR [G_L Account No_] ='4100-0003')"
                                If mpTYPE = "COA" Then
                                    ReportField = "4100-0001"
                                End If
                            Case "6"
                                ReportField = " ([G_L Account No_] = '4999-9999')"
                                If mpTYPE = "COA" Then
                                    ReportField = "4999-9999"
                                End If
                        End Select
                        
                    Case "W"
                        Select Case mpLINNO
                            Case "1"
                                ReportField = " ([G_L Account No_] = '5010-1001') "
                                If mpTYPE = "COA" Then
                                    ReportField = "5010-1001"
                                End If
                            Case "2"
                                ReportField = " ([G_L Account No_] = '5010-1002')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5010-1002"
                                End If
                            Case "3"
                                ReportField = " ([G_L Account No_] = '5010-1003')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5010-1003"
                                End If
                            Case "4"
                                ReportField = " ([G_L Account No_] = '5010-1004')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5010-1004"
                                End If
                            Case "5"
                                ReportField = " ([G_L Account No_] = '5010-1005')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5010-1005"
                                End If
                            Case "6"
                                ReportField = " ([G_L Account No_] = '5010-1006')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5010-1006"
                                End If
                            Case "7"
                                ReportField = " ([G_L Account No_] = '5010-1007')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5010-1007"
                                End If
                            Case "8"
                                ReportField = " ([G_L Account No_] = '5010-1008')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5010-1008"
                                End If
                            Case "9"
                                ReportField = " ([G_L Account No_] = '5010-1009')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5010-1009"
                                End If
                            Case "10"
                                ReportField = " ([G_L Account No_] = '5010-1010')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5010-1010"
                                End If
                            Case "11"
                                ReportField = " ([G_L Account No_] = '5010-1012')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5010-1012"
                                End If
                        End Select
                        
                    Case "C"
                        Select Case mpLINNO
                            Case "1"
                                ReportField = " ([G_L Account No_] = '5020-1999' OR [G_L Account No_] ='5020-2999')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5020-1999"
                                End If
                            Case "2"
                                ReportField = " ([G_L Account No_] = '5030-1001')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5030-1001"
                                End If
                            Case "3"
                                ReportField = " ([G_L Account No_] = '5040-1001')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5040-1001"
                                End If
                            Case "4"
                                ReportField = " ([G_L Account No_] = '5040-1002' OR [G_L Account No_] ='5040-1005')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5040-1002"
                                End If
                            Case "5"
                                ReportField = " ([G_L Account No_] = '5040-1003')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5040-1003"
                                End If
                            Case "6"
                                ReportField = " ([G_L Account No_] = '5040-1004')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5040-1004"
                                End If
                            Case "7"
                                ReportField = " ([G_L Account No_] = '5050-1001')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5050-1001"
                                End If
                            Case "8"
                                ReportField = " ([G_L Account No_] = '5050-2001' OR [G_L Account No_] ='5050-3001')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5050-2001"
                                End If
                            Case "9"
                                ReportField = " ([G_L Account No_] = '5060-1001')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5060-1001"
                                End If
                            Case "10"
                                ReportField = " ([G_L Account No_] = '5060-1002')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5060-1002"
                                End If
                            Case "11"
                                ReportField = " ([G_L Account No_] = '5060-1003')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5060-1003"
                                End If
                            Case "12"
                                ReportField = " ([G_L Account No_] = '5060-1004')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5060-1004"
                                End If
                            Case "13"
                                ReportField = " ([G_L Account No_] = '5060-1005')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5060-1005"
                                End If
                            Case "14"
                                ReportField = " ([G_L Account No_] = '5060-1006')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5060-1006"
                                End If
                            Case "15"
                                ReportField = " ([G_L Account No_] = '5060-1007')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5060-1007"
                                End If
                            Case "16"
                                ReportField = " ([G_L Account No_] = '5060-1008')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5060-1008"
                                End If
                            Case "17"
                                ReportField = " ([G_L Account No_] = '5070-1001')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5070-1001"
                                End If
                            Case "18"
                                ReportField = " ([G_L Account No_] = '5070-1002')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5070-1002"
                                End If
                            Case "19"
                                ReportField = " ([G_L Account No_] = '5070-1003')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5070-1003"
                                End If
                            Case "20"
                                ReportField = " ([G_L Account No_] = '5070-1004')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5070-1004"
                                End If
                            Case "21"
                                ReportField = " ([G_L Account No_] = '5070-1005')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5070-1005"
                                End If
                            Case "22"
                                ReportField = " ([G_L Account No_] = '5070-1006' OR [G_L Account No_] ='5080-2001' OR [G_L Account No_] ='5080-2002')"
                                If mpTYPE = "COA" Then
                                    ReportField = "5070-1006"
                                End If
                        End Select
                End Select
        End Select
    End Function
    
    Function ReportFieldGet2(ByVal mpTYPE As String, ByVal mpREPORTTYPE As String, ByVal mpLINNO As String) As String
        ReportFieldGet2 = ""
        
        Select Case mpREPORTTYPE
            Case "ID-B-10PB"
                Select Case mpLINNO
                    Case "1"
                        ReportFieldGet2 = "R,1"
                    Case "2"
                        ReportFieldGet2 = "R,2"
                    Case "3"
                        ReportFieldGet2 = "R,3"
                    Case "4"
                        ReportFieldGet2 = "R,4"
                    Case "5"
                        ReportFieldGet2 = "R,5"
                    Case "6"
                        ReportFieldGet2 = "W,1"
                    Case "7"
                        ReportFieldGet2 = "W,2"
                    Case "8"
                        ReportFieldGet2 = "W,3"
                    Case "9"
                        ReportFieldGet2 = "W,4"
                    Case "10"
                        ReportFieldGet2 = "W,5"
                    Case "11"
                        ReportFieldGet2 = "W,6"
                    Case "12"
                        ReportFieldGet2 = "W,7"
                    Case "13"
                        ReportFieldGet2 = "W,8"
                    Case "14"
                        ReportFieldGet2 = "W,9"
                    Case "15"
                        ReportFieldGet2 = "W,10"
                    Case "16"
                        ReportFieldGet2 = "C,1"
                    Case "17"
                        ReportFieldGet2 = "C,2"
                    Case "18"
                        ReportFieldGet2 = "C,3"
                    Case "19"
                        ReportFieldGet2 = "C,4"
                    Case "20"
                        ReportFieldGet2 = "C,5"
                    Case "21"
                        ReportFieldGet2 = "C,6"
                    Case "22"
                        ReportFieldGet2 = "C,7"
                    Case "23"
                        ReportFieldGet2 = "C,8"
                    Case "24"
                        ReportFieldGet2 = "C,9"
                    Case "25"
                        ReportFieldGet2 = "C,10"
                    Case "26"
                        ReportFieldGet2 = "C,11"
                    Case "27"
                        ReportFieldGet2 = "C,12"
                    Case "28"
                        ReportFieldGet2 = "C,13"
                    Case "29"
                        ReportFieldGet2 = "C,14"
                    Case "30"
                        ReportFieldGet2 = "C,15"
                    Case "31"
                        ReportFieldGet2 = "C,16"
                    Case "32"
                        ReportFieldGet2 = "C,17"
                    Case "33"
                        ReportFieldGet2 = "C,18"
                    Case "34"
                        ReportFieldGet2 = "C,19"
                    Case "35"
                        ReportFieldGet2 = "C,20"
                    Case "36"
                        ReportFieldGet2 = "C,21"
                End Select
        End Select
    End Function
    
    Function ReportFieldGet(ByVal mpTYPE As String, ByVal mpREPORTTYPE As String, ByVal mpCOA_NO As String) As String
        ReportFieldGet = ""
        
        Select Case mpREPORTTYPE
            Case "ID-B-10PB"
                Select Case mpCOA_NO
                    Case "4010-0001"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "R"
                            Case "linno"
                                ReportFieldGet = "1"
                        End Select
                    Case "4020-0002"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "R"
                            Case "linno"
                                ReportFieldGet = "2"
                        End Select
                    Case "4030-0003"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "R"
                            Case "linno"
                                ReportFieldGet = "3"
                        End Select
                    Case "4050-0005"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "R"
                            Case "linno"
                                ReportFieldGet = "4"
                        End Select
                    Case "4100-0001", "4100-0002", "4100-0003"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "R"
                            Case "linno"
                                ReportFieldGet = "5"
                        End Select
                    Case "4999-9999"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "R"
                            Case "linno"
                                ReportFieldGet = "6"
                        End Select
                        
                    Case "5010-1001"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "W"
                            Case "linno"
                                ReportFieldGet = "1"
                        End Select
                    Case "5010-1002"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "W"
                            Case "linno"
                                ReportFieldGet = "2"
                        End Select
                        
                    Case "5010-1003"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "W"
                            Case "linno"
                                ReportFieldGet = "3"
                        End Select
                        
                    Case "5010-1004"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "W"
                            Case "linno"
                                ReportFieldGet = "4"
                        End Select
                        
                    Case "5010-1005"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "W"
                            Case "linno"
                                ReportFieldGet = "5"
                        End Select
                    
                    Case "5010-1006"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "W"
                            Case "linno"
                                ReportFieldGet = "6"
                        End Select
                        
                    Case "5010-1007"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "W"
                            Case "linno"
                                ReportFieldGet = "7"
                        End Select
                        
                    Case "5010-1008"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "W"
                            Case "linno"
                                ReportFieldGet = "8"
                        End Select
                        
                    Case "5010-1009"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "W"
                            Case "linno"
                                ReportFieldGet = "9"
                        End Select
                    
                    Case "5010-1010"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "W"
                            Case "linno"
                                ReportFieldGet = "10"
                        End Select
                        
                    Case "5010-1012"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "W"
                            Case "linno"
                                ReportFieldGet = "11"
                        End Select
                        
                    Case "5020-1999", "5020-2999"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "C"
                            Case "linno"
                                ReportFieldGet = "1"
                        End Select
                        
                    Case "5030-1001"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "C"
                            Case "linno"
                                ReportFieldGet = "2"
                        End Select
                        
                    Case "5040-1001"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "C"
                            Case "linno"
                                ReportFieldGet = "3"
                        End Select
                        
                    Case "5040-1002", "5040-1005"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "C"
                            Case "linno"
                                ReportFieldGet = "4"
                        End Select
                        
                    Case "5040-1003"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "C"
                            Case "linno"
                                ReportFieldGet = "5"
                        End Select
                        
                    Case "5040-1004"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "C"
                            Case "linno"
                                ReportFieldGet = "6"
                        End Select
                        
                    Case "5050-1001"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "C"
                            Case "linno"
                                ReportFieldGet = "7"
                        End Select
                        
                    Case "5050-2001", "5050-3001"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "C"
                            Case "linno"
                                ReportFieldGet = "8"
                        End Select
                        
                    Case "5060-1001"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "C"
                            Case "linno"
                                ReportFieldGet = "9"
                        End Select
                        
                    Case "5060-1002"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "C"
                            Case "linno"
                                ReportFieldGet = "10"
                        End Select
                        
                    Case "5060-1003"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "C"
                            Case "linno"
                                ReportFieldGet = "11"
                        End Select
                        
                    Case "5060-1004"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "C"
                            Case "linno"
                                ReportFieldGet = "12"
                        End Select
                                                    
                    Case "5060-1005"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "C"
                            Case "linno"
                                ReportFieldGet = "13"
                        End Select
                        
                    Case "5060-1006"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "C"
                            Case "linno"
                                ReportFieldGet = "14"
                        End Select
                
                    Case "5060-1007"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "C"
                            Case "linno"
                                ReportFieldGet = "15"
                        End Select
                        
                    Case "5060-1008"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "C"
                            Case "linno"
                                ReportFieldGet = "16"
                        End Select
                        
                    Case "5070-1001"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "C"
                            Case "linno"
                                ReportFieldGet = "17"
                        End Select
                        
                    Case "5070-1002"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "C"
                            Case "linno"
                                ReportFieldGet = "18"
                        End Select
                        
                    Case "5070-1003"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "C"
                            Case "linno"
                                ReportFieldGet = "19"
                        End Select
                        
                    Case "5070-1004"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "C"
                            Case "linno"
                                ReportFieldGet = "20"
                        End Select
                        
                    Case "5070-1005"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "C"
                            Case "linno"
                                ReportFieldGet = "21"
                        End Select
                        
                    Case "5070-1006", "5080-2001", "5080-2002"
                        Select Case mpTYPE
                            Case "fieldtype"
                                ReportFieldGet = "C"
                            Case "linno"
                                ReportFieldGet = "22"
                        End Select
                End Select
        End Select
    End Function
    
    
    Function SQL_COA(ByVal mpTABLENAME As String, ByVal mpSITEID As String, ByVal mpCOA As String, ByVal mpREPORTTYPE As String, ByVal mpFIELDTYPE As String, ByVal mpLINNO As Integer) As String
        Dim mlSQLT As String
        mlSQLT = ""
        
        mlSQLT = "SELECT DISTINCT [G_L Account No_], Name FROM " & mpTABLENAME & _
                " WHERE [G_L Account No_] in (" & _
                SQL_SerialCOA() & _
                " )" & _
                " ORDER BY [G_L Account No_]"
        Return mlSQLT
    End Function
    
    Function SQL_SITE(ByVal mpTABLENAME As String, ByVal mpSITEID As String, ByVal mpTYPE As String, ByVal mpREPORTTYPE As String, ByVal mpFIELDTYPE As String, ByVal mpLINNO As Integer) As String
        Dim mlSQLT As String
        mlSQLT = ""
        
        mlSQLT = " '" & mpSITEID & "' =(" & _
                " SELECT Sum(Amount) FROM " & mpTABLENAME & " WHERE " & _
                " ([Others Dimension 24 Code] = '" & mpSITEID & "' OR [CustServiceSiteLineNo] = '" & mpSITEID & "')" & _
                " AND (" & _
                ReportField("", mpREPORTTYPE, mpFIELDTYPE, mpLINNO) & _
                " ))"
        Return mlSQLT
    End Function
  
    
    Function SQL_SerialCOA() As String
        Dim mlSQLT As String
        mlSQLT = ""
        
        mlSQLT = "" & _
                "'" & ReportField("COA", "ID-B-10PB", "R", "1") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "R", "2") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "R", "3") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "R", "4") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "R", "5") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "R", "6") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "W", "1") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "W", "2") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "W", "3") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "W", "4") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "W", "5") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "W", "6") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "W", "7") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "W", "8") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "W", "9") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "W", "10") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "W", "11") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "C", "1") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "C", "2") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "C", "3") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "C", "4") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "C", "5") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "C", "6") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "C", "7") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "C", "8") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "C", "9") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "C", "10") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "C", "11") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "C", "12") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "C", "13") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "C", "14") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "C", "15") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "C", "16") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "C", "17") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "C", "18") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "C", "19") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "C", "20") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "C", "21") & "'," & _
                "'" & ReportField("COA", "ID-B-10PB", "C", "22") & "'" & _
                ""
        Return mlSQLT
        
    End Function
    
    Function SQL_TOTAL(ByVal mpTYPE As String, ByVal mpTABLENAME As String, ByVal mpSITE As String, ByVal mpLEVELGROUP As Integer) As String
        'N = Normal
        'S = By Site
        Dim mlSQLT As String
        Dim mlSQLSITE As String
        mlSQLT = ""
                
        mlSQLSITE = ""
        Select Case mpTYPE
            Case "S"
                mlSQLSITE = " AND ([Others Dimension 24 Code] = '" & mpSITE & "' OR [CustServiceSiteLineNo] = '" & mpSITE & "')"
        End Select
        
        
        Select Case mpTYPE
            Case "", "N"
                mlSQLT = mlSQLT & " SELECT '4999-9999' as R_Account, 'Total Revenue' as 'Total_Revenue',R_Amount="
                mpLEVELGROUP = "1"
        End Select
        If mpLEVELGROUP = "1" Then
            mlSQLT = mlSQLT & " ("
            mlSQLT = mlSQLT & " SELECT Sum(Amount) FROM " & mpTABLENAME & " WHERE"
            mlSQLT = mlSQLT & "("
            mlSQLT = mlSQLT & ReportField("", "ID-B-10PB", "R", "1")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "R", "2")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "R", "3")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "R", "4")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "R", "5")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "R", "6")
            mlSQLT = mlSQLT & ")"
            mlSQLT = mlSQLT & mlSQLSITE
            mlSQLT = mlSQLT & ")"
        End If
        
        Select Case mpTYPE
            Case "", "N"
                mlSQLT = mlSQLT & ","
                mlSQLT = mlSQLT & " 'W01..W99' as W_Account, 'Total Wages' as 'Total_Wages' ,W_Amount="
                mpLEVELGROUP = "2"
        End Select
        If mpLEVELGROUP = "2" Then
            mlSQLT = mlSQLT & " ("
            mlSQLT = mlSQLT & " SELECT Sum(Amount) FROM " & mpTABLENAME & " WHERE"
            mlSQLT = mlSQLT & "("
            mlSQLT = mlSQLT & ReportField("", "ID-B-10PB", "W", "1")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "W", "2")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "W", "3")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "W", "4")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "W", "5")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "W", "6")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "W", "7")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "W", "8")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "W", "9")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "W", "10")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "W", "11")
            mlSQLT = mlSQLT & ")"
            mlSQLT = mlSQLT & mlSQLSITE
            mlSQLT = mlSQLT & ")"
        End If
        
        Select Case mpTYPE
            Case "", "N"
                mlSQLT = mlSQLT & ","
                mlSQLT = mlSQLT & " 'DPC00..DPC9' as C_Account, 'TOTAL DIRECT PRODUCT COST' as 'TOTAL_PDC' ,C_Amount="
                mpLEVELGROUP = "3"
        End Select
        If mpLEVELGROUP = "3" Then
            mlSQLT = mlSQLT & " ("
            mlSQLT = mlSQLT & " SELECT Sum(Amount) FROM " & mpTABLENAME & " WHERE"
            mlSQLT = mlSQLT & "("
            mlSQLT = mlSQLT & ReportField("", "ID-B-10PB", "C", "1")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "C", "2")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "C", "3")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "C", "4")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "C", "5")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "C", "6")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "C", "7")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "C", "8")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "C", "9")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "C", "10")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "C", "11")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "C", "12")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "C", "13")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "C", "14")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "C", "15")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "C", "16")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "C", "17")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "C", "18")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "C", "19")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "C", "20")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "C", "21")
            mlSQLT = mlSQLT & " OR" & ReportField("", "ID-B-10PB", "C", "22")
            mlSQLT = mlSQLT & ")"
            mlSQLT = mlSQLT & mlSQLSITE
            mlSQLT = mlSQLT & ")"
        End If
        Return mlSQLT
    End Function
    
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        QuerytoTable()
    End Sub
    
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        QuerytoTable()
        SearchRecord()
    End Sub
    
    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        LoadComboData()
        LoadComboData2()
    End Sub
</script>
   
<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
<link href="../script/calendar.css" rel="stylesheet" type="text/css" media="all" />
<script type="text/javascript" src="../script/calendar.js"></script>

<script type="text/javascript" language="Javascript">
<!-- hide script from older browsers
function openwindow(url,nama,width,height)
{
OpenWin = this.open(url, nama);
if (OpenWin != null)
{
  if (OpenWin.opener == null)
  OpenWin.opener=self;
}
OpenWin.focus();
} 
// End hiding script-->
</script>

<form id="form1" runat="server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ToolkitScriptManager1" />

<asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">
<asp:TableRow>   
<asp:TableCell> 
<asp:Panel ID="pnTOOLBAR" runat="server">  
    <table border="0" cellpadding="3" cellspacing="3">
        <tr>            
            <td><asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" /></td>
            <td><asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" /></td>            
            <td><asp:ImageButton id="btExCsv" ToolTip="csv" ImageUrl="~/images/toolbar/csvfile.png" runat="server" /></td>            
        </tr>               
    </table>
    <hr />
</asp:Panel>
</asp:TableCell>    
</asp:TableRow>

<asp:TableRow><asp:TableCell><p class="header1"><b><asp:Label id="mlTITLE" runat="server"></asp:Label></b></p></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><asp:HiddenField ID="mlSYSCODE" runat="server"/></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><p><asp:HyperLink ID="mlLINKDOC" runat="server"></asp:HyperLink></p></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><asp:Label id="mlSQLSTATEMENT" runat="server" Visible="False" /></asp:TableCell></asp:TableRow>

<asp:TableRow>
<asp:TableCell BorderWidth="0">
<asp:Panel ID="pnFILL" runat="server">   

<table width="80%" cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td><asp:Label ID="lbDOCDATE1" Text="Posting Date (From)" runat="server"></asp:Label></td>
        <td>:</td>
        <td>
            <asp:TextBox ID="mlDOCDATE1" runat="server" Width="100"></asp:TextBox>                                                                                                          
            <input id="btDOCDATE1" runat="server" onclick="displayCalendar(ctl00_mpCONTENT_mlDOCDATE1,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow" />                
            <ajaxtoolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="mlDOCDATE1" TargetControlID="mlDOCDATE1" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender> 
            <font color="blue">dd/mm/yyyy</font>
        </td>
    </tr>
    
    <tr>    
        <td><asp:Label ID="lbDOCDATE2" Text="Posting Date (To)" runat="server"></asp:Label></td>
        <td>:</td>
        <td>                
            <asp:TextBox ID="mlDOCDATE2" runat="server" Width="100"></asp:TextBox>                                                                                                          
            <input id="btDOCDATE2" runat="server" onclick="displayCalendar(ctl00_mpCONTENT_mlDOCDATE2,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow" />                
            <ajaxtoolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="mlDOCDATE2" TargetControlID="mlDOCDATE2" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender> 
            <font color="blue">dd/mm/yyyy</font>
        </td>
    </tr>        
    
    <tr>
        <td><p>Branch</p></td>            
        <td>:</td>
        <td><asp:DropDownList ID="mpBRANCH" runat="server"></asp:DropDownList></td>
    </tr>
    
    <tr>
        <td><p>Site</p></td>            
        <td>:</td>
        <td><asp:DropDownList ID="mpSITE" runat="server"></asp:DropDownList></td>
    </tr>
    
    <tr>
        <td><p>Jumlah Site Card Maksimal</p></td>            
        <td>:</td>
        <td><asp:DropDownList ID="mpMAXSITEQTY" runat="server"></asp:DropDownList></td>
    </tr>
    
    <tr>
        <td><p>Report Type</p></td>            
        <td>:</td>
        <td><asp:DropDownList ID="mpREPORTTYPE" runat="server"></asp:DropDownList></td>
    </tr>
    
    <tr>
        <td valign="top"><p>Retrieve Data to Local</p></td>            
        <td valign="top">:</td>
        <td>
            <asp:Button ID="Button1" runat="server" Text="Retrieve Data to Local Table" /><br />
            <asp:Button ID="Button2" runat="server" Text="Retrieve Data and Preview Report" /><br />
            <asp:Button ID="Button3" runat="server" Text="Load Drop Box Data" />
        </td>
    </tr>
        
</table>  

</asp:Panel>
</asp:TableCell>
</asp:TableRow>

<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="Panel1" runat="server">
    <br /><hr /><br />
    <% If mlHAVINGDETAIL = True Then%>
    <table cellspacing="5" cellpadding="5" border="1" bordercolor="#B0AEAE">
    <%
        Response.Write("<tr bgcolor=#C4C4C4>")
        Response.Write("<td><p><b>Account</b></p></td>")
        Response.Write("<td><p><b>Description</b></p></td>")
        Response.Write("<td><p><b>Total</b></p></td>")
        Response.Write("<td><p><b>%</b></p></td>")
        mlI = 0
        For mlI = 0 To mlJUMLAHSITE - 1
            mlCURRENTSITE = Trim(mlOBJGF.GetStringAtPosition(mlSITE, mlI, ","))
            Response.Write("<td><p><b>" & mlCURRENTSITE & "</b></p></td>")
            Response.Write("<td><p><b>%</b></p>")
        Next
        Response.Write("</tr>")
    %>                    



    <%
        Dim mlCOATYPE3 As String
        Dim mlLINENUMBER As Integer
        Dim mlTOTAL3_ As Double
        
        mlCOATYPE3 = ""
        mlLINENUMBER = 0
        
        
        If mlRS_SITE.IsClosed = False Then
            While mlRS_SITE.Read
                Dim mlCOATYPE As String
                Dim mlTOTAL2_ As Double
                mlCOATYPE = ReportFieldGet("fieldtype", "ID-B-10PB", mlRS_SITE("G_L Account No_"))
                Select Case mlCOATYPE
                    Case "R"
                        mlTOTAL_ = mlTOTAL_REVENUE
                    Case "W"
                        mlTOTAL_ = mlTOTAL_WAGES
                    Case "C"
                        mlTOTAL_ = mlTOTAL_COST
                End Select
                
                If (mlCOATYPE3 <> mlCOATYPE) Then
                    If mlCOATYPE3 = "" Then
                        mlCOATYPE3 = mlCOATYPE
                        mlRSSITETOTAL.Read()
                    Else
                        
                        mlLINENUMBER = mlLINENUMBER + 1
                        If mlLINENUMBER Mod 2 <> 0 Then
                            Response.Write("<tr bgcolor=" & mlGRIDCOLOR1 & ">")
                        Else
                            Response.Write("<tr bgcolor=" & mlGRIDCOLOR2 & ">")
                        End If
                        Response.Write("<td><p><b>" & mlRSSITETOTAL("G_L Account No_") & "</b></p></td>")
                        Response.Write("<td><p><b>" & mlRSSITETOTAL("Name_") & "</b></p></td>")
                        Response.Write("<td><p><b>" & mlTOTAL_ & "</b></p></td>")
                        Response.Write("<td align=right><p><b>100</b></p></td>")
                        mlI = 0
                        For mlI = 0 To mlJUMLAHSITE - 1
                            mlCURRENTSITE = Trim(mlOBJGF.GetStringAtPosition(mlSITE, mlI, ","))
                            mlTOTAL3_ = IIf(IsDBNull(mlRSSITETOTAL(mlCURRENTSITE)) = True, 0, mlRSSITETOTAL(mlCURRENTSITE))
                            Response.Write("<td align=right><p><b>" & Math.Round(CDbl(mlTOTAL3_)).ToString("n") & "</b></p></td>")
                            Response.Write("<td align=right><p><b>100</b></p></td>")
                        Next
                        Response.Write("</tr>")
                        
                        Response.Write("<tr>")
                        Response.Write("<td colspan= 4 + mlJUMLAHSITE><br></td>")
                        Response.Write("</tr>")
                        
                        mlCOATYPE3 = mlCOATYPE
                        mlRSSITETOTAL.Read()
                    End If
                End If
                
                
                mlLINENUMBER = mlLINENUMBER + 1
                If mlLINENUMBER Mod 2 <> 0 Then
                    Response.Write("<tr bgcolor=" & mlGRIDCOLOR1 & ">")
                Else
                    Response.Write("<tr bgcolor=" & mlGRIDCOLOR2 & ">")
                End If
                Response.Write("<td><p>" & mlRS_SITE("G_L Account No_") & "</p></td>")
                Response.Write("<td><p>" & mlRS_SITE("Name_") & "</p></td>")
                Response.Write("<td><p>" & mlRS_SITE("Total") & "</p></td>")
                Response.Write("<td align=right><p>" & Math.Round(CDbl((mlRS_SITE("Total") / mlTOTAL_) * 100), 2).ToString("n") & "</p></td>")
    
                mlI = 0
                For mlI = 0 To mlJUMLAHSITE - 1
                    mlCURRENTSITE = Trim(mlOBJGF.GetStringAtPosition(mlSITE, mlI, ","))
                    mlTOTAL_ = IIf(IsDBNull(mlRSSITETOTAL(mlCURRENTSITE)) = True, 0, mlRSSITETOTAL(mlCURRENTSITE))
                    mlTOTAL2_ = IIf(IsDBNull(mlRS_SITE(mlCURRENTSITE)) = True, 0, mlRS_SITE(mlCURRENTSITE))
                    Response.Write("<td align=right><p>" & Math.Round(CDbl(mlTOTAL2_), 2).ToString("n") & "</p></td>")
                    Response.Write("<td align=right><p>" & Math.Round(CDbl((mlTOTAL2_ / mlTOTAL_) * 100), 2).ToString("n") & "</p></td>")
                Next
                Response.Write("</tr>")
            End While
            
            'Add
            mlLINENUMBER = mlLINENUMBER + 1
            If mlLINENUMBER Mod 2 <> 0 Then
                Response.Write("<tr bgcolor=" & mlGRIDCOLOR1 & ">")
            Else
                Response.Write("<tr bgcolor=" & mlGRIDCOLOR2 & ">")
            End If
            Response.Write("<td><p><b>" & mlRSSITETOTAL("G_L Account No_") & "</b></p></td>")
            Response.Write("<td><p><b>" & mlRSSITETOTAL("Name_") & "</b></p></td>")
            Response.Write("<td><p><b>" & mlTOTAL_ & "</b></p></td>")
            Response.Write("<td align=right><p><b>100</b></p></td>")
            mlI = 0
            For mlI = 0 To mlJUMLAHSITE - 1
                mlCURRENTSITE = Trim(mlOBJGF.GetStringAtPosition(mlSITE, mlI, ","))
                mlTOTAL3_ = IIf(IsDBNull(mlRSSITETOTAL(mlCURRENTSITE)) = True, 0, mlRSSITETOTAL(mlCURRENTSITE))
                Response.Write("<td align=right><p><b>" & Math.Round(CDbl(mlTOTAL3_)).ToString("n") & "</b></p></td>")
                Response.Write("<td align=right><p><b>100</b></p></td>")
            Next
            Response.Write("</tr>")
                        
            Response.Write("<tr>")
            Response.Write("<td colspan= 4 + mlJUMLAHSITE><br></td>")
            Response.Write("</tr>")
            'End Add
            
            mlOBJGS.CloseFile(mlRSSITE1)
            mlOBJGS.CloseFile(mlRSCOA)
            mlOBJGS.CloseFile(mlRS_SITE)
            mlOBJGS.CloseFile(mlRSSITETOTAL)
            mlOBJGS.CloseFile(mlRS_TOTAL)
        End If
        
    %>       

    </table>
    <%End If %>
</asp:Panel>
</asp:TableCell>
</asp:TableRow>

<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnGRID" runat="server">
    <asp:DataGrid runat="server" ID="mlDATAGRID"
    HeaderStyle-BackColor="orange"  
    HeaderStyle-VerticalAlign ="top"
    HeaderStyle-HorizontalAlign="Center"    
    HeaderStyle-ForeColor="White"
    HeaderStyle-Font-Bold="True"
    AlternatingItemStyle-BackColor="#EFEFEF"
    OnItemCommand="mlDATAGRID_ItemCommand"
    
    AutoGenerateColumns = "true"
    ShowHeader="True"    
    AllowSorting="True"
    OnSortCommand="mlDATAGRID_SortCommand"    
    OnItemDataBound ="mlDATAGRID_ItemBound"
    
    AllowPaging="True"    
    PagerStyle-Mode="NumericPages"
    PagerStyle-HorizontalAlign="center"
    OnPageIndexChanged="mlDATAGRID_PageIndex"    
    >	    
    
    <AlternatingItemStyle BackColor="#F9FCA8" />
    <Columns>  
         
    </Columns>
 </asp:DataGrid>  
 
</asp:Panel>
</asp:TableCell>
</asp:TableRow>

</asp:Table>

<br /><br /><br /><br />
</form>
</asp:Content>

