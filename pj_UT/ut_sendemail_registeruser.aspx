<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterGeneral.master" AutoEventWireup="false" CodeFile="ut_sendemail_registeruser.aspx.vb" Inherits="pj_ut_ut_sendemail_registeruser" title="Untitled Page" %>
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
            <td><asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" ImageUrl="~/images/toolbar/email.gif" runat="server" OnClientClick="return confirm('Email to the User ?');" /></td>            
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
        <tr id="trU1" runat="server">
                <td valign="top">                        
                    <asp:Label ID="lbUSER" Text="User ID" runat="server"></asp:Label>
                    <asp:ImageButton ID="btSEARCHUSERID" ToolTip="User ID" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                                
                </td>
                <td valign="top">:</td>
                 <td valign="top">
                    <asp:TextBox ID="txUSERID" runat="server"> </asp:TextBox>                                
                    <asp:ImageButton ID="btUSERID" ToolTip="User ID" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
                    <asp:Label ID="txUSERDESC" Width="250" Enabled="false" runat="server"></asp:Label> 
                    
                </td>
             </tr>
             
             <tr id="trU2" runat="server">
                <td></td>            
                <td></td>         
                <td>
                    <asp:Panel ID="pnSEARCHUSERID" runat="server">
                    <asp:Label ID="lbSEARCHUSERID" Text="Employee Name : " runat="server"></asp:Label>
                    <asp:TextBox ID="mpSEARCHUSERID"  width="200" runat="server" BackColor="AntiqueWhite" ></asp:TextBox>
                    <asp:ImageButton ID="btSEARCHUSERIDSUBMIT" ToolTip="Search Staff ID" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />
                    
                    <asp:DataGrid runat="server" ID="mlDATAGRIDUSERID" 
                        HeaderStyle-BackColor="orange"  
                        HeaderStyle-VerticalAlign ="top"
                        HeaderStyle-HorizontalAlign="Center"
                        OnItemCommand="mlDATAGRIDUSERID_ItemCommand"        
                        autogeneratecolumns="false">	    
                        
                        <AlternatingItemStyle BackColor="#F9FCA8" />
                        <Columns>  
                            <asp:ButtonColumn  HeaderText = "EmployeeID" DataTextField = "UserID" ></asp:ButtonColumn>
                            <asp:ButtonColumn HeaderText = "Name"  DataTextField = "Name"></asp:ButtonColumn>                        
                        </Columns>
                     </asp:DataGrid> 
                    </asp:Panel>                       
                </td>         
             </tr>
             
        
    </table>    
    <hr /><br />
</asp:Panel>
</asp:TableCell>
</asp:TableRow>




</asp:Table>
</form>
<br /><br /><br /><br />

</asp:Content>

