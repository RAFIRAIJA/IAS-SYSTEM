Imports System.Data
Imports System.Data.OleDb
Imports System.io
Imports IAS.Core.CSCode
Partial Class ct_rpt_contract
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJPJ As New ModuleFunctionLocal

    Dim mlKEY As String
    Dim mlSQL As String
    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlDATAADAPTER As OleDb.OleDbDataAdapter
    Dim mlDATASET As New DataSet
    Dim mlDATATABLE As New DataTable
    Dim mlSQL2 As String
    Dim mlREADER2 As OleDb.OleDbDataReader
    Dim mlRSTEMP As OleDb.OleDbDataReader
    Dim mlSQLTEMP As String
    Dim mlSQLLEVEL As String

    Dim mlRECORDSTATUS As String
    Dim mlSQLRECORDSTATUS As String
    Dim mlFUNCTIONPARAMETER As String
    Dim mlI As Byte
    Dim mlCURRENTDATE As String = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        CekSession()
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Contract Tracking Report V2.01"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Contract Tracking Report V2.00"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlFUNCTIONPARAMETER = Request.QueryString("mpFP")
        If Not Page.IsPostBack Then
            pnSEARCHUSERID.Visible = False
            pnSEARCHUSERID2.Visible = False
            pnSEARCHCONTRACT.Visible = False
            pnSEARCHTRACK.Visible = False
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ap_rpt_mr_request", "MR Rpt", "")
            LoadComboData()
            btExCsv.Visible = False
        End If
    End Sub

    Protected Sub CekSession()
        If Session("mgMENUSTYLE") = "" Then
            Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=34FC35D4")
            Return
        End If
    End Sub
    Sub ClearFields()
        txDOCDATE1.Text = mlCURRENTDATE
        txDOCDATE2.Text = mlCURRENTDATE
        txDOCUMENTNO1.Text = ""
        txUSERID.Text = ""
        mlLINKDOC.Text = ""
        txCONTRACTNO1.Text = ""
        txUSERID.Text = ""
    End Sub

    Protected Sub mlDATAGRID_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRID.ItemCommand
        mlKEY = e.CommandArgument
        Select Case e.CommandName
            Case "BrowseRecord"
                mlMESSAGE.Text = "View Request for " & e.CommandArgument
                RetrieveFields()
                pnFILL.Visible = True
                pnGRID.Visible = False
            Case "EditRecord"
                mlMESSAGE.Text = "Edit Request for  " & e.CommandArgument
                EditRecord()
            Case "DeleteRecord"
                mlMESSAGE.Text = "Delete Request for  " & e.CommandArgument
                DeleteRecord()
            Case Else
                ' Ignore Other
        End Select
    End Sub

    Protected Sub mlDATAGRID_SortCommand(ByVal Source As Object, ByVal E As DataGridSortCommandEventArgs) Handles mlDATAGRID.SortCommand
        RetrieveFieldsDetail(mlSQLSTATEMENT.Text & " ORDER BY " & E.SortExpression)
    End Sub

    Sub mlDATAGRID_PageIndex(ByVal Source As Object, ByVal E As DataGridPageChangedEventArgs)
        mlDATAGRID.CurrentPageIndex = E.NewPageIndex
        RetrieveFieldsDetail(mlSQLSTATEMENT.Text)
    End Sub


    Protected Sub mlDATAGRID_ItemBound(ByVal Source As Object, ByVal E As DataGridItemEventArgs) Handles mlDATAGRID.ItemDataBound
        If E.Item.ItemType = ListItemType.Item Or E.Item.ItemType = ListItemType.AlternatingItem Then
            Select Case ddREPORT.Text
                Case "Summary"
                    mlI = 2
                    Dim mlDOCDATE2 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlDOCDATE2.ToString("d")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right


                    'mlI = 21
                    'Dim mlPOINT21 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
                    'E.Item.Cells(mlI).Text = mlPOINT21.ToString("n")
                    'E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

            End Select

        End If
    End Sub


    Protected Sub btCancelOperation_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btCancelOperation.Click
        CancelOperation()
    End Sub

    Public Sub RetrieveFields()
        ClearFields()
        DisableCancel()
    End Sub

    Sub RetrieveFieldsDetail(ByVal mpSQL As String)
        Try
            mlDATASET.Clear()
            mlDATAADAPTER = mlOBJGS.DbAdapter(mpSQL, "PB", "ISSP3")
            mlDATAADAPTER.Fill(mlDATASET)
            mlDATATABLE = mlDATASET.Tables("table")
            mlDATAGRID.DataSource = mlDATATABLE
            mlDATAGRID.DataBind()
            

            mlOBJGS.CloseDataSet(mlDATASET)
            mlOBJGS.CloseDataAdapter(mlDATAADAPTER)
            btExCsv.Visible = True
        Catch ex As Exception
        End Try
    End Sub

    Sub DeleteRecord()
        mlRECORDSTATUS = "Delete"
    End Sub

    Sub NewRecord()
        EnableCancel()
    End Sub

    Sub EditRecord()
        RetrieveFields()
        EnableCancel()
    End Sub

    Sub SaveRecord()
    End Sub

    Private Sub EnableCancel()
        pnFILL.Visible = True
        pnGRID.Visible = False
        btExCsv.Visible = False
    End Sub

    Private Sub DisableCancel()
        pnFILL.Visible = False
        pnGRID.Visible = True
        btExCsv.Visible = False
    End Sub

    Sub CancelOperation()
        pnFILL.Visible = True
        pnGRID.Visible = False
        btSearchRecord.Visible = True
        btCancelOperation.Visible = True
    End Sub

    Sub CalculateTotal()
        Dim mlGRANDTOTALPOINT As Double
        Dim mlGRANDTOTALAMOUNT As Double

        mlGRANDTOTALPOINT = 0
        mlGRANDTOTALAMOUNT = 0
    End Sub


    Sub LoadComboData()
        txDOCDATE1.Text = mlCURRENTDATE
        txDOCDATE2.Text = mlCURRENTDATE

        ddSTATUS.Items.Clear()
        ddSTATUS.Items.Add("New")
        ddSTATUS.Items.Add("Delete")

        ddREPORT.Items.Clear()
        ddREPORT.Items.Add("Summary")
        ddREPORT.Items.Add("Detail")

        ddPRODUCT.Items.Clear()
        ddPRODUCT.Items.Add("Pilih")
        mlSQLTEMP = "SELECT DISTINCT SysID FROM CT_CONTRACT_TASKDESC ORDER BY SysID"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
        While mlRSTEMP.Read
            ddPRODUCT.Items.Add(Trim(mlRSTEMP("SysID")))
        End While
    End Sub

    Protected Sub btSearchRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSearchRecord.Click
        SearchRecord()
    End Sub

    Protected Sub btExCsv_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btExCsv.Click
        ExportToCSV(mlSQLSTATEMENT.Text)
    End Sub

    Private Sub ExportToCSV(ByVal mpSQL As String)

        Dim mlDATAADAPTERCSV As OleDb.OleDbDataAdapter
        Dim mlDATASETCSV As New DataSet
        Dim mlDATATABLECSV As New DataTable
        Dim mlPATH As String
        Dim mlOBJGS_CSV As New IASClass.ucmGeneralSystem_ExporterCSV()
        Dim mlFILENAME As String
        Dim mlPATH2 As String

        mlDATASETCSV.Clear()
        mlDATAADAPTERCSV = mlOBJGS.DbAdapter(mpSQL)
        mlDATAADAPTERCSV.Fill(mlDATASETCSV)
        mlDATATABLECSV = mlDATASETCSV.Tables("table")

        mlFILENAME = "IAS_" & Left(Session("mguserid"), 3) & "_INV" & ".csv"
        mlPATH = Server.MapPath("../output/" & mlFILENAME)
        mlPATH2 = "../output/" & mlFILENAME
        Using mlOBJGS_CSVWRITER As New StreamWriter(mlPATH)
            mlOBJGS_CSVWRITER.Write(mlOBJGS_CSV.CsvFromDatatable(mlDATATABLECSV))
        End Using

        mlOBJGS.CloseDataSet(mlDATASETCSV)
        mlOBJGS.CloseDataAdapter(mlDATAADAPTERCSV)

        mlLINKDOC.Visible = True
        mlLINKDOC.Text = "<font Color=blue> Click to Download your Document (.csv) </font>"
        mlLINKDOC.NavigateUrl = mlPATH2
        mlLINKDOC.Attributes.Add("onClick", "window.open('" & mlPATH2 & "','','');")

        'System.Diagnostics.Process.Start(mlPATH)
    End Sub


    ''

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

    ''

    Protected Sub btUSERID2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btUSERID2.Click
        If mlOBJGF.IsNone(Trim(txUSERID2.Text)) = False Then
            txUSERDESC2.Text = mlOBJGS.ADGeneralLostFocus("USER", Trim(txUSERID2.Text))
        End If
    End Sub

    Protected Sub btSEARCHUSERID2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHUSERID2.Click
        If pnSEARCHUSERID2.Visible = False Then
            pnSEARCHUSERID2.Visible = True
        Else
            pnSEARCHUSERID2.Visible = False
        End If
    End Sub

    Protected Sub btSEARCHUSERID2SUBMIT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHUSERID2SUBMIT.Click
        If mlOBJGF.IsNone(mpSEARCHUSERID2.Text) = False Then SearchUser2(1, mpSEARCHUSERID2.Text)
    End Sub

    Protected Sub mlDATAGRIDUSERID2_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRIDUSERID2.ItemCommand
        Try
            txUSERID2.Text = CType(e.Item.Cells(0).Controls(0), LinkButton).Text
            txUSERDESC2.Text = CType(e.Item.Cells(1).Controls(0), LinkButton).Text
            mlDATAGRIDUSERID2.DataSource = Nothing
            mlDATAGRIDUSERID2.DataBind()
            pnSEARCHUSERID2.Visible = False
        Catch ex As Exception
        End Try
    End Sub

    Sub SearchUser2(ByVal mpTYPE As Byte, ByVal mpNAME As String)
        Try
            Select Case mpTYPE
                Case "1"
                    mlSQLTEMP = "SELECT UserID, Name FROM AD_USERPROFILE WHERE Name LIKE  '%" & mpNAME & "%'"
                    mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "AD", "AD")
                    mlDATAGRIDUSERID2.DataSource = mlRSTEMP
                    mlDATAGRIDUSERID2.DataBind()
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
            txCONTRACTNO1.Text = CType(e.Item.Cells(0).Controls(0), LinkButton).Text
            mlDATAGRIDCONTRACT.DataSource = Nothing
            mlDATAGRIDCONTRACT.DataBind()
            pnSEARCHCONTRACT.Visible = False

        Catch ex As Exception
        End Try
    End Sub

    Sub SearchCONTRACT(ByVal mpTYPE As Byte, ByVal mpNAME As String)
        Select Case mpTYPE
            Case "1"
                mlSQLTEMP = "SELECT  ContractNo as field_ID,SiteCardName as Field_Name FROM CT_CONTRACTHR WHERE [CustName] LIKE  '%" & mlSEARCHCONTRACT.Text & "%'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
                mlDATAGRIDCONTRACT.DataSource = mlRSTEMP
                mlDATAGRIDCONTRACT.DataBind()
          
        End Select
    End Sub

    ''
    Protected Sub btSEARCHTRACK_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHTRACK.Click
        If pnSEARCHTRACK.Visible = False Then
            pnSEARCHTRACK.Visible = True
        Else
            pnSEARCHTRACK.Visible = False
        End If
    End Sub


    Protected Sub btSEARCHTRACKSUBMIT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHTRACKSUBMIT.Click
        If mlOBJGF.IsNone(mlSEARCHTRACK.Text) = False Then SearchTRACK(1, mlSEARCHTRACK.Text)
    End Sub

    Protected Sub mlDATAGRIDTRACK_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRIDTRACK.ItemCommand
        Try
            txDOCUMENTNO1.Text = CType(e.Item.Cells(0).Controls(0), LinkButton).Text
            mlDATAGRIDTRACK.DataSource = Nothing
            mlDATAGRIDTRACK.DataBind()
            pnSEARCHTRACK.Visible = False

        Catch ex As Exception
        End Try
    End Sub

    Sub SearchTRACK(ByVal mpTYPE As Byte, ByVal mpNAME As String)
        Select Case mpTYPE
            Case "1"
                mlSQLTEMP = "SELECT  DocNo as field_ID,ReffNo as Field_Name FROM UT_TRANSFERTASK WHERE [CustName] LIKE  '%" & mlSEARCHTRACK.Text & "%'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
                mlDATAGRIDTRACK.DataSource = mlRSTEMP
                mlDATAGRIDTRACK.DataBind()

        End Select
    End Sub


    Sub SearchRecord()
        Dim mlDOCNO As String
        Dim mlSQLUSER As String
        Dim mlSQLDOCNO1 As String
        Dim mlSQLDOCNO2 As String
        Dim mlDOCNO_ACCM As String


        Try
            mlSQL = ""
            mlSQLUSER = ""

            If txDOCDATE1.Text <> "" And txDOCDATE2.Text <> "" Then
                mlSQL = mlSQL & " DocDate >= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE1.Text, "/")) & "' AND DocDate <= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE2.Text, "/")) & "' "
            End If

            If txDOCUMENTNO1.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " DocNo >= '" & txDOCUMENTNO1.Text & "' "
            End If

            If txCONTRACTNO1.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " ReffNo >= '" & txCONTRACTNO1.Text & "' "
            End If


            If ddPRODUCT.Text <> "Pilih" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " SysID LIKE '" & ddPRODUCT.Text & "%' "
            End If

            If txUSERID.Text <> "" Then
                mlSQLUSER = mlSQLUSER & IIf(mlSQLUSER = "", "", "AND") & " UserID1 LIKE '" & txUSERID.Text & "' "
            End If

            If txUSERID2.Text <> "" Then
                mlSQLUSER = mlSQLUSER & IIf(mlSQLUSER = "", "", "AND") & " UserID2 LIKE '" & txUSERID2.Text & "' "
            End If
            mlSQLUSER = IIf(mlSQLUSER = "", "", "AND") & mlSQLUSER


            If ddSTATUS.Text <> "all" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " RecordStatus LIKE '" & ddSTATUS.Text & "%' "
            End If

            If Not mlOBJGF.IsNone(mlSQL) Then
                Select Case ddREPORT.Text
                    Case "Summary"

                        mlDOCNO_ACCM = ""
                        mlSQLTEMP = " SELECT DISTINCT DocNo FROM UT_TRANSFERTASK" & _
                            " WHERE " & mlSQL & mlSQLUSER
                        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
                        While mlRSTEMP.Read
                            If mlDOCNO_ACCM = "" Then
                                mlSQLTEMP = IIf(mlDOCNO_ACCM = "", "", ",")
                            Else
                                mlSQLTEMP = IIf(mlDOCNO_ACCM = "", "", ",")
                            End If
                            mlDOCNO_ACCM = mlDOCNO_ACCM & IIf(mlDOCNO_ACCM <> "", ",", "") & "'" & mlRSTEMP("DocNo") & "'"
                        End While


                        If mlDOCNO_ACCM <> "" Then
                            mlDOCNO_ACCM = "(" & mlDOCNO_ACCM & ")"
                        End If

                        mlSQLDOCNO1 = ""
                        mlSQLDOCNO2 = ""
                        If mlDOCNO_ACCM <> "" Then
                            mlSQLDOCNO1 = " WHERE DocNo IN" & mlDOCNO_ACCM
                            mlSQLDOCNO2 = " AND CD.DocNo IN" & mlDOCNO_ACCM
                        End If

                        mlSQL = "SELECT DocNo as TrackNo,DocDate as Date,SysID as Type," & _
                            " ReffNo as Contract_No, CustName as Cust, SiteCardID as SiteCard, SiteCardName as SiteName," & _
                            " UserID1 as FrmID, UserName1 as FrmName, UserID2 as ToID,UserName2 as ToName, Description, Description2 FROM ( " & _
                            " SELECT DocNo,SysID,'0' as Linno, Linno as Linno2," & _
                            " ReffNo,CustName,SiteCardID, SiteCardName," & _
                            " UserID1, UserName1, UserID2,UserName2, DocDate, Description, UserName1 + ' to ' + UserName2 + ', Deadline= ' + CONVERT(VARCHAR(10),[DeadlineDate],101) as Description2 FROM UT_TRANSFERTASK " & mlSQLDOCNO1 & mlSQLUSER & _
                            " ) TB1"

                        

                    Case "Detail"

                        mlDOCNO_ACCM = ""
                        mlSQLTEMP = " SELECT DISTINCT DocNo FROM UT_TRANSFERTASK" & _
                            " WHERE " & mlSQL & mlSQLUSER
                        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
                        While mlRSTEMP.Read
                            If mlDOCNO_ACCM = "" Then
                                mlSQLTEMP = IIf(mlDOCNO_ACCM = "", "", ",")
                            Else
                                mlSQLTEMP = IIf(mlDOCNO_ACCM = "", "", ",")
                            End If
                            mlDOCNO_ACCM = mlDOCNO_ACCM & IIf(mlDOCNO_ACCM <> "", ",", "") & "'" & mlRSTEMP("DocNo") & "'"
                        End While


                        If mlDOCNO_ACCM <> "" Then
                            mlDOCNO_ACCM = "(" & mlDOCNO_ACCM & ")"
                        End If

                        mlSQLDOCNO1 = ""
                        mlSQLDOCNO2 = ""
                        If mlDOCNO_ACCM <> "" Then
                            mlSQLDOCNO1 = " WHERE DocNo IN" & mlDOCNO_ACCM
                            mlSQLDOCNO2 = " AND CD.DocNo IN" & mlDOCNO_ACCM
                        End If

                        mlSQL = "SELECT Type, DocNo as TrackNo,DocDate as Date,SysID as Type," & _
                            " ReffNo as Contract_No, CustName as Cust, SiteCardID as SiteCard, SiteCardName as SiteName," & _
                            " UserID1 as FrmID, UserName1 as FrmName, UserID2 as ToID,UserName2 as ToName, Description, Description2 FROM ( " & _
                            " SELECT '>' as Type, DocNo,SysID,'0' as Linno, Linno as Linno2," & _
                            " ReffNo,CustName,SiteCardID, SiteCardName," & _
                            " UserID1, UserName1, UserID2,UserName2, DocDate, Description, UserName1 + ' to ' + UserName2 + ', Deadline= ' + CONVERT(VARCHAR(10),[DeadlineDate],101) as Description2 FROM UT_TRANSFERTASK " & mlSQLDOCNO1 & mlSQLUSER & _
                            " UNION ALL" & _
                            " SELECT '    >>' as Type, CD.DocNo,CD.SysID,CD.Linno, CD.Linno2," & _
                            " UT.ReffNo,UT.CustName,UT.SiteCardID, UT.SiteCardName," & _
                            " CD.RecUserID as UserID1, CD.RecName as UserName2, CD.Courier_Type as UserID2, CD.Courier_Name as UserName2, CD.DocDate, CD.Description, 'Type=' + Courier_DocType + ', Courier_No=' + Courier_DocNo + ', Cargo_Date=' + CONVERT(VARCHAR(10),[Courier_Date],101) + ', Cargo_DocType=' + Courier_DocType + ', Cargo_Type=' + Courier_Type + ', Cargo_Name=' + Courier_Name + ', Cargo_PIC=' + Courier_PIC_ID + ', Cargo_PICName=' + Courier_PIC_Name + ', Cargo_PICNo=' + Courier_PIC_Phone + ', Cargo_PIC_Pos=' + " & _
                            " Courier_PIC_Pos as Description2 FROM CT_CONTRACT_TASKDESC CD" & _
                            " ,UT_TRANSFERTASK UT WHERE UT.DocNo = CD.DocNo" & mlSQLDOCNO2 & _
                            " ) TB1 ORDER BY DocNo"



                        'mlSQL = "SELECT DocNo as TrackNo,DocDate as Date,SysID as Type, " & _
                        '    " ReffNo as Contract_No, CustName as Cust, SiteCardID as SiteCard, SiteCardName as SiteName" & _
                        '    " UserID1 as FrmID, UserName1 as FrmName, UserID2 as ToID,UserName2 as ToName,  Description, Description2 FROM " & _
                        '    "( " & _
                        '    " SELECT DocNo,SysID,'0' as Linno,  Linno as Linno2,UserID1, UserName1, UserID2,UserName2, DocDate, Description," & _
                        '    " UserName1 + ' to ' + UserName2 + ', Deadline= ' + CONVERT(VARCHAR(10),[DeadlineDate],101) as Description2 FROM UT_TRANSFERTASK" & _
                        '    " WHERE DocNo IN" & mlDOCNO_ACCM & _
                        '    " UNION ALL" & _
                        '    " SELECT DocNo,SysID,Linno,  Linno2,RecUserID as UserID1, RecName as UserName2,Courier_Type as UserID2,Courier_Name as UserName2,DocDate, Description," & _
                        '    " 'Type=' +  Courier_DocType + ', Courier_No=' +  Courier_DocNo + " & _
                        '    " ', Cargo_Date=' +   CONVERT(VARCHAR(10),[Courier_Date],101) + " & _
                        '    " ', Cargo_DocType=' +  Courier_DocType + ', Cargo_Type=' +  Courier_Type +  " & _
                        '    "', Cargo_Name=' +  Courier_Name + ', Cargo_PIC=' +  Courier_PIC_ID + ', Cargo_PICName=' +  Courier_PIC_Name + " & _
                        '    "', Cargo_PICNo=' +  Courier_PIC_Phone +  ', Cargo_PIC_Pos=' +  Courier_PIC_Pos " & _
                        '    " as Description2 FROM CT_CONTRACT_TASKDESC" & _
                        '    " WHERE DocNo IN" & mlDOCNO_ACCM & _
                        '    " ) TB1"
                End Select



                mlSQLSTATEMENT.Text = mlSQL
                mlSQL = mlSQL & " ORDER BY DocNo,Linno,Linno2"
                RetrieveFieldsDetail(mlSQLSTATEMENT.Text)
                pnFILL.Visible = False
                pnGRID.Visible = True
                btSearchRecord.Visible = False
            End If


            ''Response.Write(mlSQL)
        Catch ex As Exception
            ''Response.Write(mlSQL)
            'Response.Write(Err.Description)
        End Try
    End Sub

End Class

