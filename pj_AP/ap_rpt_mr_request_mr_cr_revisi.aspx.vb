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
Partial Class pj_AP_ap_rpt_mr_request_mr_cr_revisi
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJPJ As New ModuleFunctionLocal

    Dim mlKEY As String
    Dim mlSQL As String
    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlDATAADAPTER As OleDb.OleDbDataAdapter
    Dim mlDATASET As New DataSet
    Dim mlDATATABLE As New DataTable
    Dim mlSQL2 As String
    Dim mlREADER2 As OleDb.OleDbDataReader
    Dim mlRSTEMP As OleDb.OleDbDataReader
    Dim mlSQLTEMP As String
    Dim mlSQLLEVEL As String

    Dim mlRECORDSTATUS As String
    Dim mlSQLRECORDSTATUS As String
    Dim mlFUNCTIONPARAMETER As String
    Dim mlI As Byte
    Dim mlCURRENTDATE As String = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()



    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        CekSession()
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Material Requisition Document V2.01"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Material Requisition Report V2.01"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlFUNCTIONPARAMETER = Request.QueryString("mpFP")
        If Not Page.IsPostBack Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ap_rpt_mr_request", "MR Rpt", "")
            LoadComboData()
            btExCsv.Visible = False
        End If

        hpLookupJobTask_From.NavigateUrl = "javascript:OpenWinLookUpSiteCard_From('" + txLOCID1.ClientID + "','" + txLOCID1_Desc.ClientID + "','" + hdnSiteCardID_From.ClientID + "','" + hdnSiteCardName_From.ClientID + "','" + mpJobNo_From.ClientID + "','" + mpJobTaskNo_From.ClientID + "','" + hdnJobNo_From.ClientID + "','" + hdnJobTaskNo_From.ClientID + "','" + ddlEntity.ClientID + "','AccMnt')"
        hpLookupJobTask_TO.NavigateUrl = "javascript:OpenWinLookUpSiteCard_To('" + txLOCID2.ClientID + "','" + txLOCID2_Desc.ClientID + "','" + hdnSiteCardID_TO.ClientID + "','" + hdnSiteCardName_TO.ClientID + "','" + mpJobNo_TO.ClientID + "','" + mpJobTaskNo_TO.ClientID + "','" + hdnJobNo_TO.ClientID + "','" + hdnJobTaskNo_TO.ClientID + "','" + ddlEntity.ClientID + "','AccMnt')"

    End Sub

    Protected Sub CekSession()
        If Session("mgMENUSTYLE") = "" Then
            Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=34FC35D4")
            Return
        End If
    End Sub
    Sub ClearFields()
        txDOCDATE1.Text = mlCURRENTDATE
        txDOCDATE2.Text = mlCURRENTDATE
        txDOCUMENTNO1.Text = ""
        txDOCUMENTNO2.Text = ""
        txLOCID1.Text = ""
        txLOCID2.Text = ""
        txLOCID1_Desc.Text = ""
        txLOCID2_Desc.Text = ""
        txUSERID.Text = ""
        lbNAME.Text = ""
        mlLINKDOC.Text = ""
    End Sub


    Protected Sub btCancelOperation_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btCancelOperation.Click
        CancelOperation()
    End Sub

    Public Sub RetrieveFields()
        ClearFields()
        DisableCancel()
    End Sub

    Sub RetrieveFieldsDetail(ByVal mpSQL As String)
        Try
            pnlGRID.Visible = True
            CrReport("", mpSQL, True)
        Catch ex As Exception
        End Try
    End Sub

    Sub DeleteRecord()
        mlRECORDSTATUS = "Delete"
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
        pnlFILL.Visible = True
        pnlGRID.Visible = False
        btExCsv.Visible = False
    End Sub

    Private Sub DisableCancel()
        pnlFILL.Visible = False
        pnlGRID.Visible = True
        btExCsv.Visible = False
    End Sub

    Sub CancelOperation()
        pnlFILL.Visible = True
        btSearchRecord.Visible = True
        btCancelOperation.Visible = True

        mlLINKDOC.Text = ""
    End Sub

    Sub CalculateTotal()
        Dim mlGRANDTOTALPOINT As Double
        Dim mlGRANDTOTALAMOUNT As Double

        mlGRANDTOTALPOINT = 0
        mlGRANDTOTALAMOUNT = 0
    End Sub

    Protected Sub btLOCID1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btLOCID1.Click
        txLOCID1_Desc.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_DESC", Trim(txLOCID1.Text), mlOBJGS.mgACTIVECOMPANY)
        '        txLOCID1_Desc.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_DESC", Trim(txLOCID1.Text))
    End Sub

    Protected Sub btLOCID2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btLOCID2.Click
        txLOCID2_Desc.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_DESC", Trim(txLOCID2.Text), mlOBJGS.mgACTIVECOMPANY)
        '        txLOCID2_Desc.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_DESC", Trim(txLOCID2.Text))
    End Sub


    Sub LoadComboData()
        txDOCDATE1.Text = mlCURRENTDATE
        txDOCDATE2.Text = mlCURRENTDATE

        ddSTATUS.Items.Clear()
        ddSTATUS.Items.Add("all")
        ddSTATUS.Items.Add("wait")
        ddSTATUS.Items.Add("wait1")
        ddSTATUS.Items.Add("wait2")
        ddSTATUS.Items.Add("wait3")
        ddSTATUS.Items.Add("New")
        ddSTATUS.Items.Add("Post")
        ddSTATUS.Items.Add("Delete")

        ddREPORT.Items.Clear()
        ddREPORT.Items.Add("MR Document")
        ddREPORT.Items.Add("MR Document Group By SiteCard")

        ddlEntity.Items.Clear()
        ddlEntity.Items.Add("Choose")
        mlSQLTEMP = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID='ISS_Entity'"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISS")
        While mlRSTEMP.Read
            ddlEntity.Items.Add(Trim(mlRSTEMP("LinCode")))
        End While

        'ddTYPE.Items.Clear()
        'ddTYPE.Items.Add("")
        'mlSQLTEMP = "SELECT DISTINCT ParentCode FROM AP_MR_REQUESTHR ORDER BY ParentCode"
        'mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
        'While mlRSTEMP.Read
        '    ddTYPE.Items.Add(Trim(mlRSTEMP("ParentCode")))
        'End While

        ddTYPE.Items.Clear()
        ddTYPE.Items.Add("No Type")

    End Sub

    Protected Sub btSearchRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSearchRecord.Click
        SearchRecord()
    End Sub

    Protected Sub btUSERID_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btUSERID.Click
        If mlOBJGF.IsNone(Trim(txUSERID.Text)) = False Then
            lbNAME.Text = mlOBJGS.ADGeneralLostFocus("USER", Trim(txUSERID.Text))
        End If
    End Sub

    Function MRLevel(ByVal mpUSERID As String) As Byte
        MRLevel = 0

        Try
            If mlSQLLEVEL <> "" Then
                mlSQLLEVEL = "SELECT Min(UserLevel)  as UserLevel FROM OP_USER_SITE HR WHERE " & mlSQLLEVEL
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLLEVEL, "PB", "ISSP3")
                If mlRSTEMP.HasRows Then
                    mlRSTEMP.Read()
                    If IsDBNull(mlRSTEMP("UserLevel")) Then
                        MRLevel = 0
                    Else
                        MRLevel = Convert.ToByte(mlRSTEMP("UserLevel"))
                    End If
                End If
            Else
                MRLevel = 3
            End If

        Catch ex As Exception
        End Try

        Return MRLevel
    End Function

    Sub SearchRecord()
        Try
            mlSQL = ""

            If txDOCDATE1.Text <> "" And txDOCDATE2.Text <> "" Then
                ' mlSQL = mlSQL & " HR.DocDate >= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE1.Text, "/")) & "' AND DocDate <= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE2.Text, "/")) & "' "
                mlSQL = mlSQL & " HR.DocDate >= '" & mlOBJGF.FormatDate(txDOCDATE1.Text) & "' AND DocDate <= '" & mlOBJGF.FormatDate(txDOCDATE2.Text) & "' "
            End If

            If mlDATEAPPRFROM.Text <> "" And mlDATEAPPRTO.Text <> "" Then
                'mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " (HR.PostingDate3 > = '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(mlDATEAPPRFROM.Text, "/")) & "' AND HR.PostingDate3 < = '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(mlDATEAPPRTO.Text, "/")) & "') "
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " (HR.PostingDate3 > = '" & mlOBJGF.FormatDate(mlDATEAPPRFROM.Text) & "' AND HR.PostingDate3 < = '" & mlOBJGF.FormatDate(mlDATEAPPRTO.Text) & "') "
            End If


            If ddlEntity.Text <> "Choose" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " EntityID = '" & Trim(ddlEntity.Text) & "' "
            End If

            If ddTYPE.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " ParentCode = '" & Trim(ddTYPE.Text) & "' "
            End If

            If txDOCUMENTNO1.Text <> "" And txDOCUMENTNO2.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.DocNo >= '" & txDOCUMENTNO1.Text & "' AND HR.DocNo <= '" & txDOCUMENTNO2.Text & "'"
            End If

            If mpPERIOD.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.BVMonth LIKE '" & Trim(mpPERIOD.Text) & "' "
            End If

            'If txLOCID1.Text <> "" And txLOCID2.Text <> "" Then
            '    mlSQLTEMP = "SELECT SiteCardID FROM OP_USER_SITE WHERE SiteCardID='ALL' AND UserID = '" & Session("mgUSERID") & "'"
            '    mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
            '    If mlRSTEMP.HasRows = True Then
            '        mlSQLTEMP = " HR.SiteCardID >= '" & txLOCID1.Text & "' AND HR.SiteCardID <= '" & txLOCID2.Text & "'"
            '        mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & mlSQLTEMP

            '    Else
            '        mlSQLTEMP = "  HR.SiteCardID IN " & _
            '                " (" & _
            '                " SELECT SiteCardID FROM OP_USER_SITE WHERE UserID = '" & Session("mgUSERID") & "' " & _
            '                " AND SiteCardID >= '" & txLOCID1.Text & "' AND SiteCardID <= '" & txLOCID2.Text & "'" & _
            '                " )"
            '        mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & mlSQLTEMP


            '    End If
            'End If

            If txLOCID1.Text <> "" And txLOCID2.Text <> "" Then
                mlSQLTEMP = " HR.SiteCardID >= '" & txLOCID1.Text & "' AND HR.SiteCardID <= '" & txLOCID2.Text & "'"
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & mlSQLTEMP
            End If

            If txUSERID.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " (HR.RecUserID LIKE '" & txUSERID.Text & "'  OR HR.PostingUserID1 LIKE '" & txUSERID.Text & "' OR " & _
                        "HR.PostingUserID2 LIKE '" & txUSERID.Text & "' OR HR.PostingUserID3 LIKE '" & txUSERID.Text & "')"
            End If

            If ddSTATUS.Text <> "all" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.RecordStatus LIKE '" & ddSTATUS.Text & "%'"
            End If


            If Not mlOBJGF.IsNone(mlSQL) Then
                Dim mlQUERY As String
                mlQUERY = ""
                Select Case ddREPORT.Text
                    Case "MR Document"
                        'mlSQL = "SELECT Distinct HR.DocNo,HR.DocDate,HR.MRType,HR.SiteCardID as Site,HR.SiteCardName as SiteDesc,HR.BVMonth as Period,HR.TotalAmount as Amount," & _
                        '" HR.PostingUserID1 as CreateID, HR.PostingName1 as CreateName, HR.RecordStatus as Status, " & _
                        '" DT.Linno as No,DT.ItemKey as Item, DT.Description as ItemDesc,DT.Uom,DT.Qty,DT.UnitPrice as Unit_Price,DT.TotalPrice as Total_Price," & _
                        '" DT.Qty_Bal as Saldo,DT.RequestDesc as Ukuran,DT.Description2 as Ket" & _
                        '" FROM AP_MR_REQUESTHR HR,AP_MR_REQUESTDT DT WHERE " & mlSQL & _
                        '" AND HR.DocNo = DT.DocNo"

                        mlQUERY = "SELECT Distinct " & vbCrLf
                        mlQUERY += " HR.ParentCode,HR.MRType,HR.MRDesciption,HR.DocNo,HR.DocDate,HR.SiteCardID,HR.SiteCardName,HR.Location," & vbCrLf
                        mlQUERY += " HR.DeptID,HR.BVMonth,HR.Description as DescHR,HR.MRLine,HR.PercentageMR,HR.TotalPoint,HR.TotalAmount," & vbCrLf
                        mlQUERY += " HR.RecordStatus,HR.PostingUserID1,HR.PostingName1,HR.PostingDate1,HR.PostingUserID2,HR.PostingName2,HR.PostingDate2," & vbCrLf
                        mlQUERY += " HR.PostingUserID3,HR.PostingName3,HR.PostingDate3,HR.PostingUserID4,HR.PostingName4,HR.PostingDate4,HR.PostingUserID5,HR.PostingName5,HR.PostingDate5," & vbCrLf
                        mlQUERY += " HR.RecUserID,HR.RecDate,HR.SC_State,HR.SC_Branch,HR.SC_BranchCode,HR.SC_BranchName,HR.DeptCode,HR.Do_Address,HR.Do_City,HR.Do_State,HR.Do_Country,HR.Do_Zip,HR.DO_Phone1,HR.PIC_ContactNo," & vbCrLf
                        mlQUERY += " DT.*, HR.JobNo,HR.JobTaskNo" & vbCrLf
                        mlQUERY += " FROM AP_MR_REQUESTHR HR,AP_MR_REQUESTDT DT WHERE " & mlSQL & vbCrLf
                        mlQUERY += " AND HR.DocNo = DT.DocNo" & vbCrLf

                    Case "MR Document Group By SiteCard"
                        mlQUERY = "SELECT Distinct " & vbCrLf
                        mlQUERY += " HR.ParentCode,HR.MRType,HR.MRDesciption,HR.DocNo,HR.DocDate,HR.SiteCardID,HR.SiteCardName,HR.Location," & vbCrLf
                        mlQUERY += " HR.DeptID,HR.BVMonth,HR.Description as DescHR,HR.MRLine,HR.PercentageMR,HR.TotalPoint,HR.TotalAmount," & vbCrLf
                        mlQUERY += " HR.RecordStatus,HR.PostingUserID1,HR.PostingName1,HR.PostingDate1,HR.PostingUserID2,HR.PostingName2,HR.PostingDate2," & vbCrLf
                        mlQUERY += " HR.PostingUserID3,HR.PostingName3,HR.PostingDate3,HR.PostingUserID4,HR.PostingName4,HR.PostingDate4,HR.PostingUserID5,HR.PostingName5,HR.PostingDate5," & vbCrLf
                        mlQUERY += " HR.RecUserID,HR.RecDate,HR.SC_State,HR.SC_Branch,HR.SC_BranchCode,HR.SC_BranchName,HR.DeptCode,HR.Do_Address,HR.Do_City,HR.Do_State,HR.Do_Country,HR.Do_Zip,HR.DO_Phone1,HR.PIC_ContactNo," & vbCrLf
                        mlQUERY += " DT.*, HR.JobNo,HR.JobTaskNo" & vbCrLf
                        mlQUERY += " FROM AP_MR_REQUESTHR HR,AP_MR_REQUESTDT DT WHERE " & mlSQL & vbCrLf
                        mlQUERY += " AND HR.DocNo = DT.DocNo" & vbCrLf

                End Select

                mlSQLSTATEMENT.Text = mlQUERY
                RetrieveFieldsDetail(mlSQLSTATEMENT.Text)
                pnlFILL.Visible = False
                btSearchRecord.Visible = False



            End If

        Catch ex As Exception
            mlMESSAGE.Text = ex.Message
        End Try
    End Sub

    Public Sub CrReport(ByVal mpDOCNO As String, ByVal mpSQL As String, ByVal mpEXPTOPDF As Boolean)
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

            mlREPORTNAME = ""
            mlFILENAME = ""
            Select Case ddREPORT.Text
                Case "MR Document"
                    '                    mlREPORTNAME = "../pj_rpt/rpt_crystalrpt/ap_doc_mr.rpt"
                    '                    mlFILENAME = "IAS_" & Left(Session("mguserid"), 3) & "_ap_doc_mr" & ".pdf"
                    mlREPORTNAME = "../pj_rpt/rpt_crystalrpt/ap_doc_mr_revisi.rpt"
                    mlFILENAME = "IAS_" & Left(Session("mguserid"), 3) & "_ap_doc_mr_revisi" & ".pdf"

                Case "MR Document Group By SiteCard"
                    mlREPORTNAME = "../pj_rpt/rpt_crystalrpt/ap_doc_mr_grp_sitecard.rpt"
                    mlFILENAME = "IAS_" & Left(Session("mguserid"), 3) & "_ap_doc_mr_grp_sitecard" & ".pdf"
            End Select

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
            mlREPORTQUERY = mpSQL
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

            If mpEXPTOPDF = True Then
                mlLINKDOC.Visible = True
                mlLINKDOC.Text = "<font Color=blue> Click to Download your Document (.pdf) </font>"
                mlLINKDOC.NavigateUrl = ""
                mlLINKDOC.Attributes.Add("onClick", "window.open('" & mlPATH2 & "','','');")

                mlCRDESTOPTIONS.DiskFileName = mlPATH
                mlCREXPORTOPTIONS = mlCR.ExportOptions
                mlCREXPORTOPTIONS.DestinationOptions = mlCRDESTOPTIONS
                mlCREXPORTOPTIONS.ExportDestinationType = ExportDestinationType.DiskFile
                mlCREXPORTOPTIONS.ExportFormatType = ExportFormatType.PortableDocFormat
                mlCR.Export()
                mlCR.ExportToDisk(ExportFormatType.PortableDocFormat, mlPATH)

                CRViewer.DisplayToolbar = False
                CRViewer.Visible = False


                'Dim mlFILEINFO As FileInfo = New FileInfo(mlFILENAME)
                'Response.Clear()
                'Response.ClearContent()
                'Response.ContentType = "application/pdf"
                'Response.WriteFile(mlFILEINFO.FullName)
                'Response.TransmitFile(mlFILEINFO.FullName)
            Else
                CRViewer.ShowLastPage()
                mlPAGE = CRViewer.ViewInfo.LastPageNumber
                CRViewer.ShowFirstPage()
                CRViewer.DisplayToolbar = True
            End If
            'CRViewer.ToolbarImagesFolderUrl = "../images/CrystalReportWebFormViewer3/images/toolbar/"





        Catch ex As Exception
            Response.Write(Err.Description)
        End Try
    End Sub

    Protected Sub ddlEntity_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlEntity.SelectedIndexChanged
        ddTYPE.Items.Clear()
        ddTYPE.Items.Add(New ListItem("Choose One", ""))
        mlSQLTEMP = "select Description as ID ,AdditionalDescription2 as DATA" & vbCrLf
        mlSQLTEMP += "from XM_UNIVERSALLOOKUPLIN " & vbCrLf
        mlSQLTEMP += "Where UniversalID = 'ISS_Mapping_BranchCode' and AdditionalDescription3 = '" + ddlEntity.SelectedItem.Text + "'"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISS")
        While mlRSTEMP.Read
            'ddTYPE.Items.Add(Trim(mlRSTEMP("ParentCode")))
            ddTYPE.Items.Add(New ListItem(mlRSTEMP("DATA"), mlRSTEMP("ID")))
        End While
    End Sub

End Class
