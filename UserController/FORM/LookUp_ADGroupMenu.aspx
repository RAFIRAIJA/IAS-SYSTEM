<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LookUp_ADGroupMenu.aspx.cs" Inherits="UserController_FORM_LookUp_ADGroupMenu" %>
<%@ Register src="~/usercontroller/ucPaging.ascx" tagname="ucPaging" tagprefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Look Up Group Menu</title>
    <link href="~/script/NewStyle.css" type="text/css" rel="stylesheet" />
    <script src="~/Include/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="~/Include/JavaScript/Eloan.js" type="text/javascript"></script>
    <script type="text/javascript">
        function WinClose() {
            Window.close();
        }
    </script>
    <script language="javascript">
        function sendRequest(pGroupmenuHid, pGroupmenu) {
            with (document.forms) {
                var lObjName = '<%= Request.QueryString["txtGroupMenu"]%>';
                if (eval('opener.document.forms[0].' + lObjName)) {
                    eval('opener.document.forms[0].' + lObjName).value = pGroupmenu;
                }
                var lObjName = '<%= Request.QueryString["hdnGroupMenu"]%>';
                if (eval('opener.document.forms[0].' + lObjName)) {
                    eval('opener.document.forms[0].' + lObjName).value = pGroupmenuHid;
                }
            }
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" id="hdnMenuParentID" runat="server" name="hdnMenuParentID" class="inptype"/>
	    <input type="hidden" id="hdnMenuParentName" runat="server" name="hdnMenuParentName" class="inptype"/>
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
                    <td class="tdganjil" width="17%">Search  By</td>
                    <td class="tdganjil" width="33%">
                        <asp:DropDownList runat="server" ID="ddlSearchBy">
                            <asp:ListItem Value="">Select One</asp:ListItem>                        
                            <asp:ListItem Value="gm.GroupMenu">GROUP ID</asp:ListItem>
                        </asp:DropDownList>&nbsp;
                        <asp:TextBox ID="txtSearchBy" runat="server" Width="150px" CssClass="inptype"></asp:TextBox>                    
                    </td>                    
                </tr>
                <tr>
                    <td class="tdganjil" colspan="2" width="40%"></td>
                    <td class="tdganjil" align="right">
                        <asp:ImageButton ID="btnSearch" runat="server" 
                            ImageUrl="~/Images/button/buttonSearch.gif" OnClick="btnSearch_Click" />&nbsp;
                        <asp:ImageButton ID="btnReset" runat="server" 
                            ImageUrl="~/Images/button/buttonReset.gif" />
                    </td>                    
                </tr>
            </table>   
            <hr />         
        </asp:Panel>
        <asp:Panel ID="pnlGrid" runat="server" Width="100%" >
            <table runat="server" cellSpacing="0" cellPadding="0" width="95%" border="0" >
                <TR class="trtopi">
                    <TD class="tdtopikiri" width="10" height="20">&nbsp;</TD>
                    <TD class="tdtopi" align="center">GROUP MENU LIST</TD>
                    <TD class="tdtopikanan" width="10">&nbsp;</TD>
                </TR>
            </table>
            <table runat="server" cellSpacing="0" cellPadding="0" width="95%" border="0">
                <tr>
                    <td>
                        <asp:DataGrid ID="dgListData" runat="server" AutoGenerateColumns="False" 
                            AllowSorting="true" borderwidth="0px"
                            Width="100%" CssClass="tablegrid" 
                            CellPadding="3" CellSpacing="1" 
                            onsortcommand="dgListData_SortCommand" 
                            onitemdatabound="dgListData_ItemDataBound" >
                            <SelectedItemStyle CssClass="tdgenap"></SelectedItemStyle>
                            <AlternatingItemStyle CssClass="tdgenap"></AlternatingItemStyle>
                            <ItemStyle CssClass="tdganjil"></ItemStyle>
                            <HeaderStyle CssClass="tdjudul" HorizontalAlign="Center" Height="30px"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="SELECT" ItemStyle-HorizontalAlign="center"  HeaderStyle-Width="5%">
                                    <ItemTemplate>                                        
                                        <input id="rdPilih" name="rbSelect" 
                                            onclick="sendRequest('<%# DataBinder.Eval(Container.DataItem, "groupmenu") %>    ',
                                                                 '<%# DataBinder.Eval(Container.DataItem, "description") %>    ');"
                                            type="radio" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>   
                                <asp:TemplateColumn HeaderText="GROUP ID" ItemStyle-HorizontalAlign="Left" SortExpression="groupmenu" HeaderStyle-Width="8%">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlgroupid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "groupmenu") %>' NavigateUrl="#"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="GROUP NAME" ItemStyle-HorizontalAlign="Center" SortExpression="description" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblgroupname" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"description") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>                
                <tr>
                    <td>
                        <uc2:ucPaging runat="server" id="pagingGroupMenu" 
                            OnNavigationButtonClicked="NavigationButtonClicked" PageSize="20"  ></uc2:ucPaging>        
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
