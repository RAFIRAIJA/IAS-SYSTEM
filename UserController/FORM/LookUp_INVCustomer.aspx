<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LookUp_INVCustomer.aspx.cs" Inherits="usercontroller_form_LookUp_INVCustomer" %>
<%@ Register src="~/usercontroller/ucPaging.ascx" tagname="ucPaging" tagprefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LookUp Customer</title>
    <link href="~/script/NewStyle.css" type="text/css" rel="stylesheet" />
    <script src="~/Include/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="~/Include/JavaScript/Eloan.js" type="text/javascript"></script>
    <script type="text/javascript">
        function WinClose() {
            Window.close();
        }
    </script>
    <script language="javascript">
        function sendRequest(pCustomerID, pCustomerName) {
            with(document.forms){
                var lObjName = '<%= Request.QueryString["txtCustomerID"]%>';
                if(eval('opener.document.forms[0].' + lObjName))
                {
                    eval('opener.document.forms[0].' + lObjName).value = pCustomerID;
                }
                var lObjName = '<%= Request.QueryString["txtCustomerName"]%>';
                if(eval('opener.document.forms[0].' + lObjName))
                {
                    eval('opener.document.forms[0].' + lObjName).value = pCustomerName;
                }
                var lObjName = '<%= Request.QueryString["hdnCustomerID"]%>';
                if(eval('opener.document.forms[0].' + lObjName))
                {
                    eval('opener.document.forms[0].' + lObjName).value = pCustomerID;
                }
                var lObjName = '<%= Request.QueryString["hdnCustomerName"]%>';
                if(eval('opener.document.forms[0].' + lObjName))
                {
                    eval('opener.document.forms[0].' + lObjName).value = pCustomerName;
                }
            }
            window.close();
        }
    </script>
</head>
<body>
    <form id="frmLookUpCustomer" runat="server">
        <input type="hidden" id="hdnCustomerID" runat="server" name="hdnCustomerID" class="inptype"/>
	    <input type="hidden" id="hdnCustomerName" runat="server" name="hdnCustomerName" class="inptype"/>

        <table border="0" cellpadding="2" cellspacing="1" width="95%">
            <tr>
                <td>
                    <p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlSearch" runat="server" Width="100%" >
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
                            <asp:ListItem Value="a.CustCode">Customer ID</asp:ListItem>
                            <asp:ListItem Value="a.CustName">Customer Name</asp:ListItem>
                        </asp:DropDownList>&nbsp;
                        <asp:TextBox runat="server" ID="txtSearchBy" Width="150px" CssClass="inptype"></asp:TextBox>          
                    </td>                    
                    <td class="tdganjil"></td>                    
                </tr>
                <tr>
                    <td class="tdganjil" colspan="2" align="center"></td>
                    <td class="tdganjil" align="right">
                        <asp:ImageButton ID="btnSearch" runat="server" 
                            ImageUrl="~/Images/button/buttonSearch.gif" OnClick="btnSearch_Click"  />&nbsp;
                        <asp:ImageButton ID="btnReset" runat="server" 
                            ImageUrl="~/Images/button/buttonReset.gif" OnClick="btnReset_Click"/>
                    </td>                     
                </tr>
            </table> 
        </asp:Panel>
        <asp:Panel ID="pnlListReceiptCode" runat="server" Width="100%" >
            <table runat="server" border="0" cellpadding="0" cellspacing="0" width="95%">
                <tr>
                    <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                    <td align="center" class="tdtopi">LIST CUSTOMER</td>
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
                            onitemdatabound="dgListData_ItemDataBound" >
                            <SelectedItemStyle CssClass="tdgenap"></SelectedItemStyle>
                            <AlternatingItemStyle CssClass="tdgenap"></AlternatingItemStyle>
                            <ItemStyle CssClass="tdganjil"></ItemStyle>
                            <HeaderStyle CssClass="tdjudul" HorizontalAlign="Center" Height="30px"></HeaderStyle>
                            <Columns>                                
                                <asp:TemplateColumn HeaderText="SELECT" ItemStyle-HorizontalAlign="center"  HeaderStyle-Width="7%">
                                    <ItemTemplate>                                        
                                        <input id="rdPilih" name="rbSelect" 
                                            onclick="sendRequest('<%# DataBinder.Eval(Container.DataItem, "custcode") %>','<%# DataBinder.Eval(Container.DataItem, "custname") %>');"
                                            type="radio" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                
                                <asp:TemplateColumn HeaderText="CUSTOMER ID" ItemStyle-HorizontalAlign="Left" SortExpression ="a.custcode"  HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblcustid" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"custcode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CUSTOMER NAME" ItemStyle-HorizontalAlign="Center" SortExpression="a.custname" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblcustname" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"custname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CUSTOMER ADDRESS" ItemStyle-HorizontalAlign="Center" SortExpression="a.custaddress" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblcustaddress" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"custaddress") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="FAKTUR PAJAK" ItemStyle-HorizontalAlign="Center" SortExpression="a.CustFakturPajak" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblfakturpajak" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"CustFakturPajak") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                                              
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>                
                <tr>
                    <td>
                        <uc2:ucPaging runat="server" id="pagingReceiptCode" 
                            OnNavigationButtonClicked="NavigationButtonClicked" PageSize="10"  ></uc2:ucPaging>        
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
