<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="ap_rpt_mr_request_mr_cr.aspx.vb" Inherits="ap_rpt_mr_request_mr_cr"  %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
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
<asp:Panel ID="pnTOOlbAR" runat="server">  
    <table border="0" cellpadding="3" cellspacing="3">
        <tr>            
            <td>
                <asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" />&nbsp;
                <asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" />&nbsp;
                <asp:ImageButton id="btExCsv" ToolTip="csv" ImageUrl="~/images/toolbar/csvfile.png" runat="server"  Visible="false"/>
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
<asp:TableRow><asp:TableCell><asp:Label id="mlSQLSTATEMENT" runat="server" Visible="False" /></asp:TableCell></asp:TableRow><asp:TableRow>
<asp:TableCell BorderWidth="0">
<asp:Panel ID="pnFILL" runat="server">   

<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr><td>
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
        </tr>
        
        <tr>
            <td><p><asp:Label ID="lblDATEAPPRFROM" Text="Date Last Posting From" runat="server"></asp:Label></p></td>
            <td>:</td>
            <td>
                <asp:TextBox ID="mlDATEAPPRFROM" runat="server" Width="100"></asp:TextBox>                                                                                                          
                <input id="Button2" runat="server" onclick="displayCalendar(mpCONTENT_mlDATEAPPRFROM,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />
                <font color="blue">dd/mm/yyyy</font>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="mlDATEAPPRFROM" Format="dd/MM/yyyy" />            
            </td>            
            <td></td>
            <td><p><asp:Label ID="lbDATEAPPRTO" Text="Date Last Posting To" runat="server"></asp:Label></p></td>
            <td>:</td>
            <td>
                <asp:TextBox ID="mlDATEAPPRTO" runat="server" Width="100"></asp:TextBox>                                                                                                                      
                <input id="Button3" runat="server" onclick="displayCalendar(mpCONTENT_mlDATEAPPRTO,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow" />            
                <font color="blue">dd/mm/yyyy</font>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="mlDATEAPPRTO" Format="dd/MM/yyyy" />
            </td>            
        </tr>    
                
        <tr>
            <td><asp:Label ID="lbDOCNO1" Text="MR No (From)" runat="server"></asp:Label></td>
            <td>:</td>
            <td><asp:TextBox ID="txDOCUMENTNO1" runat="server" Width="150"></asp:TextBox></td>
            <td></td>
            <td><asp:Label ID="lbDOCNO2" Text="MR No (To)" runat="server"></asp:Label></td>
            <td>:</td>
            <td><asp:TextBox ID="txDOCUMENTNO2" runat="server" Width="150"></asp:TextBox></td>
        </tr>
        
               
        <tr>
            <td><asp:Label ID="lbLOCID1" Text="Site Card (From)" runat="server"></asp:Label></td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txLOCID1" runat="server"></asp:TextBox>                                    
                <asp:ImageButton ID="btLOCID1" ToolTip="Loc ID" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
                <asp:Label ID="lbLOCDESC1"  runat="server"></asp:Label>
            </td>
            <td></td>
            <td><asp:Label ID="lbLOCID2" Text="Site Card (To)" runat="server"></asp:Label></td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txLOCID2" runat="server"></asp:TextBox>                                    
                <asp:ImageButton ID="btLOCID2" ToolTip="Loc ID" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
                <asp:Label ID="lbLOCDESC2"  runat="server"></asp:Label>
            </td>
        </tr>        
        
        <tr>
            <td><p><asp:Label ID="lbUSERID" Text="Create User ID" runat="server"></asp:Label></p></td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txUSERID" runat="server"></asp:TextBox>                                
                <asp:ImageButton ID="btUSERID" ToolTip="Employee ID" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
                <asp:Label ID="lbNAME" runat="server"></asp:Label>                                                                                                
            </td>
            <td></td>
            <td valign="top"><asp:Label ID="lbPERIOD" Text="Periode MR" runat="server"></asp:Label></td>        
            <td>:</td>
            <td valign="top">
                <asp:TextBox ID="mpPERIOD" runat="server" Width="100"></asp:TextBox>                                                                    
                <input id="btPERIOD" runat="server" onclick="displayCalendar(mpCONTENT_mpPERIOD,'mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />                                                      
                <asp:ImageButton runat="Server" ID="ImageButton1" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" /><br />
                <ajaxtoolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="mpPERIOD" TargetControlID="mpPERIOD" Format="MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender> 
            </td>
        </tr>
        
        <tr>
            <td><p>Status</p></td>
            <td>:</td>
            <td><asp:DropDownList ID="ddSTATUS" runat="server"></asp:DropDownList></td>
            <td></td>
            <td><p>Report Type</p></td>
            <td>:</td>
            <td><asp:DropDownList ID="ddREPORT" runat="server"></asp:DropDownList></td>
        </tr>
        
        <tr>
            <td><asp:Label ID="lbENTITY" runat="server" Text="Entity"></asp:Label></td>
            <td>:</td>
            <td><asp:DropDownList ID="ddENTITY" runat="server" AutoPostBack="true"></asp:DropDownList></td>     
            <td></td>
            <td><asp:Label ID="lbTYPE" runat="server" Text="Type"></asp:Label></td>
            <td>:</td>
            <td><asp:DropDownList ID="ddTYPE" runat="server"></asp:DropDownList></td> 
        </tr>
        
   </table>
</td></tr>
</table>

</asp:Panel>
</asp:TableCell></asp:TableRow><asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnGRID" runat="server">
    <%--<CR:CrystalReportViewer ID="CRViewer" runat="server" AutoDataBind="true" /> --%>
    <CR:CrystalReportViewer ID="CRViewer" runat="server" AutoDataBind="true" />
</asp:Panel>
</asp:TableCell></asp:TableRow></asp:Table><br /><br /><br /><br />
</form>
</asp:Content>

