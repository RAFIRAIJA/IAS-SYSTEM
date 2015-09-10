<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterPrint.master" AutoEventWireup="false" CodeFile="ap_doc_mr_schedule.aspx.vb" Inherits="ap_doc_mr_schedule" title="Untitled Page" %>
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
                </Columns>
                </asp:DataGrid>  
                </asp:Panel>
            </td></tr>
            </table>            
        </td>
    </tr>
    
    <tr>
        <td>        
        <table width="100%" cellpadding="0" cellspacing="0"  border="0">        
            <tr><td colspan="3"><br /></td></tr>          

            <tr><td>
                <asp:CheckBox  ID="chkVIEW" runat="server" tEXT="View Customized" AutoPostBack="true"/>
                <asp:Panel ID="pnGRID2" runat="server">
                    <%
                        Dim mlTEMPVENDOR As String
                        Dim mlTEMPITEMKEY As String
                        
                        mlTEMPVENDOR = ""
                        mlTEMPITEMKEY = ""
                        Response.Write("<table width=100% cellpadding=0 cellspacing=0 border=1>")
                        Response.Write("<tr>")
                        Response.Write("<td>Vendor No.<td>")
                        Response.Write("<td>State<td>")
                        Response.Write("<td>No_MR<td>")
                        Response.Write("<td>Site Card No.<td>")
                        Response.Write("<td>Site Card Search Name<td>")
                        Response.Write("<td>Unit<td>")
                        Response.Write("<td>Qty<td>")
                        Response.Write("<tr>")
                        
                        While mlREADER2.Read
                            If mlREADER2("ItemKey") <> mlTEMPITEMKEY Then
                                mlTEMPITEMKEY = mlREADER2("ItemKey")
                                Response.Write("<tr><td colspan=7>" & mlREADER2("ItemKey") & " - " & mlREADER2("ItemDesc") & "</td></tr>")
                                
                                Response.Write("<tr>")
                                Response.Write("<td>" & mlREADER2("Vendor No.") & "<td>")
                                Response.Write("<td>" & mlREADER2("State") & "<td>")
                                Response.Write("<td>" & mlREADER2("No_MR") & "<td>")
                                Response.Write("<td>" & mlREADER2("Site Card No.") & "<td>")
                                Response.Write("<td>" & mlREADER2("Site Card Search Name") & "<td>")
                                Response.Write("<td>" & mlREADER2("Unit") & "<td>")
                                Response.Write("<td>" & mlREADER2("Qty") & "<td>")
                                Response.Write("<tr>")
                            Else
                                Response.Write("<tr>")
                                Response.Write("<td>" & mlREADER2("Vendor No.") & "<td>")
                                Response.Write("<td>" & mlREADER2("State") & "<td>")
                                Response.Write("<td>" & mlREADER2("No_MR") & "<td>")
                                Response.Write("<td>" & mlREADER2("Site Card No.") & "<td>")
                                Response.Write("<td>" & mlREADER2("Site Card Search Name") & "<td>")
                                Response.Write("<td>" & mlREADER2("Unit") & "<td>")
                                Response.Write("<td>" & mlREADER2("Qty") & "<td>")
                                Response.Write("<tr>")
                            End If
                            
                            
                        End While
                     
                        
                        Response.Write("<table>")
                    
                    %>                    
                
                </asp:Panel>
            </td></tr>
            </table>            
        </td>
    </tr>
    
    
    
    </table>
    
    
    <br /><br />    
    <asp:Button ID="btEXPORTTOEXCEL" TEXT="Export to Excel" Width="200" runat="server" />
    <input type="button" value="Print" onclick="window.print();return false;" />
    <input type="button" value="Close" onclick="window.close();return false;" />    
    
    </form>                        

    <br /><br /><br /><br />
</asp:Content>
