
Partial Class backoffice_mm_pagetransition
    Inherits System.Web.UI.Page
    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlCOMPANYCODE As String
    Dim mlFUNCTIONPARAMETER As String
    Dim mlFUNCTIONDATA As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlCOMPANYCODE = UCase(Trim(System.Configuration.ConfigurationManager.AppSettings("mgCOMPANYID")))

        If Page.IsPostBack = False Then
            mlFUNCTIONPARAMETER = Request("mpFP")
            mlFUNCTIONDATA = Request("mpFD")

            Select Case mlCOMPANYCODE
                Case "LFA"

            End Select
        End If
    End Sub
End Class
