Imports System
Imports System.Data
Imports System.Data.OleDb
Imports IAS.Core.CSCode

Partial Class ad_updatepwd
    Inherits System.Web.UI.Page

    Dim mlOBJPJ As New ModuleFunctionLocal
    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlENCRYPTCODE As String
    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Update Password V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Update Password V2.00"
        mlENCRYPTCODE = System.Configuration.ConfigurationManager.AppSettings("mgENCRYPTCODE")
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ad_resetpassword", "update password", "")
        End If
    End Sub

    Sub ClearFields()
        mlOLDPWD.Text = ""
        mlNEWPWD.Text = ""
        mlCNEWPWD.Text = ""
    End Sub

    Protected Sub btSaveRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSaveRecord.Click
        SaveRecord()
    End Sub

    Sub SaveRecord()
        Dim mlNEWPWDN As String

        mlNEWPWDN = mlNEWPWD.Text
        mlNEWPWDN = mlOBJGF.Encrypt(mlNEWPWDN, mlENCRYPTCODE)

        Try
            mlSQL = "UPDATE AD_USERPROFILE SET Password = '" & mlNEWPWDN & "' WHERE UserID='" & Session("mgUSERID") & "'"
            mlOBJGS.ExecuteQuery(mlSQL, "AD", "AD")
            mlMESSAGE.Text = "Password Update Successfull "

        Catch ex As Exception
            mlMESSAGE.Text = "Password Update Failed "
        End Try
    End Sub
End Class
