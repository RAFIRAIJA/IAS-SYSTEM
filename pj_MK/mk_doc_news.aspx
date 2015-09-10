<%@ Page language="VB" MasterPageFile="~/pagesetting/MasterPrint.master" AutoEventWireup="false" CodeFile="mk_doc_news.aspx.vb" Inherits="backoffice_mk_doc_news" title="Untitled Page" %>
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
            <table width="50%" cellpadding="2" cellspacing="2" border="0" >           
            <tr>
            <td align="Right"><img src="../images/company/logo_100bw.png" /></td>
            
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
                    <p class="header2"><b><asp:Label ID="lbTITLE" runat="server"></asp:Label></b></p>
                </td>
            </tr>       
            
            <tr><td colspan="3"><br /><br /></td></tr>
         </table>
                     
        </td>
    </tr>
    
    <tr>
    <td>        
        <table width="50%" cellpadding="0" cellspacing="0"  border="0">
        
            <tr><td colspan="3"><br /></td></tr>
            
            <tr>
                <td><p>No Dok</p></td>
                <td><p>:</p></td>
                <td><p><asp:Label ID="lbDOCNO" runat="server"></asp:Label></p></td>
            </tr>
            
            <tr>
                <td><p>Tgl Dok</p></td>
                <td><p>:</p></td>
                <td><p><asp:Label ID="lbDOCDATE" runat="server"></asp:Label></p></td>                
            </tr>            
            
            <tr><td colspan="3"><br /></td></tr>
            
            </table>            
        </td>
    </tr>
    
    <tr>
    <td>        
    <table width="100%" cellpadding="0" cellspacing="0"   border="0">
            <tr><td colspan="3"><br /></td></tr>
            
            <tr>                
                <td colspan="3" align="left"><p><b><asp:Label ID="lbSUBJECT" runat="server" ></asp:Label></b></p></td>
            </tr>
            
            <tr><td colspan="3"><br /><br /></td></tr>
            
            
            <tr>                
                <td colspan="3" align="left"><p><asp:Label ID="lbDESC1" runat="server" ></asp:Label></p></td>
            </tr>
            
            <tr><td colspan="3"><br /><br /></td></tr>
            
            <tr>                
                <td colspan="3" align="left"><p><asp:Label ID="lbDESC2" runat="server"></asp:Label></p></td>
            </tr>
            
            <tr><td colspan="3"><br /><br /></td></tr>
            
            <tr>                
                <td colspan="3" align="left"><p><asp:Label ID="lbDESC3" runat="server"></asp:Label></p></td>
            </tr>
            
            <tr><td colspan="3"><br /><br /></td></tr>
            
            <tr>                
                <td colspan="3" align="left"><p><asp:Label ID="lbDESC4" runat="server"></asp:Label></p></td>
            </tr>
            
            <tr><td colspan="3"><br /><br /></td></tr>
            
            <tr>                
                <td colspan="3" valign="top" align="center">
                <table width="1000px" cellpadding="0" cellspacing="0" border="0">
                <tr><td>
                <asp:Image ID="imgPIC1" runat="server" />
                </td></tr>
                </table>
                </td>
            </tr>
            
            <tr><td colspan="3"><br /><br /></td></tr>
            
            <tr>
                <td colspan="3" valign="top" align="center">
                <table width="1000px" cellpadding="0" cellspacing="0" border="0">
                <tr><td>
                <asp:Image ID="imgPIC2" runat="server" />
                </td></tr>
                </table>                                
                </td>
            </tr>
            
            
        </table>
        
    </td>
    </tr>
    
    
    
    <tr>
    <td>
        <asp:DataGrid runat="server" ID="mlDATAGRID" 
        HeaderStyle-BackColor="#cdcfd0"   
        HeaderStyle-VerticalAlign ="top"
        HeaderStyle-HorizontalAlign="Center"        
        autogeneratecolumns="false" Width="100%">	           
        
        <Columns>            
        </Columns>
     </asp:DataGrid>      
    </tr>
    
    </table>
    
    
    <br /><br />    
    <input type="button" value="Print" onclick="window.print();return false;" />
    <input type="button" value="Close" onclick="window.close();return false;" />
    
    </form>                        

<br /><br /><br /><br />
</asp:Content>
