Imports System
Imports System.Data
Imports System.Data.OleDb
Imports IAS.Core.CSCode
Partial Class fs_file_download
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJFS As New IASClass.ucmFileSystem
    Dim mlOBJPJ As New ModuleFunctionLocal

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
    Dim mlSQLRECUSERID As String

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        CekSession()
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "File Download V2.01"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "File Upload V2.01"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlSQLRECUSERID = "AND (IB.ToID = '" & Trim(Session("mgUSERID")) & "' OR IB.FromID = '" & Trim(Session("mgUSERID")) & "')"

        If Page.IsPostBack = False Then
            ClearFields()
            LoadComboData()
            EnableCancel()
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
                mlMESSAGE.Text = "View Request for " & e.CommandArgument
                RetrieveFields()
                pnFILL.Visible = True
                pnFILL2.Visible = True
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

    Protected Sub mlDATAGRID2_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRID2.ItemCommand
        mlKEY = e.CommandArgument

        Select Case e.CommandName
            Case "DownloadRecord"
                DownloadRecord(mlOBJGF.GetStringAtPosition(mlKEY, 0, ","), mlOBJGF.GetStringAtPosition(mlKEY, 1, ","))

            Case Else
                ' Ignore Other
        End Select
    End Sub


    Protected Sub mlDATAGRID2_ItemBound(ByVal Source As Object, ByVal E As DataGridItemEventArgs) Handles mlDATAGRID2.ItemDataBound
        If E.Item.ItemType = ListItemType.Item Or E.Item.ItemType = ListItemType.AlternatingItem Then

        End If
    End Sub


    Sub DownloadRecord(ByVal mpDOCNO As String, ByVal mpLINNO As Integer)

        Dim mlPARAM As String

        Try
            mlPARAM = ""
            mlSQLTEMP = "SELECT FilePath,FileName FROM XM_FILEDT WHERE DocNo = '" & mpDOCNO & "' AND Linno='" & mpLINNO & "'"
            mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
            If mlRSTEMP.HasRows = True Then
                mlRSTEMP.Read()
                mlPARAM = "fs_file_download.aspx" & "#" & Trim(mlRSTEMP("FilePath")) & "#" & Trim(mlRSTEMP("FileName"))
                Session("mpPARAM1") = mlPARAM
            End If

        Catch ex As Exception

        End Try
    End Sub


    
    Protected Sub btSearchRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSearchRecord.Click
        SearchRecord()
    End Sub


    Public Sub RetrieveFields()
        DisableCancel()

        mlSQL = "SELECT * FROM XM_FILEHR WHERE DocNo = '" & Trim(mlKEY) & "'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3")
        If mlREADER.HasRows Then
            mlREADER.Read()
            lbDOCUMENTNO.Text = mlREADER("DocNo") & ""
            lbDOCDATES.Text = mlREADER("DocDate") & ""
            lbTYPES.Text = mlREADER("GroupID") & ""
            lbDESCRIPTIONS.Text = mlREADER("Description") & ""

            RetrieveFieldsDetail2("")
        End If
    End Sub

    Sub RetrieveFieldsDetail(ByVal mpSQL As String)
        If mpSQL = "" Then
            mlSQL2 = "SELECT HR.DocNo,HR.DocDate as Date,HR.GroupID as Type,HR.Description FROM XM_FILEHR HR,XM_INBOX IB " & _
                " WHERE HR.DocNo = IB.ReffDocNo AND HR.RecordStatus='New' " & mlSQLRECUSERID & " ORDER BY HR.DocNo Desc"
        Else
            mlSQL2 = mpSQL
        End If
        mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISSP3")
        mlDATAGRID.DataSource = mlREADER2
        mlDATAGRID.DataBind()

        mlOBJGS.CloseFile(mlREADER2)
    End Sub

    Sub RetrieveFieldsDetail2(ByVal mpSQL As String)
        If mpSQL = "" Then
            mlSQL2 = "SELECT DocNo,Linno as No,FileDesc as File_Name FROM XM_FILEDT" & _
                " WHERE DocNo='" & lbDOCUMENTNO.Text & "' ORDER BY Linno"
        Else
            mlSQL2 = mpSQL
        End If
        mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISSP3")
        mlDATAGRID2.DataSource = mlREADER2
        mlDATAGRID2.DataBind()

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
        pnFILL.Visible = True
        pnFILL2.Visible = False
        trUP0.Visible = False
    End Sub

    Private Sub DisableCancel()
        pnFILL.Visible = False
        pnFILL2.Visible = False
        trUP0.Visible = True
    End Sub

    Sub ClearFields()
        txDOCDATE1.Text = mlCURRENTDATE
        txDOCDATE2.Text = mlCURRENTDATE
    End Sub


    Sub LoadComboData()
        ddTYPE.Items.Clear()
        ddTYPE.Items.Add("All")
        mlSQLTEMP = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID='FS_GroupFile'"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISS")
        While mlRSTEMP.Read
            ddTYPE.Items.Add(Trim(mlRSTEMP("LinCode")))
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
            mlKEY = ""
            Sql_1(mlSPTYPE, mlKEY)
        Catch ex As Exception
        End Try

        mlSYSCODE.Value = ""
        ClearFields()
        DisableCancel()
    End Sub

    Function ValidateForm() As String
        ValidateForm = ""
    End Function


    Sub Sql_1(ByVal mpSTATUS As String, ByVal mpCODE As String)
        
    End Sub

    Sub SearchRecord()
        Dim mlSQL As String

        mlSQL = ""
        If txDOCDATE1.Text <> "" And txDOCDATE2.Text <> "" Then
            mlSQL = mlSQL & " AND HR.DocDate >= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE1.Text, "/")) & "' AND HR.DocDate <= '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE2.Text, "/")) & "' "
        End If

        If ddTYPE.text <> "All" Then
            mlSQL = mlSQL & " AND HR.GroupID = '" & ddTYPE.text & "' "
        End If

        Try
            If mlOBJGF.IsNone(mlSQL) = False Then
                Try
                    mlSQL = "SELECT Distinct HR.DocNo,HR.DocDate as Date,HR.GroupID as Type,HR.Description FROM XM_FILEHR HR,XM_INBOX IB " & _
                        " WHERE HR.DocNo = IB.ReffDocNo AND HR.RecordStatus='New' " & mlSQL & mlSQLRECUSERID & " ORDER BY HR.DocNo Desc"
                    RetrieveFieldsDetail(mlSQL)
                Catch ex As Exception
                End Try
            End If

        Catch ex As Exception
        End Try
    End Sub

End Class

