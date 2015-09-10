<%@ Page Language="VB" MasterPageFile="~/pagesetting/Masterintern.master" AutoEventWireup="false" CodeFile="ut_sendemail.aspx.vb" Inherits="utility_ut_sendemail"  %>

<asp:Content ID="Content2" ContentPlaceHolderID="mpCONTENT" Runat="Server">
<form id="mpFORM" runat="server">


<asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">
<asp:TableRow>   
<asp:TableCell> 
<asp:Panel ID="pnTOOLBAR" runat="server"> 
    <asp:ImageButton id="btEmail" ToolTip="Email" ImageUrl="~/images/toolbar/email.gif" runat="server" OnClientClick="return confirm('Email Now ?');" />
    <asp:ImageButton id="btEmail2" ToolTip="Email with Class" ImageUrl="~/images/toolbar/email.gif" runat="server" OnClientClick="return confirm('Email Now ?');" />

</asp:Panel>
</asp:TableCell>    
</asp:TableRow>

<asp:TableRow><asp:TableCell><p class="header1"><b><asp:Label id="mlTITLE" runat="server"></asp:Label></b></p></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><asp:HiddenField ID="mlSYSCODE" runat="server"/></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><p><asp:HyperLink ID="mlLINKDOC" runat="server"></asp:HyperLink></p></asp:TableCell></asp:TableRow>


<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnFILL" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">                 
        <tr>
            <td><p><asp:Label ID="lbEMAILF" Text="From Email" runat="server"></asp:Label></p></td>
            <td>:</td>
            <td><asp:TextBox ID="txEMAILF" runat="server" Width="150"> </asp:TextBox></td>
        </tr>
        
        <tr>
            <td><p><asp:Label ID="lbEMAIL" Text="To Email" runat="server"></asp:Label></p></td>
            <td>:</td>
            <td><asp:TextBox ID="txEMAIL" runat="server" Width="350"> </asp:TextBox></td>
        </tr>
        
        <tr>
            <td><p><asp:Label ID="lbCC" Text="CC" runat="server"></asp:Label></p></td>
            <td>:</td>
            <td><asp:TextBox ID="txCC" runat="server" Width="350"> </asp:TextBox></td>
        </tr>
        
        <tr>
            <td><p><asp:Label ID="lbBCC" Text="BCC" runat="server"></asp:Label></p></td>
            <td>:</td>
            <td><asp:TextBox ID="txBCC" runat="server" Width="350"> </asp:TextBox></td>
        </tr>
        
        <tr>
            <td><p><asp:Label ID="lbSUBJECT" Text="Subject" runat="server"></asp:Label></p></td>
            <td>:</td>
            <td><asp:TextBox ID="txSUBJECT" runat="server" Width="400"> </asp:TextBox></td>
        </tr>
                 
         
        <tr valign="top">
            <td valign="top"><asp:Label ID="lbBODY" Text="Contains" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top"><asp:TextBox ID="txBODY" runat="server" Width="400" Height="80" TextMode="MultiLine"></asp:TextBox></td>
        </tr> 
                     
                     
        <tr>
            <td valign="top"></td>
            <td valign="top"></td>
            <td align="left"></td>
        </tr> 
       
        
    </table>    
    <hr /><br />
</asp:Panel>
</asp:TableCell>
</asp:TableRow>


<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnGRID" runat="server">        
    <asp:DataGrid runat="server" ID="mlDATAGRID" 
    CssClass="Grid"
    autogeneratecolumns="true">	    
    
    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
    <ItemStyle CssClass="GridItem"></ItemStyle>
    <EditItemStyle  CssClass="GridItem" />
    <PagerStyle  CssClass="GridItem" />
    <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
    <Columns>  
    </Columns>
 </asp:DataGrid>  
</asp:Panel>
</asp:TableCell>
</asp:TableRow>


</asp:Table>
</form>
<br /><br /><br /><br />

</asp:Content>

