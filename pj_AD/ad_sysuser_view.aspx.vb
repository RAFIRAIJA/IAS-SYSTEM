Imports System
Imports System.Data
Imports System.Data.OleDb


Partial Class ad_sysuser_view
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
        mlTITLE.Text = "User Profile V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "User Profile V2.00"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlKEY = Session("mgUSERID")
        RetrieveFields()
        DisableCancel()

        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ad_systemuser", "System User", "")
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


    Public Sub RetrieveFields()
        On Error Resume Next
        DisableCancel()


        mlSQL = "SELECT * FROM AD_USERPROFILE WHERE UserID = '" & Trim(mlKEY) & "'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL, "AD", "AD")
        If mlREADER.HasRows Then
            mlREADER.Read()

            mlUSERID.Text = Trim(mlREADER("UserID")) & ""
            mlNAMA.Text = Trim(mlREADER("Name")) & ""
            mlDEPT.Text = mlREADER("DeptID")
            mlSTATUS.Text = mlREADER("UserStatus")
            mlMOBILENO.Text = Trim(mlREADER("TelHP")) & ""
            mlEMAIL.Text = Trim(mlREADER("EmailAddr")) & ""
            mlSTAFFID.Text = Trim(mlREADER("EmployeeID")) & ""
            mlABSTAINID.Text = Trim(mlREADER("FingerPrintID")) & ""
            mlAPPLICATIONID.Text = Trim(mlREADER("ApplicationID")) & ""

            mlCOMPANY.Text = Trim(mlREADER("CompanyID")) & ""
            mlBRANCH.Text = Trim(mlREADER("BranchID")) & ""

        End If
        mlOBJGS.CloseFile(mlREADER)
    End Sub

    Sub RetrieveFieldsDetail()
    End Sub

    Sub DeleteRecord()
    End Sub

    Sub NewRecord()
        mlOBJGS.mgNEW = True
        mlOBJGS.mgEDIT = False
        EnableCancel()
        ClearFields()
        LoadComboData()
        mlUSERID.Enabled = True

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
    End Sub

    Private Sub DisableCancel()
        pnFILL.Visible = True


        mlUSERID.Enabled = False
        mlNAMA.Enabled = False
        mlDEPT.Enabled = False
        mlSTATUS.Enabled = False
        mlMOBILENO.Enabled = False
        mlEMAIL.Enabled = False
        mlSTAFFID.Enabled = False
        mlABSTAINID.Enabled = False
        mlAPPLICATIONID.Enabled = False
    End Sub

    Sub ClearFields()
        mlDEPT.Text = ""
        mlSTATUS.Text = ""
        mlUSERID.Text = ""
        mlNAMA.Text = ""
        mlDEPT.Text = ""
        mlSTATUS.Text = ""
        mlMOBILENO.Text = ""
        mlEMAIL.Text = ""
        mlSTAFFID.Text = ""
        mlABSTAINID.Text = ""
        mlAPPLICATIONID.Text = ""
    End Sub


    Sub LoadComboData()

    End Sub

    Sub SaveRecord()
    End Sub




End Class
