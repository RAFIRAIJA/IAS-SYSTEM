<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LookUp_INVMessanger.aspx.cs" Inherits="usercontroller_form_LookUp_INVMessanger" %>
<%@ Register src="~/usercontroller/ucPaging.ascx" tagname="ucPaging" tagprefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LookUp Messanger</title>
    <link href="~/script/NewStyle.css" type="text/css" rel="stylesheet" />
    <script src="~/Include/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="~/Include/JavaScript/Eloan.js" type="text/javascript"></script>
    <script type="text/javascript">
        function WinClose() {
            Window.close();
        }
    </script>
    <script language="javascript">
        function sendRequest(pMessangerID, pMessangerName) {
            with (document.forms) {
                var lObjName = '<%= Request.QueryString["txtMessangerID"]%>';
                if (eval('opener.document.forms[0].' + lObjName)) {
                    eval('opener.document.forms[0].' + lObjName).value = pMessangerID;
                }
                var lObjName = '<%= Request.QueryString["txtMessangerName"]%>';
                if (eval('opener.document.forms[0].' + lObjName)) {
                    eval('opener.document.forms[0].' + lObjName).value = pMessangerName;
                }
                var lObjName = '<%= Request.QueryString["hdnMessangerID"]%>';
                if (eval('opener.document.forms[0].' + lObjName)) {
                    eval('opener.document.forms[0].' + lObjName).value = pMessangerID;
                }
                var lObjName = '<%= Request.QueryString["hdnMessangerName"]%>';
                if (eval('opener.document.forms[0].' + lObjName)) {
                    eval('opener.document.forms[0].' + lObjName).value = pMessangerName;
                }
            }
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" id="hdnMessangerID" runat="server" name="hdnMessangerID" class="inptype"/>
	    <input type="hidden" id="hdnMessangerName" runat="server" name="hdnMessangerName" class="inptype"/>

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
                            <asp:ListItem Value="a.inv_messangerid">Messanger ID</asp:ListItem>
                            <asp:ListItem Value="a.inv_messangername">Messanger Name</asp:ListItem>
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
                            ImageUrl="~/Images/button/buttonReset.gif"/>
                    </td>                     
                </tr>
            </table> 
        </asp:Panel>
        <asp:Panel ID="pnlMessanger" runat="server" Width="100%"  Visible ="true" >
                <table id="Table8" runat="server" border="0" cellpadding="0" cellspacing="0" width="95%">
                    <tr>
                        <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                        <td align="center" class="tdtopi">LIST MESSANGER</td>
                        <td width="10" class="tdtopikanan">&nbsp;</td>
                    </tr>
                    </table>
                <table id="Table9" runat="server" cellSpacing="0" cellPadding="0" width="95%" border="0">
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
                                                onclick="sendRequest('<%# DataBinder.Eval(Container.DataItem, "inv_messangerid") %>    ','<%# DataBinder.Eval(Container.DataItem, "inv_messangername") %>    ');"
                                                type="radio" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="MESSANGER CODE" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblMsCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "inv_messangerid") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="15%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="MESSANGER TYPE" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblMsType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "messangertype") %>'></asp:Label>
                                        </ItemTemplate>                                        
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>   
                                    <asp:TemplateColumn HeaderText="MESSANGER NAME" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblMsName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "inv_messangername") %>'></asp:Label>
                                        </ItemTemplate>                                        
                                        <ItemStyle HorizontalAlign="Center" />
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
