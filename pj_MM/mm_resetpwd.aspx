<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="mm_resetpwd.aspx.vb" Inherits="mm_resetpwd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
<form id="form1" runat="server">

<asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">

<asp:TableRow>   
<asp:TableCell> 
<asp:Panel ID="pnTOOLBAR" runat="server">  
    <table border="0" cellpadding="3" cellspacing="3">
        <tr>            
            <td><asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" ImageUrl="~/images/toolbar/save.jpg" runat="server"  OnClientClick="return confirm('Save Record ?');" /></td>
            <td><asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" /></td>            
        </tr>               
    </table>
    <hr />
</asp:Panel>
</asp:TableCell>    
</asp:TableRow>


<asp:TableRow><asp:TableCell><p class="header1"><b><asp:Label id="mlTITLE" runat="server"></asp:Label></b></p></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><asp:HiddenField ID="mlSYSCODE" runat="server"/></asp:TableCell></asp:TableRow>

<asp:TableRow>
<asp:TableCell BorderWidth="0">
<asp:Panel ID="pnFILL" runat="server">
    <table width="80%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td width="25%" valign="top">Password Lama</td>
            <td width="5%" valign="top">:</td>
            <td width="70%" valign="top">
                <asp:TextBox ID="mlOLDPWD" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="mlOLDPWD"
                    Text="Password Lama harus diisi"></asp:RequiredFieldValidator>
            </td>
        </tr>
        
        <tr>
            <td width="25%" valign="top">Password Baru</td>
            <td width="5%" valign="top">:</td>
            <td width="70%" valign="top">
                <asp:TextBox ID="mlNEWPWD" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequiredFieldValidator" runat="server" ControlToValidate="mlNEWPWD"
                    Text="Password Baru harus diisi"></asp:RequiredFieldValidator>
            </td>
        </tr>
        
        <tr>
            <td width="25%" valign="top">Konfirmasi Password Baru</td>
            <td width="5%" valign="top">:</td>
            <td width="70%" valign="top">
                <asp:TextBox ID="mlCNEWPWD" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="mlCNEWPWD"
                 Text="Konfirmasi Password Baru harus diisi"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" ControlToCompare="mlNEWPWD" ControlToValidate="mlCNEWPWD"
                 Text="Konfirmasi Password Baru harus sama dengan Password Baru" runat="server"></asp:CompareValidator>
            </td>
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

