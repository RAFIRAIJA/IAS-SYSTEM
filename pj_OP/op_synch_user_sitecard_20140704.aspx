<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="op_synch_user_sitecard_20140704.aspx.vb" Inherits="op_synch_user_sitecard_20140704"%>
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
<asp:TableRow><asp:TableCell><p><asp:HyperLink ID="mlLINKDOC" runat="server"></asp:HyperLink></p></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><asp:HiddenField ID="mlSYSCODE2" runat="server"/></asp:TableCell></asp:TableRow>


<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnFILL" runat="server">
    
   
        <table width="50%" cellpadding="1" cellspacing="1" border="0">                 
        <tr>
            <td><asp:Label ID="lbENTITY" runat="server" Text="Entity"></asp:Label></td>
            <td>:</td>
            <td><asp:DropDownList ID="ddENTITY" runat="server"></asp:DropDownList></td>        
        </tr>
        
            <tr>
                <td valign="top"><asp:Label ID="lbSYNCHRONIZE" Text="01. Synchronize User Site Card From NAV" runat="server"></asp:Label></td>
                <td valign="top">:</td>
                <td valign="top"><asp:ImageButton ID="btSYNCHRONIZE" ToolTip="Synchronize" ImageUrl="~/images/toolbar/synchronize.gif" runat="server" OnClientClick="return confirm('Synchronize Record ?');" /></td>            
            </tr>
            
            <tr>
                <td valign="top"><asp:Label ID="Label1" Text="02. Synchronize User Site Card From NAV (1 by 1)" runat="server"></asp:Label></td>
                <td valign="top">:</td>
                <td valign="top"><asp:ImageButton ID="btSYNCHRONIZE2" ToolTip="Synchronize" ImageUrl="~/images/toolbar/synchronize.gif" runat="server" OnClientClick="return confirm('Synchronize Record (1 by 1) ?');" /></td>            
            </tr>
            
            
            <tr>
                <td valign="top"><asp:Label ID="Label3" Text="03. Synchronize User Site Card From NAV (By SiteCard)" runat="server"></asp:Label></td>
                <td valign="top">:</td>                
                <td valign="top">                
                </td>            
            </tr>
            
            <tr>
                <td valign="top" colspan="3">
                    <table>
                    <tr>
                        <td valign="top">
                            <asp:Label ID="lbSITECARD" Text="Site Card" runat="server"></asp:Label>
                            <asp:ImageButton ID="btSEARCHSITECARD" ToolTip="Kode Site Card" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                                
                        </td>
                        <td valign="top">:</td>
                        <td valign="top">
                            <asp:TextBox ID="txSITECARD" runat="server" Width="100"></asp:TextBox>                                                                    
                            <asp:ImageButton ID="btSITECARD" ToolTip="Cari Site Card" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />  
                            <asp:Label ID="lbSITEDESC" Text="" runat="server"></asp:Label>
                            <asp:ImageButton ID="btSYNCHRONIZE3" ToolTip="Synchronize" ImageUrl="~/images/toolbar/synchronize.gif" runat="server" OnClientClick="return confirm('Synchronize Record (By SiteCard) ?');" />    
                        </td>
                    </tr>
                    
                    <tr>                    
                        <td valign="top"></td>            
                        <td valign="top"></td> 
                        <td valign="top">
                            <asp:Panel ID="pnSEARCHSITECARD" runat="server">                            
                                <asp:Label ID="lbSEARCHSITECARD" Text="Nama Site : " runat="server"></asp:Label>
                                <asp:TextBox ID="mlSEARCHSITECARD" runat="server" BackColor="AntiqueWhite" Width="300"></asp:TextBox>
                                <asp:ImageButton ID="btSEARCHSITECARDSUBMIT" ToolTip="Search Site" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                            
                                
                                <asp:DataGrid runat="server" ID="mlDATAGRIDSITECARD" 
                                    HeaderStyle-BackColor="orange"  
                                    HeaderStyle-VerticalAlign ="top"
                                    HeaderStyle-HorizontalAlign="Center"
                                    OnItemCommand="mlDATAGRIDSITECARD_ItemCommand"        
                                    autogeneratecolumns="false">	    
                                    
                                    <AlternatingItemStyle BackColor="#F9FCA8" />
                                    <Columns>  
                                        <asp:ButtonColumn  HeaderText = "Kode" DataTextField = "Field_ID" ></asp:ButtonColumn>
                                        <asp:ButtonColumn HeaderText = "Nama"  DataTextField = "Field_Name"></asp:ButtonColumn>
                                        <asp:ButtonColumn  HeaderText = "Branch" DataTextField = "Branch" ></asp:ButtonColumn>
                                        <asp:ButtonColumn HeaderText = "Cust"  DataTextField = "CustID"></asp:ButtonColumn>
                                    </Columns>
                                 </asp:DataGrid> 
                            </asp:Panel>            
                        </td>
                    </tr>                    
                    </table>
                    
                    
                </td>            
            </tr>
            
            <tr>
                <td valign="top"><asp:Label ID="Label2" Text="04. View Error Data Without Level Approval" runat="server"></asp:Label></td>
                <td valign="top">:</td>
                <td valign="top"><asp:ImageButton ID="btERRMRLEVEL" ToolTip="No Approval Person" ImageUrl="~/images/toolbar/view.gif" runat="server" /></td>            
            </tr>
            
            <tr>
                <td valign="top"><asp:Label ID="Label4" Text="05. View Not Synch SiteCard" runat="server"></asp:Label></td>
                <td valign="top">:</td>
                <td valign="top"><asp:ImageButton ID="btERRSITECARD" ToolTip="Error SiteCard" ImageUrl="~/images/toolbar/view.gif" runat="server" /></td>            
            </tr>
            
            
            <tr>
                <td valign="top"><asp:Label ID="lbUSERIDVIEW" Text="06. View Unknown UserID for System Login" runat="server"></asp:Label></td>
                <td valign="top">:</td>
                <td valign="top"><asp:ImageButton ID="btUSERIDVIEW" ToolTip="View User" ImageUrl="~/images/toolbar/view.gif" runat="server" /></td>            
            </tr>
            
            <tr>
                <td valign="top"><asp:Label ID="lbUSERIDADD" Text="07. Add Unknown UserID to System Login" runat="server"></asp:Label></td>
                <td valign="top">:</td>
                <td valign="top"><asp:ImageButton ID="btUSERIDADD" ToolTip="Add User" ImageUrl="~/images/toolbar/submit.gif" runat="server" OnClientClick="return confirm('Submit Record ?');" /></td>            
            </tr>           
        </table> 
      
    <hr /><br />
</asp:Panel>
</asp:TableCell>
</asp:TableRow>


<asp:TableRow><asp:TableCell>
    <p><asp:Label ID="lbRESULT" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p>
</asp:TableCell></asp:TableRow>

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
               
    </Columns>
 </asp:DataGrid>  
</asp:Panel>

</asp:TableCell>
</asp:TableRow>

</asp:Table>
</form>
<br /><br /><br /><br />

</asp:Content>

