<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterGeneral.master" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="utility_ut_forgotpswd" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
<form id="form1" runat="server">
<asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">

<asp:TableRow><asp:TableCell><p class="header1"><b><asp:Label id="mlTITLE" runat="server"></asp:Label></b></p></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><asp:HiddenField ID="mlSYSCODE" runat="server"/></asp:TableCell></asp:TableRow>

<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnFILL" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">                 
        
        <tr id="mpTR" runat="server">
            <td><p>Nomor Induk Karyawan (NIK)</p></td>
            <td><p>:</p></td>
            <td><asp:TextBox ID="mpUSERID" runat="server" Width="300px"  /></td>
        </tr>
        
        <tr id="Tr1" runat="server">
            <td><p>Dept</p></td>
            <td><p>:</p></td>
            <td><asp:DropDownList id="ddDEPT" runat="server"></asp:DropDownList></td>
        </tr>
        
        <tr>
            <td colspan="3"><asp:Button id="mpBTSUBMIT" Text="Submit" runat="server" /></td>
        </tr>
    </table>    
    <hr />
</asp:Panel>
</asp:TableCell>
</asp:TableRow>

</asp:Table>

</form>

<br /><br /><br /><br />
</asp:Content>
