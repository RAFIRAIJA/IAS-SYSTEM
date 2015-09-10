Imports System
Imports System.Data
Imports System.Data.OleDb

Partial Class utility_ut_forgotpswd
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJPJ As New FunctionLocal

    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlSQLTEMP As String
    Dim mlRSTEMP As OleDbDataReader
    Dim mlFUNCTIONPARAMETER As String
    Dim mlFUNCTIONID As String
    Dim mlKEY As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.Redirect("aktifkan_regional.aspx")
        Exit Sub


        '''' untuk menu aktifkan, langsung digabung dengan menu Aktifkan Regional
        mlTITLE.Text = "Activate My Account from Sunfish System V2.01"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Activate My Account from Sunfish System V2.01"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        Try
            mlFUNCTIONPARAMETER = Request("mpFP")
            mlFUNCTIONID = mlFUNCTIONPARAMETER

            If Page.IsPostBack = False Then
                LoadComboData()
                mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "aktifkansunfish", "aktifkansunfish", "")
            End If

        Catch ex As Exception
        End Try
    End Sub

    Sub ClearFields()
    End Sub

    Function ValidateForm() As String
        ValidateForm = ""
    End Function

    Protected Sub mpBTSUBMIT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mpBTSUBMIT.Click
        Dim mlPARAM As String

        Try

            mlSQL = "SELECT UserID FROM AD_USERPROFILE WHERE UserID = '" & mpUSERID.Text & "'"
            mlREADER = mlOBJGS.DbRecordset(mlSQL, "AD", "AD")
            If mlREADER.HasRows = False Then
                ActivateMyAccount(Trim(mpUSERID.Text))
            Else
                mlREADER.Read()
                If IsDBNull(mlREADER("UserID")) = True Then
                    ActivateMyAccount(Trim(mpUSERID.Text))
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Sub LoadComboData()
        ddDEPT.Items.Clear()
        ddDEPT.Items.Add("Lainnya")
        ddDEPT.Items.Add("Opr")
        ''ddDEPT.Items.Add("Contract")
    End Sub

    Function ActivateMyAccount(ByVal mpNIK As String) As Boolean
        Dim mlLINK As String
        Dim mlPARAM_PC As String
        Dim mlPARAM_ID As String
        Dim mlPARAM_MENU As String

        Dim mlURLDEST As New System.Net.WebClient
        Dim mlURLLOCAL As String
        Dim mlURLADDR As String
        Dim mlSENDURL As String
        Dim mlPARAM_FP As String
        Dim mlURL_FAILKEY As String


        mlPARAM_FP = "Employee_ID"
        mlURLLOCAL = mlOBJGS.FindSetup("HR_HRSETUP", "IPLocal")
        mlURL_FAILKEY = ""

        mlKEY = mpUSERID.Text
        Select Case ddDEPT.Text
            Case "Lainnya"
                mlFUNCTIONPARAMETER = mlPARAM_FP
                mlPARAM_PC = "0"
                mlPARAM_MENU = "ALL_INT"

            Case "Contract"
                mlFUNCTIONPARAMETER = mlPARAM_FP
                mlPARAM_PC = "0"
                mlPARAM_MENU = "CTA"

            Case "Opr"
                mlSQLTEMP = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID='MR_GROUPMENULEVEL'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP)
                While mlRSTEMP.Read
                    Select Case Trim(mlRSTEMP("LinCode"))
                        Case "1"
                            mlPARAM_MENU = Trim(mlRSTEMP("Description"))
                    End Select
                End While

                mlFUNCTIONPARAMETER = mlPARAM_FP
                mlPARAM_PC = "OP1"
        End Select

        Try
            mlURLADDR = "http://" & mlURLLOCAL & "/iss/pj_hr/hr_script_hr_nik_pass.aspx?mpFP=" & mlPARAM_FP & "&mpID=" & mlKEY & "&mpMN=" & mlPARAM_MENU & "&mpPC=" & mlPARAM_PC
            mlSENDURL = mlURLDEST.DownloadString(mlURLADDR)
            mlMESSAGE.Text = "Data " & Trim(mpUSERID.Text) & " sudah ditambahkan, silahkan login dengan userid adalah NIK dan password adalah 2 digit tanggal lahir + 2 digit bulan lahir + 4 digit tahun lahir anda "

            mlURLADDR = "http://" & mlURLLOCAL & "/iss/pj_ut/ut_sendemail_registeruser.aspx?mpID=" & mlKEY
            mlSENDURL = mlURLDEST.DownloadString(mlURLADDR)

        Catch ex As Exception
            mlMESSAGE.Text = "Data sudah ditemukan di dalam database"
            mlURL_FAILKEY = mlURL_FAILKEY & "<br>" & mlKEY
        End Try

        Select Case ddDEPT.Text
            Case "Opr"
                mlMESSAGE.Text = mlMESSAGE.Text & "<br> setelah login, silahkan cek terlebih dahulu apakah anda mempunyai akses ke site card"
        End Select

    End Function
End Class
