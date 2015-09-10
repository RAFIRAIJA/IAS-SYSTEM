Imports System.Data
Imports System.Data.OleDb
Imports System.io
Imports IAS.Core.CSCode
Partial Class ap_rpt_mr_request
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
        mlTITLE.Text = "Material Requisition Report V2.03"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Material Requisition Report V2.03"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If

        CekSession()

        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlFUNCTIONPARAMETER = Request.QueryString("mpFP")
        If Not Page.IsPostBack Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ap_rpt_mr_request", "MR Rpt", "")
            LoadComboData()
            btExCsv.Visible = False
        End If
    End Sub

    Protected Sub CekSession()

        If (Session("mgUSERID") = "") Then

            Response.Redirect("~/pageconfirmation.aspx?mpMESSAGE=34FC35D4")
            Return
        End If

    End Sub

    Sub ClearFields()
        txDOCDATE1.Text = mlCURRENTDATE
        txDOCDATE2.Text = mlCURRENTDATE
        txDOCUMENTNO1.Text = ""
        txDOCUMENTNO2.Text = ""
        txITEM1.Text = ""
        txITEM2.Text = ""
        txLOCID1.Text = ""
        txLOCID2.Text = ""
        lbLOCDESC1.Text = ""
        lbLOCDESC2.Text = ""
        txUSERID.Text = ""
        lbNAME.Text = ""
        mlLINKDOC.Text = ""
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
                    mlI = 3
                    Dim mlDOCDATE2 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlDOCDATE2.ToString("d")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                    mlI = 8
                    Dim mlPOINT7 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlPOINT7.ToString("n")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                Case "Detail"
                    mlI = 3
                    If E.Item.Cells(mlI).Text <> "" Then
                        Dim mlDOCDATE2 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
                        E.Item.Cells(mlI).Text = mlDOCDATE2.ToString("d")
                        E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right
                    End If

                    mlI = 8
                    If E.Item.Cells(mlI).Text <> "" Then
                        Dim mlPOINT7 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
                        E.Item.Cells(mlI).Text = mlPOINT7.ToString("n")
                        E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right
                    End If

                    mlI = 16
                    Dim mlPOINT15 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlPOINT15.ToString("n")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                    mlI = 17
                    Dim mlPOINT16 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlPOINT16.ToString("n")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                    mlI = 18
                    Dim mlPOINT17 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlPOINT17.ToString("n")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                    mlI = 19
                    Dim mlPOINT18 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlPOINT18.ToString("n")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                Case "Product Qty Request"

                    'mlI = 2
                    'E.Item.Cells(mlI).Visible = False

                    'mlI = 3
                    'E.Item.Cells(mlI).Visible = False


                    mlI = 6
                    Dim mlPOINT6 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlPOINT6.ToString("n")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                    mlI = 7
                    Dim mlPOINT7 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlPOINT7.ToString("n")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                    mlI = 8
                    Dim mlPOINT8 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlPOINT8.ToString("n")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right
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
            NeatDataGrid(ddREPORT.Text)
            ShowHideGrid(ddREPORT.Text)


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
        btSearchRecord.Visible = True
        btCancelOperation.Visible = True
    End Sub

    Sub CalculateTotal()
        Dim mlGRANDTOTALPOINT As Double
        Dim mlGRANDTOTALAMOUNT As Double

        mlGRANDTOTALPOINT = 0
        mlGRANDTOTALAMOUNT = 0
    End Sub

    Protected Sub btLOCID1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btLOCID1.Click
        lbLOCDESC1.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_DESC", Trim(txLOCID1.Text), "")
        '        lbLOCDESC1.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_DESC", Trim(txLOCID1.Text))
    End Sub

    Protected Sub btLOCID2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btLOCID2.Click
        lbLOCDESC2.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_DESC", Trim(txLOCID2.Text), "")
        '        lbLOCDESC2.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_DESC", Trim(txLOCID2.Text))
    End Sub

    Protected Sub btITEM1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btITEM1.Click
        lbITEMDESC1.Text = mlOBJPJ.ISS_INGeneralLostFocus("ITEMKEY", txITEM1.Text, "")
        '        lbITEMDESC1.Text = mlOBJPJ.ISS_INGeneralLostFocus("ITEMKEY", txITEM1.Text)
    End Sub

    Protected Sub btITEM2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btITEM2.Click
        lbITEMDESC2.Text = mlOBJPJ.ISS_INGeneralLostFocus("ITEMKEY", txITEM2.Text, "")
        '        lbITEMDESC2.Text = mlOBJPJ.ISS_INGeneralLostFocus("ITEMKEY", txITEM2.Text)
    End Sub


    Sub LoadComboData()
        txDOCDATE1.Text = mlCURRENTDATE
        txDOCDATE2.Text = mlCURRENTDATE

        ddSTATUS.Items.Clear()
        ddSTATUS.Items.Add("all")
        ddSTATUS.Items.Add("wait")
        ddSTATUS.Items.Add("wait1")
        ddSTATUS.Items.Add("wait2")
        ddSTATUS.Items.Add("wait3")
        ddSTATUS.Items.Add("New")
        ddSTATUS.Items.Add("Delete")

        ddREPORT.Items.Clear()
        ddREPORT.Items.Add("Summary")
        ddREPORT.Items.Add("Summary by SiteCard")
        ddREPORT.Items.Add("Detail")
        ddREPORT.Items.Add("Product Qty Request")
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


    Sub NeatDataGrid(ByVal mpTYPE As String)
        Dim mlDOCNO As String
        Dim mlMR_LEVEL_ As Byte
        mlDOCNO = ""

        mlMR_LEVEL_ = MRLevel(Session("mgUSERID"))
        Dim mlDG As DataGridItem

        Select Case mpTYPE
            Case "Summary"
                Select Case mlMR_LEVEL_
                    Case 0, 1
                        For Each mlDG In mlDATAGRID.Items
                            mlDG.Cells("8").Text = ""
                        Next mlDG
                End Select

            Case "Detail"
                For Each mlDG In mlDATAGRID.Items
                    Select Case mlMR_LEVEL_
                        Case 0, 1
                            mlDG.Cells("8").Text = ""
                            mlDG.Cells("17").Text = ""
                            mlDG.Cells("18").Text = ""
                        Case 2
                            mlDG.Cells("17").Text = ""
                            mlDG.Cells("18").Text = ""
                    End Select


                    If mlDOCNO = mlDG.Cells("2").Text Then
                        mlDG.Cells("0").Text = ""
                        mlDG.Cells("1").Text = ""
                        mlDG.Cells("2").Text = ""
                        mlDG.Cells("3").Text = ""
                        mlDG.Cells("4").Text = ""
                        mlDG.Cells("5").Text = ""
                        mlDG.Cells("6").Text = ""
                        mlDG.Cells("7").Text = ""
                        mlDG.Cells("8").Text = ""
                        mlDG.Cells("9").Text = ""
                        mlDG.Cells("10").Text = ""
                        mlDG.Cells("11").Text = ""
                    Else
                        mlDOCNO = mlDG.Cells("2").Text
                    End If
                Next mlDG

            Case "Product Qty Request"
                Select Case mlMR_LEVEL_
                    Case 0, 1, 2
                        For Each mlDG In mlDATAGRID.Items
                            mlDG.Cells("7").Text = ""
                            mlDG.Cells("8").Text = ""
                        Next mlDG
                End Select
        End Select
    End Sub

    Protected Sub btUSERID_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btUSERID.Click
        If mlOBJGF.IsNone(Trim(txUSERID.Text)) = False Then
            lbNAME.Text = mlOBJGS.ADGeneralLostFocus("USER", Trim(txUSERID.Text))
        End If
    End Sub

    Sub ShowHideGrid(ByVal mpTYPE As String)
        Dim mlDG As DataGridColumn

        Select Case mpTYPE
            Case "Product Qty Request"
                For Each mlDG In mlDATAGRID.Columns
                    If mlDG.HeaderText = "MR" Then
                        mlDG.Visible = False
                    End If
                Next

            Case Else
                For Each mlDG In mlDATAGRID.Columns
                    If mlDG.HeaderText = "MR" Then
                        mlDG.Visible = True
                    End If
                Next
        End Select
    End Sub

    Function MRLevel(ByVal mpUSERID As String) As Byte
        MRLevel = 0

        Try
            If mlSQLLEVEL <> "" Then
                mlSQLLEVEL = "SELECT Min(UserLevel)  as UserLevel FROM OP_USER_SITE HR WHERE " & mlSQLLEVEL
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLLEVEL, "PB", "ISSP3")
                If mlRSTEMP.HasRows Then
                    mlRSTEMP.Read()
                    If IsDBNull(mlRSTEMP("UserLevel")) Then
                        MRLevel = 0
                    Else
                        MRLevel = Convert.ToByte(mlRSTEMP("UserLevel"))
                    End If
                End If
            Else
                MRLevel = 3
            End If

        Catch ex As Exception
        End Try

        Return MRLevel
    End Function

    Sub SearchRecord()
        Try
            mlSQL = ""

            If txDOCDATE1.Text <> "" And txDOCDATE2.Text <> "" Then
                mlSQL = mlSQL & " HR.DocDate >= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE1.Text, "/")) & "' AND HR.DocDate <= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE2.Text, "/")) & "' "
            End If

            If txDOCUMENTNO1.Text <> "" And txDOCUMENTNO2.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.DocNo >= '" & txDOCUMENTNO1.Text & "' AND HR.DocNo <= '" & txDOCUMENTNO2.Text & "'"
            End If

            If mpPERIOD.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.BVMonth LIKE '" & Trim(mpPERIOD.Text) & "' "
            End If

            If txLOCID1.Text <> "" And txLOCID2.Text <> "" Then
                mlSQLTEMP = "SELECT SiteCardID FROM OP_USER_SITE WHERE SiteCardID='ALL' AND UserID = '" & Session("mgUSERID") & "'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
                If mlRSTEMP.HasRows = True Then
                    mlSQLTEMP = " HR.SiteCardID >= '" & txLOCID1.Text & "' AND HR.SiteCardID <= '" & txLOCID2.Text & "'"
                    mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & mlSQLTEMP

                Else
                    mlSQLTEMP = "  HR.SiteCardID IN " & _
                            " (" & _
                            " SELECT SiteCardID FROM OP_USER_SITE WHERE UserID = '" & Session("mgUSERID") & "' " & _
                            " AND SiteCardID >= '" & txLOCID1.Text & "' AND SiteCardID <= '" & txLOCID2.Text & "'" & _
                            " )"
                    mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & mlSQLTEMP
                End If
            Else
                mlSQLTEMP = "SELECT SiteCardID FROM OP_USER_SITE WHERE SiteCardID='ALL' AND UserID = '" & Session("mgUSERID") & "'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
                If mlRSTEMP.HasRows = False Then
                    mlSQLTEMP = "  HR.SiteCardID IN " & _
                                " (" & _
                                " SELECT SiteCardID FROM OP_USER_SITE WHERE UserID = '" & Session("mgUSERID") & "' " & _
                                " )"

                    mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & mlSQLTEMP
                End If
            End If

            If txUSERID.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " (HR.RecUserID LIKE '" & txUSERID.Text & "'  OR HR.PostingUserID1 LIKE '" & txUSERID.Text & "' OR " & _
                        "HR.PostingUserID2 LIKE '" & txUSERID.Text & "' OR HR.PostingUserID3 LIKE '" & txUSERID.Text & "')"
            End If

            If ddSTATUS.Text <> "all" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.RecordStatus LIKE '" & ddSTATUS.Text & "%'"
            End If

            If ddREPORT.Text = "Detail" Or ddREPORT.Text = "Product Qty Request" Then
                If txITEM1.Text <> "" And txITEM1.Text <> "" Then
                    mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " DT.ItemKey >= '" & txITEM1.Text & "' AND DT.ItemKey <= '" & txITEM2.Text & "'"
                End If
            End If

            If Not mlOBJGF.IsNone(mlSQL) Then
                Select Case ddREPORT.Text
                    Case "Summary"
                        mlSQL = "SELECT HR.DocNo,HR.DocDate,HR.MRType,HR.SiteCardID as Site,HR.SiteCardName as SiteDesc,HR.BVMonth as Period,HR.TotalAmount as Amount," & _
                            " HR.PostingUserID1 as CreateID, HR.PostingName1 as CreateName, HR.RecordStatus as Status FROM AP_MR_REQUESTHR HR WHERE " & mlSQL

                    Case "Detail"
                        mlSQL = "SELECT Distinct HR.DocNo,HR.DocDate,HR.MRType,HR.SiteCardID as Site,HR.SiteCardName as SiteDesc,HR.BVMonth as Period,HR.TotalAmount as Amount," & _
                        " HR.PostingUserID1 as CreateID, HR.PostingName1 as CreateName, HR.RecordStatus as Status, " & _
                        " DT.Linno as No,DT.ItemKey as Item, DT.Description as ItemDesc,DT.Uom,DT.Qty,DT.UnitPrice as Unit_Price,DT.TotalPrice as Total_Price," & _
                        " DT.Qty_Bal as Saldo,DT.RequestDesc as Ukuran,DT.Description2 as Ket" & _
                        " FROM AP_MR_REQUESTHR HR,AP_MR_REQUESTDT DT WHERE " & mlSQL & _
                        " AND HR.DocNo = DT.DocNo"

                    Case "Product Qty Request"
                        mlSQL = "SELECT Distinct '' as DocNo,'' as Site, DT.ItemKey as Item, DT.Description as ItemDesc, Sum(DT.Qty) as Qty, sum(DT.UnitPrice) as Unit_Price,sum(DT.TotalPrice) as Total_Price" & _
                        " FROM AP_MR_REQUESTHR HR,AP_MR_REQUESTDT DT WHERE " & mlSQL & _
                        " AND HR.DocNo = DT.DocNo" & _
                        " GROUP BY DT.ItemKey, DT.Description"

                    Case "Summary by SiteCard"
                        mlSQL = " SELECT Distinct '' as DocNo,'' as Site,	hr.EntityID ,hr.SiteCardID,hr.SiteCardName, " & _
                                " replace(convert(varchar, Convert(money,round(Sum(hr.TotalAmount),0,0)), 1),'.00','')  as TotalAmount " & _
                                " FROM AP_MR_REQUESTHR HR " & _
                                " left join AP_MR_REQUESTDT DT " & _
                                "    on HR.DocNo = DT.DocNo " & _
                                " WHERE hr.BVMonth = '" + mpPERIOD.Text + "' " & _
                                " group by hr.EntityID ,hr.SiteCardID,hr.SiteCardName " & _
                                " order by SiteCardID "
                End Select

                mlSQLSTATEMENT.Text = mlSQL
                RetrieveFieldsDetail(mlSQLSTATEMENT.Text)
                pnFILL.Visible = False
                btSearchRecord.Visible = False


            End If

        Catch ex As Exception
        End Try
    End Sub


    Protected Sub btPrint_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btPrint.Click
        SearchRecord()
        pnGRID.Visible = False

        'RVReport("", mlSQL, False)
        Response.Redirect("ap_rpt_mr_viewer.aspx?mpQuery=" & mlSQL & "&mpReportType=" & ddREPORT.Text)
    End Sub
End Class

