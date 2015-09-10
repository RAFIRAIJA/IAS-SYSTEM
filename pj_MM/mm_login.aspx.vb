Imports System
Imports System.Data
Imports System.Web.UI.HtmlControls
Imports System.Drawing
Imports System.Data.OleDb
Imports System.IO


Partial Class mm_login
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction

    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlENCRYPTCODE As String
    Dim mlIPADDRINFO As String
    Dim mpP1 As String
    Dim mpP2 As String
    Dim mpTYPE As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Member Login V2.04"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Member Login V2.04"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlENCRYPTCODE = System.Configuration.ConfigurationManager.AppSettings("mgENCRYPTCODE")

        mpTYPE = Request("mpFP")
        mpP1 = Request("mpP1")
        mpP2 = Request("mpP2")

        mlIPADDRINFO = ""
        mlIPADDRINFO = mlIPADDRINFO & " IP1= " & HttpContext.Current.Request.UserHostAddress & ";"
        mlIPADDRINFO = mlIPADDRINFO & " IP2= " & HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR") & ";"

        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "mm_login", "Member Login", "")
            'Session.Clear()
        End If

        Select Case mpTYPE
            Case "1"
                mpUSERID.Text = mlOBJGF.Decrypt(mpP1, mlENCRYPTCODE)
                mpPASSWORD.Text = mlOBJGF.Decrypt(mpP2, mlENCRYPTCODE)
                mlSUBMIT_Click(Nothing, Nothing)
            Case "2"
                If mpP2 <> "" Then
                    mlMESSAGE.Text = mpP2
                End If
        End Select

    End Sub

    Protected Sub mlSUBMIT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mlSUBMIT.Click
        Dim mlUSERID As String
        Dim mlPASSWORD As String

        mlUSERID = mpUSERID.Text
        mlPASSWORD = mpPASSWORD.Text
        mlPASSWORD = mlOBJGF.Encrypt(mlPASSWORD, mlENCRYPTCODE)

        mlSQL = "SELECT * FROM AR_DIST WHERE DistID = '" & UCase(mlUSERID) & "' AND Password= '" & mlPASSWORD & "'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL)
        If Not mlREADER.HasRows Then
            mlMESSAGE.Text = "UserID atau Password Salah, Silahkan Ulangi"
        End If

        If mlREADER.HasRows Then
            mlREADER.Read()
            Session("mgUSERID") = Trim(mlREADER("DistID"))
            Session("mgNAME") = Trim(mlREADER("Name"))
            Session("mgGROUPMENU") = "MM"
            Session.Timeout = "20"
            mlOBJGS.CloseFile(mlREADER)

            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "mm_login", "Login Member", mlIPADDRINFO)
            mlMESSAGE.Text = "Page akan Otomatis di alihkan ... "

            Select Case mpTYPE
                Case "2"
                    Response.Redirect(mpP1)
                Case Else
                    Response.Redirect("mm_home.aspx")
            End Select
        End If
        mlOBJGS.CloseFile(mlREADER)
    End Sub

   

End Class
