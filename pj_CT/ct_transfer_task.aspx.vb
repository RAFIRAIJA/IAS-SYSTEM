Imports System
Imports System.Data
Imports System.Data.OleDb
Imports IAS.Core.CSCode

Partial Class ct_transfer_task
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJPJ As New ModuleFunctionLocal

    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlREADER2 As OleDb.OleDbDataReader
    Dim mlSQL2 As String
    Dim mlREADER3 As OleDb.OleDbDataReader
    Dim mlSQL3 As String
    Dim mlRSTEMP As OleDb.OleDbDataReader
    Dim mlSQLTEMP As String
    Dim mlKEY As String
    Dim mlRECORDSTATUS As String
    Dim mlSPTYPE As String
    Dim mlFUNCTIONPARAMETER As String
    Dim mlI As Integer
    Dim mlCURRENTDATE As String = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()


    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        CekSession()
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Task Move V2.03"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Task Move V2.02"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        'Session("mgUSERID") = "ADMIN"

        mlFUNCTIONPARAMETER = "1"
        If Page.IsPostBack = False Then
            tr1.Visible = False
            tr2.Visible = False
            pnSEARCHUSERID.Visible = False
            pnSEARCHCONTRACT.Visible = False
            pnNOTE.Visible = False
            pnGRID2.Visible = False
            pnGRID3.Visible = False
            DisableCancel()
            LoadComboData()
            RetrieveFieldsDetail("")

            RetrieveFieldsDetail4("")
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
                mlMESSAGE.Text = "Update Request for " & e.CommandArgument
                RetrieveFields()
                pnFILL.Visible = True

            Case "EditRecord"
                If AllowEditDelete(Session("mgUSERID"), mlKEY) = False Then
                    mlMESSAGE.Text = "you are not allowed to Edit the document of  " & e.CommandArgument
                    Exit Sub
                End If

                mlMESSAGE.Text = "Edit Request for  " & e.CommandArgument
                mlSYSCODE.Value = "edit"
                EditRecord()

            Case "DeleteRecord"
                If AllowEditDelete(Session("mgUSERID"), mlKEY) = False Then
                    mlMESSAGE.Text = "you are not allowed to Delete the document of  " & e.CommandArgument
                    Exit Sub
                End If
                mlMESSAGE.Text = "Delete Request for  " & e.CommandArgument
                mlSYSCODE.Value = "delete"
                DeleteRecord()
            
            Case Else
                ' Ignore Other
        End Select
    End Sub

    Protected Sub mlDATAGRID4_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRID4.ItemCommand
        mlKEY = e.CommandArgument
        Select Case e.CommandName
            Case "BrowseRecord"
                mlMESSAGE.Text = "Update Request for " & e.CommandArgument
                RetrieveFields()
                pnFILL.Visible = True

            Case "EditRecord"
                If AllowEditDelete(Session("mgUSERID"), mlKEY) = False Then
                    mlMESSAGE.Text = "you are not allowed to Edit the document of  " & e.CommandArgument
                    Exit Sub
                End If

                mlMESSAGE.Text = "Edit Request for  " & e.CommandArgument
                mlSYSCODE.Value = "edit"
                EditRecord()

            Case "DeleteRecord"
                If AllowEditDelete(Session("mgUSERID"), mlKEY) = False Then
                    mlMESSAGE.Text = "you are not allowed to Delete the document of  " & e.CommandArgument
                    Exit Sub
                End If
                mlMESSAGE.Text = "Delete Request for  " & e.CommandArgument
                mlSYSCODE.Value = "delete"
                DeleteRecord()

            Case Else
                ' Ignore Other
        End Select
    End Sub


    Protected Sub mlDATAGRID4_ItemBound(ByVal Source As Object, ByVal E As DataGridItemEventArgs) Handles mlDATAGRID4.ItemDataBound
        Try
            mlI = 6
            Dim mlDOCDATE6 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
            E.Item.Cells(mlI).Text = mlDOCDATE6.ToString("d")
            E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right
    
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub mlDATAGRID_ItemBound(ByVal Source As Object, ByVal E As DataGridItemEventArgs) Handles mlDATAGRID.ItemDataBound
        Try
            mlI = 5
            Dim mlDOCDATE5 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
            E.Item.Cells(mlI).Text = mlDOCDATE5.ToString("d")
            E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub mlDATAGRID2_ItemBound(ByVal Source As Object, ByVal E As DataGridItemEventArgs) Handles mlDATAGRID2.ItemDataBound
        Try
            Select Case hfTYPE.Value
                Case "note"
                    mlI = 0
                    Dim mlDOCDATE0 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlDOCDATE0.ToString("d")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                Case "ship_info"
                    mlI = 1
                    Dim mlDOCDATE1 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlDOCDATE1.ToString("d")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right


                Case "receive_info"
                    mlI = 1
                    Dim mlDOCDATE1 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlDOCDATE1.ToString("d")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

            End Select

        Catch ex As Exception

        End Try
    End Sub

    ''
    Protected Sub btSEARCHCONTRACT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHCONTRACT.Click
        If pnSEARCHCONTRACT.Visible = False Then
            pnSEARCHCONTRACT.Visible = True
        Else
            pnSEARCHCONTRACT.Visible = False
        End If
    End Sub


    Protected Sub btSEARCHCONTRACTSUBMIT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHCONTRACTSUBMIT.Click
        If mlOBJGF.IsNone(mlSEARCHCONTRACT.Text) = False Then SearchCONTRACT(1, mlSEARCHCONTRACT.Text)
    End Sub

    Protected Sub mlDATAGRIDCONTRACT_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRIDCONTRACT.ItemCommand
        Try
            txREFFNO.Text = CType(e.Item.Cells(0).Controls(0), LinkButton).Text
            lbREFFDOCNO.Text = CType(e.Item.Cells(1).Controls(0), LinkButton).Text
            mlDATAGRIDCONTRACT.DataSource = Nothing
            mlDATAGRIDCONTRACT.DataBind()
            pnSEARCHCONTRACT.Visible = False

            SearchContractInfo("1")
        Catch ex As Exception
        End Try
    End Sub

    Sub SearchCONTRACT(ByVal mpTYPE As Byte, ByVal mpNAME As String)
        Select Case mpTYPE
            Case "1"
                mlSQLTEMP = "SELECT  ContractNo as field_ID,DocNo as Field_Name FROM CT_CONTRACTHR WHERE [ContractNo] LIKE  '%" & mlSEARCHCONTRACT.Text & "%' AND RecordStatus='New'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
                mlDATAGRIDCONTRACT.DataSource = mlRSTEMP
                mlDATAGRIDCONTRACT.DataBind()
            Case "2"
                lbREFFDOCNO.Text = ""
                mlSQLTEMP = "SELECT  DocNo as Field_Name FROM CT_CONTRACTHR WHERE [ContractNo] =  '" & mpNAME & "' AND RecordStatus='New'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
                If mlRSTEMP.HasRows Then
                    mlRSTEMP.Read()
                    lbREFFDOCNO.Text = mlRSTEMP("Field_Name") & ""
                End If
                mlOBJGS.CloseFile(mlRSTEMP)
        End Select
    End Sub

    Protected Sub btREFF_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btREFF.Click
        SearchCONTRACT("2", Trim(txREFFNO.Text))
        SearchContractInfo("1")
    End Sub

    Sub SearchContractInfo(ByVal mpTYPE As String)
        mlKEY = Trim(lbREFFDOCNO.Text)

        lbCUSTL.Text = ""
        lbCUSTDESC.Text = ""
        lbSITECARDL.Text = ""
        lbSITEDESC.Text = ""
        lbPRODUCTID.text = ""
        hfFILEDOCNO.Value = ""

        Select Case mpTYPE
            Case "1"
                mlSQL = "SELECT * FROM CT_CONTRACTHR WHERE DocNo = '" & Trim(mlKEY) & "'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3")
                If mlRSTEMP.HasRows Then
                    mlRSTEMP.Read()

                    lbCUSTL.Text = mlRSTEMP("CustID") & ""
                    lbCUSTDESC.Text = mlRSTEMP("CustName") & ""
                    lbSITECARDL.Text = mlRSTEMP("SiteCardID") & ""
                    lbSITEDESC.Text = mlRSTEMP("SiteCardName") & ""
                    hfFILEDOCNO.Value = mlRSTEMP("FileDocNo") & ""
                    lbPRODUCTID.Text = mlRSTEMP("ServiceType") & ""

                    tr1.Visible = True
                    tr2.Visible = True
                End If

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
        pnGRID2.Visible = False

        tr1.Visible = False
        tr2.Visible = False
        pnSEARCHUSERID.Visible = False
        pnSEARCHCONTRACT.Visible = False
        pnNOTE.Visible = False
        pnGRID2.Visible = False
        pnGRID3.Visible = False
    End Sub


    Protected Sub btUSERID_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btUSERID.Click
        Dim mlEMAIL As String
        Try
            txUSERDESC.Text = mlOBJGS.ADGeneralLostFocus("USER", txUSERID.Text)
            mlEMAIL = mlOBJGS.ADGeneralLostFocus("EMAIL", txUSERID.Text)
            txEMAILDEST.Text = mlOBJGS.ADGeneralLostFocus("EMAIL", txUSERID.Text)

        Catch ex As Exception
        End Try

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

            SearchUser(1, txUSERID.Text)

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

                    txEMAILDEST.Text = mlOBJGS.ADGeneralLostFocus("EMAIL", txUSERID.Text)
            End Select
        Catch ex As Exception
        End Try
    End Sub


    ''

    Protected Sub btUSERIDF_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btUSERIDF.Click
        lbUSERDESCF.Text = mlOBJGS.ADGeneralLostFocus("USER", txUSERIDF.Text)
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

    Protected Sub btSearchRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSearchRecord.Click
        SearchRecord()
    End Sub


    Public Sub RetrieveFields()
        DisableCancel()

        tr1.Visible = True
        tr2.Visible = True
        mlSQL = "SELECT * FROM UT_TRANSFERTASK WHERE DocNo = '" & Trim(mlKEY) & "'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3")
        If mlREADER.HasRows Then
            mlREADER.Read()
            txDOCUMENTNO.Text = Trim(mlREADER("DocNo")) & ""
            lbLINNO.Text = Trim(mlREADER("Linno")) & ""
            txDOCDATE.Text = IIf(mlOBJGF.IsNone(mlREADER("DocDate")), "", mlOBJGF.ConvertDateIntltoIDN(mlREADER("DocDate"), "/") & "")
            txREFFNO.Text = Trim(mlREADER("ReffNo")) & ""
            lbREFFDOCNO.Text = Trim(mlREADER("ReffDocNo")) & ""
            lbCUSTL.Text = Trim(mlREADER("CustID")) & ""
            lbCUSTDESC.Text = Trim(mlREADER("CustName")) & ""
            lbSITECARDL.Text = Trim(mlREADER("SiteCardID")) & ""
            lbSITEDESC.Text = Trim(mlREADER("SiteCardName")) & ""
            txUSERIDF.Text = Trim(mlREADER("UserID1")) & ""
            lbUSERDESCF.Text = Trim(mlREADER("UserName1")) & ""
            txUSERID.Text = Trim(mlREADER("UserID2")) & ""
            txUSERDESC.Text = Trim(mlREADER("UserName2")) & ""
            txEMAILDEST.Text = Trim(mlREADER("Email2")) & ""
            txDOCDATE2.Text = IIf(mlOBJGF.IsNone(mlREADER("DeadlineDate")), "", mlOBJGF.ConvertDateIntltoIDN(mlREADER("DeadlineDate"), "/") & "")
            txDESCRIPTION.Text = Trim(mlREADER("Description")) & ""
            hfFILEDOCNO.Value = Trim(mlREADER("FileDocNo")) & ""

        End If

        RetrieveFieldsDetail3("")
        pnNOTETYPE.Visible = True
    End Sub

    Sub RetrieveFieldsDetail(ByVal mpSQL As String)
        Try

            If mpSQL = "" Then
                'mlSQL2 = "" & _
                '    " SELECT DISTINCT DocNo,DocDate,ReffNo as Contract_No,ReffDocNo as ReffNo,CustID,CustName,SiteCardID,SiteCardName " & _
                '    " FROM ( " & _
                '    " SELECT DISTINCT DocNo,DocDate,ReffNo,ReffDocNo,CustID,CustName,SiteCardID,SiteCardName FROM UT_TRANSFERTASK" & _
                '    " WHERE RecordStatus='New' AND Linno = '1' AND UserID1='" & Session("mgUSERID") & "' " & _
                '    " AND DocNo in (SELECT DISTINCT ReffDocNo FROM XM_INBOX WHERE FromID = '" & Session("mgUSERID") & "' AND SysID='CT' AND RecordStatus='New' )" & _
                '    " UNION ALL" & _
                '    " SELECT DISTINCT DocNo,DocDate,ReffNo,ReffDocNo,CustID,CustName,SiteCardID,SiteCardName FROM UT_TRANSFERTASK" & _
                '    " WHERE RecordStatus='New' AND Linno <> '1' AND UserID2='" & Session("mgUSERID") & "'" & _
                '    " AND DocNo in (SELECT DISTINCT ReffDocNo FROM XM_INBOX WHERE ToID = '" & Session("mgUSERID") & "' AND SysID='CT' AND RecordStatus='New' )" & _
                '    " ) TB1 " & _
                '    " ORDER BY DocNo"

                'mlSQL2 = "" & _
                '    " SELECT DISTINCT DocNo,DocDate,ReffNo as Contract_No,ReffDocNo as ReffNo,CustID,CustName,SiteCardID,SiteCardName " & _
                '    " FROM ( " & _
                '    " SELECT DISTINCT DocNo,DocDate,ReffNo,ReffDocNo,CustID,CustName,SiteCardID,SiteCardName FROM UT_TRANSFERTASK" & _
                '    " WHERE RecordStatus='New'  AND UserID1='" & Session("mgUSERID") & "' " & _
                '    " AND DocNo in (SELECT DISTINCT ReffDocNo FROM XM_INBOX WHERE FromID = '" & Session("mgUSERID") & "' AND SysID='CT' AND RecordStatus='New' )" & _
                '    " UNION ALL" & _
                '    " SELECT DISTINCT DocNo,DocDate,ReffNo,ReffDocNo,CustID,CustName,SiteCardID,SiteCardName FROM UT_TRANSFERTASK" & _
                '    " WHERE RecordStatus='New'  AND UserID2='" & Session("mgUSERID") & "'" & _
                '    " AND DocNo in (SELECT DISTINCT ReffDocNo FROM XM_INBOX WHERE ToID = '" & Session("mgUSERID") & "' AND SysID='CT' AND RecordStatus='New' )" & _
                '    " ) TB1 " & _
                '    " ORDER BY DocNo"

                mlSQL2 = "" & _
                    " SELECT DISTINCT DocNo,DocDate,ReffNo as Contract_No,CustName,SiteCardID,SiteCardName,ReffDocNo as ReffNo " & _
                    " FROM ( " & _
                    " SELECT DISTINCT DocNo,DocDate,ReffNo,ReffDocNo,CustID,CustName,SiteCardID,SiteCardName FROM UT_TRANSFERTASK" & _
                    " WHERE RecordStatus='New'  AND UserID2='" & Session("mgUSERID") & "'" & _
                    " AND DocNo in (SELECT DISTINCT ReffDocNo FROM XM_INBOX WHERE ToID = '" & Session("mgUSERID") & "' AND SysID='CT' AND RecordStatus='New' )" & _
                    " ) TB1 " & _
                    " ORDER BY DocNo Desc"
            Else
                mpSQL = mlSQL2
            End If
            mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISSP3")
            mlDATAGRID.DataSource = mlREADER2
            mlDATAGRID.DataBind()
            mlOBJGS.CloseFile(mlREADER2)

        Catch ex As Exception

        End Try
    End Sub

    Sub RetrieveFieldsDetail4(ByVal mpSQL As String)
        Try

            If mpSQL = "" Then

                mlSQL2 = "" & _
                    " SELECT DISTINCT DocNo,DocDate,ReffNo as Contract_No,CustName,SiteCardID,SiteCardName,ReffDocNo as ReffNo " & _
                    " FROM ( " & _
                    " SELECT DISTINCT DocNo,DocDate,ReffNo,ReffDocNo,CustID,CustName,SiteCardID,SiteCardName FROM UT_TRANSFERTASK" & _
                    " WHERE RecordStatus='New'  AND UserID1='" & Session("mgUSERID") & "' " & _
                    " ) TB1 " & _
                    " ORDER BY DocNo Desc"
            Else
                mpSQL = mlSQL2
            End If

            mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISSP3")
            mlDATAGRID4.DataSource = mlREADER2
            mlDATAGRID4.DataBind()
            mlOBJGS.CloseFile(mlREADER2)

        Catch ex As Exception


        End Try
    End Sub

    Sub RetrieveFieldsDetail2(ByVal mpSQL As String, ByVal mpTYPE As String)
        Dim mlSQLADD As String
        Try

            Select Case hfTYPE.Value
                Case "note"
                    mlSQLADD = " AND SysID = 'note' "

                    mlSQL3 = "SELECT DocDate,Description, RecUserID as UserID, RecName as UserName FROM CT_CONTRACT_TASKDESC" & _
                        " WHERE DocNo = '" & Trim(txDOCUMENTNO.Text) & "' AND RecordStatus='New'" & mlSQLADD & " ORDER BY Linno2,DocDate"
                Case "ship_info"
                    mlSQLADD = " AND SysID = 'ship_info' "

                    mlSQL3 = "SELECT Courier_DocNo as CourierNo,Courier_Date as Date,Courier_DocType as DocType,Courier_Type as CourierType," & _
                        " Courier_Name as Name,Courier_PIC_ID as PIC_ID,Courier_PIC_Name as Name,Courier_PIC_Phone as Phone," & _
                        " Courier_PIC_Pos as Position, RecUserID as User_ID,RecName as Name FROM CT_CONTRACT_TASKDESC" & _
                        " WHERE DocNo = '" & Trim(txDOCUMENTNO.Text) & "' AND RecordStatus='New'" & mlSQLADD & " ORDER BY Linno2,DocDate"

                Case "receive_info"
                    mlSQLADD = " AND SysID = 'receive_info' "

                    mlSQL3 = "SELECT Courier_DocNo as CourierNo,Courier_Date as Date,Courier_DocType as DocType,Courier_Type as CourierType," & _
                      " Courier_Name as Name,Courier_PIC_ID as PIC_ID,Courier_PIC_Name as Name,Courier_PIC_Phone as Phone," & _
                      " Courier_PIC_Pos as Position,RecUserID as User_ID,RecName as Name FROM CT_CONTRACT_TASKDESC" & _
                      " WHERE DocNo = '" & Trim(txDOCUMENTNO.Text) & "' AND RecordStatus='New'" & mlSQLADD & " ORDER BY Linno2,DocDate"

            End Select


            mlREADER3 = mlOBJGS.DbRecordset(mlSQL3, "PB", "ISSP3")

            pnGRID2.Visible = True
            mlDATAGRID2.DataSource = mlREADER3
            mlDATAGRID2.DataBind()
            mlOBJGS.CloseFile(mlREADER3)

        Catch ex As Exception
        End Try
    End Sub


    Sub RetrieveFieldsDetail3(ByVal mpSQL As String)
        Try

            If mpSQL = "" Then
                mlSQL3 = "SELECT Linno as No,UserID1 as FromUserID, UserName1 as FromName, UserID2 as ToUserID, UserName2 as ToName, " & _
                    " ReffNo as ContractNO, SiteCardID, SiteCardName, CustName, " & _
                    " DeadLineDate as Deadline, TaskStartDate as StartDate, TaskEndDate as EndDate, Description FROM UT_TRANSFERTASK" & _
                    " WHERE DocNo = '" & Trim(txDOCUMENTNO.Text) & "' AND RecordStatus='New' ORDER BY Linno"
            Else
                mpSQL = mlSQL3
            End If
            mlREADER3 = mlOBJGS.DbRecordset(mlSQL3, "PB", "ISSP3")

            pnGRID3.Visible = True
            mlDATAGRID3.DataSource = mlREADER3
            mlDATAGRID3.DataBind()
            mlOBJGS.CloseFile(mlREADER3)


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
        RetrieveFieldsDetail("")
    End Sub

    Sub NewRecord()
        mlOBJGS.mgNEW = True
        mlOBJGS.mgEDIT = False
        EnableCancel()
        ClearFields()
        LoadComboData()
        txDOCUMENTNO.Text = "--AUTONUMBER--"
        mlOBJPJ.SetTextBox(True, txDOCUMENTNO)
        tr1.Visible = False
        tr2.Visible = False
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
        mlOBJPJ.SetTextBox(False, txDOCDATE)
        mlOBJPJ.SetTextBox(False, txREFFNO)
        mlOBJPJ.SetTextBox(True, txUSERIDF)
        mlOBJPJ.SetTextBox(False, txUSERID)
        mlOBJPJ.SetTextBox(False, txEMAILDEST)
        mlOBJPJ.SetTextBox(False, txDOCDATE2)
        mlOBJPJ.SetTextBox(False, txDESCRIPTION)

        btSEARCHCONTRACT.Visible = True
        btSEARCHUSERID.Visible = True

        btDOCDATE2.Visible = False
        btDOCDATE22.Visible = False

    End Sub

    Private Sub DisableCancel()
        btNewRecord.Visible = True
        btSaveRecord.Visible = False
        pnFILL.Visible = False

        mlOBJPJ.SetTextBox(True, txDOCUMENTNO)
        mlOBJPJ.SetTextBox(True, txDOCDATE)
        mlOBJPJ.SetTextBox(True, txREFFNO)
        mlOBJPJ.SetTextBox(True, txUSERIDF)
        mlOBJPJ.SetTextBox(True, txUSERID)
        mlOBJPJ.SetTextBox(True, txEMAILDEST)
        mlOBJPJ.SetTextBox(True, txDOCDATE2)
        mlOBJPJ.SetTextBox(True, txDESCRIPTION)

        btSEARCHCONTRACT.Visible = False
        btSEARCHUSERID.Visible = False

        btDOCDATE2.Visible = False
        btDOCDATE22.Visible = False
    End Sub

    Sub ClearFields()
        txDOCUMENTNO.Text = ""
        txDOCDATE.Text = mlCURRENTDATE
        txREFFNO.Text = ""
        lbREFFDOCNO.Text = ""
        lbCUSTL.Text = ""
        lbCUSTDESC.Text = ""
        lbSITECARDL.Text = ""
        lbSITEDESC.Text = ""
        txUSERIDF.Text = Session("mgUSERID")
        lbUSERDESCF.Text = Session("mgNAME")
        txUSERID.Text = ""
        txUSERDESC.Text = ""
        txEMAILDEST.Text = ""
        txDOCDATE2.Text = ""
        txDESCRIPTION.Text = ""
        hfFILEDOCNO.Value = ""
        lbPRODUCTID.text = ""
    End Sub


    Sub ClearFields2()
        tx_COURIER_NAMER.Text = ""
        tx_DOCNOR.Text = ""
        txSENDDATER.Text = mlCURRENTDATE
        tx_COURIER_PIC_ID.Text = ""
        tx_COURIER_PIC_NAME.Text = ""
        tx_COURIER_PIC_PHONE.Text = ""
        tx_COURIER_PIC_POS.Text = ""
        txDESCRIPTIONR.Text = ""
        tx_COURIER_NAMER.Text = ""
    End Sub


    Sub LoadComboData()
        ddGROUPTASK.Items.Clear()
        ddGROUPTASK.Items.Add("Pilih")
        mlSQLTEMP = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID='GroupTask'"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISS")
        While mlRSTEMP.Read
            ddGROUPTASK.Items.Add(mlRSTEMP("LinCode") & " - " & mlRSTEMP("Description"))
        End While

        ddCOURIER_TYPE.Items.Clear()
        ddCOURIER_TYPE.Items.Add("Pilih")
        mlSQLTEMP = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID='ShipType'"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISS")
        While mlRSTEMP.Read
            ddCOURIER_TYPE.Items.Add(mlRSTEMP("Description"))
        End While

        ddDOCTYPER.Items.Clear()
        ddDOCTYPER.Items.Add("Pilih")
        mlSQLTEMP = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID='CtDocType'"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISS")
        While mlRSTEMP.Read
            ddDOCTYPER.Items.Add(mlRSTEMP("Description"))
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
        RetrieveFieldsDetail("")
        mlMESSAGE.Text = mlMESSAGE.Text & "<br>" & " Data Save Successfull with Document No " & mlKEY

    End Sub

    Function ValidateForm() As String
        ValidateForm = ""

        btUSERID_Click(Nothing, Nothing)
        btUSERIDF_Click(Nothing, Nothing)
        btSEARCHCONTRACT_Click(Nothing, Nothing)


        If lbREFFDOCNO.Text = "" Then
            ValidateForm = ValidateForm & " <br /> Contract No Not allowed empty"
        End If

        If lbCUSTL.Text = "" Or lbCUSTDESC.Text = "" Then
            ValidateForm = ValidateForm & " <br /> Customer Not allowed empty"
        End If


        If lbSITECARDL.Text = "" Or lbSITEDESC.Text = "" Then
            ValidateForm = ValidateForm & " <br /> Site Card Not allowed empty"
        End If


        'If lbUSERDESCF.Text = "" Then
        '    ValidateForm = ValidateForm & " <br /> From Employee ID Not allowed empty"
        'End If


        If txUSERDESC.Text = "" Then
            ValidateForm = ValidateForm & " <br /> To Employee ID Not allowed empty"
        End If

    End Function



    Sub Sql_Note(ByVal mpSTATUS As String, ByVal mpCODE As String)
        Dim mlLINNO2 As Double
        Dim mlLINNO1 As Double

        Try
            mlLINNO1 = Trim(lbLINNO.Text)
            mlSQLTEMP = "SELECT Max(Linno) as Linno FROM UT_TRANSFERTASK WHERE DocNo='" & mlKEY & "' AND UserID2 = '" & Session("mgUSERID") & "'"
            mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
            mlRSTEMP.Read()
            If IsDBNull(mlRSTEMP("Linno")) = False Then
                mlLINNO1 = Convert.ToInt32(mlRSTEMP("Linno"))
            End If


            mlLINNO2 = 1
            mlSQLTEMP = "SELECT Max(Linno2) as Linno2 FROM CT_CONTRACT_TASKDESC WHERE DocNo='" & mlKEY & "'"
            mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
            mlRSTEMP.Read()
            If IsDBNull(mlRSTEMP("Linno2")) = False Then
                mlLINNO2 = Convert.ToInt32(mlRSTEMP("Linno2")) + 1
            End If
            mlLINNO2 = mlLINNO2 + 100

            mlSQL = mlSQL & " INSERT INTO CT_CONTRACT_TASKDESC (ParentCode,SysID,DocNo,DocDate,Linno,Linno2,UserID2,UserName2,Description,RecName," & _
                        " Courier_DocNo, Courier_Date, Courier_DocType,Courier_Type,Courier_Name,Courier_PIC_ID,Courier_PIC_Name,Courier_PIC_Phone,Courier_PIC_Pos," & _
                        " RecordStatus,RecUserID,RecDate) VALUES ( " & _
                        " '" & mlFUNCTIONPARAMETER & "', '" & hfTYPE.Value & "', '" & mlKEY & "','" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATER.Text, "/")) & "'," & _
                        "  '" & mlLINNO1 & "', '" & mlLINNO2 & "'," & _
                        " '" & Trim(txUSERID.Text) & "', '" & Trim(txUSERDESC.Text) & "'," & _
                        " '" & Trim(txDESCRIPTIONR.Text) & "','" & Session("mgNAME") & "'," & _
                        " '" & Trim(tx_DOCNOR.Text) & "', '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txSENDDATER.Text, "/")) & "'," & _
                        " '" & Trim(ddDOCTYPER.Text) & "', '" & Trim(ddCOURIER_TYPE.Text) & "'," & _
                        " '" & Trim(tx_COURIER_NAMER.Text) & "', '" & Trim(tx_COURIER_PIC_ID.Text) & "'," & _
                        " '" & Trim(tx_COURIER_PIC_NAME.Text) & "', '" & Trim(tx_COURIER_PIC_PHONE.Text) & "'," & _
                        " '" & Trim(tx_COURIER_PIC_POS.Text) & "'," & _
                        " 'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "');"
            mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")

            RetrieveFieldsDetail2("", hfTYPE.Value)

        Catch ex As Exception
            mlOBJGS.XMtoLog("", "", "" & mlKEY, Err.Description, "New", Session("mgUSERID"), mlOBJGF.FormatDate(Now))
        End Try
    End Sub



    Function AllowEditDelete(ByVal mpUSERID As String, ByVal mpDOCNO As String) As String
        AllowEditDelete = False
        Try
            mlSQLTEMP = "SELECT UserID1 FROM UT_TRANSFERTASK WHERE DocNo = '" & mpDOCNO & "' AND Linno = '1'"
            mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
            If mlRSTEMP.HasRows Then
                mlRSTEMP.Read()
                If IsDBNull(mlRSTEMP("UserID1")) = False Then
                    If mlRSTEMP("UserID1") = Session("mgUSERID") Then
                        Return True
                    End If
                End If
            End If

        Catch ex As Exception
        End Try
    End Function

    Protected Sub btNnote_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btNNOTE.Click
        NoteType(1)
    End Sub

    Protected Sub btNSENTMAIL_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btNSENTMAIL.Click
        NoteType(2)
    End Sub

    Protected Sub btNSENTRECEIVE_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btNSENTRECEIVE.Click
        NoteType(3)
    End Sub


    Protected Sub btNTRANSFER_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btNTRANSFER.Click
        Dim mlSTATUS As Boolean
        Dim mlADDITIONAL_MSG As String

        Try
            mlADDITIONAL_MSG = ""
            mlMESSAGE.Text = ""
            mlKEY = Trim(Request.Form("rb2"))

            mlSTATUS = False
            mlSQLTEMP = "SELECT UserID1, UserName1, UserID2, UserName2 FROM UT_TRANSFERTASK WHERE DOCNO='" & mlKEY & "'  ORDER BY Linno Desc"
            mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
            If mlRSTEMP.HasRows Then
                mlRSTEMP.Read()
                If Trim(UCase(mlRSTEMP("UserID2"))) = Trim(UCase(Session("mgUSERID"))) Then
                    mlSTATUS = True
                End If
                mlADDITIONAL_MSG = ", the Task control has been transfer to " & Trim(mlRSTEMP("UserID2")) & " - " & Trim(mlRSTEMP("UserName2"))
            End If

            Response.Write("aa:" & mlRSTEMP("UserID2") & " - " & Session("mgUSERID"))

            If mlSTATUS = False Then
                mlMESSAGE.Text = "The Task is Not in Your Control Side" & mlADDITIONAL_MSG
                Exit Sub
            End If


        Catch ex As Exception

        End Try

        NoteType(4)
    End Sub


    Protected Sub btNTAKE_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btNTAKE.Click
        Dim mlSTATUS As Boolean
        Dim mlADDITIONAL_MSG As String

        Try
            mlADDITIONAL_MSG = ""
            mlMESSAGE.Text = ""
            mlKEY = Trim(Request.Form("rb2"))

            mlSTATUS = False
            mlSQLTEMP = "SELECT UserID1, UserName1, UserID2, UserName2 FROM UT_TRANSFERTASK WHERE DOCNO='" & mlKEY & "'  ORDER BY Linno Desc"
            mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
            If mlRSTEMP.HasRows Then
                mlRSTEMP.Read()
                If Trim(mlRSTEMP("UserID2")) = Session("mgUSERID") Then
                    mlSTATUS = True
                End If
            End If

            If mlSTATUS = True Then
                mlMESSAGE.Text = "The Task already in Your Control Side, Can't Take Over The Task Control"
                Exit Sub
            End If
        Catch ex As Exception

        End Try

        NoteType(5)
    End Sub


    Protected Sub btNTFINISH_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btNTFINISH.Click
        Dim mlSTATUS As Boolean
        Dim mlADDITIONAL_MSG As String

        Try
            mlADDITIONAL_MSG = ""
            mlMESSAGE.Text = ""
            mlKEY = Trim(Request.Form("rb2"))

            mlSTATUS = False
            mlSQLTEMP = "SELECT UserID1, UserName1, UserID2, UserName2 FROM UT_TRANSFERTASK WHERE DOCNO='" & mlKEY & "'  ORDER BY Linno Desc"
            mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
            If mlRSTEMP.HasRows Then
                mlRSTEMP.Read()
                If Trim(mlRSTEMP("UserID2")) = Session("mgUSERID") Then
                    mlSTATUS = True
                End If
            End If

            If mlSTATUS = True Then
                mlMESSAGE.Text = "The Task already in Your Control Side, Can't Take Over The Task Control"
                Exit Sub
            End If
        Catch ex As Exception

        End Try


    End Sub

    Sub Transfer2Task()
        txUSERIDF.Text = Session("mgUSERID")
        lbUSERDESCF.Text = Session("mgNAME")
        txUSERID.Text = ""
        txUSERDESC.Text = ""
        txEMAILDEST.Text = ""
        txDOCDATE2.Text = ""
        txDESCRIPTION.Text = ""

        mlOBJPJ.SetTextBox(False, txUSERID)
        mlOBJPJ.SetTextBox(False, txEMAILDEST)
        mlOBJPJ.SetTextBox(False, txDOCDATE2)
        mlOBJPJ.SetTextBox(False, txDESCRIPTION)
        btSEARCHUSERID.Visible = False
        btSaveRecord.Visible = True
        btNewRecord.Visible = False
    End Sub

    Sub Take2Task()
        mlSQLTEMP = "SELECT UserID1, UserName1, UserID2, UserName2 FROM UT_TRANSFERTASK WHERE DOCNO='" & mlKEY & "'  ORDER BY Linno Desc"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
        If mlRSTEMP.HasRows Then
            mlRSTEMP.Read()
            txUSERIDF.Text = Trim(mlRSTEMP("UserID2")) & ""
            lbUSERDESCF.Text = Trim(mlRSTEMP("UserName2")) & ""
            txUSERID.Text = Session("mgUSERID") & ""
            txUSERDESC.Text = Session("mgNAME") & ""
            txEMAILDEST.Text = Session("mgUSERMAIL") & ""
            txDOCDATE2.Text = IIf(mlOBJGF.IsNone(mlCURRENTDATE), "", mlOBJGF.ConvertDateIntltoIDN(mlCURRENTDATE, "/") & "")
            txDESCRIPTION.Text = ""
            btDOCDATE2.Visible = False
            btDOCDATE22.Visible = False
        End If


        mlOBJPJ.SetTextBox(False, txEMAILDEST)
        mlOBJPJ.SetTextBox(False, txDESCRIPTION)


        btSEARCHUSERID.Visible = False
        btSaveRecord.Visible = True
        btNewRecord.Visible = False
    End Sub

    Protected Sub btNOTE_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btNOTE.Click
        mlKEY = Trim(txDOCUMENTNO.Text)
        Sql_Note("", "")
        pnNOTE.Visible = False
    End Sub

    Protected Sub ddGROUPTASK_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddGROUPTASK.TextChanged
        mlSQLTEMP = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID='GroupTask'"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP)
        If mlRSTEMP.HasRows Then
            mlRSTEMP.Read()
            If mlRSTEMP("AdditionalDescription1") = "" Then
                btDOCDATE2.Visible = True
                btDOCDATE22.Visible = True
            Else
                lbLONGTASK.Text = CDbl(mlRSTEMP("AdditionalDescription1"))
                txDOCDATE2.Text = Now.Date.AddDays(CDbl(mlRSTEMP("AdditionalDescription1")))
                txDOCDATE2.Text = IIf(mlOBJGF.IsNone(txDOCDATE2.Text), "", mlOBJGF.ConvertDateIntltoIDN(txDOCDATE2.Text, "/") & "")
                btDOCDATE2.Visible = False
                btDOCDATE22.Visible = False
            End If
        End If
    End Sub

    Sub NoteType(ByVal mpTYPE As Byte)
        mlMESSAGE.Text = ""
        mlKEY = Trim(Request.Form("rb2"))
        txDOCDATER.Text = mlCURRENTDATE

        If mlKEY = "" Then
            mlMESSAGE.Text = "Please Choose the Track No"
            Exit Sub
        End If

        Select Case mpTYPE
            Case "1"
                mlMESSAGE.Text = "Create Note for  " & mlKEY
                mlSYSCODE.Value = "note"
                RetrieveFields()
                pnFILL.Visible = False
                pnNOTETYPE.Visible = True
                mlSPTYPE = "note"

                hfTYPE.Value = "note"
                ClearFields2()
                pnFILL.Visible = False

                pnNOTE.Visible = True
                trR1.Visible = True
                trR2.Visible = False
                trR3.Visible = False
                trR4.Visible = False
                trR5.Visible = False
                trR6.Visible = False
                trR7.Visible = False
                trR8.Visible = False
                trR9.Visible = False
                trR10.Visible = False
                trR11.Visible = True
                trR12.Visible = True

                pnNOTE.Focus()
                RetrieveFieldsDetail2("", hfTYPE.Value)


            Case "2"
                mlMESSAGE.Text = "Shipping Info for  " & mlKEY
                hfTYPE.Value = "ship_info"
                ClearFields2()
                pnFILL.Visible = False

                pnNOTE.Visible = True
                trR1.Visible = True
                trR2.Visible = True
                trR3.Visible = True
                trR4.Visible = True
                trR5.Visible = True
                trR6.Visible = True
                trR7.Visible = True
                trR8.Visible = True
                trR9.Visible = True
                trR10.Visible = True
                trR11.Visible = True
                trR12.Visible = True
                pnNOTE.Focus()
                RetrieveFields()
                RetrieveFieldsDetail2("", hfTYPE.Value)

            Case "3"
                mlMESSAGE.Text = "Receive Info for  " & mlKEY
                hfTYPE.Value = "receive_info"
                ClearFields2()
                pnFILL.Visible = False

                pnNOTE.Visible = True
                trR1.Visible = True
                trR2.Visible = True
                trR3.Visible = True
                trR4.Visible = True
                trR5.Visible = True
                trR6.Visible = True
                trR7.Visible = True
                trR8.Visible = True
                trR9.Visible = True
                trR10.Visible = True
                trR11.Visible = True
                trR12.Visible = True
                pnNOTE.Focus()
                RetrieveFields()
                RetrieveFieldsDetail2("", hfTYPE.Value)

            Case "4"
                mlMESSAGE.Text = "Transfer Task for  " & mlKEY
                mlSYSCODE.Value = "transfer"
                ClearFields()
                RetrieveFields()
                pnFILL.Visible = True

                Transfer2Task()
                btSEARCHUSERID.Visible = True


            Case "5"
                mlMESSAGE.Text = "Take Task for  " & mlKEY
                mlSYSCODE.Value = "take"
                ClearFields()
                RetrieveFields()
                pnFILL.Visible = True
                Take2Task()


        End Select
    End Sub


    Sub Sql_1(ByVal mpSTATUS As String, ByVal mpCODE As String)
        Dim mlSTATUS As String
        Dim mlNEWRECORD As Boolean
        Dim mlEMAILFROM As String
        Dim mlSYSID As String

        Try
            mlEMAILFROM = ""
            mlNEWRECORD = False

            Select Case mpSTATUS
                Case "Edit", "Delete"
                    mlSQL = ""
                    mlSQL = mlSQL & mlOBJPJ.ISS_UT_TRANSFERTASK_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
            End Select

            Select Case mpSTATUS
                Case "New"
                    mlNEWRECORD = True
                    If txDOCUMENTNO.Text = "--AUTONUMBER--" Then
                        mlKEY = mlOBJGS.GetModuleCounter("CT_TRACK_" & mlFUNCTIONPARAMETER, "00000000")
                        txDOCUMENTNO.Text = mlKEY
                    Else
                        mlKEY = Trim(txDOCUMENTNO.Text)
                    End If

                Case "Edit"
                    mlSTATUS = "Edit"
                    mlNEWRECORD = True
                    mlSQL = mlSQL & " DELETE FROM UT_TRANSFERTASK WHERE DocNo = '" & mlKEY & "';"

                Case "Delete"
                    mlSTATUS = "Delete"
                    mlSQL = mlSQL & " DELETE FROM UT_TRANSFERTASK WHERE DocNo = '" & mlKEY & "';"
            End Select
            If mlOBJGF.IsNone(mlSQL) = False Then mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
            mlSQL = ""


            mlI = 0
            mlSQLTEMP = "SELECT Linno FROM UT_TRANSFERTASK WHERE DocNo = '" & mlKEY & "'  ORDER BY Linno Desc"
            mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
            If mlRSTEMP.HasRows Then
                mlRSTEMP.Read()
                If IsDBNull(mlRSTEMP("Linno")) = False Then
                    mlI = CInt(mlRSTEMP("Linno"))
                End If
            End If



            If mlNEWRECORD = True Then
                mlSQL = ""
                mlI = mlI + 1
                mlSYSID = "Transfer"

                mlSQL = mlSQL & " INSERT INTO UT_TRANSFERTASK (ParentCode,SysID,DocNo,DocDate,Linno,ReffNo,ReffDocNo,CustID,CustName,SiteCardID,SiteCardName," & _
                    " UserID1,UserName1,Email1,UserID2,UserName2,Email2,DeadlineDate,Description," & _
                    " TaskStartDate,TaskEndDate,FileDocNo," & _
                    " GroupTask,LongTask, " & _
                    " RecordStatus,RecUserID,RecDate) VALUES ( " & _
                    " '" & mlFUNCTIONPARAMETER & "', '" & mlSYSID & "', '" & mlKEY & "','" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE.Text, "/")) & "'," & _
                    "  '" & Trim(mlI) & "', '" & Trim(txREFFNO.Text) & "','" & Trim(lbREFFDOCNO.Text) & "'," & _
                    "  '" & Trim(lbCUSTL.Text) & "', '" & Trim(lbCUSTDESC.Text) & "','" & Trim(lbSITECARDL.Text) & "'," & _
                    " '" & Trim(lbSITEDESC.Text) & "', '" & Trim(txUSERIDF.Text) & "','" & Trim(lbUSERDESCF.Text) & "', '" & mlEMAILFROM & "'," & _
                    " '" & Trim(txUSERID.Text) & "', '" & Trim(txUSERDESC.Text) & "','" & Trim(txEMAILDEST.Text) & "'," & _
                    " '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE2.Text, "/")) & "', '" & Trim(txDESCRIPTION.Text) & "'," & _
                    " '" & mlOBJGF.FormatDate(Now) & "','','" & hfFILEDOCNO.Value & "'," & _
                    " '" & Trim(ddGROUPTASK.Text) & "', '" & Trim(lbLONGTASK.Text) & "'," & _
                    " 'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "');"


                Dim mlINBOX_DOCNO As String
                Dim mlINBOXNO_STATUS As Boolean
                Dim mlTOUSER As String
                Dim mlTOUSERDESC As String
                Dim mlPROCESSID As String
                Dim mlPROCESS_SUBJECT As String
                Dim mlPROCESS_DESC As String

                mlINBOX_DOCNO = mlOBJGS.GetModuleCounter("XM_INBOX", "00000000")
                mlINBOXNO_STATUS = False
                mlPROCESSID = "Contract"
                mlPROCESS_SUBJECT = "Ask for Review Contract No: " & Trim(txREFFNO.Text)
                mlPROCESS_DESC = ""
                mlTOUSER = Trim(txUSERID.Text)
                mlTOUSERDESC = Trim(txUSERDESC.Text)

                mlSQL = mlSQL & " INSERT INTO XM_FILEDTU " & _
                            " (DocNo,Linno,Type,UserID,Name,TaskID,Description," & _
                            " RecordStatus,RecUserID,RecDate) VALUES ( " & _
                            " '" & hfFILEDOCNO.Value & "','" & mlI & "','1'," & _
                            " '" & Trim(txUSERID.Text) & "','" & Trim(txUSERDESC.Text) & "', '" & mlPROCESSID & " ','" & mlPROCESS_SUBJECT & "'," & _
                            " 'New','" & Session("mgUSERID") & "','" & Now & "');"

                mlSQL = mlSQL & mlOBJPJ.XM_INBOX(mlFUNCTIONPARAMETER, "CT", mlINBOX_DOCNO, mlFUNCTIONPARAMETER, mlKEY, Now, Session("mgUSERID"), Session("mgNAME"), mlTOUSER, mlTOUSERDESC, mlPROCESSID, mlPROCESS_SUBJECT, mlPROCESS_DESC)
                mlSQL = mlSQL & mlOBJPJ.XM_Inbox_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
            End If

            Select Case mpSTATUS
                Case "New"
                    mlSQL = mlSQL & mlOBJPJ.ISS_UT_TRANSFERTASK_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
            End Select


            Select Case mlSYSCODE.Value
                Case "transfer"
                    mlSQL = mlSQL & "Update XM_Inbox Set RecordStatus = 'Post' WHERE ReffDocNo='" & mlKEY & "' AND ToID = '" & Session("mgUSERID") & "';"
                    mlSQL = mlSQL & "Update XM_FILEDTU Set RecordStatus = 'Post' WHERE DocNo='" & mlKEY & "' AND UserID = '" & Session("mgUSERID") & "';"
                    mlSQL = mlSQL & "Update UT_TRANSFERTASK Set TaskEndDate = '" & mlOBJGF.FormatDate(Now) & "' WHERE DocNo='" & mlKEY & "' AND Linno = '" & lbLINNO.Text & "';"
            End Select

            mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
            'mlSQL = ""


            mlSQL = "SELECT * FROM UT_TRANSFERTASK WHERE DocNo = '" & mlKEY & "'"
            mlRSTEMP = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3")
            If mlRSTEMP.HasRows = False Then
                mpSTATUS = "FALSE"
            End If

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
                        Dim mlBCC As String

                        mlRECEIVER = ""
                        mlEMAIL_TO = ""
                        mlBCC = "iassystem_log@iss.co.id"

                        mlRECEIVER = Trim(txUSERID.Text) & " - " & Trim(txUSERDESC.Text)
                        mlEMAIL_TO = IIf(mlOBJGF.IsNone(Session("mgUSERMAIL")), "", Session("mgUSERMAIL") & ",")
                        mlEMAIL_TO = mlEMAIL_TO & Trim(txEMAILDEST.Text)
                        mlEMAIL_TO = IIf(mlOBJGF.IsNone(Trim(mlEMAIL_TO)) = True, "", mlEMAIL_TO & ",")
                        If mlOBJGF.IsNone(Trim(mlEMAIL_TO)) = False Then
                            mlEMAIL_SUBJECT = "TEST-" & " Review Task of Contract Document "
                            mlEMAIL_BODY = ""
                            mlEMAIL_BODY = mlEMAIL_BODY & "Dear : " & mlRECEIVER
                            mlEMAIL_BODY = mlEMAIL_BODY & "<br><br>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "You have Receive a Task Related to Contract Document"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<br><br>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<table border=0>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "Date	</td><td valign=top>:</td><td valign=top>" & Now
                            mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "Document No  </td><td valign=top>:</td><td valign=top>" & mlKEY
                            mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "Contract No  </td><td valign=top>:</td><td valign=top>" & Trim(txREFFNO.Text)
                            mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "Customer  </td><td valign=top>:</td><td valign=top>" & Trim(lbCUSTL.Text) & " - " & Trim(lbCUSTDESC.Text)
                            mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "Site Card  </td><td valign=top>:</td><td valign=top>" & Trim(lbSITECARDL.Text) & " - " & Trim(lbSITEDESC.Text)
                            mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "Product  </td><td valign=top>:</td><td valign=top>" & Trim(lbPRODUCTID.Text)
                            mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "Deadline  </td><td valign=top>:</td><td valign=top>" & Trim(txDOCDATE2.Text)
                            mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "Description  </td><td valign=top>:</td><td valign=top>" & Trim(txDESCRIPTION.Text)
                            mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "</table>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "Thank You"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<br>" & Session("mgUSERID") & "-" & Session("mgNAME")
                            mlEMAIL_BODY = mlEMAIL_BODY & "<br><br><i>Email ini dikirim Otomatis oleh Sistem Komputer, Email ini tidak perlu dibalas/</i>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<br><i>This is an automatically generated email by computer system, please do not reply </i>"
                            mlEMAIL_STATUS = mlOBJPJ_UT.Sendmail_1("1", mlEMAIL_TO, "", mlBCC, mlEMAIL_SUBJECT, mlEMAIL_BODY)
                        End If

                    Catch ex As Exception
                    End Try
            End Select

            RetrieveFieldsDetail("")
            RetrieveFieldsDetail3("")
            RetrieveFieldsDetail4("")

        Catch ex As Exception
            mlOBJGS.XMtoLog("", "", "" & mlKEY, Err.Description, "New", Session("mgUSERID"), mlOBJGF.FormatDate(Now))
        End Try
    End Sub


    
    
End Class
