Imports System
Imports System.Data
Imports System.Data.OleDb
Imports IAS.Core.CSCode

Partial Class fs_file_upload
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJFS As New IASClass.ucmFileSystem
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
    Dim mlPATHNORMAL As String
    Dim mlPATHNORMAL2 As String

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        CekSession()
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "File Upload V2.05"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "File Upload V2.05"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlPATHNORMAL = "../App_Data/fs_updownload/"
        mlPATHNORMAL2 = "App_Data/fs_updownload/"
        mlFUNCTIONPARAMETER = "FS1"

        If Page.IsPostBack = False Then
            ClearFields()
            DisableCancel()
            RetrieveFieldsDetail("")
            pnSEARCHUSERID.Visible = False
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "", "", "")
        Else
        End If
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

    Protected Sub mlDATAGRID2_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRID2.ItemCommand
        mlKEY = e.CommandArgument
        Select Case e.CommandName
            Case "BrowseRecord"

            Case Else
                ' Ignore Other
        End Select
    End Sub

    Protected Sub mlDATAGRID3_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRID3.ItemCommand
        mlKEY = e.CommandArgument
        Select Case e.CommandName
            Case "BrowseRecord"

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
        Try

        DisableCancel()
            LoadComboData()

            mlSQL = "SELECT * FROM XM_FILEHR WHERE DocNo = '" & Trim(mlKEY) & "'"
            mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3")
            If mlREADER.HasRows Then
                mlREADER.Read()
                txDOCUMENTNO.Text = mlREADER("DocNo") & ""
                txDOCDATE1.Text = IIf(mlOBJGF.IsNone(mlREADER("DocDate")), "", mlOBJGF.ConvertDateIntltoIDN(mlREADER("DocDate"), "/") & "")

                Try
                    ddTYPE.SelectedValue = mlREADER("GroupID")
                Catch ex As Exception
                    ddTYPE.Items.Add(mlREADER("GroupID"))
                End Try

                txDESCRIPTION.Text = mlREADER("Description") & ""

                RetrieveFieldsDetail2("")
                RetrieveFieldsDetail3("")
            End If

        Catch ex As Exception

        End Try

    End Sub

    Sub RetrieveFieldsDetail(ByVal mpSQL As String)
        Try

        If mpSQL = "" Then
                mlSQL2 = "SELECT DocNo,DocDate as Date,GroupID as Group_ID,Description,RecUserID as UserID FROM XM_FILEHR" & _
                    " WHERE RecordStatus='New' AND RecUserID ='" & Session("mgUSERID") & "' AND DocDate>= '" & mlOBJGF.FormatDate(Now.Date.AddMonths(-1)) & "' " & _
                    " ORDER BY DocNo Desc"
            Else
                mlSQL2 = mpSQL
            End If
            mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISSP3")
            mlDATAGRID.DataSource = mlREADER2
            mlDATAGRID.DataBind()

            mlOBJGS.CloseFile(mlREADER2)

            'Response.Write(mlSQL2)
        Catch ex As Exception
            'Response.Write(Err.Description)
            'Response.Write(mlSQL2)

        End Try

    End Sub

    Sub RetrieveFieldsDetail2(ByVal mpSQL As String)
        Try

            If mpSQL = "" Then
                mlSQL2 = "SELECT Linno as No,FileDesc as File_Name FROM XM_FILEDT" & _
                    " WHERE DocNo='" & txDOCUMENTNO.Text & "'"
            Else
                mlSQL2 = mpSQL
            End If
            mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISSP3")
            mlDATAGRID2.DataSource = mlREADER2
            mlDATAGRID2.DataBind()

            mlOBJGS.CloseFile(mlREADER2)
        Catch ex As Exception

        End Try

    End Sub


    Sub RetrieveFieldsDetail3(ByVal mpSQL As String)
        If mpSQL = "" Then
            mlSQL2 = "SELECT Linno as No,UserID as User_id, Name FROM XM_FILEDTU" & _
                " WHERE DocNo='" & txDOCUMENTNO.Text & "' AND TYPE='1' AND RecordStatus = 'New' ORDER BY Linno"
        Else
            mlSQL2 = mpSQL
        End If
        mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISSP3")
        mlDATAGRID3.DataSource = mlREADER2
        mlDATAGRID3.DataBind()

        mlOBJGS.CloseFile(mlREADER2)
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
        txDOCUMENTNO.Text = "--AUTONUMBER--"
        mlOBJPJ.SetTextBox(True, txDOCUMENTNO)
        TR1.Visible = True
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

        mlOBJPJ.SetTextBox(False, txDOCUMENTNO)
        mlOBJPJ.SetTextBox(False, txDOCDATE1)
        btDOCDATE1.Visible = True
        ddTYPE.Enabled = True
        mlOBJPJ.SetTextBox(False, txOPENPWD)
        mlOBJPJ.SetTextBox(False, txDESCRIPTION)
        mlOBJPJ.SetTextBox(False, txUSERID)
        btITEMKEYADD.Visible = False
        btCLEARCART.Visible = True
        mlOBJPJ.SetTextBox(False, txFILEUPLOAD1_N)
        mlOBJPJ.SetTextBox(False, txFILEUPLOAD2_N)
        mlOBJPJ.SetTextBox(False, txFILEUPLOAD3_N)
        mlOBJPJ.SetTextBox(False, txFILEUPLOAD4_N)
        mlOBJPJ.SetTextBox(False, txFILEUPLOAD5_N)

        trU0.Visible = False
        trU1.Visible = True
        trU2.Visible = True
        trU3.Visible = True

        trUP0.Visible = False
        trUP1.Visible = True
        trUP1.Visible = True
        trUP2.Visible = True
        trUP3.Visible = True
        trUP4.Visible = True
        trUP5.Visible = True
    End Sub

    Private Sub DisableCancel()
        btNewRecord.Visible = True
        btSaveRecord.Visible = False
        pnFILL.Visible = False
        TR1.visible = False

        mlOBJPJ.SetTextBox(True, txDOCUMENTNO)
        mlOBJPJ.SetTextBox(True, txDOCDATE1)
        btDOCDATE1.Visible = False
        ddTYPE.Enabled = False
        mlOBJPJ.SetTextBox(True, txOPENPWD)
        mlOBJPJ.SetTextBox(True, txDESCRIPTION)
        mlOBJPJ.SetTextBox(True, txUSERID)
        btITEMKEYADD.Visible = False
        btCLEARCART.Visible = False
        mlOBJPJ.SetTextBox(True, txFILEUPLOAD1_N)
        mlOBJPJ.SetTextBox(True, txFILEUPLOAD2_N)
        mlOBJPJ.SetTextBox(True, txFILEUPLOAD3_N)
        mlOBJPJ.SetTextBox(True, txFILEUPLOAD4_N)
        mlOBJPJ.SetTextBox(True, txFILEUPLOAD5_N)

        trU0.Visible = True
        trU1.Visible = False
        trU2.Visible = False
        trU3.Visible = False
        
        trUP0.Visible = True
        trUP1.Visible = False
        trUP2.Visible = False
        trUP3.Visible = False
        trUP4.Visible = False
        trUP5.Visible = False
    End Sub

    Sub ClearFields()
        txDOCUMENTNO.Text = ""
        txDOCDATE1.Text = mlCURRENTDATE

        ddTYPE.Items.Clear()
        txOPENPWD.Text = ""
        txDESCRIPTION.Text = ""
        txUSERID.Text = ""

        txFILEUPLOAD1_N.Text = ""
        lnLINK1.Text = ""
        txFILEUPLOAD2_N.Text = ""
        lnLINK2.Text = ""
        txFILEUPLOAD3_N.Text = ""
        lnLINK3.Text = ""
        txFILEUPLOAD4_N.Text = ""
        lnLINK4.Text = ""
        txFILEUPLOAD5_N.Text = ""
        lnLINK5.Text = ""

        lbITEMCART.Text = ""
        lbITEMCART2.Value = ""
        txUSERDESC.Text = ""


        mlDATAGRID2.DataSource = Nothing
        mlDATAGRID2.DataBind()
    End Sub

    Protected Sub btITEMKEYADD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btITEMKEYADD.Click
        If txUSERDESC.Text <> "" Then
            lbITEMCART.Text = lbITEMCART.Text & IIf(mlOBJGF.IsNone(lbITEMCART.Text) = False, ",<br>", lbITEMCART.Text) & Trim(txUSERID.Text) & "-" & Trim(txUSERDESC.Text) & "-" & Trim(txUSEREMAIL.Text)
            lbITEMCART2.Value = lbITEMCART2.Value & IIf(mlOBJGF.IsNone(lbITEMCART2.Value) = False, "#", lbITEMCART2.Value) & Trim(txUSERID.Text) & "-" & Trim(txUSERDESC.Text) & "-" & Trim(txUSEREMAIL.Text)

            txUSERDESC.Text = ""
            btITEMKEYADD.Visible = False
        End If
    End Sub

    Protected Sub btCLEARCART_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btCLEARCART.Click
        lbITEMCART.Text = ""
        txUSERDESC.Text = ""
    End Sub

    ''
    Protected Sub btUSERID_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btUSERID.Click
        If mlOBJGF.IsNone(Trim(txUSERID.Text)) = False Then
            txUSERDESC.Text = mlOBJGS.ADGeneralLostFocus("USER", Trim(txUSERID.Text))
            If txUSERDESC.Text <> "" Then btITEMKEYADD.Visible = True
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
            If txUSERDESC.Text <> "" Then btITEMKEYADD.Visible = True
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


    Sub LoadComboData()
        ddTYPE.Items.Clear()
        ddTYPE.Items.Add("Pilih")
        mlSQLTEMP = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID='FS_GroupFile'"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISS")
        While mlRSTEMP.Read
            ddTYPE.Items.Add(Trim(mlRSTEMP("LinCode")))
        End While
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
            mlKEY = Trim(txDOCUMENTNO.Text)
            Sql_1(mlSPTYPE, mlKEY)
        Catch ex As Exception
        End Try

        mlSYSCODE.Value = ""
        ClearFields()
        DisableCancel()
    End Sub


    Sub ClearSession()
        Session.Remove("fileupload1")
        Session.Remove("fileupload2")
        Session.Remove("fileupload3")
        Session.Remove("fileupload4")
        Session.Remove("fileupload5")
    End Sub


    Function ValidateForm() As String
        ValidateForm = ""

        If ddTYPE.Text = "Pilih" Then
            ValidateForm = ValidateForm & " <br /> Please choose your Group of File "
        End If

        If FileUpload1.HasFile = True Then Session("fileupload1") = FileUpload1
        If Session("fileupload1") IsNot Nothing And FileUpload1.HasFile = False Then FileUpload1 = Session("fileupload1")
        If FileUpload2.HasFile = True Then Session("fileupload2") = FileUpload2
        If Session("fileupload2") IsNot Nothing And FileUpload2.HasFile = False Then FileUpload1 = Session("fileupload2")
        If FileUpload3.HasFile = True Then Session("fileupload3") = FileUpload3
        If Session("fileupload3") IsNot Nothing And FileUpload3.HasFile = False Then FileUpload3 = Session("fileupload3")
        If FileUpload4.HasFile = True Then Session("fileupload4") = FileUpload4
        If Session("fileupload4") IsNot Nothing And FileUpload4.HasFile = False Then FileUpload4 = Session("fileupload4")
        If FileUpload5.HasFile = True Then Session("fileupload5") = FileUpload5
        If Session("fileupload5") IsNot Nothing And FileUpload5.HasFile = False Then FileUpload5 = Session("fileupload5")

        If (FileUpload1.HasFile = False And FileUpload2.HasFile = False And FileUpload3.HasFile = False And FileUpload4.HasFile = False And FileUpload5.HasFile = False) Then
            ValidateForm = ValidateForm & " <br /> Files are not found"
        End If


        If FileUpload1.HasFile = True Then
            If txFILEUPLOAD1_N.Text = "" Then
                ValidateForm = ValidateForm & " <br /> File Description empty on File 1"
            End If
        End If

        If FileUpload2.HasFile = True Then
            If txFILEUPLOAD2_N.Text = "" Then
                ValidateForm = ValidateForm & " <br /> File Description empty on File 2"
            End If
        End If

        If FileUpload3.HasFile = True Then
            If txFILEUPLOAD3_N.Text = "" Then
                ValidateForm = ValidateForm & " <br /> File Description empty on File 3"
            End If
        End If

        If FileUpload4.HasFile = True Then
            If txFILEUPLOAD4_N.Text = "" Then
                ValidateForm = ValidateForm & " <br /> File Description empty on File 4"
            End If
        End If

        If FileUpload5.HasFile = True Then
            If txFILEUPLOAD5_N.Text = "" Then
                ValidateForm = ValidateForm & " <br /> File Description empty on File 5"
            End If
        End If

    End Function


    Sub Sql_1(ByVal mpSTATUS As String, ByVal mpCODE As String)
        Dim mlSTATUS As String
        Dim mlNEWRECORD As Boolean

        Try
            mlNEWRECORD = False


            Dim mlFOLDERPATH As String
            Dim mlFOLDERPATH2 As String
            Dim mlPATHDESTDEFAULT As String
            Dim mlFILENAME As String
            Dim mlFILEPATH As String
            Dim mlFOLDERNAMERND As String
            Dim mlLOOP As Boolean

            Dim mlFILENAME1 As String
            Dim mlFILEPATH1 As String
            Dim mlFILEDESC1 As String
            Dim mlFILEUSERID1 As String
            Dim mlFILEPASSWORD1 As String

            Dim mlPROCESSID As String
            Dim mlPROCESS_SUBJECT As String
            Dim mlPROCESS_DESC As String
            Dim mlLINE As String
            Dim mlITEMKEY2 As String
            Dim mlITEMDESC2 As String
            Dim mlFIRST As Boolean
            Dim mlUSERID2 As String

            mlMESSAGE.Text = ""


            Select Case mpSTATUS
                Case "Edit", "Delete"
                    mlSQL = ""
                    mlSQL = mlSQL & mlOBJPJ.XM_FILEHR_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
            End Select


            Select Case mpSTATUS
                Case "New"
                    mlNEWRECORD = True
                    If txDOCUMENTNO.Text = "--AUTONUMBER--" Then
                        txDOCUMENTNO.Text = mlKEY
                    Else
                        mlKEY = Trim(txDOCUMENTNO.Text)
                    End If

                Case "Edit"
                    mlSTATUS = "Edit"
                    mlNEWRECORD = True
                    mlSQL = mlSQL & " DELETE FROM XM_FILEHR WHERE DocNo = '" & mlKEY & "';"

                Case "Delete"
                    mlSTATUS = "Delete"
                    mlSQL = mlSQL & " UPDATE XM_FILEHR SET RecordStatus='Delete' WHERE DocNo = '" & mlKEY & "';"
            End Select
            If mlOBJGF.IsNone(mlSQL) = False Then mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
            mlSQL = ""




            mlUSERID2 = ""
            mlFILENAME1 = ""
            mlFILEPATH1 = ""
            mlFILEDESC1 = ""
            mlFILEUSERID1 = ""
            mlFILEPASSWORD1 = ""

            mlLOOP = True
            mlFOLDERPATH = ""
            mlPATHDESTDEFAULT = ""
            mlFOLDERNAMERND = ""
            mlI = 0

            Do While mlLOOP = True
                mlFOLDERNAMERND = mlOBJGF.GetRandomPasswordUsingGUID(8)
                mlFOLDERNAMERND = "ud_" & mlFOLDERNAMERND
                mlFOLDERPATH = mlPATHNORMAL & mlFOLDERNAMERND
                mlFOLDERPATH = Server.MapPath(mlFOLDERPATH)
                If mlOBJFS.Folder_Exists(mlFOLDERPATH) = True Then
                    mlLOOP = True
                Else
                    mlOBJFS.Folder_New(mlFOLDERPATH)
                    mlLOOP = False
                End If
            Loop

            Select Case mpSTATUS
                Case "New"
                    If txDOCUMENTNO.Text = "--AUTONUMBER--" Then
                        mlKEY = mlOBJGS.GetModuleCounter("FS_UPLOAD_" & mlFUNCTIONPARAMETER, "00000000")
                    End If
            End Select

            Try
                If FileUpload1.HasFile Then
                    mlFILENAME = FileUpload1.FileName
                    mlFILENAME = mlKEY & "_" & mlFILENAME
                    mlFILEPATH = mlFOLDERPATH & "/" & mlFILENAME
                    FileUpload1.SaveAs(mlFILEPATH)

                    mlFILEPATH1 = mlPATHNORMAL2 & mlFOLDERNAMERND & "/" & mlFILENAME
                    mlFILENAME1 = mlFILENAME
                    mlFILEDESC1 = Trim(txFILEUPLOAD1_N.Text)
                    mlFILEUSERID1 = ""
                    mlFILEPASSWORD1 = Trim(txOPENPWD.Text)

                    mlI = mlI + 1
                    mlSQL = mlSQL & " INSERT INTO XM_FILEDT " & _
                                " (DocNo,Linno,FilePath,FileName,FileDesc,FileUserID,FilePassword) VALUES ( " & _
                                " '" & mlKEY & "','" & mlI & "'," & _
                                " '" & mlFILEPATH1 & "','" & mlFILENAME1 & "','" & mlFILEDESC1 & "'," & _
                                " '" & mlFILEUSERID1 & "','" & mlFILEPASSWORD1 & "');"

                End If
            Catch ex As Exception

            End Try

            Try
                If FileUpload2.HasFile Then
                    mlFILENAME = FileUpload2.FileName
                    mlFILENAME = mlKEY & "_" & mlFILENAME
                    mlFILEPATH = mlFOLDERPATH & "/" & mlFILENAME
                    FileUpload2.SaveAs(mlFILEPATH)

                    mlFILEPATH1 = mlPATHNORMAL2 & mlFOLDERNAMERND & "/" & mlFILENAME
                    mlFILENAME1 = mlFILENAME
                    mlFILEDESC1 = Trim(txFILEUPLOAD2_N.Text)
                    mlFILEUSERID1 = ""
                    mlFILEPASSWORD1 = Trim(txOPENPWD.Text)

                    mlI = mlI + 1
                    mlSQL = mlSQL & " INSERT INTO XM_FILEDT " & _
                                " (DocNo,Linno,FilePath,FileName,FileDesc,FileUserID,FilePassword) VALUES ( " & _
                                " '" & mlKEY & "','" & mlI & "'," & _
                                " '" & mlFILEPATH1 & "','" & mlFILENAME1 & "','" & mlFILEDESC1 & "'," & _
                                " '" & mlFILEUSERID1 & "','" & mlFILEPASSWORD1 & "');"
                End If
            Catch ex As Exception
            End Try

            Try
                If FileUpload3.HasFile Then
                    mlFILENAME = FileUpload3.FileName
                    mlFILENAME = mlKEY & "_" & mlFILENAME
                    mlFILEPATH = mlFOLDERPATH & "/" & mlFILENAME
                    FileUpload3.SaveAs(mlFILEPATH)


                    mlFILEPATH1 = mlPATHNORMAL2 & mlFOLDERNAMERND & "/" & mlFILENAME
                    mlFILENAME1 = mlFILENAME
                    mlFILEDESC1 = Trim(txFILEUPLOAD3_N.Text)
                    mlFILEUSERID1 = ""
                    mlFILEPASSWORD1 = Trim(txOPENPWD.Text)

                    mlI = mlI + 1
                    mlSQL = mlSQL & " INSERT INTO XM_FILEDT " & _
                                " (DocNo,Linno,FilePath,FileName,FileDesc,FileUserID,FilePassword) VALUES ( " & _
                                " '" & mlKEY & "','" & mlI & "'," & _
                                " '" & mlFILEPATH1 & "','" & mlFILENAME1 & "','" & mlFILEDESC1 & "'," & _
                                " '" & mlFILEUSERID1 & "','" & mlFILEPASSWORD1 & "');"

                End If
            Catch ex As Exception
            End Try

            Try
                If FileUpload4.HasFile Then
                    mlFILENAME = FileUpload4.FileName
                    mlFILEPATH = mlFOLDERPATH & "/" & mlFILENAME
                    FileUpload4.SaveAs(mlFILEPATH)


                    mlFILEPATH1 = mlPATHNORMAL2 & mlFOLDERNAMERND & "/" & mlFILENAME
                    mlFILENAME1 = mlFILENAME
                    mlFILEDESC1 = Trim(txFILEUPLOAD4_N.Text)
                    mlFILEUSERID1 = ""
                    mlFILEPASSWORD1 = Trim(txOPENPWD.Text)

                    mlI = mlI + 1
                    mlSQL = mlSQL & " INSERT INTO XM_FILEDT " & _
                                " (DocNo,Linno,FilePath,FileName,FileDesc,FileUserID,FilePassword) VALUES ( " & _
                                " '" & mlKEY & "','" & mlI & "'," & _
                                " '" & mlFILEPATH1 & "','" & mlFILENAME1 & "','" & mlFILEDESC1 & "'," & _
                                " '" & mlFILEUSERID1 & "','" & mlFILEPASSWORD1 & "');"


                End If
            Catch ex As Exception
            End Try

            Try
                If FileUpload5.HasFile Then
                    mlFILENAME = FileUpload5.FileName
                    mlFILENAME = mlKEY & "_" & mlFILENAME
                    mlFILEPATH = mlFOLDERPATH & "/" & mlFILENAME
                    FileUpload5.SaveAs(mlFILEPATH)


                    mlFILEPATH1 = mlPATHNORMAL2 & mlFOLDERNAMERND & "/" & mlFILENAME
                    mlFILENAME1 = mlFILENAME
                    mlFILEDESC1 = Trim(txFILEUPLOAD5_N.Text)
                    mlFILEUSERID1 = ""
                    mlFILEPASSWORD1 = Trim(txOPENPWD.Text)

                    mlI = mlI + 1
                    mlSQL = mlSQL & " INSERT INTO XM_FILEDT " & _
                                " (DocNo,Linno,FilePath,FileName,FileDesc,FileUserID,FilePassword) VALUES ( " & _
                                " '" & mlKEY & "','" & mlI & "'," & _
                                " '" & mlFILEPATH1 & "','" & mlFILENAME1 & "','" & mlFILEDESC1 & "'," & _
                                " '" & mlFILEUSERID1 & "','" & mlFILEPASSWORD1 & "');"
                End If
            Catch ex As Exception
            End Try


            If mlNEWRECORD = True Then
                mlSQL = mlSQL & " INSERT INTO XM_FILEHR " & _
                            " (ParentCode,SysID,DocNo,DocDate,GroupID,Description,RandomStr," & _
                            " RecordStatus,RecUserID,RecDate) VALUES ( " & _
                            " '" & mlFUNCTIONPARAMETER & "','','" & mlKEY & "','" & mlOBJGF.FormatDate(Now) & "'," & _
                            " '" & Trim(ddTYPE.Text) & "', '" & Trim(txDESCRIPTION.Text) & "','" & mlFOLDERNAMERND & "'," & _
                            " 'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "');"

                mlI = 1
                mlPROCESSID = "Upload"
                mlPROCESS_SUBJECT = "Upload " & Trim(ddTYPE.Text) & ", Document No:" & mlKEY
                mlPROCESS_DESC = ""

                mlSQL = mlSQL & " INSERT INTO XM_FILEDTU " & _
                            " (DocNo,Linno,Type,UserID,Name,TaskID,Description," & _
                            " RecordStatus,RecUserID,RecDate) VALUES ( " & _
                            " '" & mlKEY & "','" & mlI & "','1'," & _
                            " '" & Session("mgUSERID") & "','" & Session("mgNAME") & "', '" & mlPROCESSID & " ','" & mlPROCESS_SUBJECT & "'," & _
                            " 'New','" & Session("mgUSERID") & "','" & Now & "');"


                Dim mlINBOX_DOCNO As String
                Dim mlINBOXNO_STATUS As Boolean
                mlINBOX_DOCNO = ""
                mlINBOXNO_STATUS = False
                mlLOOP = True

                mlI = 0
                Do While mlLOOP
                    mlLINE = mlOBJGF.GetStringAtPosition(lbITEMCART2.Value, mlI, "#")
                    If mlLINE = "" Then
                        mlLOOP = False
                    Else
                        If mlINBOXNO_STATUS = False Then
                            mlINBOX_DOCNO = mlOBJGS.GetModuleCounter("XM_INBOX", "00000000")
                            mlINBOXNO_STATUS = True
                        End If
                        mlPROCESSID = "Upload"
                        mlPROCESS_SUBJECT = "Ask for Download " & Trim(ddTYPE.Text) & ", Document No:" & mlKEY
                        mlPROCESS_DESC = ""

                        mlITEMKEY2 = mlOBJGF.GetStringAtPosition(mlLINE, 0, "-")
                        mlITEMDESC2 = mlOBJGF.GetStringAtPosition(mlLINE, 1, "-")
                        mlSQL = mlSQL & mlOBJPJ.XM_INBOX(mlFUNCTIONPARAMETER, "FS", mlINBOX_DOCNO, mlFUNCTIONPARAMETER, mlKEY, Now, Session("mgUSERID"), Session("mgNAME"), mlITEMKEY2, mlITEMDESC2, mlPROCESSID, mlPROCESS_SUBJECT, mlPROCESS_DESC)
                        mlSQL = mlSQL & mlOBJPJ.XM_Inbox_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))

                        mlI = mlI + 1
                        mlUSERID2 = mlUSERID2 & IIf(mlOBJGF.IsNone(mlUSERID2) = False, ",", "") & "'" & mlITEMKEY2 & "'"
                        mlSQL = mlSQL & " INSERT INTO XM_FILEDTU " & _
                                    " (DocNo,Linno,Type,UserID,Name," & _
                                    " TaskID,Description," & _
                                    " RecordStatus,RecUserID,RecDate) VALUES ( " & _
                                    " '" & mlKEY & "','" & mlI + 1 & "','1'," & _
                                    " '" & mlITEMKEY2 & "','" & mlITEMDESC2 & "', '" & mlPROCESSID & " ','" & mlPROCESS_SUBJECT & "'," & _
                                    " 'New','" & Session("mgUSERID") & "','" & Now & "');"
                    End If
                Loop
            End If

            Select Case mpSTATUS
                Case "New"
                    mlSQL = mlSQL & mlOBJPJ.XM_FILEHR_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
                    mlMESSAGE.Text = "Upload File Successfull with Document No : " & mlKEY
            End Select
            mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
            mlSQL = ""





            mlSQLTEMP = "SELECT * FROM XM_FILEDTU WHERE DocNo = '" & mlKEY & "'"
            mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
            If mlRSTEMP.HasRows Then

                Select Case mpSTATUS
                    Case "New"
                        Try

                            Dim mlOBJPJ_UT As New IASClass_Local_ut.ucmLOCAL_ut
                            Dim mlEMAIL_STATUS As String
                            Dim mlEMAIL_TO As String
                            Dim mlEMAIL_SUBJECT As String
                            Dim mlEMAIL_BODY As String
                            Dim mlLINKSERVER1 As String
                            Dim mlRECEIVER As String

                            mlRECEIVER = ""
                            mlEMAIL_TO = ""
                            mlLINKSERVER1 = System.Configuration.ConfigurationManager.AppSettings("mgLINKEDSERVER1")

                            mlSQLTEMP = "SELECT UserID,Name,EmailAddr FROM " & mlLINKSERVER1 & "AD_USERPROFILE  WHERE UserID IN (" & mlUSERID2 & ")"
                            mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "AD", "AD")
                            While mlRSTEMP.Read
                                mlRECEIVER = Trim(mlRSTEMP("UserID")) & " - " & Trim(mlRSTEMP("Name"))
                                mlEMAIL_TO = mlEMAIL_TO & IIf(mlOBJGF.IsNone(Trim(mlEMAIL_TO)) = True, "", ",") & mlRSTEMP("EmailAddr") & ""
                            End While

                            mlEMAIL_TO = IIf(mlOBJGF.IsNone(Trim(mlEMAIL_TO)) = True, "", mlEMAIL_TO & ",")
                            If mlOBJGF.IsNone(Trim(mlEMAIL_TO)) = False Then
                                mlEMAIL_SUBJECT = "" & " Upload File untuk " & Trim(ddTYPE.Text)
                                mlEMAIL_BODY = ""
                                mlEMAIL_BODY = mlEMAIL_BODY & "Dear : " & mlRECEIVER
                                mlEMAIL_BODY = mlEMAIL_BODY & "<br><br>"
                                mlEMAIL_BODY = mlEMAIL_BODY & "Anda Mempunyai File untuk di Download "
                                mlEMAIL_BODY = mlEMAIL_BODY & "<br><br>"
                                mlEMAIL_BODY = mlEMAIL_BODY & "<table border=0>"
                                mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                                mlEMAIL_BODY = mlEMAIL_BODY & "Tanggal	</td><td valign=top>:</td><td valign=top>" & Now
                                mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                                mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                                mlEMAIL_BODY = mlEMAIL_BODY & "Jenis(Transaksi) </td><td valign=top>:</td><td valign=top>" & "Upload " & Trim(ddTYPE.Text)
                                mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                                mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                                mlEMAIL_BODY = mlEMAIL_BODY & "No Dokumen  </td><td valign=top>:</td><td valign=top>" & mlKEY
                                mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                                mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                                mlEMAIL_BODY = mlEMAIL_BODY & "Open File Password  </td><td valign=top>:</td><td valign=top>" & Trim(txOPENPWD.Text)
                                mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                                mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                                mlEMAIL_BODY = mlEMAIL_BODY & "Keterangan  </td><td valign=top>:</td><td valign=top>" & Trim(txDESCRIPTION.Text)
                                mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                                mlEMAIL_BODY = mlEMAIL_BODY & "</table>"
                                mlEMAIL_BODY = mlEMAIL_BODY & "<br>Terima Kasih"
                                mlEMAIL_BODY = mlEMAIL_BODY & "<br><br><i>Email ini dikirim Otomatis oleh Sistem Komputer, Email ini tidak perlu dibalas/</i>"
                                mlEMAIL_BODY = mlEMAIL_BODY & "<br><i>This is an automatically generated email by computer system, please do not reply </i>"
                                mlEMAIL_STATUS = mlOBJPJ_UT.Sendmail_1("1", mlEMAIL_TO, "", "", mlEMAIL_SUBJECT, mlEMAIL_BODY)
                            End If

                        Catch ex As Exception
                        End Try
                End Select

            End If

            ClearFields()
            DisableCancel()
            RetrieveFieldsDetail("")
            ClearSession()

        Catch ex As Exception
            mlOBJGS.XMtoLog("", "", "" & mlKEY, Err.Description, "New", Session("mgUSERID"), mlOBJGF.FormatDate(Now))
        End Try
    End Sub

    Public Sub New()

    End Sub
End Class
