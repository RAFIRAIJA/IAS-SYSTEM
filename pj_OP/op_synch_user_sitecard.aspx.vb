Imports System
Imports System.Data
Imports System.Web.UI.HtmlControls
Imports System.Data.OleDb
Imports IAS.Core.CSCode

Partial Class op_synch_user_sitecard
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJPJ As New ModuleFunctionLocal

    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlREADER2 As OleDb.OleDbDataReader
    Dim mlSQL2 As String
    Dim mlKEY As String
    Dim mlKEY2 As String
    Dim mlRECORDSTATUS As String
    Dim mlSPTYPE As String
    Dim mlFUNCTIONPARAMETER_ORI As String
    Dim mlFUNCTIONPARAMETER As String
    Dim mlPARAM_COMPANY As String
    Dim mlPARAM_COMPANY_VALUE As String

    Dim mlSQLTEMP As String
    Dim mlRSTEMP As OleDb.OleDbDataReader
    Dim mlCURRENTDATE As String = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()
    Dim mlI As Integer
    Dim mlLINKEDSERVER1 As String
    Dim mlSQL_UPDATEINTERVAL As Integer

    Dim mlCOMPANYTABLENAME As String
    Dim mlCOMPANYID As String

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Synchronize Nav User Site Card V2.10"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Synchronize Nav User Site Card V2.10"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlLINKEDSERVER1 = System.Configuration.ConfigurationManager.AppSettings("mgLINKEDSERVER1")
        mlFUNCTIONPARAMETER = Request("mpFP")
        If mlOBJGF.IsNone(mlFUNCTIONPARAMETER) = True Then mlFUNCTIONPARAMETER = "1"
        mlFUNCTIONPARAMETER_ORI = mlFUNCTIONPARAMETER
        mlFUNCTIONPARAMETER = "USC" & mlFUNCTIONPARAMETER

        mlPARAM_COMPANY = Trim(Request("mpFP"))
        Select Case mlPARAM_COMPANY
            Case "", "1"
                ddENTITY.Items.Clear()
                ddENTITY.Text = "ISS"
                ddENTITY.Items.Add("ISS")
                mlTITLE.Text = mlTITLE.Text & " (ISS)"
            Case "2"
                ddENTITY.Items.Clear()
                ddENTITY.Text = "IFS"
                ddENTITY.Items.Add("IFS")
                mlTITLE.Text = mlTITLE.Text & " (IFS)"
            Case "3"
                ddENTITY.Items.Clear()
                ddENTITY.Text = "IPM"
                ddENTITY.Items.Add("IPM")
                mlTITLE.Text = mlTITLE.Text & " (IPM)"
            Case "4"
                ddENTITY.Items.Clear()
                ddENTITY.Text = "ICS"
                ddENTITY.Items.Add("ICS")
                mlTITLE.Text = mlTITLE.Text & " (ICS)"
        End Select

        mlCOMPANYTABLENAME = "ISS Servisystem, PT$"
        mlCOMPANYID = mlCOMPANYID
        Select Case ddENTITY.Text
            Case "ISS"
                mlCOMPANYTABLENAME = "ISS Servisystem, PT$"
                mlCOMPANYID = "ISSN3"
            Case "IPM"
                mlCOMPANYTABLENAME = "ISS Parking Management$"
                mlCOMPANYID = "IPM3"
            Case "ICS"
                mlCOMPANYTABLENAME = "ISS CATERING SERVICES$"
                mlCOMPANYTABLENAME = "ISS Catering Service 5SP1$"
                mlCOMPANYID = "ICS5"
            Case "IFS"
                mlCOMPANYTABLENAME = "INTEGRATED FACILITY SERVICES$"
                mlCOMPANYID = "IFS3"
        End Select

        If Page.IsPostBack = False Then
            pnSEARCHSITECARD.Visible = "false"
            DisableCancel()
            RetrieveFieldsDetail("")
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "op_user_sitecard", "User SiteCard", "")
        Else
        End If
    End Sub

    Protected Sub mlDATAGRID_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRID.ItemCommand
        mlKEY = e.CommandArgument
        Select Case e.CommandName
            Case "BrowseRecord"
                mlMESSAGE.Text = "Update Request for " & e.CommandArgument
                RetrieveFields()
                pnFILL.Visible = True
            Case "EditRecord"
                mlMESSAGE.Text = "Edit Request for  " & e.CommandArgument
                mlSYSCODE.Value = "edit"
                EditRecord()
            Case "DeleteRecord"
                mlMESSAGE.Text = "Delete Request for  " & e.CommandArgument
                mlSYSCODE.Value = "delete"
                DeleteRecord()
            Case Else
                ' Ignore Other
        End Select
    End Sub

    Protected Sub btNewRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btNewRecord.Click
        NewRecord()
    End Sub

    Protected Sub btSaveRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSaveRecord.Click
        ClearFields()
        If pnFILL.Visible = True Then
            SaveRecord("", "")
        End If
    End Sub

    Protected Sub btSYNCHRONIZE_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSYNCHRONIZE.Click
        ClearFields()
        If pnFILL.Visible = True Then
            mlSQL_UPDATEINTERVAL = 100
            SaveRecord("", "")
        End If
    End Sub

    Protected Sub btSYNCHRONIZE2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSYNCHRONIZE2.Click
        ClearFields()
        If pnFILL.Visible = True Then
            mlSQL_UPDATEINTERVAL = 1
            SaveRecord("", "")
        End If
    End Sub

    Protected Sub btSYNCHRONIZE3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSYNCHRONIZE3.Click
        ClearFields()
        If pnFILL.Visible = True Then
            mlSQL_UPDATEINTERVAL = 1
            SaveRecord("1", txSITECARD.Text)
        End If
    End Sub


    Protected Sub btUSERIDVIEW_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btUSERIDVIEW.Click
        ClearFields()
        Try
            mlSQL = "SELECT Distinct UserID, UserName FROM OP_USER_SITE WHERE PARENTCODE='" & mlFUNCTIONPARAMETER & "' AND UserID Not IN" & _
                " (SELECT USERID FROM " & mlLINKEDSERVER1 & "AD_USERPROFILE) ORDER BY UserID"
            RetrieveFieldsDetail(mlSQL)

        Catch ex As Exception
            mlMESSAGE.Text = Err.Description
        End Try

    End Sub

    Protected Sub btERRMRLEVEL_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btERRMRLEVEL.Click
        ClearFields()
        Try
            mlSQL = "SELECT DISTINCT SITECARDID,SiteCardName  FROM OP_USER_SITE WHERE PARENTCODE='" & mlFUNCTIONPARAMETER & "' AND Userlevel<>'3' AND SITECARDID NOT IN" & _
                " (SELECT SITECARDID FROM OP_USER_SITE  WHERE Userlevel='3')"
            RetrieveFieldsDetail(mlSQL)

        Catch ex As Exception
            mlMESSAGE.Text = Err.Description
        End Try

    End Sub

    Protected Sub btERRSITECARD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btERRSITECARD.Click
        Dim mlSITECARD As String
        ClearFields()
        Try
            mlSITECARD = ""

            mlSQL = "SELECT DISTINCT SITECARDID FROM OP_USER_SITE WHERE PARENTCODE='" & mlFUNCTIONPARAMETER & "'"
            mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3")
            While mlREADER.Read
                mlSITECARD = mlSITECARD & IIf(mlOBJGF.IsNone(mlSITECARD) = True, "", ",") & "'" & mlREADER("SiteCardID") & "'"
            End While

            mlSYSCODE2.Value = mlSITECARD

            mlSQL = "SELECT LineNo_ FROM [" & mlCOMPANYTABLENAME & "CustServiceSite] WHERE Status='0' AND LineNo_ NOT IN (" & mlSITECARD & ")"
            mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", mlCOMPANYID)
            mlDATAGRID.DataSource = mlREADER
            mlDATAGRID.DataBind()


        Catch ex As Exception
            mlMESSAGE.Text = Err.Description
        End Try

    End Sub

    Protected Sub btUSERIDADD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btUSERIDADD.Click
        Sql_AddUser()
    End Sub

    Protected Sub btBRANCHADD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btBRANCHADD.Click
        Sql_UpdateBranch()
    End Sub

    Protected Sub btCancelOperation_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btCancelOperation.Click
        DisableCancel()
    End Sub

    Sub SearchRecord()
        Dim mlSQL As String
        Dim mlFUNCTIONPARAMETER2 As String

        If pnFILL.Visible = False Then
            ClearFields()
            EnableCancel()
            pnFILL.Visible = True
            Exit Sub
        End If

        Try
            mlSQL = ""

            mlFUNCTIONPARAMETER2 = mlFUNCTIONPARAMETER
            mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " ParentCode = '" & mlFUNCTIONPARAMETER2 & "'"

            If mlOBJGF.IsNone(mlSQL) = False Then
                Try
                    mlSQL2 = "SELECT DocNo,UserID,SiteCardID as SiteCard,SiteCardName as SiteName, UserLevel  as MRLevel FROM OP_USER_SITE" & _
                            " WHERE " & mlSQL & " ORDER BY DocNo"
                    'RetrieveFieldsDetail(mlSQL)
                Catch ex As Exception
                End Try
            End If


        Catch ex As Exception
        End Try
    End Sub

    Public Sub RetrieveFields()
        DisableCancel()
    End Sub

    Sub RetrieveFieldsDetail(ByVal mpSQL As String)
        If mpSQL = "" Then
            mlSQL2 = "SELECT DocNo,DocDate,ItemKey as Item,Description, RecUserID as UserID FROM UT_SYNCHRONIZE " & _
                " WHERE RecordStatus='New' AND ParentCode = '" & Trim(mlFUNCTIONPARAMETER) & "' AND CompanyID='" & Trim(ddENTITY.Text) & "' ORDER BY DocNo"
        Else
            mlSQL2 = mpSQL
        End If
        mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISSP3")
        mlDATAGRID.DataSource = mlREADER2
        mlDATAGRID.DataBind()

        If mlREADER2.HasRows = False Then
            mlMESSAGE.Text = "no data"
        End If
    End Sub


    Sub DeleteRecord()
        mlSPTYPE = "Delete"
        Try
            Sql_1(mlSPTYPE, mlKEY, "")
        Catch ex As Exception
        End Try

        mlSYSCODE.Value = ""
        RetrieveFields()
        RetrieveFieldsDetail("")
    End Sub

    Sub NewRecord()
        mlOBJGS.mgNEW = True
        mlOBJGS.mgEDIT = False
        EnableCancel()
        ClearFields()
        LoadComboData()

        mlMESSAGE.Text = "System Ready"
        lbRESULT.Text = ""
    End Sub

    Sub EditRecord()
        mlOBJGS.mgNEW = False
        mlOBJGS.mgEDIT = True
        ClearFields()
        LoadComboData()
        RetrieveFields()
        EnableCancel()
    End Sub


    Private Sub EnableCancel()
        btNewRecord.Visible = False
        btSaveRecord.Visible = True
        pnFILL.Visible = True
    End Sub

    Private Sub DisableCancel()
        btNewRecord.Visible = True
        btSaveRecord.Visible = False
        pnFILL.Visible = False
    End Sub

    Sub ClearFields()
        lbRESULT.Text = ""
        mlMESSAGE.Text = ""
    End Sub

    ''
    Protected Sub btSEARCHSITECARD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHSITECARD.Click
        If pnSEARCHSITECARD.Visible = False Then
            pnSEARCHSITECARD.Visible = True
        Else
            pnSEARCHSITECARD.Visible = False
        End If
    End Sub

    Protected Sub btSEARCHSITECARDSUBMIT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHSITECARDSUBMIT.Click
        If mlOBJGF.IsNone(mlSEARCHSITECARD.Text) = False Then SearchSiteCard(1, mlSEARCHSITECARD.Text)
    End Sub

    Protected Sub mlDATAGRIDSITECARD_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRIDSITECARD.ItemCommand
        Try
            txSITECARD.Text = CType(e.Item.Cells(0).Controls(0), LinkButton).Text
            lbSITEDESC.Text = CType(e.Item.Cells(1).Controls(0), LinkButton).Text
            mlDATAGRIDSITECARD.DataSource = Nothing
            mlDATAGRIDSITECARD.DataBind()
            pnSEARCHSITECARD.Visible = False


        Catch ex As Exception
        End Try
    End Sub

    Sub SearchSiteCard(ByVal mpTYPE As Byte, ByVal mpNAME As String)
        Select Case mpTYPE
            Case "1"
                'mlSQLTEMP = "SELECT LineNo_ as Field_ID,SearchName as Field_Name FROM [" & mlCOMPANYTABLENAME & "CustServiceSite] WHERE SearchName LIKE  '%" & mlSEARCHSITECARD.Text & "%' AND CustomerNo_= '" & txCUST.Text & "'"
                'mlSQLTEMP = "SELECT LineNo_ as Field_ID,SearchName as Field_Name FROM [" & mlCOMPANYTABLENAME & "CustServiceSite] WHERE SearchName LIKE  '%" & mlSEARCHSITECARD.Text & "%'"
                mlSQLTEMP = "SELECT LineNo_ as Field_ID,SearchName as Field_Name, Branch,CustomerNo_ as CustID FROM [" & mlCOMPANYTABLENAME & "CustServiceSite] WHERE SearchName LIKE  '%" & mlSEARCHSITECARD.Text & "%'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", mlCOMPANYID)
                mlDATAGRIDSITECARD.DataSource = mlRSTEMP
                mlDATAGRIDSITECARD.DataBind()
        End Select
    End Sub

    Protected Sub btSITECARD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSITECARD.Click
        lbSITEDESC.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_DESC", Trim(txSITECARD.Text), mlCOMPANYID)
    End Sub



    Sub LoadComboData()
        'ddENTITY.Items.Clear()
        'ddENTITY.Items.Add("Pilih")
        'mlSQLTEMP = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID='ISS_Entity'"
        'mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISS")
        'While mlRSTEMP.Read
        '    ddENTITY.Items.Add(Trim(mlRSTEMP("LinCode")))
        'End While
    End Sub

    Sub SaveRecord(ByVal mpTASKCODE As String, ByVal mpPARAM1 As String)
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
            Sql_1(mlSPTYPE, mpTASKCODE, mpPARAM1)
        Catch ex As Exception
        End Try

        mlSYSCODE.Value = ""
        ClearFields()
        DisableCancel()
        RetrieveFieldsDetail("")
    End Sub

    Function ValidateForm() As String
        ValidateForm = ""
    End Function


    Function Sql_InsertSiteCard(ByVal mpUSERID As String, ByVal mpUSERNAME As String, ByVal mpSITECARD As String, ByVal mpSITECARDDESC As String, ByVal mpUSERLEVEL As String, ByVal mpCOMPANYID As String, ByVal mpBRANCHID As String) As String
        Sql_InsertSiteCard = ""
        Sql_InsertSiteCard = Sql_InsertSiteCard & " INSERT INTO OP_USER_SITE (ParentCode,SysID,DocNo,DocDate," & _
            " UserID,UserName,Linno,SiteCardID,SiteCardName,UserLevel,EntityID,BranchID,RecordStatus,RecUserID,RecDate) VALUES ( " & _
            " '" & mlFUNCTIONPARAMETER & "','MR', '" & mlKEY & "','" & mlOBJGF.FormatDate(Now) & "'," & _
            " '" & Trim(mpUSERID) & "','" & Trim(Replace(mpUSERNAME, "'", "")) & "','" & mlI & "', '" & Trim(mpSITECARD) & "','" & Trim(Replace(mpSITECARDDESC, "'", "")) & "'," & _
            " '" & Trim(mpUSERLEVEL) & "','" & Trim(mpCOMPANYID) & "','" & Trim(mpBRANCHID) & "'," & _
            " 'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "');"
    End Function


    Sub Sql_AddUser()
        Dim mlNEWPWDN As String
        Dim mlBLANK As String
        Dim mlDEFAULTMENU As String
        Dim mlDEFAULTUSERSTATUS As String
        Dim mlMENUSTYLE As String
        Dim mlCOMPANY As String
        Dim mlSYSTEM As String
        Dim mlENCRYPTCODE As String
        Dim mlSYSPARAM As String
        Dim mlSYNCHITEM As String
        Dim mlSYNCHITEMQ As Integer

        Dim mlDEFAULTMENU_0 As String
        Dim mlDEFAULTMENU_1 As String
        Dim mlDEFAULTMENU_2 As String
        Dim mlDEFAULTMENU_3 As String

        Dim mlSTARTTIME As Date
        Dim mlENDTIME As Date

        Dim mlURLDEST As New System.Net.WebClient
        Dim mlURLLOCAL As String
        Dim mlURLTEST As String
        Dim mlURLADDR As String
        Dim mlSENDURL As String
        Dim mlURL_FAILKEY As String
        Dim mlPARAM_FP As String


        mlPARAM_FP = "Employee_ID"
        mlURLLOCAL = mlOBJGS.FindSetup("HR_HRSETUP", "IPLocal")
        mlURL_FAILKEY = ""
        mlSENDURL = ""

        mlSTARTTIME = Now
        mlDEFAULTMENU_0 = ""
        mlDEFAULTMENU_1 = ""
        mlDEFAULTMENU_2 = ""
        mlDEFAULTMENU_3 = ""

        Try
            mlSQLTEMP = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID='MR_GROUPMENULEVEL'"
            mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP)
            While mlRSTEMP.Read
                Select Case Trim(mlRSTEMP("LinCode"))
                    Case "0"
                        mlDEFAULTMENU_0 = Trim(mlRSTEMP("Description"))
                    Case "1"
                        mlDEFAULTMENU_1 = Trim(mlRSTEMP("Description"))
                    Case "2"
                        mlDEFAULTMENU_2 = Trim(mlRSTEMP("Description"))
                    Case "3"
                        mlDEFAULTMENU_3 = Trim(mlRSTEMP("Description"))
                End Select
            End While


            mlMESSAGE.Text = ""
            mlSYNCHITEM = ""
            mlSYNCHITEMQ = 0

            mlSYSPARAM = "OP1"
            mlBLANK = ""

            mlDEFAULTUSERSTATUS = "Active"
            mlMENUSTYLE = mlOBJGS.FindSetup("AD_ADSETUP", "MenuStyleDefault", "AD", "AD")
            mlCOMPANY = mlOBJGS.FindSetup("AD_ADSETUP", "CompanyDefault", "AD", "AD")
            mlSYSTEM = mlOBJGS.FindSetup("AD_ADSETUP", "SystemDefault", "AD", "AD")
            mlENCRYPTCODE = System.Configuration.ConfigurationManager.AppSettings("mgENCRYPTCODE")

            mlSQLTEMP = "SELECT Distinct UserID, UserName, Max(UserLevel)  AS  UserLevel FROM  OP_USER_SITE WHERE UserID Not IN" & _
                " (SELECT USERID FROM " & mlLINKEDSERVER1 & "AD_USERPROFILE) " & _
                " GROUP BY UserID, UserName "
            mlREADER2 = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
            While mlREADER2.Read
                'mlNEWPWDN = mlOBJGF.GetRandomPasswordUsingGUID(6)
                mlKEY = Trim(mlREADER2("UserID"))
                mlNEWPWDN = mlKEY
                mlSYNCHITEMQ = mlSYNCHITEMQ + 1
                mlSYNCHITEM = mlSYNCHITEM & "<br>" & mlSYNCHITEMQ & "# " & Trim(mlREADER2("UserID")) & " - " & Trim(mlREADER2("UserName")) & " - " & mlNEWPWDN
                mlNEWPWDN = mlOBJGF.Encrypt(mlNEWPWDN, mlENCRYPTCODE)

                mlDEFAULTMENU = ""
                Select Case Trim(mlREADER2("UserLevel"))
                    Case "1"
                        mlDEFAULTMENU = mlDEFAULTMENU_1
                    Case "2"
                        mlDEFAULTMENU = mlDEFAULTMENU_2
                    Case "3"
                        mlDEFAULTMENU = mlDEFAULTMENU_3
                End Select

                Try


                    'mlURLADDR = "http://" & mlURLLOCAL & "/iss/pj_hr/hr_script_hr_nik_pass.aspx?mpFP=" & mlPARAM_FP & "&mpID=" & mlKEY & "&mpMN=" & mlDEFAULTMENU & "&mpPC=" & mlSYSPARAM
                    'mlSENDURL = mlURLDEST.DownloadString(mlURLADDR)

                    'mlURLTEST = mlOBJGS.FindSetup("HR_HRSETUP", "IPTEST")
                    'mlURLADDR = "http://" & mlURLTEST & "/isstest/pj_hr/hr_script_hr_nik_pass.aspx?mpFP=" & mlPARAM_FP & "&mpID=" & mlKEY & "&mpMN=" & mlPARAM_MENU & "&mpPC=" & mlPARAM_PC
                    'mlSENDURL = mlURLDEST.DownloadString(mlURLADDR)

                Catch ex As Exception
                    mlURL_FAILKEY = mlSENDURL & "<br>" & mlKEY
                End Try


                'mlSQL = mlSQL & " INSERT INTO AD_USERPROFILE (ParentCode,UserID,Password,Name,UserLevel,GroupID,DeptID,MenuStyle,LastCompany,LastSystem," & _
                '   " UserStatus,TelHP,EmailAddr,EmployeeID,FingerPrintID,ApplicationID,RecordStatus,RecUserID,RecDate)" & _
                '   " VALUES ('" & mlSYSPARAM & "','" & mlKEY & "', '" & mlNEWPWDN & "'," & _
                '   " '" & Trim(mlREADER2("UserName")) & "', 'User', '" & Trim(mlDEFAULTMENU) & "', " & _
                '   " '" & Trim(mlBLANK) & "'," & _
                '   " '" & mlMENUSTYLE & "', '" & mlCOMPANY & "', '" & mlSYSTEM & "'," & _
                '   " '" & Trim(mlDEFAULTUSERSTATUS) & "','" & Trim(mlBLANK) & "'," & _
                '   " '" & Trim(mlBLANK) & "'," & _
                '   " '" & Trim(mlKEY) & "'," & _
                '   " '" & Trim(mlBLANK) & "','" & Trim(mlBLANK) & "'," & _
                '   " 'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "');"
                'mlSQL = mlSQL & mlOBJPJ.AD_UserProfile_ToLog(mlKEY, "New", Session("mgUSERID"))

            End While
            mlOBJGS.CloseFile(mlREADER2)

            If mlOBJGF.IsNone(mlSQL) = False Then
                mlOBJGS.ExecuteQuery(mlSQL, "AD", "AD")
                mlSQL = ""
            End If

            lbRESULT.Text = lbRESULT.Text & "Data Not Found for " & mlURL_FAILKEY
            lbRESULT.Text = lbRESULT.Text & " <br><br>" & mlSYNCHITEMQ & " Data Update <br> " & mlSYNCHITEM
            mlENDTIME = Now
            mlMESSAGE.Text = "User has been added Successfully, Start on :  " & mlSTARTTIME & " End of : " & mlENDTIME
        Catch ex As Exception
            mlMESSAGE.Text = "Synchronize Not Complete" & "<br>" & Err.Description
            mlOBJGS.XMtoLog("MR", "UserSiteCard", "UserSiteCard" & mlKEY, Err.Description, "New", Session("mgUSERID"), mlOBJGF.FormatDate(Now))
        End Try
    End Sub

    Sub Sql_UpdateBranch()
        Dim mlSTARTTIME As Date
        Dim mlENDTIME As Date
        Dim mlCOUNT As Integer

        Dim mlSITECARDID As String
        Dim mlSITECARDDESC As String
        Dim mlBRANCH As String
        Dim mlSQLADDBRANCH As String


        mlSTARTTIME = Now
        mlMESSAGE.Text = ""
        mlCOUNT = 0

        mlSQLADDBRANCH = ""
        mlSQLTEMP = "SELECT LineNo_,SearchName,Scat,SiteSupv_,OM,[SeniorOM],[Team Leader],[zone],Branch FROM [" & mlCOMPANYTABLENAME & "CustServiceSite] WHERE Status='0' "
        mlREADER = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", mlCOMPANYID)
        While mlREADER.Read
            mlSITECARDID = Trim(mlREADER("LineNo_")) & ""
            mlSITECARDDESC = Trim(mlREADER("SearchName")) & ""
            mlBRANCH = Trim(mlREADER("Branch")) & ""

            mlSQLADDBRANCH = mlSQLADDBRANCH & " UPDATE OP_USER_SITE SET BranchID= '" & mlBRANCH & "' WHERE SiteCardID ='" & mlSITECARDID & "';"

            mlCOUNT = mlCOUNT + 1
            If mlCOUNT >= 500 Then
                mlOBJGS.ExecuteQuery(mlSQLADDBRANCH, "PB", "ISSP3")
                mlSQLADDBRANCH = ""
            End If
        End While

        If mlSQLADDBRANCH <> "" Then
            mlOBJGS.ExecuteQuery(mlSQLADDBRANCH, "PB", "ISSP3")
            mlSQLADDBRANCH = ""
        End If

        mlENDTIME = Now
        mlMESSAGE.Text = "Update branches Successfull, Start on :  " & mlSTARTTIME & " End of : " & mlENDTIME

    End Sub



    Sub Sql_1(ByVal mpSTATUS As String, ByVal mpTASKCODE As String, ByVal mpPARAM1 As String)
        Dim mlSTATUS As String
        Dim mlSQL_SITECARD As String
        Dim mlSQL_SITECARD2 As String
        Dim mlNEWRECORD As Boolean
        Dim mlUSERLEVEL2 As String

        Dim mlPASS_STRING As String
        Dim mlPASS_STRING2 As String
        Dim mlUSER As String
        Dim mlUSERID As String
        Dim mlUSERNAME As String
        Dim mlLOOPTIMES As Byte

        Dim mlK As Byte
        Dim mlSYNCHITEMQ As Integer
        Dim mlSYNCHITEM As String
        Dim mlSYNCHNOT As String
        Dim mlHAVINGDETAILS As Boolean
        Dim mlSITECARDID As String
        Dim mlSITECARDDESC As String
        Dim mlBRANCH As String
        Dim mlSQL_UPDATELINE As Integer
        Dim mlSQL_NOTUPDATE As String

        Dim mlSTARTTIME As Date
        Dim mlENDTIME As Date

        mlSTARTTIME = Now
        mlMESSAGE.Text = ""
        Try
            mlNEWRECORD = False
            Select Case mpSTATUS
                Case "Edit", "Delete"
                    mlSQL = ""
            End Select

            Select Case mpSTATUS
                Case "New"
                    mlNEWRECORD = True

                Case "Edit"
                    mlSTATUS = "Edit"
                    mlNEWRECORD = True

                Case "Delete"
                    mlSTATUS = "Delete"
            End Select
            If mlOBJGF.IsNone(mlSQL) = False Then mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
            mlSQL = ""

            mlHAVINGDETAILS = False
            mlUSER = ""
            mlUSERLEVEL2 = 0
            mlSYNCHITEM = ""
            mlSYNCHNOT = ""
            mlSQL_UPDATELINE = 1
            mlSQL_NOTUPDATE = ""
            mlPASS_STRING = "DIRECT"
            mlPASS_STRING2 = "N/A"
            mlLOOPTIMES = "5"
            mlSYNCHITEMQ = 0

            mlSQL_SITECARD = ""
            mlSQL_SITECARD2 = ""
            Select Case mpTASKCODE
                Case "1"
                    mlSQL_SITECARD = " AND LineNo_ = '" & mpPARAM1 & "'"
                    mlSQL_SITECARD2 = " AND SiteCardID = '" & mpPARAM1 & "'"
            End Select

            mlI = 0
            If mlNEWRECORD = True Then
                mlSQL = ""

                mlSQL = mlSQL & "DELETE FROM OP_USER_SITE WHERE PARENTCODE='" & mlFUNCTIONPARAMETER & "' AND UserID IN " & _
                    " (SELECT DISTINCT UserID FROM OP_USER_SITE_LOG WHERE PARENTCODE='" & mlFUNCTIONPARAMETER & "') " & mlSQL_SITECARD2 & ";"
                If mlOBJGF.IsNone(mlSQL) = False Then mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
                mlSQL = ""

                
                mlSQLTEMP = "SELECT LineNo_,SearchName,Scat,SiteSupv_,OM,[SeniorOM],[Team Leader],[zone],Branch FROM [" & mlCOMPANYTABLENAME & "CustServiceSite] WHERE Status='0' " & mlSQL_SITECARD
                mlREADER = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", mlCOMPANYID)
                While mlREADER.Read
                    mlSITECARDID = Trim(mlREADER("LineNo_")) & ""
                    mlSITECARDDESC = Trim(mlREADER("SearchName")) & ""
                    mlBRANCH = Trim(mlREADER("Branch")) & ""

                    mlK = 1
                    For mlK = 1 To mlLOOPTIMES
                        Select Case mlK
                            Case "1"
                                mlUSER = Trim(mlREADER("SiteSupv_"))
                                mlUSERLEVEL2 = "1"
                            Case "1"
                                mlUSER = Trim(mlREADER("Scat"))
                                mlUSERLEVEL2 = "1"
                            Case "2"
                                If mlCOMPANYID = "IFS3" Or mlCOMPANYID = "IPM3" Or mlCOMPANYID = "ISSN3" Then
                                    If (UCase(Trim(mlREADER("OM"))) = "N/A") Or (UCase(Trim(mlREADER("OM"))) = "DIRECT") Then
                                        If (UCase(Trim(mlREADER("SeniorOM"))) = "N/A") Then
                                            mlUSER = Trim(mlREADER("Team Leader"))
                                            mlUSERLEVEL2 = "2"
                                        Else
                                            mlUSER = Trim(mlREADER("SeniorOM"))
                                            mlUSERLEVEL2 = "2"
                                        End If
                                    Else
                                        mlUSER = Trim(mlREADER("OM"))
                                        mlUSERLEVEL2 = "2"
                                    End If
                                Else
                                    mlUSER = Trim(mlREADER("OM"))
                                    mlUSERLEVEL2 = "2"
                                End If

                            Case "3"
                                If mlCOMPANYID = "IFS3" Or mlCOMPANYID = "IPM3" Or mlCOMPANYID = "ISSN3" Then
                                    If (UCase(Trim(mlREADER("SeniorOM"))) = "N/A") Then
                                        If (UCase(Trim(mlREADER("Team Leader"))) = "N/A") Then
                                            mlUSER = mlUSER
                                            mlUSERLEVEL2 = "3"
                                        Else
                                        End If
                                    Else
                                        mlUSER = Trim(mlREADER("SeniorOM"))
                                        mlUSERLEVEL2 = "3"
                                    End If
                                Else
                                    mlUSER = Trim(mlREADER("SeniorOM"))
                                    mlUSERLEVEL2 = "2"
                                End If

                            Case "4"
                                mlUSER = Trim(mlREADER("Team Leader"))
                                mlUSERLEVEL2 = "3"

                            Case "5"
                                mlUSER = Trim(mlREADER("ZONE"))
                                If mlUSER = "N/A" Then mlUSER = ""
                                If mlUSER.Contains("/") = True Then
                                    mlUSERLEVEL2 = "3"
                                Else
                                    mlUSER = ""
                                End If
                        End Select

                        If (mlUSER <> mlPASS_STRING And mlUSER <> mlPASS_STRING2) Then
                            mlUSERID = mlOBJGF.GetStringAtPosition(mlUSER, 1, "/")
                            mlUSERNAME = mlOBJGF.GetStringAtPosition(mlUSER, 0, "/")

                            'If mlUSERID = "N100074" Then Stop

                            If mlUSERID <> "" Then
                                mlSQL_NOTUPDATE = IIf(mlSQL_NOTUPDATE <> "", " UNION ALL ", "") & "SELECT * FROM OP_USER_SITE WHERE SiteCardID = '" & mlSITECARDID & "' " & _
                                " AND UserID = '" & mlUSERID & "' AND UserLevel = '" & mlUSERLEVEL2 & "'"

                                mlSQLTEMP = "SELECT * FROM OP_USER_SITE WHERE SiteCardID = '" & mlSITECARDID & "' " & _
                                " AND UserID = '" & mlUSERID & "' AND UserLevel = '" & mlUSERLEVEL2 & "' AND PARENTCODE='" & mlFUNCTIONPARAMETER & "'"
                                mlREADER2 = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
                                If mlREADER2.HasRows Then
                                    mlSYNCHNOT = mlSYNCHNOT & "<br> " & mlUSERID & "-" & mlUSERNAME & "-" & mlSITECARDID & "-" & mLSITECARDDESC
                                Else
                                    If mlHAVINGDETAILS = False Then mlHAVINGDETAILS = True
                                    mlSYNCHITEMQ = mlSYNCHITEMQ + 1
                                    mlSYNCHITEM = mlSYNCHITEM & "<br> " & mlSYNCHITEMQ & "# " & mlUSERID & "-" & mlUSERNAME & "-" & mlSITECARDID & "-" & mLSITECARDDESC
                                    mlI = mlI + 1
                                    mpSTATUS = "New"

                                    mlKEY = mlOBJGS.GetModuleCounter("OP_USERSITECARD_" & mlFUNCTIONPARAMETER_ORI, "")
                                    mlSQL = mlSQL & "DELETE FROM OP_USER_SITE WHERE SiteCardID = '" & mlSITECARDID & "' " & _
                                        " AND UserID = '" & mlUSERID & "' AND UserLevel = '" & mlUSERLEVEL2 & "';"
                                    mlSQL = mlSQL & Sql_InsertSiteCard(mlUSERID, mlUSERNAME, mlSITECARDID, mlSITECARDDESC, mlUSERLEVEL2, ddENTITY.Text, mlBRANCH)
                                    mlSQL = mlSQL & mlOBJPJ.ISS_OP_UserSiteCard_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
                                    mlSQL_UPDATELINE = mlSQL_UPDATELINE + 1
                                End If
                                mlOBJGS.CloseFile(mlREADER2)
                            End If
                        End If
                    Next

                    If mlSQL_UPDATELINE >= mlSQL_UPDATEINTERVAL Then
                        mlSQL_UPDATELINE = 1
                        If mlOBJGF.IsNone(mlSQL) = False Then mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
                        mlSQL = ""
                    End If
                End While
                mlOBJGS.CloseFile(mlREADER)
                mlOBJGS.CloseFile(mlREADER2)

                If mlHAVINGDETAILS = True Then
                    mlKEY = mlOBJGS.GetModuleCounter("UT_SYNCHRONIZE_" & mlFUNCTIONPARAMETER_ORI, "00000000")
                    mlSQL = mlSQL & " INSERT INTO UT_SYNCHRONIZE (ParentCode,SysID,DocNo,DocDate,ItemKey,Description,CompanyID," & _
                        " RecordStatus,RecUserID,RecDate) VALUES ( " & _
                        " '" & mlFUNCTIONPARAMETER & "','OP', '" & mlKEY & "','" & Now & "'," & _
                        " 'USC','Nav User Site Card Level','" & ddENTITY.Text & "'," & _
                        " 'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "');"
                End If
            End If
            If mlOBJGF.IsNone(mlSQL) = False Then mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
            mlSQL = ""

            mlSQL = "DELETE FROM OP_USER_SITE WHERE NOT  EXISTS ( " & mlSQL_NOTUPDATE & ")"
            'If mlOBJGF.IsNone(mlSQL) = False Then mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
            mlSQL = ""

            RetrieveFieldsDetail("")

            lbRESULT.Text = mlSYNCHITEMQ & " Data Update <br> " & mlSYNCHITEM
            mlENDTIME = Now
            mlMESSAGE.Text = "Synchronize Successfull, Start on :  " & mlSTARTTIME & " End of : " & mlENDTIME
        Catch ex As Exception
            mlSQL = Err.Description
            mlMESSAGE.Text = "Synchronize Not Complete" & "<br>" & Err.Description
            mlOBJGS.XMtoLog("MR", "UserSiteCard", "UserSiteCard" & mlKEY, Err.Description, "New", Session("mgUSERID"), mlOBJGF.FormatDate(Now))
        End Try
    End Sub


    
    
  
End Class
