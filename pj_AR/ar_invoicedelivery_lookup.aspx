<%@ Page Title="" Language="C#" MasterPageFile="~/pagesetting/MsPageBlank.master" AutoEventWireup="true" CodeFile="ar_invoicedelivery_lookup.aspx.cs" Inherits="pj_ar_ar_invoicedelivery_lookup" %>
<%@ Register src="~/usercontroller/ucPaging.ascx" tagname="ucPaging" tagprefix="uc2" %>
<%@ Register src="~/UserController/ValidDate.ascx" tagname="ValidDate" tagprefix="uc1" %>
<%@ Register src="~/UserController/ucInputNumber.ascx" tagname="ucInputNumber" tagprefix="uc4" %>


<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <link href="~/script/NewStyle.css" type="text/css" rel="stylesheet" />
    <script src="~/Include/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="~/Include/JavaScript/Eloan.js" type="text/javascript"></script>
    <script type="text/javascript">
        function WinClose()
        {
            Window.close();
        }
    </script>

     <form id="frmtypedoclampiranlookup" runat="server">
        <table border="0" cellpadding="2" cellspacing="1" width="100%">
            <tr>
                <td>
                    <p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p>
                </td>
            </tr>
            <tr>
                <td>
                    <b><asp:Label id="mlTITLE" runat="server"></asp:Label></b>
                </td>
            </tr>            
        </table>
         <br />
        <asp:Panel ID="pnlInfo" runat="server" Width="100%" >
            <table runat="server" border="0" cellpadding="0" cellspacing="0" width="95%">
                <tr>
                    <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                    <td align="center" class="tdtopi">INFORMATION RECEIPT CODE</td>
                    <td width="10" class="tdtopikanan">&nbsp;</td>
                </tr>
            </table>
            <table runat="server" border="0" cellpadding="2" cellspacing="1" width="95%">
                <tr>
                    <td class="tdganjil" width="15%">Receipt Code</td>
                    <td class="tdganjil" width="35%">
                         :&nbsp&nbsp<asp:Label runat="server" ID="lblReceiptCode"></asp:Label>
                    </td>                    
                    <td class="tdganjil" width="15%">Receipt Code Status</td>
                    <td class="tdganjil" width="35%">
                         :&nbsp&nbsp<asp:Label runat="server" ID="lblReceiptCodeStatus"></asp:Label>
                    </td>                 
                </tr>
                <tr>
                    <td class="tdganjil" width="15%">Proceeds Date</td>
                    <td class="tdganjil" width="35%">
                         :&nbsp&nbsp<asp:Label runat="server" ID="lblProceedDate"></asp:Label>
                    </td>                    
                    <td class="tdganjil" width="15%">Sending Date</td>
                    <td class="tdganjil" width="35%">
                         :&nbsp&nbsp<asp:Label runat="server" ID="lblSendingDate"></asp:Label>
                    </td>                 
                </tr>
                <tr>
                    <td class="tdganjil" width="15%">Delivery Date</td>
                    <td class="tdganjil" width="35%">
                         :&nbsp&nbsp<asp:Label runat="server" ID="lblDeliveryDate"></asp:Label>
                    </td>                    
                    <td class="tdganjil" width="15%">Return Date</td>
                    <td class="tdganjil" width="35%">
                         :&nbsp&nbsp<asp:Label runat="server" ID="lblReturnDate"></asp:Label>
                    </td>                 
                </tr>
                <tr>
                    <td class="tdganjil" width="15%">Customer Name</td>
                    <td class="tdganjil" width="35%">
                         :&nbsp&nbsp<asp:Label runat="server" ID="lblCustName"></asp:Label>
                    </td>                    
                    <td class="tdganjil" width="15%">Customer Address</td>
                    <td class="tdganjil" width="35%">
                         :&nbsp&nbsp<asp:Label runat="server" ID="lblCustAddress"></asp:Label>
                    </td>                 
                </tr> 
                <tr>
                    <td class="tdganjil" width="15%">Messanger Name</td>
                    <td class="tdganjil" width="35%">
                         :&nbsp&nbsp<asp:Label runat="server" ID="lblMessangerName"></asp:Label>
                    </td>                    
                    <td class="tdganjil" width="15%">Messanger Type</td>
                    <td class="tdganjil" width="35%">
                         :&nbsp&nbsp<asp:Label runat="server" ID="lblMessangerType"></asp:Label>
                    </td>                 
                </tr> 
                               
            </table> 
        </asp:Panel>
         <br />
        <asp:Panel ID="pnlListData" runat="server" Width="100%" >
            <table runat="server" border="0" cellpadding="0" cellspacing="0" width="95%">
                <tr>
                    <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                    <td align="center" class="tdtopi">DETAIL INVOICE - RECEIPT CODE </td>
                    <td width="10" class="tdtopikanan">&nbsp;</td>
                </tr>
                </table>
            <table runat="server" cellSpacing="0" cellPadding="0" width="95%" border="0">
                <tr>
                    <td colspan="4">
                        <asp:DataGrid ID="dgListData" runat="server" AutoGenerateColumns="False" 
                            AllowSorting="true" borderwidth="0px"
                            Width="100%" CssClass="tablegrid" 
                            CellPadding="3" CellSpacing="1"                             
                            onitemdatabound="dgListData_ItemDataBound" >
                            <SelectedItemStyle CssClass="tdgenap"></SelectedItemStyle>
                            <AlternatingItemStyle CssClass="tdgenap"></AlternatingItemStyle>
                            <ItemStyle CssClass="tdganjil"></ItemStyle>
                            <HeaderStyle CssClass="tdjudul" HorizontalAlign="Center" Height="30px"></HeaderStyle>
                            <Columns>     
                                <asp:TemplateColumn HeaderText="INVOICE NO" ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "inv_invoiceno") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>                                                                                                                                                                                                  
                                <asp:TemplateColumn HeaderText="INVOICE DATE" ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "inv_invoicedate") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>                               
                                <asp:TemplateColumn HeaderText="INVOICE AMOUNT" ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoiceAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "inv_invoiceamount","{0:n}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="STATUS" ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoiceStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "inv_invoicestatus") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="BRANCH" ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranch" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "inv_branch") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr> 
                <%--<tr>
                    <td>
                        <uc2:ucPaging runat="server" id="pagingMonitoring" 
                            OnNavigationButtonClicked="NavigationButtonClicked" PageSize="10"  ></uc2:ucPaging>        
                    </td>
                </tr>    --%>      
                <tr>
                    <td class="tdganjil" width="50%" colspan="2"></td>
                    <td class="tdganjil" colspan="2" align="right">
                        <asp:ImageButton runat="server" ID="imbClose" ImageUrl="../Images/button/buttonexit.jpg" />
                    </td>
                </tr>     
            </table>
        </asp:Panel>
     </form>
</asp:Content>

