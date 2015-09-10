Imports System.Data.OleDb
Imports System.Data
Imports System.IO
Imports IAS.Core.CSCode
Partial Class backoffice_mk_doc_news
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
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "News Directory Doc V2.02"
        mlMEMBERGROUP = System.Configuration.ConfigurationManager.AppSettings("mgMEMBERGROUPMENU")
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlKEY = Request.QueryString("mpID")
        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "mk_doc_news", "Document News", "")
        End If
        
        RetrieveFields()
        RetrieveCompanyInfo()

    End Sub

    Public Sub RetrieveFields()
        Try
            mlSQL = "SELECT * FROM MK_NEWS WHERE DocNo='" & mlKEY & "' "
            mlREADER = mlOBJGS.DbRecordset(mlSQL)
            If mlREADER.HasRows Then
                mlREADER.Read()

                Select Case mlREADER("Type").ToString
                    Case "B", ""
                        lbTITLE.Text = "BERITA"
                    Case "P"
                        lbTITLE.Text = "PENGUMUMAN"
                End Select

                lbDOCNO.Text = CType(mlREADER("DocNo"), String)
                'lbDOCDATE.Text = CType(mlREADER("DocDate"), String)
                lbDOCDATE.Text = mlOBJGF.ConvertDateIntltoIDN(Replace(mlREADER("DocDate"), "-", "/") & "", "/")

                lbSUBJECT.Text = CType(mlREADER("Subject"), String)
                'lbHEADER.Text = CType(mlREADER("Header"), String)
                lbDESC1.Text = CType(IIf(mlOBJGF.IsNone(mlREADER("Description")) = True, "", mlREADER("Description")), String)
                lbDESC2.Text = CType(IIf(mlOBJGF.IsNone(mlREADER("Description2")) = True, "", mlREADER("Description2")), String)
                lbDESC3.Text = CType(IIf(mlOBJGF.IsNone(mlREADER("Description3")) = True, "", mlREADER("Description3")), String)
                lbDESC4.Text = CType(IIf(mlOBJGF.IsNone(mlREADER("Description4")) = True, "", mlREADER("Description4")), String)
                imgPIC1.ImageUrl = CType(IIf(mlOBJGF.IsNone(mlREADER("ImagePath3")) = True, "", mlREADER("ImagePath3")), String)
                imgPIC2.ImageUrl = CType(IIf(mlOBJGF.IsNone(mlREADER("ImagePath4")) = True, "", mlREADER("ImagePath4")), String)
            Else
                mlKEY = "33"
                mlKEY = mlOBJGF.Encrypt(mlKEY)
                'Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=" & mlKEY)
            End If

        Catch ex As Exception
            mlKEY = "33"
            mlKEY = mlOBJGF.Encrypt(mlKEY)
            'Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=" & mlKEY)
        End Try
    End Sub

    Sub RetrieveFieldsDetail()
        mlSQL2 = "SELECT * FROM AR_INVDT WHERE DocNo = '" & mlKEY & "' ORDER BY LinNo"
        mlREADER2 = mlOBJGS.DbRecordset(mlSQL2)
        mlDATAGRID.DataSource = mlREADER2
        mlDATAGRID.DataBind()
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


End Class
