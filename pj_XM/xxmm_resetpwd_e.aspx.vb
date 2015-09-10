Imports System
Imports System.Data
Imports System.Web.UI.HtmlControls
Imports System.Drawing
Imports System.Data.OleDb
Imports System.IO



Partial Class ad_resetpwd
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlENCRYPTCODE As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Reset Password to Mail V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Reset Password to Mail V2.00"
        mlENCRYPTCODE = System.Configuration.ConfigurationManager.AppSettings("mgENCRYPTCODE")
        mlOBJGS.Main()

        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "mm_resetpwd_e", "Member Reset Pswrd E", "")
        End If
    End Sub

    Sub ClearFields()
        'mlOLDPWD.Text = ""
        
    End Sub

    Protected Sub btSaveRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSaveRecord.Click
        SaveRecord()
    End Sub

    Sub SaveRecord()
        Dim mlNEWPWDN As String
        Dim mlOLDPWDN As String

        mlNEWPWDN = ""
        mlOLDPWDN = ""

        Try
            mlSQL = "SELECT * FROM AD_USERPROFILE WHERE UserID='" & Session("mgUSERID") & "' AND Password = '" & mlOLDPWDN & "'"
            mlREADER = mlOBJGS.DbRecordset(mlSQL)
            If mlREADER.HasRows Then
                mlSQL = "UPDATE AD_USERPROFILE SET Password = '" & mlNEWPWDN & "' WHERE UserID='" & Session("mgUSERID") & "'"
                mlOBJGS.ExecuteQuery(mlSQL)
                mlMESSAGE.Text = "Password Update Successfull "
            Else
                mlMESSAGE.Text = "Password Lama Anda Salah "
            End If

        Catch ex As Exception
            mlMESSAGE.Text = "Password Update Failed "
        End Try
    End Sub
End Class
