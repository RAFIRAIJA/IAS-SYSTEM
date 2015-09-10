<%@ Page Title="" Language="C#" MasterPageFile="~/pj_delinv/MasterInternCS.master" AutoEventWireup="true" CodeFile="ReceiptData.aspx.cs" Inherits="pj_delinv_ReceiptData" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <link href="../script/calendar.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="../script/calendar.js"></script>
    <link rel="stylesheet" href="script/style-page.css" type="text/css" media="all" />

    <script type="text/javascript" language="Javascript">
        //<!-- hide script from older browsers
        function openwindow(url, nama, width, height) {
            OpenWin = this.open(url, nama);
            if (OpenWin != null) {
                if (OpenWin.opener == null)
                    OpenWin.opener = self;
            }
            OpenWin.focus();
        }
        // End hiding script-->

        function PostBackOnMainPage(){
          <%=GetPostBackScript()%>
        }

        function pop_up(url, nama, wi, he) {
            var left = (screen.width / 2) - (wi / 2);
            var top = (screen.height / 2) - (he / 2);
            var newwin;
            newwin = window.open(url, nama, 'width=' + wi + ', height=' + he + ', top=' + top + ', left=' + left + ', scrollbars=yes, resizable=no');
            if (window.focus) 
            { newwin.focus() }
            return false;
        }
    </script>

    <form id="mpFORM" runat="server">
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ToolkitScriptManager1" />
        
        <asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Panel ID="pnTOOLBAR" runat="server">  
                        <table border="0" cellpadding="3" cellspacing="3">
                            <tr>
                                <td valign="top"><asp:ImageButton id="btNewRecord" ToolTip="NewRecord" ImageUrl="~/images/toolbar/new.jpg" runat="server" /></td>
                                <td valign="top"><asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');" /></td>
                                <td valign="top"><asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" /></td>
                                <td valign="top"><asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" /></td>
                                <%--<input type=button onclick="openPopup('Inventory.aspx')" value="Open the popup" />OnClick="btnPostback_Click"--%>
                                <asp:Button ID="btnPostback" runat="server" Visible="false" />
                            </tr>               
                        </table>
                        <hr />
                    </asp:Panel>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow><asp:TableCell><p class="header1"><b><asp:Label id="mlTITLE" runat="server"></asp:Label></b></p></asp:TableCell></asp:TableRow>
            <asp:TableRow><asp:TableCell><p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p></asp:TableCell></asp:TableRow>
            <asp:TableRow><asp:TableCell><asp:HiddenField ID="mlSYSCODE" runat="server"/></asp:TableCell></asp:TableRow>
            <asp:TableRow><asp:TableCell><p><asp:HyperLink ID="mlLINKDOC" runat="server"></asp:HyperLink></p></asp:TableCell></asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <div id="headline-receiptdata"></div>
                    <div id="space"></div>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <div id="space"></div>
                    <div id="line-blue"></div>
                    <div id="space"></div>
                    <div id="lblTitle1" style="font-size:16px;"><strong>RECEIPT DATA</strong></div>
                    <div id="space"></div>

                    <asp:Panel ID="mlPNLGRID" runat="server" ScrollBars="Auto" Width="1020px">
                        <table>
                            <tr>
                                <td align="center">
                                    <asp:CheckBox ID="mlCHECKALL" runat="server" AutoPostBack="true" OnCheckedChanged="mlCHECKALL_CheckedChanged" />
                                </td>
                                <td><span class="add" style="padding-left: 20px;"><asp:Button ID="mlBTNPREVIEW" runat="server" Text="Preview" Height="25px" Width="125px" OnClick="mlBTNPREVIEW_Click"></asp:Button></span></td>
                                <td><span class="add" style="padding-left: 20px;"><asp:Button ID="mlBTNCONFIRMDELIVERED" runat="server" Text="Confirm Delivered" Height="25px" Width="125px" OnClick="mlBTNCONFIRMDELIVERED_Click" OnClientClick="return confirm('Confirm Delivered ?');"></asp:Button></span></td>
                                <td><span class="delete" style="padding-left: 20px;"><asp:Button ID="mlBTNDELETE" runat="server" Text="Delete" Height="25px" Width="125px" OnClick="mlBTNDELETE_Click" OnClientClick="return confirm('Confirm Delete ?');"></asp:Button></span></td>
                            </tr>
                        </table>
                        <asp:DataGrid runat="server" ID="mlDGRECEIPT" 
                        CssClass="Grid"
                        autogeneratecolumns="false"
                        AllowPaging="True"
                        AllowSorting="True"
                        PagerStyle-Mode="NumericPages" PagerStyle-HorizontalAlign="Left" PagerStyle-PageButtonCount="20" PagerStyle-BorderStyle="Solid"
                        OnPageIndexChanged="mlDGRECEIPT_PageIndexChanged">

                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                        <ItemStyle CssClass="GridItem"></ItemStyle>
                        <EditItemStyle  CssClass="GridItem" />
                        <PagerStyle  CssClass="GridItem" />
                        <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
                        <Columns>
    
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                <asp:CheckBox id="mlCHECKBOX" runat="server"/>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:BoundColumn HeaderText="Company" DataField="CompanyCode" SortExpression = "CompanyCode" HeaderStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Receipt Code" DataField="InvReceiptCode" SortExpression = "InvReceiptCode" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Status" DataField="InvStatus" SortExpression = "InvStatus" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Description" DataField="InvDesc" SortExpression = "InvDesc" Visible="false" HeaderStyle-Wrap="False" ItemStyle-Wrap="True"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Prepared For" DataField="InvPreparedForDate" SortExpression = "InvPreparedForDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Delivered" DataField="InvDeliveredDate" SortExpression = "InvDeliveredDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Resi TIKI" DataField="InvResiTiki" SortExpression = "InvResiTiki" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Messenger NIK" DataField="InvCodeMess" SortExpression = "InvCodeMess" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Messenger Name" DataField="InvMessName" SortExpression = "InvMessName" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="User NIK" DataField="UserID" SortExpression = "UserID" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="User Name" DataField="UserName" SortExpression = "UserName" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>                            
                            <asp:BoundColumn HeaderText="Invoice No" DataField="InvNo" SortExpression = "InvNo" Visible="false" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Customer Code" DataField="InvCustCode" SortExpression = "InvCustCode" Visible="false" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Customer Name" DataField="InvCustName" SortExpression = "InvCustName" Visible="false" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Invoice Date" DataField="InvDate" SortExpression = "InvDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" Visible="false" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Branch" DataField="InvBranch" SortExpression = "InvBranch" Visible="false" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Amount" DataField="InvAmount" SortExpression = "InvAmount" DataFormatString ="{0:#,##0}" ItemStyle-HorizontalAlign="right" Visible="false" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Proceeds" DataField="InvProceedsDate" SortExpression = "InvProceedsDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" Visible="false" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Returned" DataField="InvReturnedDate" SortExpression = "InvReturnedDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" Visible="false" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Done" DataField="InvDoneDate" SortExpression = "InvDoneDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" Visible="false" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Penerima" DataField="InvCustPenerima" SortExpression = "InvCustPenerima" Visible="false" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Site Card" DataField="InvSiteCard" SortExpression = "InvSiteCard" Visible="false" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Product Offering" DataField="InvProdOffer" SortExpression = "InvProdOffer" Visible="false" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="OCM" DataField="InvOCM" SortExpression = "InvOCM" Visible="false" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="FCM" DataField="InvFCM" SortExpression = "InvFCM" Visible="false" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Collector" DataField="InvCollector" SortExpression = "InvCollector" Visible="false" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Created Date" DataField="InvCreatedDate" SortExpression = "InvCreatedDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" Visible="false" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Created By" DataField="InvCreatedBy" SortExpression = "InvCreatedBy" Visible="false" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Modified Date" DataField="InvModifiedDate" SortExpression = "InvModifiedDate" DataFormatString ="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="right" Visible="false" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Modified By" DataField="InvModifiedBy" SortExpression = "InvModifiedBy" Visible="false" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Receipt Flag" DataField="InvReceiptFlag" SortExpression = "InvReceiptFlag" Visible="false" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>

                        </Columns>
                        </asp:DataGrid>

                        <asp:Panel ID ="pnlSEARCH" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <table id="mlTABLESEARCH">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="mlLBLSEARCH" runat="server" Text="Find"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="mlTXTSEARCH" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btSearch" runat="server" ImageUrl="~/images/toolbar/find.jpg" OnClick="btSearch_Click" />
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="mlDDLSEARCH" runat="server">
                                                        <asp:ListItem Text="Company" Value="CompanyCode"></asp:ListItem>
                                                        <asp:ListItem Text="Invoice No" Value="InvNo"></asp:ListItem>
                                                        <asp:ListItem Text="Customer Code" Value="InvCustCode"></asp:ListItem>
                                                        <asp:ListItem Text="Customer Name" Value="InvCustName"></asp:ListItem>
                                                        <asp:ListItem Text="Invoice Date" Value="InvDate"></asp:ListItem>
                                                        <asp:ListItem Text="Branch" Value="InvBranch"></asp:ListItem>
                                                        <asp:ListItem Text="Amount" Value="InvAmount"></asp:ListItem>
                                                        <asp:ListItem Text="Status" Value="InvStatus"></asp:ListItem>
                                                        <asp:ListItem Text="Description" Value="InvDesc"></asp:ListItem>
                                                        <asp:ListItem Text="Receipt Code" Value="InvReceiptCode"></asp:ListItem>
                                                        <asp:ListItem Text="Proceeds" Value="InvProceedsDate"></asp:ListItem>
                                                        <asp:ListItem Text="Delivered" Value="InvDeliveredDate"></asp:ListItem>
                                                        <asp:ListItem Text="Returned" Value="InvReturnedDate"></asp:ListItem>
                                                        <asp:ListItem Text="Done" Value="InvDoneDate"></asp:ListItem>
                                                        <asp:ListItem Text="Penerima" Value="InvCustPenerima"></asp:ListItem>
                                                        <asp:ListItem Text="Messenger NIK" Value="InvCodeMess"></asp:ListItem>
                                                        <asp:ListItem Text="Messenger Name" Value="InvMessName"></asp:ListItem>
                                                        <asp:ListItem Text="User NIK" Value="UserID"></asp:ListItem>
                                                        <asp:ListItem Text="User Name" Value="UserName"></asp:ListItem>
                                                        <asp:ListItem Text="Created Date" Value="InvCreatedDate"></asp:ListItem>
                                                        <asp:ListItem Text="Created By" Value="InvCreatedBy"></asp:ListItem>
                                                        <asp:ListItem Text="Modified Date" Value="InvModifiedDate"></asp:ListItem>
                                                        <asp:ListItem Text="Modified By" Value="InvModifiedBy"></asp:ListItem>
                                                        <asp:ListItem Text="Site Card" Value="InvSiteCard"></asp:ListItem>
                                                        <asp:ListItem Text="Resi TIKI" Value="InvResiTiki"></asp:ListItem>
                                                        <asp:ListItem Text="Prepared For" Value="InvPreparedForDate"></asp:ListItem>
                                                        <asp:ListItem Text="Product Offering" Value="InvProdOffer"></asp:ListItem>
                                                        <asp:ListItem Text="OCM" Value="InvOCM"></asp:ListItem>
                                                        <asp:ListItem Text="FCM" Value="InvFCM"></asp:ListItem>
                                                        <asp:ListItem Text="Collector" Value="InvCollector"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btClear" runat="server" Text="Clear" OnClick="btClear_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="mlDDLPAGESIZE" runat="server" AutoPostBack="true" OnSelectedIndexChanged ="mlDDLPAGESIZE_SelectedIndexChanged">
                                                        <asp:ListItem Text="10" Value="10" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                        <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                        <asp:ListItem Text="60" Value="60"></asp:ListItem>
                                                        <asp:ListItem Text="70" Value="70"></asp:ListItem>
                                                        <asp:ListItem Text="80" Value="80"></asp:ListItem>
                                                        <asp:ListItem Text="90" Value="90"></asp:ListItem>
                                                        <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                    </asp:Panel>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <div id="space"></div>
                    <div id="line-blue"></div>
                    <div id="space"></div>
                </asp:TableCell>
            </asp:TableRow>

        </asp:Table>
    </form>

</asp:Content>

