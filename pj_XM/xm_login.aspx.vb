Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Data.OleDb



Partial Class xm_login
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlENCRYPTCODE As String

    Protected Sub mlSUBMIT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mlSUBMIT.Click
        Dim mlUSERID As String
        Dim mlPASSWORD As String

        Try
            mlUSERID = mpUSERID.Text
            mlPASSWORD = mpPASSWORD.Text
            mlPASSWORD = mlOBJGF.Encrypt(mlPASSWORD, mlENCRYPTCODE)

            mlSQL = "SELECT * FROM XM_ADDRESSBOOK WHERE UserID = '" & UCase(mlUSERID) & "' AND Password= '" & mlPASSWORD & "'"
            mlREADER = mlOBJGS.DbRecordset(mlSQL)
            If Not mlREADER.HasRows Then
                mlMESSAGE.Text = "UserID atau Password Salah, Silahkan Ulangi"
            End If

            If mlREADER.HasRows Then
                mlREADER.Read()
                Session("mgUSERID") = Trim(mlREADER("AddressKey"))
                Session("mgNAME") = Trim(mlREADER("Name"))
                Session("mgGROUPMENU") = "ST"
                mlMESSAGE.Text = "Page akan Otomatis di alihkan ... "
                Response.Redirect("xm_home.aspx")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Stockist Login V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Stockist Login V2.00"
        mlENCRYPTCODE = System.Configuration.ConfigurationManager.AppSettings("mgENCRYPTCODE")

        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "xm_login", "Stockist Login", "")
        End If

        Session.Clear()
    End Sub
End Class
