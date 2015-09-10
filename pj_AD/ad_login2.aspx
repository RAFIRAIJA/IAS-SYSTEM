<%@ Page Language="VB" MasterPageFile="~/pagesetting/Masterlogin2.master" AutoEventWireup="false" CodeFile="ad_login2.aspx.vb" Inherits="ad_Login2"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">

<form id="form1" runat="server">
<%--
<asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">

    <asp:TableRow><asp:TableCell>&nbsp;</asp:TableCell></asp:TableRow>
    <asp:TableRow>
    <asp:TableCell BorderWidth="0">
    <asp:Panel ID="pnFILL" runat="server">
    <table width="30%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td colspan="2" align="center">
                <p class="header1"><b><asp:Label id="mlTITLE" runat="server"></asp:Label></b></p>
                <asp:HiddenField ID="mlSYSCODE" runat="server"/>                
            </td>                        
        </tr>        
        <tr>
            <td colspan="2"><p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p></td>                 
        </tr>  
        <tr>
            <td valign="top" align="center">
                <img src="../images/login/key.gif" alt="login" />
            </td>

            <td align="center">            
                    <table  border="0">                    
                        <tr>
                            <td>
                                <asp:Label ID="mpUSERIDL" runat="server">UserID</asp:Label>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="mpUSERID" runat="server" Width="120px"> </asp:TextBox>
                            </td>
                        </tr>                    
                        <tr>
                            <td>
                                <asp:Label ID="mpPASSWORDL" runat="server">Password</asp:Label>
                            </td>
                            <td></td>
                            <td>
                                <asp:TextBox runat="server" ID="mpPASSWORD" TextMode="Password" Width="120px"></asp:TextBox>
                            </td>
                        </tr>                    
                        <tr>
                            <td>
                                <asp:Label ID="lbIMAGETEXT" runat="server">Image Text</asp:Label>
                            </td>
                            <td></td>
                            <td>
                                <asp:TextBox runat="server" ID="mpIMAGETEXT" Width="120px"></asp:TextBox>                            
                            </td>
                        </tr>                    
                        <tr>
                            <td colspan="3" align="center">
                                <br />
                                <asp:Image ID="mpVIMAGE" runat="server" BorderWidth="2px" BorderColor="Red"  Width="100px" />
                                <asp:ImageButton ID="btRELOAD" ImageUrl="~/images/toolbar/reload_20.gif" ToolTip="refresh Image" runat="server" />                            
                            </td>
                        </tr>                    
                        <tr>                                                
                            <td align="center" colspan="3">
                                <asp:Button ID="mlSUBMIT" runat="server" Width="100"  Text="Login" />
                            </td>
                        </tr>
                        
                        <tr>                       
                            <td align="center" colspan="3">
                                <i><a href="../utility/ut_forgotpswd.aspx?mpFP=A">Problem with Login, Click Here</a></i>
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
--%>
<asp:Panel ID="pnlLogin" runat="server" Width="100%">
    <table border="0" cellpadding="2" cellspacing="1" runat="server" width="100%">
        <tr>                       
            <td colspan="3" align="center" >
                <b><asp:Label id="mlTITLE" runat="server" ForeColor="#5377A9" Font-Size="Medium"></asp:Label></b>
                <asp:HiddenField ID="mlSYSCODE" runat="server"/> 
            </td>
            <td width="10%" align="center" />
        </tr>
        <tr>                      
            <td colspan="3" align="center">
                <p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p>
            </td>            
        </tr>
        <tr>
            <td rowspan="4" width="30%" align="right" valign="middle">
                <img src="../images/login/key.gif" alt="login" />
            </td>
        </tr>
        <tr>
            <td width="15%" align="center">
                <asp:Label ID="mpUSERIDL" runat="server">UserID</asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="mpUSERID" runat="server" Width="150px"> </asp:TextBox>
            </td>
            <td width="10%" align="center" />
        </tr>
        <tr>
            <td width="15%" align="center">
                 <asp:Label ID="mpPASSWORDL" runat="server">Password</asp:Label>
            </td>
            <td align="left">
                 <asp:TextBox runat="server" ID="mpPASSWORD" TextMode="Password" Width="150px"></asp:TextBox>
            </td>
            <td width="10%" align="center" />
        </tr>
        <tr>
            <td width="15%" align="center">
                <asp:Label ID="lbIMAGETEXT" runat="server">Image Text</asp:Label>
            </td>            
            <td align="left">
                <asp:TextBox runat="server" ID="mpIMAGETEXT" Width="150px"></asp:TextBox>                            
            </td>
            <td width="10%" align="center" />
        </tr>
        <tr>
            <td colspan="3" align="center">
                <br />
                <asp:Image ID="mpVIMAGE" runat="server" BorderWidth="2px" BorderColor="Red"  Width="100px" />&nbsp;&nbsp;
                <asp:ImageButton ID="btRELOAD" ImageUrl="~/images/toolbar/reload_20.gif" ToolTip="refresh Image" runat="server" />                            
            </td>
        </tr>                    
        <tr>                                                                        
            <td colspan="3" align="center" >
                <asp:Button ID="mlSUBMIT" runat="server" Width="100"  Text="Login" />
            </td>
            <td width="10%" align="center" />
        </tr>        
        <tr>                                              
            <td colspan="3" align="center" >
                <i><a href="../utility/ut_forgotpswd.aspx?mpFP=A">Problem with Login, Click Here</a></i>
            </td>
            <td width="10%" align="center" />
        </tr>
    </table> 

</asp:Panel>
</form>
</asp:Content>



