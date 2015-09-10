<%@ Page Language="VB" MasterPageFile="~/pagesetting/Masterlogin.master" AutoEventWireup="false" CodeFile="ad_logout.aspx.vb" Inherits="ad_logout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">

<asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">



<asp:TableRow><asp:TableCell>&nbsp;</asp:TableCell></asp:TableRow>
<asp:TableRow>
<asp:TableCell BorderWidth="0">
<asp:Panel ID="pnFILL" runat="server">



<table width="100%" cellpadding="0" cellspacing="0" border="0">
    <tr>
    <td colspan="3" align="center"><p class="header1"><b><asp:Label id="mlTITLE" runat="server"></asp:Label></b></p></td>                        
    </tr>      

    
    <tr>
        <td align="center">
            <img src="../images/login/key.gif" alt="login" />
        </td>

        <td>&nbsp;&nbsp;&nbsp;</td>
        
        <td valign="top" align="left">            
            <p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p>
            <br />
            <a href="ad_login2.aspx"> Klik di sini untuk Login kembali</a>               
        </td>
    </tr>
</table>        

</asp:Panel>
</asp:TableCell>
</asp:TableRow>

</asp:Table>
</asp:Content>

