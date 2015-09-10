Imports System.Data.OleDb
Imports System.Data
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
    Dim mlKEY As String
    Dim mlMEMBERGROUP As String

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        CekSession()
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "File Directory Doc V2.02"
        mlMEMBERGROUP = System.Configuration.ConfigurationManager.AppSettings("mgMEMBERGROUPMENU")
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlKEY = Request.QueryString("mpID")
        'mlKEY = "CT/00000015"

        lbTITLE.Text = "CONTRACT MANAGEMENT"
        lbTITLE2.Text = ""
        RetrieveFields()
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

            mlSQL = "SELECT * FROM CT_CONTRACTHR WHERE DocNo = '" & Trim(mlKEY) & "'"
            mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3")
            If mlREADER.HasRows Then
                mlREADER.Read()
                lbTDOCUMENTNO.Text = mlREADER("DocNo") & ""
                lbTDOCDATE.Text = IIf(mlOBJGF.IsNone(mlREADER("DocDate")), "", mlOBJGF.ConvertDateIntltoIDN(mlREADER("DocDate"), "/") & "")
                lbTCUST.Text = mlREADER("CustID") & ""
                lbCUSTDESC.Text = mlREADER("CustName") & ""
                lbTSITECARD.Text = mlREADER("SiteCardID") & ""
                lbSITEDESC.Text = mlREADER("SiteCardName") & ""
                lbTADDR.Text = mlREADER("Address") & ""
                lbTCITY.Text = mlREADER("City") & ""

                lbtSTATE.Text = mlREADER("State") & ""
                lbTCOUNTRY.Text = mlREADER("Country") & ""

                lbTZIP.Text = mlREADER("Zip") & ""
                lbTPHONE1.Text = mlREADER("Phone1") & ""
                lbTPHONE1_PIC.Text = mlREADER("PIC_ContactNo") & ""
                lbTCTDOCNO.Text = mlREADER("ContractNo") & ""
                lbTDOCDATE2.Text = IIf(mlOBJGF.IsNone(mlREADER("ContractDate")), "", mlOBJGF.ConvertDateIntltoIDN(mlREADER("ContractDate"), "/") & "")
                lbTREFFNO.Text = mlREADER("ReffNo") & ""
                lbREFFDOCNO.Text = mlREADER("ReffDocNo") & ""
                lbTCRDOCDATE1.Text = IIf(mlOBJGF.IsNone(mlREADER("StartDate")), "", mlOBJGF.ConvertDateIntltoIDN(mlREADER("StartDate"), "/") & "")
                lbTCRDOCDATE2.Text = IIf(mlOBJGF.IsNone(mlREADER("EndDate")), "", mlOBJGF.ConvertDateIntltoIDN(mlREADER("EndDate"), "/") & "")

                lbTPRODUCT.Text = mlREADER("ServiceType") & ""

                lbTMANPOWER.Text = mlREADER("EmployeeQty") & ""
                lbTPERCENTAGE.Text = mlREADER("IncrementPercent") & ""
                lbTPRICE2.Text = mlREADER("ExistingPrice") & ""
                lbTPRICE3.Text = mlREADER("ProposePrice") & ""
                lbTPRICE.Text = mlREADER("Price") & ""
                lbTUSERID.Text = mlREADER("Negotiator") & ""
                lbTBRANCH.Text = mlREADER("SC_Branch") & ""
                lbBRANCH.Text = mlREADER("SC_BranchName") & ""
                lbTDESCRIPTION.Text = mlREADER("Description") & ""
                lbFILEDOCNO.Value = mlREADER("FileDocNo") & ""

                lbTMANPOWER.Text = Convert.ToDouble(lbTMANPOWER.Text).ToString("n")
                lbTPERCENTAGE.Text = Convert.ToDouble(lbTPERCENTAGE.Text).ToString("n")
                lbTPRICE2.Text = Convert.ToDouble(lbTPRICE2.Text).ToString("n")
                lbTPRICE3.Text = Convert.ToDouble(lbTPRICE3.Text).ToString("n")
                lbTPRICE.Text = Convert.ToDouble(lbTPRICE.Text).ToString("n")


                RetrieveFieldsDetail2("")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Sub RetrieveFieldsDetail2(ByVal mpSQL As String)
        Try

            If mpSQL = "" Then
                mlSQL2 = "SELECT DocNo,Linno as No,FileDesc as File_Name FROM XM_FILEDT" & _
                    " WHERE DocNo='" & lbFILEDOCNO.Value & "'"
            Else
                mlSQL2 = mpSQL
            End If
            mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISSP3")
            mlDATAGRID2.DataSource = mlREADER2
            mlDATAGRID2.DataBind()

            mlOBJGS.CloseFile(mlREADER2)


        Catch ex As Exception


        End Try

    End Sub

    Sub RetrieveFieldsDetail()

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
            Case "lbTt"
                mlFILE_MIMETYPE = "text/plain"
            Case "jpg", "jpg", "jpe"
                mlFILE_MIMETYPE = "image/jpeg"
            Case "gif"
                mlFILE_MIMETYPE = "image/gif"
            Case "png"
                mlFILE_MIMETYPE = "image/png"
            Case "pdf"
                mlFILE_MIMETYPE = "application/pdf"
            Case "ppt", "pplbT"
                mlFILE_MIMETYPE = "application/vnd.ms-powerpoint"
            Case "xls", "xlsx"
                mlFILE_MIMETYPE = "application/vnd.ms-excel"
            Case "doc", "docx"
                mlFILE_MIMETYPE = "application/msword"

        End Select
        Return mlFILE_MIMETYPE

    End Function

End Class

