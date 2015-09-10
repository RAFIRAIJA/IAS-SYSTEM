<%@ Page Title="" Language="C#" MasterPageFile="~/pagesetting/MsPageBlank.master" AutoEventWireup="true" CodeFile="ar_invoicedelivery.aspx.cs" Inherits="pj_ar_ar_invoicedelivery" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register src="../usercontroller/ucPaging.ascx" tagname="ucPaging" tagprefix="uc2" %>
<%@ Register src="../UserController/ValidDate.ascx" tagname="ValidDate" tagprefix="uc1" %>
<%@ Register src="../UserController/ucInputNumber.ascx" tagname="ucInputNumber" tagprefix="uc4" %>
<%@ Register src="../UserController/ucLookUp_INVCustomer.ascx" tagname="ucLookUpCustomer" tagprefix="uc5" %>
<%@ Register src="../UserController/ucLookUp_INVMessanger.ascx" tagname="ucLookUpMessanger" tagprefix="uc6" %>


<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <link href="../script/NewStyle.css" type="text/css" rel="stylesheet" />
    <script src="../Include/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="../Include/JavaScript/Eloan.js" type="text/javascript"></script>

     <script type="text/javascript">
         function OpenWinLookUp(pReceiptCode) {
             var AppInfo = '<%= Request.ServerVariables["PATH_INFO"]%>';            
        var App = AppInfo.substr(1, AppInfo.indexOf('/', 1) - 1)
        window.open('http://<%=Request.ServerVariables["SERVER_NAME"]%>:<%=Request.ServerVariables["SERVER_PORT"]%>/' + App + '/pj_ar/ar_invoicedelivery_lookup.aspx?ReceiptCode=' + pReceiptCode, 'UserLookup', 'left=50, top=10, width=1000, height=600, menubar=0, scrollbars=yes');
         }

         function OpenWinLookUpMessanger(pMessangerIDHid, pMessangerID, pMessangerName, pMessangerNameHid, pStyle) {
             var AppInfo = '<%= Request.ServerVariables["PATH_INFO"]%>';
             var App = AppInfo.substr(1, AppInfo.indexOf('/', 1) - 1)
             window.open('http://<%=Request.ServerVariables["SERVER_NAME"]%>:<%=Request.ServerVariables["SERVER_PORT"]%>/' + App + '/UserController/form/LookUp_INVMessanger.aspx?style=' + pStyle + '&hdnMessangerID=' + pMessangerIDHid + '&txtMessangerID=' + pMessangerID + '&hdnMessangerName=' + pMessangerNameHid + '&txtMessangerName=' + pMessangerName, 'UserLookup', 'left=50, top=10, width=900, height=600, menubar=0, scrollbars=yes');
         }
    </script>

    <form id="frmInvoicedelivery" runat="server">
        <input type="hidden" id="hdnMessangerID" runat="server" name="hdnMessangerID" class="inptype"/>
		<input type="hidden" id="hdnMessangerName" runat="server" name="hdnMessangerName" class="inptype"/>

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
                    <td><asp:ImageButton id="btNewRecord" ToolTip="NewRecord" ImageUrl="~/images/toolbar/new.jpg" runat="server" OnClick="btNewRecord_Click" />&nbsp;
                        <asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" Visible="false" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');" OnClick="btSaveRecord_Click" style="height: 20px" />&nbsp;
                        <asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" OnClick="btSearchRecord_Click" />&nbsp;
                        <asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" OnClick="btCancelOperation_Click" />&nbsp;    
                        <asp:ImageButton id="btPrintRecord" ToolTip="PrintRecord" ImageUrl="~/images/toolbar/print.jpg" runat="server" Visible="false" OnClick="btPrintRecord_Click" />    

                    </td>
                </tr>                      
            </table>
        <hr />
        <table runat="server" border="0" cellpadding="2" cellspacing="1" width="95%">
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
                        <asp:DropDownList runat="server" ID="ddlSearchBy">
                            <asp:ListItem Value="">Select One</asp:ListItem>
                            <asp:ListItem Value="a.inv_receiptcode">Receipt Code</asp:ListItem>
                            <asp:ListItem Value="a.CustName">Customer Name</asp:ListItem>
                        </asp:DropDownList>&nbsp;
                        <asp:TextBox runat="server" ID="txtSearchBy" Width="150px" CssClass="inptype"></asp:TextBox>          
                    </td>                    
                    <td class="tdganjil" colspan="2"></td>                    
                </tr>
                <tr>
                    <td class="tdganjil" colspan="2" align="center"></td>
                    <td class="tdganjil" colspan="2" align="right">
                        <asp:ImageButton ID="btnSearch" runat="server" 
                            ImageUrl="../Images/button/buttonSearch.gif" OnClick="btnSearch_Click" />&nbsp;                      
                    </td>                     
                </tr>
            </table> 
        </asp:Panel>
        <asp:Panel ID="pnlListReceiptCode" runat="server" Width="100%" >
            <table runat="server" border="0" cellpadding="0" cellspacing="0" width="95%">
                <tr>
                    <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                    <td align="center" class="tdtopi">LIST RECEIPT CODE</td>
                    <td width="10" class="tdtopikanan">&nbsp;</td>
                </tr>
            </table>
            <table runat="server" cellSpacing="0" cellPadding="0" width="95%" border="0">
                <tr>
                    <td>
                        <asp:DataGrid ID="dgListData" runat="server" AutoGenerateColumns="False" 
                            AllowSorting="true" borderwidth="0px"
                            Width="100%" CssClass="tablegrid" 
                            CellPadding="3" CellSpacing="1"                             
                            onitemdatabound="dgListData_ItemDataBound" 
                            OnItemCommand="dgListData_ItemCommand"
                            >
                            <SelectedItemStyle CssClass="tdgenap"></SelectedItemStyle>
                            <AlternatingItemStyle CssClass="tdgenap"></AlternatingItemStyle>
                            <ItemStyle CssClass="tdganjil"></ItemStyle>
                            <HeaderStyle CssClass="tdjudul" HorizontalAlign="Center" Height="30px"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="PRINT LAMP." ItemStyle-HorizontalAlign="center" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imbPrintLampiran" runat="server" ImageUrl="~/images/toolbar/iconprinter.jpg" ToolTip="Print Lampiran" CommandName="printlampiran"/>
                                        <asp:Label id="lblflagLampiran" Visible="false" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"flagLampiran") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="PRINT" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imbPrint" runat="server" ImageUrl="~/images/toolbar/iconprinter.jpg" ToolTip="Print Record" CommandName="print"/>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="EDIT" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imbEdit" runat="server" ImageUrl="~/images/toolbar/edit.jpg" ToolTip="Edit Record" CommandName="edit"/>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CONFIRM" ItemStyle-HorizontalAlign="center"  HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imbConfirm" runat="server" ImageUrl="~/images/button/iconapproval.gif" ToolTip="Confirm Record" CommandName="confirm" />                                      
                                        <asp:Label id="lblflagconfirm" Visible="false" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"flagconfirm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                
                                <asp:TemplateColumn HeaderText="RECEIPT CODE" ItemStyle-HorizontalAlign="Left" SortExpression ="a.inv_receiptcode"  HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlreceiptcode" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"receiptcode") %>'></asp:HyperLink>                                       
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="PROCEEDS DATE" ItemStyle-HorizontalAlign="Center" SortExpression="a.inv_proceedsdate" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblproceedsdate" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"proceedsdate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="SENDING DATE" ItemStyle-HorizontalAlign="Center" SortExpression="a.inv_sendingdate" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblsendingdate" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"sendingdate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DELIVERED DATE" ItemStyle-HorizontalAlign="Center" SortExpression="a.inv_delivereddate" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lbldelivereddate" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"delivereddate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="RETURN DATE" ItemStyle-HorizontalAlign="Center" SortExpression="a.inv_returneddate" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblreturndate" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"returneddate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DONE DATE" ItemStyle-HorizontalAlign="Center" SortExpression="a.inv_donedate" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lbldonedate" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"donedate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="MESSANGER" ItemStyle-HorizontalAlign="Center" SortExpression="a.inv_messangerid"  HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmessangerid" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "messangerid") %>'></asp:Label>
                                        <asp:Label ID="lblmessangername" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "messangername") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                
                                <asp:TemplateColumn HeaderText="CUST CODE" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcustcode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "custcode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn> 
                                <asp:TemplateColumn HeaderText="PIC CUSTOMER" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcustpenerima" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "custpenerima") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="RECEIPT STATUS" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblreceiptstatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "receiptstatus") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn> 
                                <asp:TemplateColumn HeaderText="TOTAL AMOUNT" ItemStyle-HorizontalAlign="Right"  HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalInvoice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TotalInvoice","{0:n}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                 
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>                
                <tr>
                    <td>
                        <uc2:ucPaging runat="server" id="pagingReceiptCode" 
                            OnNavigationButtonClicked="NavigationButtonClicked" PageSize="20"  ></uc2:ucPaging>        
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlInputData" runat="server" Width="100%" Visible="false" >
            <table runat="server" border="0" cellpadding="0" cellspacing="0" width="95%">
                <tr>
                    <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                    <td align="center" class="tdtopi">CREATE RECEIPT CODE</td>
                    <td width="10" class="tdtopikanan">&nbsp;</td>
                </tr>
            </table>
            <table runat="server" border="0" cellpadding="2" cellspacing="1" width="95%">
                <tr>
                    <td class="tdganjil" width="13%">
                        Receipt Code
                    </td>
                    <td class="tdganjil" width="37%">
                        <asp:Label runat="server" ID="lblReceiptCode" ForeColor="Blue" Font-Bold="true"></asp:Label>
                    </td>
                    <td class="tdganjil" width="13%">
                        Proceeds Date
                    </td>
                    <td class="tdganjil" >
                        <uc1:ValidDate runat="server" ID="ucProceedsDate" />
                    </td>
                </tr>
                <tr>
                    <td class="tdganjil" >
                        Messanger
                    </td>
                    <td class="tdganjil" >
			            <asp:textbox id="txtMessangerID" cssclass="inptype" runat="server"  Width="50px"></asp:textbox>&nbsp;
			            <asp:textbox id="txtMessangerName" cssclass="inptype" runat="server"  Width="150px"></asp:textbox>&nbsp;
			            <asp:hyperlink id="hpLookup" runat="server" imageurl="~/images/toolbar/find.jpg"></asp:hyperlink>
                        <asp:RequiredFieldValidator id="rfvMessanger" runat="server" ControlToValidate="txtMessangerID" ErrorMessage="Please...Choose Messanger"></asp:RequiredFieldValidator>

                    </td>
                    <td class="tdganjil" >
                        No.Resi
                    </td>
                    <td class="tdganjil" >
                        <asp:TextBox runat="server" ID="txtNoResi" CssClass="inptype" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdganjil" >
                        PIC Customer
                    </td>
                    <td class="tdganjil" >
                        <asp:TextBox runat="server" ID="txtPIC" CssClass="inptype" Width="150px"></asp:TextBox>
                    </td>
                    <td class="tdganjil" >
                        Invoice Status
                    </td>
                    <td class="tdganjil" >
                        <asp:Label runat="server" ID="lblStatus"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdganjil" >
                        Customer
                    </td>
                    <td class="tdganjil" >                        
                        <uc5:ucLookUpCustomer ID="ucCustomer" runat ="server" />&nbsp;
                        <asp:ImageButton ID="imbRefreshINV" runat="server" ImageUrl="~/Images/toolbar/autocomplete.jpg" CausesValidation="false" OnClick="imbRefreshINV_Click" />
                    </td>
                    <td class="tdganjil">
                        Is Document Lampiran
                    </td>
                    <td class="tdganjil">
                        <asp:CheckBox runat="server" ID="chkDocLampiran" />
                    </td>
                </tr>
                <tr>
                    <td class="tdganjil" >
                        Sending Date
                    </td>
                    <td class="tdganjil" >
                        <uc1:ValidDate runat="server" ID="ucSendingDate" isEnabled="false"/>
                    </td>
                    <td class="tdganjil" >
                        Delivered Date
                    </td>
                    <td class="tdganjil" >
                        <uc1:ValidDate runat="server" ID="ucDeliveredDate" isEnabled="false"/>
                    </td>
                </tr>
                <tr>
                    <td class="tdganjil" >
                        Return Date
                    </td>
                    <td class="tdganjil" >
                        <uc1:ValidDate runat="server" ID="ucReturnDate" isEnabled="false"/>
                    </td>
                    <td class="tdganjil" >
                        Done Date
                    </td>
                    <td class="tdganjil" >
                        <uc1:ValidDate runat="server" ID="ucDoneDate" isEnabled="false"/>
                    </td>
                </tr>
            </table>
            <br />            
            <asp:Panel ID="pnlInvoice" runat="server" Width="100%" Visible="false" >
                <table runat="server" border="0" cellpadding="0" cellspacing="0" width="95%">
                    <tr>
                        <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                        <td align="center" class="tdtopi">LIST INVOICE</td>
                        <td width="10" class="tdtopikanan">&nbsp;</td>
                    </tr>
                    </table>
                <table runat="server" cellSpacing="0" cellPadding="0" width="95%" border="0">
                    <tr>
                        <td colspan="4">
                            <asp:DataGrid ID="dgInvoice" runat="server" AutoGenerateColumns="False" 
                                AllowSorting="true" borderwidth="0px"
                                Width="100%" CssClass="tablegrid" 
                                CellPadding="3" CellSpacing="1"                             
                                onitemdatabound="dgInvoice_ItemDataBound" 
                                >
                                <SelectedItemStyle CssClass="tdgenap"></SelectedItemStyle>
                                <AlternatingItemStyle CssClass="tdgenap"></AlternatingItemStyle>
                                <ItemStyle CssClass="tdganjil"></ItemStyle>
                                <HeaderStyle CssClass="tdjudul" HorizontalAlign="Center" Height="30px"></HeaderStyle>
                                <Columns>          
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" ID="chkSelectAll" OnCheckedChanged="chkSelectAll_CheckedChanged" AutoPostBack="true" />
                                            CHECK ALL
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkSelect" />
                                            <asp:Label ID="lblFlagCheck" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FlagCheck") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>                                                     
                                    <asp:TemplateColumn HeaderText="INVOICE NO" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinvoiceno" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "invoiceno") %>'></asp:Label>
                                            <asp:Label ID="lblinvoicestatus" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "invoicestatus") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="INVOICE DATE" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinvdate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "invdate") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>        
                                    <asp:TemplateColumn HeaderText="BRANCH" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbranch" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "branch") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>                                                         
                                    <asp:TemplateColumn HeaderText="SITE CARD" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsitecard" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "sitecard") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>                                                         
                                    <asp:TemplateColumn HeaderText="CUST CODE" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcustcode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "custcode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn> 
                                    <%--<asp:TemplateColumn HeaderText="CUSTOMER NAME" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcustname" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "custname") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>--%>
                                    <asp:TemplateColumn HeaderText="TOTAL AMOUNT" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalInvoice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceAmount","{0:n}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn> 
                                    <asp:TemplateColumn HeaderText="DESCRIPTION" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDescription" Width="150px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "inv_canceldesc") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>                                 
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>                
                </table>
            </asp:Panel>                        
        </asp:Panel>
        <asp:Panel ID="pnlLampiran" runat="server" Width="100%" Visible="false" >
            <table runat="server" border="0" cellpadding="0" cellspacing="0" width="95%">
                <tr>
                    <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                    <td align="center" class="tdtopi">LAMPIRAN RECEIPT CODE</td>
                    <td width="10" class="tdtopikanan">&nbsp;</td>
                </tr>
            </table>
            <table runat="server" cellSpacing="0" cellPadding="0" width="95%" border="0">
                <tr>
                    <td colspan="4">
                        <asp:DataGrid ID="dgLampiran" runat="server" AutoGenerateColumns="False" 
                            AllowSorting="true" borderwidth="0px"
                            Width="100%" CssClass="tablegrid" 
                            CellPadding="3" CellSpacing="1"                             
                            onitemdatabound="dgLampiran_ItemDataBound">
                            <SelectedItemStyle CssClass="tdgenap"></SelectedItemStyle>
                            <AlternatingItemStyle CssClass="tdgenap"></AlternatingItemStyle>
                            <ItemStyle CssClass="tdganjil"></ItemStyle>
                            <HeaderStyle CssClass="tdjudul" HorizontalAlign="Center" Height="30px"></HeaderStyle>
                            <Columns>          
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="10%">
                                    <HeaderTemplate>
                                        <asp:CheckBox runat="server" ID="chkSelectAll" OnCheckedChanged="chkSelectAllLampiran_CheckedChanged" AutoPostBack="true" />
                                        CHECK ALL
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkSelect" />
                                        <asp:Label ID="lblFlagCheck" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FlagCheck") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>                                                     
                                <asp:TemplateColumn HeaderText="DOCUMENT TYPE" ItemStyle-HorizontalAlign="Center"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lbldoctypeid" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "inv_typedoc_id") %>'></asp:Label>
                                        <asp:Label ID="lbldoctypename" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "inv_typedoc_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateColumn>
                                                               
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>                
            </table>
            
        </asp:Panel>
        <asp:Panel ID="pnlPrintPreview" runat="server" Width="100%" Visible="false" >
            <div>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:ImageButton ID="imbBack" runat="server" ImageUrl="../Images/button/ButtonBack.gif" OnClick="imbBack_Click"/>
                <rsweb:ReportViewer  Width="100%" ProcessingMode="Local" SizeToReportContent="true"
                ZoomMode="FullPage" Height="100%" ID="rptViewer" AsyncRendering="false" runat="server"></rsweb:ReportViewer>
            </div>
        </asp:Panel>
        
    </form> 
</asp:Content>

