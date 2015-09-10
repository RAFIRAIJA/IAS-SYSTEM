Imports System.Data.OleDb
Imports System.Data
Imports System.IO
Imports IAS.Core.CSCode
Partial Class mk_doc_file
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
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "File Directory Doc V2.02"
        mlMEMBERGROUP = System.Configuration.ConfigurationManager.AppSettings("mgMEMBERGROUPMENU")
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlKEY = Request.QueryString("mpID")
        'mlKEY = "MKF000002"

        RetrieveFields()
        RetrieveCompanyInfo()

        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "mk_doc_file", "Document File", "")
        End If

    End Sub

    Public Sub RetrieveFields()
        Dim mlFILEPATH As String
        Dim mlFILEPATH2 As String
        Dim mlFILENAME As String
        Dim mlFILEEXT As String

        Try
            mlSQL = "SELECT * FROM MK_FILE WHERE DocNo='" & mlKEY & "' "
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

                mlFILEPATH = CType(mlREADER("FilePath1"), String)
                mlFILENAME = mlOBJGF.GetStringAtPosition(mlFILEPATH, 3, "/")
                mlFILEEXT = mlOBJGF.GetStringAtPosition(Right(mlFILEPATH, 5), "1", ".")
                mlFILEPATH2 = Server.MapPath(mlFILEPATH)


                mlLINKDOC.Text = "<font Color=blue> Download your Document (Right Click * Save Target as) </font>"
                mlLINKDOC.NavigateUrl = mlFILEPATH
                mlLINKDOC.Attributes.Add("onClick", "window.open('" & mlFILEPATH2 & "','','');")

              
            Else
                mlKEY = "33"
                mlKEY = mlOBJGF.Encrypt(mlKEY)
                'Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=" & mlKEY)
            End If

        Catch ex As Exception
            mlKEY = "33"
            mlKEY = mlOBJGF.Encrypt(mlKEY)
            'Response.Write(Err.Description)
            'Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=" & mlKEY)
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

End Class

