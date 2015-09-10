<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="ut_usertrack.aspx.vb" Inherits="backoffice_ut_usertrack" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">

<link href="../script/calendar.css" rel="stylesheet" type="text/css" media="all" />
<script type="text/javascript" src="../script/calendar.js"></script>



<form id="form1" runat="server">
<table width="100%" border="0" cellpadding="0" cellspacing="0">
<tr>
    <td>
        <asp:Panel ID="pnTOOLBAR" runat="server">
            <table width="0" border="0" cellpadding="3" cellspacing="3">
            <tr>
                <td><asp:ImageButton ID="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" /></td>                
            </tr>
            </table>
        </asp:Panel>
    </td>
</tr>

<tr>
    <td>
    <p class="header1"><b><asp:Label id="mlTITLE" runat="server"></asp:Label></b></p>
    <p><asp:Label ID="mlMESSAGE" runat="server"  Font-Italic=true ForeColor ="red"></asp:Label> <br /> </p>
    <asp:HiddenField ID="mlSYSCODE" runat="server"/>                
    <asp:Label id="mlSQLSTATEMENT" runat="server" Visible="False" />    
    </td>
</tr>
</table>

<asp:Table ID="Table4" Width="80%" CellPadding="0" CellSpacing="0" BorderWidth="0" runat="server">

<asp:TableRow>
<asp:TableCell><p>User ID</p></asp:TableCell>
<asp:TableCell ColumnSpan="5"><asp:TextBox ID="mpUSERID" runat="server" Width="160px"></asp:TextBox> </asp:TableCell>
</asp:TableRow>


<asp:TableRow>
<asp:TableCell><p>Date From</p></asp:TableCell>
<asp:TableCell>
<asp:TextBox runat="server" ID="mpDATE1" Width="100"></asp:TextBox>
<input id="Button1" runat="server" onclick="displayCalendar(ctl00_mpCONTENT_mpDATE1,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />
<font color="blue">dd/mm/yyyy</font>
</asp:TableCell>
</asp:TableRow>

<asp:TableRow>
<asp:TableCell><p>Date To</p></asp:TableCell>
<asp:TableCell>
<asp:TextBox runat="server" ID="mpDATE2" Width="100"></asp:TextBox>
<input id="btJOINDATE" runat="server" onclick="displayCalendar(ctl00_mpCONTENT_mpDATE2,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />
<font color="blue">dd/mm/yyyy</font>
</asp:TableCell>
</asp:TableRow>

</asp:Table>


<br />
<asp:Panel ID="pnLINK" runat="server">
    <hr />
    <p class="header1"><b><asp:Label id="mlTITLE2" runat="server"></asp:Label></b></p>
    <p><asp:HyperLink ID="mlLINKDOC" runat="server"></asp:HyperLink></p>    
</asp:Panel>
<br />

<asp:DataGrid runat="server" ID="mlDATAGRID"
CssClass="Grid"
AutoGenerateColumns = "true"
ShowHeader="True"    
AllowSorting="True"
OnSortCommand="mlDATAGRID_SortCommand"    
OnItemDataBound ="mlDATAGRID_ItemBound"
>	    

<HeaderStyle CssClass="GridHeader"></HeaderStyle>
<ItemStyle CssClass="GridItem"></ItemStyle>
<EditItemStyle  CssClass="GridItem" />
<PagerStyle  CssClass="GridItem" />
<AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
<Columns>           
   
                            
</Columns>
</asp:DataGrid>  


</form>

<br /><br /><br /><br />
</asp:Content>


