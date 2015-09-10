Imports System.Data.OleDb
Imports System.Data
Imports System.IO
Imports IAS.Core.CSCode
Partial Class ap_doc_mr
    Inherits System.Web.UI.Page
    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJPJ As New ModuleFunctionLocal

    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlREADER2 As OleDb.OleDbDataReader
    Dim mlSQL2 As String
    Dim mlFUNCTIONPARAMETER As String
    Dim mlKEY As String
    Dim mlKEY2 As String
    Dim mlKEY3 As String
    Dim mlMEMBERGROUP As String
    Dim mlI As Integer
    Dim mlMRUSERLEVEL As String
    Dim mlTABLELOG As String
    Dim mlSQLCOUNTER As String

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        CekSession()
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Material Requisition Doc V2.00"
        mlMEMBERGROUP = System.Configuration.ConfigurationManager.AppSettings("mgMEMBERGROUPMENU")
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlFUNCTIONPARAMETER = Request.QueryString("mpFP")
        mlKEY = Request.QueryString("mpID")
        mlKEY2 = Request.QueryString("mpID2")
        mlKEY3 = Request.QueryString("mpRD")

        mlTABLELOG = ""
        Select Case Trim(UCase(mlFUNCTIONPARAMETER))
            Case "L"
                mlTABLELOG = "_LOG"
        End Select

        If mlKEY3 <> "" Then
            mlSQLCOUNTER = " AND RecUDocNo = '" & mlKEY3 & "' "
        End If

        If mlKEY2 <> "" Then
            mlMRUSERLEVEL = Left(mlOBJPJ.ISS_MR_UserLevel(Trim(Session("mgUSERID")), mlKEY2), 1)
            If mlMRUSERLEVEL = "0" Then
                mlMESSAGE.Text = "No Authorization"
                Exit Sub
            End If
        End If


        RetrieveFields()
        RetrieveFieldsDetail()
        RetrieveFieldsDetail2()
        RetrieveCompanyInfo()

        If Page.IsPostBack = False Then
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
        Try
            mlSQL = "SELECT * FROM AP_MR_REQUESTHR" & mlTABLELOG & " WHERE DocNo='" & mlKEY & "' " & mlSQLCOUNTER
            mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3")
            If mlREADER.HasRows Then
                mlREADER.Read()
                lbSITECARD.Text = CType(Trim(mlREADER("SiteCardID") & ""), String)
                mlMRUSERLEVEL = Left(mlOBJPJ.ISS_MR_UserLevel(Trim(Session("mgUSERID")), lbSITECARD.Text), 1)
                If mlMRUSERLEVEL = "0" Then
                    mlMESSAGE.Text = "No Authorization"
                    Exit Sub
                End If


                lbTITLE.Text = "MATERIAL REQUISITION (" & CType(Trim(mlREADER("MRType") & ")"), String)
                lbTITLE2.Text = CType(Trim(mlREADER("MRDesciption") & ""), String)

                lbDOCNO.Text = CType(Trim(mlREADER("DocNo") & ""), String)
                lbPRJNAME.Text = CType(Trim(mlREADER("SiteCardName") & ""), String)
                lbDEPT.Text = CType(Trim(mlREADER("DeptID") & ""), String)
                lbLOCATION.Text = CType(Trim(mlREADER("Location") & ""), String)
                lbPERIOD.Text = CType(Trim(mlREADER("BVMonth") & ""), String)

                lbADDR.Text = CType(Trim(mlREADER("Do_Address") & ""), String)
                lbCITY.Text = CType(Trim(mlREADER("Do_City") & ""), String)
                lbPROVINCE.Text = CType(Trim(mlREADER("Do_State") & ""), String)
                lbZIP.Text = CType(Trim(mlREADER("Do_Zip") & ""), String)
                lbPHONE1.Text = CType(Trim(mlREADER("DO_Phone1") & ""), String)
                lbPHONE_PIC.Text = CType(Trim(mlREADER("PIC_ContactNo") & ""), String)
                lbDEPTCODE.Text = CType(Trim(mlREADER("DeptCode") & ""), String)
                
                lbPREPARED1.Text = CType(Trim(mlREADER("PostingUserID1") & "") & "-" & Trim(mlREADER("PostingName1") & ""), String)
                If IsDBNull(mlREADER("PostingDate1")) = False Then lbPREPARED_DATE1.Text = mlOBJGF.ConvertDateIntltoIDN(Replace(mlREADER("PostingDate1"), "-", "/") & "", "/")
                lbPREPARED2.Text = CType(Trim(mlREADER("PostingUserID2") & "") & "-" & Trim(mlREADER("PostingName2") & ""), String)
                If IsDBNull(mlREADER("PostingDate2")) = False Then lbPREPARED_DATE2.Text = mlOBJGF.ConvertDateIntltoIDN(Replace(mlREADER("PostingDate2"), "-", "/") & "", "/")
                lbPREPARED3.Text = CType(Trim(mlREADER("PostingUserID3") & "") & "-" & Trim(mlREADER("PostingName3") & ""), String)
                If IsDBNull(mlREADER("PostingDate3")) = False Then lbPREPARED_DATE3.Text = mlOBJGF.ConvertDateIntltoIDN(Replace(mlREADER("PostingDate3"), "-", "/") & "", "/")


                Select Case mlMRUSERLEVEL
                    Case "2", "3"
                        lbTOTAL.Text = CType(Trim(mlREADER("TotalAmount") & ""), Double).ToString("n")
                        TR1.Visible = True
                End Select

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
            Select Case mlMRUSERLEVEL
                Case "1", "2"
                    mlSQL2 = "SELECT Linno AS No,ItemKey as Kode,Description as Nama_Item,Uom as Satuan,Qty as Kebutuhan," & _
                        " Qty_Bal as Saldo_di_Area,RequestDesc as Permintaan,Description2 as Keterangan" & _
                        " FROM AP_MR_REQUESTDT" & mlTABLELOG & "  WHERE DocNo = '" & mlKEY & "' " & mlSQLCOUNTER
                Case "3"
                    mlSQL2 = "SELECT Linno AS No,ItemKey as Kode,Description as Nama_Item,Uom as Satuan,Qty as Kebutuhan," & _
                        " Qty_Bal as Saldo_di_Area,RequestDesc as Permintaan,Description2 as Keterangan," & _
                        " UnitPrice as HrgSatuan,TotalPrice as Total FROM AP_MR_REQUESTDT" & mlTABLELOG & " WHERE DocNo = '" & mlKEY & "' " & mlSQLCOUNTER
            End Select
            If mlSQL2 <> "" Then
                mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISSP3")
                mlDATAGRID.DataSource = mlREADER2
                mlDATAGRID.DataBind()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Sub RetrieveFieldsDetail2()
        Try
            mlSQL2 = "SELECT Linno as No,ItemKey as Code,Description,fSize as Size,Qty " & _
                        " FROM AP_MR_REQUESTDT2" & mlTABLELOG & " WHERE DocNo = '" & mlKEY & "' " & mlSQLCOUNTER

            If mlSQL2 <> "" Then
                mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISSP3")
                mlDATAGRID2.DataSource = mlREADER2
                mlDATAGRID2.DataBind()
            End If
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
            Case "jpg", "jpg", "jpe"
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

    Protected Sub mlDATAGRID_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles mlDATAGRID.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'mlI = 6
            'Dim mlPOINT1 As Double = Convert.ToDouble(E.Item.Cells(mlI).Text)
            'E.Item.Cells(mlI).Text = mlPOINT1.ToString("n")
            'E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

            mlI = 4
            Dim mlPOINT4 As Double = Convert.ToDouble(e.Item.Cells(mlI).Text)
            e.Item.Cells(mlI).Text = mlPOINT4.ToString("n")
            e.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

            mlI = 5
            Dim mlPOINT5 As Double = Convert.ToDouble(e.Item.Cells(mlI).Text)
            e.Item.Cells(mlI).Text = mlPOINT5.ToString("n")
            e.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

            Select Case mlMRUSERLEVEL
                Case "3"
                    mlI = 8
                    Dim mlPOINT8 As Double = Convert.ToDouble(e.Item.Cells(mlI).Text)
                    e.Item.Cells(mlI).Text = mlPOINT8.ToString("n")
                    e.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

                    mlI = 9
                    Dim mlPOINT9 As Double = Convert.ToDouble(e.Item.Cells(mlI).Text)
                    e.Item.Cells(mlI).Text = mlPOINT9.ToString("n")
                    e.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right
            End Select
        End If
    End Sub

    Protected Sub mlDATAGRID2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles mlDATAGRID2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            mlI = 4
            Dim mlPOINT4 As Double = Convert.ToDouble(e.Item.Cells(mlI).Text)
            e.Item.Cells(mlI).Text = mlPOINT4.ToString("n")
            e.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right
        End If
    End Sub
End Class

