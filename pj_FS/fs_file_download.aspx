<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="fs_file_download.aspx.vb" Inherits="fs_file_download"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
<link href="../script/calendar.css" rel="stylesheet" type="text/css" media="all" />
<script type="text/javascript" src="../script/calendar.js"></script>

<script type="text/javascript" language="Javascript">
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
            <td><asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" /></td>            
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
            <td><asp:Label ID="lbDOCDATE1" Text="Date (From)" runat="server"></asp:Label></td>
            <td>:</td>
            <td>                             
                <asp:TextBox ID="txDOCDATE1" runat="server" Width="100"></asp:TextBox>                                                                    
                <input id="btDOCDATE1" runat="server" onclick="displayCalendar(mpCONTENT_txDOCDATE1,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />                                                                      
                <asp:ImageButton runat="Server" ID="btCALENDAR1" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" />
                <ajaxtoolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="bt_ajDOCDATE1" TargetControlID="txDOCDATE1" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender>
                <font color="blue">dd/mm/yyyy</font>
            </td>
            
            <td></td>
            <td><asp:Label ID="lbDOCDATE2" Text="Date (To)" runat="server"></asp:Label></td>
            <td>:</td>
            <td>                
                <asp:TextBox ID="txDOCDATE2" runat="server" Width="100"></asp:TextBox>                                                                                                          
                <input id="btJOINDATE2" runat="server" onclick="displayCalendar(mpCONTENT_txDOCDATE2,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />                                
                <asp:ImageButton runat="Server" ID="btCALENDAR2" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" />
                <ajaxtoolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="bt_ajDOCDATE2" TargetControlID="txDOCDATE2" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender>                 
                <font color="blue">dd/mm/yyyy</font>
            </td>
            
            <td></td>
            <td valign="top"><p><asp:Label ID="Label1" Text="Group File" runat="server"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top"><asp:DropDownList ID="ddTYPE" runat="server"></asp:DropDownList></td>  
        </tr>       
      
        
    </table>    
    <hr /><br />
</asp:Panel>
</asp:TableCell>
</asp:TableRow>


<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnFILL2" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">                 
        <tr>
            <td><p><asp:Label ID="lbDOCID" Text="DocID" runat="server"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top"><asp:Label ID="lbDOCUMENTNO" runat="server" Width="150" Enabled="false"></asp:Label>
            <asp:HyperLink ID="lnLINK1" runat="server">babbabab</asp:HyperLink>
            </td>            
        </tr>
        
        <tr>
            <td><asp:Label ID="lbDOCDATE" Text="Doc Date" runat="server"></asp:Label></td>
            <td>:</td>
            <td><asp:Label ID="lbDOCDATES" runat="server" Width="100"></asp:Label></td>
        </tr>
             
        <tr>
            <td valign="top"><p><asp:Label ID="lbTYPE" Text="Group File" runat="server"></asp:Label></p></td>
            <td valign="top">:</td>
            <td><asp:Label ID="lbTYPES" runat="server" Width="100"></asp:Label></td>
        </tr>
        
                        
        <tr>
            <td valign="top"><p><asp:Label ID="lbDESCRIPTION" Text="Description" runat="server"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top"><asp:Label ID="lbDESCRIPTIONS" runat="server" /></td>  
        </tr>        
                
        <tr><td colspan="3"><br /><hr /><br /></td></tr>
        
        <tr id="trUP0" runat="server">
            <td colspan="3">
                <asp:DataGrid runat="server" ID="mlDATAGRID2" 
                HeaderStyle-BackColor="orange"  
                HeaderStyle-VerticalAlign ="top"
                HeaderStyle-HorizontalAlign="Center"    
                OnItemCommand="mlDATAGRID2_ItemCommand"
                OnItemDataBound ="mlDATAGRID2_ItemBound"    
                autogeneratecolumns="true">	                   
                               
                <AlternatingItemStyle BackColor="#F9FCA8" />
                <Columns>               
                    <asp:templatecolumn headertext="VW">
                    <ItemTemplate>        
                        <asp:hyperlink  Target="_blank"  runat="server" id="lnLINK1" navigateurl='<%# String.Format("fs_file_download_auth.ashx?mpID={0}&mpNO={1}", Eval("DocNo"),Eval("No")) %>' text="DW"></asp:hyperlink>
                    </ItemTemplate>
                    </asp:templatecolumn>                         
                    
                </Columns>
             </asp:DataGrid>  
            </td>
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
    OnItemCommand="mlDATAGRID_ItemCommand"
    autogeneratecolumns="true"
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
            <asp:imagebutton id="btBrowseRecord" Runat="server" AlternateText="BrowseRecord" ImageUrl="~/images/toolbar/browse.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.DocNo")%>' CommandName="BrowseRecord">
            </asp:imagebutton>
            </ItemTemplate>
        </asp:TemplateColumn>
        
        <asp:templatecolumn headertext="LG">
        <ItemTemplate>        
            <asp:hyperlink  Target="_blank"  runat="server" id="lnLINK2" navigateurl='<%# String.Format("fs_doc_file_downloadlog.aspx?mpID={0}", Eval("DocNo")) %>' text="LG"></asp:hyperlink>
        </ItemTemplate>
        </asp:templatecolumn>
                        
    </Columns>
 </asp:DataGrid>  
</asp:Panel>

</asp:TableCell>
</asp:TableRow>

</asp:Table>
</form>
<br /><br /><br /><br />

</asp:Content>

