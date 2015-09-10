
Partial Class mm_logout
    Inherits System.Web.UI.Page
    Dim mlOBJGS As New IASClass.ucmGeneralSystem

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Member Logout V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Member Logout V2.00"

        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "mm_logout", "Member Logout", "")
        End If

        Session.Clear()
        mlMESSAGE.Text = "Anda telah berhasil Logout"
    End Sub
End Class
