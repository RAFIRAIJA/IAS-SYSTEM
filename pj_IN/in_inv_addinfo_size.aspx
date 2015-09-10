<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="in_inv_addinfo_size.aspx.vb" Inherits="in_inv_addinfo_size" title="Untitled Page" %>
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
<asp:TableRow><asp:TableCell><asp:Label id="mlSQLSTATEMENT" runat="server" Visible="False" /></asp:TableCell></asp:TableRow>

<asp:TableRow>
<asp:TableCell BorderWidth="0">
<table width="20%" cellpadding="0" cellspacing="0" border="0">
<tr>
    <td><asp:Label ID="lbENTITY" runat="server" Text="Entity"></asp:Label></td>            
    <td>
        <asp:DropDownList ID="ddENTITY" runat="server"></asp:DropDownList>
        <asp:ImageButton ID="btENTITY" ToolTip="Entity" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
    </td>     
</tr>
</table>

<asp:Panel ID="pnFILL" runat="server">
<table width="80%" cellpadding="0" cellspacing="0" border="0">
<tr>    
    <td valign="top">                        
        <asp:Label ID="lbITEMKEY" Text="Item Code" runat="server"></asp:Label>
        <asp:ImageButton ID="btSEARCHITEMKEY" ToolTip="Item Code" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                                
    </td>
    
    <td valign="top">
        <asp:TextBox ID="mpITEMKEY" runat="server"> </asp:TextBox>                                
        <asp:ImageButton ID="btITEMKEY" ToolTip="Item Code" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
        <asp:Label ID="mpITEMDESC" Width="250" Enabled="false" runat="server"></asp:Label> 
    </td>
</tr>

<tr>                    
    <td></td>            
    <td>
        <asp:Panel ID="pnSEARCHITEMKEY" runat="server">
        <asp:Label ID="lbSEARCHITEMKEY" Text="Item Description : " runat="server"></asp:Label>
        <asp:TextBox ID="mpSEARCHITEMKEY"  width="300" runat="server" BackColor="AntiqueWhite" ></asp:TextBox>
        <asp:ImageButton ID="btSEARCHITEMKEYSUBMIT" ToolTip="Search Item Key" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />
        
        <asp:DataGrid runat="server" ID="mlDATAGRIDITEMKEY" 
            HeaderStyle-BackColor="orange"  
            HeaderStyle-VerticalAlign ="top"
            HeaderStyle-HorizontalAlign="Center"
            OnItemCommand="mlDATAGRIDITEMKEY_ItemCommand"        
            autogeneratecolumns="false">	    
            
            <AlternatingItemStyle BackColor="#F9FCA8" />
            <Columns>  
                <asp:ButtonColumn  HeaderText = "Code" DataTextField = "No_" ></asp:ButtonColumn>
                <asp:ButtonColumn HeaderText = "Name"  DataTextField = "Description"></asp:ButtonColumn>
            </Columns>
         </asp:DataGrid> 
        </asp:Panel>                       
    </td>
 </tr>      


<tr>
    <td valign="top"><asp:Label ID="lbSIZE" Text="Size" runat="server"></asp:Label></td>
    <td valign="top"><asp:TextBox ID="mpSIZE" runat="server" Width="150"></asp:TextBox></td>            
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
OnItemCommand="mlDATAGRID_ItemCommand"    
autogeneratecolumns="true">	    

<HeaderStyle CssClass="GridHeader"></HeaderStyle>
<ItemStyle CssClass="GridItem"></ItemStyle>
<EditItemStyle  CssClass="GridItem" />
<PagerStyle  CssClass="GridItem" />
<AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
<Columns>  

    <asp:TemplateColumn>
        <ItemTemplate>
        <asp:imagebutton id="btBrowseRecord" Runat="server" AlternateText="BrowseRecord" ImageUrl="~/images/toolbar/browse.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ItemCode") & ";" & DataBinder.Eval(Container,"DataItem.Size")%>' CommandName="BrowseRecord">
        </asp:imagebutton>
        </ItemTemplate>
    </asp:TemplateColumn>   
    
    
    <asp:TemplateColumn>
        <ItemTemplate>
        <asp:imagebutton id="btEditRecord" Runat="server" AlternateText="Edit" ImageUrl="~/images/toolbar/edit.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ItemCode")& ";" & DataBinder.Eval(Container,"DataItem.Size")%>' CommandName="EditRecord">
        </asp:imagebutton>
        </ItemTemplate>
    </asp:TemplateColumn>   
    
     <asp:TemplateColumn>
        <ItemTemplate>
        <asp:imagebutton id="btDeleteRecord" Runat="server" AlternateText="Delete" ImageUrl="~/images/toolbar/delete.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ItemCode")& ";" & DataBinder.Eval(Container,"DataItem.Size")%>' OnClientClick="return confirm('Save Record ?');" CommandName="DeleteRecord">
        </asp:imagebutton>
        </ItemTemplate>
    </asp:TemplateColumn>     
    
</Columns>
</asp:DataGrid>  
</asp:Panel>

</asp:TableCell>
</asp:TableRow>

</asp:Table>

</form>
<br /><br /><br /><br />

</asp:Content>

