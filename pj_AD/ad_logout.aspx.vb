
Partial Class ad_logout
    Inherits System.Web.UI.Page
    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Admin Logout V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Admin Logout V2.00"

        mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ad_logout", "LogOut", "")

        Session.Clear()
        mlMESSAGE.Text = "Anda telah berhasil Logout"
    End Sub
End Class
