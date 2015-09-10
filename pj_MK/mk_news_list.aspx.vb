Imports System
Imports System.Data
Imports System.Web.UI.HtmlControls
Imports System.Drawing
Imports System.Data.OleDb
Imports System.IO
Imports IAS.Core.CSCode

Partial Class mk_news_list
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJPJ As New ModuleFunctionLocal

    Dim mlKEY As String
    Dim mlSQL As String
    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlDATAADAPTER As OleDb.OleDbDataAdapter
    Dim mlDATASET As New DataSet
    Dim mlSQL2 As String
    Dim mlREADER2 As OleDb.OleDbDataReader
    Dim mlSQLSOLD As String
    Dim mlREADERSOLD As OleDb.OleDbDataReader

    Dim mlRECORDSTATUS As String
    Dim mlSQLRECORDSTATUS As String
    Dim mlFUNCTIONPARAMETER As String
    Dim mlI As Byte
    Dim mlCURRENTDATE As String = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()
    Dim mlSQLPARAM As String

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "List Berita V2.01"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "List Berita V2.01"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlFUNCTIONPARAMETER = Request.QueryString("mpFP")
        If mlFUNCTIONPARAMETER <> "" Then
            mlSQLPARAM = " AND ParentCode = '" & mlFUNCTIONPARAMETER & "' "
        End If

        Select Case Request("mpSTATUS")
            Case "", "N"
                mlSQLRECORDSTATUS = "WHERE  RecordStatus='New' " & mlSQLPARAM
            Case "D"
                mlSQLRECORDSTATUS = "WHERE  RecordStatus='Del' " & mlSQLPARAM
        End Select

        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "mk_news_list", "News List Maintenance", "")

            mlSQL = "SELECT Type,DocNo,DocDate as Date,Subject FROM MK_NEWS " & mlSQLRECORDSTATUS
            mlSQLSTATEMENT.Text = mlSQL
            mlSQL = mlSQL & " ORDER BY DocNo Desc"
            RetrieveFieldsDetail(mlSQL)

            ClearFields()
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
            Case "EditRecord"
                mlMESSAGE.Text = "Edit Request for  " & e.CommandArgument
            Case "DeleteRecord"
                mlMESSAGE.Text = "Delete Request for  " & e.CommandArgument
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
        mlDATAADAPTER.Fill(mlDATASET, "table")
        mlDATAGRID.DataSource = mlDATASET.Tables("table")
        mlDATAGRID.DataBind()
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
        btSearchRecord.Visible = True
        btCancelOperation.Visible = True
    End Sub


    Sub SearchRecord()
        mlSQL = ""


        If mlDOCDATE1.Text <> "" And mlDOCDATE2.Text <> "" Then
            mlSQL = mlSQL & " DocDate >= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(mlDOCDATE1.Text, "/")) & "' AND DocDate <= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(mlDOCDATE2.Text, "/")) & "' "
        End If

        If Not mlOBJGF.IsNone(mlSQL) Then
            mlSQL = "SELECT Type,DocNo,DocDate as Date,Subject FROM MK_NEWS " & mlSQLRECORDSTATUS & " ORDER BY DocNo Desc"
            mlSQLSTATEMENT.Text = mlSQL
            RetrieveFieldsDetail(mlSQLSTATEMENT.Text)
            pnFILL.Visible = False
            btSearchRecord.Visible = False
        End If
    End Sub


    Protected Sub btSearchRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSearchRecord.Click
        SearchRecord()
    End Sub

End Class
