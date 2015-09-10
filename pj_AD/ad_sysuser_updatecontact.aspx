<%@ Page Language="VB" MasterPageFile="~/pagesetting/MsPageBlank.master" AutoEventWireup="false" CodeFile="ad_sysuser_updatecontact.aspx.vb" Inherits="ad_sysuser_updatecontact" %>
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
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td valign="top">Email Address</td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="txEMAIL" runat="server" width="450"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txEMAIL"
                    Text="Email Address not allowed empty"></asp:RequiredFieldValidator>
            </td>
        </tr>
        
        <tr>
            <td valign="top">Handphone No</td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="txMOBILENO" runat="server" width="450"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequiredFieldValidator" runat="server" ControlToValidate="txMOBILENO"
                    Text="Handphone No not allowed empty"></asp:RequiredFieldValidator>
            </td>
        </tr>
        
        <tr>            
            <td colspan="3"> <br /><i>Gunakan Komma (,) bila alamat email lebih dari satu</i></td>
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

