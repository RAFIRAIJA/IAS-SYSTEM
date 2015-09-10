Imports System
Imports System.Data
Imports System.Web.UI.HtmlControls
Imports System.Drawing
Imports System.Data.OleDb
Imports System.IO
Imports IAS.Core.CSCode

Partial Class in_inv_addinfo_size
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
    Dim mlUNIVERSALID As String

    Dim mlCOMPANYTABLENAME As String
    Dim mlCOMPANYID As String

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Inventory Additional Info V2.01"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Inventory Additional Info V2.01"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlCOMPANYID = mlOBJPJ.ISS_GetCompanyID("1", ddENTITY.Text)
        mlCOMPANYTABLENAME = mlOBJPJ.ISS_GetCompanyID("2", ddENTITY.Text)
        mlUNIVERSALID = "Item_Size"
        mlFUNCTIONPARAMETER = "1"
        If Page.IsPostBack = False Then
            LoadComboData()
            DisableCancel()
            RetrieveFieldsDetail("")
            pnSEARCHITEMKEY.Visible = False
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "in_inv_addinfo", "in_inv_addinfo", "")
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
                EditRecord()
            Case "DeleteRecord"
                mlMESSAGE.Text = "Delete Request for  " & e.CommandArgument
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

        mlSQL = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID  = '" & mlUNIVERSALID & "' AND  LinCode = '" & Trim(mlKEY) & "' AND AdditionalDescription1 = '" & Trim(mlKEY2) & "'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3")
        If mlREADER.HasRows Then
            mlREADER.Read()
            mpITEMKEY.Text = mlREADER("LinCode") & ""
            mpITEMDESC.Text = mlREADER("Description") & ""
            mpSIZE.Text = mlREADER("AdditionalDescription1") & ""
        End If
    End Sub

    Sub RetrieveFieldsDetail(ByVal mpSQL As String)
        If mlOBJGF.IsNone(mpSQL) = True Then
            mlSQL2 = "SELECT LinCode AS ItemCode,Description,AdditionalDescription1 as Size,AdditionalDescription2 AS Entity FROM XM_UNIVERSALLOOKUPLIN WHERE UNIVERSALID='" & mlUNIVERSALID & "' AND AdditionalDescription2='" & ddENTITY.Text & "' ORDER BY LinCode"
        Else
            mlSQL2 = mpSQL
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

        mlSYSCODE.Value = ""
        RetrieveFields()
        RetrieveFieldsDetail("")
    End Sub

    Sub NewRecord()
        EnableCancel()
        ClearFields()
    End Sub

    Sub EditRecord()
        RetrieveFields()
        EnableCancel()
    End Sub

    Function ValidateForm() As String
        ValidateForm = ""

        If mpITEMDESC.Text = "" Then
            ValidateForm = ValidateForm & " <br /> Item Code or Item Description not allowed empty"
        End If

        mlSQLTEMP = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE LinCode = '" & Trim(mpITEMKEY.Text) & "'" & _
                " AND AdditionalDescription1 = '" & Trim(mpSIZE.Text) & "' AND AdditionalDescription2 = '" & DDENTITY.text & "' "
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
        If mlRSTEMP.HasRows Then
            ValidateForm = ValidateForm & " <br /> Item Code for " & Trim(mpITEMKEY.Text) & " with size of " & Trim(mpSIZE.Text) & " was found in Database (not allow duplicate) "
        End If
    End Function

    Sub LoadComboData()
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
        pnGRID.Visible = False
        mlOBJPJ.SetTextBox(False, mpITEMKEY)
        mlOBJPJ.SetTextBox(False, mpSIZE)
    End Sub

    Private Sub DisableCancel()
        btNewRecord.Visible = True
        btSaveRecord.Visible = False
        pnFILL.Visible = False
        pnGRID.Visible = True
        mlOBJPJ.SetTextBox(True, mpITEMKEY)
        mlOBJPJ.SetTextBox(True, mpSIZE)
    End Sub

    Sub ClearFields()
        mpITEMKEY.Text = ""
        mpITEMDESC.Text = ""
        mpSIZE.Text = ""
    End Sub

    Protected Sub btITEMKEY_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btITEMKEY.Click
        mpITEMDESC.Text = mlOBJPJ.ISS_INGeneralLostFocus("ITEMKEY", Trim(mpITEMKEY.Text), ddENTITY.Text)
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
                mlSQLTEMP = "SELECT No_, Description FROM [" & mlCOMPANYTABLENAME & "Item] INV WHERE Description LIKE  '%" & mpNAME & "%'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", mlCOMPANYID)
                mlDATAGRIDITEMKEY.DataSource = mlRSTEMP
                mlDATAGRIDITEMKEY.DataBind()
        End Select
    End Sub

    Protected Sub btSearchRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSearchRecord.Click
        SearchRecord()
    End Sub

    Protected Sub btENTITY_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btENTITY.Click
        RetrieveFieldsDetail("")
    End Sub

    Sub SearchRecord()
        Dim mlSQLSEARCH2 As String

        mlMESSAGE.Text = ""
        mlSQLSEARCH2 = ""
        mlSQL = ""

        If pnFILL.Visible = True Then
            Try
                If mlOBJGF.IsNone(mpSIZE.Text) = False Then
                    mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " AdditionalDescription1 LIKE '" & mpSIZE.Text & "' AND AdditionalDescription2='" & ddENTITY.Text & "' AND "
                End If

                mlSQL = "SELECT  LinCode AS ItemCode,Description,AdditionalDescription1 as Size,AdditionalDescription2 AS Entity  FROM XM_UNIVERSALLOOKUPLIN WHERE " & mlSQL & " UniversalID = '" & mlUNIVERSALID & "' AND LinCode = '" & Trim(mpITEMKEY.Text) & "';"
                mlSQLSTATEMENT.Text = mlSQL
                RetrieveFieldsDetail(mlSQLSTATEMENT.Text)
                DisableCancel()

            Catch ex As Exception
                Response.Write(mlSQL)
                Response.Write(mlSQL)
            End Try
        Else
            EnableCancel()
            ClearFields()
            pnFILL.Visible = True
        End If

    End Sub


    Sub Sql_1(ByVal mpSTATUS As String, ByVal mpCODE As String)
        Dim mlSTATUS As String
        Dim mlNEWRECORD As Boolean
        Dim mlI As Byte
        Dim mlTOTAL As Double

        Try
            mlNEWRECORD = False
            mlTOTAL = 0

            Select Case mpSTATUS
                Case "Edit", "Delete"
                    mlSQL = ""
                    'mlSQL = mlSQL & mlOBJPJ.ISS_IN_INADDINFO_SIZE_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
            End Select

            Select Case mpSTATUS
                Case "New"
                    mlNEWRECORD = True
                    mlKEY = Trim(mpITEMKEY.Text)

                Case "Edit"
                    mlSTATUS = "Edit"
                    mlNEWRECORD = True
                    mlSQL = mlSQL & " DELETE FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID = '" & mlUNIVERSALID & "' AND LinCode = '" & mlKEY & "' AND AdditionalDescription1 = '" & Trim(mlKEY2) & "';"

                Case "Delete"
                    mlSTATUS = "Delete"
                    mlSQL = mlSQL & " DELETE FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID = '" & mlUNIVERSALID & "' AND LinCode = '" & mlKEY & "' AND AdditionalDescription1 = '" & Trim(mlKEY2) & "';"
            End Select
            If mlOBJGF.IsNone(mlSQL) = False Then mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
            mlSQL = ""

            If mlNEWRECORD = True Then
                mlSQL = ""
                mlSQL = mlSQL & " INSERT INTO XM_UNIVERSALLOOKUPLIN (UniversalID,LinCode,Description," & _
                    " AdditionalDescription1,AdditionalDescription2,AdditionalDescription3)" & _
                    " VALUES ( " & _
                    " '" & mlUNIVERSALID & "','" & mlKEY & "','" & Replace(Trim(mpITEMDESC.Text), "'", "`") & "'," & _
                    " '" & Trim(mpSIZE.Text) & "','" & Trim(ddENTITY.Text) & "','');"
            End If


            Select Case mpSTATUS
                Case "New"
                    'mlSQL = mlSQL & mlOBJPJ.ISS_IN_INADDINFO_SIZE_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
            End Select
            mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
            mlSQL = ""

        Catch ex As Exception
            mlOBJGS.XMtoLog("MR", "in_inv_addinfo", "in_inv_addinfo" & mlKEY, Err.Description, "New", Session("mgUSERID"), mlOBJGF.FormatDate(Now))
        End Try
    End Sub


    
End Class
