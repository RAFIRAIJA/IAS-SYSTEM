<%@ Page language="VB" MasterPageFile="~/pagesetting/MasterPrint.master" AutoEventWireup="false" CodeFile="mk_doc_file.aspx.vb" Inherits="mk_doc_file" title="Untitled Page" %>
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
            
            <tr><td colspan="3"><br /></td></tr>
            <tr><td colspan="3"><hr /></td></tr>            
            <tr><td colspan="3"><br /></td></tr>            
            
            <tr>
                <td colspan="3" align="left"><p><asp:HyperLink ID="mlLINKDOC" runat="server"></asp:HyperLink></p></td>                
            </tr>            
            
            <tr><td colspan="3"><br /></td></tr>            
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
