<%@ Page Title="" Language="C#" MasterPageFile="~/PageSetting/MsPageBlank.master" AutoEventWireup="true" CodeFile="ad_menu.aspx.cs" Inherits="pj_ad_ad_menu" %>
<%@ Register src="../usercontroller/ucPaging.ascx" tagname="ucPaging" tagprefix="uc2" %>
<%@ Register src="../UserController/ValidDate.ascx" tagname="ValidDate" tagprefix="uc1" %>
<%@ Register src="../UserController/ucInputNumber.ascx" tagname="ucInputNumber" tagprefix="uc3" %>
<%@ Register src="../UserController/ucLookUp_ADMenu.ascx" tagname="ucLookUpMenu" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <link href="../script/NewStyle.css" type="text/css" rel="stylesheet" />
    <script src="../script/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="../script/JavaScript/Eloan.js" type="text/javascript"></script>

    <form runat="server">
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
                    <td><asp:ImageButton id="btNewRecord" ToolTip="NewRecord" 
                            ImageUrl="~/images/toolbar/new.jpg" runat="server" 
                            onclick="btNewRecord_Click" />&nbsp;
                        <asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" Visible="false" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');" OnClick="btSaveRecord_Click" />&nbsp;
                        <asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" 
                            ImageUrl="~/images/toolbar/find.jpg" runat="server" 
                            onclick="btSearchRecord_Click" />&nbsp;
                        <asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" 
                            OnClick="btCancelOperation_Click" CausesValidation="false"/>    
                    </td>
                </tr>                      
            </table>
        <hr />
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
                    <td class="tdganjil" width="17%">Search  By</td>
                    <td class="tdganjil" width="33%">
                        <asp:DropDownList runat="server" ID="ddlSearchBy">
                            <asp:ListItem Value="">Select One</asp:ListItem>                        
                            <asp:ListItem Value="menu_id">MENU ID</asp:ListItem>
                            <asp:ListItem Value="menu_name">DESCRIPTION</asp:ListItem>
                        </asp:DropDownList>&nbsp;
                        <asp:TextBox ID="txtSearchBy" runat="server" Width="200px" CssClass="inptype"></asp:TextBox>                    
                    </td>                    
                </tr>
                <tr>
                    <td class="tdganjil" colspan="2" width="40%"></td>
                    <td class="tdganjil" align="right">
                        <asp:ImageButton ID="btnSearch" runat="server" 
                            ImageUrl="../Images/button/buttonSearch.gif" onclick="btnSearch_Click" />&nbsp;
                        </td>                    
                </tr>
            </table>   
            <hr />         
        </asp:Panel>
        <asp:Panel ID="pnlGrid" runat="server" Width="100%" >
            <table runat="server" cellSpacing="0" cellPadding="0" width="95%" border="0" >
                <TR class="trtopi">
                    <TD class="tdtopikiri" width="10" height="20">&nbsp;</TD>
                    <TD class="tdtopi" align="center">MENU LIST</TD>
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
                            onitemdatabound="dgListData_ItemDataBound" OnItemCommand="dgListData_ItemCommand">
                            <SelectedItemStyle CssClass="tdgenap"></SelectedItemStyle>
                            <AlternatingItemStyle CssClass="tdgenap"></AlternatingItemStyle>
                            <ItemStyle CssClass="tdganjil"></ItemStyle>
                            <HeaderStyle CssClass="tdjudul" HorizontalAlign="Center" Height="30px"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="EDIT" HeaderStyle-Width="5%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imbEdit" runat="server" ImageUrl="~/images/toolbar/edit.jpg" CommandName="Edit" CausesValidation="false" />                        
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DELETE" HeaderStyle-Width="5%">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imbDelete" runat="server" ImageUrl="~/images/toolbar/delete.jpg" CommandName="Delete" CausesValidation="false" />                        
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="MENU ID" ItemStyle-HorizontalAlign="Left" SortExpression="menu_id" HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label id="lblMenuID" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"menu_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="MENU NAME" ItemStyle-HorizontalAlign="Left" SortExpression="menu_name" HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label id="lblMenuName" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"menu_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                
                                <asp:TemplateColumn HeaderText="PATH MENU" ItemStyle-HorizontalAlign="Left" SortExpression="menu_path" HeaderStyle-Width="25%">
                                    <ItemTemplate>
                                        <asp:Label id="lblPathmenu" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"menu_path") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                
                                <asp:TemplateColumn HeaderText="LEVEL" ItemStyle-HorizontalAlign="Center" SortExpression="menu_level" HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label id="lblMenuLevel" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"menu_level") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                
                                <asp:TemplateColumn HeaderText="FLAG MENU" ItemStyle-HorizontalAlign="Center" SortExpression="menu_flag" HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label id="lblMenuFlag" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"menu_flag") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                
                                
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>                
                <tr>
                    <td>
                        <uc2:ucPaging runat="server" id="pagingMenu" 
                            OnNavigationButtonClicked="NavigationButtonClicked" PageSize="20"  ></uc2:ucPaging>        
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlInput" runat="server" Width="100%" Visible="false" >
            <table runat="server" cellSpacing="0" cellPadding="0" width="100%" border="0" >
                <TR class="trtopi">
                    <TD class="tdtopikiri" width="10" height="20">&nbsp;</TD>
                    <TD class="tdtopi" align="center">MENU INPUT</TD>
                    <TD class="tdtopikanan" width="10">&nbsp;</TD>
                </TR>
            </table>
             <table runat="server" cellSpacing="1" cellPadding="2" width="100%" border="0" >
                <tr>
                    <td class="tdganjil" width="20%">
                        Entity ID
                    </td>
                    <td class="tdganjil" width="30%">
                        <asp:DropDownList id="ddlEntity" runat="server" CssClass="inptypemandatory">
                            <asp:ListItem Value="ISS" Selected="True">ISS</asp:ListItem>
                        </asp:DropDownList>                                                
                    </td>
                    <td class="tdganjil" colspan="2">
                        
                    </td>
                </tr>    
                <tr>
                    <td class="tdganjil" width="20%">
                        Menu ID
                    </td>
                    <td class="tdganjil" width="30%">
                        <asp:Label runat="server" id="lblMenuID" ForeColor="OrangeRed" Font-Bold="true">[Auto Generate]</asp:Label>                                                 
                    </td>
                    <td class="tdganjil" width="20%">
                        Menu Name
                    </td>
                    <td class="tdganjil">
                        <asp:TextBox runat="server" ID="txtMenuName" Width="200px" CssClass="inptypemandatory"></asp:TextBox>
                    </td>
                </tr>     
                <tr>
                    <td class="tdganjil" >
                        Parent Menu
                    </td>
                    <td class="tdganjil" >
                        <uc4:ucLookUpMenu runat="server" ID="ucLUMenu" />
                        <%--<asp:DropDownList runat="server" ID="ddlParentMenu" CssClass="inptypemandatory">
                            <asp:ListItem Value="">Select One</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvParentMenu" runat="server" ErrorMessage="Please Fill Parent Menu" ForeColor="Red" ControlToValidate="ddlParentMenu"></asp:RequiredFieldValidator>--%>
                    </td>
                    <td class="tdganjil" >
                        Flag Menu
                    </td>
                    <td class="tdganjil">
                        <asp:DropDownList runat="server" ID="ddlFlagMenu" CssClass="inptypemandatory">
                            <asp:ListItem Value="">Select One</asp:ListItem>
                            <asp:ListItem Value="SM">SUB MENU</asp:ListItem>
                            <asp:ListItem Value="MN">MENU</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvFalgMenu" runat="server" ErrorMessage="Please Fill Flag Menu" ForeColor="Red" ControlToValidate="ddlFlagMenu"></asp:RequiredFieldValidator>
                    </td>
                </tr>     
                <tr>
                    <td class="tdganjil" >
                        Trans Type
                    </td>
                    <td class="tdganjil" >
                        <asp:DropDownList runat="server" ID="ddlTransType" CssClass="inptypemandatory">
                            <asp:ListItem Value="">Select One</asp:ListItem>
                            <asp:ListItem Value="AD">ADMINISTRATION</asp:ListItem>
                            <asp:ListItem Value="MS">MASTER</asp:ListItem>
                            <asp:ListItem Value="TR">TRANSACTION</asp:ListItem>
                            <asp:ListItem Value="PS">POSTING</asp:ListItem>
                            <asp:ListItem Value="RP">REPORT</asp:ListItem>

                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvTransType" runat="server" ErrorMessage="Please Fill Trans Type" ForeColor="Red" ControlToValidate="ddlTransType"></asp:RequiredFieldValidator>
                    </td>
                    <td class="tdganjil" >
                        Parameter Menu
                    </td>
                    <td class="tdganjil">
                        <asp:TextBox runat="server" ID="txtParam" Width="200px" CssClass="inptype"></asp:TextBox>
                    </td>
                </tr>    
                <tr>
                    <td class="tdganjil" >
                        Menu SysID
                    </td>
                    <td class="tdganjil" >
                        <asp:TextBox runat="server" ID="txtMenuSysID" Width="100px" CssClass="inptype"></asp:TextBox>    
                    </td>
                    <td class="tdganjil" >
                        Menu Path
                    </td>
                    <td class="tdganjil" >
                        <asp:TextBox runat="server" ID="txtMenuPath" Width="200px" CssClass="inptype"></asp:TextBox>    
                    </td>
                </tr>   
            </table>
        </asp:Panel>
        <%--<asp:Panel ID="pnlSave" runat="server" Visible="false" Width="100%">
            <table runat="server" cellSpacing="1" cellPadding="2" width="60%" border="0">
                    <tr>
                        <td width="50%"  class="tdganjil" align="right">                            
                        </td>                        
                        <td class="tdganjil" align="right">                            
                            <asp:ImageButton runat="server" ID="imbBack" CausesValidation="false"
                                ImageUrl="~/images/button/buttonBack.gif" onclick="imbBack_Click" />&nbsp;
                            <asp:ImageButton runat="server" ID="imbSave" 
                                ImageUrl="~/images/button/buttonsave.gif" onclick="imbSave_Click" />
                        </td>
                    </tr>
                </table>
        </asp:Panel>--%>


    </form>
</asp:Content>

