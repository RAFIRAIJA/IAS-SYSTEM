<%@ Page Language="VB" MasterPageFile="~/pagesetting/MsPageBlank.master" AutoEventWireup="false" CodeFile="in_inv_image.aspx.vb" Inherits="in_inv_image" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
<form id="form1" runat="server">

<asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">

<asp:TableRow>   
<asp:TableCell> 
<asp:Panel ID="pnTOOLBAR" runat="server">  
<table border="0" cellpadding="2" cellspacing="1">
    <tr>
        <td>
            <asp:ImageButton id="btNewRecord" ToolTip="NewRecord" ImageUrl="~/images/toolbar/new.jpg" runat="server" />&nbsp;
            <asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');" />&nbsp;
            <asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" />&nbsp;
            <asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" />
        </td>            
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
        <td colspan="2" align="left" valign="top">
            <br /><br /><p><b>Image</b></p>
            <table cellpadding="0" cellspacing="0" border="1" width="100%" bordercolor="silver">                
                <tr>
                    <td valign="top" align="center"><asp:Image ID="mlIMAGE11" runat="server" Width="150" Height="150" /></td>                        
                </tr>
                
                <tr>
                    <td>
                        <asp:ImageButton ID="mlIMAGE_BT11" ImageUrl="~/images/toolbar/upload.gif" runat="server" />
                        <asp:ImageButton ID="mlIMAGE_BT12" ImageUrl="~/images/toolbar/delete.jpg" runat="server" />
                        <asp:FileUpload ID="mlIMAGEP11" runat="server" />                            
                        <asp:TextBox id="mlIMAGENAMES11" runat="server"/>
                    </td>
                </tr>
            </table>
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
        <asp:imagebutton id="btBrowseRecord" Runat="server" AlternateText="BrowseRecord" ImageUrl="~/images/toolbar/browse.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ItemKey")%>' CommandName="BrowseRecord">
        </asp:imagebutton>
        </ItemTemplate>
    </asp:TemplateColumn>   
    
    
    <asp:TemplateColumn>
        <ItemTemplate>
        <asp:imagebutton id="btEditRecord" Runat="server" AlternateText="Edit" ImageUrl="~/images/toolbar/edit.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ItemKey")%>' CommandName="EditRecord">
        </asp:imagebutton>
        </ItemTemplate>
    </asp:TemplateColumn>   
    
     <asp:TemplateColumn>
        <ItemTemplate>
        <asp:imagebutton id="btDeleteRecord" Runat="server" AlternateText="Delete" ImageUrl="~/images/toolbar/delete.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ItemKey")%>' OnClientClick="return confirm('Save Record ?');" CommandName="DeleteRecord">
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

