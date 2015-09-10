<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="gl_rpt_profitandloss_1_tr.aspx.vb" Inherits="gl_rpt_profitandloss_1_tr" title="Untitled Page" %>
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

<form id="form1" runat="server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ToolkitScriptManager1" />

<asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">
<asp:TableRow>   
<asp:TableCell> 
<asp:Panel ID="pnTOOLBAR" runat="server">  
    <table border="0" cellpadding="3" cellspacing="3">
        <tr>            
            <td><asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');" /></td>
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
<asp:TableRow><asp:TableCell><asp:Label id="mlSQLSTATEMENT" runat="server" Visible="False" /></asp:TableCell></asp:TableRow>

<asp:TableRow>
<asp:TableCell BorderWidth="0">
<asp:Panel ID="pnFILL" runat="server">   
<table width="80%" cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td><asp:Label ID="lbDOCDATE1" Text="Posting Date (From)" runat="server"></asp:Label></td>
        <td>:</td>
        <td>
            <asp:TextBox ID="mlDOCDATE1" runat="server" Width="100"></asp:TextBox>                                                                                                          
            <input id="btDOCDATE1" runat="server" onclick="displayCalendar(ctl00_mpCONTENT_mlDOCDATE1,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow" />                
            <ajaxtoolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="mlDOCDATE1" TargetControlID="mlDOCDATE1" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender> 
            <font color="blue">dd/mm/yyyy</font>
        </td>
    </tr>
    
    <tr>    
        <td><asp:Label ID="lbDOCDATE2" Text="Posting Date (To)" runat="server"></asp:Label></td>
        <td>:</td>
        <td>                
            <asp:TextBox ID="mlDOCDATE2" runat="server" Width="100"></asp:TextBox>                                                                                                          
            <input id="btDOCDATE2" runat="server" onclick="displayCalendar(ctl00_mpCONTENT_mlDOCDATE2,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow" />                
            <ajaxtoolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="mlDOCDATE2" TargetControlID="mlDOCDATE2" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender> 
            <font color="blue">dd/mm/yyyy</font>
        </td>
    </tr>        
    
    <tr>
        <td><p>Branch</p></td>            
        <td>:</td>
        <td><asp:DropDownList ID="mpBRANCH" runat="server"></asp:DropDownList></td>
    </tr>
    
    
    <tr>
        <td colspan="3">    
            <br /><hr /><br />
            Filter Export Data untuk GL Profit & Loss Pekan Baru bulan November adl :
            <br />Posting Date (From) = 01/11/2012
            <br />Posting Date (To) = 30/11/2012
            <br />Branch = "10PB"    
            <br /><br />Setelah Pilih Kriteria tekan tombol Save                        
        </td>
    </tr>

   
        
</table>  

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
    HeaderStyle-ForeColor="White"
    HeaderStyle-Font-Bold="True"
    AlternatingItemStyle-BackColor="#EFEFEF"
    OnItemCommand="mlDATAGRID_ItemCommand"
    
    AutoGenerateColumns = "true"
    ShowHeader="True"    
    AllowSorting="True"
    OnSortCommand="mlDATAGRID_SortCommand"    
    OnItemDataBound ="mlDATAGRID_ItemBound"
    
    AllowPaging="True"    
    PagerStyle-Mode="NumericPages"
    PagerStyle-HorizontalAlign="center"
    OnPageIndexChanged="mlDATAGRID_PageIndex"    
    >	    
    
    <AlternatingItemStyle BackColor="#F9FCA8" />
    <Columns>  
         
    </Columns>
 </asp:DataGrid>  
 
</asp:Panel>
</asp:TableCell>
</asp:TableRow>

</asp:Table>

<br /><br /><br /><br />
</form>

</asp:Content>

