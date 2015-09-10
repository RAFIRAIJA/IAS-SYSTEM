Option Explicit On

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

Partial Class ap_doc_mr_worksheet
    Inherits System.Web.UI.Page
    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJFS As New IASClass.ucmFileSystem
    Dim mlOBJPJ As New ModuleFunctionLocal

    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlREADER2 As OleDb.OleDbDataReader
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
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Requisition Worksheet Doc V2.01"
        mlMEMBERGROUP = System.Configuration.ConfigurationManager.AppSettings("mgMEMBERGROUPMENU")
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlKEY = Request.QueryString("mpID")
        'mlKEY = "AP/ORW/00000064"
        'mlKEY = "AP/ORW/00000060"
        'Session("mgUSERID") = "admin"

        Select Case Trim(mlOBJGF.GetStringAtPosition(mlKEY, 1, "-"))
            Case "dt2"
                mlSQL2 = "SELECT " & _
                        " [Type],[No.],[Driver],convert(varchar(10),[Delivery Date],103) as [Delivery Date]," & _
                        " [Created By],[Verified By]," & _
                        " [Agreed By]," & _
                        " [Site Card No.],[Location Code],[Material Requisition No.]," & _
                        " [Description],[Material Requisition Quantity],[Unit of Measure Code]," & _
                        " [Item Journal Batch], [Vendor No.],[Sitecard Reference]," & _
                        " [Qty PO w1],[Qty PO w2],[Qty PO w3],[Qty PO w4],[Qty PO w5],DescriptionDt2,JobNo,JobTaskNo " & _
                        " FROM AP_OUT_REQUISITION WHERE ReffNo='" & mlKEY & "';"

            Case Else
                mlSQL2 = "SELECT " & _
                        " [Type],[No.],[Driver],convert(varchar(10),[Delivery Date],103) as [Delivery Date]," & _
                        " [Created By],[Verified By]," & _
                        " [Agreed By]," & _
                        " [Site Card No.],[Location Code],[Material Requisition No.]," & _
                        " [Description],[Material Requisition Quantity],[Unit of Measure Code]," & _
                        " [Item Journal Batch], [Vendor No.],[Sitecard Reference]," & _
                        " [Qty PO w1],[Qty PO w2],[Qty PO w3],[Qty PO w4],[Qty PO w5], JobNo,JobTaskNo " & _
                        " FROM AP_OUT_REQUISITION WHERE ReffNo='" & mlKEY & "';"
        End Select

        



        If Page.IsPostBack = False Then
            tb1.Visible = False
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "mk_doc_file", "Document File", "")
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
            mlSQL = "SELECT * FROM AP_OUT_REQUISITION WHERE DocNo='" & mlKEY & "'"
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

    Private Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable()
        Dim strConnString As [String] = System.Configuration _
            .ConfigurationManager.ConnectionStrings("conString").ConnectionString
        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        Try
            con.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
            sda.Dispose()
            con.Dispose()
        End Try
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

            mlFILENAME = Session("mgUSERID") & "_ap_worksheet_requisition_" & mlOBJGF.CurrentBVMonthDate() & ".xls"
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
            Response.Write(Err.Description)
        End Try
    End Sub



    Sub ExportToExcel2()
        Dim mlDATAADAPTERDT As OleDb.OleDbDataAdapter
        Dim mlDATASETDT As New DataSet
        Dim mlDATATABLE As New DataTable
        Dim mlTABLEDETAIL As String
        Dim mlFILENAME As String
        Dim mlPATH As String
        Dim mlPATH2 As String

        Try
            mlFILENAME = "ap_worksheet_requisition" & ".xls"
            mlPATH = Server.MapPath("../pj_tmplate/xls/" & mlFILENAME)
            mlFILENAME = Session("mgUSERID") & "_ap_worksheet_requisition_" & mlOBJGF.CurrentBVMonthDate() & ".xls"
            mlPATH2 = Server.MapPath("../output/" & mlFILENAME)
            mlOBJFS.File_Copy(mlPATH, mlPATH2)


            'mlFILENAME = Session("mgUSERID") & "_ap_worksheet_requisition_" & mlOBJGF.CurrentBVMonthDate() & ".xls"
            'mlFILENAME = "ap_worksheet_requisition" & ".xls"
            mlPATH = Server.MapPath("../output/" & mlFILENAME)
            mlPATH2 = "../output/" & mlFILENAME

            mlDATAADAPTERDT = mlOBJGS.DbAdapter(mlSQL2, "PB", "ISSP3")
            mlTABLEDETAIL = "table"
            mlDATASETDT.Clear()
            mlDATATABLE.Clear()
            mlDATAADAPTERDT.Fill(mlDATASETDT)
            mlDATATABLE = mlDATASETDT.Tables(mlTABLEDETAIL)

            Response.ClearContent()
            Response.AddHeader("content-disposition", _
                 "attachment;filename=" & mlFILENAME & "")
            Response.ContentType = "application/ms-excel"

            For Each dc As DataColumn In mlDATATABLE.Columns
                'sep = ";";
                Response.Write(dc.ColumnName + vbTab)
            Next
            Response.Write(System.Environment.NewLine)
            For Each dr As DataRow In mlDATATABLE.Rows
                For i As Integer = 0 To mlDATATABLE.Columns.Count - 1
                    Response.Write(dr(i).ToString() & vbTab)
                Next
                Response.Write(vbLf)
            Next

            Response.Flush()
            Response.End()
            Response.Write("finish")


        Catch ex As Exception
            Response.Write(Err.Description)
        End Try
    End Sub


    Protected Sub btExCsv_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btExCsv.Click
        Try
            ExportToCSV("")

        Catch ex As Exception
        End Try
    End Sub


    Private Sub ExportToCSV(ByVal mpSQL As String)
        Dim mlDATAADAPTERCSV As OleDb.OleDbDataAdapter
        Dim mlDATASETCSV As New DataSet
        Dim mlDATATABLECSV As New DataTable
        Dim mlPATH As String
        Dim mlPATH2 As String
        Dim mlOBJGS_CSV As New IASClass.ucmGeneralSystem_ExporterCSV()
        Dim mlFILENAME As String

        Try

            mlFILENAME = Session("mgUSERID") & "_ap_worksheet_requisition_" & mlOBJGF.CurrentBVMonthDate() & ".csv"
            mlPATH = Server.MapPath("../output/" & mlFILENAME)
            mlPATH2 = "../output/" & mlFILENAME

            mlDATASETCSV.Clear()
            mlDATAADAPTERCSV = mlOBJGS.DbAdapter(mlSQL2, "PB", "ISSP3")
            mlDATAADAPTERCSV.Fill(mlDATASETCSV)
            mlDATATABLECSV = mlDATASETCSV.Tables("table")

            'Response.ClearContent()
            'Response.AddHeader("content-disposition", _
            '     "attachment;filename=" & mlFILENAME & "")
            'Response.ContentType = "text/plain"


            Using mlOBJGS_CSVWRITER As New StreamWriter(mlPATH)
                mlOBJGS_CSVWRITER.Write(mlOBJGS_CSV.CsvFromDatatable(mlDATATABLECSV))
            End Using

            mlOBJGS.CloseDataSet(mlDATASETCSV)
            mlOBJGS.CloseDataAdapter(mlDATAADAPTERCSV)

            'Response.Flush()
            'Response.End()


            mlLINKDOC2.Visible = True
            mlLINKDOC2.Text = "<font Color=blue> Click to Download your Document (.csv) </font>"
            mlLINKDOC2.NavigateUrl = mlPATH2
            mlLINKDOC2.Attributes.Add("onClick", "window.open('" & mlPATH2 & "','','');")
            mlLINKDOC2.Focus()
        Catch ex As Exception
            mlOBJGS.XMtoLog("CSV", "BB", "Bonus Harian", "Bonus Harian to CSV", "New", Session("mguserid"), Now)
        End Try
    End Sub

    
  

    Protected Sub btEXPORTTOEXCEL_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btEXPORTTOEXCEL.Click
        ExportToExcel()
    End Sub


    Protected Sub btEXPORTTOEXCEL2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btEXPORTTOEXCEL2.Click
        ExportToExcel2()
    End Sub

    Protected Sub btCR1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btCR1.Click
        'Response.Redirect("ap_doc_mr_worksheet_cr.aspx?mpID=" & mlKEY & "")
        Response.Redirect("ap_doc_mr_worksheet_cr_revisi.aspx?mpID=" & mlKEY & "&FSize=1")
    End Sub 

    Protected Sub btVIEW_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btVIEW.Click
        tb1.Visible = True
        RetrieveFields()
        RetrieveFieldsDetail()
        RetrieveCompanyInfo()
    End Sub

    Protected Sub btRV_Click(sender As Object, e As ImageClickEventArgs) Handles btRV.Click
        Response.Redirect("ap_doc_mr_worksheet_cr_revisi.aspx?mpID=" & mlKEY & "&FSize=0")
    End Sub
End Class

