Imports System.Data.OleDb
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Imports IAS.Core.CSCode
Partial Class ap_doc_mr_schedule
    Inherits System.Web.UI.Page
    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJPJ As New ModuleFunctionLocal

    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Public mlREADER2 As OleDb.OleDbDataReader
    Dim mlSQL2 As String
    Dim mlKEY As String
    Dim mlMEMBERGROUP As String
    Dim mlI As Integer
    Dim mlMRUSERLEVEL As String

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        CekSession()
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Schedule Doc V2.00"
        mlMEMBERGROUP = System.Configuration.ConfigurationManager.AppSettings("mgMEMBERGROUPMENU")
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlKEY = Request.QueryString("mpID")

        mlSQL2 = "SELECT " & _
                " ItemKey,ItemDesc,[Vendor No.],No_MR,[Site Card No.],[Site Card Search Name],Unit,Qty,State" & _
                " FROM AP_OUT_SCHEDULE WHERE DocNo='" & mlKEY & "' ORDER BY ItemKey,[Vendor No.],State;"

        RetrieveFields()
        RetrieveFieldsDetail()
        RetrieveCompanyInfo()

        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "mk_doc_file", "Document File", "")
            pnGRID2.Visible = False
        End If

    End Sub
    Protected Sub CekSession()
        If Session("mgMENUSTYLE") = "" Then
            Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=34FC35D4")
            Return
        End If
    End Sub
    Public Sub RetrieveFields()
        lbTITLE.Text = "REQUISITION WORKSHEET"
        Try
            mlSQL = "SELECT * FROM AP_OUT_SCHEDULE WHERE DocNo='" & mlKEY & "'"
            mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3")
            If mlREADER.HasRows Then
                mlREADER.Read()
                lbDOCNO.Text = CType(Trim(mlREADER("DocNo") & ""), String)
                lbRECDATE.Text = CType(Trim(mlREADER("RecDate") & ""), Date)
            Else
                mlKEY = "33"
                mlKEY = mlOBJGF.Encrypt(mlKEY)
                Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=" & mlKEY)
            End If

            mlOBJGS.CloseFile(mlREADER)

        Catch ex As Exception
            mlKEY = "33"
            mlKEY = mlOBJGF.Encrypt(mlKEY)
            Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=" & mlKEY)
        End Try
    End Sub

    Sub RetrieveFieldsDetail()
        Try

            mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISSP3")
            mlDATAGRID.DataSource = mlREADER2
            mlDATAGRID.DataBind()

            mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISSP3")


        Catch ex As Exception
        End Try
    End Sub

    Public Sub RetrieveCompanyInfo()
        mlCOMPANYNAME.Text = "<b>" & System.Configuration.ConfigurationManager.AppSettings("mgCOMPANYDESC") & "</b>"
        mlCOMPANYADDRESS.Text = System.Configuration.ConfigurationManager.AppSettings("mgCOMPANYADDR1") & "<br>" & _
                                System.Configuration.ConfigurationManager.AppSettings("mgCOMPANYADDR2") & ", " & _
                                System.Configuration.ConfigurationManager.AppSettings("mgCOMPANYTOWN") & "-" & _
                                System.Configuration.ConfigurationManager.AppSettings("mgCOMPANYPOSTCODE") & "<br>" & _
                                "Phone: " & System.Configuration.ConfigurationManager.AppSettings("mgCOMPANYPHONE1") & " " & _
                                "Faxs: " & System.Configuration.ConfigurationManager.AppSettings("mgCOMPANYFAXS") & "<br>" & _
                                "Email:" & System.Configuration.ConfigurationManager.AppSettings("mgCOMPANYEMAIL") & " " & _
                                "Website:" & System.Configuration.ConfigurationManager.AppSettings("mgCOMPANYWEB")

    End Sub

    Protected Sub btEXPORTTOEXCEL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btEXPORTTOEXCEL.Click
        ExportToExcel()
    End Sub

    Function MimeTypeFile(ByVal mpFILEEXT As String) As String
        Dim mlFILE_MIMETYPE As String

        mlFILE_MIMETYPE = ""
        Select Case LCase(mpFILEEXT)
            Case "txt"
                mlFILE_MIMETYPE = "text/plain"
            Case "jpg", "jpg", "jpeg"
                mlFILE_MIMETYPE = "image/jpeg"
            Case "gif"
                mlFILE_MIMETYPE = "image/gif"
            Case "png"
                mlFILE_MIMETYPE = "image/png"
            Case "pdf"
                mlFILE_MIMETYPE = "application/pdf"
            Case "ppt", "pptx"
                mlFILE_MIMETYPE = "application/vnd.ms-powerpoint"
            Case "xls", "xlsx"
                mlFILE_MIMETYPE = "application/vnd.ms-excel"
            Case "doc", "docx"
                mlFILE_MIMETYPE = "application/msword"
        End Select
        Return mlFILE_MIMETYPE

    End Function


    Sub ExportToExcel()
        Dim mlDATAADAPTERDT As OleDb.OleDbDataAdapter
        Dim mlDATASETDT As New DataSet
        Dim mlDATATABLE As New DataTable
        Dim mlTABLEDETAIL As String
        Dim mlFILENAME As String
        Dim mlPATH As String
        Dim mlPATH2 As String

        Try

            mlFILENAME = Session("mgUSERID") & "_ap_schedule_" & mlOBJGF.CurrentBVMonthDate() & ".xls"
            mlPATH = Server.MapPath("../output/" & mlFILENAME)
            mlPATH2 = "../output/" & mlFILENAME


            mlDATAADAPTERDT = mlOBJGS.DbAdapter(mlSQL2, "PB", "ISSP3")
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
        End Try
    End Sub


    Protected Sub chkVIEW_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkVIEW.CheckedChanged
        If chkVIEW.Checked = True Then
            pnGRID2.Visible = True
        Else
            pnGRID2.Visible = False
        End If
    End Sub
End Class

