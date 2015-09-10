Imports System
Imports System.Data
Imports System.Web.UI.HtmlControls
Imports System.Drawing
Imports System.Data.OleDb
Imports System.IO
Imports IAS.Core.CSCode

Partial Class in_inv_image
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJIS As New IASClass.ucmImageSystem
    Dim mlOBJFS As New IASClass.ucmFileSystem
    Dim mlOBJPJ As New ModuleFunctionLocal

    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlREADER2 As OleDb.OleDbDataReader
    Dim mlSQL2 As String

    Dim mlKEY As String
    Dim mlRECORDSTATUS As String
    Dim mlSPTYPE As String
    Dim mlFUNCTIONPARAMETER As String

    Dim mlSQLTEMP As String
    Dim mlRSTEMP As OleDb.OleDbDataReader
    Dim mlCURRENTDATE As String = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()
    Dim mlPATHNORMAL As String
    Dim mlPATHWATERMARK As String
    Dim mlPATHTHUMBS As String

    Dim mlIMGSTATUS As Boolean
    Dim mlWATERMARKTEXT As String
    Dim mlPATHFROM As String
    Dim mlPATHTO As String

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            mlTITLE.Text = "Inventory Image Maintenance V2.03"
            Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Inventory Image Maintenance V2.03"
            mlOBJGS.Main()
            If mlOBJGS.ValidateExpiredDate() = True Then
                Exit Sub
            End If
            If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
            mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

            mlPATHNORMAL = System.Configuration.ConfigurationManager.AppSettings("mgIN_PATHNORMAL")
            mlPATHTHUMBS = System.Configuration.ConfigurationManager.AppSettings("mgIN_PATHTHUMBS")
            mlPATHWATERMARK = System.Configuration.ConfigurationManager.AppSettings("mgIN_PATHWATERMARK")


            mlFUNCTIONPARAMETER = "1"
            If Page.IsPostBack = False Then
                LoadComboData()
                DisableCancel()
                RetrieveFieldsDetail()
                pnSEARCHITEMKEY.Visible = False
                mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "IN_INMAST_ADDINFO", "IN_AddInfo", "")
            Else
            End If

        Catch ex As Exception
            Response.Write(Err.Description)

        End Try
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
                EditRecord()
            Case "DeleteRecord"
                mlMESSAGE.Text = "Delete Request for  " & e.CommandArgument
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

    Public Sub RetrieveFields()
        ClearFields()
        DisableCancel()

        mlSQL = "SELECT * FROM IN_INMAST_ADDINFO WHERE ItemKey = '" & Trim(mlKEY) & "'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3")
        If mlREADER.HasRows Then
            mlREADER.Read()
            mpITEMKEY.Text = mlREADER("ItemKey") & ""
            mpITEMDESC.Text = mlREADER("Description") & ""
            mlIMAGENAMES11.Text = mlREADER("ImageName1T") & ""

            If mlOBJGF.IsNone(mlREADER("ImagePath1N")) = False Then
                mlIMAGE11.ImageUrl = mlREADER("ImagePath1T") & "?t=" & Now.Ticks.ToString
            End If

        End If
    End Sub

    Sub RetrieveFieldsDetail()
        mlSQL2 = "SELECT ItemKey, Description FROM IN_INMAST_ADDINFO WHERE RecordStatus='New' AND CompanyID='" & ddENTITY.Text & "' ORDER BY ItemKey"
        mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISSP3")
        mlDATAGRID.DataSource = mlREADER2
        mlDATAGRID.DataBind()
    End Sub

    Sub DeleteRecord()
        mlSPTYPE = "Delete"
        Try
            Sql_1(mlSPTYPE, mlKEY)
        Catch ex As Exception
        End Try

        mlSYSCODE.Value = ""
        RetrieveFields()
        RetrieveFieldsDetail()
    End Sub

    Sub NewRecord()
        EnableCancel()
        ClearFields()
    End Sub

    Sub EditRecord()
        RetrieveFields()
        EnableCancel()
    End Sub

    Protected Sub btENTITY_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btENTITY.Click
        RetrieveFieldsDetail()
    End Sub

    Function ValidateForm() As String
        ValidateForm = ""

        If mpITEMDESC.Text = "" Then
            ValidateForm = ValidateForm & " <br /> Item Code or Item Description not allowed empty"
        End If

        mlSQLTEMP = "SELECT * FROM IN_INMAST_ADDINFO WHERE ItemKey = '" & Trim(mpITEMKEY.Text) & "'"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
        If mlRSTEMP.HasRows Then
            ValidateForm = ValidateForm & " <br /> Item Code for " & Trim(mpITEMKEY.Text) & " was found in Database (not allow duplicate) "
        End If

    End Function


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
        RetrieveFieldsDetail()

    End Sub

    Sub CancelOperation()
        mlMESSAGE.Text = ""
        DisableCancel()
        RetrieveFieldsDetail()
    End Sub

    Private Sub EnableCancel()
        btNewRecord.Visible = False
        btSaveRecord.Visible = True
        pnFILL.Visible = True
        pnGRID.Visible = False

        mlOBJPJ.SetTextBox(False, mpITEMKEY)
        mlIMAGEP11.Enabled = True
        mlIMAGE_BT11.Visible = False
        mlOBJPJ.SetTextBox(True, mlIMAGENAMES11)

    End Sub

    Private Sub DisableCancel()
        btNewRecord.Visible = True
        btSaveRecord.Visible = False
        pnFILL.Visible = False
        pnGRID.Visible = True

        mlOBJPJ.SetTextBox(False, mpITEMKEY)
        mlIMAGEP11.Enabled = False
        mlIMAGE_BT11.Visible = True

        mlOBJPJ.SetTextBox(True, mlIMAGENAMES11)
    End Sub

    Sub ClearFields()
        mpITEMKEY.Text = ""
        mpITEMDESC.Text = ""
        mlIMAGE11.ImageUrl = ""
        mlIMAGENAMES11.Text = ""
    End Sub


    Protected Sub btITEMKEY_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btITEMKEY.Click
        mpITEMDESC.Text = mlOBJPJ.ISS_INGeneralLostFocus("ITEMKEY", Trim(mpITEMKEY.Text), ddENTITY.SelectedValue)
    End Sub

    Protected Sub btSEARCHITEMKEY_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHITEMKEY.Click
        If pnSEARCHITEMKEY.Visible = False Then
            pnSEARCHITEMKEY.Visible = True
        Else
            pnSEARCHITEMKEY.Visible = False
        End If
    End Sub

    Protected Sub btSEARCHITEMKEYSUBMIT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHITEMKEYSUBMIT.Click
        If mlOBJGF.IsNone(mpSEARCHITEMKEY.Text) = False Then SearchItem(1, mpSEARCHITEMKEY.Text)
    End Sub

    Protected Sub mlDATAGRIDITEMKEY_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRIDITEMKEY.ItemCommand
        Try
            mpITEMKEY.Text = CType(e.Item.Cells(0).Controls(0), LinkButton).Text
            mpITEMDESC.Text = CType(e.Item.Cells(1).Controls(0), LinkButton).Text
            mlDATAGRIDITEMKEY.DataSource = Nothing
            mlDATAGRIDITEMKEY.DataBind()
            pnSEARCHITEMKEY.Visible = False
        Catch ex As Exception
        End Try
    End Sub

    Sub SearchItem(ByVal mpTYPE As Byte, ByVal mpNAME As String)
        Select Case mpTYPE
            Case "1"
                mlSQLTEMP = "SELECT No_, Description FROM [ISS Servisystem, PT$Item] INV WHERE Description LIKE  '%" & mpNAME & "%'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSN3")
                mlDATAGRIDITEMKEY.DataSource = mlRSTEMP
                mlDATAGRIDITEMKEY.DataBind()
        End Select
    End Sub

    Sub LoadComboData()
        ddENTITY.Items.Clear()
        ddENTITY.Items.Add("ISS")
        mlSQLTEMP = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID='ISS_Entity'"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISS")
        While mlRSTEMP.Read
            ddENTITY.Items.Add(Trim(mlRSTEMP("LinCode")))
        End While
    End Sub

    Sub Sql_1(ByVal mpSTATUS As String, ByVal mpCODE As String)
        Dim mlSTATUS As String
        Dim mlNEWRECORD As Boolean
        Dim mlI As Byte
        Dim mlTOTAL As Double

        Dim mlIMAGEPATH_1N As String
        Dim mlIMAGEPATH_1W As String
        Dim mlIMAGEPATH_1T As String
        Dim mlIMAGENAME_1N As String
        Dim mlIMAGENAME_1W As String
        Dim mlIMAGENAME_1T As String
        Dim mlIMAGEPATH As String = ""
        Dim mlIMAGENAMERND As String

        Try
            mlNEWRECORD = False
            mlTOTAL = 0

            mlIMAGEPATH_1N = ""
            mlIMAGEPATH_1W = ""
            mlIMAGEPATH_1T = ""

            mlIMAGENAME_1N = ""
            mlIMAGENAME_1W = ""
            mlIMAGENAME_1T = ""


            Select Case mpSTATUS
                Case "Edit", "Delete"
                    mlSQL = ""
                    mlSQL = mlSQL & mlOBJPJ.ISS_IN_INADDINFO_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
            End Select

            Select Case mpSTATUS
                Case "New"
                    mlNEWRECORD = True
                    mlKEY = Trim(mpITEMKEY.Text)

                Case "Edit"
                    mlSTATUS = "Edit"
                    mlNEWRECORD = True
                    mlSQL = mlSQL & " DELETE FROM IN_INMAST_ADDINFO WHERE ItemKey = '" & mlKEY & "';"

                    mlIMAGE11.ImageUrl = Nothing
                    mlIMAGE11.Dispose()
                    mlSQLTEMP = "SELECT * FROM IN_INMAST_ADDINFO WHERE ItemKey = '" & mlKEY & "'"
                    mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
                    While mlRSTEMP.Read
                        mlOBJFS.File_Delete(Server.MapPath(mlRSTEMP("ImagePath1N")))
                        mlOBJFS.File_Delete(Server.MapPath(mlRSTEMP("ImagePath1T")))
                        mlOBJFS.File_Delete(Server.MapPath(mlRSTEMP("ImagePath1W")))
                    End While

                Case "Delete"
                    mlSTATUS = "Delete"
                    mlSQL = mlSQL & " DELETE FROM IN_INMAST_ADDINFO WHERE ItemKey = '" & mlKEY & "';"

                    mlIMAGE11.ImageUrl = Nothing
                    mlIMAGE11.Dispose()
                    mlSQLTEMP = "SELECT * FROM IN_INMAST_ADDINFO WHERE ItemKey = '" & mlKEY & "'"
                    mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
                    While mlRSTEMP.Read
                        mlOBJFS.File_Delete(Server.MapPath(mlRSTEMP("ImagePath1N")))
                        mlOBJFS.File_Delete(Server.MapPath(mlRSTEMP("ImagePath1T")))
                        mlOBJFS.File_Delete(Server.MapPath(mlRSTEMP("ImagePath1W")))
                    End While

            End Select
            If mlOBJGF.IsNone(mlSQL) = False Then mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
            mlSQL = ""


            mlI = 11
            mlIMAGENAMERND = mlOBJGF.GetRandomPasswordUsingGUID(6)
            If mlNEWRECORD = True Then
                If mlIMAGEP11.HasFile Then
                    mlIMAGEPATH_1N = mlPATHNORMAL & mlIMAGEP11.FileName
                    mlIMAGENAME_1N = mlKEY & "_11_" & mlIMAGENAMERND & "." & mlOBJGF.GetStringAtPosition(Right(mlIMAGEP11.FileName, 5), "1", ".")
                    mlIMAGEPATH_1N = mlPATHNORMAL & mlIMAGENAME_1N
                    mlIMAGEP11.SaveAs(Server.MapPath(mlIMAGEPATH_1N))

                    mlIMAGEPATH_1T = mlPATHTHUMBS & mlIMAGENAME_1N
                    mlIMAGENAME_1T = mlIMAGENAME_1N
                    mlIMAGEPATH_1W = mlPATHWATERMARK & mlIMAGENAME_1N
                    mlIMAGENAME_1W = mlIMAGENAME_1N
                    mlWATERMARKTEXT = "ISS Indonesia"

                    mlPATHFROM = Server.MapPath(mlPATHNORMAL.Substring(0, mlPATHNORMAL.Length - 1))
                    mlPATHTO = Server.MapPath(mlPATHWATERMARK.Substring(0, mlPATHWATERMARK.Length - 1))
                    mlIMGSTATUS = mlOBJIS.Img_Watermark(mlPATHFROM, mlIMAGENAME_1N, mlPATHTO, mlIMAGENAME_1N, mlWATERMARKTEXT, 0, "", "0")

                    mlPATHFROM = mlPATHTO
                    mlPATHTO = Server.MapPath(mlPATHTHUMBS.Substring(0, mlPATHTHUMBS.Length - 1))
                    mlIMGSTATUS = mlOBJIS.Img_ResizeImage(mlPATHFROM, mlIMAGENAME_1N, mlPATHTO, mlIMAGENAME_1N, 50, 0, 0)

                Else
                    If mpSTATUS = "Edit" Then
                        mlIMAGEPATH_1N = mlOBJGF.GetStringAtPosition(mlIMAGE11.ImageUrl, 0, "?")
                        mlIMAGENAME_1N = mlIMAGENAMES11.Text
                    End If
                End If
            End If

            If mlNEWRECORD = True Then
                mlSQL = ""
                mlSQL = mlSQL & " INSERT INTO IN_INMAST_ADDINFO (ParentCode,ItemKey,Description,RandomStr," & _
                    " ImagePath1N,ImageName1N,ImagePath1W,ImageName1W,ImagePath1T,ImageName1T,CompanyID," & _
                    " RecordStatus,RecUserID,RecDate)" & _
                    " VALUES ( " & _
                    " '" & mlFUNCTIONPARAMETER & "','" & mlKEY & "','" & Trim(mpITEMDESC.Text) & "'," & _
                    " '" & Trim(mlIMAGENAMERND) & "'," & _
                    " '" & Trim(mlIMAGEPATH_1N) & "','" & Trim(mlIMAGENAME_1N) & "'," & _
                    " '" & Trim(mlIMAGEPATH_1W) & "','" & Trim(mlIMAGENAME_1W) & "'," & _
                    " '" & Trim(mlIMAGEPATH_1T) & "','" & Trim(mlIMAGENAME_1T) & "'," & _
                    " '" & Trim(ddENTITY.TEXT) & "'," & _
                    " 'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "');"
            End If

            Select Case mpSTATUS
                Case "New"
                    mlSQL = mlSQL & mlOBJPJ.ISS_IN_INADDINFO_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
            End Select
            mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
            mlSQL = ""

        Catch ex As Exception
            mlOBJGS.XMtoLog("IN", "IN_AddInfo", "IN_AddInfo" & mlKEY, Err.Description, "New", Session("mgUSERID"), mlOBJGF.FormatDate(Now))
        End Try
    End Sub

End Class
