Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports IAS.Core.CSCode
Partial Class pj_AP_ap_rpt_mr_request_wait_revisi
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
        mlTITLE.Text = "Material Requisition Wait Status Report V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Material Requisition Wait Status Report V2.00"
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

        hpLookUpItem_From.NavigateUrl = "javascript:OpenWinLookUpItem_From('" + txITEM1.ClientID + "','" + txtItemName_From.ClientID + "','" + hdnItemNo_From.ClientID + "','" + hdnItemName_From.ClientID + "','','AccMnt')"
        hpLookUpItem_TO.NavigateUrl = "javascript:OpenWinLookUpItem_To('" + txITEM2.ClientID + "','" + txtItemName_TO.ClientID + "','" + hdnItemNo_TO.ClientID + "','" + hdnItemName_TO.ClientID + "','','AccMnt')"

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
        txITEM1.Text = ""
        txITEM2.Text = ""
        txLOCID1.Text = ""
        txLOCID2.Text = ""
        txLOCID1_Desc.Text = ""
        txLOCID2_Desc.Text = ""
        txUSERID.Text = ""
        lbNAME.Text = ""
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
            Select Case ddREPORT.Text
                Case "Summary", "Summary_with_Approval_ID"
                    mlI = 3
                    Dim mlDOCDATE2 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlDOCDATE2.ToString("d")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                    mlI = 9
                    Dim mlPOINT7 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlPOINT7.ToString("n")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right
            End Select

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
        Try
            mlDATASET.Clear()
            mlDATAADAPTER = mlOBJGS.DbAdapter(mpSQL, "PB", "ISSP3")
            mlDATAADAPTER.Fill(mlDATASET)
            mlDATATABLE = mlDATASET.Tables("table")
            mlDATAGRID.DataSource = mlDATATABLE
            mlDATAGRID.DataBind()
            ShowHideGrid(ddREPORT.Text)


            mlOBJGS.CloseDataSet(mlDATASET)
            mlOBJGS.CloseDataAdapter(mlDATAADAPTER)
            btExCsv.Visible = True


            Select Case ddREPORT.Text
                Case "Summary_with_Approval_ID"
                    FillApprovalID()
            End Select

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

    Protected Sub btLOCID1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btLOCID1.Click
        txLOCID1_Desc.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_DESC", Trim(txLOCID1.Text), "")
        '        txLOCID1_Desc.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_DESC", Trim(txLOCID1.Text))
    End Sub

    Protected Sub btLOCID2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btLOCID2.Click
        txLOCID2_Desc.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_DESC", Trim(txLOCID2.Text), "")
        '        txLOCID2_Desc.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_DESC", Trim(txLOCID2.Text))
    End Sub

    'Protected Sub btITEM1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btITEM1.Click
    '    lbITEMDESC1.Text = mlOBJPJ.ISS_INGeneralLostFocus("ITEMKEY", txITEM1.Text, "")
    '    '        lbITEMDESC1.Text = mlOBJPJ.ISS_INGeneralLostFocus("ITEMKEY", txITEM1.Text)
    'End Sub
    'Protected Sub btITEM2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btITEM2.Click
    '    lbITEMDESC2.Text = mlOBJPJ.ISS_INGeneralLostFocus("ITEMKEY", txITEM2.Text, "")
    '    '        lbITEMDESC2.Text = mlOBJPJ.ISS_INGeneralLostFocus("ITEMKEY", txITEM2.Text)
    'End Sub


    Sub LoadComboData()
        txDOCDATE1.Text = mlCURRENTDATE
        txDOCDATE2.Text = mlCURRENTDATE

        ddSTATUS.Items.Clear()
        ddSTATUS.Items.Add("wait_all")

        ddREPORT.Items.Clear()
        ddREPORT.Items.Add("Summary")
        ddREPORT.Items.Add("Summary_with_Approval_ID")

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



    Sub ShowHideGrid(ByVal mpTYPE As String)
        Dim mlDG As DataGridColumn

        Select Case mpTYPE

            Case Else
                For Each mlDG In mlDATAGRID.Columns
                    If mlDG.HeaderText = "MR" Then
                        mlDG.Visible = True
                    End If
                Next
        End Select
    End Sub


    Sub SearchRecord()
        Try
            mlSQL = ""

            If txDOCDATE1.Text <> "" And txDOCDATE2.Text <> "" Then
                '                mlSQL = mlSQL & " HR.DocDate >= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE1.Text, "/")) & "' AND DocDate <= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE2.Text, "/")) & "' "
                mlSQL = mlSQL & " HR.DocDate >= '" & mlOBJGF.FormatDate(txDOCDATE1.Text) & "' AND DocDate <= '" & mlOBJGF.FormatDate(txDOCDATE2.Text) & "' "
            End If

            If txDOCUMENTNO1.Text <> "" And txDOCUMENTNO2.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.DocNo >= '" & txDOCUMENTNO1.Text & "' AND DocNo <= '" & txDOCUMENTNO2.Text & "'"
            End If

            If ddlEntity.Text <> "Choose" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " EntityID = '" & Trim(ddlEntity.Text) & "' "
            End If

            If ddTYPE.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " ParentCode = '" & Trim(ddTYPE.Text) & "' "
            End If


            If mpPERIOD.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " BVMonth LIKE '" & Trim(mpPERIOD.Text) & "' "
            End If

            If txLOCID1.Text <> "" And txLOCID2.Text <> "" Then
                mlSQLLEVEL = " HR.SiteCardID >= '" & txLOCID1.Text & "' AND HR.SiteCardID <= '" & txLOCID2.Text & "'" & _
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & mlSQLLEVEL
            End If

            If txUSERID.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " (HR.PostingUserID1 LIKE '" & txUSERID.Text & "'" & _
                        "HR.PostingUserID2 LIKE '" & txUSERID.Text & "' OR HR.PostingUserID3 LIKE '" & txUSERID.Text & "')"
            End If

            Select Case ddSTATUS.Text
                Case "wait_all"
                    mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " (HR.RecordStatus <> 'New' AND HR.RecordStatus <> 'Post') "
            End Select


            If Not mlOBJGF.IsNone(mlSQL) Then
                Select Case ddREPORT.Text
                    Case "Summary"
                        mlSQL = "SELECT HR.DocNo,HR.DocDate,HR.MRType,HR.MRDesciption,HR.SiteCardID as Site,HR.SiteCardName as SiteDesc,HR.JobNo,HR.JobTaskNo,HR.BVMonth as Period,HR.TotalAmount as Amount," & _
                            " HR.PostingUserID1 as User1, HR.PostingName1 as UserName1, " & _
                            " HR.PostingUserID2 as User2, HR.PostingName2 as UserName2, " & _
                            " HR.PostingUserID3 as User3, HR.PostingName3 as UserName3, " & _
                            " HR.RecordStatus as Status " & _
                            " FROM AP_MR_REQUESTHR HR WHERE " & mlSQL

                    Case "Summary_with_Approval_ID"
                        mlSQL = "SELECT HR.DocNo,HR.DocDate,HR.MRType,HR.MRDesciption,HR.SiteCardID as Site,HR.SiteCardName as SiteDesc,HR.JobNo,HR.JobTaskNo,HR.BVMonth as Period,HR.TotalAmount as Amount," & _
                            " HR.PostingUserID1 as User1, HR.PostingName1 as UserName1, " & _
                            " HR.PostingUserID2 as User2, HR.PostingName2 as UserName2, " & _
                            " HR.PostingUserID3 as User3, HR.PostingName3 as UserName3, " & _
                            " HR.RecordStatus as Status, " & _
                            " '' as ID1,'' as ID2,'' as ID3" & _
                            " FROM AP_MR_REQUESTHR HR WHERE " & mlSQL

                End Select

                mlSQLSTATEMENT.Text = mlSQL
                RetrieveFieldsDetail(mlSQLSTATEMENT.Text)
                pnFILL.Visible = False
                btSearchRecord.Visible = False
            End If



        Catch ex As Exception

        End Try
    End Sub

    Sub FillApprovalID()
        Dim mlMR_SITECARDID As String
        Dim mlVALUE As String

        mlI = 0
        Dim mlDG As DataGridItem
        For Each mlDG In mlDATAGRID.Items
            mlMR_SITECARDID = Trim(mlDG.Cells("6").Text)
            mlSQLTEMP = "SELECT * FROM OP_USER_SITE WHERE SiteCardID='" & Trim(mlMR_SITECARDID) & "' ORDER BY SiteCardID, UserLevel"
            mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
            While mlRSTEMP.Read
                Select Case mlRSTEMP("UserLevel")
                    Case "1"
                        mlVALUE = Trim(mlDG.Cells("17").Text)
                        mlVALUE = Replace(mlVALUE, "&nbsp;", "")
                        mlDG.Cells("17").Text = mlVALUE & IIf(mlOBJGF.IsNone(mlVALUE) = False, ",", "") & mlRSTEMP("UserID") & "-" & mlRSTEMP("UserName")
                    Case "2"
                        mlVALUE = Trim(mlDG.Cells("18").Text)
                        mlVALUE = Replace(mlVALUE, "&nbsp;", "")
                        mlDG.Cells("18").Text = mlVALUE & IIf(mlOBJGF.IsNone(mlVALUE) = False, ",", "") & mlRSTEMP("UserID") & "-" & mlRSTEMP("UserName")
                    Case "3"
                        mlVALUE = Trim(mlDG.Cells("19").Text)
                        mlVALUE = Replace(mlVALUE, "&nbsp;", "")
                        mlDG.Cells("19").Text = mlVALUE & IIf(mlOBJGF.IsNone(mlVALUE) = False, ",", "") & mlRSTEMP("UserID") & "-" & mlRSTEMP("UserName")
                End Select
            End While

        Next mlDG
    End Sub

    Protected Sub btEMAIL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btEMAIL.Click
        Dim mlGOFORMAIL As Boolean
        Dim mlSQLUSERSITE As String
        Dim mlRSUSERSITE As OleDbDataReader

        Dim mlMR_DOCNO As String
        Dim mlMR_PERIOD As String
        Dim mlMR_STATUS As String
        Dim mlMR_SITECARDID As String
        Dim mlMR_SITECARDDESC As String
        Dim mlMR_TYPE As String

        Dim mlMR_USER1 As String
        Dim mlMR_USER2 As String
        Dim mlMR_USER3 As String

        Dim mlOBJPJ_UT As New IASClass_Local_ut.ucmLOCAL_ut
        Dim mlEMAIL_STATUS As String
        Dim mlEMAIL_TO As String

        Dim mlEMAIL_SUBJECT As String
        Dim mlEMAIL_BODY As String
        Dim mlLINKSERVER1 As String

        mlGOFORMAIL = False

        Dim mlDG As DataGridItem
        For Each mlDG In mlDATAGRID.Items

            mlMR_DOCNO = Trim(mlDG.Cells("2").Text)
            mlMR_TYPE = Trim(mlDG.Cells("4").Text) & " - " & Trim(mlDG.Cells("5").Text)
            mlMR_SITECARDID = Trim(mlDG.Cells("6").Text)
            mlMR_SITECARDDESC = Trim(mlDG.Cells("6").Text) & " - " & Trim(mlDG.Cells("7").Text)
            mlMR_PERIOD = Trim(mlDG.Cells("8").Text)
            mlMR_STATUS = Trim(mlDG.Cells("16").Text)

            mlMR_USER1 = Trim(mlDG.Cells("10").Text) & " - " & Trim(mlDG.Cells("11").Text)
            mlMR_USER2 = Trim(mlDG.Cells("12").Text) & " - " & Trim(mlDG.Cells("13").Text)
            mlMR_USER3 = Trim(mlDG.Cells("14").Text) & " - " & Trim(mlDG.Cells("15").Text)

            If mlMR_USER1 = "&nbsp; - &nbsp;" Then mlMR_USER1 = "Belum diBuat"
            If mlMR_USER2 = "&nbsp; - &nbsp;" Then mlMR_USER2 = "Belum diPeriksa"
            If mlMR_USER3 = "&nbsp; - &nbsp;" Then mlMR_USER3 = "Belum diSetujui"


            mlEMAIL_TO = ""
            mlLINKSERVER1 = System.Configuration.ConfigurationManager.AppSettings("mgLINKEDSERVER1")

            mlSQLTEMP = "SELECT EmailAddr FROM " & mlLINKSERVER1 & "AD_USERPROFILE  WHERE UserID IN " & _
                                    " (SELECT UserID FROM OP_USER_SITE WHERE SiteCardID='" & Trim(mlMR_SITECARDID) & "') "
            mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
            While mlRSTEMP.Read
                mlEMAIL_TO = mlEMAIL_TO & IIf(mlOBJGF.IsNone(Trim(mlEMAIL_TO)) = True, "", ",") & mlRSTEMP("EmailAddr") & ""
            End While


            mlEMAIL_TO = IIf(mlOBJGF.IsNone(Trim(mlEMAIL_TO)) = True, "", mlEMAIL_TO & ",") & "sugianto@iss.co.id,agustinus@iss.co.id,iwan.setiawansyah@iss.co.id," & txMAILCC.Text
            'mlEMAIL_TO = "sugianto@iss.co.id,agustinus@iss.co.id"

            If mlOBJGF.IsNone(Trim(mlEMAIL_TO)) = False Then
                mlEMAIL_SUBJECT = "" & " List MR Online yang Menunggu Approval Anda : " & mlMR_SITECARDDESC
                mlEMAIL_BODY = ""
                mlEMAIL_BODY = mlEMAIL_BODY & "Dear Bapak / Ibu"
                mlEMAIL_BODY = mlEMAIL_BODY & "<br><br>"
                mlEMAIL_BODY = mlEMAIL_BODY & "<br> Berikut ini adalah MR Online yang masih Menunggu Approval Anda"
                mlEMAIL_BODY = mlEMAIL_BODY & "<br><br>"
                mlEMAIL_BODY = mlEMAIL_BODY & "<table border=0>"
                mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                mlEMAIL_BODY = mlEMAIL_BODY & "No MR </td><td valign=top>:</td><td valign=top>" & mlMR_DOCNO
                mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                mlEMAIL_BODY = mlEMAIL_BODY & "Tipe MR </td><td valign=top>:</td><td valign=top>" & mlMR_TYPE
                mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                mlEMAIL_BODY = mlEMAIL_BODY & "Periode </td><td valign=top>:</td><td valign=top>" & mlMR_PERIOD
                mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                mlEMAIL_BODY = mlEMAIL_BODY & "SiteCard </td><td valign=top>:</td><td valign=top>" & mlMR_SITECARDDESC
                mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                mlEMAIL_BODY = mlEMAIL_BODY & "Status  </td><td valign=top>:</td><td valign=top>" & mlMR_STATUS
                mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                mlEMAIL_BODY = mlEMAIL_BODY & "diBuat Oleh   </td><td valign=top>:</td><td valign=top>" & mlMR_USER1
                mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                mlEMAIL_BODY = mlEMAIL_BODY & "diPeriksa Oleh   </td><td valign=top>:</td><td valign=top>" & mlMR_USER2
                mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                mlEMAIL_BODY = mlEMAIL_BODY & "diSetujui Oleh   </td><td valign=top>:</td><td valign=top>" & mlMR_USER3
                mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                mlEMAIL_BODY = mlEMAIL_BODY & "Pesan Tambahan  </td><td valign=top>:</td><td valign=top>" & Trim(txMAILMESSAGES.Text)
                mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                mlEMAIL_BODY = mlEMAIL_BODY & "</table>"
                mlEMAIL_BODY = mlEMAIL_BODY & "<br>Terima Kasih"
                mlEMAIL_BODY = mlEMAIL_BODY & "<br>Salam"
                mlEMAIL_BODY = mlEMAIL_BODY & "<br>" & Session("mgUSERID") & "-" & Session("mgNAME")

                mlEMAIL_BODY = mlEMAIL_BODY & "<br><br><i>Email ini dikirim Otomatis oleh Sistem Komputer, Email ini tidak perlu dibalas/</i>"
                mlEMAIL_BODY = mlEMAIL_BODY & "<br><i>This is an automatically generated email by computer system, please do not reply </i>"
                mlEMAIL_STATUS = mlOBJPJ_UT.Sendmail_1("1", mlEMAIL_TO, "", "", mlEMAIL_SUBJECT, mlEMAIL_BODY)

                Stop

            End If
        Next mlDG
    End Sub

    Protected Sub ddlEntity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEntity.SelectedIndexChanged
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
