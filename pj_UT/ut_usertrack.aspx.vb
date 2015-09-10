Imports System.Data
Imports System.Data.OleDb
Imports System.Web
Imports System.Web.UI.WebControls
Imports System.IO


Partial Class backoffice_ut_usertrack
    Inherits System.Web.UI.Page
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJGS As New IASClass.ucmGeneralSystem

    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlDATEF As Date
    Dim mlDATET As Date

    Dim mlDATAADAPTER As OleDb.OleDbDataAdapter
    Dim mlDATASET As New DataSet
    Dim mlDATATABLE As New DataTable
    Dim mlI As Byte
    Dim mlCURRENTDATE As String = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "User Tracking V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "User Tracking V2.00"

        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ut_usertrack", "User Track", "")
            LoadComboData()
            mpDATE1.Text = mlCURRENTDATE
            mpDATE2.Text = mlCURRENTDATE
        End If
    End Sub

    Sub SearchRecord()
        Dim mlSQLDATE As String

        If mpUSERID.Text = "" Then
            mlMESSAGE.Text = "UserID can't empty"
            Exit Sub
        End If

        Try
            mlDATEF = mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(mpDATE1.Text, "/"))
            mlDATET = mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(mpDATE2.Text, "/"))
            mlDATET = mlDATET.AddDays(1)

            mlSQLDATE = ""
            If mlDATEF.ToString <> "" And mlDATET.ToString <> "" Then
                mlSQLDATE = " AND (RecDate >='" & mlDATEF.ToString & "' AND RecDate <'" & mlDATET.ToString & "')"
            End If

            mlTITLE2.Text = ""
            mlTITLE2.Text = "Date (" & mlDATEF.ToString & "-----" & mlDATET.ToString & ")"


            mlSQL = " SELECT UserID, MenuID as Menu,MenuDescription as Remark, RecDate as Date FROM XM_USERLOG DS WHERE UserID = '" & Trim(mpUSERID.Text) & "' " & mlSQLDATE
            mlSQLSTATEMENT.Text = mlSQL
            RetrieveFieldsDetail(mlSQLSTATEMENT.Text)

            'Response.Write(mlSQL)
        Catch ex As Exception

        End Try
    End Sub

    Sub SaveRecord()
        Try
        Catch ex As Exception
        End Try
    End Sub


    Sub LoadComboData()
    End Sub

    Protected Sub mlDATAGRID_SortCommand(ByVal Source As Object, ByVal E As DataGridSortCommandEventArgs) Handles mlDATAGRID.SortCommand
        RetrieveFieldsDetail(mlSQLSTATEMENT.Text & " ORDER BY " & E.SortExpression)
    End Sub

    Protected Sub mlDATAGRID_ItemBound(ByVal Source As Object, ByVal E As DataGridItemEventArgs) Handles mlDATAGRID.ItemDataBound
        If E.Item.ItemType = ListItemType.Item Or E.Item.ItemType = ListItemType.AlternatingItem Then

            'mlI = 5
            'Dim mlAMOUNT1 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
            'E.Item.Cells(mlI).Text = mlAMOUNT1.ToString("n")
            'E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right


            mlI = 3
            Dim mlDATE1 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
        End If
    End Sub

    Protected Sub btSearchRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSearchRecord.Click
        SearchRecord()
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

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
