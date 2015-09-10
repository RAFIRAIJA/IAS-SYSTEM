<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterPrint.master" AutoEventWireup="false" CodeFile="hr_script_hr_nik_pass.aspx.vb" Inherits="pj_hr_hr_script_mr_frontliner" title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
<form id="mpFORM" runat="server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ToolkitScriptManager1" />

<asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">
<asp:TableRow>   
<asp:TableCell> 
<asp:Panel ID="pnTOOLBAR" runat="server">  
    <table border="0" cellpadding="3" cellspacing="3">
        <tr>
            <td><asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');" /></td>
            <td><asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" /></td>
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
<asp:TableRow><asp:TableCell><p><asp:HyperLink ID="mlLINKDOC" runat="server"></asp:HyperLink></p></asp:TableCell></asp:TableRow>


<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnFILL" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">                 
        <tr>
            <td><p><asp:Label ID="lbNIK" Text="Employee ID" runat="server"></asp:Label></p></td>
            <td>:</td>
            <td><asp:TextBox ID="txNIK" runat="server" Width="100"> </asp:TextBox></td>
        </tr>
                 
        <tr>
            <td><p><asp:Label ID="lbGROUPMENU" Text="Group Menu" runat="server"></asp:Label></p></td>            
            <td>:</td>
            <td><asp:DropDownList ID="ddGROUPMENU" runat="server"></asp:DropDownList></td>
        </tr>
        
        <tr>
            <td><p><asp:Label ID="lbTYPE" Text="Script Type" runat="server"></asp:Label></p></td>            
            <td>:</td>
            <td valign="top"><asp:DropDownList ID="ddMENU" runat="server" AutoPostBack="true"></asp:DropDownList></td>           
        </tr>
        
        <tr id="TR1" runat="server">
            <td><p><asp:Label ID="lbURL" Text="URL Address" runat="server"></asp:Label></p></td>
            <td>:</td>
            <td><asp:TextBox ID="txURL" runat="server" Width="600"> </asp:TextBox></td>
        </tr>
        
        
        
       
                      
        <tr>
            <td><p><asp:Label ID="lbSUBMIT" Text="Click to Submit" runat="server"></asp:Label></p></td>
            <td>:</td>
            <td valign="top"><asp:ImageButton ID="btSUBMIT" ToolTip="" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" /></td>
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
    HeaderStyle-BackColor="orange"  
    HeaderStyle-VerticalAlign ="top"
    HeaderStyle-HorizontalAlign="Center"
    autogeneratecolumns="true">	    
    
    <AlternatingItemStyle BackColor="#F9FCA8" />
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

