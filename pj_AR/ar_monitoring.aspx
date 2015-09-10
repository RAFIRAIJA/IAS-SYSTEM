<%@ Page Title="" Language="C#" MasterPageFile="~/pagesetting/MsPageBlank.master" AutoEventWireup="true" CodeFile="ar_monitoring.aspx.cs" Inherits="pj_ar_ar_monitoring" %>
<%@ Register src="~/usercontroller/ucPaging.ascx" tagname="ucPaging" tagprefix="uc2" %>
<%@ Register src="~/UserController/ValidDate.ascx" tagname="ValidDate" tagprefix="uc1" %>
<%@ Register src="~/UserController/ucInputNumber.ascx" tagname="ucInputNumber" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <link href="~/script/NewStyle.css" type="text/css" rel="stylesheet" />
    <script src="~/Include/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="~/Include/JavaScript/Eloan.js" type="text/javascript"></script>

    <form id="frmtypedoclampiran" runat="server">
        <table border="0" cellpadding="2" cellspacing="1" width="100%">
            <tr>
                <td>
                    <p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnTOOLBAR" runat="server" Width="100%">  
            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                <tr>
                    <td>
                        <b><asp:Label id="mlTITLE" runat="server"></asp:Label></b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="mlSYSCODE" runat="server" Visible="false"/>
                    </td>
                </tr>  
                <tr>
                    <td><asp:ImageButton id="btNewRecord" ToolTip="NewRecord" Visible="false" ImageUrl="~/images/toolbar/new.jpg" runat="server" />&nbsp;
                        <asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" Visible="false" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');"  />&nbsp;
                        <asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" OnClick="btSearchRecord_Click" />&nbsp;
                        <asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" Visible="false" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" OnClick="btCancelOperation_Click"  />    
                    </td>
                </tr>                      
            </table>
        <hr />
            <table id="Table1" runat="server" border="0" cellpadding="2" cellspacing="1" width="95%">
                <tr>
                    <td class="tdganjil" width="15%">Entity ID</td>
                    <td class="tdganjil" width="35%">
                        <asp:DropDownList runat="server" ID="ddlEntity">
                            <asp:ListItem Value="">Select One</asp:ListItem>
                            <asp:ListItem Value="ISS">ISS</asp:ListItem>
                            <asp:ListItem Value="IFS">IFS</asp:ListItem>
                            <asp:ListItem Value="IPM">IPM</asp:ListItem>
                        </asp:DropDownList>&nbsp;
                        <asp:RequiredFieldValidator ID="rfvEntity" runat="server" ErrorMessage="Select Entity...Please..." ControlToValidate="ddlEntity"></asp:RequiredFieldValidator>
                    </td>
                    <td class="tdganjil" colspan="2"></td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlSearch" runat="server" Width="100%" Visible="false" >
            <table runat="server" border="0" cellpadding="0" cellspacing="0" width="95%">
                <tr>
                    <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                    <td align="center" class="tdtopi">SEARCH BY</td>
                    <td width="10" class="tdtopikanan">&nbsp;</td>
                </tr>
            </table>
            <table runat="server" border="0" cellpadding="2" cellspacing="1" width="95%">
                <tr>
                    <td class="tdganjil" width="15%">Search By</td>
                    <td class="tdganjil" width="35%">
                        <asp:DropDownList runat="server" ID="ddlSearchBy" >
                            <asp:ListItem Value="">Select One</asp:ListItem>
                            <asp:ListItem Value="a.InvoiceNO">Invoice No</asp:ListItem>
                            <asp:ListItem Value="b.ReceiptCode">Receipt Code</asp:ListItem>
                            <asp:ListItem Value="a.CustName">Customer Name</asp:ListItem>   
                        </asp:DropDownList>&nbsp;
                        <asp:TextBox runat="server" ID="txtSearchBy" Width="150px" CssClass="inptype"></asp:TextBox>          
                    </td>                    
                    <td class="tdganjil" colspan="2"></td>                    
                </tr>
                <tr>
                    <td class="'tdganjil" width="13%">
                        <asp:CheckBox runat="server" ID="chkPeriode" AutoPostBack="true" OnCheckedChanged="ChkPeriodeCheckedChanged" />&nbsp;
                        Periode Invoice
                    </td>
                    <td class="tdganjil" colspan="3">
                        <uc1:ValidDate runat="server" ID="ucStartDate" />&nbsp;s/d&nbsp;
                        <uc1:ValidDate runat="server" ID="ucEndDate" />&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdganjil" colspan="2" align="center"></td>
                    <td class="tdganjil" colspan="2" align="right">
                        <asp:ImageButton ID="btnSearch" runat="server" 
                            ImageUrl="~/Images/button/buttonSearch.gif" OnClick="btnSearch_Click" />
                    </td>                     
                </tr>
            </table> 
        </asp:Panel>
        <asp:Panel ID="pnlListData" runat="server" Width="100%"  Visible ="true" >
                <table runat="server" border="0" cellpadding="0" cellspacing="0" width="95%">
                    <tr>
                        <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                        <td align="center" class="tdtopi">MONITORING INVOICE DELIVERY</td>
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
                                onitemdatabound="dgListData_ItemDataBound" 
                                OnItemCommand="dgListData_ItemCommand">
                                <SelectedItemStyle CssClass="tdgenap"></SelectedItemStyle>
                                <AlternatingItemStyle CssClass="tdgenap"></AlternatingItemStyle>
                                <ItemStyle CssClass="tdganjil"></ItemStyle>
                                <HeaderStyle CssClass="tdjudul" HorizontalAlign="Center" Height="30px"></HeaderStyle>
                                <Columns>                                                                                                                 
                                    <asp:TemplateColumn HeaderText="INVOICE NO" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNo") %>'></asp:Label>
                                        </ItemTemplate>                                        
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>                                                        
                                    <asp:TemplateColumn HeaderText="INVOICE DATE" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "InvDate") %>'></asp:Label>
                                        </ItemTemplate>                                        
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>                                                        
                                    <asp:TemplateColumn HeaderText="INVOICE AMOUNT" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoiceAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceAmount","{0:n}") %>'></asp:Label>
                                        </ItemTemplate>                                        
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="PIC CUSTOMER" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblpicCustName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PICCustName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>                                                        
                                    <asp:TemplateColumn HeaderText="CUSTOMER" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CustName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="SITECARD" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSiteCard" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SiteCard") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="RECEIPT CODE" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblReceiptCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ReceiptCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="PROCEEDS DATE" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblProceedDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProceedsDate") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="SENDING DATE" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSendingDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SendingDate") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="DELIVERED DATE" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveryDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryDate") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="RETURN DATE" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblReturnDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ReturnDate") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="DONE DATE" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDoneDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DoneDate") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="MESSANGER" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblMessangerName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MessangerName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="STATUS" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblReceiptStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="AGING DAYS" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblAgingDays" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AgingDays") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr> 
                    <tr>
                        <td>
                            <uc2:ucPaging runat="server" id="pagingMonitoring" 
                                OnNavigationButtonClicked="NavigationButtonClicked" PageSize="20"  ></uc2:ucPaging>        
                        </td>
                    </tr>               
                </table>                
                <br />
                <br />                
                <table runat="server" border="0" cellpadding="0" cellspacing="0" width="95%">
                    <tr>
                        <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                        <td align="center" class="tdtopi">SUMMARY INVOICE DELIVERY</td>
                        <td width="10" class="tdtopikanan">&nbsp;</td>
                    </tr>
                </table>
                <table runat="server" cellSpacing="0" cellPadding="0" width="95%" border="0">
                    <tr>
                        <td colspan="4">
                            <asp:DataGrid ID="dgSummary" runat="server" AutoGenerateColumns="False" 
                                AllowSorting="true" borderwidth="0px"
                                Width="100%" CssClass="tablegrid" 
                                CellPadding="3" CellSpacing="1"                             
                                onitemdatabound="dgSummary_ItemDataBound" >
                                <SelectedItemStyle CssClass="tdgenap"></SelectedItemStyle>
                                <AlternatingItemStyle CssClass="tdgenap"></AlternatingItemStyle>
                                <ItemStyle CssClass="tdganjil"></ItemStyle>
                                <HeaderStyle CssClass="tdjudul" HorizontalAlign="Center" Height="30px"></HeaderStyle>
                                <Columns>                                                                                                                 
                                    <asp:TemplateColumn HeaderText="ENTITY" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEntitiy" runat="server" Font-Size="25" Text='<%# DataBinder.Eval(Container.DataItem, "Entity") %>'></asp:Label>
                                        </ItemTemplate>                                        
                                        <ItemStyle HorizontalAlign="Center" Height="80" />
                                    </asp:TemplateColumn>                                                        
                                    <asp:TemplateColumn HeaderText="PROCEEDS" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblProceeds" runat="server" Font-Size="25" Text='<%# DataBinder.Eval(Container.DataItem, "Proceed") %>'></asp:Label>
                                        </ItemTemplate>                                        
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>                                                                                                                                                  
                                    <asp:TemplateColumn HeaderText="DELIVERY" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDelivered" runat="server" Font-Size="25" Text='<%# DataBinder.Eval(Container.DataItem, "Delivery") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="DONE" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDone" runat="server" Font-Size="25" Text='<%# DataBinder.Eval(Container.DataItem, "Done") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="CANCEL" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCancel" runat="server" Font-Size="25" Text='<%# DataBinder.Eval(Container.DataItem, "Cancel") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>                         
                </table>
            <br />
            <br />
                <table runat="server" border="0" cellpadding="0" cellspacing="0" width="95%">
                    <tr>
                        <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                        <td align="center" class="tdtopi">SUMMARY DETAIL INVOICE DELIVERY</td>
                        <td width="10" class="tdtopikanan">&nbsp;</td>
                    </tr>
                </table>
                <table runat="server" cellSpacing="0" cellPadding="0" width="95%" border="0">
                    <tr>
                        <td colspan="4">
                            <asp:DataGrid ID="dgSummaryDetail" runat="server" AutoGenerateColumns="False" 
                                AllowSorting="true" borderwidth="0px"
                                Width="100%" CssClass="tablegrid" 
                                CellPadding="3" CellSpacing="1"                             
                                onitemdatabound="dgSummaryDetail_ItemDataBound" >
                                <SelectedItemStyle CssClass="tdgenap"></SelectedItemStyle>
                                <AlternatingItemStyle CssClass="tdgenap"></AlternatingItemStyle>
                                <ItemStyle CssClass="tdganjil"></ItemStyle>
                                <HeaderStyle CssClass="tdjudul" HorizontalAlign="Center" Height="30px"></HeaderStyle>
                                <Columns>                                                                                                                 
                                    <asp:TemplateColumn HeaderText="ENTITY" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEntitiy" runat="server" Font-Size="12" Text='<%# DataBinder.Eval(Container.DataItem, "Entity") %>'></asp:Label>
                                        </ItemTemplate>                                        
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>                                                        
                                    <asp:TemplateColumn HeaderText="JML UPLOAD" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lbljmlupload" runat="server" Font-Size="12" Text='<%# DataBinder.Eval(Container.DataItem, "jumlahupload") %>'></asp:Label>
                                        </ItemTemplate>                                        
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>                                                                                                                                                  
                                    <asp:TemplateColumn HeaderText="JML TERKIRIM" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lbljmlterkirim" runat="server" Font-Size="12" Text='<%# DataBinder.Eval(Container.DataItem, "jumlahterkirim") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="JML BLM TERKIRIM" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblblmterkirim" runat="server" Font-Size="12" Text='<%# DataBinder.Eval(Container.DataItem, "jumlahbelumterkirim") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="JML CANCEL" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblcancel" runat="server" Font-Size="12" Text='<%# DataBinder.Eval(Container.DataItem, "jumlahcancel") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="JML BLM KEMBALI" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblblmkembali" runat="server" Font-Size="12" Text='<%# DataBinder.Eval(Container.DataItem, "jumlahbelumkembali") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="MESSANGER" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblMessanger" runat="server" Font-Size="12" Text='<%# DataBinder.Eval(Container.DataItem, "messanger") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="CRO" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblcro" runat="server" Font-Size="12" Text='<%# DataBinder.Eval(Container.DataItem, "cro") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="OPERATION" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lbloperation" runat="server" Font-Size="12" Text='<%# DataBinder.Eval(Container.DataItem, "operation") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="CITOX" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblcitox" runat="server" Font-Size="12" Text='<%# DataBinder.Eval(Container.DataItem, "citox") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>                         
                </table>
        </asp:Panel> 
    </form>
</asp:Content>

