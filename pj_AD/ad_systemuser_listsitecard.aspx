<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ad_systemuser_listsitecard.aspx.cs" Inherits="pj_AD_ad_systemuser_listsitecard" %>
<%@ Register src="../usercontroller/ucPaging.ascx" tagname="ucPaging" tagprefix="uc2" %>
<%@ Register src="../UserController/ValidDate.ascx" tagname="ValidDate" tagprefix="uc1" %>
<%@ Register src="../UserController/ucInputNumber.ascx" tagname="ucInputNumber" tagprefix="uc4" %>
<%@ Register src="../UserController/ucLookUp_ADGroupMenu.ascx" tagname="ucGroupMenu" tagprefix="uc3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../script/NewStyle.css" type="text/css" rel="stylesheet" />
    <script src="../script/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="../script/JavaScript/Eloan.js" type="text/javascript"></script>
    <script type="text/javascript" Language="Javascript">
    function WindowClose(url, nama, width, height) {
        OpenWin = this.open(url, nama);
        if (OpenWin != null) {
            if (OpenWin.opener == null)
                OpenWin.opener = self;
        }
        OpenWin.focus();
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="2" cellspacing="1" width="100%">
            <tr>
                <td>
                    <p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p>
                </td>
            </tr>
        </table>
    <asp:Panel ID="pnlSearch" runat="server" Width="100%" Visible="true" >
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
                    <td class="tdganjil" colspan="2">
                        <asp:DropDownList runat="server" ID="ddlSearchBy">
                            <asp:ListItem Value="">Select One</asp:ListItem>                        
                            <asp:ListItem Value="a.SitecardID">SITECARD</asp:ListItem>
                        </asp:DropDownList>&nbsp;
                        <asp:TextBox ID="txtSearchBy" runat="server" Width="200px" CssClass="inptype"></asp:TextBox>                    
                    </td>                    
                </tr>
                <tr>
                    <td class="tdganjil" colspan="2" width="40%"></td>
                    <td class="tdganjil" align="right">
                        <asp:ImageButton ID="btnSearch" runat="server" 
                            ImageUrl="../Images/button/buttonSearch.gif" onclick="btnSearch_Click" />&nbsp;
                        <asp:ImageButton ID="btnReset" runat="server" 
                            ImageUrl="../Images/button/buttonReset.gif" onclick="btnReset_Click" />
                    </td>                    
                </tr>
            </table>   
            <hr />         
        </asp:Panel>
        <asp:Panel ID="pnlGrid" runat="server" Width="100%" >
            <table runat="server" cellSpacing="0" cellPadding="0" width="95%" border="0" >
                <TR class="trtopi">
                    <TD class="tdtopikiri" width="10" height="20">&nbsp;</TD>
                    <TD class="tdtopi" align="center">LIST SITECARD</TD>
                    <TD class="tdtopikanan" width="10">&nbsp;</TD>
                </TR>
            </table>
            <table runat="server" cellSpacing="1" cellPadding="2" width="95%" border="0">
                <tr>
                    <td width="17%" class="tdganjil">
                        NIK
                    </td>
                    <td class="tdganjil" WIDTH="30%">
                        <asp:Label runat="server" ID="lblNIK"></asp:Label>
                    </td>
                    <td width="17%" class="tdganjil">
                        Nama
                    </td>
                    <td class="tdganjil" >
                        <asp:Label runat="server" ID="lblNama"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td class="tdganjil">
                        Group Menu
                    </td>
                    <td class="tdganjil">
                        <asp:Label runat="server" ID="lblGroupMenu"></asp:Label>
                    </td>
                    <td class="tdganjil">
                        Branch
                    </td>
                    <td class="tdganjil">
                        <asp:Label runat="server" ID="lblBranch"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="17%" class="tdganjil">
                        Entity
                    </td>
                    <td class="tdganjil">
                        <asp:Label runat="server" ID="lblEntity"></asp:Label>
                    </td>
                </tr>
            </table>
            <table runat="server" cellSpacing="0" cellPadding="0" width="95%" border="0">
                <tr>
                    <td>
                        <asp:DataGrid ID="dgSitecardList" runat="server" AutoGenerateColumns="False" 
                            AllowSorting="true" borderwidth="0px"
                            Width="100%" CssClass="tablegrid" 
                            CellPadding="3" CellSpacing="1" 
                            onsortcommand="dgSitecardList_SortCommand" 
                            OnItemCommand ="dgSitecardList_ItemCommand" 
                            onitemdatabound="dgSitecardList_ItemDataBound">
                            <SelectedItemStyle CssClass="tdgenap"></SelectedItemStyle>
                            <AlternatingItemStyle CssClass="tdgenap"></AlternatingItemStyle>
                            <ItemStyle CssClass="tdganjil"></ItemStyle>
                            <HeaderStyle CssClass="tdjudul" HorizontalAlign="Center" Height="30px"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="SELECT" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="3%">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkSelect" />
                                        <asp:Label id="lblSelect" runat="server" Visible="false" Text="0"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="SITECARD" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblsitecardID" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"sitecardID") %>'></asp:Label>
                                        <asp:Label id="lblFlagSelect" runat="server" Visible="false" text='<%# DataBinder.Eval(Container.DataItem,"FlagSelect") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                                                                                
                                <asp:TemplateColumn HeaderText="SITECARD NAME" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label id="lblsitecardName" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"sitecardname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                                                                                
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>                
                <tr>
                    <td>
                        <uc2:ucPaging runat="server" id="pagingSitecard" 
                            OnNavigationButtonClicked="NavigationButtonClicked" PageSize="20"  ></uc2:ucPaging>        
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:ImageButton runat="server" ID="imbSaveSelected" ImageUrl ="../Images/button/buttonsaveselectednew.gif" OnClick="imbSaveSelected_Click" />&nbsp;
                        <asp:ImageButton runat="server" ID="imbnext" Visible="false" ImageUrl ="../Images/button/buttonnext.gif" OnClick="imbnext_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:ImageButton runat="server" ID="imbSave" Visible="false" ImageUrl ="../Images/button/buttonsave.gif" OnClick="imbSave_Click" />&nbsp;
                        <asp:ImageButton runat="server" ID="imbExit" Visible="false" ImageUrl ="../Images/button/buttonexit.gif" OnClick="imbExit_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
