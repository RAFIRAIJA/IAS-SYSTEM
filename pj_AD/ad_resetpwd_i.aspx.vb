Imports System
Imports System.Data
Imports System.Web.UI.HtmlControls
Imports System.Drawing
Imports System.Data.OleDb
Imports System.IO
Imports IASClass


Partial Class backoffice_ad_resetpwd_i
    Inherits System.Web.UI.Page
    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction


    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlENCRYPTCODE As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Reset Internal Password V2.01"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Reset Internal Password V2.01"
        mlENCRYPTCODE = System.Configuration.ConfigurationManager.AppSettings("mgENCRYPTCODE")
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ad_resetpwd_i", "reset password admin", "")
        End If
    End Sub

    Sub ClearFields()
        mluserid.Text = ""
    End Sub

    Protected Sub btSaveRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSaveRecord.Click
        SaveRecord()
    End Sub

    Sub SaveRecord()
        If Page.IsPostBack Then
            Dim mlNEWPWDN As String

            mlSQL = "SELECT UserID FROM AD_USERPROFILE WHERE UserID = '" & mlUSERID.Text & "'"
            mlREADER = mlOBJGS.DbRecordset(mlSQL, "AD", "AD")
            If mlREADER.HasRows Then
                mlNEWPWDN = mlOBJGF.GetRandomPasswordUsingGUID(6)
                mlMESSAGE.Text = "Reset Password Successfull, New Password for " & mlUSERID.Text & " is : " & mlNEWPWDN
                mlNEWPWDN = mlOBJGF.Encrypt(mlNEWPWDN, mlENCRYPTCODE)

                mlSQL = "UPDATE AD_USERPROFILE SET Password = '" & mlNEWPWDN & "' WHERE UserID='" & mlUSERID.Text & "'"
                mlOBJGS.ExecuteQuery(mlSQL, "AD", "AD")
            Else
                mlMESSAGE.Text = "User ID : " & mlUSERID.Text & " not found "
            End If
            mlOBJGS.CloseFile(mlREADER)
        End If
    End Sub

End Class
