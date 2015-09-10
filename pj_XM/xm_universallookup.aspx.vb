Imports System
Imports System.Data
Imports System.Web.UI.HtmlControls
Imports System.Drawing
Imports System.Data.OleDb


Partial Class xm_universallookup
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction

    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlREADER2 As OleDb.OleDbDataReader
    Dim mlSQL2 As String

    Dim mlKEY As String
    Dim mlRECORDSTATUS As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Universal Master Data V2.01"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Universal Master Data V2.01"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")
        
        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "xm_universallookup", "Universal Lookup", "")
            DisableCancel()
            LoadComboData()
            RetrieveFieldsDetail()
        Else
        End If
    End Sub


    Protected Sub mlDATAGRID_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRID.ItemCommand
        mlKEY = e.CommandArgument
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

        mlSQL = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID = '" & Trim(mlUNIVID.Text) & "' AND LinCode= '" & Trim(mlKEY) & "'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL)
        If mlREADER.HasRows Then
            mlREADER.Read()
            mlLINCODE.Text = mlREADER("LinCode") & ""
            mlDESC.Text = mlREADER("Description") & ""
            mlDESC1.Text = mlREADER("AdditionalDescription1") & ""
            mlDESC2.Text = mlREADER("AdditionalDescription2") & ""
            mlDESC3.Text = mlREADER("AdditionalDescription3") & ""
        End If
    End Sub

    Sub RetrieveFieldsDetail()
        mlSQL2 = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID = '" & Trim(mlUNIVID.Text) & "'"
        mlREADER2 = mlOBJGS.DbRecordset(mlSQL2)
        mlDATAGRID.DataSource = mlREADER2
        mlDATAGRID.DataBind()
    End Sub

    Sub DeleteRecord()
        mlRECORDSTATUS = "Delete"
        mlSQL = "DELETE FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID= '" & Trim(mlUNIVID.Text) & "' AND LinCode= '" & Trim(mlKEY) & "'"

        mlOBJGS.ExecuteQuery(mlSQL)
        RetrieveFields()
        RetrieveFieldsDetail()
    End Sub

    Sub NewRecord()
        EnableCancel()
        ClearFields()
    End Sub

    Sub EditRecord()
        RetrieveFields()
        EnableCancel()
    End Sub

    Sub SaveRecord()
        Dim mlKEY As String
        

        mlKEY = Trim(mlLINCODE.Text)
        mlSQL = "DELETE FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID= '" & Trim(mlUNIVID.Text) & "' AND LinCode= '" & Trim(mlKEY) & "'"
        mlOBJGS.ExecuteQuery(mlSQL)

        mlSQL = "INSERT INTO XM_UNIVERSALLOOKUPLIN (UniversalID,LinCode,Description,AdditionalDescription1,AdditionalDescription2,AdditionalDescription3) " & _
               " VALUES ('" & Trim(mlUNIVID.Text) & "', '" & Trim(mlLINCODE.Text) & "', '" & Trim(mlDESC.Text) & "', '" & Trim(mlDESC1.Text) & "'," & _
               " '" & Trim(mlDESC2.Text) & "', '" & Trim(mlDESC3.Text) & "')"
        mlOBJGS.ExecuteQuery(mlSQL)

        DisableCancel()
        RetrieveFieldsDetail()
    End Sub


    Private Sub EnableCancel()
        btNewRecord.Visible = False
        btSaveRecord.Visible = True
        pnFILL.Visible = True

        mlLINCODE.Enabled = True
        mlDESC.Enabled = True
        mlDESC1.Enabled = True
        mlDESC2.Enabled = True
        mlDESC3.Enabled = True
    End Sub

    Private Sub DisableCancel()
        btNewRecord.Visible = True
        btSaveRecord.Visible = False
        pnFILL.Visible = False

        mlLINCODE.Enabled = False
        mlDESC.Enabled = False
        mlDESC1.Enabled = False
        mlDESC2.Enabled = False
        mlDESC3.Enabled = False
    End Sub

    Sub ClearFields()
        mlLINCODE.Text = ""
        mlDESC.Text = ""
        mlDESC1.Text = ""
        mlDESC2.Text = ""
        mlDESC3.Text = ""
    End Sub


    Sub LoadComboData()
        mlUNIVID.Items.Clear()
        mlSQL = "SELECT DISTINCT UniversalID FROM XM_UNIVERSALLOOKUPLIN ORDER BY UniversalID"
        mlREADER = mlOBJGS.DbRecordset(mlSQL)
        While mlREADER.Read
            mlUNIVID.Items.Add(Trim(mlREADER("UniversalID")))
        End While
    End Sub


    Protected Sub mlSUBMIT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mlSUBMIT.Click
        DisableCancel()
        RetrieveFieldsDetail()
    End Sub

    Protected Sub mlBTNEW_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mlBTNEW.Click
        Dim mlNEWID As String

        Try
            If mlUNIVIDNEW.Text <> "" Then
                mlNEWID = mlUNIVIDNEW.Text
                mlUNIVID.Items.Add(Trim(mlNEWID))
                mlUNIVID.SelectedValue = mlNEWID
            End If
            
        Catch ex As Exception

        End Try
    End Sub
End Class
