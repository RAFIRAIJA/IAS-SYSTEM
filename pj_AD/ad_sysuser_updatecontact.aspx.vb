Imports System
Imports System.Data
Imports System.Data.OleDb

Partial Class ad_sysuser_updatecontact
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlKEY As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Update Contact V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Update Contact V2.00"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        If Page.IsPostBack = False Then
            mlKEY = Session("mgUSERID")
            RetrieveFields()
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ad_resetpassword", "update password", "")
        End If
    End Sub

    Sub ClearFields()
        txEMAIL.text = ""
        txMOBILENO.Text = ""
    End Sub

    Protected Sub btSaveRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSaveRecord.Click
        SaveRecord()
    End Sub

    Public Sub RetrieveFields()
        Try

        
            mlSQL = "SELECT * FROM AD_USERPROFILE WHERE UserID = '" & Trim(mlKEY) & "'"
            mlREADER = mlOBJGS.DbRecordset(mlSQL, "AD", "AD")
            If mlREADER.HasRows Then
                mlREADER.Read()
                txMOBILENO.Text = Trim(mlREADER("TelHP")) & ""
                txEMAIL.Text = Trim(mlREADER("EmailAddr")) & ""
            End If
            mlOBJGS.CloseFile(mlREADER)

        Catch ex As Exception

        End Try
    End Sub


    Sub SaveRecord()
        Try
            mlSQL = "UPDATE AD_USERPROFILE SET EmailAddr='" & Trim(txEMAIL.Text) & "', TelHP = '" & Trim(txMOBILENO.Text) & "'" & _
                    " WHERE UserID='" & Session("mgUSERID") & "'"
            mlOBJGS.ExecuteQuery(mlSQL, "AD", "AD")
            mlMESSAGE.Text = "Contact Update Successfull "

        Catch ex As Exception
            mlMESSAGE.Text = "Contact Update Failed "
        End Try
    End Sub
End Class
