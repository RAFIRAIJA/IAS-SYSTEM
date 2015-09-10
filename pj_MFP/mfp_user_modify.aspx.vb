Imports System
Imports System.Data
Imports System.Data.OleDb
Imports IAS.Core.CSCode
Partial Class mfp_user_modify
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJPJ As New ModuleFunctionLocal

    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlREADER2 As OleDb.OleDbDataReader
    Dim mlSQL2 As String
    Dim mlRSTEMP As OleDb.OleDbDataReader
    Dim mlSQLTEMP As String
    Dim mlKEY As String
    Dim mlRECORDSTATUS As String
    Dim mlSPTYPE As String
    Dim mlFUNCTIONPARAMETER As String
    Dim mlI As Integer
    Dim mlCURRENTDATE As String = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Multi Function Printer Xerox Utility V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Multi Function Printer Xerox Utility V2.00"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")


        'Session("mgUSERID") = "n101121"
        'Session("mgNAME") = "sugianto"

        mlFUNCTIONPARAMETER = Request("mpFP")
        If Page.IsPostBack = False Then
            LoadComboData()
            RetrieveFieldsDetail("")
            DisableCancel()

            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "", "", "")
        Else
        End If
    End Sub

    Protected Sub mlDATAGRID_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRID.ItemCommand
        mlKEY = e.CommandArgument
        Select Case e.CommandName
            Case "BrowseRecord"
                mlMESSAGE.Text = "View Request for " & e.CommandArgument
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
        mlMESSAGE.Text = ""
        ClearFields()
        DisableCancel()
    End Sub

    Sub SearchRecord()
        Dim mlSQL As String

        If pnFILL.Visible = False Then
            ClearFields()
            EnableCancel()
            pnFILL.Visible = True
            Exit Sub
        End If

        Try
            mlSQL = ""
            If mlOBJGF.IsNone(mlSQL) = False Then
                Try
                    mlSQL2 = "SELECT "
                    RetrieveFieldsDetail(mlSQL)
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
        Try

        
            If mpSQL = "" Then
                mlSQL2 = "SELECT UNIQUE_ID as EmployeeID,CN as Name, DISPLAY_NAME as NickName, MAIL as Email, OU as Position" & _
                    " FROM NIM_USER" & _
                    " WHERE UNIQUE_ID = '" & Session("mgUSERID") & "'"
            Else
                mlSQL2 = mpSQL
            End If
            mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "MFIMD")
            mlDATAGRID.DataSource = mlREADER2
            mlDATAGRID.DataBind()

            mlOBJGS.CloseFile(mlREADER2)

        Catch ex As Exception
        End Try
    End Sub


    Sub DeleteRecord()
        mlSPTYPE = "Delete"
        Try
            Sql_1(mlSPTYPE, mlKEY)
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
        btNewRecord.Visible = False
        btSaveRecord.Visible = False
        pnFILL.Visible = True

        pnUSER.Visible = False
        pnPASSWORD.Visible = False
        pnSEARCHUSERID.Visible = False
    End Sub

    Sub ClearFields()
        txUSERID.Text = ""
        txUSERDESC.Text = ""
        txPASSWORD.Text = ""
        txREPASSWORD.Text = ""
    End Sub


    Sub LoadComboData()
        ddTYPE.Items.Clear()
        ddTYPE.Items.Add("Choose")

        If mlFUNCTIONPARAMETER = "2" Then
            ddTYPE.Items.Add("Activate Other User")
            ddTYPE.Items.Add("Reset Password for Other User")
        End If

        ddTYPE.Items.Add("Activate My Printer Account")
        ddTYPE.Items.Add("Change My Printer Password")
    End Sub

    Protected Sub btUSERID_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btUSERID.Click
        If mlOBJGF.IsNone(Trim(txUSERID.Text)) = False Then
            txUSERDESC.Text = mlOBJGS.ADGeneralLostFocus("USER", Trim(txUSERID.Text))
        End If
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
            txUSERID.Text = CType(e.Item.Cells(0).Controls(0), LinkButton).Text
            txUSERDESC.Text = CType(e.Item.Cells(1).Controls(0), LinkButton).Text
            mlDATAGRIDUSERID.DataSource = Nothing
            mlDATAGRIDUSERID.DataBind()
            pnSEARCHUSERID.Visible = False
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


    Protected Sub btTYPE_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btTYPE.Click
        Select Case ddTYPE.Text
            Case "Choose"
                mlMESSAGE.Text = "Please Choose your order"

            Case "Activate Other User"
                pnUSER.Visible = True
                pnPASSWORD.Visible = False
                txUSERID.Text = ""
                txUSERDESC.Text = ""
                btUSERID.Visible = True
                btSEARCHUSERID.Visible = True
                mlOBJPJ.SetTextBox(False, txUSERID)

            Case "Activate My Printer Account"
                pnUSER.Visible = True
                pnPASSWORD.Visible = False
                txUSERID.Text = Session("mgUSERID")
                txUSERDESC.Text = Session("mgNAME")
                mlOBJPJ.SetTextBox(True, txUSERID)
                btUSERID.Visible = False
                btSEARCHUSERID.Visible = False

            Case "Change My Printer Password"
                pnUSER.Visible = False
                pnPASSWORD.Visible = True

                txUSERID.Text = Session("mgUSERID")
                txUSERDESC.Text = Session("mgNAME")
                mlOBJPJ.SetTextBox(True, txUSERID)
                txPASSWORD.Text = ""
                txREPASSWORD.Text = ""

            Case "Reset Password for Other User"
                pnUSER.Visible = True
                pnPASSWORD.Visible = True

                txUSERID.Text = ""
                txUSERDESC.Text = ""
                btUSERID.Visible = True
                btSEARCHUSERID.Visible = True
                mlOBJPJ.SetTextBox(False, txUSERID)

                txPASSWORD.Text = ""
                txREPASSWORD.Text = ""
        End Select

        mlMESSAGE.Text = ""
        EnableCancel()
    End Sub

    

    Function ValidateForm() As String
        ValidateForm = ""

        btUSERID_Click(Nothing, Nothing)

        Select Case ddTYPE.Text
            Case "Choose"
                mlMESSAGE.Text = "Please Choose your order"

            Case "Activate Other User"
                If txUSERID.Text = "" Then
                    ValidateForm = ValidateForm & " <br /> User ID tidak boleh kosong"
                End If

                If txUSERDESC.Text = "" Then
                    ValidateForm = ValidateForm & " <br /> User ID tidak boleh kosong"
                End If

            Case "Activate My Printer Account"
                If txUSERID.Text = "" Then
                    ValidateForm = ValidateForm & " <br /> User ID tidak boleh kosong"
                End If

                If txUSERDESC.Text = "" Then
                    ValidateForm = ValidateForm & " <br /> User ID tidak boleh kosong"
                End If

                
            Case "Change My Printer Password"
                If txUSERID.Text = "" Then
                    ValidateForm = ValidateForm & " <br /> User ID tidak boleh kosong"
                End If

                If txUSERDESC.Text = "" Then
                    ValidateForm = ValidateForm & " <br /> User ID tidak boleh kosong"
                End If

                If Len(txPASSWORD.Text) < "6" Then
                    ValidateForm = ValidateForm & " <br /> Panjang Password harus minimal 6 huruf/angka"
                End If


                If txPASSWORD.Text = "" Then
                    ValidateForm = ValidateForm & " <br /> Password tidak boleh kosong"
                End If

                If txREPASSWORD.Text = "" Then
                    ValidateForm = ValidateForm & " <br /> Retype Password tidak boleh kosong"
                End If


                If txPASSWORD.Text <> txREPASSWORD.Text Then
                    ValidateForm = ValidateForm & " <br /> Password dan  Retype Password harus sama"
                End If
        End Select
    End Function



    Sub Sql_1(ByVal mpSTATUS As String, ByVal mpCODE As String)
        Dim mlSTATUS As String
        Dim mlNEWRECORD As Boolean

        Try
            mlNEWRECORD = False

            Select Case mpSTATUS
                Case "Edit", "Delete"
                    mlSQL = ""
                    mlSQL = mlSQL & mlOBJPJ.ISS_OP_UserSiteCard_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
            End Select

            Select Case mpSTATUS
                Case "New"
                    mlNEWRECORD = True
                    
                Case "Edit"
                    mlSTATUS = "Edit"
                    mlNEWRECORD = True
                    mlSQL = mlSQL & " DELETE FROM  WHERE DocNo = '" & mlKEY & "';"

                Case "Delete"
                    mlSTATUS = "Delete"
                    mlSQL = mlSQL & " DELETE FROM  WHERE DocNo = '" & mlKEY & "';"
            End Select
            If mlOBJGF.IsNone(mlSQL) = False Then mlOBJGS.ExecuteQuery(mlSQL)
            mlSQL = ""


            mlI = 0
            If mlNEWRECORD = True Then
                mlSQL = ""
                mlI = mlI + 1
                mlSQL = mlSQL & " INSERT INTO " & _
                            " RecordStatus,RecUserID,RecDate) VALUES ( " & _
                            " '" & mlFUNCTIONPARAMETER & "','" & mlKEY & "','" & mlOBJGF.FormatDate(Now) & "'," & _
                            " 'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "');"
            End If

            Select Case mpSTATUS
                Case "New"
                    mlSQL = mlSQL & mlOBJPJ.ISS_OP_UserSiteCard_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
            End Select
            mlOBJGS.ExecuteQuery(mlSQL)
            mlSQL = ""

        Catch ex As Exception
            mlOBJGS.XMtoLog("", "", "" & mlKEY, Err.Description, "New", Session("mgUSERID"), mlOBJGF.FormatDate(Now))
        End Try
    End Sub


    Sub SaveRecord()
        Dim mlSQLHR As String = ""
        Dim mlSQLDT As String = ""

        Dim mlFILENAME As String
        Dim mlPROCESS_TYPE As String
        Dim mlORGANIZATION As String
        Dim mlUSER_NAME As String
        Dim mlUSER_ID As String
        Dim mlEMAIL As String
        Dim mlPASSWORD As String
        Dim mlAFFILIATE As String

        Dim mlURLDEST As New System.Net.WebClient
        Dim mlURLLOCAL As String
        Dim mlURLADDR As String
        Dim mlSENDURL As String
        Dim mlURL_FAILKEY As String

        Dim mlEXECUTETASK As Boolean

        mlMESSAGE.Text = ""
        mlEXECUTETASK = False

        mlOBJGS.mgMESSAGE = ValidateForm()
        If mlOBJGF.IsNone(mlOBJGS.mgMESSAGE) = False Then
            mlMESSAGE.Text = mlOBJGS.mgMESSAGE
            Exit Sub
        End If

        mlSPTYPE = "New"
        If mlSYSCODE.Value = "edit" Then
            mlSPTYPE = "Edit"
        End If


        mlURLLOCAL = mlOBJGS.FindSetup("UT_UTSETUP", "MFP_URL_User")
        mlPASSWORD = ""
        mlSENDURL = ""

        Try
            Select Case ddTYPE.Text
                Case "Activate Other User", "Activate My Printer Account"
                    mlPROCESS_TYPE = "a"
                    mlPASSWORD = ""
                    mlEXECUTETASK = True

                Case "Change My Printer Password"
                    mlPROCESS_TYPE = "m"
                    mlPASSWORD = Trim(txPASSWORD.Text)
                    mlEXECUTETASK = True

                Case "Reset Password for Other User"
                    mlPROCESS_TYPE = "m"
                    mlPASSWORD = Trim(txPASSWORD.Text)
                    mlEXECUTETASK = True
            End Select


            If mlEXECUTETASK = True Then
                mlSQLTEMP = "SELECT * FROM AD_USERPROFILE WHERE UserID = '" & Trim(txUSERID.Text) & "'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "AD", "AD")
                If mlRSTEMP.HasRows Then
                    mlRSTEMP.Read()

                    Try
                        If mlPASSWORD = "" Then
                            mlPASSWORD = Day(mlRSTEMP("dob")).ToString("00") & Month(mlRSTEMP("dob")).ToString("00") & Year(mlRSTEMP("dob")).ToString("00")
                            mlMESSAGE.Text = "password adalah tanggal lahir anda"
                        End If
                    Catch ex As Exception
                        mlMESSAGE.Text = "tanggal lahir tidak ditemukan, password adalah userid anda"
                        mlPASSWORD = Trim(mlRSTEMP("UserID"))
                    End Try

                    mlFILENAME = Trim(mlRSTEMP("UserID"))
                    mlORGANIZATION = Trim(mlRSTEMP("DeptID")) & ""
                    mlUSER_ID = Trim(mlRSTEMP("UserID"))
                    mlUSER_NAME = Trim(mlRSTEMP("Name"))
                    mlEMAIL = Trim(Trim(mlRSTEMP("EmailAddr")).ToString) & ""

                    mlAFFILIATE = Trim(mlRSTEMP("CompanyID").ToString) & ""

                    Try
                        mlURLADDR = "http://" & mlURLLOCAL & "/ISS_mfp/pj_mfp/mfp_modifyuser.aspx?mpFN=" & mlFILENAME & "&mpPT=" & mlPROCESS_TYPE & "&mpOR=" & mlORGANIZATION & "&mpUN=" & mlUSER_NAME & "&mpUID=" & mlUSER_ID & "&mpEM=" & mlEMAIL & "&mpPW=" & mlPASSWORD & "&mpAF=" & mlAFFILIATE
                        mlSENDURL = mlURLDEST.DownloadString(mlURLADDR)

                        mlMESSAGE.Text = mlMESSAGE.Text & "<br>" & mlSENDURL
                    Catch ex As Exception
                        mlMESSAGE.Text = mlMESSAGE.Text & "<br>" & mlSENDURL
                        mlURL_FAILKEY = mlSENDURL & "<br>" & mlKEY
                    End Try
                End If

                mlMESSAGE.Text = "Successfull, Please wait 20 Minutes to apply the changes" & "<br> / Sukses, silahkan tunggu 20 Menit untuk perubahannnya <br>" & mlMESSAGE.Text
                LoadComboData()
            End If

            mlRSTEMP.Close()

        Catch ex As Exception
            Response.Write(Trim(mlRSTEMP("CompanyID")))
            mlMESSAGE.Text = "Task Fail"
        End Try

        mlSYSCODE.Value = ""
        ClearFields()
        DisableCancel()
    End Sub

    End Class
