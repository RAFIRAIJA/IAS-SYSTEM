Imports System
Imports System.Data
Imports System.Web.UI.HtmlControls
Imports System.Drawing
Imports System.Data.OleDb
Imports IAS.Core.CSCode
Partial Class backoffice_ap_mr_template
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

    Dim mlCOMPANYTABLENAME As String
    Dim mlCOMPANYID As String

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        CekSession()
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        mlTITLE.Text = "Material Requisition Maintenance V2.02"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Material Requisition Maintenance V2.01"

        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")


        Try
            mlCOMPANYID = mlOBJPJ.ISS_GetCompanyID("1", ddENTITY.Text)
            mlCOMPANYTABLENAME = mlOBJPJ.ISS_GetCompanyID("2", ddENTITY.Text)

            If Page.IsPostBack = False Then
                DisableCancel()
                RetrieveFieldsDetail()
                pnSEARCHITEMKEY.Visible = False
                mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "pc_mr_entry", "Mr_Request", "")
            Else
            End If

            Response.Write(Err.Description)
        Catch ex As Exception
            Response.Write(Err.Description)
        End Try
    End Sub
    Protected Sub CekSession()
        If Session("mgMENUSTYLE") = "" Then
            Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=34FC35D4")
            Return
        End If
    End Sub
    Protected Sub mlDATAGRID_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRID.ItemCommand
        mlKEY = e.CommandArgument
        Select Case e.CommandName
            Case "BrowseRecord"
                mlMESSAGE.Text = "Update Request for " & e.CommandArgument
                mlSYS_DOCNO.Value = mlKEY
                'RetrieveFields()
                'pnFILL.Visible = True
                RetrieveFieldsDetail2()
                pnGRID.Visible = False

            Case "EditRecord"
                mlMESSAGE.Text = "Edit Request for  " & e.CommandArgument
                mlSYSCODE.Value = "edit"
                'EditRecord()
            Case "DeleteRecord"
                mlMESSAGE.Text = "Delete Request for  " & e.CommandArgument
                mlSYSCODE.Value = "delete"
                DeleteRecord()
            Case Else
                ' Ignore Other
        End Select
    End Sub


    Protected Sub mlDATAGRID2_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRID2.ItemCommand
        mlKEY2 = e.CommandArgument
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
                DeleteRecord2()
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
        DisableCancel()

        mlSQL = "SELECT DocNo,DocDate,RecordStatus as Status,RecUserID as UserID FROM AP_MR_TEMPLATEHR WHERE DocNo = '" & Trim(mlKEY) & "'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3")
        If mlREADER.HasRows Then
            mlREADER.Read()
            Try
                mpDOCID.SelectedValue = mlREADER("DocNo")
            Catch ex As Exception
                mpDOCID.Items.Add(mlREADER("DocNo"))
            End Try
        End If
    End Sub

    Sub RetrieveFieldsDetail()
        mlSQL2 = "SELECT Distinct DocNo,RecordStatus as Status FROM AP_MR_TEMPLATEHR WHERE RecordStatus='New' ORDER BY DocNo"
        mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "", "ISSP3")
        mlDATAGRID.DataSource = mlREADER2
        mlDATAGRID.DataBind()
    End Sub

    Sub RetrieveFieldsDetail2()
        mlSQL2 = "SELECT ItemKey AS Itemkey,Description,Uom,Qty, RecUserID as UserID FROM AP_MR_TEMPLATEDT WHERE DocNo = '" & mlKEY & "' AND RecordStatus='New' ORDER BY DocNo,Description"
        mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "", "ISSP3")
        mlDATAGRID2.DataSource = mlREADER2
        mlDATAGRID2.DataBind()
    End Sub

    Sub DeleteRecord()
        mlSPTYPE = "Delete"
        Try
            Sql_1(mlSPTYPE, mlKEY)
        Catch ex As Exception
        End Try

        mlSYSCODE.Value = ""
        RetrieveFields()
        RetrieveFieldsDetail()
    End Sub

    Sub DeleteRecord2()
        mlSPTYPE = "Delete"
        Try
            'mlKEY = Trim(mlOBJGF.GetStringAtPosition(mpDOCID.Text, 0, "-"))
            mlKEY = mlSYS_DOCNO.Value
            Sql_2(mlSPTYPE, mlKEY, mlKEY2)
        Catch ex As Exception
        End Try

        mlSYSCODE.Value = ""
        RetrieveFields()
        RetrieveFieldsDetail()
        RetrieveFieldsDetail2()
    End Sub

    Sub NewRecord()
        mlOBJGS.mgNEW = True
        mlOBJGS.mgEDIT = False

        EnableCancel()
        ClearFields()
        LoadComboData()
        mpDOCDATE.Text = mlCURRENTDATE
        pnGRID.Visible = False
    End Sub

    Sub EditRecord()
        mlOBJGS.mgNEW = False
        mlOBJGS.mgEDIT = True
        ClearFields()
        LoadComboData()
        RetrieveFields()
        EnableCancel()
    End Sub


    Private Sub EnableCancel()
        btNewRecord.Visible = False
        btSaveRecord.Visible = True
        pnFILL.Visible = True

        mpDOCID.Enabled = True
        btDOCDATE.Visible = True
        mlOBJPJ.SetTextBox(False, mpDOCDATE)
    End Sub

    Private Sub DisableCancel()
        btNewRecord.Visible = True
        btSaveRecord.Visible = False
        pnFILL.Visible = False

        mpDOCID.Enabled = False
        btDOCDATE.Visible = False
        mlOBJPJ.SetTextBox(True, mpDOCDATE)

    End Sub

    Sub ClearFields()
        mpDOCDATE.Text = mlCURRENTDATE
        mpITEMKEY.Text = ""
        mpITEMDESC.Text = ""
        mpUOM.Items.Clear()

    End Sub


    Sub LoadComboData()
        mlSQL = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID='MR_FORM' ORDER BY LinCode"
        mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISS")
        While mlREADER.Read
            mpDOCID.Items.Add(Trim(mlREADER("LinCode")) & "-" & mlREADER("Description"))
        End While

        ddENTITY.Items.Clear()
        ddENTITY.Items.Add("ISS")
        mlSQLTEMP = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID='ISS_Entity'"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISS")
        While mlRSTEMP.Read
            ddENTITY.Items.Add(Trim(mlRSTEMP("LinCode")))
        End While
    End Sub

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
            mlKEY = mpDOCID.Text
            Sql_1(mlSPTYPE, mlKEY)
        Catch ex As Exception
        End Try


        mlSYSCODE.Value = ""
        ClearFields()
        DisableCancel()
        RetrieveFieldsDetail()
        RetrieveFieldsDetail2()
    End Sub

    Function ValidateForm() As String
        ValidateForm = ""

        If mpITEMKEY.Text = "" Then
            ValidateForm = ValidateForm & " <br /> Item Codce not allowed empty"
        End If


        'mlSQLTEMP = "SELECT * FROM AP_MR_TEMPLATEHR WHERE DocNo = '" & mpDOCID.Text & "' "
        'mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP)
        'If mlRSTEMP.HasRows Then
        '    mlRSTEMP.Read()
        '    ValidateForm = ValidateForm & " <br /> MR Templates with ID of " & mpDOCID.Text & " are found in database (not allow duplicate)"
        'End If
        'mlOBJGS.CloseFile(mlRSTEMP)

      
    End Function

    Protected Sub btITEMKEY_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btITEMKEY.Click
        mpITEMDESC.Text = mlOBJPJ.ISS_INGeneralLostFocus("ITEMKEY", mpITEMKEY.Text, ddENTITY.Text)
        mpUOM.Items.Add(mlOBJPJ.ISS_INGeneralLostFocus("UOM", mpITEMKEY.Text, ddENTITY.Text))
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
            mpUOM.Items.Add(mlOBJPJ.ISS_INGeneralLostFocus("UOM", mpITEMKEY.Text, ""))
            'mpUOM.Items.Add(mlOBJPJ.ISS_INGeneralLostFocus("UOM", mpITEMKEY.Text))

        Catch ex As Exception
        End Try
    End Sub

    Sub SearchItem(ByVal mpTYPE As Byte, ByVal mpNAME As String)
        Select Case mpTYPE
            Case "1"
                Dim mlDTTEMP = New DataTable
                'mlRSTEMP = mlOBJPJ.ISS_INGeneralLookup("ITEMKEY", mpNAME, mlCOMPANYID)
                mlDTTEMP = mlOBJPJ.ISS_INGeneralLookup("ITEMKEY", mpNAME, mlCOMPANYID)
                mlDATAGRIDITEMKEY.DataSource = mlDTTEMP
                mlDATAGRIDITEMKEY.DataBind()

        End Select
    End Sub

    Sub Sql_2(ByVal mpSTATUS As String, ByVal mpCODE As String, ByVal mpITEMKEY As String)
        Dim mlSTATUS As String
        Dim mlNEWRECORD As Boolean
        Dim mlDOCID As String


        mlDOCID = Trim(mlOBJGF.GetStringAtPosition(mpDOCID.Text, 0, "-"))
        Try
            mlNEWRECORD = False

            Select Case mpSTATUS
                Case "Edit", "Delete"
                    mlSQL = ""
                    mlSQL = mlSQL & mlOBJPJ.ISS_AP_MRTemplate_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
            End Select

            Select Case mpSTATUS
                Case "New"
                    mlKEY = mlDOCID
                    mlNEWRECORD = True

                Case "Edit"
                    mlSTATUS = "Edit"
                    mlNEWRECORD = True
                    mlKEY = mlDOCID

                    mlSQL = mlSQL & " DELETE FROM AP_MR_TEMPLATEDT WHERE DocNo = '" & mlKEY & "' AND ItemKey = '" & mpITEMKEY & "';"

                Case "Delete"
                    mlSTATUS = "Delete"
                    mlSQL = mlSQL & " DELETE FROM AP_MR_TEMPLATEDT WHERE DocNo = '" & mlKEY & "' AND ItemKey = '" & mpITEMKEY & "';"
            End Select
            If mlOBJGF.IsNone(mlSQL) = False Then mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
            mlSQL = ""



        Catch ex As Exception

            mlOBJGS.XMtoLog("AP", "MRRequest", "MRRequest" & mlKEY, Err.Description, "New", Session("mgUSERID"), mlOBJGF.FormatDate(Now))
        End Try
    End Sub



    Sub Sql_1(ByVal mpSTATUS As String, ByVal mpCODE As String)
        Dim mlSTATUS As String
        Dim mlNEWRECORD As Boolean
        Dim mlDOCID As String
        Dim mlITEMDESC As String

        mlDOCID = Trim(mlOBJGF.GetStringAtPosition(mpDOCID.Text, 0, "-"))

        Try
            mlNEWRECORD = False

            Select Case mpSTATUS
                Case "Edit", "Delete"
                    mlSQL = ""
                    mlSQL = mlSQL & mlOBJPJ.ISS_AP_MRTemplate_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
            End Select

            Select Case mpSTATUS
                Case "New"
                    mlKEY = mlDOCID
                    mlNEWRECORD = True
                    mlSQL = mlSQL & " DELETE FROM AP_MR_TEMPLATEHR WHERE DocNo = '" & mlKEY & "';"

                Case "Edit"
                    mlSTATUS = "Edit"
                    mlNEWRECORD = True
                    mlKEY = mlDOCID

                    mlSQL = mlSQL & " DELETE FROM AP_MR_TEMPLATEHR WHERE DocNo = '" & mlKEY & "';"
                    mlSQL = mlSQL & " DELETE FROM AP_MR_TEMPLATEDT WHERE DocNo = '" & mlKEY & "' AND ItemKey = '" & Trim(mpITEMKEY.Text) & "';"

                Case "Delete"
                    mlSTATUS = "Delete"
                    mlSQL = mlSQL & " DELETE FROM AP_MR_TEMPLATEHR WHERE DocNo = '" & mlKEY & "';"
                    mlSQL = mlSQL & " DELETE FROM AP_MR_TEMPLATEDT WHERE DocNo = '" & mlKEY & "';"
            End Select
          

            If mlNEWRECORD = True Then
                mlSQL = ""
                mlITEMDESC = Trim(mpITEMDESC.Text)
                mlITEMDESC = Replace(mlITEMDESC, "'", "")

                mlSQL = mlSQL & " INSERT INTO AP_MR_TEMPLATEHR (ParentCode,DocNo,DocDate,RecordStatus,RecUserID,RecDate) VALUES ( " & _
                            " '" & mlFUNCTIONPARAMETER & "','" & mlKEY & "','" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(mpDOCDATE.Text, "/")) & "'," & _
                            " 'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "');"


                mlSQL = mlSQL & " INSERT INTO AP_MR_TEMPLATEDT (ParentCode,DocNo,Linno,ItemKey,Description,Uom,Qty,RecordStatus,RecUserID,RecDate) VALUES ( " & _
                            " '" & mlFUNCTIONPARAMETER & "','" & mlKEY & "','0','" & Trim(mpITEMKEY.Text) & "'," & _
                            " '" & mlITEMDESC & "','" & Trim(mpUOM.Text) & "','1'," & _
                            " 'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "');"

            End If


            Select Case mpSTATUS
                Case "New"
                    mlSQL = mlSQL & mlOBJPJ.ISS_AP_MRTemplate_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
            End Select
            mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
            mlSQL = ""

        Catch ex As Exception

            mlOBJGS.XMtoLog("MR", "MRRequest", "MRRequest" & mlKEY, Err.Description, "New", Session("mgUSERID"), mlOBJGF.FormatDate(Now))
        End Try
    End Sub

    
End Class
