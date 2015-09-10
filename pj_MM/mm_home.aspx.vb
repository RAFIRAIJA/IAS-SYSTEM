
Partial Class mm_home
    Inherits System.Web.UI.Page
    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "DashBoard V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "DashBoard V2.00"

        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "mm_home", "Member Home", "")
        End If
    End Sub
End Class
