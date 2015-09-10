Imports System
Imports System.Data
Imports System.Web.UI.HtmlControls
Imports System.Drawing
Imports System.Data.OleDb
Imports System.IO
Imports IAS.Core.CSCode

Partial Class cm_bo_template1
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJPJ As New ModuleFunctionLocal

    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlREADER2 As OleDb.OleDbDataReader
    Dim mlSQL2 As String
    Dim mlKEY As String
    Dim mlKEY2 As String
    Dim mlRECORDSTATUS As String
    Dim mlSPTYPE As String
    Dim mlFUNCTIONPARAMETER As String
    Dim mlMRUSERLEVEL As String

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        CekSession()
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Material Requisition Entry Log V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Material Requisition Entry Log V2.00"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlFUNCTIONPARAMETER = Request("mpID")
        mlKEY = Request("mpID")
        mlKEY2 = Request("mpID2")

        mlMRUSERLEVEL = Left(mlOBJPJ.ISS_MR_UserLevel(Trim(Session("mgUSERID")), mlKEY2), 1)
        If mlMRUSERLEVEL = "0" Then
            mlMESSAGE.Text = "No Authorization for the Site Card of " & mlKEY2
            Exit Sub
        End If

        If Page.IsPostBack = False Then
            DisableCancel()
            RetrieveFieldsDetail("")
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ap_doc_mr_entrylog", "ap_doc_mr_entrylog", "")
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
                mlMESSAGE.Text = "Update Request for " & e.CommandArgument
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

    
    Sub SearchRecord()
    End Sub


    Public Sub RetrieveFields()
        DisableCancel()
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
    End Sub

    Sub EditRecord()
        mlOBJGS.mgNEW = False
        mlOBJGS.mgEDIT = True
        ClearFields()
    End Sub


    Private Sub EnableCancel()
        pnFILL.Visible = True
    End Sub

    Private Sub DisableCancel()
        pnFILL.Visible = False
    End Sub

    Sub ClearFields()
    End Sub


    Sub LoadComboData()
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

    Sub RetrieveFieldsDetail(ByVal mpSQL As String)
        If mpSQL = "" Then
            mlSQL2 = "SELECT DocNo,RecUDocNo as Counter, DocDate,MRType,SiteCardID,SiteCardName,Location,PercentageMR,RecordStatus as Status,RecUserID as UserID, RecDate as RevisedDate " & _
                    " FROM AP_MR_REQUESTHR_LOG  WHERE DocNo = '" & mlKEY & "' ORDER BY RecUDocNo"
        Else
            mpSQL = mlSQL2
        End If
        mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISSP3")
        mlDATAGRID.DataSource = mlREADER2
        mlDATAGRID.DataBind()
        mlOBJGS.CloseFile(mlREADER2)

    End Sub

End Class
