<%@ Page Title="" Language="C#" MasterPageFile="~/pagesetting/MsPageBlank.master" AutoEventWireup="true" CodeFile="ar_monitoringmessanger.aspx.cs" Inherits="pj_ar_ar_monitoringmessanger" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register src="~/usercontroller/ucPaging.ascx" tagname="ucPaging" tagprefix="uc2" %>
<%@ Register src="~/UserController/ValidDate.ascx" tagname="ValidDate" tagprefix="uc1" %>
<%@ Register src="~/UserController/ucInputNumber.ascx" tagname="ucInputNumber" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <link href="~/script/NewStyle.css" type="text/css" rel="stylesheet" />
    <script src="~/Include/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="~/Include/JavaScript/Eloan.js" type="text/javascript"></script>
    
    <script type="text/javascript">
    function OpenWinLookUp(pReceiptCode){
			var AppInfo = '<%= Request.ServerVariables["PATH_INFO"]%>';
			var App = AppInfo.substr(1, AppInfo.indexOf('/', 1) - 1)
			window.open('http://<%=Request.ServerVariables["SERVER_NAME"]%>:<%=Request.ServerVariables["SERVER_PORT"]%>/' + App + '/pj_ar/ar_invoicedelivery_lookup.aspx?ReceiptCode='+ pReceiptCode, 'UserLookup', 'left=50, top=10, width=1000, height=600, menubar=0, scrollbars=yes');
			}
    </script>

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
                        <asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" Visible="false" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');" style="height: 20px" />&nbsp;
                        <asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" OnClick="btSearchRecord_Click" />&nbsp;
                        <asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" Visible="false" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" OnClick="btCancelOperation_Click" />&nbsp;   
                        <asp:ImageButton id="btPrintRecord" ToolTip="PrintRecord" Visible="false" ImageUrl="~/images/toolbar/print.jpg" runat="server" OnClick="btPrintRecord_Click" />                           
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
                        <asp:DropDownList runat="server" ID="ddlSearchBy" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged" >
                            <asp:ListItem Value="">Select One</asp:ListItem>
                            <asp:ListItem Value="a.MessangerName">Messanger Name</asp:ListItem>
                            <asp:ListItem Value="a.ReceiptCode">Receipt Code</asp:ListItem>
                        </asp:DropDownList>&nbsp;
                        <asp:TextBox runat="server" ID="txtSearchBy" Width="150px" CssClass="inptype"></asp:TextBox>          
                    </td>                    
                    <td class="tdganjil" colspan="2"></td>                    
                </tr>
                <tr id="tr_PERIODE" runat="server" visible="false">
                    <td class="'tdganjil" width="15%">
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
                            ImageUrl="~/Images/button/buttonSearch.gif" OnClick="btnSearch_Click" />&nbsp;
                    </td>                     
                </tr>
            </table> 
        </asp:Panel>
        <asp:Panel ID="pnlListData" runat="server" Width="100%"  Visible ="true" >
            <table runat="server" border="0" cellpadding="0" cellspacing="0" width="95%">
                <tr>
                    <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                    <td align="center" class="tdtopi">MONITORING RECEIPT CODE BY MESSANGER</td>
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
                                <asp:TemplateColumn HeaderText="RECEIPT CODE" ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblReceiptCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ReceiptCode") %>'></asp:Label>--%>
                                        <asp:HyperLink ID="hlReceiptCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ReceiptCode") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>                                                                                                                                                                                                  
                                <asp:TemplateColumn HeaderText="CUSTOMER" ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CustName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                               <%-- <asp:TemplateColumn HeaderText="SITECARD" ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblSiteCard" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SiteCard") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>         --%>                       
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
                                <asp:TemplateColumn HeaderText="DELIVERY DATE" ItemStyle-HorizontalAlign="Center" >
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
                                        <asp:Label ID="lblReceiptStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ReceiptStatus") %>'></asp:Label>
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
        </asp:Panel>
        <asp:Panel ID="pnlPrintPreview" runat="server" Width="100%"  Visible ="false" >
            <div>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:ImageButton ID="imbBack" runat="server" ImageUrl="~/Images/button/ButtonBack.gif" OnClick="imbBack_Click"/>               

                <rsweb:ReportViewer Width="100%" ProcessingMode="Local" SizeToReportContent="true"
                ZoomMode="FullPage" Height="100%" ID="rptViewer" AsyncRendering="false" runat="server"></rsweb:ReportViewer>
            </div>
        </asp:Panel>
    </form>
</asp:Content>

