<%@ Page Language="VB" MasterPageFile="~/pagesetting/Masterlogin.master" AutoEventWireup="false" CodeFile="xm_login.aspx.vb" Inherits="xm_login"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
<form id="form1" runat="server">

<asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">
<asp:TableRow><asp:TableCell>&nbsp;</asp:TableCell></asp:TableRow>
<asp:TableRow>
<asp:TableCell BorderWidth="0">
<asp:Panel ID="pnFILL" runat="server">


<table width="30%" cellpadding="0" cellspacing="0" border="0">
    <tr>
    <td colspan="2" align="center"><p class="header1"><b><asp:Label id="mlTITLE" runat="server"></asp:Label></b></p></td>                        
    </tr>        

    <tr>
    <td colspan="2"><p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p></td>                 
    </tr>  

    <tr>
        <td>
            <img src="../images/login/key.gif" alt="login" />
        </td>

        <td align="center">            
                <table  border="0">                    
                    <tr>
                        <td>
                            <asp:Label ID="mpUSERIDL" runat="server">UserID</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="mpUSERID" runat="server" Width="120px"> </asp:TextBox>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <asp:Label ID="mpPASSWORDL" runat="server">Password</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="mpPASSWORD" TextMode="Password" Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                        </td>
                        <td align="center">
                            <asp:Button ID="mlSUBMIT" runat="server" Text="Login" />
                        </td>
                    </tr>
                    
                    <tr>                       
                        <td align="center" colspan="2">
                            <i><a href="../utility/ut_forgotpswd.aspx?mpFP=M">Forgot Password, Click Here</a></i>
                        </td>
                    </tr>
                    
                    
                </table>
            
        </td>
    </tr>
</table>        
    
</asp:Panel>
</asp:TableCell>
</asp:TableRow>

</asp:Table>

</form>

<br /><br /><br /><br />
</asp:Content>




