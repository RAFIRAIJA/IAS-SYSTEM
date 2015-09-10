<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="ap_mr_template.aspx.vb" Inherits="backoffice_ap_mr_template" title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
<link href="../script/calendar.css" rel="stylesheet" type="text/css" media="all" />
<script type="text/javascript" src="../script/calendar.js"></script>


<script type="text/javascript" Language="Javascript">
<!-- hide script from older browsers
function openwindow(url,nama,width,height)
{
OpenWin = this.open(url, nama);
if (OpenWin != null)
{
  if (OpenWin.opener == null)
  OpenWin.opener=self;
}
OpenWin.focus();
} 
// End hiding script-->
</script>

<form id="mpFORM" runat="server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ToolkitScriptManager1" />
<asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">
<asp:TableRow>   
<asp:TableCell> 
<asp:Panel ID="pnTOOLBAR" runat="server">  
    <table border="0" cellpadding="3" cellspacing="3">
        <tr>
            <td>
                <asp:ImageButton id="btNewRecord" ToolTip="NewRecord" ImageUrl="~/images/toolbar/new.jpg" runat="server" />&nbsp;
                <asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" Visible="false" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');" />&nbsp;
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
<asp:TableRow><asp:TableCell><asp:HiddenField ID="mlSYS_DOCNO" runat="server"/></asp:TableCell></asp:TableRow>

<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnFILL" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td><asp:Label ID="lbENTITY" runat="server" Text="Entity"></asp:Label></td>            
            <td><asp:DropDownList ID="ddENTITY" runat="server"></asp:DropDownList></td>     
        </tr>
            
        <tr>
            <td><p><asp:Label ID="lbDOCID" Text="DocID" runat="server"></asp:Label></p></td>
            <td valign="top">
                <asp:DropDownList ID="mpDOCID" runat="server"></asp:DropDownList>
                <asp:Label ID="mpDOCDESC" Text="" runat="server"></asp:Label>
            </td>  
        </tr>
        
        <tr>
            <td><p><asp:Label ID="lbDOCDATE" Text="Doc Date" runat="server"></asp:Label></p></td>
            <td>
                <asp:TextBox ID="mpDOCDATE" runat="server" Width="100"></asp:TextBox>                                                                    
                <input id="btDOCDATE" runat="server" onclick="displayCalendar(mpCONTENT_mpDOCDATE,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />                                                      
                <asp:ImageButton runat="Server" ID="btCALENDAR1" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" /><br />
                <ajaxtoolkit:CalendarExtender ID="CalendarExtender2" popupbuttonID="mpDOCDATE" TargetControlID="mpDOCDATE" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender> 
            </td>
        </tr>
        
        <tr>
            <td valign="top">                        
                <asp:Label ID="lbITEMKEY" Text="Item Code" runat="server"></asp:Label>
                <asp:ImageButton ID="btSEARCHITEMKEY" ToolTip="Item Code" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                                
            </td>            
            <td valign="top">
                <asp:TextBox ID="mpITEMKEY" runat="server" width="100"> </asp:TextBox>                                
                <asp:ImageButton ID="btITEMKEY" ToolTip="Recruiter ID" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
                <asp:Label ID="mpITEMDESC"  Enabled="false" runat="server"></asp:Label>                                                                
            </td>
        </tr>
        
        <tr>
            <td valign="top"></td>            
            <td valign="top">
                <asp:Panel ID="pnSEARCHITEMKEY" runat="server">
                <asp:Label ID="lbSEARCHITEMKEY" Text="Item Description : " runat="server"></asp:Label>
                <asp:TextBox ID="mpSEARCHITEMKEY" runat="server" BackColor="AntiqueWhite" ></asp:TextBox>
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
            <td valign="top"><p><asp:Label ID="lbUOM" runat="server" text="UOM"></asp:Label></p></td>
            <td><asp:DropDownList ID="mpUOM" runat="server"></asp:DropDownList></td>
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
    HeaderStyle-BackColor="orange"  
    HeaderStyle-VerticalAlign ="top"
    HeaderStyle-HorizontalAlign="Center"
    OnItemCommand="mlDATAGRID_ItemCommand"    
    autogeneratecolumns="true">	    
    
    <AlternatingItemStyle BackColor="#F9FCA8" />
    <Columns>  
    
        <asp:TemplateColumn>
            <ItemTemplate>
            <asp:imagebutton id="btBrowseRecord" Runat="server" AlternateText="BrowseRecord" ImageUrl="~/images/toolbar/browse.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.DocNo")%>' CommandName="BrowseRecord">
            </asp:imagebutton>
            </ItemTemplate>
        </asp:TemplateColumn>   
        
        <asp:TemplateColumn>
            <ItemTemplate>
            <asp:imagebutton id="btDeleteRecord" Runat="server" AlternateText="Delete" ImageUrl="~/images/toolbar/delete.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.DocNo")%>'   CommandName="DeleteRecord" OnClientClick="return confirm('Delete Record ?');">
            </asp:imagebutton>
            </ItemTemplate>
        </asp:TemplateColumn>  
                
    </Columns>
 </asp:DataGrid>  
</asp:Panel>

</asp:TableCell>
</asp:TableRow>


<asp:TableRow>
<asp:TableCell>
<br /> <hr /> <br />
<asp:Panel ID="pnGRID2" runat="server">    
    
    <asp:DataGrid runat="server" ID="mlDATAGRID2" 
    CssClass="Grid"
    OnItemCommand="mlDATAGRID2_ItemCommand"    
    autogeneratecolumns="true">	    
    
    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
    <ItemStyle CssClass="GridItem"></ItemStyle>
    <EditItemStyle  CssClass="GridItem" />
    <PagerStyle  CssClass="GridItem" />
    <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
    <Columns>          
        
         <asp:TemplateColumn>
            <ItemTemplate>
            <asp:imagebutton id="btDeleteRecord2" Runat="server" AlternateText="Delete" ImageUrl="~/images/toolbar/delete.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ItemKey")%>'   CommandName="DeleteRecord" OnClientClick="return confirm('Delete Record ?');">
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

