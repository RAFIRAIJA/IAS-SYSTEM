Imports System
Imports System.Data
Imports System.Data.OleDb
Imports IAS.Core.CSCode

Partial Class in_inv_image
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJPJ As New ModuleFunctionLocal


    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlREADER2 As OleDb.OleDbDataReader
    Dim mlSQL2 As String

    Dim mlKEY As String
    Dim mlKEY2 As String
    Dim mlRECORDSTATUS As String
    Dim mlSPTYPE As String
    Dim mlFUNCTIONPARAMETER As String

    Dim mlSQLTEMP As String
    Dim mlRSTEMP As OleDb.OleDbDataReader

    Dim mlCURRENTDATE As String = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "AP Inventory Delivery Zone V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "AP Inventory Delivery Zone V2.00"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")


        mlFUNCTIONPARAMETER = "1"
        If Page.IsPostBack = False Then
            DisableCancel()
            RetrieveFieldsDetail("")
            pnSEARCHITEMKEY.Visible = False
            pnSEARCHSITECARD.Visible = False
            pnSEARCHVENDOR.Visible = False
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "IN_INMAST_ADDINFO_APZONE", "Menu", "")
        Else
        End If
    End Sub


    Protected Sub mlDATAGRID_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRID.ItemCommand
        mlKEY = e.CommandArgument
        mlKEY = mlOBJGF.GetStringAtPosition(e.CommandArgument, 0, ";")
        mlKEY2 = mlOBJGF.GetStringAtPosition(e.CommandArgument, 1, ";")
        Select Case e.CommandName
            Case "BrowseRecord"
                mlMESSAGE.Text = "Update Request for " & e.CommandArgument
                RetrieveFields()
                pnFILL.Visible = True
            Case "EditRecord"
                mlMESSAGE.Text = "Edit Request for  " & e.CommandArgument
                mlSYSCODE.Value = "edit"
                EditRecord()
            Case "DeleteRecord"
                mlMESSAGE.Text = "Delete Request for  " & e.CommandArgument
                mlSYSCODE.Value = "delete"
                DeleteRecord()
            Case Else
                ' Ignore Other
        End Select
    End Sub

    Protected Sub btNewRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btNewRecord.Click
        NewRecord()
    End Sub

    Protected Sub btSaveRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSaveRecord.Click
        If pnFILL.Visible = True Then
            SaveRecord()
        End If
    End Sub

    Protected Sub btCancelOperation_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btCancelOperation.Click
        DisableCancel()
    End Sub

    Public Sub RetrieveFields()
        ClearFields()
        DisableCancel()

        mlSQL = "SELECT * FROM IN_INMAST_ADDINFO_APZONE WHERE SiteCardID = '" & Trim(mlKEY) & "' AND ItemKey='" & Trim(mlKEY2) & "'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3")
        If mlREADER.HasRows Then
            mlREADER.Read()
            mpSITECARD.Text = mlREADER("SiteCardID") & ""
            mpITEMKEY.Text = mlREADER("ItemKey") & ""
            mpVENDOR.Text = mlREADER("VendID") & ""

            btSITECARD_Click(Nothing, Nothing)
            btITEMKEY_Click(Nothing, Nothing)
            btITEMKEY_Click(Nothing, Nothing)
            btVENDOR_Click(Nothing, Nothing)
        End If
    End Sub

    Sub RetrieveFieldsDetail(ByVal mpSQL As String)
        If mpSQL = "" Then
            mlSQL2 = "SELECT SiteCardID as SiteCard,SiteCardDesc as SiteName,ItemKey as ItemCode,Description as ItemDesc,VendID as Vend,VendName as VendName,RecUserID as UserID FROM IN_INMAST_ADDINFO_APZONE" & _
                " WHERE RecordStatus='New' AND RecUserID = '" & Trim(Session("mgUSERID")) & "' ORDER BY SiteCard,ItemKey"
        Else
            mpSQL = mlSQL2
        End If
        mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISSP3")
        mlDATAGRID.DataSource = mlREADER2
        mlDATAGRID.DataBind()
    End Sub

    Sub DeleteRecord()
        mlSPTYPE = "Delete"
        Try
            Sql_1(mlSPTYPE, mlKEY)
        Catch ex As Exception
        End Try
0:
        mlSYSCODE.Value = ""
        RetrieveFields()
        RetrieveFieldsDetail("")
    End Sub

    Sub NewRecord()
        EnableCancel()
        ClearFields()

        mlOBJPJ.SetTextBox(False, mpSITECARD)
    End Sub

    Sub EditRecord()
        RetrieveFields()
        EnableCancel()
    End Sub

    Function ValidateForm() As String
        ValidateForm = ""

        If mpSITEDESC.Text = "" Then
            ValidateForm = ValidateForm & " <br /> Site Card Code or Description not allowed empty"
        End If

        If mpITEMDESC.Text = "" Then
            ValidateForm = ValidateForm & " <br /> Item Code or Item Description not allowed empty"
        End If

        If mpVENDORDESC.Text = "" Then
            ValidateForm = ValidateForm & " <br /> Vendor Code or Item Description not allowed empty"
        End If

        'mlSQLTEMP = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE LinCode = '" & Trim(mpITEMKEY.Text) & "'" & _
        '        " AND AdditionalDescription1 = '" & Trim(mpSIZE.Text) & "' "
        'mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
        'If mlRSTEMP.HasRows Then
        '    ValidateForm = ValidateForm & " <br /> Item Code for " & Trim(mpITEMKEY.Text) & " was found in Database (not allow duplicate) "
        'End If

    End Function


    Sub SaveRecord()
        Dim mlSQLHR As String = ""
        Dim mlSQLDT As String = ""

        mlOBJGS.mgMESSAGE = ValidateForm()
        If mlOBJGF.IsNone(mlOBJGS.mgMESSAGE) = False Then
            mlMESSAGE.Text = mlOBJGS.mgMESSAGE
            Exit Sub
        End If

        mlSPTYPE = "New"
        If mlSYSCODE.Value = "edit" Then
            mlSPTYPE = "Edit"
        End If

        Try
            Sql_1(mlSPTYPE, mlKEY)
        Catch ex As Exception
        End Try


        mlSYSCODE.Value = ""
        ClearFields()
        DisableCancel()
        RetrieveFieldsDetail("")

    End Sub

    Sub CancelOperation()
        mlMESSAGE.Text = ""
        DisableCancel()
        RetrieveFieldsDetail("")
    End Sub

    Private Sub EnableCancel()
        btNewRecord.Visible = False
        btSaveRecord.Visible = True
        pnFILL.Visible = True
        'pnGRID.Visible = False
        'tr1.Visible = False
        'tr2.Visible = False
        'tr3.Visible = False
        'tr4.Visible = False
        mlOBJPJ.SetTextBox(True, mpSITECARD)
        mlOBJPJ.SetTextBox(False, mpITEMKEY)
        mlOBJPJ.SetTextBox(False, mpVENDOR)
        
    End Sub

    Private Sub DisableCancel()
        btNewRecord.Visible = True
        btSaveRecord.Visible = False
        pnFILL.Visible = False
        pnGRID.Visible = True
        'tr1.Visible = False
        'tr2.Visible = False
        'tr3.Visible = False
        'tr4.Visible = False

        mlOBJPJ.SetTextBox(True, mpSITECARD)
        mlOBJPJ.SetTextBox(True, mpITEMKEY)
        mlOBJPJ.SetTextBox(True, mpVENDOR)
        
    End Sub

    Sub ClearFields()
        mpSITECARD.Text = ""
        mpSITEDESC.Text = ""
        mpITEMKEY.Text = ""
        mpITEMDESC.Text = ""
        mpVENDOR.Text = ""
        mpVENDORDESC.Text = ""
    End Sub


    Protected Sub btITEMKEY_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btITEMKEY.Click
        If Trim(UCase(mpITEMKEY.Text)) = "ALL" Then
            mpITEMKEY.Text = "ALL"
            mpITEMDESC.Text = "ALL"
            ViewLink()
            Exit Sub
        End If
        mpITEMDESC.Text = mlOBJPJ.ISS_INGeneralLostFocus("ITEMKEY", Trim(mpITEMKEY.Text), "")
        '        mpITEMDESC.Text = mlOBJPJ.ISS_INGeneralLostFocus("ITEMKEY", Trim(mpITEMKEY.Text))
        ViewLink()
    End Sub


    Protected Sub btSEARCHITEMKEY_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHITEMKEY.Click
        If pnSEARCHITEMKEY.Visible = False Then
            pnSEARCHITEMKEY.Visible = True
        Else
            pnSEARCHITEMKEY.Visible = False
        End If
    End Sub

    Protected Sub btSEARCHITEMKEYSUBMIT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHITEMKEYSUBMIT.Click
        If mlOBJGF.IsNone(mpSEARCHITEMKEY.Text) = False Then SearchItem(1, mpSEARCHITEMKEY.Text)
    End Sub

    Protected Sub mlDATAGRIDITEMKEY_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRIDITEMKEY.ItemCommand
        Try
            mpITEMKEY.Text = CType(e.Item.Cells(0).Controls(0), LinkButton).Text
            mpITEMDESC.Text = CType(e.Item.Cells(1).Controls(0), LinkButton).Text
            mlDATAGRIDITEMKEY.DataSource = Nothing
            mlDATAGRIDITEMKEY.DataBind()
            pnSEARCHITEMKEY.Visible = False
        Catch ex As Exception
        End Try
    End Sub

    Sub SearchItem(ByVal mpTYPE As Byte, ByVal mpNAME As String)
        Select Case mpTYPE
            Case "1"
                mlSQLTEMP = "SELECT No_, Description FROM [ISS Servisystem, PT$Item] INV WHERE Description LIKE  '%" & mpNAME & "%'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSN3")
                mlDATAGRIDITEMKEY.DataSource = mlRSTEMP
                mlDATAGRIDITEMKEY.DataBind()
        End Select
    End Sub

    Protected Sub btSITECARD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSITECARD.Click
        mpSITEDESC.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_DESC", Trim(mpSITECARD.Text), "")
        '        mpSITEDESC.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_DESC", Trim(mpSITECARD.Text))
        ViewLink()
    End Sub

    Protected Sub btSEARCHSITECARD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHSITECARD.Click
        If pnSEARCHSITECARD.Visible = False Then
            pnSEARCHSITECARD.Visible = True
        Else
            pnSEARCHSITECARD.Visible = False
        End If
    End Sub

    Protected Sub btSEARCHSITECARDSUBMIT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHSITECARDSUBMIT.Click
        If mlOBJGF.IsNone(mlSEARCHSITECARD.Text) = False Then SearchSiteCard(1, mlSEARCHSITECARD.Text)
    End Sub

    Protected Sub mlDATAGRIDSITECARD_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRIDSITECARD.ItemCommand
        Try
            mpSITECARD.Text = CType(e.Item.Cells(0).Controls(0), LinkButton).Text
            mpSITEDESC.Text = CType(e.Item.Cells(1).Controls(0), LinkButton).Text
            mlDATAGRIDSITECARD.DataSource = Nothing
            mlDATAGRIDSITECARD.DataBind()
            pnSEARCHSITECARD.Visible = False
        Catch ex As Exception
        End Try
    End Sub

    Sub SearchSiteCard(ByVal mpTYPE As Byte, ByVal mpNAME As String)
        Select Case mpTYPE
            Case "1"
                mlSQLTEMP = "SELECT LineNo_,SearchName FROM [ISS Servisystem, PT$CustServiceSite] WHERE SearchName LIKE  '%" & mlSEARCHSITECARD.Text & "%'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSN3")
                mlDATAGRIDSITECARD.DataSource = mlRSTEMP
                mlDATAGRIDSITECARD.DataBind()
        End Select
    End Sub

    Protected Sub btVENDOR_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btVENDOR.Click
        mpVENDORDESC.Text = mlOBJPJ.ISS_APGeneralLostFocus("VENDOR", Trim(mpVENDOR.Text), "")
        '        mpVENDORDESC.Text = mlOBJPJ.ISS_APGeneralLostFocus("VENDOR", Trim(mpVENDOR.Text))
        ViewLink()
    End Sub

    Protected Sub btSEARCHVENDOR_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHVENDOR.Click
        If pnSEARCHVENDOR.Visible = False Then
            pnSEARCHVENDOR.Visible = True
        Else
            pnSEARCHVENDOR.Visible = False
        End If
    End Sub

    Protected Sub btSEARCHVENDORSUBMIT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHVENDORSUBMIT.Click
        If mlOBJGF.IsNone(mpSEARCHVENDOR.Text) = False Then SearchVendor(1, mpSEARCHVENDOR.Text)
    End Sub

    Protected Sub mlDATAGRIDVENDOR_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRIDVENDOR.ItemCommand
        Try
            mpVENDOR.Text = CType(e.Item.Cells(0).Controls(0), LinkButton).Text
            mpVENDORDESC.Text = CType(e.Item.Cells(1).Controls(0), LinkButton).Text
            mlDATAGRIDVENDOR.DataSource = Nothing
            mlDATAGRIDVENDOR.DataBind()
            pnSEARCHVENDOR.Visible = False
        Catch ex As Exception
        End Try
    End Sub

    Sub SearchVendor(ByVal mpTYPE As Byte, ByVal mpNAME As String)
        Select Case mpTYPE
            Case "1"
                mlSQLTEMP = "SELECT No_, [Search Name] FROM [ISS Servisystem, PT$Vendor] tbl WHERE [Search Name] LIKE  '%" & mpNAME & "%'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSN3")
                mlDATAGRIDVENDOR.DataSource = mlRSTEMP
                mlDATAGRIDVENDOR.DataBind()
        End Select
    End Sub

    Protected Sub lnVIEWSITECARD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnVIEWSITECARD.Click
        mlSQL2 = "SELECT SiteCardID as SiteCard,SiteCardDesc as SiteName,ItemKey as ItemCode,Description as ItemDesc,VendID as Vend,VendName as VendName,RecUserID as UserID FROM IN_INMAST_ADDINFO_APZONE" & _
        " WHERE RecordStatus='New' AND SiteCardID = '" & Trim(mpSITECARD.Text) & "' ORDER BY SiteCard,ItemKey"
        RetrieveFieldsDetail(mlSQL2)
    End Sub

    Protected Sub lnVIEWVENDOR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnVIEWVENDOR.Click
        mlSQL2 = "SELECT SiteCardID as SiteCard,SiteCardDesc as SiteName,ItemKey as ItemCode,Description as ItemDesc,VendID as Vend,VendName as VendName,RecUserID as UserID FROM IN_INMAST_ADDINFO_APZONE" & _
            " WHERE RecordStatus='New' AND VendID = '" & Trim(mpVENDOR.Text) & "' ORDER BY SiteCard,ItemKey"
        RetrieveFieldsDetail(mlSQL2)
    End Sub

    Protected Sub lnVIEWITEM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnVIEWITEM.Click
        mlSQL2 = "SELECT SiteCardID as SiteCard,SiteCardDesc as SiteName,ItemKey as ItemCode,Description as ItemDesc,VendID as Vend,VendName as VendName,RecUserID as UserID FROM IN_INMAST_ADDINFO_APZONE" & _
        " WHERE RecordStatus='New' AND ItemKey = '" & Trim(mpITEMKEY.Text) & "' ORDER BY SiteCard,ItemKey"
        RetrieveFieldsDetail(mlSQL2)
    End Sub



    Protected Sub lnlnVIEWNORMAL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnVIEWNORMAL.Click
        mlSQL2 = ""
        RetrieveFieldsDetail(mlSQL2)
    End Sub

    Sub ViewLink()
        If mpSITECARD.Text <> "" Then lnVIEWSITECARD.Visible = True
        If mpITEMKEY.Text <> "" Then lnVIEWITEM.Visible = True
        If mpVENDOR.Text <> "" Then lnVIEWVENDOR.Visible = True
    End Sub


    Sub Sql_1(ByVal mpSTATUS As String, ByVal mpCODE As String)
        Dim mlSTATUS As String
        Dim mlNEWRECORD As Boolean


        Try
            mlNEWRECORD = False

            Select Case mpSTATUS
                Case "Edit", "Delete"
                    mlSQL = ""
                    mlSQL = mlSQL & mlOBJPJ.ISS_IN_INADDINFO_APDOZONE_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
            End Select

            Select Case mpSTATUS
                Case "New"
                    mlNEWRECORD = True
                    mlKEY = Trim(mpSITECARD.Text)


                Case "Edit"
                    mlSTATUS = "Edit"
                    mlNEWRECORD = True
                    mlSQL = mlSQL & " DELETE FROM IN_INMAST_ADDINFO_APZONE WHERE SiteCardID = '" & mlKEY & "' AND ItemKey = '" & mlKEY2 & "';"

                Case "Delete"
                    mlSTATUS = "Delete"
                    mlSQL = mlSQL & " DELETE FROM IN_INMAST_ADDINFO_APZONE WHERE SiteCardID = '" & mlKEY & "' AND ItemKey = '" & mlKEY2 & "';"

            End Select
            If mlOBJGF.IsNone(mlSQL) = False Then mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
            mlSQL = ""

            If mlNEWRECORD = True Then
                mlSQL = ""
                mlSQL = mlSQL & " INSERT INTO IN_INMAST_ADDINFO_APZONE (ParentCode,SiteCardID,SiteCardDesc,ItemKey,Description,VendID,VendName," & _
                    " RecordStatus,RecUserID,RecDate)" & _
                    " VALUES ( " & _
                    " '" & mlFUNCTIONPARAMETER & "','" & mlKEY & "','" & Trim(mpSITEDESC.Text) & "'," & _
                    " '" & Trim(mpITEMKEY.Text) & "','" & Trim(mpITEMDESC.Text) & "','" & Trim(mpVENDOR.Text) & "','" & Trim(mpVENDORDESC.Text) & "'," & _
                    " 'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "');"
            End If

            Select Case mpSTATUS
                Case "New"
                    mlSQL = mlSQL & mlOBJPJ.ISS_IN_INADDINFO_APDOZONE_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
            End Select
            mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
            mlSQL = ""

        Catch ex As Exception
            mlOBJGS.XMtoLog("MR", "MRRequest", "MRRequest" & mlKEY, Err.Description, "New", Session("mgUSERID"), mlOBJGF.FormatDate(Now))
        End Try
    End Sub

    
End Class
