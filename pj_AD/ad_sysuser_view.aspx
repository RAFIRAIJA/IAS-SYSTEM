<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="ad_sysuser_view.aspx.vb" Inherits="ad_sysuser_view" title="User Maintenance V2.02" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
<form id="form1" runat="server">

<asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">

<asp:TableRow>   
<asp:TableCell> 
<asp:Panel ID="pnTOOLBAR" runat="server">  
    <table border="0" cellpadding="3" cellspacing="3">
        <tr>
            
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
<asp:TableCell>
<asp:Panel ID="pnFILL" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">                 
        <tr>
            <td><p>UserID</p></td>
            <td><asp:label ID="mlUSERID" runat="server" /></td>            
        </tr>
        
        
        <tr>
            <td><p>Nama</p></td>
            <td><asp:label ID="mlNAMA" width="400" runat="server" /></td>            
        </tr>
        
        <tr>
            <td><p>Departement</p></td>            
            <td><asp:label ID="mlDEPT" width="400" runat="server" /></td>
        </tr>
        
        <tr>
            <td><p>Status</p></td>            
            <td><asp:label ID="mlSTATUS" width="400" runat="server" /></td>
        </tr>
        
        <tr>
            <td><p>No Mobile</p></td>
            <td><asp:label ID="mlMOBILENO" runat="server" /></td>            
        </tr>
        
        <tr>
            <td><p>Email</p></td>
            <td><asp:label ID="mlEMAIL" runat="server" /></td>            
        </tr>
        
        <tr>
            <td><p>No Pegawai</p></td>
            <td><asp:label ID="mlSTAFFID" runat="server" /></td>            
        </tr>
        
        <tr>
            <td><p>No Absensi</p></td>
            <td><asp:label ID="mlABSTAINID" runat="server" /></td>            
        </tr>
        
        <tr>
            <td><p>No Aplikasi</p></td>
            <td><asp:label ID="mlAPPLICATIONID" runat="server" /></td>            
        </tr>     
        
        <tr>
            <td><p>Nama Perusahaan</p></td>
            <td><asp:label ID="mlCOMPANY" runat="server" /></td>            
        </tr>     
        
        <tr>
            <td><p>Cabang</p></td>
            <td><asp:label ID="mlBRANCH" runat="server" /></td>            
        </tr>     
        
    </table>    
    <hr />
</asp:Panel>
</asp:TableCell>
</asp:TableRow>


<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnGRID" runat="server">    
    
    <asp:DataGrid runat="server" ID="mlDATAGRID"     
    OnItemCommand="mlDATAGRID_ItemCommand"    
    autogeneratecolumns="false"
    CssClass="Grid"
    >	    
    
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




