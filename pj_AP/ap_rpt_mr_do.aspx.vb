Imports System.Data
Imports System.Data.OleDb
Imports System.io
Imports IAS.Core.CSCode
Partial Class ap_rpt_mr_request_wait
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
        mlTITLE.Text = "Delivery Note from MR Report V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Delivery Note from MR Report V2.00"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
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
        If Session("mgMENUSTYLE") = "" Then
            Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=34FC35D4")
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
                    'mlI = 3
                    'Dim mlDOCDATE2 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
                    'E.Item.Cells(mlI).Text = mlDOCDATE2.ToString("d")
                    'E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                    'mlI = 9
                    'Dim mlPOINT7 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
                    'E.Item.Cells(mlI).Text = mlPOINT7.ToString("n")
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
            mlREADER2 = mlOBJGS.DbRecordset(mpSQL, "PB", "ISSP3")
            mlDATAGRID.DataSource = mlREADER2
            mlDATAGRID.DataBind()


            'mlDATASET.Clear()
            'mlDATAADAPTER = mlOBJGS.DbAdapter(mpSQL, "PB", "ISSP3")
            'mlDATAADAPTER.Fill(mlDATASET)
            'mlDATATABLE = mlDATASET.Tables("table")
            'mlDATAGRID.DataSource = mlDATATABLE
            'mlDATAGRID.DataBind()


            'mlOBJGS.CloseDataSet(mlDATASET)
            'mlOBJGS.CloseDataAdapter(mlDATAADAPTER)
            'btExCsv.Visible = True
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
        
        ddREPORT.Items.Clear()
        ddREPORT.Items.Add("Summary")
        ddREPORT.Items.Add("Summary_with_Dt2")
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

    Sub Fill_Dt2()
        Dim mlDESCDT2 As String
        Dim mlMRNO As String
        Dim mlITEMKEY As String

        Try

        
            Dim mlDG As DataGridItem
            For Each mlDG In mlDATAGRID.Items
                mlMRNO = mlDG.Cells("1").Text
                mlITEMKEY = mlDG.Cells("12").Text

                mlSQLTEMP = "SELECT * FROM AP_MR_REQUESTDT2 WHERE DocNo = '" & Trim(mlMRNO) & "' AND ItemKey='" & mlITEMKEY & "'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
                If mlRSTEMP.HasRows Then
                    While mlRSTEMP.Read
                        mlDESCDT2 = mlDESCDT2 & IIf(mlOBJGF.IsNone(mlDESCDT2) = True, "", ",") & mlRSTEMP("fsize") & "=" & mlRSTEMP("Qty")
                    End While
                    mlDG.Cells("18").Text = mlDESCDT2

                End If
                mlOBJGS.CloseFile(mlRSTEMP)

            Next mlDG

        Catch ex As Exception

        End Try
    End Sub


    Sub SearchRecord()
        Dim mlMRDOCNO As String
        Dim mlDONO As String
        Dim mlSQL2 As String

        Try
            mlSQL = ""

            'If txDOCDATE1.Text <> "" And txDOCDATE2.Text <> "" Then
            '    mlSQL = mlSQL & " HR.[Document Date] >= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE1.Text, "/")) & "' AND HR.[Document Date] <= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE2.Text, "/")) & "' "
            'End If

            If txDOCUMENTNO1.Text <> "" And txDOCUMENTNO2.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.[Document No_] >= '" & txDOCUMENTNO1.Text & "' AND HR.[Document No_] <= '" & txDOCUMENTNO2.Text & "'"
            End If


            If txLOCID1.Text <> "" And txLOCID2.Text <> "" Then
                mlSQLLEVEL = " HR.[CustServiceSiteLineNo] >= '" & txLOCID1.Text & "' AND HR.[CustServiceSiteLineNo] <= '" & txLOCID2.Text & "'" & _
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & mlSQLLEVEL
            End If

            mlMRDOCNO = ""
            mlSQLTEMP = "SELECT [External Document No_]" & _
                " FROM [ISS Servisystem, PT$Item Journal Line] HR WHERE " & mlSQL
            mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSN3")
            While mlRSTEMP.Read
                mlMRDOCNO = mlMRDOCNO & IIf(mlOBJGF.IsNone(mlMRDOCNO) = True, "", ",") & "'" & mlRSTEMP("External Document No_") & "'"
            End While


            If Not mlOBJGF.IsNone(mlSQL) Then
                Select Case ddREPORT.Text
                    Case "Summary"
                        mlSQL = "SELECT " & _
                            " HR.DocNo,HR.SiteCardID,HR.SiteCardName,HR.Do_Address,HR.Do_City,HR.Do_State,HR.Do_Country,HR.Do_Zip,HR.DO_Phone1,HR.PIC_ContactNo," & _
                            " DT.Linno AS No,DT.ItemKey as Kode,DT.Description as Nama_Item,DT.Uom as Satuan, DT.Qty as Kebutuhan," & _
                            " DT.RequestDesc as Permintaan,DT.Description2 as Keterangan, '' as DT2" & _
                            " FROM AP_MR_REQUESTHR HR,AP_MR_REQUESTDT DT " & _
                            " WHERE HR.DocNo = DT.DocNo AND HR.DocNo in (" & mlMRDOCNO & ")"

                      
                End Select

                mlSQLSTATEMENT.Text = mlSQL
                RetrieveFieldsDetail(mlSQLSTATEMENT.Text)
                Fill_Dt2()
                pnFILL.Visible = False
                btSearchRecord.Visible = False
            End If


        Catch ex As Exception

        End Try
    End Sub

    
End Class

