<%@ Page Language="C#" MasterPageFile="~/pagesetting/MasterPrint.master" AutoEventWireup="true" CodeFile="FormPrint.aspx.cs" Inherits="pj_delinv_FormPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">

    <script type="text/javascript" language="Javascript">
    //<!-- hide script from older browsers
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

    function PrintPanel() {
        var panel = document.getElementById("<%=pnlContents.ClientID %>");
        //var printWindow = window.open('', '', 'height=400,width=800');
        var printWindow = this;
        printWindow.document.write('<html><head><title>DIV Contents</title>');
        printWindow.document.write('</head><body >');
        printWindow.document.write(panel.innerHTML);
        printWindow.document.write('</body></html>');
        printWindow.document.close();

        //setTimeout(function () {
        //  printWindow.print();
        //}, 500);

        printWindow.print();
        return false;
    }
    </script>

    <form id="form1" runat="server">
    <asp:Panel ID="pnlContents" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="1" bordercolor="gray" id="tb1"  runat="server">  
    <tr>
        <td align="Center">       
            <table width="100%" cellpadding="2" cellspacing="2" border="0" >           
                <tr>
                    <td align="right" style="width: 57px"><img src="../images/company/logo_100bw.png" 
                            alt="" align="left" style="height: 49px; width: 50px" />
                    </td>
            
                    <td align="left">
                        <p class="header2"><asp:Label ID="mlCOMPANYNAME" runat="server"></asp:Label></p>
                        <asp:Label ID="mlCOMPANYADDRESS" runat="server"></asp:Label><br />                                 
                    </td>
            
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="mlLBLTANGGAL" runat="server" Text="Tanggal"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="mlLBLTITIKDUA" runat="server" Text=" : "></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="mlTANGGAL" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="mlLBLRECEIPTCODE" runat="server" Text="Receipt Code"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="mlLBLTITIKDUA1" runat="server" Text=" : "></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="mlRECEIPTCODE" runat="server" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="mlLBLRESITIKI" runat="server" Text="Resi TIKI"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="mlLBLTITIKDUA2" runat="server" Text=" : "></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="mlRESITIKI" runat="server" ></asp:Label>
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
            <table width="100%" cellpadding="0" cellspacing="0" border="0">        
                <tr><td colspan="3"><br /></td></tr>
                        
                <tr>
                    <td align="center" colspan="3">
                        <p class="header2"><b><asp:Label ID="lbTITLE" runat="server"></asp:Label></b></p>
                        <%--<asp:HyperLink ID="mlLINKDOC" runat="server"></asp:HyperLink>--%>
                    </td>
                </tr>       
            
                <tr><td colspan="3"><br /></td></tr>
            </table>                     
        </td>
    </tr>
    
    <%--<tr>
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
    </tr>--%>
    
    <tr>
        <td>      
            <center>
            <table width="100%" cellpadding="0" cellspacing="0" border="0">        
            <tr><td colspan="3"><br /></td></tr>          
            <tr><td>
                <asp:Panel ID="pnGRID" runat="server" HorizontalAlign="Center">
                    <center>
                    <asp:DataGrid runat="server" ID="mlDATAGRID" HorizontalAlign="Center" 
                    HeaderStyle-BackColor="orange"  
                    HeaderStyle-VerticalAlign ="top"
                    HeaderStyle-HorizontalAlign="Center"                
                    autogeneratecolumns="false">	    
                
                    <AlternatingItemStyle BackColor="#F9FCA8" />
                    <Columns>
                        <asp:templatecolumn headertext="No">
                        <ItemTemplate>        
                            <%#Container.ItemIndex + 1%>
                        </ItemTemplate>        
                        </asp:templatecolumn>

                        <asp:BoundColumn HeaderText="Invoice No" DataField="InvNo" Visible="true"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Customer Name" DataField="InvCustName" Visible="true"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Amount" DataField="InvAmount" DataFormatString ="{0:#,##0}" ItemStyle-HorizontalAlign="right" Visible="true"></asp:BoundColumn>

                        <asp:BoundColumn HeaderText="Receipt Code" DataField="InvReceiptCode" Visible="false" ></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Status" DataField="InvStatus" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Description" DataField="InvDesc" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Prepared For" DataField="InvPreparedForDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Delivered" DataField="InvDeliveredDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Resi TIKI" DataField="InvResiTiki" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Messenger NIK" DataField="InvCodeMess" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Messenger Name" DataField="InvMessName" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="User NIK" DataField="UserID" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="User Name" DataField="UserName" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Customer Code" DataField="InvCustCode" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Invoice Date" DataField="InvDate" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Branch" DataField="InvBranch" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Proceeds" DataField="InvProceedsDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Returned" DataField="InvReturnedDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Done" DataField="InvDoneDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Penerima" DataField="InvCustPenerima" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Site Card" DataField="InvSiteCard" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Product Offering" DataField="InvProdOffer" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="OCM" DataField="InvOCM" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="FCM" DataField="InvFCM" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Collector" DataField="InvCollector" Visible="false"></asp:BoundColumn>

                    </Columns>
                    </asp:DataGrid>
                    </center>
                </asp:Panel>
            </td></tr>
            <tr><td colspan="3"><br /><br /></td></tr>
            </table>
            </center>           
        </td>
    </tr>
    
    <tr>
        <td>
            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td style="width:50%;" align="center">
                        <asp:Label ID="mlLBLRECEIVEDBY" runat="server" Text="Received By" ></asp:Label>
                    </td>
                    <td style="width:50%;" align="center">
                        <asp:Label ID="mlLBLPREPAREDBY" runat="server" Text="Prepared By" ></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>

    <tr>
        <td style="height:100px;">
            
        </td>
    </tr>
    
    <tr>
        <td>
            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td style="width:50%;" align="center">
                        <asp:Label ID="mlRECEIVEDBY" runat="server"></asp:Label>
                    </td>
                    <td style="width:50%;" align="center">
                        <asp:Label ID="mlPREPAREDBY" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>

    </table>
    </asp:Panel>

    <table width="100%">
        <tr>
            <td align="center">
                <input type="button" value="Print" onclick="return PrintPanel();" />
                <input type="button" value="Close" onclick="window.close();return false;" />    
            </td>
        </tr>
    </table>
    
    <%--<br />    
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
    </tr>
    </table>--%>
    
    
    
    
    </form>

    <br /><br /><br /><br />

</asp:Content>
