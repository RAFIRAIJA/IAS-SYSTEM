Imports System
Imports System.Data
Imports System.Data.OleDb


Partial Class ad_Login
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
        mlTITLE.Text = "Admin Login V2.05"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Admin Login V2.05"
        mlOBJGS.Main()
        'If mlOBJGS.ValidateExpiredDate() = True Then
        '    Exit Sub
        'End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")



        mlENCRYPTCODE = System.Configuration.ConfigurationManager.AppSettings("mgENCRYPTCODE")


        Try
            mpTYPE = Request("mpFP")
            mpP1 = Request("mpP1")
            mpP2 = Request("mpP2")

            mlIPADDRINFO = ""
            mlIPADDRINFO = mlIPADDRINFO & " IP1= " & HttpContext.Current.Request.UserHostAddress & ";"
            mlIPADDRINFO = mlIPADDRINFO & " IP2= " & HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR") & ";"

            If Page.IsPostBack = False Then
                mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ad_login", "Login Admin", mlIPADDRINFO)
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
        Catch ex As Exception
            Response.Write(Err.Description)
        End Try
    End Sub

    Function IsVal_MsSqlCheatCharacter(ByVal mpVALUE As String) As Boolean
        Dim mlVALUE As String
        Dim mlPATTERN As String
        Dim mlMATCH As Match

        IsVal_MsSqlCheatCharacter = False
        mlVALUE = mpVALUE
        mlPATTERN = "^[a-zA-Z0-9'.\s]{1,40}$"
        mlMATCH = Regex.Match(mlVALUE.Trim(), mlPATTERN, RegexOptions.IgnoreCase)

        If (mlMATCH.Success) = True Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Sub mlSUBMIT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mlSUBMIT.Click
        Dim mlUSERID As String
        Dim mlPASSWORD As String
        Dim mlNUMBERLOGIN As Integer

        Try

            mlMESSAGE.Text = "loading"

            mlUSERID = mpUSERID.Text
            mlPASSWORD = mpPASSWORD.Text
            mlPASSWORD = mlOBJGF.Encrypt(mlPASSWORD, mlENCRYPTCODE)

	    If IsVal_MsSqlCheatCharacter(mlUSERID) = False Then
                mlMESSAGE.Text = "UserID atau Password Salah, Silahkan Ulangi"
		exit sub
	    End If

            mlSQL = "SELECT * FROM AD_USERPROFILE WHERE UserID = '" & UCase(mlUSERID) & "' AND Password= '" & mlPASSWORD & "'"
            mlMESSAGE.Text = "loading 2"
            mlREADER = mlOBJGS.DbRecordset(mlSQL, "AD", "AD")
            mlMESSAGE.Text = "loading3"
            If Not mlREADER.HasRows Then
                mlMESSAGE.Text = "UserID atau Password Salah, Silahkan Ulangi"
            End If


            If mlREADER.HasRows Then
                mlREADER.Read()
                Session("mgUSERID") = Trim(mlREADER("UserID"))
                Session("mgNAME") = Trim(mlREADER("Name"))
                Session("mgGROUPMENU") = Trim(mlREADER("GroupID"))
                Session("mgUSERMAIL") = Trim(mlREADER("EmailAddr")) & ""
                Session("mgUSERHP") = Trim(mlREADER("TelHP")) & ""
                Session("mgACTIVECOMPANY") = Trim(mlREADER("LastCompany")) & ""
                Session("mgACTIVESYSTEM") = Trim(mlREADER("LastSystem")) & ""
                Session.Timeout = "20"
                mlNUMBERLOGIN = IIf(mlOBJGF.IsNone(mlREADER("NumberLogin")), 0, mlREADER("NumberLogin")) + 1
                mlOBJGS.CloseFile(mlREADER)

                mlSQL = "UPDATE AD_USERPROFILE SET IsLogin = '1'," & _
                "NumberLogin = '" & mlNUMBERLOGIN & "' WHERE UserID = '" & Session("mgUSERID") & "'"
                mlOBJGS.ExecuteQuery(mlSQL, "AD", "AD")

                mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ad_login", "Login Admin", mlIPADDRINFO)
                mlMESSAGE.Text = "Page akan Otomatis di alihkan ... "

                Select Case mpTYPE
                    Case "2"
                        Response.Redirect(mpP1)
                    Case Else
                        Response.Redirect("ad_home.aspx")
                End Select
            End If
            mlOBJGS.CloseFile(mlREADER)

        Catch ex As Exception
        End Try
    End Sub
End Class
