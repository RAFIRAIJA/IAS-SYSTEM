Imports System.Data
Imports System.Data.OleDb
Imports System.io

Partial Class xm_inbox
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJPJ As New FunctionLocal

    Dim mlKEY As String
    Dim mlSQL As String
    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlDATAADAPTER As OleDb.OleDbDataAdapter
    Dim mlDATASET As New DataSet
    Dim mlDATATABLE As New DataTable
    Dim mlSQL2 As String
    Dim mlREADER2 As OleDb.OleDbDataReader

    Dim mlRECORDSTATUS As String
    Dim mlSQLRECORDSTATUS As String
    Dim mlFUNCTIONPARAMETER As String
    Dim mlI As Byte
    Dim mlLINKEDSERVER1 As String
    Dim mlCURRENTDATE As String = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Inbox V2.01"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Inbox V2.01"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlFUNCTIONPARAMETER = Request.QueryString("mpFP")


        If Not Page.IsPostBack Then
            RetrieveFieldsDetail("")

            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ap_rpt_mr_sitenorequest", "MR Rpt", "")
            LoadComboData()
            btExCsv.Visible = False
        End If
    End Sub


    Sub ClearFields()
        txDOCDATE1.Text = mlCURRENTDATE
        txDOCDATE2.Text = mlCURRENTDATE
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
            mlI = 1
            Dim mlDOCDATE1 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
            E.Item.Cells(mlI).Text = mlDOCDATE1.ToString("d")
            E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right
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
        If mpSQL = "" Then
            mlSQL2 = "SELECT ReffDocNo AS DocNo_,ReffDocDate as Date,FromID as SenderID,FromName as SenderName,ProcessID as Process,Subject" & _
                    " FROM XM_INBOX  WHERE ToID = '" & Session("mgUSERID") & "' AND RecordStatus='New' ORDER BY DocNo Desc"
        Else
            mlSQL2 = mpSQL
        End If

        mlDATASET.Clear()
        mlDATAADAPTER = mlOBJGS.DbAdapter(mlSQL2, "PB", "ISSP3")
        mlDATAADAPTER.Fill(mlDATASET)
        mlDATATABLE = mlDATASET.Tables("table")
        mlDATAGRID.DataSource = mlDATATABLE
        mlDATAGRID.DataBind()


        mlOBJGS.CloseDataSet(mlDATASET)
        mlOBJGS.CloseDataAdapter(mlDATAADAPTER)
        btExCsv.Visible = True
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
        ddSTATUS.Items.Add("Post")

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

        mlFILENAME = "IAS_" & Left(Session("mguserid"), 3) & "_INBOX" & ".csv"
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

    Sub SearchRecord()
        Try
            If txDOCDATE1.Text <> "" And txDOCDATE2.Text <> "" Then
                mlSQL = mlSQL & " (DocDate >= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE1.Text, "/")) & "' AND DocDate <= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE2.Text, "/")) & "') "
            End If

            If ddSTATUS.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " RecordStatus = '" & ddSTATUS.Text & "' "
            End If

            If Not mlOBJGF.IsNone(mlSQL) Then
                mlSQL = "SELECT ReffDocNo AS DocNo,ReffDocDate as Date,FromID as FromUser,FromName as Name,ProcessID as Process,Subject" & _
                            " FROM XM_INBOX  WHERE ToID = '" & Session("mgUSERID") & "'" & mlSQL


                mlSQLSTATEMENT.Text = mlSQL
                RetrieveFieldsDetail(mlSQLSTATEMENT.Text)
                pnFILL.Visible = False
                btSearchRecord.Visible = False
            End If



        Catch ex As Exception
            Response.Write(mlSQL)
            Response.Write(Err.Description)
        End Try
    End Sub
End Class

