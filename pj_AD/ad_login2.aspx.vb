Imports System
Imports System.Data
Imports System.Web.UI.HtmlControls
Imports System.Drawing
Imports System.Data.OleDb
Imports System.IO
Imports IASClass
Imports IAS.Core.CSCode
Imports IAS.Initialize
Imports IAS.APP.DataAccess.AD

Partial Class ad_Login2
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJIS As New IASClass.ucmImageSystem

    Dim mlREADER As OleDb.OleDbDataReader


    Dim oMDBF As New IAS.Core.CSCode.ModuleDBFunction()
    Dim oMGF As New IAS.Core.CSCode.ModuleGeneralFunction()
    Dim oMGS As New IAS.Core.CSCode.ModuleGeneralSystem()
    Dim oMI As New IAS.Core.CSCode.ModuleInitialization()

    Dim oFunc As New IAS.Core.CSCode.FunctionCore()
    Dim oVar As New IAS.Core.CSCode.VariableCore()

    Dim oEnt As New IAS.APP.DataAccess.AD.VariableAD()
    Dim oDA As New IAS.APP.DataAccess.AD.FunctionAD()


    Dim mlSQL As String
    Dim mlENCRYPTCODE As String
    Dim mlIPADDRINFO As String
    Dim mpP1 As String
    Dim mpP2 As String
    Dim mpTYPE As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Admin Login (Image Security) V2.09"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Admin Login (Image Security) V2.09"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
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
                RunCreateImage()
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

            If Page.IsPostBack = True Then
                mlSUBMIT_Click(Nothing, Nothing)
            End If

        Catch ex As Exception
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
        Dim mlDTListData As New DataTable()

        mlMESSAGE.Text = ""
        If UCase(Trim(mlSYSCODE.Value)) <> UCase(Trim(mpIMAGETEXT.Text)) Then
            mlMESSAGE.Text = "Text Image Salah, Silahkan di Ulangi"
            RunCreateImage()
            mpIMAGETEXT.Focus()
            Exit Sub
        End If

        Try

            mlUSERID = mpUSERID.Text
            mlPASSWORD = mpPASSWORD.Text
            mlPASSWORD = mlOBJGF.Encrypt(mlPASSWORD, mlENCRYPTCODE)

	    If IsVal_MsSqlCheatCharacter(mlUSERID) = False Then
                mlMESSAGE.Text = "UserID atau Password Salah, Silahkan Ulangi"
		exit sub
	    End If

            mlSQL = "SELECT * FROM AD_USERPROFILE WHERE UserID = '" & UCase(mlUSERID) & "' AND Password= '" & mlPASSWORD & "'"
            mlDTListData = oMDBF.OpenRecordSet("AD", "AD", mlSQL, CommandType.Text).Tables(0)
            'mlREADER = mlOBJGS.DbRecordset(mlSQL, "AD", "AD")
            'If Not mlREADER.HasRows Then
            '    mlMESSAGE.Text = "UserID atau Password Salah, Silahkan Ulangi"
            '    RunCreateImage()
            '    mpPASSWORD.Focus()
            'End If

            If mlDTListData.Rows.Count <= 0 Then
                mlMESSAGE.Text = "UserID atau Password Salah, Silahkan Ulangi"
                RunCreateImage()
                mpPASSWORD.Focus()
            Else                
                Session("mgUSERID") = mlDTListData.Rows(0)("UserID").ToString()
                Session("mgNAME") = mlDTListData.Rows(0)("Name").ToString()
                Session("mgGROUPMENU") = mlDTListData.Rows(0)("GroupID").ToString()
                Session("mgUSERMAIL") = mlDTListData.Rows(0)("EmailAddr").ToString()
                Session("mgUSERHP") = mlDTListData.Rows(0)("TelHP").ToString()
                Session("mgACTIVECOMPANY") = mlDTListData.Rows(0)("LastCompany").ToString()
                Session("mgACTIVESYSTEM") = mlDTListData.Rows(0)("LastSystem").ToString()
                Session("mgMENUSTYLE") = mlDTListData.Rows(0)("MenuStyle").ToString()

                Session("mgServerName") = ModuleBaseSetting.DATASOURCE.ToString()
                Session("mgDBName") = ModuleBaseSetting.DATABASE.ToString()
                Session("mgDBUserID") = ModuleBaseSetting.SYSTEMUID.ToString()

                If (ModuleBaseSetting.SYSTEMPASSWORD.ToString() = "") Then
                    Session("mgPassword") = ""
                Else
                    Session("mgPassword") = ModuleBaseSetting.SYSTEMPASSWORD.ToString()
                End If

                Session("mgDatabaseType") = ModuleBaseSetting.DATABASETYPE.ToString()
                Session("mgDataSource") = ModuleBaseSetting.DATASOURCE.ToString()
                Session("mgDataBase") = ModuleBaseSetting.DATABASE.ToString()
                Session("mgSystemUID") = ModuleBaseSetting.SYSTEMUID.ToString()
                Session("mgSystemPassword") = ModuleBaseSetting.SYSTEMPASSWORD.ToString()
                Session("mgDatabaseDriver") = ModuleBaseSetting.DATABASEDRIVER.ToString()
                Session("mgDateTime") = DateTime.Now.ToString()

                mlNUMBERLOGIN = IIf(mlOBJGF.IsNone(mlDTListData.Rows(0)("NumberLogin").ToString()), 0, mlDTListData.Rows(0)("NumberLogin").ToString()) + 1
                mlOBJGS.CloseFile(mlREADER)

                mlSQL = "UPDATE AD_USERPROFILE SET IsLogin = '1'," & _
                "NumberLogin = '" & mlNUMBERLOGIN & "' WHERE UserID = '" & Session("mgUSERID") & "'"
                oMDBF.ExecRecordSet("AD", "AD", mlSQL, CommandType.Text)

                oDA.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ad_login", "Login Admin", mlIPADDRINFO)

                mlMESSAGE.Text = "Page akan Otomatis di alihkan ... "
                Select Case mpTYPE
                    Case "2"
                        Response.Redirect(mpP1)
                    Case Else
                        If Session("mgMENUSTYLE").ToString() = "MENUSMENU" Then
                            Response.Redirect("ad_home.aspx")
                        ElseIf Session("mgMENUSTYLE").ToString() = "TREE" Then
                            Response.Redirect("ad_menuutama.aspx")      'menu treeview
                        End If
                End Select


                'mlSQL = "SELECT * FROM AD_USERPROFILE WHERE UserID = '" & UCase(mlUSERID) & "' AND Password= '" & mlPASSWORD & "'"
                'mlREADER = mlOBJGS.DbRecordset(mlSQL, "AD", "AD")
                'If Not mlREADER.HasRows Then
                '    mlMESSAGE.Text = "UserID atau Password Salah, Silahkan Ulangi"
                '    RunCreateImage()
                '    mpPASSWORD.Focus()
                'End If

                'If mlREADER.HasRows Then
                '    mlREADER.Read()
                '    Session("mgUSERID") = Trim(mlREADER("UserID"))
                '    Session("mgNAME") = Trim(mlREADER("Name"))
                '    Session("mgGROUPMENU") = Trim(mlREADER("GroupID"))
                '    Session("mgUSERMAIL") = Trim(mlREADER("EmailAddr")) & ""
                '    Session("mgUSERHP") = Trim(mlREADER("TelHP")) & ""
                '    Session("mgACTIVECOMPANY") = Trim(mlREADER("LastCompany")) & " "
                '    Session("mgACTIVESYSTEM") = Trim(mlREADER("LastSystem")) & ""

                '    Session("mgServerName") = Trim(mlOBJGS.mgDATASOURCE.ToString())
                '    Session("mgDBName") = Trim(mlOBJGS.mgDATABASE.ToString())
                '    Session("mgDBUserID") = Trim(mlOBJGS.mgSYSTEMUID.ToString())
                '    Session("mgPassword") = IIf(IsNothing(Trim(mlOBJGS.mgSYSTEMPASSWORD.ToString())), "", Trim(mlOBJGS.mgSYSTEMPASSWORD.ToString()))
                '    Session("mgDatabaseType") = Trim(mlOBJGS.mgDATABASETYPE.ToString())
                '    Session("mgDataSource") = Trim(mlOBJGS.mgDATASOURCE.ToString())
                '    Session("mgDataBase") = Trim(mlOBJGS.mgDATABASE.ToString())
                '    Session("mgSystemUID") = Trim(mlOBJGS.mgSYSTEMUID.ToString())
                '    Session("mgSystemPassword") = Trim(mlOBJGS.mgSYSTEMPASSWORD.ToString())
                '    Session("mgDatabaseDriver") = Trim(mlOBJGS.mgDATABASEDRIVER.ToString())
                '    Session("mgDateTime") = DateAndTime.Now.ToString()

                '    mlNUMBERLOGIN = IIf(mlOBJGF.IsNone(mlREADER("NumberLogin")), 0, mlREADER("NumberLogin")) + 1
                '    mlOBJGS.CloseFile(mlREADER)

                '    mlSQL = "UPDATE AD_USERPROFILE SET IsLogin = '1'," & _
                '    "NumberLogin = '" & mlNUMBERLOGIN & "' WHERE UserID = '" & Session("mgUSERID") & "'"
                '    mlOBJGS.ExecuteQuery(mlSQL, "AD", "AD")

                '    mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ad_login", "Login Admin", mlIPADDRINFO)
                '    mlMESSAGE.Text = "Page akan Otomatis di alihkan ... "
                '    Select Case mpTYPE
                '        Case "2"
                '            Response.Redirect(mpP1)
                '        Case Else
                '            'Response.Redirect("ad_home.aspx")          'menu dropdown style
                '            'Response.Redirect("ad_main.aspx")
                '            Response.Redirect("ad_menuutama.aspx")      'menu treeview
                '    End Select
                '    '
                'End If
            End If

            mlOBJGS.CloseFile(mlREADER)

        Catch ex As Exception

        End Try
    End Sub

    Sub RunCreateImage()
        mpIMAGETEXT.Text = ""

        Try
            Dim mlPATH As String
            Dim mlPATH2 As String
            mlPATH = Server.MapPath("..\output\vimage.jpeg")
            Dim mlVCODE As String = mlOBJIS.LoginImg_GenerateVCodeImage(mlPATH)
            mlPATH2 = "..\output\vimage.jpeg" & "?t=" & Now.Ticks.ToString
            mpVIMAGE.ImageUrl = mlPATH2
            mlSYSCODE.Value = mlVCODE

        Catch ex As Exception
            Response.Write(Err.Description)
        End Try
    End Sub

    Protected Sub btRELOAD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btRELOAD.Click
        RunCreateImage()
    End Sub
End Class
