<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterPrint.master" AutoEventWireup="false" CodeFile="ap_doc_mr_worksheet.aspx.vb" Inherits="ap_doc_mr_worksheet" title="Untitled Page" %>
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
    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="gray" id="tb1"  runat="server">  
    <tr>
        <td align="Center">       
            <table width="100%" cellpadding="2" cellspacing="2" border="0" >           
            <tr>
            <td align="right"><img src="../images/company/logo_100bw.png" alt="" /></td>
            
            <td align="left">
                <p class="header2"><asp:Label ID="mlCOMPANYNAME" runat="server"></asp:Label></p>
                <asp:Label ID="mlCOMPANYADDRESS" runat="server"></asp:Label><br />                                 
            </td>
            
            </tr>
            </table>            
        </td>
    </tr>
    
    <tr>
        <td>
        <table width="100%" cellpadding="0" cellspacing="0" border="0">        
            <tr><td colspan="3"><br /><br /></td></tr>
                        
            <tr>
                <td align="center" colspan="3">
                    <p class="header2"><b><asp:Label ID="lbTITLE" runat="server"></asp:Label></b></p>
                    <asp:HyperLink ID="mlLINKDOC" runat="server"></asp:HyperLink>
                </td>
            </tr>       
            
            <tr><td colspan="3"><br /><br /></td></tr>
         </table>                     
        </td>
    </tr>
    
    <tr>
    <td>        
        <table width="100%" cellpadding="0" cellspacing="0"  border="0">        
            <tr><td colspan="3"><br /></td></tr>
            
            <tr>
                <td><p>Doc No</p></td>
                <td><p>:</p></td>
                <td><p><asp:Label ID="lbDOCNO" runat="server"></asp:Label></p></td>
                <td><p></p></td>
                <td><p>Create Date</p></td>
                <td><p>:</p></td>
                <td><p><asp:Label ID="lbRECDATE" runat="server"></asp:Label></p></td>                
            </tr>
            <tr><td colspan="3"><br /><br /></td></tr>
         </table>                     
        </td>
    </tr>
    
    <tr>
        <td>        
        <table width="100%" cellpadding="0" cellspacing="0"  border="0">        
            <tr><td colspan="3"><br /></td></tr>          

            <tr><td>
                <asp:Panel ID="pnGRID" runat="server">                    
                <asp:DataGrid runat="server" ID="mlDATAGRID" 
                HeaderStyle-BackColor="orange"  
                HeaderStyle-VerticalAlign ="top"
                HeaderStyle-HorizontalAlign="Center"                
                autogeneratecolumns="true">	    
                
                <AlternatingItemStyle BackColor="#F9FCA8" />
                <Columns>
                    <asp:templatecolumn headertext="No">
                    <ItemTemplate>        
                        <%#Container.ItemIndex + 1%>
                    </ItemTemplate>        
                    </asp:templatecolumn>
                </Columns>
                </asp:DataGrid>  
                </asp:Panel>
            </td></tr>
            </table>            
        </td>
    </tr>
    
    <tr>
    <td>
        <input type="button" value="Print" onclick="window.print();return false;" />
        <input type="button" value="Close" onclick="window.close();return false;" />    
    </td>
    </tr>
    </table>
    
    
    <br />    
    <table width="100%" cellpadding="0" cellspacing="0" border="0" bordercolor="gray" id="Table1"  runat="server">  
    <tr>
        <td>
            <asp:ImageButton id="btVIEW" ToolTip="View Data" ImageUrl="~/images/toolbar/browse.jpg" runat="server" />
            <font>View Data</font>        
        </td>
        <td>&nbsp;&nbsp;|&nbsp;&nbsp;</td>
        
        <td>
            <asp:ImageButton id="btEXPORTTOEXCEL" ToolTip="Excel" ImageUrl="~/images/toolbar/excelfile.png" runat="server" />
            <font>Export to Excel I</font>        
        </td>
        <td>&nbsp;&nbsp;|&nbsp;&nbsp;</td>
        
        <td>
            <asp:ImageButton id="btEXPORTTOEXCEL2" ToolTip="Excel" ImageUrl="~/images/toolbar/excelfile.png" runat="server" />
            <font>Export to Excel II</font>  
        
        </td>
        <td>&nbsp;&nbsp;|&nbsp;&nbsp;</td>
        
        <td>
            <asp:ImageButton id="btExCsv" ToolTip="Csv" ImageUrl="~/images/toolbar/csvfile.png" runat="server" />
            <font>Export to CSV</font>
            <asp:HyperLink ID="mlLINKDOC2" runat="server"></asp:HyperLink>        
        </td>
        <td>&nbsp;&nbsp;|&nbsp;&nbsp;</td>
        
        <td>              
            <asp:ImageButton id="btCR1" ToolTip="Crystal Report" ImageUrl="~/images/toolbar/crystalreportsfile.png" runat="server" />
            <font>View with Crystal Report</font>   
        
        </td>        
        <td>
            <asp:ImageButton id="btRV" ToolTip="Report Viewer" ImageUrl="~/images/toolbar/rptViewer.png" runat="server"  />
            <font>View with Report Viewer</font>   

        </td>
    </tr>
    </table>
    

    
    
    </form>                        

    <br /><br /><br /><br />
</asp:Content>
