<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterPrint.master" AutoEventWireup="false" CodeFile="ct_doc_ctentry.aspx.vb" Inherits="ap_doc_mr" title="Untitled Page" %>
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
    <asp:HiddenField ID="lbFILEDOCNO" runat="server"/>    
    
    <table width="100%" cellpadding="0" cellspacing="2" border="0">
    
    <tr>
    <td align="Center" colspan="2">       
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
    <td colspan="2">        
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
    <td valign="top">
        <table cellpadding="1" cellspacing="1" border="0">
        <tr>
            <td valign="top"><p><asp:Label ID="lbDOCUMENTNO" runat="server" Text="Document No"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top"><asp:Label ID="lbTDOCUMENTNO" runat="server" Enabled="false"></asp:Label></td>            
        </tr>
        
        <tr>
            <td valign="top"><asp:Label ID="lbDOCDATE1" Text="Date" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top">                             
                <asp:Label ID="lbTDOCDATE" runat="server"></asp:Label>                                                                                        
            </td>
        </tr>
        
    
        <tr>
            <td valign="top">
                <asp:Label ID="lbLCUST" Text="Customer" runat="server"></asp:Label>                    
            </td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:Label ID="lbTCUST" runat="server"></asp:Label>                                                                                        
                <asp:Label ID="lbCUSTDESC" Text="" runat="server"></asp:Label>                                               
            </td>
        </tr>
                     
        <tr>
            <td valign="top">
                <asp:Label ID="lbSITECARD" Text="Site Card" runat="server"></asp:Label>                    
            </td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:Label ID="lbTSITECARD" runat="server"></asp:Label>                                                                                        
                <asp:Label ID="lbSITEDESC" Text="" runat="server"></asp:Label>
            </td>
        </tr>
        
        
    <tr>
        <td valign="top"><p><asp:Label ID="lblADDR" runat="server" Text="Alamat Site"></asp:Label></p></td>
        <td valign="top">:</td>
        <td valign="top">
            <asp:Label ID="lbTADDR" runat="server" />
        </td>
    </tr>

    <tr>
        <td valign="top"><p><asp:Label ID="lbCITY" runat="server" Text="Kota"></asp:Label></p></td>
        <td valign="top">:</td>
        <td valign="top"><asp:Label ID="lbTCITY" runat="server" /></td>
    </tr> 

    <tr>
        <td valign="top"><p><asp:Label ID="lbSTATE" runat="server" Text="Propinsi"></asp:Label></p></td>
        <td valign="top">:</td>
        <td valign="top"><asp:Label ID="lbtSTATE" runat="server" /></td>
    </tr> 

     <tr>
        <td valign="top"><p><asp:Label ID="lbCOUNTRY" runat="server" Text="Negara"></asp:Label></p></td>
        <td valign="top">:</td>
        <td valign="top"><asp:Label ID="lbTCOUNTRY" runat="server" /></td>            
    </tr>

    <tr>
        <td valign="top"><p><asp:Label ID="lbZIP" runat="server" Text="Kode Pos"></asp:Label></p></td>
        <td valign="top">:</td>
        <td valign="top"><asp:Label ID="lbTZIP" runat="server" /></td>
    </tr> 

    <tr>
        <td valign="top"><p><asp:Label ID="lbPHONE1" runat="server" Text="Telp"></asp:Label></p></td>
        <td valign="top">:</td>
        <td valign="top"><asp:Label ID="lbTPHONE1" runat="server" /></td>
    </tr>
    
    <tr>
        <td valign="top"><p><asp:Label ID="lbPHONE_PIC" runat="server" Text="PIC & Hp"></asp:Label></p></td>
        <td valign="top">:</td>
        <td valign="top"><asp:Label ID="lbTPHONE1_PIC" runat="server" /></td>
    </tr>            
    
    </table>
    </td>
                
    <td valign="top">
        <table cellpadding="1" cellspacing="1" border="0">
         <tr>
            <td valign="top"><p><asp:Label ID="lbCTDOCNO" runat="server" Text="Contract No"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top"><asp:Label ID="lbTCTDOCNO" runat="server"></asp:Label></td>            
         </tr>
        
        <tr>
            <td valign="top"><asp:Label ID="lbDOCDATE2" Text="Create Date" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top">                
                <asp:Label ID="lbTDOCDATE2" runat="server"></asp:Label>                                                                                                                              
            </td>
        </tr>
        
        <tr>
            <td valign="top">
                <asp:Label ID="lbREFFNO" runat="server" Text="Reff No"></asp:Label>                    
             </td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:Label ID="lbTREFFNO" runat="server"></asp:Label>
                <asp:Label ID="lbREFFDOCNO" Text="" runat="server"></asp:Label>
            </td>                            
        </tr>
        
         
        <tr>
            <td valign="top"><asp:Label ID="lbCRDOCDATE1" Text="Start Date" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top">                             
                <asp:Label ID="lbTCRDOCDATE1" runat="server"></asp:Label>                                                                    
            </td>
        </tr>
         
         
        <tr>
            <td valign="top"><asp:Label ID="lbCRDOCDATE2" Text="End Date" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top">                
                <asp:Label ID="lbTCRDOCDATE2" runat="server"></asp:Label>                                                                                                                              
            </td>
        </tr>
         
        <tr>
            <td valign="top"><p><asp:Label ID="lbLPRODUCT" runat="server" Text="Service"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:Label ID="lbTPRODUCT" Text="" runat="server"></asp:Label>                                                                                       
                <asp:Label ID="lbPRODUCT" Text="" runat="server"></asp:Label>                                                                   
            </td>
        </tr> 
    
         <tr>
            <td valign="top"><p><asp:Label ID="lbMANPOWER" runat="server" Text="Man Power"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top"><asp:Label ID="lbTMANPOWER" runat="server" style="text-align:right"></asp:Label></td>            
         </tr>
         
         
         
         <tr id="tr1" runat="server">
            <td valign="top"><p><asp:Label ID="lbPERCENTAGE" runat="server" Text="Increment %"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top"><asp:Label ID="lbTPERCENTAGE" runat="server" style="text-align:right"></asp:Label></td>            
         </tr>
         
         <tr id="tr3" runat="server">
            <td valign="top"><p><asp:Label ID="lbPRICE2" runat="server" Text="Existing Price"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top"><asp:Label ID="lbTPRICE2" runat="server" style="text-align:right" ></asp:Label></td>            
         </tr>
         
         <tr id="tr4" runat="server">
            <td valign="top"><p><asp:Label ID="lbPRICE3" runat="server" Text="Propose Price"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top"><asp:Label ID="lbTPRICE3" runat="server"  style="text-align:right"></asp:Label></td>            
         </tr>
         
         <tr>
            <td valign="top"><p><asp:Label ID="lbPRICE" runat="server" Text="Price"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top"><asp:Label ID="lbTPRICE" runat="server" style="text-align:right"></asp:Label></td>            
         </tr>
         
         <tr id="trU1" runat="server">
            <td valign="top">                        
                <asp:Label ID="lbUSER" Text="Negotiator" runat="server"></asp:Label>                    
            </td>
            <td valign="top">:</td>
             <td valign="top">
                <asp:Label ID="lbTUSERID" runat="server"> </asp:Label>                                                    
                <asp:Label ID="lbTUSERDESC" Enabled="false" runat="server"></asp:Label> 
                
            </td>
         </tr>
         
         <tr id="tr2" runat="server">
            <td valign="top">
                <asp:Label ID="lbLBRANCH" Text="Branch" runat="server"></asp:Label>                    
            </td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:Label ID="lbTBRANCH" runat="server"></asp:Label>                    
                <asp:Label ID="lbBRANCH" Text="" runat="server"></asp:Label>                                               
            </td>
        </tr>
        
         <tr>
            <td valign="top"><p><asp:Label ID="lbDESCRIPTION" runat="server" Text="Remarks"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:Label ID="lbTDESCRIPTION"  runat="server" />
            </td>
         </tr>
         
        </table>
    </td>
    </tr>
        
    <tr> <td colspan="2"><br /><br /></td></tr>
    
    <tr id="trUP0" runat="server">
        <td colspan="2">
            <asp:DataGrid runat="server" ID="mlDATAGRID2" 
            HeaderStyle-BackColor="orange"  
            HeaderStyle-VerticalAlign ="top"
            HeaderStyle-HorizontalAlign="Center"                    
            autogeneratecolumns="true">	                   
                           
            <AlternatingItemStyle BackColor="#F9FCA8" />
            <Columns>                
                                    
                <asp:templatecolumn headertext="VW">
                <ItemTemplate>        
                    <asp:hyperlink  Target="_blank"  runat="server" id="lnLINK1" navigateurl='<%# String.Format("../pj_fs/fs_file_download_auth.ashx?mpID={0}&mpNO={1}", Eval("DocNo"),Eval("No")) %>' text="DW"></asp:hyperlink>
                </ItemTemplate>
                </asp:templatecolumn>
                
                
                 
            </Columns>
         </asp:DataGrid>  
        </td>
    </tr>
    
    
    </table>

    
    <br /><br />    
    <input type="button" value="Print" onclick="window.print();return false;" />
    <input type="button" value="Close" onclick="window.close();return false;" />
    
    </form>                        

<br /><br /><br /><br />
</asp:Content>
