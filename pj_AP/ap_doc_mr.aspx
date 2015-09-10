<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterPrint.master" AutoEventWireup="false" CodeFile="ap_doc_mr.aspx.vb" Inherits="ap_doc_mr" title="Untitled Page" %>
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
            
            <tr>
                <td><p>Site Card</p></td>
                <td><p>:</p></td>
                <td><p><asp:Label ID="lbSITECARD" runat="server"></asp:Label></p></td>
                <td><p></p></td>
                <td><p>No</p></td>
                <td><p>:</p></td>
                <td><p><asp:Label ID="lbDOCNO" runat="server"></asp:Label></p></td>                
            </tr>
            
            <tr>
                <td><p>Nama proyek</p></td>
                <td><p>:</p></td>
                <td><p><asp:Label ID="lbPRJNAME" runat="server"></asp:Label></p></td>                
                <td><p></p></td>
                <td><p>Departemen</p></td>
                <td><p>:</p></td>
                <td><p><asp:Label ID="lbDEPT" runat="server"></asp:Label></p></td>
            </tr>            
            
            <tr>
                <td><p>Lokasi Site</p></td>
                <td><p>:</p></td>
                <td><p><asp:Label ID="lbLOCATION" runat="server"></asp:Label></p></td>                
                <td><p></p></td>
                <td><p>Periode</p></td>
                <td><p>:</p></td>
                <td><p><asp:Label ID="lbPERIOD" runat="server"></asp:Label></p></td>
            </tr>            
            
            <tr>
                <td><p>Keterangan</p></td>
                <td><p>:</p></td>
                <td><p><asp:Label ID="lbDESCRIPTION" runat="server"></asp:Label></p></td>                
                <td><p></p></td>
                <td><p>Persentase</p></td>
                <td><p>:</p></td>
                <td><p><asp:Label ID="lbPERCENTAGE" runat="server"></asp:Label></p></td>
            </tr>                        
        
            <tr><td colspan="7"><br /><hr /></td></tr>
            
            <tr><td colspan="7"><p class="header2"><b>Delivery Address</b></p></td></tr>
            
            <tr>
                <td><p>Alamat</p></td>
                <td><p>:</p></td>
                <td><p><asp:Label ID="lbADDR" runat="server"></asp:Label></p></td>
                <td><p></p></td>
                <td><p>Telp</p></td>
                <td><p>:</p></td>
                <td><p><asp:Label ID="lbPHONE1" runat="server"></asp:Label></p></td>                
                
            </tr>
            
            <tr>
                <td><p>Kota</p></td>
                <td><p>:</p></td>
                <td><p><asp:Label ID="lbCITY" runat="server"></asp:Label></p></td>
                <td><p></p></td>
                <td><p>Telp PIC</p></td>
                <td><p>:</p></td>
                <td><p><asp:Label ID="lbPHONE_PIC" runat="server"></asp:Label></p></td>                
            </tr>
            
            <tr>
                <td><p>Propinsi</p></td>
                <td><p>:</p></td>
                <td><p><asp:Label ID="lbPROVINCE" runat="server"></asp:Label></p></td>
                <td><p></p></td>
                <td><p>Dept Code</p></td>
                <td><p>:</p></td>
                <td><p><asp:Label ID="lbDEPTCODE" runat="server"></asp:Label></p></td>                
            </tr>
            
             <tr>
                <td><p>Kode Pos</p></td>
                <td><p>:</p></td>
                <td><p><asp:Label ID="lbZIP" runat="server"></asp:Label></p></td>
                <td><p></p></td>
                <td colspan="3"><br /></td>
            </tr>
            
            <tr><td colspan="7"><br /></td></tr>
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
                HeaderStyle-BackColor="orange"  
                HeaderStyle-VerticalAlign ="top"
                HeaderStyle-HorizontalAlign="Center"                
                autogeneratecolumns="true">	    
                
                <AlternatingItemStyle BackColor="#F9FCA8" />
                <Columns>
                </Columns>
                </asp:DataGrid>  
                </asp:Panel>
            </td></tr>
                 
            <tr><td colspan="3"><br /></td></tr>
            
            <tr id="TR1" runat="server" visible = "false">
            <td colspan="3" align="right">
                <table width="100%" cellpadding="0" cellspacing="0"  border="0"> 
                <tr>
                    <td align="center"><h4><b>Total</b></h4></td>
                    <td align="right">
                    <h4><b><asp:Label ID="lbTOTAL" runat="server"></asp:Label> </b></h4>
                    </td>
                </tr>
                </table>                
            </td>
        </tr>
        
         </table>            
        </td>
    </tr>
    
    <tr>
        <td>        
            <br />
            <asp:Panel ID="pnGRID2" runat="server">                    
            <asp:DataGrid runat="server" ID="mlDATAGRID2" 
            HeaderStyle-BackColor="orange"  
            HeaderStyle-VerticalAlign ="top"
            HeaderStyle-HorizontalAlign="Center"                
            autogeneratecolumns="true">	    
            
            <AlternatingItemStyle BackColor="#F9FCA8" />
            <Columns>
            </Columns>
            </asp:DataGrid>  
            </asp:Panel>
            <br />
        </td>
    </tr>
    
    
    
    
    <tr>
    <td>        
        <table width="60%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td colspan="6"><br /></td>
        </tr>
                
        
        <tr>                
            <td><p>Dibuat Oleh,</p></td>
            <td><p>:</p></td>
            <td><p><asp:Label ID="lbPREPARED1" runat="server"></asp:Label></p></td>                
            <td><p></p></td>
            <td><p>Tgl :</p></td>
            <td><p><asp:Label ID="lbPREPARED_DATE1" runat="server"></asp:Label></p></td>                
        </tr>
        
        <tr>                
            <td><p>Diperiksa Oleh,</p></td>
            <td><p>:</p></td>
            <td><p><asp:Label ID="lbPREPARED2" runat="server"></asp:Label></p></td>                
            <td><p></p></td>
            <td><p>Tgl :</p></td>
            <td><p><asp:Label ID="lbPREPARED_DATE2" runat="server"></asp:Label></p></td>                
        </tr>
        
        <tr>                
            <td><p>DiSetujui Oleh,</p></td>
            <td><p>:</p></td>
            <td><p><asp:Label ID="lbPREPARED3" runat="server"></asp:Label></p></td>                
            <td><p></p></td>
            <td><p>Tgl :</p></td>
            <td><p><asp:Label ID="lbPREPARED_DATE3" runat="server"></asp:Label></p></td>                
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
