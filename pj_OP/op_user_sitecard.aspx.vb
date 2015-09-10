Imports System
Imports System.Data
Imports System.Web.UI.HtmlControls
Imports System.Data.OleDb
Imports IAS.Core.CSCode

Partial Class op_user_sitecard
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
    Dim mlFUNCTIONPARAMETER As String

    Dim mlSQLTEMP As String
    Dim mlRSTEMP As OleDb.OleDbDataReader
    Dim mlCURRENTDATE As String = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()
    Dim mlI As Integer
    Dim mlCOMPANYTABLENAME As String
    Dim mlEntityID As String
    Dim mlPARAM_COMPANY As String

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        CekSession()
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "User Site Card V2.04"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "User Site Card V2.04"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlFUNCTIONPARAMETER = Request("mpFP")
        If mlOBJGF.IsNone(mlFUNCTIONPARAMETER) = True Then mlFUNCTIONPARAMETER = "1"

        mlPARAM_COMPANY = Trim(Request("mpFP"))
        Select Case mlPARAM_COMPANY
            Case "", "1"
                ddENTITY.Items.Clear()
                ddENTITY.Items.Add(New ListItem("ISS", "ISS"))
            Case "2"
                ddENTITY.Items.Clear()
                ddENTITY.Items.Add(New ListItem("IFS", "IFS"))
            Case "3"
                ddENTITY.Items.Clear()
                ddENTITY.Items.Add(New ListItem("IPM", "IPM"))
            Case "4"
                ddENTITY.Items.Clear()
                ddENTITY.Items.Add(New ListItem("ICS", "ICS"))
        End Select



        If Page.IsPostBack = False Then
            'LoadComboData()
            pnSEARCHSITECARD.Visible = False
            pnSEARCHUSERID.Visible = False
            DisableCancel()
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "op_user_sitecard", "User SiteCard", "")
        Else
        End If

        hpLookup.NavigateUrl = "javascript:OpenWinLookUpSiteCard('" + mpSITECARD.ClientID + "','" + mpSITEDESC.ClientID + "','" + hdnSiteCardID.ClientID + "','" + hdnSiteCardName.ClientID + "','" + mpJobNo.ClientID + "','" + mpJobTaskNo.ClientID + "','" + hdnJobNo.ClientID + "','" + hdnJobTaskNo.ClientID + "','" + ddENTITY.ClientID + "," + "'AccMnt')"

    End Sub

    Protected Sub CekSession()
        If Session("mgMENUSTYLE") = "" Then
            Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=34FC35D4")
            Return
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
        If pnFILL.Visible = True Then
            SaveRecord()
        End If
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

            If mpUSERID.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " UserID = '" & mpUSERID.Text & "' "
            End If

            If mpSITECARD.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " SiteCardID = '" & mpSITECARD.Text & "'"
            End If

            'If mlSTATUS.Text <> "" Then
            mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " RecordStatus LIKE 'New' "
            'End If

            mlFUNCTIONPARAMETER2 = mlFUNCTIONPARAMETER
            mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " ParentCode = '" & mlFUNCTIONPARAMETER2 & "'"

            If mlOBJGF.IsNone(mlSQL) = False Then
                Try
                    mlSQL2 = "SELECT DocNo,UserID,SiteCardID as SiteCard,SiteCardName as SiteName, UserLevel  as MRLevel FROM OP_USER_SITE" & _
                            " WHERE " & mlSQL & " ORDER BY DocNo"
                    RetrieveFieldsDetail(mlSQL)
                Catch ex As Exception
                End Try
            End If


        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btSearchRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSearchRecord.Click
        SearchRecord()
    End Sub


    Public Sub RetrieveFields()
        DisableCancel()

        mlSQL = "SELECT * FROM OP_USER_SITE WHERE DocNo = '" & Trim(mlKEY) & "'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3")
        If mlREADER.HasRows Then
            mlREADER.Read()
            mpDOCUMENTNO.Text = mlREADER("DocNo") & ""
            mpUSERID.Text = mlREADER("UserID") & ""
            mpUSERDESC.Text = mlREADER("UserName") & ""
            mpSITECARD.Text = mlREADER("SiteCardID") & ""
            mpSITEDESC.Text = mlREADER("SiteCardName") & ""
            Try
                mpMRLEVEL.SelectedValue = mlREADER("UserLevel")
            Catch ex As Exception
                mpMRLEVEL.Items.Add(mlREADER("UserLevel"))
            End Try

            btSITECARD_Click(Nothing, Nothing)
            btUSERID_Click(Nothing, Nothing)
        End If
    End Sub

    Sub RetrieveFieldsDetail(ByVal mpSQL As String)
        Try

        
            If mpSQL = "" Then
                mlSQL2 = "SELECT DocNo,UserID,UserName,SiteCardID as SiteCard,SiteCardName as SiteName, UserLevel  as MRLevel FROM OP_USER_SITE" & _
                    " WHERE RecordStatus='New'  AND EntityID='" & ddENTITY.Text & "'   ORDER BY DocNo"
            Else
                mpSQL = mlSQL2
            End If
            mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISSP3")
            mlDATAGRID.DataSource = mlREADER2
            mlDATAGRID.DataBind()

            mlOBJGS.CloseFile(mlREADER2)

        Catch ex As Exception
            mlSQL2 = Err.Description

        End Try
    End Sub


    Sub DeleteRecord()
        mlSPTYPE = "Delete"
        Try
            Sql_1(mlSPTYPE, mlKEY)
            lnVIEWUSER_Click(Nothing, Nothing)
        Catch ex As Exception
        End Try

        mlSYSCODE.Value = ""
        RetrieveFields()
    End Sub

    Sub NewRecord()
        mlOBJGS.mgNEW = True
        mlOBJGS.mgEDIT = False
        EnableCancel()
        ClearFields()
        LoadComboData()
        mpDOCUMENTNO.Text = "--AUTONUMBER--"
        mlOBJPJ.SetTextBox(True, mpDOCUMENTNO)
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

        mlOBJPJ.SetTextBox(False, mpUSERID)
        mlOBJPJ.SetTextBox(False, mpSITECARD)
        btUSERID.Enabled = True
        btSITECARD.Enabled = True
        mpMRLEVEL.Enabled = True
    End Sub

    Private Sub DisableCancel()
        btNewRecord.Visible = True
        btSaveRecord.Visible = False
        pnFILL.Visible = False

        mlOBJPJ.SetTextBox(True, mpUSERID)
        mlOBJPJ.SetTextBox(True, mpSITECARD)
        btUSERID.Enabled = False
        btSITECARD.Enabled = False
        mpMRLEVEL.Enabled = False
    End Sub

    Sub ClearFields()
        mpDOCUMENTNO.Text = ""
        mpUSERID.Text = ""
        mpSITECARD.Text = ""
        mpUSERDESC.Text = ""
        mpSITEDESC.Text = ""
        mpMRLEVEL.Items.Clear()
    End Sub


    Sub LoadComboData()
        mlSQL = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID='MR_POSTLEVEL' ORDER BY LinCode"
        mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3")
        While mlREADER.Read
            mpMRLEVEL.Items.Add(Trim(mlREADER("LinCode")) & "-" & mlREADER("Description"))
        End While

        'ddENTITY.Items.Clear()
        'ddENTITY.Items.Add(New ListItem("Choose", ""))
        'mlSQLTEMP = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID='ISS_Entity'"
        'mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISS")
        'While mlRSTEMP.Read
        '    ddENTITY.Items.Add(New ListItem(Trim(mlRSTEMP("LinCode")), Trim(mlRSTEMP("LinCode"))))
        'End While

    End Sub

    Sub SaveRecord()
        Dim mlSQLHR As String = ""
        Dim mlSQLDT As String = ""

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
            mlKEY = Trim(mpDOCUMENTNO.Text)
            Sql_1(mlSPTYPE, mlKEY)
        Catch ex As Exception
        End Try

        mlSYSCODE.Value = ""
        ClearFields()
        DisableCancel()
    End Sub

    Function ValidateForm() As String
        ValidateForm = ""

        If ddENTITY.SelectedValue = "" Then
            ValidateForm = ValidateForm & " <br /> Entity can't be empty, please choose Entity.."
        End If

        If mlOBJGS.mgNEW = True Then

            mlSQLTEMP = "SELECT * FROM OP_USER_SITE WHERE UserID = '" & mpUSERID.Text & "' AND SiteCardID = '" & Trim(mpSITECARD.Text) & "'"
            mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
            If mlRSTEMP.HasRows Then
                mlRSTEMP.Read()
                ValidateForm = ValidateForm & " <br /> User ID  of " & mpUSERID.Text & " has assign to " & mpSITECARD.Text & " e (not allow duplicate)"
            End If
            mlOBJGS.CloseFile(mlRSTEMP)
        End If

    End Function

    Protected Sub btUSERID_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btUSERID.Click
        mpUSERDESC.Text = mlOBJGS.ADGeneralLostFocus("USER", mpUSERID.Text)
        ViewLink()
    End Sub

    Protected Sub btSEARCHUSERID_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHUSERID.Click
        If pnSEARCHUSERID.Visible = False Then
            pnSEARCHUSERID.Visible = True
        Else
            pnSEARCHUSERID.Visible = False
        End If
    End Sub

    Protected Sub btSEARCHUSERIDSUBMIT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHUSERIDSUBMIT.Click
        If mlOBJGF.IsNone(mpSEARCHUSERID.Text) = False Then SearchUser(1, mpSEARCHUSERID.Text)
    End Sub

    Protected Sub mlDATAGRIDUSERID_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRIDUSERID.ItemCommand
        Try
            mpUSERID.Text = CType(e.Item.Cells(0).Controls(0), LinkButton).Text
            mpUSERDESC.Text = CType(e.Item.Cells(1).Controls(0), LinkButton).Text
            mlDATAGRIDUSERID.DataSource = Nothing
            mlDATAGRIDUSERID.DataBind()
            pnSEARCHUSERID.Visible = False
            ViewLink()
        Catch ex As Exception
        End Try
    End Sub

    Sub SearchUser(ByVal mpTYPE As Byte, ByVal mpNAME As String)
        Try
            Select Case mpTYPE
                Case "1"
                    mlSQLTEMP = "SELECT UserID, Name FROM AD_USERPROFILE WHERE Name LIKE  '%" & mpNAME & "%'"
                    mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "AD", "AD")
                    mlDATAGRIDUSERID.DataSource = mlRSTEMP
                    mlDATAGRIDUSERID.DataBind()
            End Select
        Catch ex As Exception
        End Try
    End Sub

    'ss

    Protected Sub btSITECARD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSITECARD.Click
        If Trim(UCase(Trim(mpSITECARD.Text))) = "ALL" Then
            mpSITECARD.Text = "ALL"
            mpSITEDESC.Text = "ALL"
            ViewLink()
        Else
            Try
                mpSITEDESC.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_DESC", Trim(mpSITECARD.Text), Trim(ddENTITY.SelectedItem.Text))
            Catch ex As Exception
                mlMESSAGE.Text = ex.Message
            End Try

            ViewLink()
        End If



    End Sub


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
            mpSITECARD.Text = CType(e.Item.Cells(0).Controls(0), LinkButton).Text
            mpSITEDESC.Text = CType(e.Item.Cells(1).Controls(0), LinkButton).Text
            mlDATAGRIDSITECARD.DataSource = Nothing
            mlDATAGRIDSITECARD.DataBind()
            pnSEARCHSITECARD.Visible = False
            ViewLink()
        Catch ex As Exception
        End Try
    End Sub

    Sub SearchSiteCard(ByVal mpTYPE As Byte, ByVal mpNAME As String)
        Select Case mpTYPE
            Case "1"
                'mlSQLTEMP = "SELECT LineNo_,SearchName FROM  [" & mlCOMPANYTABLENAME & "CustServiceSite] WHERE SearchName LIKE  '%" & mlSEARCHSITECARD.Text & "%'"
                'mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", mlEntityID)
                'mlDATAGRIDSITECARD.DataSource = mlRSTEMP

                mlDATAGRIDSITECARD.DataSource = mlOBJPJ.ISS_XMGeneralLookup("SITECARD_SEARCH", Trim(mlSEARCHSITECARD.Text), Trim(ddENTITY.Text))
                mlDATAGRIDSITECARD.DataBind()
        End Select
    End Sub

    
    Protected Sub lnVIEWSITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnVIEWSITE.Click
        mlSQL2 = "SELECT DocNo,UserID,UserName,SiteCardID as SiteCard,SiteCardName as SiteName, UserLevel  as MRLevel FROM OP_USER_SITE" & _
            " WHERE RecordStatus='New' AND SiteCardID = '" & Trim(mpSITECARD.Text) & "' AND EntityID='" & ddENTITY.Text & "' ORDER BY DocNo"
        RetrieveFieldsDetail(mlSQL2)
    End Sub

    Protected Sub lnVIEWUSER_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnVIEWUSER.Click
        mlSQL2 = "SELECT DocNo,UserID,UserName,SiteCardID as SiteCard,SiteCardName as SiteName, UserLevel  as MRLevel FROM OP_USER_SITE" & _
        " WHERE RecordStatus='New' AND UserID = '" & Trim(mpUSERID.Text) & "' AND EntityID='" & ddENTITY.Text & "' ORDER BY DocNo"
        RetrieveFieldsDetail(mlSQL2)
    End Sub


    Protected Sub lnlnVIEWNORMAL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnVIEWNORMAL.Click
        mlSQL2 = ""
        RetrieveFieldsDetail(mlSQL2)
    End Sub

    Sub ViewLink()
        If mpUSERDESC.Text <> "" And mpSITEDESC.Text <> "" Then
            lnVIEWUSER.Visible = True
            lnVIEWSITE.Visible = True
        ElseIf mpUSERDESC.Text <> "" Then
            lnVIEWUSER.Visible = True
            lnVIEWSITE.Visible = False
        ElseIf mpSITEDESC.Text <> "" Then
            lnVIEWUSER.Visible = False
            lnVIEWSITE.Visible = True
        End If
    End Sub

    Sub Sql_1(ByVal mpSTATUS As String, ByVal mpCODE As String)
        Dim mlSTATUS As String
        Dim mlNEWRECORD As Boolean
        Dim mlUSERLEVEL2 As String

        Try
            mlNEWRECORD = False
            mlUSERLEVEL2 = mlOBJGF.GetStringAtPosition(mpMRLEVEL.Text, 0, "-")

            Select Case mpSTATUS
                Case "Edit", "Delete"
                    mlSQL = ""
                    mlSQL = mlSQL & mlOBJPJ.ISS_OP_UserSiteCard_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
            End Select

            Select Case mpSTATUS
                Case "New"
                    mlNEWRECORD = True
                    If mpDOCUMENTNO.Text = "--AUTONUMBER--" Then
                        mlKEY = mlOBJGS.GetModuleCounter("OP_USERSITECARD_" & mlFUNCTIONPARAMETER, "00000000")
                        mpDOCUMENTNO.Text = mlKEY
                    Else
                        mlKEY = Trim(mpDOCUMENTNO.Text)
                    End If

                Case "Edit"
                    mlSTATUS = "Edit"
                    mlNEWRECORD = True
                    mlSQL = mlSQL & " DELETE FROM OP_USER_SITE WHERE DocNo = '" & mlKEY & "';"

                Case "Delete"
                    mlSTATUS = "Delete"
                    mlSQL = mlSQL & " DELETE FROM OP_USER_SITE WHERE DocNo = '" & mlKEY & "';"
            End Select
            If mlOBJGF.IsNone(mlSQL) = False Then mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
            mlSQL = ""


            mlI = 0
            If mlNEWRECORD = True Then
                mlSQL = ""
                mlI = mlI + 1
                mlSQL += " INSERT INTO OP_USER_SITE " & vbCrLf
                mlSQL += "  (ParentCode,SysID,DocNo,DocDate,UserID,UserName,Linno,SiteCardID,SiteCardName,UserLevel,EntityID,RecordStatus,RecUserID,RecDate,JobNo,JobTaskNo) " & vbCrLf
                mlSQL += " VALUES ( '" & mlFUNCTIONPARAMETER & "','MR', '" & mlKEY & "','" & mlOBJGF.FormatDate(Now) & "'," & vbCrLf
                mlSQL += " '" & Trim(mpUSERID.Text) & "','" & Trim(mpUSERDESC.Text) & "','" & mlI & "', '" & Trim(mpSITECARD.Text) & "','" & Trim(Replace(mpSITEDESC.Text, "'", "")) & "'," & vbCrLf
                mlSQL += " '" & Trim(mlUSERLEVEL2) & "','" & Trim(ddENTITY.Text) & "'," & vbCrLf
                mlSQL += " 'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "', '" & Trim(mpJobNo.Text) & "','" & Trim(mpJobTaskNo.Text) & "');" & vbCrLf
            End If

            Select Case mpSTATUS
                Case "New"
                    mlSQL = mlSQL & mlOBJPJ.ISS_OP_UserSiteCard_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
            End Select
            mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
            mlSQL = ""

            lnVIEWSITE_Click(Nothing, Nothing)

        Catch ex As Exception
            mlOBJGS.XMtoLog("MR", "UserSiteCard", "UserSiteCard" & mlKEY, Err.Description, "New", Session("mgUSERID"), mlOBJGF.FormatDate(Now))
        End Try
    End Sub

    
    
End Class
