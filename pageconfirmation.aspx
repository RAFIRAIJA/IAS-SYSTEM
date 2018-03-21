<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterGeneral.master" AutoEventWireup="false" CodeFile="pageconfirmation.aspx.vb" Inherits="pageconfirmation"  %>

<script language="vbscript"  runat="server">
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlDESC As String
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "System Confirmation V2.00"

        mlDESC = Request("mpMESSAGE")
        mlDESC = mlOBJGF.Decrypt(Request("mpMESSAGE"))
        Select Case mlDESC
            Case ""
                mlDESC = "Sorry, we have error in this Page"
            Case "31"
                mlDESC = "Sorry, Your Session was Time Out, Please Re Login"
            Case "32"
                mlDESC = "Sorry, you are not authorize to access this menu, please contact your system administrator"
            Case "33"
                mlDESC = "Sorry, the page you try to find are not available"
            Case "34"
                mlDESC = "Sorry, the page that you try was expired"
            Case Else
                mlDESC = mlDESC
        End Select
        mlMESSAGE.Text = mlDESC
    End Sub
</script>


<asp:Content ID="Content2" ContentPlaceHolderID="mpCONTENT" Runat="Server">
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td valign="top" align="center" style="width: 167px">
    <img src="images/system/stop.png" />
</td>
    
<td valign="top" align="left">
    <table width="50%" cellpadding="0" cellspacing="0" border="0">
    <tr>
    <td colspan="2" align="left"><p><b>System Confirmation V2.00</b></p></td>                        
    </tr>        

    <tr>
    <td colspan="2" align="left"><p>
    <asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label>
    </p></td>
    </tr>  
    </table>

</td>
</tr>
</table>


</asp:Content>

