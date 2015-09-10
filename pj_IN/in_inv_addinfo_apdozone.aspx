<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="in_inv_addinfo_apdozone.aspx.vb" Inherits="in_inv_image" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
<form id="form1" runat="server">

<asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">

<asp:TableRow>   
<asp:TableCell> 
<asp:Panel ID="pnTOOLBAR" runat="server">  
<table border="0" cellpadding="3" cellspacing="3">
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
<asp:Panel ID="pnFILL" runat="server">
<table width="80%" cellpadding="0" cellspacing="0" border="0">
<tr>
    <td valign="top">
        <asp:Label ID="lbSITECARD" Text="Site Card" runat="server"></asp:Label>
        <asp:ImageButton ID="btSEARCHSITECARD" ToolTip="Kode Site Card" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                                
    </td>
    <td valign="top">
        <asp:TextBox ID="mpSITECARD" runat="server" Width="100"></asp:TextBox>                                                                    
        <asp:ImageButton ID="btSITECARD" ToolTip="Cari Site Card" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />  
        <asp:Label ID="mpSITEDESC" Text="" runat="server"></asp:Label>
    </td>
</tr>   
   
<tr>                    
    <td></td>            
    <td>
        <asp:Panel ID="pnSEARCHSITECARD" runat="server">                            
            <asp:Label ID="lbSEARCHSITECARD" Text="Nama Site : " runat="server"></asp:Label>
            <asp:TextBox ID="mlSEARCHSITECARD" runat="server" BackColor="AntiqueWhite" Width="300"></asp:TextBox>
            <asp:ImageButton ID="btSEARCHSITECARDSUBMIT" ToolTip="Search Agent ID" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                            
            
            <asp:DataGrid runat="server" ID="mlDATAGRIDSITECARD" 
                HeaderStyle-BackColor="orange"  
                HeaderStyle-VerticalAlign ="top"
                HeaderStyle-HorizontalAlign="Center"
                OnItemCommand="mlDATAGRIDSITECARD_ItemCommand"        
                autogeneratecolumns="false">	    
                
                <AlternatingItemStyle BackColor="#F9FCA8" />
                <Columns>  
                    <asp:ButtonColumn  HeaderText = "Kode" DataTextField = "LineNo_" ></asp:ButtonColumn>
                    <asp:ButtonColumn HeaderText = "Nama"  DataTextField = "SearchName"></asp:ButtonColumn>
                </Columns>
             </asp:DataGrid> 
        </asp:Panel>            
    </td>
 </tr>
 
<tr id="tr1" runat="server">
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

<tr id="tr2" runat="server">                    
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

<tr id="tr3" runat="server">
    <td valign="top">                        
        <asp:Label ID="lbVENDOR" Text="Vendor Code" runat="server"></asp:Label>
        <asp:ImageButton ID="btSEARCHVENDOR" ToolTip="Vendor Code" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                                
    </td>
    
    <td valign="top">
        <asp:TextBox ID="mpVENDOR" runat="server"> </asp:TextBox>                                
        <asp:ImageButton ID="btVENDOR" ToolTip="Vendor Code" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
        <asp:Label ID="mpVENDORDESC" Width="250" Enabled="false" runat="server"></asp:Label> 
    </td>
</tr>

<tr id="tr4" runat="server">                    
    <td></td>            
    <td>
        <asp:Panel ID="pnSEARCHVENDOR" runat="server">
        <asp:Label ID="lbSEARCHVENDOR" Text="Vendor Description : " runat="server"></asp:Label>
        <asp:TextBox ID="mpSEARCHVENDOR"  width="300" runat="server" BackColor="AntiqueWhite" ></asp:TextBox>
        <asp:ImageButton ID="btSEARCHVENDORSUBMIT" ToolTip="Search Vendor Key" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />
        
        <asp:DataGrid runat="server" ID="mlDATAGRIDVENDOR" 
            HeaderStyle-BackColor="orange"  
            HeaderStyle-VerticalAlign ="top"
            HeaderStyle-HorizontalAlign="Center"
            OnItemCommand="mlDATAGRIDVENDOR_ItemCommand"        
            autogeneratecolumns="false">	    
            
            <AlternatingItemStyle BackColor="#F9FCA8" />            
            <Columns>  
                <asp:ButtonColumn  HeaderText = "Code" DataTextField = "No_" ></asp:ButtonColumn>
                <asp:ButtonColumn HeaderText = "Name"  DataTextField = "Search Name"></asp:ButtonColumn>
            </Columns>
         </asp:DataGrid> 
        </asp:Panel>                       
    </td>
 </tr>      
</table>    
<br />
<p><i>
    - Item Code : "All: for All Item <br />
    - Item to Vendor Searching : Check Specified Item with Vendor -> Check All Item With Vendor -> Check Default Navision Vendor
</i></p>
<hr />
</asp:Panel>
</asp:TableCell>
</asp:TableRow>

<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnGRID" runat="server">    
<asp:LinkButton  ID="lnVIEWNORMAL" Text="Normal View |" runat="server"></asp:LinkButton> 
<asp:LinkButton  ID="lnVIEWSITECARD" Text="View SiteCard Assignment |" runat="server" Visible="false"></asp:LinkButton> 
<asp:LinkButton  ID="lnVIEWITEM" Text="View Item Assignment |" runat="server" Visible="false"></asp:LinkButton> 
<asp:LinkButton  ID="lnVIEWVENDOR" Text="View Vendor Assignment |" runat="server" Visible="false"></asp:LinkButton> 

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
        <asp:imagebutton id="btBrowseRecord" Runat="server" AlternateText="BrowseRecord" ImageUrl="~/images/toolbar/browse.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SiteCard")& ";" & DataBinder.Eval(Container,"DataItem.ItemCode") %>' CommandName="BrowseRecord">
        </asp:imagebutton>
        </ItemTemplate>
    </asp:TemplateColumn>   
    
    
    <asp:TemplateColumn>
        <ItemTemplate>
        <asp:imagebutton id="btEditRecord" Runat="server" AlternateText="Edit" ImageUrl="~/images/toolbar/edit.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SiteCard")& ";" & DataBinder.Eval(Container,"DataItem.ItemCode")%>' CommandName="EditRecord">
        </asp:imagebutton>
        </ItemTemplate>
    </asp:TemplateColumn>   
    
     <asp:TemplateColumn>
        <ItemTemplate>
        <asp:imagebutton id="btDeleteRecord" Runat="server" AlternateText="Delete" ImageUrl="~/images/toolbar/delete.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SiteCard")& ";" & DataBinder.Eval(Container,"DataItem.ItemCode")%>' OnClientClick="return confirm('Delete Record ?');" CommandName="DeleteRecord">
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

