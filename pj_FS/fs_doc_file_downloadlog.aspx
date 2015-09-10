<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterPrint.master" AutoEventWireup="false" CodeFile="fs_doc_file_downloadlog.aspx.vb" Inherits="fs_doc_file_downloadlog" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">

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
    
    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="gray">
    <tr>
        <td align="Center">       
            <table width="100%" cellpadding="2" cellspacing="2" border="0" >           
            <tr>
            <td align="Right"><img src="../images/company/logo_100bw.png" alt="" /></td>
            
            <td align="center">
                <p class="header2"><asp:Label ID="mlCOMPANYNAME" runat="server"></asp:Label></p>
                <asp:Label ID="mlCOMPANYADDRESS" runat="server"></asp:Label><br />                                 
            </td>
            
            <td></td>
            
            </tr>
            </table>            
        </td>
    </tr>
    
    <tr>
    <td>        
        <table width="100%" cellpadding="0" cellspacing="0"  border="0">
        
            <tr><td colspan="3"><br /><br /></td></tr>
            
            <tr>
                <td align="center" colspan="3">
                    <p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p>
                    <p class="header2"><b><asp:Label ID="lbTITLE" runat="server"></asp:Label></b></p>
                    <p class="header2"><b><asp:Label ID="lbTITLE2" runat="server"></asp:Label></b></p>                    
                </td>
            </tr>       
            
            <tr><td colspan="3"><br /><br /></td></tr>
         </table>                     
        </td>
    </tr>    
    
    <tr>
        <td>        
        <table width="100%" cellpadding="0" cellspacing="0"  border="0">        
            <tr><td colspan="7"><br /></td></tr>

            <tr><td>
                <asp:Panel ID="pnGRID" runat="server">                    
                <asp:DataGrid runat="server" ID="mlDATAGRID"                 
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
            </td></tr>
                 
         
         </table>            
        </td>
    </tr>
    
    </table>
    
    
    <br /><br />    
    <input type="button" value="Print" onclick="window.print();return false;" />
    <input type="button" value="Close" onclick="window.close();return false;" />
    
    </form>                        

<br /><br /><br /><br />
</asp:Content>
