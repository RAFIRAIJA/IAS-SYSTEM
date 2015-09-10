<%@ Page Language="C#" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="true" CodeFile="fa_fixedasset_list.aspx.cs" Inherits="backoffice_fa_fixedasset_list" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">

<link href="../script/calendar.css" rel="stylesheet" type="text/css" media="all" />
<script type="text/javascript" src="../script/calendar.js"></script>

<form id="form1" runat="server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"  EnableScriptLocalization="true" ID="ToolkitScriptManager1" />

<asp:Table id="mpTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">
<asp:TableRow>   
<asp:TableCell> 
<asp:Panel ID="pnTOOLBAR" runat="server">  
    <table border="0" cellpadding="3" cellspacing="3">
        <tr>            
            <td><asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" /></td>
            <td><asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" /></td>            
        </tr>               
    </table>
    <hr />
</asp:Panel>
</asp:TableCell>    
</asp:TableRow>

<asp:TableRow><asp:TableCell><br /></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><p class="header1"><b><asp:Label id="mlTITLE" runat="server"></asp:Label></b></p></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><asp:HiddenField ID="mlSYSCODE" runat="server"/></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><asp:Label id="mlSQLSTATEMENT" runat="server" Visible="False" /></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><br /></asp:TableCell></asp:TableRow>

<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnFILL" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">                 
    <tr valign="top">
        <td><asp:Label ID="lbRPTTYPE" Text="Jenis Laporan" runat="server"></asp:Label></td>
        <td>:</td>
        <td>
            <asp:DropDownList ID="mpRPTTYPE" runat="server"></asp:DropDownList>            
        </td>
    </tr>
    
    <tr>
        <td><asp:Label ID="lbFANO" Text="Fixed Asset No" runat="server"></asp:Label></td>
        <td>:</td>
        <td>
            <asp:TextBox ID="mpFANO" runat="server"></asp:TextBox>                                    
            <asp:ImageButton ID="btFANO" ToolTip="Fixed Asset No" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
            <asp:Label ID="mpFADESC"  runat="server"></asp:Label>
        </td>
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
    CssClass="Grid"
    AutoGenerateColumns = "true"
    ShowHeader="True"    
    AllowSorting="True"    
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

