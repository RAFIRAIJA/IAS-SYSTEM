Imports System
Imports System.Data
Imports System.Data.OleDb

Partial Class aktifkan_regional
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
        mlTITLE.Text = "Activate My Account from Sunfish System (Regional) V2.01"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Activate My Account from Sunfish System (Regional) V2.01"
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
                Else
                    mlMESSAGE.Text = "Data sudah ditemukan di dalam database"
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Sub LoadComboData()
        ddDEPT.Items.Clear()
        'ddDEPT.Items.Add("Opr_Bdg")
        ddDEPT.Items.Add("Opr_METRO")
        ddDEPT.Items.Add("Opr_BANDUNG")
        ddDEPT.Items.Add("Opr_BALI")
        ddDEPT.Items.Add("Opr_MAKASSAR")
        ddDEPT.Items.Add("Opr_SEMARANG")
        ddDEPT.Items.Add("Opr_SURABAYA")
        ddDEPT.Items.Add("Opr_PALEMBANG")
        ddDEPT.Items.Add("Opr_BATAM")
        ddDEPT.Items.Add("Opr_PEKANBARU")
        ddDEPT.Items.Add("Opr_MEDAN")
        ddDEPT.Items.Add("Opr_BALIKPAPAN")

        ddENTITY.Items.Clear()
        ddENTITY.Items.Add("ISS")
        mlSQLTEMP = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID='ISS_Entity'"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISS")
        While mlRSTEMP.Read
            ddENTITY.Items.Add(Trim(mlRSTEMP("LinCode")))
        End While
    End Sub

    Function ActivateMyAccount(ByVal mpNIK As String) As Boolean
        Dim mlLINK As String
        Dim mlPARAM_PC As String
        Dim mlPARAM_ID As String
        Dim mlPARAM_MENU As String

        Dim mlURLDEST As New System.Net.WebClient
        Dim mlURLLOCAL As String
        Dim mlURLTEST As String
        Dim mlURLADDR As String
        Dim mlSENDURL As String
        Dim mlPARAM_FP As String
        Dim mlURL_FAILKEY As String


        mlPARAM_FP = "Employee_ID"
        mlURLLOCAL = mlOBJGS.FindSetup("HR_HRSETUP", "IPLocal")
        mlURLTEST = mlOBJGS.FindSetup("HR_HRSETUP", "IPTEST")
        mlURL_FAILKEY = ""

        mlKEY = mpUSERID.Text
        Select Case ddDEPT.Text
            Case "Opr_BANDUNG"
                Select Case ddENTITY.Text
                    Case "ISS"
                        mlPARAM_MENU = "ISS_BDG_MR-Entry"
                    Case "IPM"
                        mlPARAM_MENU = "IPM_BDG_MR-Entry"
                    Case "ICS"
                        mlPARAM_MENU = "ICS_BDG_MR-Entry"
                    Case "IFS"
                        mlPARAM_MENU = "IFS_BDG_MR-Entry"
                End Select

            Case "Opr_BALI"
                Select Case ddENTITY.Text
                    Case "ISS"
                        mlPARAM_MENU = "ISS_BALI_MR"
                    Case "IPM"
                        mlPARAM_MENU = "IPM_BALI_MR"
                    Case "ICS"
                        mlPARAM_MENU = "ICS_BALI_MR"
                    Case "IFS"
                        mlPARAM_MENU = "IFS_BALI_MR"
                End Select

            Case "Opr_MAKASSAR"
                Select Case ddENTITY.Text
                    Case "ISS"
                        mlPARAM_MENU = "ISS_MAKASSAR_MR"
                    Case "IPM"
                        mlPARAM_MENU = "IPM_MAKASSAR_MR"
                    Case "ICS"
                        mlPARAM_MENU = "ICS_MAKASSAR_MR"
                    Case "IFS"
                        mlPARAM_MENU = "IFS_MAKASSAR_MR"
                End Select

            Case "Opr_SEMARANG"
                Select Case ddENTITY.Text
                    Case "ISS"
                        mlPARAM_MENU = "ISS_SEMARANG_MR"
                    Case "IPM"
                        mlPARAM_MENU = "IPM_SEMARANG_MR"
                    Case "ICS"
                        mlPARAM_MENU = "ICS_SEMARANG_MR"
                    Case "IFS"
                        mlPARAM_MENU = "IFS_SEMARANG_MR"
                End Select

            Case "Opr_SURABAYA"
                Select Case ddENTITY.Text
                    Case "ISS"
                        mlPARAM_MENU = "ISS_SURABAYA_MR"
                    Case "IPM"
                        mlPARAM_MENU = "IPM_SURABAYA_MR"
                    Case "ICS"
                        mlPARAM_MENU = "ICS_SURABAYA_MR"
                    Case "IFS"
                        mlPARAM_MENU = "IFS_SURABAYA_MR"
                End Select

            Case "Opr_PALEMBANG"
                Select Case ddENTITY.Text
                    Case "ISS"
                        mlPARAM_MENU = "ISS_PALEMBANG_MR"
                    Case "IPM"
                        mlPARAM_MENU = "IPM_PALEMBANG_MR"
                    Case "ICS"
                        mlPARAM_MENU = "ICS_PALEMBANG_MR"
                    Case "IFS"
                        mlPARAM_MENU = "IFS_PALEMBANG_MR"
                End Select

            Case "Opr_BATAM"
                Select Case ddENTITY.Text
                    Case "ISS"
                        mlPARAM_MENU = "ISS_BATAM_MR"
                    Case "IPM"
                        mlPARAM_MENU = "IPM_BATAM_MR"
                    Case "ICS"
                        mlPARAM_MENU = "ICS_BATAM_MR"
                    Case "IFS"
                        mlPARAM_MENU = "IFS_BATAM_MR"
                End Select

            Case "Opr_PEKANBARU"
                Select Case ddENTITY.Text
                    Case "ISS"
                        mlPARAM_MENU = "ISS_PEKANBARU_MR"
                    Case "IPM"
                        mlPARAM_MENU = "IPM_PEKANBARU_MR"
                    Case "ICS"
                        mlPARAM_MENU = "ICS_PEKANBARU_MR"
                    Case "IFS"
                        mlPARAM_MENU = "IFS_PEKANBARU_MR"
                End Select

            Case "Opr_MEDAN"
                Select Case ddENTITY.Text
                    Case "ISS"
                        mlPARAM_MENU = "ISS_MEDAN_MR"
                    Case "IPM"
                        mlPARAM_MENU = "IPM_MEDAN_MR"
                    Case "ICS"
                        mlPARAM_MENU = "ICS_MEDAN_MR"
                    Case "IFS"
                        mlPARAM_MENU = "IFS_MEDAN_MR"
                End Select

            Case "Opr_BALIKPAPAN"
                Select Case ddENTITY.Text
                    Case "ISS"
                        mlPARAM_MENU = "ISS_BALIKPAPAN_MR"
                    Case "IPM"
                        mlPARAM_MENU = "IPM_BALIKPAPAN_MR"
                    Case "ICS"
                        mlPARAM_MENU = "ICS_BALIKPAPAN_MR"
                    Case "IFS"
                        mlPARAM_MENU = "IFS_BALIKPAPAN_MR"
                End Select

            Case "Opr_METRO"
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

        mlFUNCTIONPARAMETER = mlPARAM_FP
        mlPARAM_PC = "OP1"

        Try
            mlURLADDR = "http://" & mlURLLOCAL & "/iss/pj_hr/hr_script_hr_nik_pass.aspx?mpFP=" & mlPARAM_FP & "&mpID=" & mlKEY & "&mpMN=" & mlPARAM_MENU & "&mpPC=" & mlPARAM_PC
            mlSENDURL = mlURLDEST.DownloadString(mlURLADDR)
            mlURLADDR = "http://" & mlURLTEST & "/isstest/pj_hr/hr_script_hr_nik_pass.aspx?mpFP=" & mlPARAM_FP & "&mpID=" & mlKEY & "&mpMN=" & mlPARAM_MENU & "&mpPC=" & mlPARAM_PC
            mlSENDURL = mlURLDEST.DownloadString(mlURLADDR)

            mlSQL = "SELECT UserID FROM AD_USERPROFILE WHERE UserID = '" & mpUSERID.Text & "'"
            mlREADER = mlOBJGS.DbRecordset(mlSQL, "AD", "AD")
            If mlREADER.HasRows Then
                mlMESSAGE.Text = "Data " & Trim(mpUSERID.Text) & " berhasil ditambahkan, silahkan login dengan userid adalah NIK dan password adalah 2 digit tanggal lahir + 2 digit bulan lahir + 4 digit tahun lahir anda "
                mlURLADDR = "http://" & mlURLLOCAL & "/iss/pj_ut/ut_sendemail_registeruser.aspx?mpID=" & mlKEY
                mlSENDURL = mlURLDEST.DownloadString(mlURLADDR)
            Else
                mlMESSAGE.Text = "Data " & Trim(mpUSERID.Text) & " gagal ditambahkan "
            End If

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
