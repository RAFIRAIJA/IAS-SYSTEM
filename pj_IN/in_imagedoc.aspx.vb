Imports System.Data.OleDb
Imports System.Data
Imports System.IO
Imports IAS.Core.CSCode
Partial Class in_imagedoc
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
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Product Image Doc V2.02"
        mlMEMBERGROUP = System.Configuration.ConfigurationManager.AppSettings("mgMEMBERGROUPMENU")
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlKEY = Request.QueryString("mpID")
        RetrieveFields()
        RetrieveCompanyInfo()

        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "in_imagedoc", "Product Image Doc", "")
        End If

    End Sub

    Public Sub RetrieveFields()
        Dim mlFILEPATH As String
        Dim mlFILEPATH2 As String
        Dim mlFILENAME As String
        Dim mlFILEEXT As String

        Try

            mlSQL = "SELECT * FROM IN_INMAST_ADDINFO WHERE ItemKey='" & mlKEY & "' "
            mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3")
            If mlREADER.HasRows Then
                mlREADER.Read()

                lbITEMKEY.Text = CType(mlREADER("ItemKey"), String)
                lbITEMDESC.Text = CType(mlREADER("Description"), String)

                mlFILEPATH = CType(mlREADER("ImagePath1T"), String)
                mlFILENAME = mlOBJGF.GetStringAtPosition(mlFILEPATH, 3, "/")
                mlFILEEXT = mlOBJGF.GetStringAtPosition(Right(mlFILEPATH, 5), "1", ".")
                mlFILEPATH2 = Server.MapPath(mlFILEPATH)
                mpIMAGE.ImageUrl = mlFILEPATH

                lbTITLE.Text = "Product Image for " & lbITEMDESC.Text
            Else
                mlMESSAGE.Text = "No Image Found for " & mlKEY
            End If

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

