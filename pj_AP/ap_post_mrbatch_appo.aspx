<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="ap_post_mrbatch_appo.aspx.vb" Inherits="ap_post_mrbatch_appo"  %>
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



<form id="form1" runat="server" method="post">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"  EnableScriptLocalization="true" ID="ScriptManager1" />

<asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">

<asp:TableRow>   
<asp:TableCell> 
<asp:Panel ID="pnTOOLBAR" runat="server">  
<table border="0" cellpadding="3" cellspacing="3">
    <tr>        
        <td>
            <asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Submit Record ?');" />&nbsp;
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
<asp:TableRow><asp:TableCell><p><asp:HyperLink ID="mlLINKDOC" runat="server"></asp:HyperLink></p></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><p><asp:HyperLink ID="mlLINKDOC2" runat="server"></asp:HyperLink></p></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><p><asp:HyperLink ID="mlLINKDOC3" runat="server"></asp:HyperLink></p></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><p><asp:HyperLink ID="mlLINKDOC4" runat="server"></asp:HyperLink></p></asp:TableCell></asp:TableRow>

<asp:TableRow>
<asp:TableCell BorderWidth="0">
<asp:Panel ID="pnFILL" runat="server">
<table width="80%" cellpadding="0" cellspacing="0" border="0">            
    <tr>
        <td><p><asp:Label ID="lblDATEFROM" Text="Date From" runat="server"></asp:Label></p></td>
        <td>
            <asp:TextBox ID="mlDATEFROM" runat="server" Width="100"></asp:TextBox>                                                                                                          
            <input id="Button1" runat="server" onclick="displayCalendar(mpCONTENT_mlDATEFROM,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />
            <asp:ImageButton runat="Server" ID="btCALENDAR1" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" />
            <font color="blue">dd/mm/yyyy</font>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="bt_ajDOCDATE1"  TargetControlID="mlDATEFROM" Format="dd/MM/yyyy" />            
        </td>            
        <td></td>
        <td><p><asp:Label ID="lbDATETO" Text="Date To" runat="server"></asp:Label></p></td>
        <td>
            <asp:TextBox ID="mlDATETO" runat="server" Width="100"></asp:TextBox>                                                                                                                      
            <input id="btJOINDATE" runat="server" onclick="displayCalendar(mpCONTENT_mlDATETO,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow" />            
            <asp:ImageButton runat="Server" ID="btCALENDAR12" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" />
            <font color="blue">dd/mm/yyyy</font>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="bt_ajDOCDATE2" TargetControlID="mlDATETO" Format="dd/MM/yyyy" />
        </td>            
    </tr>
    
    <tr>
        <td><p><asp:Label ID="lblDATEAPPRFROM" Text="Date Last Posting From" runat="server"></asp:Label></p></td>
        <td>
            <asp:TextBox ID="mlDATEAPPRFROM" runat="server" Width="100"></asp:TextBox>                                                                                                          
            <input id="Button2" runat="server" onclick="displayCalendar(mpCONTENT_mlDATEAPPRFROM,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />
            <font color="blue">dd/mm/yyyy</font>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="mlDATEAPPRFROM" Format="dd/MM/yyyy" />            
        </td>            
        <td></td>
        <td><p><asp:Label ID="lbDATEAPPRTO" Text="Date Last Posting To" runat="server"></asp:Label></p></td>
        <td>
            <asp:TextBox ID="mlDATEAPPRTO" runat="server" Width="100"></asp:TextBox>                                                                                                                      
            <input id="Button3" runat="server" onclick="displayCalendar(mpCONTENT_mlDATEAPPRTO,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow" />            
            <font color="blue">dd/mm/yyyy</font>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server"  TargetControlID="mlDATEAPPRTO" Format="dd/MM/yyyy" />
        </td>            
    </tr>    
    
    <tr>
        <td><asp:Label ID="lbDOCNO1" Text="Document No (From)" runat="server"></asp:Label></td>        
        <td><asp:TextBox ID="mlDOCUMENTNO1" runat="server" Width="150"></asp:TextBox></td>
        <td></td>
        <td><asp:Label ID="lbDOCNO2" Text="Document No (To)" runat="server"></asp:Label></td>        
        <td><asp:TextBox ID="mlDOCUMENTNO2" runat="server" Width="150"></asp:TextBox></td>
    </tr>
        
    <tr>
        <td><p><asp:Label ID="lbUSERID" Text="Create User ID" runat="server"></asp:Label></p></td>
        <td>
            <asp:TextBox ID="mlUSERID" runat="server" Width="120"></asp:TextBox>                                
            <asp:ImageButton ID="btUSERID" ToolTip="Upline ID" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" Visible="false"/>
            <asp:Label ID="mlNAME" runat="server"></asp:Label>                                                                                                
        </td>
        <td></td>
        <td valign="top"><asp:Label ID="lbPERIOD" Text="Periode MR" runat="server"></asp:Label></td>        
        <td valign="top">
            <asp:TextBox ID="mpPERIOD" runat="server" Width="100"></asp:TextBox>                                                                    
            <input id="btPERIOD" runat="server" onclick="displayCalendar(mpCONTENT_mpPERIOD,'mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />                                                      
            <asp:ImageButton runat="Server" ID="btCALENDAR2" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" /><br />
            <ajaxtoolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="mpPERIOD" TargetControlID="mpPERIOD" Format="MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender> 
        </td>
    </tr>
    
    <tr>
        <td><p>Status</p></td>
        <td><asp:DropDownList ID="mlSTATUS" runat="server"></asp:DropDownList></td>
        <td></td>
        <td><p>Break By Vendor</p></td>
        <td><asp:DropDownList ID="ddBREAK" runat="server"></asp:DropDownList></td>    
    </tr>
    
    <tr>
        <td><asp:Label ID="lbENTITY" runat="server" Text="Entity"></asp:Label></td>
        <td><asp:DropDownList ID="ddENTITY" runat="server" AutoPostBack="true"></asp:DropDownList></td>     
        <td></td>
        <td><asp:Label ID="lbTYPE" runat="server" Text="Type"></asp:Label></td>
        <td><asp:DropDownList ID="ddTYPE" runat="server"></asp:DropDownList></td>     
    </tr>
    
    
</table>    
<hr />
</asp:Panel>
</asp:TableCell>
</asp:TableRow>

<asp:TableRow>
<asp:TableCell>

<asp:Panel ID="pnGRID" runat="server">    

<table width="70%" cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
Check All <asp:CheckBox ID="mlCHECKALL" runat="server" AutoPostBack="true" />
</td>

<td>
Checked By Row <asp:DropDownList ID="ddCHECK" runat="server" AutoPostBack="true"></asp:DropDownList>
</td>


<td>
<p>Update Status to :<asp:DropDownList ID="mlUPDATESTATUS" runat="server"></asp:DropDownList></p>
</td>
</tr>
</table>

            
<asp:DataGrid runat="server" ID="mlDATAGRID" 
CssClass="Grid"
OnItemCommand="mlDATAGRID_ItemCommand"    
autogeneratecolumns="false">	    

<HeaderStyle CssClass="GridHeader"></HeaderStyle>
<ItemStyle CssClass="GridItem"></ItemStyle>
<EditItemStyle  CssClass="GridItem" />
<PagerStyle  CssClass="GridItem" />
<AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
<Columns>
   
    <asp:TemplateColumn>
        <ItemTemplate>
        <asp:CheckBox id="mlCHECKBOX" runat="server"/>
        </ItemTemplate>
    </asp:TemplateColumn>   
               
    <asp:templatecolumn headertext="No">
        <ItemTemplate>        
            <%#Container.ItemIndex + 1%>
        </ItemTemplate>        
    </asp:templatecolumn>
                       
    <asp:templatecolumn headertext="Doc No">
        <ItemTemplate>        
            <asp:hyperlink  Target="_blank"  runat="server" id="Hyperlink2" navigateurl='<%# String.Format("ap_doc_mr.aspx?mpID={0}", Eval("DocNo")) %>' text='<%# Eval("DocNo") %>'></asp:hyperlink>
        </ItemTemplate>        
    </asp:templatecolumn>
    
    <asp:BoundColumn HeaderText="Doc No" DataField="DocNo" Visible="false"></asp:BoundColumn>
    <asp:BoundColumn HeaderText="Doc Date" DataField="DocDate" DataFormatString ="{0:d}"></asp:BoundColumn>
    <asp:BoundColumn HeaderText="SiteCard ID" DataField="SiteCardID"></asp:BoundColumn>
    <asp:BoundColumn HeaderText="SiteCard Name" DataField="SiteCardName"></asp:BoundColumn>
    <asp:BoundColumn HeaderText="Delivery Addr" DataField="Delivery_Address" Visible ="false" ></asp:BoundColumn>
    <asp:BoundColumn HeaderText="MR%" DataField="PercentageMR" DataFormatString ="{0:n}" ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
    <asp:BoundColumn HeaderText="Periode" DataField="BVMonth"></asp:BoundColumn>
    <asp:BoundColumn HeaderText="Total Amount" DataField="TotalAmount" DataFormatString ="{0:n}" ItemStyle-HorizontalAlign="right"></asp:BoundColumn>    
    <asp:BoundColumn HeaderText="Status" DataField="RecordStatus"></asp:BoundColumn>
    <asp:BoundColumn HeaderText="UserID1" DataField="PostingUserID1"></asp:BoundColumn>
    <asp:BoundColumn HeaderText="Date1" DataField="PostingDate1" DataFormatString ="{0:d}" ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
    <asp:BoundColumn HeaderText="UserID2" DataField="PostingUserID2"></asp:BoundColumn>
    <asp:BoundColumn HeaderText="Date2" DataField="PostingDate2" DataFormatString ="{0:d}" ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
    <asp:BoundColumn HeaderText="UserID3" DataField="PostingUserID3"></asp:BoundColumn>
    <asp:BoundColumn HeaderText="Date3" DataField="PostingDate3" DataFormatString ="{0:d}" ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
    <asp:BoundColumn HeaderText="Keterangan" DataField="Keterangan"  ></asp:BoundColumn>
    <%--<asp:BoundColumn HeaderText="JobNo" DataField="JobNo"  ></asp:BoundColumn>
    <asp:BoundColumn HeaderText="JobTaskNo" DataField="JobTaskNo"  ></asp:BoundColumn>--%>
    
</Columns>
</asp:DataGrid>  
</asp:Panel>

</asp:TableCell>
</asp:TableRow>

<asp:TableRow>
<asp:TableCell>
    <br /><br />
    <p><i>
    Keterangan MR Status :  <br />
    Wait1 = Permintaan Baru, Menunggu Proses Review <br />
    Wait2 = Selesai Review, Menunggu Proses Authorize <br />
    New = Selesai Authorize, Menunggu Proses Procurement <br />
    </i></p>

</asp:TableCell>
</asp:TableRow>

</asp:Table>

</form>
<br /><br /><br /><br />
</asp:Content>

