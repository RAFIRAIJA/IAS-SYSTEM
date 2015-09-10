<%@ Page Language="VB" MasterPageFile="~/pagesetting/MsPageBlank.master" AutoEventWireup="false" CodeFile="ad_universallookup.aspx.vb" Inherits="ad_universallookup"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
<form id="form1" runat="server">

<asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">

<asp:TableRow>   
<asp:TableCell> 
<asp:Panel ID="pnTOOLBAR" runat="server">  
    <table border="0" cellpadding="3" cellspacing="3">
        <tr>
            <td><asp:ImageButton id="btNewRecord" ToolTip="NewRecord" ImageUrl="~/images/toolbar/new.jpg" runat="server" /></td>
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

<asp:TableRow>
<asp:TableCell BorderWidth="0">

<table width="100%" cellpadding="0" cellspacing="0" border="0">               
    <tr><td>        
        <table width="40%" cellpadding="0" cellspacing="0" border="0">               
        <tr>
        <td><p>Group ID</p></td>            
        <td><asp:DropDownList ID="mlUNIVID" runat="server"></asp:DropDownList></td>
        <td><asp:Button ID="mlSUBMIT" text="Submit" runat="server" /></td>
        <td><asp:Button ID="mlBTNEW" text="New" runat="server" /></td>
        </tr>
        </table>        
    </td></tr>
    
    <tr><td><hr /></td></tr>  
 </table>        
        
<asp:Panel ID="pnFILL" runat="server">
    <table width="80%" cellpadding="0" cellspacing="0" border="0">
       <tr>
            <td><p>Kode</p></td>
            <td><asp:TextBox ID="mlLINCODE" runat="server" /></td>            
        </tr>
        
        <tr>
            <td><p>Keterangan</p></td>
            <td><asp:TextBox ID="mlDESC" width="500" runat="server" /></td>
        </tr>
        
        <tr>
            <td><p>Keterangan1</p></td>
            <td><asp:TextBox ID="mlDESC1" width="300" runat="server" /></td>
        </tr>
        
        <tr>
            <td><p>Keterangan2</p></td>
            <td><asp:TextBox ID="mlDESC2" width="300" runat="server" /></td>
        </tr>
        
        <tr>
            <td><p>Keterangan3</p></td>
            <td><asp:TextBox ID="mlDESC3" runat="server" /></td>
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
    
        <asp:TemplateColumn>
            <ItemTemplate>
            <asp:imagebutton id="btBrowseRecord" Runat="server" AlternateText="BrowseRecord" ImageUrl="~/images/toolbar/browse.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.LinCode")%>' CommandName="BrowseRecord">
            </asp:imagebutton>
            </ItemTemplate>
        </asp:TemplateColumn>   
        
        
        <asp:TemplateColumn>
            <ItemTemplate>
            <asp:imagebutton id="btEditRecord" Runat="server" AlternateText="Edit" ImageUrl="~/images/toolbar/edit.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.LinCode")%>' CommandName="EditRecord">
            </asp:imagebutton>
            </ItemTemplate>
        </asp:TemplateColumn>   
        
         <asp:TemplateColumn>
            <ItemTemplate>
            <asp:imagebutton id="btDeleteRecord" Runat="server" AlternateText="Delete" ImageUrl="~/images/toolbar/delete.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.LinCode")%>' CommandName="DeleteRecord" OnClientClick="return confirm('Delete Record ?');" >
            </asp:imagebutton>
            </ItemTemplate>
        </asp:TemplateColumn>             

        
        <asp:BoundColumn HeaderText="Kode" DataField="LinCode"></asp:BoundColumn>
        <asp:BoundColumn HeaderText="Keterangan" DataField="Description"></asp:BoundColumn>
        <asp:BoundColumn HeaderText="Keterangan1" DataField="AdditionalDescription1"></asp:BoundColumn>
        <asp:BoundColumn HeaderText="Keterangan2" DataField="AdditionalDescription2"></asp:BoundColumn>
        <asp:BoundColumn HeaderText="Keterangan3" DataField="AdditionalDescription3"></asp:BoundColumn>        
        
        
    </Columns>
 </asp:DataGrid>  
</asp:Panel>

</asp:TableCell>
</asp:TableRow>

</asp:Table>

</form>

<br /><br /><br /><br />
</asp:Content>

