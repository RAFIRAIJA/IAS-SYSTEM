Imports System
Imports System.Data
Imports System.Data.OleDb



Partial Class ut_ap_fix_mrrequestdt2desc
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




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Repair MR Online Detail-2 V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Repair MR Online Detail-2 V2.00"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")


        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "", "", "")
        Else
        End If
    End Sub

    Protected Sub mlDATAGRID_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRID.ItemCommand
        mlKEY = e.CommandArgument
        Select Case e.CommandName
            Case "BrowseRecord"
                mlMESSAGE.Text = "View Request for " & e.CommandArgument
                RetrieveFields()
                pnFILL.Visible = True
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

        mlSQL = "SELECT * FROM WHERE DocNo = '" & Trim(mlKEY) & "'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL)
        If mlREADER.HasRows Then
            mlREADER.Read()
            txDOCUMENTNO.Text = mlREADER("DocNo") & ""
        End If
    End Sub

    Sub RetrieveFieldsDetail(ByVal mpSQL As String)
        If mpSQL = "" Then
            mlSQL2 = "SELECT * FROM " & _
                " WHERE RecordStatus='New' ORDER BY DocNo"
        Else
            mlSQL2 = mpSQL
        End If
        mlREADER2 = mlOBJGS.DbRecordset(mlSQL2)
        mlDATAGRID.DataSource = mlREADER2
        mlDATAGRID.DataBind()

        mlOBJGS.CloseFile(mlREADER2)
    End Sub


    Sub DeleteRecord()
        mlSPTYPE = "Delete"
        Try
            Sql_1(mlSPTYPE, mlKEY)
        Catch ex As Exception
        End Try

        mlSYSCODE.Value = ""
        RetrieveFields()
    End Sub

    Sub NewRecord()
        mlOBJGS.mgNEW = True
        mlOBJGS.mgEDIT = False
        EnableCancel()
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

        mlOBJPJ.SetTextBox(False, txDOCUMENTNO)

    End Sub

    Private Sub DisableCancel()
        btNewRecord.Visible = True
        btSaveRecord.Visible = True
        pnFILL.Visible = False

        mlOBJPJ.SetTextBox(True, txDOCUMENTNO)

    End Sub

    Sub ClearFields()
        txDOCUMENTNO.Text = ""
    End Sub


    Sub LoadComboData()
        mlSQL = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID='MR_POSTLEVEL' ORDER BY LinCode"
        mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3")
        While mlREADER.Read
            ddGROUPID.Items.Add(Trim(mlREADER("LinCode")) & "-" & mlREADER("Description"))
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
    End Sub

    Function ValidateForm() As String
        ValidateForm = ""

        If mlOBJGS.mgNEW = True Then

        End If

    End Function


    Sub Sql_1(ByVal mpSTATUS As String, ByVal mpCODE As String)
        Dim mlNEWRECORD As Boolean

        Try

            Dim mlSQLDT3 As String
            Dim mlDOCNO3 As String
            Dim mlITEMKEYDT3 As String
            Dim mlDESCDT3 As String
            Dim mlFIRST3 As Boolean
            Dim mlORIQTY3 As Double
            Dim mlORIITEMKEY3 As String
            Dim mlORISIZE As String

            Dim mlFIRST4 As Boolean
            Dim mlORIDOCNO3 As String

            mlITEMKEYDT3 = ""
            mlSQLDT3 = ""
            mlDESCDT3 = ""
            mlFIRST3 = True
            mlORIDOCNO3 = ""

            mlFIRST4 = True
            mlDOCNO3 = ""

            mlSQLTEMP = "SELECT * FROM AP_MR_REQUESTDT2 ORDER BY DocNo"
            mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
            While mlRSTEMP.Read
                mlORIDOCNO3 = Trim(mlRSTEMP("DocNo"))
                mlORIITEMKEY3 = Trim(mlRSTEMP("ItemKey"))
                mlORIQTY3 = Trim(Replace(mlRSTEMP("Qty"), ",", "."))
                mlORISIZE = Trim(mlRSTEMP("fSize"))

                If mlDOCNO3 <> mlORIDOCNO3 Then
                    If mlDOCNO3 <> "" Then
                        mlSQLDT3 = mlSQLDT3 & "UPDATE AP_MR_REQUESTDT SET Description3='" & mlDESCDT3 & "' WHERE DocNo='" & mlDOCNO3 & "' AND ItemKey = '" & mlITEMKEYDT3 & "';"
                        mlOBJGS.ExecuteQuery(mlSQLDT3, "PB", "ISSP3")
                        mlSQLDT3 = ""
                    End If

                    mlITEMKEYDT3 = ""
                    mlSQLDT3 = ""
                    mlDESCDT3 = ""
                    mlFIRST3 = True
                End If


                If mlITEMKEYDT3 = mlORIITEMKEY3 Then
                    mlDESCDT3 = mlDESCDT3 & IIf(mlOBJGF.IsNone(mlDESCDT3) = True, "", ", ") & mlORISIZE & "=" & mlORIQTY3
                Else
                    If mlFIRST3 = True Then
                        mlFIRST3 = False
                    Else
                        mlSQLDT3 = mlSQLDT3 & "UPDATE AP_MR_REQUESTDT SET Description3='" & mlDESCDT3 & "' WHERE DocNo='" & mlDOCNO3 & "' AND ItemKey = '" & mlITEMKEYDT3 & "';"
                        mlOBJGS.ExecuteQuery(mlSQLDT3, "PB", "ISSP3")
                        mlSQLDT3 = ""
                    End If

                    mlITEMKEYDT3 = mlORIITEMKEY3
                    mlDESCDT3 = mlORISIZE & "=" & mlORIQTY3
                End If

                If mlDOCNO3 <> mlORIDOCNO3 Then
                    mlDOCNO3 = mlORIDOCNO3
                End If
            End While


            If mlDOCNO3 <> mlORIDOCNO3 Then
                mlSQLDT3 = mlSQLDT3 & "UPDATE AP_MR_REQUESTDT SET Description3='" & mlDESCDT3 & "' WHERE DocNo='" & mlDOCNO3 & "' AND ItemKey = '" & mlITEMKEYDT3 & "';"
                mlOBJGS.ExecuteQuery(mlSQLDT3, "PB", "ISSP3")
            End If

            mlMESSAGE.Text = "Finish " & Now
        Catch ex As Exception
            mlOBJGS.XMtoLog("", "", "" & mlKEY, Err.Description, "New", Session("mgUSERID"), mlOBJGF.FormatDate(Now))
        End Try
    End Sub


End Class
