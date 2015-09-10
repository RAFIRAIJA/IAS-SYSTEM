Imports System
Imports System.Data
Imports System.Data.OleDb



Partial Class cm_bo_template1
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJPJ As New FunctionLocal

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
    Dim mlTYPE2 As String
    Dim mlSYSID As String




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "HCD Media Board Entry V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "HCD Media Board Entry V2.00"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")


        If mlFUNCTIONPARAMETER = "" Then mlFUNCTIONPARAMETER = "1"
        If Page.IsPostBack = False Then
            pnSEARCHUSERID.Visible = False
            ClearFields()
            DisableCancel()
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "", "", "")
        Else
        End If
    End Sub

    Protected Sub mlDATAGRID1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRID1.ItemCommand
        mlKEY = e.CommandArgument
        Select Case e.CommandName
            Case "BrowseRecord"
                mlMESSAGE.Text = "View Request for " & e.CommandArgument
                RetrieveFields()
                pnFILL.Visible = True
                pnFILL1.Visible = True
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
                mlMESSAGE.Text = "View Request for " & e.CommandArgument
                RetrieveFields()
                pnFILL.Visible = True
                pnFILL2.Visible = True
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

    Protected Sub mlDATAGRID3_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRID3.ItemCommand
        mlKEY = e.CommandArgument
        Select Case e.CommandName
            Case "BrowseRecord"
                mlMESSAGE.Text = "View Request for " & e.CommandArgument
                RetrieveFields()
                pnFILL.Visible = True
                pnFILL3.Visible = True
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

        'If pnFILL.Visible = False Then
        '    ClearFields()
        '    EnableCancel()
        '    pnFILL.Visible = True
        '    Exit Sub
        'End If

        Try
            mlSQL = ""
            RetrieveFieldsDetail(mlSQL)



            'If mlOBJGF.IsNone(mlSQL) = False Then
            '    Try
            '        RetrieveFieldsDetail(mlSQL)
            '    Catch ex As Exception
            '    End Try
            'End If


        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btSearchRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSearchRecord.Click
        SearchRecord()
    End Sub


    Public Sub RetrieveFields()
        DisableCancel()

        mlSQL = "SELECT * FROM HC_MEDIA_DISPLAYHR WHERE DocNo = '" & Trim(mlKEY) & "'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISS")
        If mlREADER.HasRows Then
            mlREADER.Read()
            txDOCDATE1.Text = IIf(mlOBJGF.IsNone(mlREADER("DocDate1")), "", mlOBJGF.ConvertDateIntltoIDN(mlREADER("DocDate1"), "/") & "")
            txDOCDATE2.Text = IIf(mlOBJGF.IsNone(mlREADER("DocDate2")), "", mlOBJGF.ConvertDateIntltoIDN(mlREADER("DocDate2"), "/") & "")
            txDOCUMENTNO.Text = mlREADER("DocNo") & ""
            txTIME1.Text = mlREADER("Time1") & ""
            txTIME2.Text = mlREADER("Time2") & ""
            txDESC.Text = mlREADER("Description") & ""
            txLOC.Text = mlREADER("Location") & ""
            txUSERID.Text = mlREADER("UserID") & ""
            txUSERDESC.Text = mlREADER("UserName") & ""
            txMARQUEE.Text = mlREADER("Marquee") & ""
            txFILEPATH1_N.Text = mlREADER("FilePath") & ""
            txFILEUPLOAD1_N.Text = mlREADER("FileName") & ""

        End If
    End Sub

    Sub RetrieveFieldsDetail(ByVal mpSQL As String)
        Try


            Select Case UCase(mlTYPE.Value)
                Case "S"
                    If mpSQL = "" Then
                        mlSQL2 = "SELECT " & _
                            " DocNo,DocDate," & _
                            " Time1,Time2," & _
                            " UserID,UserName,Location,Description," & _
                            " RecordStatus,RecUserID, RecName, RecDate" & _
                            " FROM HC_MEDIA_DISPLAYHR WHERE Type='" & UCase(mlTYPE.Value) & "' " & _
                            " AND DocDate1 >= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE1.Text, "/")) & "'" & _
                            " AND DocDate2 <= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE2.Text, "/")) & "'" & _
                            " AND RecordStatus='New' ORDER BY DocNo"
                    Else
                        mlSQL2 = mpSQL
                    End If
                    mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISS")

                    mlDATAGRID1.DataSource = mlREADER2
                    mlDATAGRID1.DataBind()

                Case "M"
                    If mpSQL = "" Then
                        mlSQL2 = "SELECT " & _
                            " DocNo,DocDate," & _
                            " Marquee," & _
                            " RecordStatus,RecUserID, RecName, RecDate" & _
                            " FROM HC_MEDIA_DISPLAYHR WHERE Type='" & UCase(mlTYPE.Value) & "' " & _
                            " AND DocDate1 >= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE1.Text, "/")) & "'" & _
                            " AND DocDate2 <= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE2.Text, "/")) & "'" & _
                            " AND RecordStatus='New' ORDER BY DocNo"
                    Else
                        mlSQL2 = mpSQL
                    End If
                    mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISS")

                    mlDATAGRID2.DataSource = mlREADER2
                    mlDATAGRID2.DataBind()

                Case "V"
                    If mpSQL = "" Then
                        mlSQL2 = "SELECT " & _
                            " DocNo,DocDate," & _
                            " FilePath,FileName," & _
                            " RecordStatus,RecUserID, RecName, RecDate" & _
                            " FROM HC_MEDIA_DISPLAYHR WHERE Type='" & UCase(mlTYPE.Value) & "' " & _
                            " AND DocDate1 >= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE1.Text, "/")) & "'" & _
                            " AND DocDate2 <= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE2.Text, "/")) & "'" & _
                            " AND RecordStatus='New' ORDER BY DocNo"
                    Else
                        mlSQL2 = mpSQL
                    End If
                    mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISS")

                    mlDATAGRID3.DataSource = mlREADER2
                    mlDATAGRID3.DataBind()
            End Select
            ControlPanel(False, True)

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

    End Sub

    Sub NewRecord()
        mlOBJGS.mgNEW = True
        mlOBJGS.mgEDIT = False
        EnableCancel()
        ControlPanel(True, True)

        ClearFields()
        LoadComboData()
        txDOCUMENTNO.Text = "--AUTONUMBER--"
        mlOBJPJ.SetTextBox(True, txDOCUMENTNO)
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
        pnFILL1.Visible = False
        pnFILL2.Visible = False
        pnFILL3.Visible = False
        trDOCNO.Visible = False


        mlOBJPJ.SetTextBox(True, txDOCUMENTNO)
        mlOBJPJ.SetTextBox(False, txDOCDATE1)
        mlOBJPJ.SetTextBox(False, txDOCDATE2)
        mlOBJPJ.SetTextBox(False, txTIME1)
        mlOBJPJ.SetTextBox(False, txTIME2)
        mlOBJPJ.SetTextBox(False, txDESC)
        mlOBJPJ.SetTextBox(False, txLOC)
        mlOBJPJ.SetTextBox(False, txUSERID)
        mlOBJPJ.SetTextBox(False, txMARQUEE)
        mlOBJPJ.SetTextBox(True, txFILEPATH1_N)
        mlOBJPJ.SetTextBox(False, txFILEUPLOAD1_N)

    End Sub

    Private Sub DisableCancel()
        btNewRecord.Visible = True
        btSaveRecord.Visible = False
        pnFILL.Visible = True
        pnFILL1.Visible = False
        pnFILL2.Visible = False
        pnFILL3.Visible = False
        trDOCNO.Visible = False

        mlOBJPJ.SetTextBox(True, txDOCUMENTNO)
        mlOBJPJ.SetTextBox(False, txDOCDATE1)
        mlOBJPJ.SetTextBox(False, txDOCDATE2)
        mlOBJPJ.SetTextBox(True, txTIME1)
        mlOBJPJ.SetTextBox(True, txTIME2)
        mlOBJPJ.SetTextBox(True, txDESC)
        mlOBJPJ.SetTextBox(True, txLOC)
        mlOBJPJ.SetTextBox(True, txUSERID)
        mlOBJPJ.SetTextBox(True, txMARQUEE)
        mlOBJPJ.SetTextBox(True, txFILEPATH1_N)
        mlOBJPJ.SetTextBox(True, txFILEUPLOAD1_N)
        
    End Sub

    Sub ClearFields()
        txDOCDATE1.Text = mlCURRENTDATE
        txDOCDATE2.Text = mlCURRENTDATE
        txDOCUMENTNO.Text = ""
        txTIME1.Text = ""
        txTIME2.Text = ""
        txDESC.Text = ""
        txLOC.Text = ""
        txUSERID.Text = ""
        txUSERDESC.Text = ""
        txMARQUEE.Text = ""
        txFILEPATH1_N.Text = ""
        txFILEUPLOAD1_N.Text = ""
    End Sub


    Sub LoadComboData()
        
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

    Function ValidateForm() As String
        ValidateForm = ""

        If mlOBJGS.mgNEW = True Then

        End If

    End Function

    '''
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


    Protected Sub btVideo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btVideo.Click
        mlTYPE.Value = "V"
        RetrieveFieldsDetail("")
    End Sub


    Protected Sub btSchedule_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSchedule.Click
        mlTYPE.Value = "S"
        RetrieveFieldsDetail("")
    End Sub


    Protected Sub btMarquee_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btMarquee.Click
        mlTYPE.Value = "M"
        RetrieveFieldsDetail("")
    End Sub

    Sub ControlPanel(ByVal mpSHOWFILL As Boolean, ByVal mpSHOWGRID As Boolean)
        Select Case UCase(mlTYPE.Value)
            Case "S"
                If mpSHOWFILL = True Then
                    pnFILL1.Visible = True
                    pnFILL2.Visible = False
                    pnFILL3.Visible = False
                End If

                If mpSHOWGRID = True Then
                    pnGRID1.Visible = True
                    pnGRID2.Visible = False
                    pnGRID3.Visible = False
                End If

            Case "M"
                If mpSHOWFILL = True Then
                    pnFILL1.Visible = False
                    pnFILL2.Visible = True
                    pnFILL3.Visible = False
                End If

                If mpSHOWGRID = True Then
                    pnGRID1.Visible = False
                    pnGRID2.Visible = True
                    pnGRID3.Visible = False
                End If
            Case "V"
                If mpSHOWFILL = True Then
                    pnFILL1.Visible = False
                    pnFILL2.Visible = False
                    pnFILL3.Visible = True
                End If

                If mpSHOWGRID = True Then
                    pnGRID1.Visible = False
                    pnGRID2.Visible = False
                    pnGRID3.Visible = True
                End If
        End Select

    End Sub

    Sub Sql_1(ByVal mpSTATUS As String, ByVal mpCODE As String)
        Dim mlSTATUS As String
        Dim mlNEWRECORD As Boolean

        mlSYSID = ""

        Try
            mlNEWRECORD = False

            Select Case mpSTATUS
                Case "Edit", "Delete"
                    mlSQL = ""
                    'mlSQL = mlSQL & mlOBJPJ.ISS_OP_UserSiteCard_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
            End Select

            Select Case mpSTATUS
                Case "New"
                    mlNEWRECORD = True
                    If txDOCUMENTNO.Text = "--AUTONUMBER--" Then
                        Select Case UCase(mlTYPE.Value)
                            Case "S"
                                mlKEY = mlOBJGS.GetModuleCounter("HC_MMDisplay_S" & mlFUNCTIONPARAMETER, "00000000")
                            Case "M"
                                mlKEY = mlOBJGS.GetModuleCounter("HC_MMDisplay_M" & mlFUNCTIONPARAMETER, "00000000")
                            Case "V"
                                mlKEY = mlOBJGS.GetModuleCounter("HC_MMDisplay_V" & mlFUNCTIONPARAMETER, "00000000")
                        End Select

                        txDOCUMENTNO.Text = mlKEY
                    Else
                        mlKEY = Trim(txDOCUMENTNO.Text)
                    End If

                Case "Edit"
                    mlSTATUS = "Edit"
                    mlNEWRECORD = True
                    mlSQL = mlSQL & " DELETE FROM HC_MEDIA_DISPLAYHR WHERE DocNo = '" & mlKEY & "';"

                Case "Delete"
                    mlSTATUS = "Delete"
                    mlSQL = mlSQL & " DELETE FROM  HC_MEDIA_DISPLAYHR WHERE DocNo = '" & mlKEY & "';"
            End Select
            If mlOBJGF.IsNone(mlSQL) = False Then mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISS")
            mlSQL = ""


            mlI = 0
            If mlNEWRECORD = True Then
                mlSQL = ""
                mlI = mlI + 1

                Select Case UCase(mlTYPE.Value)
                    Case "S"
                        mlSQL = mlSQL & " INSERT INTO HC_MEDIA_DISPLAYHR (ParentCode,SysID,DocNo,DocDate,Type," & _
                                " DocDate1,DocDate2,Time1,Time2," & _
                                " UserID,UserName,Location,Description," & _
                                " RecordStatus,RecUserID, RecName, RecDate) VALUES ( " & _
                                " '" & mlFUNCTIONPARAMETER & "','" & mlSYSID & "','" & mlKEY & "','" & mlOBJGF.FormatDate(Now) & "','" & Trim(mlTYPE.Value) & "'," & _
                                " '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE1.Text, "/")) & "','" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE2.Text, "/")) & "'," & _
                                " '" & Trim(txTIME1.Text) & "','" & Trim(txTIME2.Text) & "','" & Trim(txUSERID.Text) & "'," & _
                                " '" & Trim(txUSERDESC.Text) & "','" & Trim(txLOC.Text) & "','" & Trim(txDESC.Text) & "'," & _
                                " 'New','" & Session("mgUSERID") & "','" & Session("mgNAME") & "','" & mlOBJGF.FormatDate(Now) & "');"
                    Case "M"
                        mlSQL = mlSQL & " INSERT INTO HC_MEDIA_DISPLAYHR (ParentCode,SysID,DocNo,DocDate,Type," & _
                                " DocDate1,DocDate2,Marquee," & _
                                " RecordStatus,RecUserID, RecName, RecDate) VALUES ( " & _
                                " '" & mlFUNCTIONPARAMETER & "','" & mlSYSID & "','" & mlKEY & "','" & mlOBJGF.FormatDate(Now) & "','" & Trim(mlTYPE.Value) & "'," & _
                                " '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE1.Text, "/")) & "','" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE2.Text, "/")) & "'," & _
                                " '" & Trim(txMARQUEE.Text) & "'," & _
                                " 'New','" & Session("mgUSERID") & "','" & Session("mgNAME") & "','" & mlOBJGF.FormatDate(Now) & "');"
                    Case "V"
                        txFILEPATH1_N.Text = "C:\IAS\ISS\HC\"
                        mlSQL = mlSQL & " INSERT INTO HC_MEDIA_DISPLAYHR (ParentCode,SysID,DocNo,DocDate,Type," & _
                                " DocDate1,DocDate2,FilePath,FileName," & _
                                " RecordStatus,RecUserID, RecName, RecDate) VALUES ( " & _
                                " '" & mlFUNCTIONPARAMETER & "','" & mlSYSID & "','" & mlKEY & "','" & mlOBJGF.FormatDate(Now) & "','" & Trim(mlTYPE.Value) & "'," & _
                                " '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE1.Text, "/")) & "','" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE2.Text, "/")) & "'," & _
                                " '" & Trim(txFILEPATH1_N.Text) & "','" & Trim(txFILEUPLOAD1_N.Text) & "'," & _
                                " 'New','" & Session("mgUSERID") & "','" & Session("mgNAME") & "','" & mlOBJGF.FormatDate(Now) & "');"
                End Select
            End If

            Select Case mpSTATUS
                Case "New"
                    'mlSQL = mlSQL & mlOBJPJ.ISS_OP_UserSiteCard_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
            End Select
            mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISS")
            mlSQL = ""

            RetrieveFieldsDetail("")

        Catch ex As Exception
            mlSQL = Err.Description
            mlOBJGS.XMtoLog("", "", "" & mlKEY, Err.Description, "New", Session("mgUSERID"), mlOBJGF.FormatDate(Now))
        End Try
    End Sub





    
End Class
