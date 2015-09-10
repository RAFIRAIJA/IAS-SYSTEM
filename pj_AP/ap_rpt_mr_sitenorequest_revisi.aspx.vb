Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports IAS.Core.CSCode
Partial Class pj_AP_ap_rpt_mr_sitenorequest_revisi
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
        mlTITLE.Text = "Material Requisition - Site Card Without MR Request - Report V2.02"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Material Requisition - Site Card Without MR Request - Report V2.02"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlFUNCTIONPARAMETER = Request.QueryString("mpFP")
        If Not Page.IsPostBack Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ap_rpt_mr_sitenorequest", "MR Rpt", "")
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
                pnlFILL.Visible = True
                pnlGRID.Visible = False
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
        mpSQL = mpSQL & " ORDER BY SiteCardName"
        mlDATAADAPTER = mlOBJGS.DbAdapter(mpSQL, "PB", "ISSP3")
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

    Sub LoadComboData()
        txDOCDATE1.Text = mlCURRENTDATE
        txDOCDATE2.Text = mlCURRENTDATE

        ddSTATUS.Items.Clear()

        ddREPORT.Items.Clear()
        ddREPORT.Items.Add("No_Request")

        'Added by Rafi Dec 2014
        ddlEntity.Items.Clear()
        ddlEntity.Items.Add("Choose")
        mlSQLTEMP = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID='ISS_Entity'"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISS")
        While mlRSTEMP.Read
            ddlEntity.Items.Add(Trim(mlRSTEMP("LinCode")))
        End While

        ddType.Items.Clear()
        ddType.Items.Add("No Type")
        'end of Added
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

    Sub SearchRecord()
        Dim mlALLSITE As Boolean

        Try
            mlALLSITE = False
            mlSQL = "(SELECT DISTINCT SiteCardID FROM AP_MR_REQUESTHR WHERE "

            If txDOCDATE1.Text <> "" And txDOCDATE2.Text <> "" Then
                '                mlSQL = mlSQL & " DocDate >= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE1.Text, "/")) & "' AND DocDate <= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE2.Text, "/")) & "' "
                mlSQL = mlSQL & " DocDate >= '" & mlOBJGF.FormatDate(txDOCDATE1.Text) & "' AND DocDate <= '" & mlOBJGF.FormatDate(txDOCDATE2.Text) & "' "
            End If

            If mpPERIOD.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " BVMonth LIKE '" & Trim(mpPERIOD.Text) & "' "
            End If

            If txLOCID1.Text <> "" And txLOCID2.Text <> "" Then
                mlSQLTEMP = "SELECT DISTINCT SiteCardID FROM OP_USER_SITE WHERE SiteCardID='ALL' AND UserID = '" & Session("mgUSERID") & "'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
                If mlRSTEMP.HasRows = False Then
                    mlSQLLEVEL = " HR.SiteCardID >= '" & txLOCID1.Text & "' AND HR.SiteCardID <= '" & txLOCID2.Text & "'" & _
                        " AND SiteCardID IN " & _
                        " (SELECT SiteCardID FROM OP_USER_SITE WHERE UserID = '" & Session("mgUSERID") & "')"
                    mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & mlSQLLEVEL
                End If

            Else
                mlSQLTEMP = "SELECT DISTINCT SiteCardID FROM OP_USER_SITE WHERE SiteCardID='ALL' AND UserID = '" & Session("mgUSERID") & "'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
                If mlRSTEMP.HasRows = False Then
                    mlSQLLEVEL = "  SiteCardID IN " & _
                        " (SELECT SiteCardID FROM OP_USER_SITE WHERE UserID = '" & Session("mgUSERID") & "')"
                    mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & mlSQLLEVEL
                Else
                    mlALLSITE = True
                End If
            End If



            If txUSERID.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.PostingUserID1 LIKE '" & txUSERID.Text & "'"
            End If

            'If ddSTATUS.Text <> "all" Then
            '    mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.RecordStatus LIKE '" & ddSTATUS.Text & "%'"
            'End If

            mlSQL = mlSQL & " )"

            If Not mlOBJGF.IsNone(mlSQL) Then
                Select Case ddREPORT.Text
                    Case "No_Request"
                        If mlALLSITE = True Then
                            mlSQL = " SELECT Distinct SiteCardID,SiteCardName,JobNo,JobTaskNo" & _
                                    " FROM OP_USER_SITE HR WHERE HR.SiteCardID <>'ALL' AND HR.SiteCardID NOT IN " & mlSQL
                        Else
                            mlSQL = " SELECT Distinct SiteCardID,SiteCardName,JobNo,JobTaskNo" & _
                                    " FROM OP_USER_SITE HR WHERE HR.SiteCardID <>'ALL' AND UserID = '" & Session("mgUSERID") & "' AND HR.SiteCardID NOT IN " & mlSQL
                        End If

                End Select

                'Added By Rafi Dec 2014
                If ddlEntity.Text <> "Choose" Then
                    mlSQL = mlSQL & IIf(mlSQL = "", "", " AND") & " EntityID = '" & Trim(ddlEntity.Text) & "' "
                End If

                If ddType.Text <> "" Then
                    mlSQL = mlSQL & IIf(mlSQL = "", "", " AND") & " BranchID = (sELECT AdditionalDescription1 FROM PROD_ISS.dbo.XM_UNIVERSALLOOKUPLIN Where UniversalID = 'ISS_Mapping_BranchCode' and AdditionalDescription3 = '" + ddlEntity.SelectedValue + "' and Description = '" + ddType.SelectedValue + "')"
                End If
                ' End of Added

                mlSQLSTATEMENT.Text = mlSQL
                RetrieveFieldsDetail(mlSQLSTATEMENT.Text)
                pnlFILL.Visible = False
                btSearchRecord.Visible = False
            End If



        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ddlEntity_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlEntity.SelectedIndexChanged
        ddType.Items.Clear()
        ddType.Items.Add(New ListItem("Choose One", ""))
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
