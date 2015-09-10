Imports System
Imports System.Data
Imports System.Web.UI.HtmlControls
Imports System.Drawing
Imports System.Data.OleDb
Imports System.IO
Imports System.Xml
Imports system.data.DataView
Imports system.data.DataTable
Imports System.Data.Common
Imports IAS.Core.CSCode
Partial Class pj_hr_hr_script_mr_frontliner
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJPJ As New ModuleFunctionLocal

    Dim mlSQL As String
    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlDATAADAPTER As OleDb.OleDbDataAdapter
    Dim mlDATASET As New DataSet
    Dim mlDATATABLE As New DataTable

    Dim mlPATH As String
    Dim mlKEY As String
    Dim mlRECORDSTATUS As String
    Dim mlSPTYPE As String
    Dim mlFUNCTIONPARAMETER As String
    Dim mlID As String
    Dim mlENCRYPTCODE As String

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Script Sunfish MR Frontliner List V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Script Sunfish MR Frontliner List V2.00"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        TR1.Visible = False

        LoadURL()
        If Page.IsPostBack = False Then
            DisableCancel()
            LoadComboData()
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "hr_script_mr_frontliner", "hr_script_mr_frontliner", "")
        Else
        End If
    End Sub

    Sub LoadComboData()
        ddMENU.Items.Clear()
        ddMENU.Items.Add("Pilih")
        ddMENU.Items.Add("MR Frontliner")
    End Sub

    Sub LoadURL()
        Select Case DDMENU.TEXT
            Case "MR Frontliner"
                txURL.Text = "http://10.62.0.43/iss/a2a/?key=1e59b_c4f5798d_845cc9"
        End Select
    End Sub

    Protected Sub btSUBMIT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSUBMIT.Click
        RetrieveFieldsDetail()
    End Sub
    
    Sub RetrieveFieldsDetail()
        Try
            Dim mlDATASET As New DataSet

            mlPATH = txURL.Text
            If Trim(txURL.Text) = "" Then Exit Sub

            mlDATASET.ReadXml(mlPATH, XmlReadMode.Auto)
            mlDATAGRID.DataSource = Nothing
            mlDATAGRID.DataSource = mlDATASET
            mlDATAGRID.DataBind()
            EnableCancel()
            mlMESSAGE.Text = ""

        Catch ex As Exception
            Response.Write(Err.Description)
        End Try
    End Sub

    Protected Sub btSaveRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSaveRecord.Click
        If pnFILL.Visible = True Then
            SaveRecord()
        End If
    End Sub

    Sub SaveRecord()
        mlOBJGS.mgMESSAGE = ValidateForm()
        If mlOBJGF.IsNone(mlOBJGS.mgMESSAGE) = False Then
            mlMESSAGE.Text = mlOBJGS.mgMESSAGE
            Exit Sub
        End If

        mlSPTYPE = "New"
        If mlSYSCODE.Value = "edit" Then
            mlSPTYPE = "Edit"
        End If

        Try
            Sql_1(mlSPTYPE, mlKEY)
        Catch ex As Exception
        End Try


        mlSYSCODE.Value = ""
        ClearFields()
    End Sub

    Function ValidateForm() As String
        ValidateForm = ""
    End Function

    Sub ClearFields()
    End Sub

    Private Sub DisableCancel()
        btSaveRecord.Visible = False
    End Sub

    Private Sub EnableCancel()
        btSaveRecord.Visible = True
    End Sub


    Sub Sql_1(ByVal mpSTATUS As String, ByVal mpCODE As String)
        Select Case ddMENU.Text
            Case "MR Frontliner"
                Sql_FL(mpSTATUS, mpCODE)
        End Select
    End Sub

    Sub Sql_FL(ByVal mpSTATUS As String, ByVal mpCODE As String)
        Dim mlSTATUS As String
        Dim mlNEWRECORD As Boolean
        Dim mlPASSWORDSAVE As String
        Dim mlBLANK As String
        Dim mlDEFAULTMENU As String

        Dim mlMENUSTYLE As String
        Dim mlCOMPANY As String
        Dim mlSYSTEM As String
        Dim mlBIRTHDATE As String

        Dim mlDEPTID As String
        Dim mlUSERSTATUS As String
        Dim mlHP As String
        Dim mlEMAILADDR As String
        Dim mlUSERID As String
        Dim mlNAME As String
        Dim mlBRANCHID As String
        Dim mlCOMPANYID As String


        Try
            mlMESSAGE.Text = ""
            mlNEWRECORD = True
            mlBLANK = ""
            mlMENUSTYLE = mlOBJGS.FindSetup("AD_ADSETUP", "MenuStyleDefault", "AD", "AD")
            mlCOMPANY = mlOBJGS.FindSetup("AD_ADSETUP", "CompanyDefault", "AD", "AD")
            mlSYSTEM = mlOBJGS.FindSetup("AD_ADSETUP", "SystemDefault", "AD", "AD")
            mlENCRYPTCODE = System.Configuration.ConfigurationManager.AppSettings("mgENCRYPTCODE")

            Dim mlDG As DataGridItem
            For Each mlDG In mlDATAGRID.Items

                mlDEFAULTMENU = "FR"
                mlBRANCHID = Trim(Replace(mlDG.Cells("0").Text, "'", ""))
                mlDEPTID = Trim(Replace(mlDG.Cells("1").Text, "'", ""))
                mlEMAILADDR = Trim(Replace(mlDG.Cells("2").Text, "&nbsp;", ""))
                mlUSERSTATUS = Trim(mlDG.Cells("3").Text)
                mlHP = Trim(Replace(mlDG.Cells("4").Text, "&nbsp;", ""))
                mlNAME = Trim(Replace(mlDG.Cells("6").Text, "'", ""))
                mlKEY = mlDG.Cells("7").Text
                mlUSERID = Trim(mlDG.Cells("7").Text)
                mlBIRTHDATE = mlOBJGF.GetStringAtPosition(Trim(mlDG.Cells("9").Text), 0, "")
                mlPASSWORDSAVE = mlOBJGF.GetStringAtPosition(mlBIRTHDATE, 2, "-") & mlOBJGF.GetStringAtPosition(mlBIRTHDATE, 1, "-") & mlOBJGF.GetStringAtPosition(mlBIRTHDATE, 0, "-")
                mlPASSWORDSAVE = mlOBJGF.Encrypt(Trim(mlPASSWORDSAVE), mlENCRYPTCODE)
                mlCOMPANYID = ""

                mlSQL = mlSQL & mlOBJPJ.AD_UserProfile_ToLog(mlKEY, "New", Session("mgUSERID"))
                mlSQL = mlSQL & "IF EXISTS (SELECT * FROM AD_USERPROFILE WHERE UserID = '" & Trim(mlKEY) & "') "
                mlSQL = mlSQL & " UPDATE AD_USERPROFILE SET " & _
                    " Name='" & mlNAME & "'," & _
                    " BranchID='" & mlBRANCHID & "'," & _
                    " CompanyID='" & mlCOMPANYID & "'," & _
                    " DeptID='" & mlDEPTID & "'," & _
                    " UserStatus='" & mlUSERSTATUS & "'," & _
                    " TelHP = '" & mlHP & "'," & _
                    " EmailAddr = '" & mlEMAILADDR & "'," & _
                    " RecUserID='" & Session("mgUSERID") & "',RecDate='" & mlOBJGF.FormatDate(Now) & "'" & _
                    " WHERE UserID = '" & Trim(mlKEY) & "';"

                mlSQL = mlSQL & " ELSE "
                mlSQL = mlSQL & " INSERT INTO AD_USERPROFILE (UserID,Password,Name,UserLevel,GroupID,CompanyID,BranchID,DeptID,MenuStyle,LastCompany,LastSystem," & _
                    " UserStatus,TelHP,EmailAddr,EmployeeID,FingerPrintID,ApplicationID,RecordStatus,RecUserID,RecDate)" & _
                    " VALUES ('" & mlUSERID & "', '" & mlPASSWORDSAVE & "'," & _
                    " '" & mlNAME & "', 'User', '" & Trim(mlDEFAULTMENU) & "', " & _
                    " '" & mlCOMPANYID & "','" & mlBRANCHID & "'," & _
                    " '" & mlDEPTID & "'," & _
                    " '" & mlMENUSTYLE & "', '" & mlCOMPANY & "', '" & mlSYSTEM & "'," & _
                    " '" & mlUSERSTATUS & "','" & mlHP & "'," & _
                    " '" & mlEMAILADDR & "'," & _
                    " '" & mlUSERID & "'," & _
                    " '" & Trim(mlBLANK) & "','" & Trim(mlBLANK) & "'," & _
                    " 'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "');"
            Next mlDG


            If Trim(mlSQL) <> "" Then
                mlOBJGS.ExecuteQuery(mlSQL, "AD", "AD")
                mlMESSAGE.Text = "Database Update Successfull"
                DisableCancel()
            End If
            
        Catch ex As Exception
            mlMESSAGE.Text = "Database Update Fail"
            mlOBJGS.XMtoLog("HR", "HRScript", "hr_script_mr_frontliner" & mlKEY, Err.Description, "New", Session("mgUSERID"), mlOBJGF.FormatDate(Now))
        End Try
    End Sub


End Class
