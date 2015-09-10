<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="ut_securityq_c.aspx.vb" Inherits="ad_resetpwd" %>
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
            <td width="25%" valign="top">Pertanyaan 1</td>
            <td width="5%" valign="top">:</td>
            <td width="70%" valign="top">
                <asp:TextBox ID="mpQ1" runat="server" Width="400" MaxLength="254"></asp:TextBox>                
            </td>
        </tr>
        
        <tr>
            <td width="25%" valign="top">Jawaban 1</td>
            <td width="5%" valign="top">:</td>
            <td width="70%" valign="top">
                <asp:TextBox ID="mpA1" runat="server" Width="400" MaxLength="254"></asp:TextBox>                
            </td>
        </tr>
        
        <tr><td colspan="3"><br /></td></tr>
        
        
        <tr>
            <td width="25%" valign="top">Pertanyaan 2</td>
            <td width="5%" valign="top">:</td>
            <td width="70%" valign="top">
                <asp:TextBox ID="mpQ2" runat="server" Width="400" MaxLength="254"></asp:TextBox>                
            </td>
        </tr>
        
        <tr>
            <td width="25%" valign="top">Jawaban 2</td>
            <td width="5%" valign="top">:</td>
            <td width="70%" valign="top">
                <asp:TextBox ID="mpA2" runat="server" Width="400" MaxLength="254"></asp:TextBox>                
            </td>
        </tr>
        
        <tr><td colspan="3"><br /></td></tr>
        
        
        
        <tr>
            <td width="25%" valign="top">Pertanyaan 3</td>
            <td width="5%" valign="top">:</td>
            <td width="70%" valign="top">
                <asp:TextBox ID="mpQ3" runat="server" Width="400" MaxLength="254"></asp:TextBox>                
            </td>
        </tr>
        
        <tr>
            <td width="25%" valign="top">Jawaban 3</td>
            <td width="5%" valign="top">:</td>
            <td width="70%" valign="top">
                <asp:TextBox ID="mpA3" runat="server" Width="400" MaxLength="254"></asp:TextBox>                
            </td>
        </tr>
        
        <tr><td colspan="3"><br /></td></tr>
        
         <tr>
            <td width="25%" valign="top">Pertanyaan 4</td>
            <td width="5%" valign="top">:</td>
            <td width="70%" valign="top">
                <asp:TextBox ID="mpQ4" runat="server" Width="400" MaxLength="254"></asp:TextBox>                
            </td>
        </tr>
        
        <tr>
            <td width="25%" valign="top">Jawaban 4</td>
            <td width="5%" valign="top">:</td>
            <td width="70%" valign="top">
                <asp:TextBox ID="mpA4" runat="server" Width="400" MaxLength="254"></asp:TextBox>                
            </td>
        </tr>
        
        <tr><td colspan="3"><br /></td></tr>
        
        
         <tr>
            <td width="25%" valign="top">Pertanyaan 5</td>
            <td width="5%" valign="top">:</td>
            <td width="70%" valign="top">
                <asp:TextBox ID="mpQ5" runat="server" Width="400" MaxLength="254"></asp:TextBox>                
            </td>
        </tr>
        
        <tr>
            <td width="25%" valign="top">Jawaban 5</td>
            <td width="5%" valign="top">:</td>
            <td width="70%" valign="top">
                <asp:TextBox ID="mpA5" runat="server" Width="400" MaxLength="254"></asp:TextBox>                
            </td>
        </tr>
        
        <tr><td colspan="3"><br /></td></tr>
        
    </table>    
    <hr />
</asp:Panel>
</asp:TableCell>
</asp:TableRow>


</asp:Table>

</form>

<br /><br /><br /><br />
</asp:Content>

