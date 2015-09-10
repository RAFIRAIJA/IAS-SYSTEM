Imports System
Imports System.Data
Imports System.Web.UI.HtmlControls
Imports System.Drawing
Imports System.Data.OleDb
Imports System.IO


Partial Class gl_rpt_profitandloss_1_tr
    Inherits System.Web.UI.Page

    Public mlOBJGS As New IASClass.ucmGeneralSystem
    Public mlOBJGF As New IASClass.ucmGeneralFunction

    Dim mlKEY As String
    Dim mlSQL As String
    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlDATAADAPTER As OleDb.OleDbDataAdapter
    Dim mlDATASET As New DataSet
    Dim mlDATATABLE As New DataTable
    Dim mlSQL2 As String
    Dim mlREADER2 As OleDb.OleDbDataReader
    Dim mlSQLSOLD As String
    Public mlREADERSOLD As OleDb.OleDbDataReader

    Dim mlRECORDSTATUS As String
    Dim mlSQLRECORDSTATUS As String
    Dim mlFUNCTIONPARAMETER As String
    Protected mlSQLSTATP As String = "SELECT * FROM AR_INVHR"
    Public mlI As Integer
    Dim mlCURRENTDATE As String = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()

    'Search Variable
    Dim mlBRANCH2 As String
    Dim mlSITE2 As String

    Dim mlSQL_ADD1 As String
    Dim mlSQL_ADD2 As String
    Dim mlSQL_ADD3 As String
    Dim mlSQL_BRANCH As String
    Dim mlSQL_SITE As String
    Dim mlSQL_DATE As String
    Dim mlTABLENAME As String
    Dim mlSTARTTIME As String
    Dim mlENDTIME As String



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "GL Profit and Loss Report T1 (Transfer Data) V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "GL Profit and Loss Report T1 (Transfer Data) V2.00"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlFUNCTIONPARAMETER = Request.QueryString("mpFP")

        If Not Page.IsPostBack Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ar_rpt_invoice", "ar_rpt_invoice", "")
            LoadComboData()
        End If
    End Sub


    Sub ClearFields()
        mlDOCDATE1.Text = mlCURRENTDATE
        mlDOCDATE2.Text = mlCURRENTDATE
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
            'mlI = 5
            'Dim mlDOCDATE1 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
            'E.Item.Cells(mlI).Text = mlDOCDATE1.ToString("d")
            'E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

            'mlI = 5
            'E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

            'mlI = 9
            'Dim mlPOINT1 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
            'E.Item.Cells(mlI).Text = mlPOINT1.ToString("n")
            'E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

            'mlI = 10
            'Dim mlAMOUNT1 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
            'E.Item.Cells(mlI).Text = mlAMOUNT1.ToString("n")
            'E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

            'mlI = 12
            'Dim mlRECDATE1 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
            'E.Item.Cells(mlI).Text = mlRECDATE1.ToString("d")
            'E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right
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
        mlDATASET.Clear()
        mlDATAADAPTER = mlOBJGS.DbAdapter(mpSQL)
        mlDATAADAPTER.Fill(mlDATASET)
        mlDATATABLE = mlDATASET.Tables("table")
        mlDATAGRID.DataSource = mlDATATABLE
        mlDATAGRID.DataBind()

        mlOBJGS.CloseDataSet(mlDATASET)
        mlOBJGS.CloseDataAdapter(mlDATAADAPTER)
    End Sub

    Sub DeleteRecord()
        mlRECORDSTATUS = "Delete"
        'mlSQL = "UPDATE FROM AR_INVHR WHERE MenuID= '" & Trim(mlKEY) & "'"
        'mlOBJGS.ExecuteQuery(mlSQL)
        'RetrieveFields()
        'RetrieveFieldsDetail()
    End Sub

    Sub NewRecord()
        EnableCancel()
    End Sub

    Sub EditRecord()
        RetrieveFields()
        EnableCancel()
    End Sub

    Sub SaveRecord()
        QuerytoTable()
    End Sub

    Private Sub EnableCancel()
        pnFILL.Visible = True
        pnGRID.Visible = False
    End Sub

    Private Sub DisableCancel()
        pnFILL.Visible = False
        pnGRID.Visible = True
    End Sub

    Sub CancelOperation()
        pnFILL.Visible = True
        btCancelOperation.Visible = True
    End Sub

    Sub CalculateTotal()
        Dim mlGRANDTOTALPOINT As Double
        Dim mlGRANDTOTALAMOUNT As Double

        mlGRANDTOTALPOINT = 0
        mlGRANDTOTALAMOUNT = 0
    End Sub


    Sub SearchRecord()
    End Sub

    Protected Sub btSaveRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSaveRecord.Click
        SaveRecord()
    End Sub

    Sub LoadComboData()
        mlDOCDATE1.Text = mlCURRENTDATE
        mlDOCDATE2.Text = mlCURRENTDATE

        mpBRANCH.Items.Clear()
        mpBRANCH.Items.Add("10PB # Ware house - Pekan Baru")
        mlSQL = "SELECT [Branch Location], Name FROM [ISS Servisystem, PT$Location] ORDER BY Name"
        mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSN3")
        While mlREADER.Read
            mpBRANCH.Items.Add(mlREADER("Branch Location") & " # " & mlREADER("Name"))
        End While

    End Sub


    Sub QuerytoTable()
        Dim mlBVMONTH As String

        mlSTARTTIME = Now
        mlTABLENAME = " GL_PL_1 "
        mlBVMONTH = "201111"
        mlSQL = ""

        mlBRANCH2 = Trim(mlOBJGF.GetStringAtPosition(mpBRANCH.Text, "0", "#"))
        If mlSITE2 = "All" Then mlSITE2 = ""

        'mlSITE2 = ""
        'mlBRANCH2 = "10PB"
        'mlDOCDATE1.Text = "01/11/2012"
        'mlDOCDATE2.Text = "30/11/2012"


        mlSQL_DATE = ""
        If mlDOCDATE1.Text <> "" And mlDOCDATE2.Text <> "" Then
            mlSQL_DATE = " AND (GLE.[Posting Date] >= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(mlDOCDATE1.Text, "/")) & "'" & _
                         " AND GLE.[Posting Date] <= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(mlDOCDATE2.Text, "/")) & "' )"
        End If


        mlSQL_ADD1 = " SELECT DISTINCT PTSITE.LineNo_" & _
                    " FROM   [ISS-NAV-3].dbo.[ISS Servisystem, PT$CustServiceSite] PTSITE,  [ISS-NAV-3].dbo.[ISS Servisystem, PT$Location] PTLOC" & _
                    " WHERE PTSITE.Branch  = PTLOC.[Branch Location]"

        mlSQL_BRANCH = ""
        If mlOBJGF.IsNone(mlBRANCH2) = False Then
            mlSQL_BRANCH = "AND PTSITE.Branch='" & mlBRANCH2 & "'"
        End If

        mlSQL_SITE = ""
        If mlOBJGF.IsNone(mlSITE2) = False Then
            mlSQL_SITE = "AND PTSITE.LineNo_='" & mlSITE2 & "'"
        End If

        mlSQL_ADD1 = mlSQL_ADD1 & mlSQL_BRANCH & mlSQL_SITE

        mlSQL_ADD2 = " AND (" & _
                    " GLE.[CustServiceSiteLineNo] IN " & _
                    " (" & mlSQL_ADD1 & " )"
        mlSQL_ADD3 = " OR GLE.[Others Dimension 24 Code] IN " & _
                    " (" & mlSQL_ADD1 & " )" & _
                    " )"

        mlSQL = "SELECT DISTINCT " & _
                " GLE.[G_L Account No_],GLA.Name, GLE.[CustServiceSiteLineNo], GLE.[Others Dimension 24 Code]," & _
                " GLE.Amount, GLE.[Others Dimension 26 Code]" & _
                " INTO " & mlTABLENAME & _
                " FROM  [ISS Servisystem, PT$G_L Account] GLA, [ISS Servisystem, PT$G_L Entry] GLE" & _
                " WHERE " & _
                " GLE.[G_L Account No_] = GLA.No_" & mlSQL_DATE & mlSQL_ADD2 & mlSQL_ADD3
        'mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSN3")

        Try
            mlSQL = "DELETE FROM " & mlTABLENAME & " WHERE BVMonth = '" & mlBVMONTH & "'"
            mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")

            mlSQL = "INSERT INTO " & mlTABLENAME & " (" & _
                    " [G_L Account No_],Name,[CustServiceSiteLineNo],[Others Dimension 24 Code]," & _
                    " SearchName,Amount,Branch,BVMonth,[Posting Date])" & _
                     "SELECT DISTINCT " & _
                    " GLE.[G_L Account No_],GLA.Name, GLE.[CustServiceSiteLineNo], GLE.[Others Dimension 24 Code]," & _
                    " PTSITE.SearchName,GLE.Amount, GLE.[Others Dimension 26 Code],'" & mlBVMONTH & "',[Posting Date]" & _
                    " FROM  [ISS-NAV-3].dbo.[ISS Servisystem, PT$G_L Account] GLA, [ISS-NAV-3].dbo.[ISS Servisystem, PT$G_L Entry] GLE," & _
                    " [ISS-NAV-3].dbo.[ISS Servisystem, PT$CustServiceSite] PTSITE" & _
                    " WHERE " & _
                    " GLE.[G_L Account No_] = GLA.No_ AND PTSITE.Branch = GLE.[Others Dimension 26 Code] " & _
                    mlSQL_DATE & mlSQL_ADD2 & mlSQL_ADD3
            mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")

            mlENDTIME = Now
            mlMESSAGE.Text = "Task Transfer Data  : Start at " & mlSTARTTIME & " End on " & mlENDTIME & " "

        Catch ex As Exception
            mlMESSAGE.Text = Err.Description

        End Try

    End Sub



End Class
