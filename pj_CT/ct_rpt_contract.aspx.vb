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
    Dim mlCOMPANYID As String

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        CekSession()
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Contract Report V2.01"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Contract Report V2.01"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlCOMPANYID = "ISSP3"

        mlFUNCTIONPARAMETER = Request.QueryString("mpFP")
        If Not Page.IsPostBack Then
            pnSEARCHSITECARD.Visible = False
            pnSEARCHCUST.Visible = False
            pnSEARCHSITECARD2.Visible = False
            pnSEARCHCUST2.Visible = False
            pnSEARCHUSERID.Visible = False
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
        txUSERID.Text = ""
        mlLINKDOC.Text = ""
        txCONTRACTNO1.Text = ""
        txCONTRACTNO2.Text = ""
        txCUST.Text = ""
        lbCUSTDESC.Text = ""
        txCUST2.Text = ""
        lbCUST2DESC.Text = ""
        txSITECARD.Text = ""
        lbSITEDESC.Text = ""
        txSITECARD2.Text = ""
        lbSITEDESC2.Text = ""
        txCRDOCDATE1.Text = ""
        txCRDOCDATE2.Text = ""
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
                Case "Summary2"
                    mlI = 2
                    Dim mlDOCDATE2 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlDOCDATE2.ToString("d")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right


                    mlI = 15
                    Dim mlDOCDATE15 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlDOCDATE15.ToString("d")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                    mlI = 18
                    Dim mlDOCDATE18 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlDOCDATE18.ToString("d")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                    mlI = 19
                    Dim mlDOCDATE19 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlDOCDATE19.ToString("d")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right


                    mlI = 21
                    Dim mlPOINT21 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlPOINT21.ToString("n")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                    mlI = 22
                    Dim mlPOINT22 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlPOINT22.ToString("n")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                    mlI = 23
                    Dim mlPOINT23 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlPOINT23.ToString("n")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                    mlI = 24
                    Dim mlPOINT24 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlPOINT24.ToString("n")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                    mlI = 25
                    Dim mlPOINT25 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlPOINT25.ToString("n")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                    mlI = 33
                    Dim mlDOCDATE33 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlDOCDATE33.ToString("d")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                Case "Summary1"
                    mlI = 6
                    Dim mlDOCDATE6 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlDOCDATE6.ToString("d")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right


                    mlI = 7
                    Dim mlDOCDATE7 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlDOCDATE7.ToString("d")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                    mlI = 8
                    Dim mlDOCDATE8 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlDOCDATE8.ToString("d")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right


                    mlI = 10
                    Dim mlPOINT10 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlPOINT10.ToString("n")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                    mlI = 11
                    Dim mlPOINT11 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlPOINT11.ToString("n")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                    mlI = 12
                    Dim mlPOINT12 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlPOINT12.ToString("n")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                    mlI = 13
                    Dim mlPOINT13 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlPOINT13.ToString("n")
                    E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                    mlI = 14
                    Dim mlPOINT14 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
                    E.Item.Cells(mlI).Text = mlPOINT14.ToString("n")
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


    Sub LoadComboData()
        txDOCDATE1.Text = mlCURRENTDATE
        txDOCDATE2.Text = mlCURRENTDATE

        ddSTATUS.Items.Clear()
        ddSTATUS.Items.Add("New")
        ddSTATUS.Items.Add("Delete")

        ddREPORT.Items.Clear()
        ddREPORT.Items.Add("Summary1")
        ddREPORT.Items.Add("Summary2")


        ddPRODUCT.Items.Clear()
        ddPRODUCT.Items.Add("Pilih")
        mlSQLTEMP = "SELECT * FROM  [dbo].[ISS Servisystem, PT$Dimension Value] WHERE [DIMENSION CODE]='PRD-OFF' ORDER BY Code"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSN3")
        While mlRSTEMP.Read
            ddPRODUCT.Items.Add(Trim(mlRSTEMP("Code") & "@" & mlRSTEMP("Name")))
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
        mlDATAADAPTERCSV = mlOBJGS.DbAdapter(mpSQL, "PB", mlCOMPANYID)
        mlDATAADAPTERCSV.Fill(mlDATASETCSV)
        mlDATATABLECSV = mlDATASETCSV.Tables("table")

        mlFILENAME = "IAS_" & Left(Session("mguserid"), 3) & "_ct_duedate" & ".csv"
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

    Protected Sub btExXls_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btExXls.Click
        ExportToExcel(mlSQLSTATEMENT.Text)
    End Sub

    Sub ExportToExcel(ByVal mpSQL As String)
        Dim mlDATAADAPTERDT As OleDb.OleDbDataAdapter
        Dim mlDATASETDT As New DataSet
        Dim mlDATATABLE As New DataTable
        Dim mlTABLEDETAIL As String
        Dim mlFILENAME As String
        Dim mlPATH As String
        Dim mlPATH2 As String

        Try

            mlFILENAME = Session("mgUSERID") & "_ct_duedate_" & mlOBJGF.CurrentBVMonthDate() & ".xls"
            mlPATH = Server.MapPath("../output/" & mlFILENAME)
            mlPATH2 = "../output/" & mlFILENAME


            mlDATAADAPTERDT = mlOBJGS.DbAdapter(mpSQL, "PB", mlCOMPANYID)
            mlTABLEDETAIL = "table"
            mlDATASETDT.Clear()
            mlDATATABLE.Clear()
            mlDATAADAPTERDT.Fill(mlDATASETDT)
            mlDATATABLE = mlDATASETDT.Tables(mlTABLEDETAIL)


            'Create a dummy GridView
            Dim GridView1 As New GridView()
            GridView1.AllowPaging = False
            GridView1.DataSource = mlDATATABLE
            GridView1.DataBind()

            Response.Clear()
            Response.Buffer = True
            Response.AddHeader("content-disposition", _
                 "attachment;filename=" & mlFILENAME & "")
            Response.Charset = ""
            Response.ContentType = "application/vnd.ms-excel"
            Dim sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)


            For i As Integer = 0 To GridView1.Rows.Count - 1
                'Apply text style to each Row
                GridView1.Rows(i).Attributes.Add("class", "textmode")
            Next
            GridView1.RenderControl(hw)

            ''style to format numbers to string
            Dim style As String = "<style> .textmode{mso-number-format:\@;}</style>"

            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.End()

        Catch ex As Exception
            Response.Write(Err.Description)
        End Try
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
                mlSQLTEMP = "SELECT LineNo_ as Field_ID,SearchName as Field_Name FROM [ISS Servisystem, PT$CustServiceSite] WHERE SearchName LIKE  '%" & mlSEARCHSITECARD.Text & "%' AND CustomerNo_= '" & txCUST.Text & "'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSN3")
                mlDATAGRIDSITECARD.DataSource = mlRSTEMP
                mlDATAGRIDSITECARD.DataBind()
        End Select
    End Sub

    Protected Sub btSITECARD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSITECARD.Click
        lbSITEDESC.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_DESC", Trim(txSITECARD.Text), "")
        '        lbSITEDESC.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_DESC", Trim(txSITECARD.Text))
    End Sub

    ''

    Protected Sub btSEARCHSITECARD2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHSITECARD2.Click
        If pnSEARCHSITECARD2.Visible = False Then
            pnSEARCHSITECARD2.Visible = True
        Else
            pnSEARCHSITECARD2.Visible = False
        End If
    End Sub


    Protected Sub btSEARCHSITECARD2SUBMIT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHSITECARD2SUBMIT.Click
        If mlOBJGF.IsNone(mlSEARCHSITECARD2.Text) = False Then SearchSITECARD2(1, mlSEARCHSITECARD2.Text)
    End Sub

    Protected Sub mlDATAGRIDSITECARD2_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRIDSITECARD2.ItemCommand
        Try
            txSITECARD2.Text = CType(e.Item.Cells(0).Controls(0), LinkButton).Text
            lbSITEDESC.Text = CType(e.Item.Cells(1).Controls(0), LinkButton).Text
            mlDATAGRIDSITECARD2.DataSource = Nothing
            mlDATAGRIDSITECARD2.DataBind()
            pnSEARCHSITECARD2.Visible = False
        Catch ex As Exception
        End Try
    End Sub

    Sub SearchSITECARD2(ByVal mpTYPE As Byte, ByVal mpNAME As String)
        Select Case mpTYPE
            Case "1"
                mlSQLTEMP = "SELECT LineNo_ as Field_ID,SearchName as Field_Name FROM [ISS Servisystem, PT$CustServiceSite] WHERE SearchName LIKE  '%" & mlSEARCHSITECARD2.Text & "%' AND CustomerNo_= '" & txCUST.Text & "'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSN3")
                mlDATAGRIDSITECARD2.DataSource = mlRSTEMP
                mlDATAGRIDSITECARD2.DataBind()
        End Select
    End Sub

    Protected Sub btSITECARD2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSITECARD2.Click
        lbSITEDESC2.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_DESC", Trim(txSITECARD2.Text), "")
        '        lbSITEDESC2.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_DESC", Trim(txSITECARD2.Text))
    End Sub

    ''
    Protected Sub btSEARCHCUST_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHCUST.Click
        If pnSEARCHCUST.Visible = False Then
            pnSEARCHCUST.Visible = True
        Else
            pnSEARCHCUST.Visible = False
        End If
    End Sub


    Protected Sub btSEARCHCUSTSUBMIT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHCUSTSUBMIT.Click
        If mlOBJGF.IsNone(mlSEARCHCUST.Text) = False Then SearchCUST(1, mlSEARCHCUST.Text)
    End Sub

    Protected Sub mlDATAGRIDCUST_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRIDCUST.ItemCommand
        Try
            txCUST.Text = CType(e.Item.Cells(0).Controls(0), LinkButton).Text
            lbCUSTDESC.Text = CType(e.Item.Cells(1).Controls(0), LinkButton).Text
            mlDATAGRIDCUST.DataSource = Nothing
            mlDATAGRIDCUST.DataBind()
            pnSEARCHCUST.Visible = False
        Catch ex As Exception
        End Try
    End Sub

    Sub SearchCUST(ByVal mpTYPE As Byte, ByVal mpNAME As String)
        Select Case mpTYPE
            Case "1"
                mlSQLTEMP = "SELECT No_ as Field_ID,[Search Name] as Field_Name FROM [ISS Servisystem, PT$Customer] WHERE [Search Name] LIKE  '%" & mlSEARCHCUST.Text & "%'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSN3")
                mlDATAGRIDCUST.DataSource = mlRSTEMP
                mlDATAGRIDCUST.DataBind()
        End Select
    End Sub

    Protected Sub btCUST_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btCUST.Click
        lbCUSTDESC.Text = mlOBJPJ.ISS_XMGeneralLostFocus("CUST_DESC", Trim(txCUST.Text), "")
        '        lbCUSTDESC.Text = mlOBJPJ.ISS_XMGeneralLostFocus("CUST_DESC", Trim(txCUST.Text))
    End Sub

    ''

    Protected Sub btSEARCHCUST2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHCUST2.Click
        If pnSEARCHCUST2.Visible = False Then
            pnSEARCHCUST2.Visible = True
        Else
            pnSEARCHCUST2.Visible = False
        End If
    End Sub


    Protected Sub btSEARCHCUST2SUBMIT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHCUST2SUBMIT.Click
        If mlOBJGF.IsNone(mlSEARCHCUST2.Text) = False Then SearchCUST2(1, mlSEARCHCUST2.Text)
    End Sub

    Protected Sub mlDATAGRIDCUST2_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRIDCUST2.ItemCommand
        Try
            txCUST2.Text = CType(e.Item.Cells(0).Controls(0), LinkButton).Text
            lbCUST2DESC.Text = CType(e.Item.Cells(1).Controls(0), LinkButton).Text
            mlDATAGRIDCUST2.DataSource = Nothing
            mlDATAGRIDCUST2.DataBind()
            pnSEARCHCUST2.Visible = False
        Catch ex As Exception
        End Try
    End Sub

    Sub SearchCUST2(ByVal mpTYPE As Byte, ByVal mpNAME As String)
        Select Case mpTYPE
            Case "1"
                mlSQLTEMP = "SELECT No_ as Field_ID,[Search Name] as Field_Name FROM [ISS Servisystem, PT$Customer] WHERE [Search Name] LIKE  '%" & mlSEARCHCUST2.Text & "%'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSN3")
                mlDATAGRIDCUST2.DataSource = mlRSTEMP
                mlDATAGRIDCUST2.DataBind()
        End Select
    End Sub

    Protected Sub btCUST2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btCUST2.Click
        lbCUST2DESC.Text = mlOBJPJ.ISS_XMGeneralLostFocus("CUST_DESC", Trim(txCUST2.Text), "")
        '        lbCUST2DESC.Text = mlOBJPJ.ISS_XMGeneralLostFocus("CUST_DESC", Trim(txCUST2.Text))
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

    Sub SearchRecord()
        Try
            mlSQL = ""

            If txDOCDATE1.Text <> "" And txDOCDATE2.Text <> "" Then
                mlSQL = mlSQL & " HR.DocDate >= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE1.Text, "/")) & "' AND DocDate <= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE2.Text, "/")) & "' "
            End If

            If txDOCUMENTNO1.Text <> "" And txDOCUMENTNO2.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.DocNo >= '" & txDOCUMENTNO1.Text & "' AND HR.DocNo <= '" & txDOCUMENTNO2.Text & "'"
            End If

            If txCONTRACTNO1.Text <> "" And txCONTRACTNO2.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.ContractNo >= '" & txCONTRACTNO1.Text & "' AND HR.ContractNo <= '" & txCONTRACTNO2.Text & "'"
            End If

            If txCUST.Text <> "" And txCUST2.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.CustID >= '" & txCUST.Text & "' AND HR.CustID <= '" & txCUST2.Text & "'"
            End If

            If txSITECARD.Text <> "" And txSITECARD2.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.SiteCardID >= '" & txSITECARD.Text & "' AND HR.SiteCardID <= '" & txSITECARD2.Text & "'"
            End If

            If txCRDOCDATE1.Text <> "" Then
                mlSQL = mlSQL & " HR.StartDate = '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txCRDOCDATE1.Text, "/")) & "' "
            End If

            If txCRDOCDATE2.Text <> "" Then
                mlSQL = mlSQL & " HR.EndDate = '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txCRDOCDATE2.Text, "/")) & "' "
            End If

            If ddPRODUCT.Text <> "Pilih" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.ServiceType LIKE '" & ddPRODUCT.Text & "%'"
            End If

            If txUSERID.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.RecUserID LIKE '" & txUSERID.Text & "'"
            End If

            If ddSTATUS.Text <> "all" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.RecordStatus LIKE '" & ddSTATUS.Text & "%'"
            End If

            If Not mlOBJGF.IsNone(mlSQL) Then
                Select Case ddREPORT.Text
                    Case "Summary2"
                        mlSQL = "" & _
                            " SELECT HR.DocNo,HR.DocDate,HR.CustID,HR.CustName,HR.SiteCardID,HR.SiteCardName,HR.Address,HR.City,HR.State,HR.Country,HR.Zip," & _
                            " HR.Phone1,HR.PIC_ContactNo," & _
                            " HR.ContractNo,HR.ContractDate,HR.ReffNo,HR.ReffDocNo,HR.StartDate,HR.EndDate,HR.ServiceType,HR.EmployeeQty,HR.IncrementPercent," & _
                            " HR.ExistingPrice,HR.ProposePrice,HR.Price,HR.Negotiator,HR.SC_Branch,HR.SC_BranchName," & _
                            " HR.Description,HR.FileDocNo," & _
                            " HR.RecordStatus,HR.RecUserID,HR.RecDate" & _
                            " FROM CT_CONTRACTHR HR WHERE " & mlSQL

                    Case "Summary1"
                        mlSQL = "" & _
                            " SELECT HR.DocNo,HR.CustName,HR.SiteCardID,HR.SiteCardName," & _
                            " HR.ContractNo,HR.ContractDate,HR.StartDate,HR.EndDate,HR.ServiceType,HR.EmployeeQty,HR.IncrementPercent," & _
                            " HR.ExistingPrice,HR.ProposePrice,HR.Price," & _
                            " HR.Description," & _
                            " HR.RecordStatus,HR.RecUserID,HR.RecDate" & _
                            " FROM CT_CONTRACTHR HR WHERE " & mlSQL

                End Select

                mlSQLSTATEMENT.Text = mlSQL
                RetrieveFieldsDetail(mlSQLSTATEMENT.Text)
                pnFILL.Visible = False
                btSearchRecord.Visible = False
            End If

            btExCsv.Visible = True
            btExXls.Visible = True

        Catch ex As Exception
        End Try
    End Sub

End Class

